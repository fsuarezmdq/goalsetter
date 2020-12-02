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
    public class UnitOfWork : IUnitOfWork
    {
        public AppContext AppContext { get; }

        private bool _disposed;

        public UnitOfWork(AppContext appContext)
        {
            AppContext = appContext;
        }

        public async Task Commit()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            await AppContext.SaveChangesAsync();
        }

        //public IQueryable<T> GetAsync<T>(Guid guid) where T : AggregateRoot
        //{
        //    return AppContext.Set<T>().Where(p => p.Id == guid);
        //}

        //public void Update<T>(T entity)
        //{
        //    AppContext.Update(entity);
        //}

        //public void Add<T>(T entity)
        //{
        //    AppContext.Add(entity);
        //}

        //public IQueryable<T> Query<T>() where T : AggregateRoot
        //{
        //    return AppContext.Set<T>();
        //}

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    AppContext.Dispose();
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
