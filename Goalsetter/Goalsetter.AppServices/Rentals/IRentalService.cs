using CSharpFunctionalExtensions;
using Goalsetter.AppServices.Dtos;
using System;
using System.Threading.Tasks;

namespace Goalsetter.AppServices.Rentals
{
    public interface IRentalService
    {
        Task<Result> AddRentalAsync(NewRentalDto newRentalDto);
        Task<Result> RemoveRentalAsync(Guid id);
    }
}
