using Goalsetter.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goalsetter.DataAccess
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetAsync();
        Task<Vehicle> GetByIdAsync(Guid guid);
        void Save(Vehicle vehicle);
    }
}
