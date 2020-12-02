using CSharpFunctionalExtensions;
using Goalsetter.AppServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goalsetter.AppServices.Rentals
{
    public class RentalService : IRentalService
    {
        private readonly IMessages _messages;

        public RentalService(IMessages messages)
        {
            _messages = messages ?? throw new ArgumentException(nameof(IMessages));
        }
        public async Task<Result> AddRentalAsync(NewRentalDto dto)
        {
            var command = new AddRentalCommand(dto.ClientId, dto.VehicleId, dto.StartDate, dto.EndDate);

            return await _messages.Dispatch(command);
        }
        

        public async Task<Result> RemoveRentalAsync(Guid id)
        {
            var command = new RemoveRentalCommand(id);
            return await _messages.Dispatch(command);
        }
    }
}
