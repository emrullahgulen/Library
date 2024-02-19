using Business.Absract;
using Core;
using Core.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BookTransactionService:IBookTransactionService
    {
        public IBookTransactionDAL _borrowedBookDAL;
        private readonly ILogger<BookTransactionService> _logger;
        public BookTransactionService(IBookTransactionDAL borrowedBookDAL, ILogger<BookTransactionService> logger)
        {
            _borrowedBookDAL = borrowedBookDAL;
            _logger = logger;
        }

        public IResult AddBorrowedBook(BookTransaction borrowedBook)
        {
            try
            {
                _borrowedBookDAL.Insert(borrowedBook);
                return new SuccessDataResult<BookTransaction>(Messages.BorrowedBookInsert);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<BookTransaction>(ex.Message);
            }
        }

        public IResult RemoveBorrowedBook(BookTransaction borrowedBook)
        {
            try
            {
                _borrowedBookDAL.Delete(borrowedBook);
                return new SuccessDataResult<BookTransaction>(Messages.BorrowedBookDelete);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<BookTransaction>(ex.Message);
            }
        }

        public IResult UpdateBorrowedBook(BookTransaction borrowedBook)
        {
            try
            {
                borrowedBook.UpdateDate = DateTime.Now;
                _borrowedBookDAL.Update(borrowedBook);
                return new SuccessDataResult<BookTransaction>(Messages.BorrowedBookUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<BookTransaction>(ex.Message);
            }
        }

        public IResult RemoveBookById(int Id)
        {
            try
            {
                using (var _context = new LibraryDbContext())
                {
                    BookTransaction book = _context.BorrowedBooks.FirstOrDefault(x => x.Id == Id);
                    RemoveBorrowedBook(book);
                    return new SuccessDataResult<BookTransaction>(Messages.BookDelete);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<BookTransaction>(ex.Message);
            }
        }

        public IDataResult<BookTransaction> GetBorrowedBookById(int Id)
        {
            try
            {
                var result = _borrowedBookDAL.Get(x => x.Id == Id);
                return new SuccessDataResult<BookTransaction>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<BookTransaction>(ex.Message);
            }
        }
    }
}
