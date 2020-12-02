using CSharpFunctionalExtensions;
using Goalsetter.AppServices.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Goalsetter.AppServices.Vehicles
{
    public interface IVehicleService
    {
        Task<Result> AddVehicleAsync(NewVehicleDto newVehicleDto);
        Task<Result> RemoveVehicleAsync(Guid id);
        Task<IEnumerable<VehicleDto>> GetAsync();
    }
}
