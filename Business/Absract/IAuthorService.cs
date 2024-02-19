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
    public interface IAuthorService
    {
        IResult AddAuthor(Author author);
        IResult RemoveAuthor(Author author);
        IResult UpdateAuthor(Author author);
        IDataResult<Author> GetAuthorById(int Id);
        IDataResult<List<Author>> GetAuthorList();
        IResult RemoveAuthorById(int Id);
    }
}
