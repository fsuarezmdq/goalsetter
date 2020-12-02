using System;
using Goalsetter.Domains;
using Goalsetter.Domains.ValueObjects;

namespace Goalsetter.Tests.DataAccess
{
    public static class MockedData
    {
        public static Client Client =
            Client.Create((ClientName)"FakeClient", (Email)"FakeMail@mail.com").Value;

        public static Vehicle Vehicle1 =
            Vehicle.Create((VehicleMakes) "Ford", (VehicleModel) "F-100", 2012, (Price) 80).Value;
        public static Vehicle Vehicle2 =
            Vehicle.Create((VehicleMakes)"Chevrolet", (VehicleModel)"Cruze", 2012, (Price)80).Value;
        public static Vehicle Vehicle3 =
            Vehicle.Create((VehicleMakes)"Audi", (VehicleModel)"TT", 2012, (Price)80).Value;

        public static Vehicle Vehicle4 =
            Vehicle.Create((VehicleMakes)"Audi", (VehicleModel)"TT", 2015, (Price)80).Value;

        public static DateRange DateRange = DateRange.Create(new DateTime(2020,1,1), new DateTime(2020,1,15)).Value;

        public static Rental Rental = Rental.Create(Client, Vehicle1, DateRange).Value;

        public static Vehicle[] VehicleItems = new Vehicle[]
        {
            Vehicle1,
            Vehicle2,
            Vehicle3,
        };

        public static Rental[] RentalItems = new Rental[]
        {
            Rental,
            Rental.Create(Client, Vehicle2, DateRange).Value,
            Rental.Create(Client, Vehicle3, DateRange).Value,
        };
}
}
