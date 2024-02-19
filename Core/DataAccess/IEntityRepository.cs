using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class,IEntity, new()
    {
        T Get(Expression<Func<T,bool>> expression);
        IList<T> GetList(Expression<Func<T, bool>> expression=null);

        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
