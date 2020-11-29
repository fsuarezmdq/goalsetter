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
using ValueObjects = Goalsetter.Domains.ValueObjects;


namespace Goalsetter.AppServices.Clients
{
    public sealed class AddClientCommand : ICommand
    {
        public string Name { get; }
        public string Email { get; }

        public AddClientCommand(string name, string email)
        {
            Name = name;
            Email = email;
        }

        internal sealed class AddClientCommandHandler : ICommandHandler<AddClientCommand>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IClientRepository _clientRepository;
            public AddClientCommandHandler(IUnitOfWork unitOfWork,IClientRepository clientRepository)
            {
                _unitOfWork = unitOfWork ?? throw  new NullReferenceException($"{nameof(IUnitOfWork)} not defined in {nameof(AddClientCommandHandler)}");
                _clientRepository = clientRepository ?? throw new NullReferenceException($"{nameof(IClientRepository)} not defined in {nameof(AddClientCommandHandler)}");
            }

            public async Task<Result> Handle(AddClientCommand command)
            {
                Result<ValueObjects.ClientName> clientName = ValueObjects.ClientName.Create(command.Name);
                if (clientName.IsFailure) throw new ArgumentException(clientName.Error);

                Result<ValueObjects.Email> email = ValueObjects.Email.Create(command.Email);
                if (email.IsFailure) throw new ArgumentException(email.Error);

                Result<Client> client = Client.Create(clientName.Value, email.Value);
                if(client.IsFailure)
                    return Result.Failure($"Could not create the vehicle. {client.Error}");

                _clientRepository.Add(client.Value);

                await _unitOfWork.Commit();

                return Result.Success();
            }
        }
    }
}
