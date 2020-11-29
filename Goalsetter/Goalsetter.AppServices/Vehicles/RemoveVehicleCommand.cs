using CSharpFunctionalExtensions;
using Goalsetter.AppServices.Utils;
using Goalsetter.DataAccess;
using Goalsetter.DataAccess.Repositories;
using Goalsetter.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goalsetter.AppServices.Vehicles
{
    public sealed class RemoveVehicleCommand : ICommand
    {
        public Guid Id { get; }

        public RemoveVehicleCommand(Guid id)
        {
            Id = id;
        }

        internal sealed class RemoveVehicleCommandHandler : ICommandHandler<RemoveVehicleCommand>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IVehicleRepository _vehicleRepository;
            public RemoveVehicleCommandHandler(IUnitOfWork unitOfWork, IVehicleRepository vehicleRepository)
            {
                _unitOfWork = unitOfWork ?? throw new NullReferenceException($"{nameof(IUnitOfWork)} not defined in {nameof(RemoveVehicleCommandHandler)}");
                _vehicleRepository = vehicleRepository ?? throw new NullReferenceException($"{nameof(IVehicleRepository)} not defined in {nameof(RemoveVehicleCommandHandler)}");
            }

            public async Task<Result> Handle(RemoveVehicleCommand command)
            {
                Vehicle vehicle = await _vehicleRepository.GetByIdAsync(command.Id);
                if (vehicle == null)
                    return Result.Failure($"No vehicle found for Id {command.Id}");

                if (vehicle.CanRemove() != string.Empty)
                    return Result.Failure($"The vehicle {command.Id} was already removed");

                vehicle.Remove();

                await _unitOfWork.Commit();

                return Result.Success();
            }
        }
    }
}
