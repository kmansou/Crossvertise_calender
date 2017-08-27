using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Crossvertise.Calender.DAL.Domain.Repository
{
    public interface IReadOnlyRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets entity by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        TEntity Get(int id);

        /// <summary>
        /// Gets many entities that matches where clause
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Generic method to check if entity exists
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        bool Exists(int primaryKey);

        /// <summary>
        /// Gets a single record by the specified criteria (usually the unique identifier)
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record that matches the specified criteria</returns>
        TEntity GetSingle(Func<TEntity, bool> predicate);

        /// <summary>
        /// The first record matching the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record containing the first record matching the specified criteria</returns>
        TEntity GetFirst(Func<TEntity, bool> predicate);

        /// <summary>
        /// Inclue multiple
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetWithInclude(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate,
            params string[] include);
    }
}