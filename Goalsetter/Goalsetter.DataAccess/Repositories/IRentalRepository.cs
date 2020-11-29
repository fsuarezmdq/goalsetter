using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Goalsetter.Domains;

namespace Goalsetter.DataAccess.Repositories
{
    public interface IRentalRepository
    {
        Task<IEnumerable<Rental>> GetAsync();
        Task<Rental> GetByIdAsync(Guid guid);
        void Save(Rental vehicle);
        void Add(Rental vehicle);
    }
}
