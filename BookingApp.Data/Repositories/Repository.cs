using BookingApp.Data.Context;
using BookingApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Data.Repositories
{
    // Generic repository class implementing IRepository interface
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly BookingAppDbContext _db; // Database context for accessing the database
        private readonly DbSet<TEntity> _dbSet; // DbSet for managing entities of type TEntity

        // Constructor to initialize the repository with a database context
        public Repository(BookingAppDbContext db)
        {
            _db = db; // Assign the database context
            _dbSet = _db.Set<TEntity>(); // Set the DbSet for the entity type
        }

        // Adds a new entity to the repository
        public void Add(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now; // Set the creation date
            _dbSet.Add(entity); // Add the entity to the DbSet
            // _db.SaveChanges(); // Uncomment to save changes to the database
        }

        // Deletes an existing entity from the repository, supports soft delete
        public void Delete(TEntity entity, bool softDelete = true)
        {
            if (softDelete)
            {
                entity.ModifiedDate = DateTime.Now; // Set the modification date
                entity.IsDeleted = true; // Mark the entity as deleted
                _db.Update(entity); // Update the entity in the DbSet
                // _db.SaveChanges(); // Uncomment to save changes to the database
            }
            else
            {
                _dbSet.Remove(entity); // Remove the entity from the DbSet
            }
        }

        // Deletes an entity by its ID
        public void Delete(int id)
        {
            var entity = _dbSet.Find(id); // Find the entity by ID
            Delete(entity); // Call the Delete method
        }

        // Retrieves an entity matching the specified criteria
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate); // Return the first matching entity or null
        }

        // Retrieves all entities that match the specified criteria
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate is null ? _dbSet : _dbSet.Where(predicate); // Return all entities or filtered ones
        }

        // Retrieves an entity by its ID
        public TEntity GetById(int id)
        {
            return _dbSet.Find(id); // Find and return the entity by ID
        }

        // Updates an existing entity in the repository
        public void Update(TEntity entity)
        {
            entity.ModifiedDate = DateTime.Now; // Set the modification date
            _dbSet.Update(entity); // Update the entity in the DbSet
            // _db.SaveChanges(); // Uncomment to save changes to the database
        }
    }
}

// For transactions, the Unit of Work pattern will be used.