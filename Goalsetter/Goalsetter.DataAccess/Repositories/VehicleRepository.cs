using Goalsetter.AppServices;
using Goalsetter.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goalsetter.DataAccess.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IApplicationContext _applicationContext;

        public VehicleRepository(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }              

        public async Task<Vehicle> GetByIdAsync(Guid guid)
        {
            return await _applicationContext.Vehicles.Where(p => p.Id == guid)
                .Include(p => p.RentalPrice)
                .Include(p => p.Rentals)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetAsync()
        {
            return await _applicationContext.Vehicles
                .Include(p=> p.RentalPrice)
                .Include(p=>p.Rentals)
                    .ThenInclude(p=> p.Client)
                .Where(p=> p.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public void Save(Vehicle vehicle)
        {
            _applicationContext.Vehicles.Update(vehicle);
        }
        public void Add(Vehicle vehicle)
        {
            _applicationContext.Vehicles.Add(vehicle);
        }    
    }
}
