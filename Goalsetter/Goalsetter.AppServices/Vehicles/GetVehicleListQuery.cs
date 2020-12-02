using Goalsetter.AppServices.Dtos;
using Goalsetter.AppServices.Utils;
using Goalsetter.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goalsetter.AppServices.Vehicles
{
    public sealed class GetVehicleListAsyncQuery : IQuery<Task<IEnumerable<VehicleDto>>>
    {
        public GetVehicleListAsyncQuery()
        {

        }
        
        internal sealed class GetVehicleListQueryHandler : IQueryHandler<GetVehicleListAsyncQuery, Task<IEnumerable<VehicleDto>>>
        {
            private readonly IVehicleRepository _vehicleRepository;

            public GetVehicleListQueryHandler(IVehicleRepository vehicleRepository)
            {
                _vehicleRepository = vehicleRepository ?? throw new NullReferenceException($"{nameof(IVehicleRepository)} not defined in {nameof(GetVehicleListQueryHandler)}");
            }

            public async Task<IEnumerable<VehicleDto>> Handle(GetVehicleListAsyncQuery query)
            {
                var result = await _vehicleRepository.GetAsync();

                return result.Select(p => new VehicleDto(p));
            }
        }
    }

}
