using Goalsetter.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goalsetter.DataAccess.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly UnitOfWork _unitOfWork;
        public VehicleRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }              

        public async Task<Vehicle> GetByIdAsync(Guid guid)
        {
            return await _unitOfWork.GetAsync<Vehicle>(guid);
        }

        public async Task<IEnumerable<Vehicle>> GetAsync()
        {
            return await _unitOfWork.Query<Vehicle>()
                .Where(p=> p.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public void Save(Vehicle vehicle)
        {
            _unitOfWork.Update(vehicle);
        }
        public void Add(Vehicle vehicle)
        {
            _unitOfWork.Add(vehicle);
        }    
    }
}
