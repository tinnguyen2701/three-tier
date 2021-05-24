using ApplicationTier.Domain.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationTier.Infrastructure
{
    public class DbFactory : IDisposable
    {

        private bool _disposed;
        private DbContext _dbContext;
        private Func<DbContext> _instanceFunc;
        public DbContext DbContext => _dbContext ?? (_dbContext = _instanceFunc.Invoke());

        public DbFactory(Func<DemoContext> dbContext)
        {
            _instanceFunc = dbContext;
        }

        public void Dispose()
        {
            if (!_disposed && _dbContext != null)
            {
                _disposed = true;
                _dbContext.Dispose();
            }
        }
    }
}
