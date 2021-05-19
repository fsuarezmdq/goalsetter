using Goalsetter.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Goalsetter.AppServices;

namespace Goalsetter.DataAccess.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IApplicationContext _applicationContext;

        public ClientRepository(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }              

        public async Task<Client> GetByIdAsync(Guid guid)
        {
            return await _applicationContext.Clients.Where(p => p.Id == guid)
                .Include(p=> p.Rentals)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Client>> GetAsync()
        {
            return await _applicationContext.Clients
                .Where(p=> p.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public void Save(Client vehicle)
        {
            _applicationContext.Clients.Update(vehicle);
        }
        public void Add(Client client)
        {
            _applicationContext.Clients.Add(client);
        }    
    }
}
