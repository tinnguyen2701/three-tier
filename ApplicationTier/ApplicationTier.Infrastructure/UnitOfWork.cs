using ApplicationTier.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationTier.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction _transaction;
        private IsolationLevel? _isolationLevel;
        private Dictionary<string, object> Repositories { get; }

        public DbContext DbContext { get; private set; }


        public UnitOfWork(DbFactory dbFactory)
        {
            DbContext = dbFactory.DbContext;
            Repositories = new Dictionary<string, dynamic>();
        }


        public async Task BeginTransaction()
        {
            if (_transaction == null)
            {
                _transaction = _isolationLevel.HasValue
                    ? await DbContext.Database.BeginTransactionAsync(_isolationLevel.GetValueOrDefault())
                    : await DbContext.Database.BeginTransactionAsync();
            }
        }

        public async Task CommitTransaction()
        {
            await DbContext.SaveChangesAsync();

            if (_transaction == null) return;

            await _transaction.CommitAsync();

            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task RollbackTransaction()
        {
            if (_transaction == null) return;

            await _transaction.RollbackAsync();

            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await DbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            if (DbContext == null) return;

            if (DbContext.Database.GetDbConnection().State == ConnectionState.Open)
            {
                DbContext.Database.GetDbConnection().Close();
            }

            DbContext.Dispose();
            DbContext = null;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            var entity = typeof(T);
            var entityName = entity.Name;

            lock (Repositories)
            {
                if (Repositories.ContainsKey(entityName))
                {
                    return (IRepository<T>)Repositories[entityName];
                }

                var repository = new Repository<T>(DbContext);

                Repositories.Add(entityName, repository);
                return repository;
            }
        }
    }
}
