using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public class EFEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Insert(TEntity entity)
        {
            using (var _context = new TContext())
            {
                var insertEntity = _context.Entry(entity);
                insertEntity.State = EntityState.Added;
                _context.SaveChanges();
            }
        }
        public void Delete(TEntity entity)
        {
            using (var _context = new TContext())
            {
                var DeleteEntity = _context.Remove(entity);
                DeleteEntity.State = EntityState.Deleted;
                _context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> expression)
        {
            using (var _context = new TContext())
            {
                return _context.Set<TEntity>().SingleOrDefault(expression);
            }
        }

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> expression = null)
        {
            using (var _context = new TContext())
            {
                return expression == null
                    ? _context.Set<TEntity>().ToList()
                    : _context.Set<TEntity>().Where(expression).ToList();
            }
        }



        public void Update(TEntity entity)
        {
            using (var _context = new TContext())
            {
                var UpdateEntity = _context.Update(entity);
                UpdateEntity.State = EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}
