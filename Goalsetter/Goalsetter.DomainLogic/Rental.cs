using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Goalsetter.Domains.Utils;
using Goalsetter.Domains.ValueObjects;
using Microsoft.VisualBasic;

namespace Goalsetter.Domains
{
    public sealed class Rental : AggregateRoot
    {
        public Client Client { get; }
        public Vehicle Vehicle { get; }
        public DateRange DateRange { get; }
        public Price TotalPrice { get; }
        
        private Rental()
        {
        }
        public Rental(Guid id, Client client, Vehicle vehicle, DateRange dateRange, Price totalPrice, DateTime createdDate, 
            DateTime updatedDate, bool isActive)
        {
            Id = Guard.NotDefault(id);
            Client = Guard.NotNull(client);
            Vehicle = Guard.NotNull(vehicle);
            DateRange = Guard.NotNull(dateRange);
            TotalPrice = Guard.NotNull(totalPrice);
            CreatedDate = Guard.NotDefault(createdDate);
            UpdatedDate = Guard.NotDefault(updatedDate);
            IsActive = isActive;
        }

        public static Result<Rental> Create(Client client, Vehicle vehicle, DateRange dateRange, Guid id = default)
        {
            var guid = GetId(id);

            if (client == null)
                return Result.Failure<Rental>("Client is required value.");

            if(!client.IsActive)
                return Result.Failure<Rental>("Client should be active to create a rental.");

            if (vehicle == null)
                return Result.Failure<Rental>("Vehicle is required value.");

            if (!vehicle.IsActive)
                return Result.Failure<Rental>("Vehicle should be active to create a rental.");

            if (vehicle.RentalPrice == null)
                return Result.Failure<Rental>("Vehicle rental price is required value.");

            if (dateRange == null)
                return Result.Failure<Rental>("Date Range is required value.");


            Result vehicleAvailable = vehicle.IsRentable(dateRange);
            if (vehicleAvailable.IsFailure)
                return Result.Failure<Rental>(vehicleAvailable.Error);

            var totalPrice = CalculateTotalPrice(vehicle.RentalPrice.Price,dateRange);

            return Result.Success(new Rental(guid,client,vehicle, dateRange, totalPrice, DateTimeNow, DateTimeNow, true));
        }

        public string CanRemove()
        {
            return (IsActive)
                ? string.Empty
                : "The Rental is already removed.";
        }

        public void Remove()
        {
            var validation = CanRemove();
            if (validation != string.Empty)
            {
                throw new ValidationException(validation);
            }
            IsActive = false;
        }

        private static Price CalculateTotalPrice(Price rentalPrice, DateRange dateRange)
        {
            rentalPrice = Guard.NotNull(rentalPrice);
            dateRange = Guard.NotNull(dateRange);

            return (Price)(dateRange.Days * rentalPrice);
        }

        public bool Overlap(DateRange dateRange)
        {
            _ = dateRange ?? throw new ArgumentNullException(nameof(dateRange));

            return DateRange.Overlap(dateRange);
        }
    }
}
