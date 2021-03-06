﻿using System;
using Goalsetter.Domains;
using Goalsetter.Domains.ValueObjects;

namespace Goalsetter.Tests.Services
{
    public static class MockedData
    {
        public static Client Client = 
            Client.Create((ClientName) "FakeClient", (Email) "FakeMail@mail.com").Value;

        public static Vehicle Vehicle =
            Vehicle.Create((VehicleMakes) "Ford", (VehicleModel) "F-100", 2012, (Price) 80).Value;

        public static Vehicle VehicleRemoved
        {
            get
            {
                var vehicle = Vehicle.Create((VehicleMakes) "Ford", (VehicleModel) "F-100", 2012, (Price) 80).Value;
                vehicle.Remove();
                return vehicle;
            }
        }

        public static DateRange DateRange = DateRange.Create(new DateTime(2020,1,1), new DateTime(2020,1,15)).Value;

        public static Rental Rental =
            Rental.Create(Client, Vehicle, DateRange).Value;
    }
}
