using CSharpFunctionalExtensions;
using Goalsetter.AppServices.Dtos;
using Goalsetter.AppServices.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Goalsetter.AppServices.Clients
{
    public class ClientService : IClientService
    {
        private readonly IMessages _messages;
        public ClientService(IMessages messages)
        {
            _messages = messages;
        }

        public async Task<Result> AddClientAsync(NewClientDto dto)
        {
            var command = new AddClientCommand(dto.Name, dto.Email);

            return await _messages.Dispatch(command);
        }

        public async Task<IEnumerable<ClientDto>> GetAsync()
        {
            return await _messages.Dispatch(new GetClientListAsyncQuery());
        }

        public async Task<Result> RemoveClientAsync(Guid id)
        {
            return await _messages.Dispatch(new RemoveClientCommand(id));
        }
    }
}

