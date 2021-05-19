using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Goalsetter.Domains;

namespace Goalsetter.AppServices
{
    public interface IRentalRepository
    {
        Task<IEnumerable<Rental>> GetAsync();
        Task<Rental> GetByIdAsync(Guid guid);
        void Save(Rental rental);
        void Add(Rental rental);
    }
}
