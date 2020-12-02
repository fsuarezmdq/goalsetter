using CSharpFunctionalExtensions;
using Goalsetter.Domains.ValueObjects;
using System;

namespace Goalsetter.Domains
{
    public sealed class VehiclePrice: AuditedEntity
    {
        public Vehicle Vehicle { get;} 
        public Price Price { get; }

        private VehiclePrice()
        {
        }

        public VehiclePrice(Guid id ,Price price, DateTime createdDate, DateTime updatedDate, bool isActive)
        {
            Price = price ?? throw new ArgumentNullException(nameof(price));
            Id = id == default ? throw new ArgumentNullException(nameof(id)) : id;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
            IsActive = isActive;
        }

        public static Result<VehiclePrice> Create(Guid id, Price price)
        {
            var guid = GetId(id);

            if (price == null)
                return Result.Failure<VehiclePrice>("Price value can not be null");

            return Result.Success(new VehiclePrice(guid, price,DateTimeNow,DateTimeNow,true));
        }
    }
}
