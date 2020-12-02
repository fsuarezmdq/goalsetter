using CSharpFunctionalExtensions;
using Goalsetter.AppServices.Utils;
using Goalsetter.DataAccess;
using Goalsetter.DataAccess.Repositories;
using Goalsetter.Domains;
using System;
using System.Threading.Tasks;

namespace Goalsetter.AppServices.Rentals
{
    public sealed class RemoveRentalCommand : ICommand
    {
        public Guid Id { get; }

        public RemoveRentalCommand(Guid id)
        {
            Id = id;
        }

        internal sealed class RemoveRentalCommandHandler : ICommandHandler<RemoveRentalCommand>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IRentalRepository _rentalRepository;
            public RemoveRentalCommandHandler(IUnitOfWork unitOfWork, IRentalRepository rentalRepository)
            {
                _unitOfWork = unitOfWork ?? throw new NullReferenceException($"{nameof(IUnitOfWork)} not defined in {nameof(RemoveRentalCommandHandler)}");
                _rentalRepository = rentalRepository ?? throw new NullReferenceException($"{nameof(IRentalRepository)} not defined in {nameof(RemoveRentalCommandHandler)}");
            }

            public async Task<Result> Handle(RemoveRentalCommand command)
            {
                Rental rental = await _rentalRepository.GetByIdAsync(command.Id);
                if (rental == null)
                    return Result.Failure($"No rental found for Id {command.Id}");

                if (rental.CanRemove() != string.Empty)
                    return Result.Failure($"The rental {command.Id} was already removed");

                rental.Remove();

                await _unitOfWork.Commit();

                return Result.Success();
            }
        }
    }
}
