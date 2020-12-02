using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Goalsetter.Domains;
using Goalsetter.Domains.ValueObjects;

namespace Goalsetter.Tests
{
    public static class MockedData
    {
        public static Client Client = 
            Client.Create((ClientName) "FakeClient", (Email) "FakeMail@mail.com").Value;

        public static Vehicle Vehicle =
            Vehicle.Create((VehicleMakes) "Ford", (VehicleModel) "F-100", 2012, (Price) 80).Value;

        public static DateRange DateRange = DateRange.Create(new DateTime(2020,1,1), new DateTime(2020,1,15)).Value;

        public static Rental Rental =
            Rental.Create(Client, Vehicle, DateRange).Value;

        public static Vehicle[] VehicleItems = new Vehicle[]
        {
            Vehicle,
            Vehicle.Create((VehicleMakes) "Ford", (VehicleModel) "Fiesta", 2000, (Price) 50).Value,
            Vehicle.Create((VehicleMakes) "Chevrolet", (VehicleModel) "Cruze", 2020, (Price) 80).Value,
        };

        public static Rental[] RentalItems = new Rental[]
        {
            Rental
        };
}
}
