using Goalsetter.AppServices.Decorators;
using Goalsetter.AppServices.Dtos;
using Goalsetter.AppServices.Utils;
using Goalsetter.DataAccess;
using Goalsetter.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goalsetter.AppServices
{
    public sealed class GetListAsyncQuery : IQuery<Task<IEnumerable<VehicleDto>>>
    {
        public GetListAsyncQuery()
        {

        }
        //[DatabaseRetry]
        internal sealed class GetListQueryHandler : IQueryHandler<GetListAsyncQuery, Task<IEnumerable<VehicleDto>>>
        {
            private readonly UnitOfWork _unitOfWork;

            public GetListQueryHandler(UnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<VehicleDto>> Handle(GetListAsyncQuery query)
            {
                var vehicleRepository = new VehicleRepository(_unitOfWork);

                var result = await vehicleRepository.GetAsync();

                return result.Select(p => new VehicleDto()
                {
                    Id    = p.Id,
                    Year  = p.Year,
                    Makes = p.Makes,
                    Model = p.Model
                }).ToList();
            }
        }
    }

}
