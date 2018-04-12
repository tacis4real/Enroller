using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioEnumerator.DataAccessManager.Infrastructure.Contract;

namespace BioEnumerator.DataAccessManager.Infrastructure
{
    internal class BioEnumeratorUoWork : IBioEnumeratorUoWork, IDisposable
    {

        private readonly BioEnumeratorContext _dbContext;

        public BioEnumeratorUoWork(BioEnumeratorContext context)
        {
            _dbContext = context;
        }

        public BioEnumeratorUoWork()
        {
            _dbContext = new BioEnumeratorContext();
        }

        public DbContextTransaction BeginTransaction()
        {
            return _dbContext.BioEnumeratorDbContext.Database.BeginTransaction();
        }


        public void SaveChanges()
        {
            _dbContext.BioEnumeratorDbContext.SaveChanges();
        }

        public BioEnumeratorContext Context { get { return _dbContext; } }


        #region Implementation of IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_disposed) return;
            _dbContext.Dispose();
            _disposed = true;
        }

        ~BioEnumeratorUoWork()
        {
            Dispose(false);
        }
        #endregion

        
    }
}
