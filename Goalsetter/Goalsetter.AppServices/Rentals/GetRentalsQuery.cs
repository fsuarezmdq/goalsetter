using Goalsetter.AppServices.Dtos;
using Goalsetter.AppServices.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goalsetter.AppServices.Rentals
{
    public class GetRentalsQuery : IQuery<Task<IEnumerable<RentalDto>>>
    {
        public GetRentalsQuery()
        {

        }

        internal sealed class GetRentalsQueryHandler : IQueryHandler<GetRentalsQuery, Task<IEnumerable<RentalDto>>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetRentalsQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork ?? throw new NullReferenceException($"{nameof(IUnitOfWork)} not defined in {nameof(GetRentalsQueryHandler)}");
            }

            public async Task<IEnumerable<RentalDto>> Handle(GetRentalsQuery query)
            {
                var result = await _unitOfWork.RentalRepository.GetAsync();

                return result.Select(p => new RentalDto(p));
            }
        }
    }

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
