using Goalsetter.AppServices;
using Goalsetter.DataAccess.Repositories;
using System;
using System.Threading.Tasks;

namespace Goalsetter.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationContext _appContext;
        public IClientRepository ClientRepository { get; }
        public IVehicleRepository VehicleRepository { get; }
        public IRentalRepository RentalRepository { get; }

        private bool _disposed;

        public UnitOfWork(IApplicationContext appContext)
        {
            _appContext = appContext;

            ClientRepository = new ClientRepository(appContext);
            VehicleRepository = new VehicleRepository(appContext);
            RentalRepository = new RentalRepository(appContext);
        }

        public async Task CommitAsync()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            await _appContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _appContext.Dispose();
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
