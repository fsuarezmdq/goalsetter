using CSharpFunctionalExtensions;
using Goalsetter.Domains.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Goalsetter.Domains.Utils;

namespace Goalsetter.Domains
{
    public sealed class Vehicle: AggregateRoot
    {
        private const int YearFirstCarEverMade = 1769;
        public VehicleMakes Makes { get;}
        public VehicleModel Model { get;}
        public int Year { get; }
        public VehiclePrice RentalPrice { get; }

        private readonly List<Rental> _rentals = new List<Rental>();
        public IReadOnlyList<Rental> Rentals => _rentals.ToList();

        private Vehicle()
        {

        }
        
        public Vehicle(Guid id, VehicleMakes makes, VehicleModel model, int year, VehiclePrice rentalPrice ,DateTime createdDate,
            DateTime updatedDate, bool isActive)
        {
            Id = Guard.NotDefault(id);
            Makes = Guard.NotNull(makes);
            Model = Guard.NotNull(model);
            RentalPrice = Guard.NotNull(rentalPrice);
            Year = Guard.NotDefault(year) ;
            CreatedDate = Guard.NotDefault(createdDate);
            UpdatedDate = Guard.NotDefault(updatedDate);
            IsActive = isActive;
        }

        public static Result<Vehicle> Create(VehicleMakes makes, VehicleModel model, int year, Price price, Guid id = default) 
        {
            var canCreate = CreateValidations(makes,model,year, id, out var guid);

            if(canCreate != string.Empty)
               return Result.Failure<Vehicle>(canCreate);

            if (price == null)
                return Result.Failure<Vehicle>("Vehicle Price is required value.");

            Result<VehiclePrice> vehiclePrice = VehiclePrice.Create(guid,price);
            if(vehiclePrice.IsFailure)
                return Result.Failure<Vehicle>(vehiclePrice.Error);

            return Result.Success(new Vehicle(guid, makes,model,year, vehiclePrice.Value, DateTimeNow, DateTimeNow,true));
        }

        private static string CreateValidations(VehicleMakes makes, VehicleModel model, int year, Guid id, out Guid guid)
        {
            var output = string.Empty;

            //Common defaults
            guid = GetId(id);

            //Common validations
            if (year < YearFirstCarEverMade)
                output =  "Vehicle year is invalid. ";
            if (makes == null)
                output += "Vehicle Makes is required. ";
            if (model == null)
                output += "Vehicle Model is required. ";

            return output;
        }

        public string CanRemove()
        {
            return  (IsActive)
                ? string.Empty
                : "The vehicle is already removed.";
        }

        public void Remove()
        {
            var validation = CanRemove();
            if ( validation != string.Empty)
            {
                throw new ValidationException(validation);
            }

            //Remove all active rentals
            if (_rentals.Any())
            {
                _rentals.ForEach(p =>
                {
                    if (p.IsActive)
                    {
                        p.Remove();
                    }
                });
            }

            //Remove the vehicle
            IsActive = false;
        }

        public Result IsRentable(DateRange dateRange)
        {
            dateRange = Guard.NotNull(dateRange);

            if (!IsActive)
                return Result.Failure("Vehicle should be active to create a rental.");

            //Validate if its available in the date range
            var overlap = _rentals.FirstOrDefault(p => p.IsActive && p.Overlap(dateRange));
            if (overlap != null)
                return Result.Failure("The Vehicle is not available in that period of time.");
            
            return Result.Success();
        }
    }
}
