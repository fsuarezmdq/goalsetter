using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Goalsetter.Domains;

namespace Goalsetter.AppServices
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAsync();
        Task<Client> GetByIdAsync(Guid guid);
        void Save(Client vehicle);
        void Add(Client client);
    }
}
