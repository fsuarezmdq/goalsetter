using CSharpFunctionalExtensions;
using Goalsetter.AppServices.Dtos;
using Goalsetter.AppServices.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Goalsetter.AppServices.Vehicles
{
    public class VehicleService : IVehicleService
    {
        private readonly IMessages _messages;
        public VehicleService(IMessages messages)
        {
            _messages = messages;
        }

        public async Task<Result> AddVehicleAsync(NewVehicleDto dto)
        {
            var command = new AddVehicleCommand(dto.Makes, dto.Model, dto.Year, dto.RentalPrice);

            return await _messages.Dispatch(command);
        }

        public async Task<IEnumerable<VehicleDto>> GetAsync()
        {
            return await _messages.Dispatch(new GetVehicleListAsyncQuery());
        }

        public async Task<Result> RemoveVehicleAsync(Guid id)
        {
            return await _messages.Dispatch(new RemoveVehicleCommand(id));
        }
    }
}

