using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Goalsetter.Domains.ValueObjects
{
    public class VehicleMakes : ValueObject
    {
        public const int MaxLength = 250;
        public string Value { get; }

        private VehicleMakes(string value)
        {
            Value = value;
        }

        public static Result<VehicleMakes> Create(string value)
        {
            value = (value ?? string.Empty).Trim();

            if (value.Length == 0)
                return Result.Failure<VehicleMakes>("Vehicle Makes should not be empty.");

            if (value.Length > MaxLength)
                return Result.Failure<VehicleMakes>("Vehicle Makes name is too long.");

            return Result.Success(new VehicleMakes(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(VehicleMakes vehicleMakes)
        {
            return vehicleMakes.Value;
        }

        public static explicit operator VehicleMakes(string vehicleMakes)
        {
            return Create(vehicleMakes).Value;
        }
    }
}
