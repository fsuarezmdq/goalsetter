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

namespace Goalsetter.AppServices.Rentals
{
    public sealed class AddRentalCommand : ICommand
    {
        public Guid ClientId { get; }
        public Guid VehicleId { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public AddRentalCommand(Guid clientId, Guid vehicleId, DateTime startDate, DateTime endDate)
        {
            ClientId = clientId;
            VehicleId = vehicleId;
            StartDate = startDate;
            EndDate = endDate;
        }

        internal sealed class AddRentalCommandHandler : ICommandHandler<AddRentalCommand>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IRentalRepository _rentalRepository;
            private readonly IClientRepository _clientRepository;
            private readonly IVehicleRepository _vehicleRepository;

            public AddRentalCommandHandler(IUnitOfWork unitOfWork,IRentalRepository rentalRepository,
                IClientRepository clientRepository, IVehicleRepository vehicleRepository)
            {
                _unitOfWork = unitOfWork ?? throw  new NullReferenceException($"{nameof(IUnitOfWork)} not defined in {nameof(AddRentalCommandHandler)}");
                _rentalRepository = rentalRepository ?? throw new NullReferenceException($"{nameof(IRentalRepository)} not defined in {nameof(AddRentalCommandHandler)}");
                _clientRepository = clientRepository ?? throw new NullReferenceException($"{nameof(IClientRepository)} not defined in {nameof(AddRentalCommandHandler)}");
                _vehicleRepository = vehicleRepository ?? throw new NullReferenceException($"{nameof(IVehicleRepository)} not defined in {nameof(AddRentalCommandHandler)}");
            }

            public async Task<Result> Handle(AddRentalCommand command)
            {
                var vehicle = await _vehicleRepository.GetByIdAsync(command.VehicleId);
                if (vehicle == null)
                    return Result.Failure("The selected vehicle does not exists.");

                var client = await _clientRepository.GetByIdAsync(command.ClientId);
                if (client == null)
                    return Result.Failure("The selected client does not exists.");

                var dateRange = DateRange.Create(command.StartDate, command.EndDate);
                if (dateRange.IsFailure)
                    return Result.Failure(dateRange.Error);

                var rental = Rental.Create(client,vehicle,dateRange.Value);
                if(rental.IsFailure)
                    return Result.Failure($"Could not create the Rental. {rental.Error} ");

                _rentalRepository.Add(rental.Value);

                await _unitOfWork.Commit();

                return Result.Success();
            }
        }
    }
}
