using Goalsetter.AppServices;
using Goalsetter.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goalsetter.DataAccess.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly IApplicationContext _applicationContext;

        public RentalRepository(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }              

        public async Task<Rental> GetByIdAsync(Guid guid)
        {
            return await _applicationContext.Rentals.Where(p => p.Id == guid)
                .Include(p => p.Vehicle)
                    .ThenInclude(p=> p.Rentals)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Rental>> GetAsync()
        {
            return await _applicationContext.Rentals
                .Include(p=> p.Vehicle)
                    .ThenInclude(p=> p.RentalPrice)
                .Include(p => p.Client)
                .Where(p=> p.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public void Save(Rental rental)
        {
            _applicationContext.Rentals.Update(rental);
        }

        public void Add(Rental rental)
        {
            _applicationContext.Rentals.Add(rental);
        }    
    }
}
