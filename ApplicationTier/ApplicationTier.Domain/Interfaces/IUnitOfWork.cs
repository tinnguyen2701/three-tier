using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationTier.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext DbContext { get; }
        Task BeginTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        IRepository<T> Repository<T>() where T : class;
    }
}
