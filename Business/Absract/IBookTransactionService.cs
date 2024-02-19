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
    public interface IBookTransactionService
    {
        IResult AddBorrowedBook(BookTransaction borrowedBook);
        IResult RemoveBorrowedBook(BookTransaction borrowedBook);
        IResult UpdateBorrowedBook(BookTransaction borrowedBook);
        IDataResult<BookTransaction> GetBorrowedBookById(int Id);
        IResult RemoveBookById(int Id);
    }
}
