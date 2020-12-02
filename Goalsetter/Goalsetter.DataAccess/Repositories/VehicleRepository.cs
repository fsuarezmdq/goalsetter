using Goalsetter.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goalsetter.DataAccess.Repositories
{
    public class VehicleRepository : AppContext, IVehicleRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public VehicleRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }              

        public async Task<Vehicle> GetByIdAsync(Guid guid)
        {
            return await _unitOfWork.AppContext.Set<Vehicle>().Where(p => p.Id == guid)
                .Include(p => p.RentalPrice)
                .Include(p => p.Rentals)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetAsync()
        {
            return await _unitOfWork.AppContext.Set<Vehicle>()
                .Include(p=> p.RentalPrice)
                .Include(p=>p.Rentals)
                    .ThenInclude(p=> p.Client)
                .Where(p=> p.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public void Save(Vehicle vehicle)
        {
            _unitOfWork.AppContext.Update(vehicle);
        }
        public void Add(Vehicle vehicle)
        {
            _unitOfWork.AppContext.Add(vehicle);
        }    
    }
}
