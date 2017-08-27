using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Crossvertise.Calender.DAL.Domain.Repository;
using Crossvertise.Calender.DAL.EF.Context;

namespace Crossvertise.Calender.DAL.EF.Repository
{
    public class GenericReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : class
    {
        internal readonly DbSet<TEntity> DbSet;

        public GenericReadOnlyRepository(CalenderDbContext context)
        {
            this.DbSet = context.Set<TEntity>();
        }

        public virtual TEntity Get(int id)
        {
            return DbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return DbSet.Where(where).ToList();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public virtual bool Exists(int primaryKey)
        {
            return DbSet.Find(primaryKey) != null;
        }

        public virtual TEntity GetSingle(Func<TEntity, bool> predicate)
        {
            return DbSet.Single<TEntity>(predicate);
        }

        public virtual TEntity GetFirst(Func<TEntity, bool> predicate)
        {
            return DbSet.First<TEntity>(predicate);
        }

        public virtual IEnumerable<TEntity> GetWithInclude(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> query = this.DbSet;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate).ToList();
        }
    }
}
