using Core.DataAccess;
using Core.Results;
using DataAccess.Abstract;
using Entity.Entities;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFBookDAL : EFEntityRepositoryBase<Book, LibraryDbContext>, IBookDAL
    {

    }
}
