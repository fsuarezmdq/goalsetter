using Goalsetter.AppServices.Dtos;
using Goalsetter.AppServices.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goalsetter.AppServices.Clients
{
    public sealed class GetClientListAsyncQuery : IQuery<Task<IEnumerable<ClientDto>>>
    {
        public GetClientListAsyncQuery()
        {

        }
        
        internal sealed class GetListClientQueryHandler : IQueryHandler<GetClientListAsyncQuery, Task<IEnumerable<ClientDto>>>
        {
            private readonly IClientRepository _clientRepository;
            public GetListClientQueryHandler(IClientRepository clientRepository)
            {
               _clientRepository = clientRepository ?? throw new NullReferenceException($"{nameof(IClientRepository)} not defined in {nameof(GetListClientQueryHandler)}");
            }

            public async Task<IEnumerable<ClientDto>> Handle(GetClientListAsyncQuery query)
            {
                var result = await _clientRepository.GetAsync();

                return result.Select(p => new ClientDto(p));
            }
        }
    }

}
