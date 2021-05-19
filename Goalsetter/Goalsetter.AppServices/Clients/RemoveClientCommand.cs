using CSharpFunctionalExtensions;
using Goalsetter.AppServices.Utils;
using Goalsetter.Domains;
using System;
using System.Threading.Tasks;

namespace Goalsetter.AppServices.Clients
{
    public sealed class RemoveClientCommand : ICommand
    {
        public Guid Id { get; }

        public RemoveClientCommand(Guid id)
        {
            Id = id;
        }

        internal sealed class RemoveClientCommandHandler : ICommandHandler<RemoveClientCommand>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IClientRepository _clientRepository;
            public RemoveClientCommandHandler(IUnitOfWork unitOfWork, IClientRepository clientRepository)
            {
                _unitOfWork = unitOfWork ?? throw new NullReferenceException($"{nameof(IUnitOfWork)} not defined in {nameof(RemoveClientCommandHandler)}");
                _clientRepository = clientRepository ?? throw new NullReferenceException($"{nameof(IClientRepository)} not defined in {nameof(RemoveClientCommandHandler)}");
            }

            public async Task<Result> Handle(RemoveClientCommand command)
            {
                Client client = await _clientRepository.GetByIdAsync(command.Id);
                if (client == null)
                    return Result.Failure($"No client found for Id {command.Id}");
                
                if (client.CanRemove() != string.Empty)
                    return Result.Failure($"The client {command.Id} was already removed");

                client.Remove();

                await _unitOfWork.CommitAsync();

                return Result.Success();
            }
        }
    }
}
