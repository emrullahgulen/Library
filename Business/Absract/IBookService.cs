using Core.Results;
using Entity.Entities;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Absract
{
    public interface IBookService
    {
        IResult AddBook(Book book);
        IResult RemoveBook(Book book);
        IResult UpdateBook(Book book);
        IDataResult<List<BookListModel>> GetBookStatusList();
        IDataResult<Book> GetBookById(int Id);
        IDataResult<List<BookModel>> GetBookList();
        IResult RemoveBookById(int Id);
        IResult AddBorrowedBook(Book book);
        IResult TakeBackBorrowedBook(int Id);
    }
}
