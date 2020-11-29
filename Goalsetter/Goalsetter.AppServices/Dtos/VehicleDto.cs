using Goalsetter.Domains.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Goalsetter.Domains;

namespace Goalsetter.AppServices.Dtos
{
    public class VehicleDto
    {
        public Guid Id { get; set; }
        public string Makes { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public decimal RentalPrice { get; set; }
        public RentalDto [] Rentals { get; set; }

        public VehicleDto(Vehicle vehicle)
        {
            Id = vehicle.Id;
            Makes = vehicle.Makes;
            Model = vehicle.Model;
            Year = vehicle.Year;
            CreatedDate = vehicle.CreatedDate;
            UpdatedDate = vehicle.UpdatedDate;
            RentalPrice = vehicle.RentalPrice.Price;

            if (vehicle.Rentals.Any())
            {
                Rentals = vehicle.Rentals
                    .Select(p => new RentalDto(p))
                    .ToArray();
            }
        }
    }
}
