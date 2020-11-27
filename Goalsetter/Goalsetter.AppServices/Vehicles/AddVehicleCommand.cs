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
    public sealed class AddVehicleCommand : ICommand
    {
        public string Makes { get; }
        public string Model { get; }
        public int Year { get; }

        public AddVehicleCommand(string makes, string model, int year)
        {
            Makes = makes;
            Model = model;
            Year = year;
        }

        internal sealed class AddVehicleCommandHandler : ICommandHandler<AddVehicleCommand>
        {
            private readonly UnitOfWork _unitOfWork;
            public AddVehicleCommandHandler(UnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result> Handle(AddVehicleCommand command)
            {
                var vehicleRepository = new VehicleRepository(_unitOfWork);

                Result<VehicleMakes> vehicleMakes = VehicleMakes.Create(command.Makes);
                if (vehicleMakes.IsFailure) throw new ArgumentException(vehicleMakes.Error);
                               
                var result = Vehicle.Create(vehicleMakes.Value, command.Model, command.Year);
                if(result.IsFailure)
                    return Result.Failure($"Could not create the vechicle. {result.Error} ");

                vehicleRepository.Add(result.Value);

                await _unitOfWork.Commit();

                return Result.Success();
            }
        }
    }
}
