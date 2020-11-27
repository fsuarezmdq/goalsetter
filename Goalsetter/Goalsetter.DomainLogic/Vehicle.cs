using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goalsetter.Domains
{
    public class Vehicle: AggregateRoot
    {
        private const int YearFirstCarEverMade = 1769;
        public VehicleMakes Makes { get;}
        public string Model { get;}
        public int Year { get; }

        private Vehicle()
        {

        }
        private Vehicle(Guid id, VehicleMakes makes, string model, int year, DateTime createdDate, 
            bool isActive)
        {
            Id = (id == default)? Guid.NewGuid() : id;
            Makes = makes;
            Model = model;
            Year = year;
            IsActive = isActive;
            CreatedDate = UpdatedDate = createdDate;
        }


        public static Result<Vehicle> Create(VehicleMakes makes, string model, int year, Guid id = default) 
        {
                     

            model = (model ?? string.Empty).Trim();

            if (model.Length == 0)
                return Result.Failure<Vehicle>("Vehicle Model should not be empty.");

            if (model.Length > 200)
                return Result.Failure<Vehicle>("Vehicle Model name is too long.");

            if(year < YearFirstCarEverMade)
                return Result.Failure<Vehicle>("Vehicle year is invalid.");            

            return Result.Success(new Vehicle(id,makes,model,year,DateTime.UtcNow,true));
        }

        public void Remove()
        {
            IsActive = false;
        }
    }
}
