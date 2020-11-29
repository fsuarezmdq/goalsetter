using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Goalsetter.Domains.ValueObjects
{
    public class VehicleModel : ValueObject
    {
        public const int MaxLength = 250;
        public string Value { get; }

        private VehicleModel(string value)
        {
            Value = value;
        }

        public static Result<VehicleModel> Create(string value)
        {
            value = (value ?? string.Empty).Trim();

            if (value.Length == 0)
                return Result.Failure<VehicleModel>("Vehicle Model should not be empty.");

            if (value.Length > MaxLength)
                return Result.Failure<VehicleModel>("Vehicle Model name is too long.");

            return Result.Success(new VehicleModel(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(VehicleModel vehicleModel)
        {
            return vehicleModel.Value;
        }

        public static explicit operator VehicleModel(string vehicleModel)
        {
            return Create(vehicleModel).Value;
        }
    }
}
