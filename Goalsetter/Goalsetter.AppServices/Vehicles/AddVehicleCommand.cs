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
using Goalsetter.Domains.ValueObjects;

namespace Goalsetter.AppServices.Vehicles
{
    public sealed class AddVehicleCommand : ICommand
    {
        public string Makes { get; }
        public string Model { get; }
        public int Year { get; }
        public int RentalPrice { get; }

        public AddVehicleCommand(string makes, string model, int year, int price)
        {
            Makes = makes;
            Model = model;
            Year = year;
            RentalPrice = price;
        }

        internal sealed class AddVehicleCommandHandler : ICommandHandler<AddVehicleCommand>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IVehicleRepository _vehicleRepository;
            public AddVehicleCommandHandler(IUnitOfWork unitOfWork,IVehicleRepository vehicleRepository)
            {
                _unitOfWork = unitOfWork ?? throw  new NullReferenceException($"{nameof(IUnitOfWork)} not defined in {nameof(AddVehicleCommandHandler)}");
                _vehicleRepository = vehicleRepository ?? throw new NullReferenceException($"{nameof(IVehicleRepository)} not defined in {nameof(AddVehicleCommandHandler)}");
            }

            public async Task<Result> Handle(AddVehicleCommand command)
            {
                Result<VehicleMakes> vehicleMakes = VehicleMakes.Create(command.Makes);
                if (vehicleMakes.IsFailure) throw new ArgumentException(vehicleMakes.Error);

                Result<VehicleModel> vehicleModel = VehicleModel.Create(command.Model);
                if (vehicleModel.IsFailure) throw new ArgumentException(vehicleModel.Error);

                Result<Price> rentalPrice = Price.Create(command.RentalPrice);


                Result<Vehicle> vehicle = Vehicle.Create(vehicleMakes.Value, vehicleModel.Value, command.Year, rentalPrice.Value);
                if(vehicle.IsFailure)
                    return Result.Failure($"Could not create the vehicle. {vehicle.Error} ");

                _vehicleRepository.Add(vehicle.Value);

                await _unitOfWork.Commit();

                return Result.Success();
            }
        }
    }
}
