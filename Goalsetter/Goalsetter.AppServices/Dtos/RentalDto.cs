using Goalsetter.Domains;
using System;

namespace Goalsetter.AppServices.Dtos
{
    public class RentalDto
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public decimal TotalPrice { get; set; }

        public RentalDto (Rental rental)
        {
            Id = rental.Id;
            ClientId = rental.Client.Id;
            VehicleId = rental.Vehicle.Id;
            StartDate = rental.DateRange.StartDate;
            EndDate = rental.DateRange.EndDate;
            CreatedDate = rental.CreatedDate;
            UpdatedDate = rental.UpdatedDate;
            TotalPrice = rental.TotalPrice;
        }
    }

}
