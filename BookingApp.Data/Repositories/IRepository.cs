using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Data.Repositories
{
    // Generic repository interface for managing TEntity type entities
    public interface IRepository<TEntity> where TEntity : class
    {
        // Adds a new entity to the repository
        void Add(TEntity entity);

        // Deletes an existing entity from the repository. Soft delete can be performed.
        void Delete(TEntity entity, bool softDelete = true);

        // Deletes an entity from the repository by ID
        void Delete(int id);

        // Updates an existing entity in the repository
        void Update(TEntity entity);

        // Retrieves an entity by the specified ID
        TEntity GetById(int id);

        // Retrieves an entity that matches the specified criteria
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        // Retrieves all entities that match the specified criteria (if any). 
        // If no criteria is provided, retrieves all entities.
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
    }
}