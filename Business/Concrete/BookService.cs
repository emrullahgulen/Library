using Business.Absract;
using Core;
using Core.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Entities;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BookService : IBookService
    {
        public IBookDAL _bookDAL;
        private readonly ILogger<BookService> _logger;
        public BookService(IBookDAL bookDAL, ILogger<BookService> logger)
        {
            _bookDAL = bookDAL;
            _logger = logger;
        }

        public IDataResult<Book> GetBookById(int Id)
        {
            try
            {
                var result = _bookDAL.Get(x => x.Id == Id);
                return new SuccessDataResult<Book>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<Book>(ex.Message);
            }
        }

        public IDataResult<List<BookListModel>> GetBookStatusList()
        {
            try
            {

                using (var _context = new LibraryDbContext())
                {
                    var bookList = from books in _context.Books
                                   join borrowedBook in _context.BorrowedBooks on books.Id equals borrowedBook.BookId into bookBorrowInfo
                                   from bookBorrowListResult in bookBorrowInfo.DefaultIfEmpty()
                                   join member in _context.Members on bookBorrowListResult.MemberId equals member.Id into memberInfo
                                   from memberResult in memberInfo.DefaultIfEmpty()
                                   join author in _context.Authors on books.AuthorId equals author.Id into authorInfo
                                   from authorResult in authorInfo.DefaultIfEmpty()
                                   select new BookListModel
                                   {
                                       Id = books.Id,
                                       Title = books.Title,
                                       Year = books.Year,
                                       PageCount = books.PageCount,
                                       AuthorName = authorResult.Name,
                                       PictureUrl = books.PictureUrl,
                                       StatusType = books.StatusType,
                                       BookType = books.BookType,
                                       BorrowedMemberName = books.StatusType == Entity.Enums.BookStatusType.Emanette ? memberResult.FullName : "",
                                       ReservedDate = books.StatusType == Entity.Enums.BookStatusType.Emanette ? books.ReservedDate : null,
                                       //  ReturnDate = books.StatusType == Entity.Enums.BookStatusType.Emanette ? bookBorrowListResult.ReturnDate:null, // geri getirilmesi listede yer alması anlamsız oldu. Zamanında geri getirilmeyen kitaplar için bir rapor yazılabilir

                                   };

                    return new SuccessDataResult<List<BookListModel>>((List<BookListModel>)bookList.OrderBy(x => x.Title).ToList());
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<List<BookListModel>>(ex.Message);
            }
        }
        public IResult AddBook(Book book)
        {
            try
            {
                _bookDAL.Insert(book);
                return new SuccessDataResult<Book>(Messages.BookInsert);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<Book>(ex.Message);
            }
        }
        public IResult RemoveBook(Book book)
        {
            try
            {
                _bookDAL.Delete(book);
                return new SuccessDataResult<Book>(Messages.BookDelete);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<Book>(ex.Message);
            }
        }

        public IResult UpdateBook(Book book)
        {
            try
            {
                book.UpdateDate = DateTime.Now;
                _bookDAL.Update(book);
                return new SuccessDataResult<Book>(Messages.BookUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<Book>(ex.Message);
            }
        }

        public IDataResult<List<BookModel>> GetBookList()
        {
            try
            {

                using (var _context = new LibraryDbContext())
                {
                    var bookList = from books in _context.Books
                                   join author in _context.Authors on books.AuthorId equals author.Id into authorInfo
                                   from authorResult in authorInfo.DefaultIfEmpty()
                                   select new BookModel
                                   {
                                       Id = books.Id,
                                       Title = books.Title,
                                       Year = books.Year,
                                       PageCount = books.PageCount,
                                       AuthorName = authorResult.Name,
                                       PictureUrl = books.PictureUrl,
                                       StatusType = books.StatusType,
                                       BookType = books.BookType,
                                   };

                    return new SuccessDataResult<List<BookModel>>((List<BookModel>)bookList.OrderBy(x => x.Title).ToList());
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<List<BookModel>>(ex.Message);
            }
        }

        public IResult RemoveBookById(int Id)
        {
            try
            {
                using (var _context = new LibraryDbContext())
                {
                    Book book = _context.Books.FirstOrDefault(x => x.Id == Id);
                    RemoveBook(book);
                    return new SuccessDataResult<Book>(Messages.BookDelete);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<Book>(ex.Message);
            }
        }

        public IResult AddBorrowedBook(Book book)
        {
            try
            {
                using (var _context = new LibraryDbContext())
                {
                    Book _book = _context.Books.AsNoTracking().Where(x => x.Id == book.Id).FirstOrDefault();
                    _book.BookedMemberId = book.BookedMemberId;
                    _book.ReservedDate = book.ReservedDate;
                    _book.StatusType = Entity.Enums.BookStatusType.Emanette;
                    _context.Books.Update(_book);

                    BookTransaction borrowedBook = new BookTransaction();
                    borrowedBook.BookId = book.Id;
                    borrowedBook.BorrowedDate = book.ReservedDate;
                    borrowedBook.MemberId = (int)book.BookedMemberId;
                   _context.Add(borrowedBook);
                    _context.SaveChanges();
                }

                return new SuccessDataResult<Book>(Messages.BookUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<Book>(ex.Message);
            }


        }
        public IResult TakeBackBorrowedBook(int Id)
        {
            try
            {
                using (var _context = new LibraryDbContext())
                {
                    Book _book = _context.Books.AsNoTracking().Where(x => x.Id == Id).FirstOrDefault();

                    BookTransaction borrowedBook = new BookTransaction();
                    borrowedBook.BookId = Id;
                    borrowedBook.ReturnDate = DateTime.Now;
                    borrowedBook.MemberId = (int)_book.BookedMemberId;
                    _context.Add(borrowedBook);

                    _book.BookedMemberId = null;
                    _book.ReservedDate = null;
                    _book.StatusType = Entity.Enums.BookStatusType.Rafta;
                    _context.Books.Update(_book);

                    _context.SaveChanges();
                }

                return new SuccessDataResult<Book>(Messages.BookUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<Book>(ex.Message);
            }


        }
    }
}
