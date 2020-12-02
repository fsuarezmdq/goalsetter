using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Goalsetter.Domains;
using Microsoft.EntityFrameworkCore;

namespace Goalsetter.DataAccess.Repositories
{
    public interface IVehicleRepository
    {
        DbSet<Vehicle> Vehicles { get; }
        Task<IEnumerable<Vehicle>> GetAsync();
        Task<Vehicle> GetByIdAsync(Guid guid);
        void Save(Vehicle vehicle);
        void Add(Vehicle vehicle);
    }
}
