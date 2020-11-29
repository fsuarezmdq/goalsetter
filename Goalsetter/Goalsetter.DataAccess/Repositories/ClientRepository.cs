using Goalsetter.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goalsetter.DataAccess.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClientRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }              

        public async Task<Client> GetByIdAsync(Guid guid)
        {
            return await _unitOfWork.GetAsync<Client>(guid)
                .Include(p=> p.Rentals)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Client>> GetAsync()
        {
            return await _unitOfWork.Query<Client>()
                .Where(p=> p.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public void Save(Client vehicle)
        {
            _unitOfWork.Update(vehicle);
        }
        public void Add(Client vehicle)
        {
            _unitOfWork.Add(vehicle);
        }    
    }
}
