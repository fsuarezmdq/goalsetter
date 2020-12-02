using System;

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
