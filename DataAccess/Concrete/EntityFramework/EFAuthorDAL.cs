using Core.DataAccess;
using DataAccess.Abstract;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFAuthorDAL : EFEntityRepositoryBase<Author, LibraryDbContext>, IAuthorDAL
    {
    }
}
