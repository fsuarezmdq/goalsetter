using Goalsetter.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goalsetter.DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private readonly AppContext _dbContext;
        private bool _disposed;

        public UnitOfWork(AppContext appContext)
        {
            _dbContext = appContext;
        }

        public async Task Commit()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> GetAsync<T>(Guid guid) where T : AggregateRoot
        {
            return await _dbContext.Set<T>().SingleAsync(p => p.Id == guid);
        }

        public void Update<T>(T entity)
        {
            _dbContext.Update(entity);
        }

        public void Add<T>(T entity)
        {
            _dbContext.Add(entity);
        }

        public IQueryable<T> Query<T>() where T : AggregateRoot
        {
            return _dbContext.Set<T>();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }
}
