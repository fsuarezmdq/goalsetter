using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goalsetter.AppServices.Dtos
{
    public class NewRentalDto
    {
        public Guid ClientId { get; set; }
        public Guid VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
