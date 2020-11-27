using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goalsetter.Domains
{
    public class Price : ValueObject
    {
        public const decimal MinPriceValue = 0;
        public decimal Value { get; }

        private Price(decimal value)
        {
            Value = value;
        }

        public static Result<Price> Create(decimal value)
        {
            if (value < MinPriceValue)
                return Result.Failure<Price>("Price should be more than zero.");

            return Result.Success(new Price(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
