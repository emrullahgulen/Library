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
    public class AuthorService : IAuthorService
    {
        public IAuthorDAL _authorDAL;
        private readonly ILogger<BookService> _logger;

        public AuthorService(IAuthorDAL authorDAL,ILogger<BookService> logger)
        {
            _authorDAL = authorDAL;
            _logger = logger;
        }
        public IResult AddAuthor(Author author)
        {
            try
            {
                _authorDAL.Insert(author);
                return new SuccessDataResult<Author>(Messages.AuthorInsert);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<Author>(ex.Message);
            }
        }

        public IDataResult<Author> GetAuthorById(int Id)
        {
            try
            {
                var result = _authorDAL.Get(x => x.Id == Id);
                return new SuccessDataResult<Author>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<Author>(ex.Message);
            }
        }

        public IResult RemoveAuthor(Author author)
        {
            try
            {
                _authorDAL.Delete(author);
                return new SuccessDataResult<Author>(Messages.AuthorDelete);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<Author>(ex.Message);
            }
        }

        public IResult UpdateAuthor(Author author)
        {
            try
            {
                author.UpdateDate = DateTime.Now;
                _authorDAL.Update(author);
                return new SuccessDataResult<Author>(Messages.AuthorUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<Author>(ex.Message);
            }
        }

        public IDataResult<List<Author>> GetAuthorList()
        {
            try
            {
                var List= _authorDAL.GetList();
                return new SuccessDataResult<List<Author>>((List<Author>)List);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<List<Author>>(ex.Message);
            }
        }

        public IResult RemoveAuthorById(int Id)
        {
            try
            {
                using (var _context = new LibraryDbContext())
                {
                    Author author = _context.Authors.FirstOrDefault(x => x.Id == Id);
                    RemoveAuthor(author);
                    return new SuccessDataResult<Book>(Messages.AuthorDelete);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<Book>(ex.Message);
            }
        }
    }
}
