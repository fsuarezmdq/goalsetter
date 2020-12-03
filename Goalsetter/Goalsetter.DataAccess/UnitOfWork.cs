using System;
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
