using System;
using System.Threading.Tasks;

namespace Goalsetter.AppServices
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepository ClientRepository { get; }

        IVehicleRepository VehicleRepository { get; }

        IRentalRepository RentalRepository { get; }

        Task CommitAsync();
    }
}
