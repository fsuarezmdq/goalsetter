using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Goalsetter.AppServices.Dtos;

namespace Goalsetter.AppServices.Rentals
{
    public interface IRentalService
    {
        Task<Result> AddRentalAsync(NewRentalDto newRentalDto);
        Task<Result> RemoveRentalAsync(Guid id);
    }
}
