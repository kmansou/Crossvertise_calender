using System.Data.Entity;
using Crossvertise.Calender.DAL.Domain.Repository;
using Crossvertise.Calender.DAL.EF.Context;

namespace Crossvertise.Calender.DAL.EF.Repository
{
    public class GenericReadWriteRepository<TEntity> : GenericReadOnlyRepository<TEntity>, IReadWriteRepository<TEntity>
        where TEntity : class
    {
        internal readonly CalenderDbContext Context;
        public GenericReadWriteRepository(CalenderDbContext context)
            : base(context)
        {
            Context = context;
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Delete(int id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
