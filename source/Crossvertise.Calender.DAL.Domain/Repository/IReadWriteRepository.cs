namespace Crossvertise.Calender.DAL.Domain.Repository
{
    public interface IReadWriteRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        void Insert(TEntity entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        void Delete(int id);

        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="entityToDelete"></param>
        void Delete(TEntity entityToDelete);
    }
}
