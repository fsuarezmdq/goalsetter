using Goalsetter.Domains;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Goalsetter.AppServices
{
    public interface IApplicationContext
    {
        DbSet<Client> Clients { get; set; }
        DbSet<Rental> Rentals { get; set; }
        DbSet<VehiclePrice> VehiclePrices { get; set; }
        DbSet<Vehicle> Vehicles { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        
        void Dispose();
    }
}