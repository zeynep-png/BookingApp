using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Data.UnifOfWork
{
    // Interface for Unit of Work pattern, implementing IDisposable for resource management
    public interface IUnitOfWork : IDisposable
    {
        // Asynchronously saves all changes made in the context
        Task<int> SaveChangesAsync();

        // Begins a new transaction
        Task BeginTransaction();

        // Commits the current transaction
        Task CommitTransaction();

        // Rolls back the current transaction in case of an error
        Task RollBackTransaction();
    }
}