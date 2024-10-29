using BookingApp.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Data.UnifOfWork
{
    // Implementation of the UnitOfWork pattern
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingAppDbContext _db; // Database context instance
        private IDbContextTransaction _transaction; // Transaction instance

        // Constructor accepting the database context
        public UnitOfWork(BookingAppDbContext db)
        {
            _db = db;
        }

        // Begins a new database transaction
        public async Task BeginTransaction()
        {
            _transaction = await _db.Database.BeginTransactionAsync();
        }

        // Commits the current transaction
        public async Task CommitTransaction()
        {
            await _transaction.CommitAsync();
        }

        // Disposes the database context to free resources
        public void Dispose()
        {
            _db.Dispose();
        }

        // Rolls back the current transaction in case of an error
        public async Task RollBackTransaction()
        {
            await _transaction.RollbackAsync();
        }

        // Saves changes made in the context asynchronously
        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
