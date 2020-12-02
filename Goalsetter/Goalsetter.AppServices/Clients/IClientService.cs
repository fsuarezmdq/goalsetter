using CSharpFunctionalExtensions;
using Goalsetter.AppServices.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Goalsetter.AppServices.Clients
{
    public interface IClientService
    {
        Task<Result> AddClientAsync(NewClientDto newClientDto);
        Task<Result> RemoveClientAsync(Guid id);
        Task<IEnumerable<ClientDto>> GetAsync();
    }
}
