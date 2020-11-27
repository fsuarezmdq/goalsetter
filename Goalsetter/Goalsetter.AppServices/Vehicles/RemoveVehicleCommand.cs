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
            private readonly UnitOfWork _unitOfWork;
            public RemoveVehicleCommandHandler(UnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result> Handle(RemoveVehicleCommand command)
            {
                var vehicleRepository = new VehicleRepository(_unitOfWork);

                Vehicle vehicle = await vehicleRepository.GetByIdAsync(command.Id);
                if (vehicle == null)
                    return Result.Failure($"No vehicle found for Id {command.Id}");

                vehicle.Remove();

                await _unitOfWork.Commit();

                return Result.Success();
            }
        }
    }
}
