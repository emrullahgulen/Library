using Core.DataAccess;
using Core.Results;
using Entity.Entities;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IBookDAL : IEntityRepository<Book>
    {

    }
}
