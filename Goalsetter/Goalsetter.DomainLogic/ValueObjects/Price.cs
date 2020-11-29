﻿using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Goalsetter.Domains.ValueObjects
{
    public class Price : ValueObject
    {
        public const decimal MinPriceValue = 0;
        public static Price Undefined => new Price(0);
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

        public static implicit operator decimal(Price price)
        {
            return price.Value;
        }

        public static explicit operator Price(decimal price)
        {
            return Create(price).Value;
        }
    }
}
