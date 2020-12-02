using Goalsetter.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goalsetter.DataAccess.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public RentalRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }              

        public async Task<Rental> GetByIdAsync(Guid guid)
        {
            return await _unitOfWork.AppContext.Set<Rental>().Where(p => p.Id == guid)
                .Include(p => p.Vehicle)
                    .ThenInclude(p=> p.Rentals)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Rental>> GetAsync()
        {
            return await _unitOfWork.AppContext.Set<Rental>()
                .Include(p=> p.Vehicle)
                    .ThenInclude(p=> p.RentalPrice)
                .Include(p => p.Client)
                .Where(p=> p.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public void Save(Rental rental)
        {
            _unitOfWork.AppContext.Update(rental);
        }
        public void Add(Rental rental)
        {
            _unitOfWork.AppContext.Add(rental);
        }    
    }
}
