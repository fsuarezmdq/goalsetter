using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Goalsetter.DataAccess;
using Goalsetter.DataAccess.Repositories;
using Goalsetter.Domains;
using Goalsetter.Domains.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Goalsetter.Tests
{
    [TestClass]
    public class RentalTest
    {
        private static Client _testClient;
        private static Client _testInactiveClient;
        private static Vehicle _testVehicle;
        private static Vehicle _testInactiveVehicle;
        private static DateRange _testDateRange;
        public RentalTest()
        {
            _testClient = Client.Create((ClientName)"FakeClient", (Email)"FakeMail@mail.com").Value;
            _testVehicle = Vehicle.Create((VehicleMakes) "Ford", (VehicleModel) "Fiesta", 2000, (Price) 50).Value;
            _testDateRange = DateRange.Create(DateTime.MinValue, DateTime.MaxValue).Value;

            _testInactiveVehicle = Vehicle.Create((VehicleMakes)"Ford", (VehicleModel)"Fiesta", 2000, (Price)50).Value;
            _testInactiveVehicle.Remove();
            _testInactiveClient = Client.Create((ClientName)"FakeClient", (Email)"FakeMail@mail.com").Value;
            _testInactiveClient.Remove();
        }
        

        public static IEnumerable<object[]> ArgumentNullExceptionTestParameters
        {
            get
            {
                yield return new object[] { Guid.NewGuid(), null, null, null, default, default, null, default,
                    $"Value cannot be null. (Parameter '{typeof(Client).FullName}')" };
                yield return new object[] { Guid.NewGuid(), _testClient, null, null, default, default, null, default,
                    $"Value cannot be null. (Parameter '{typeof(Vehicle).FullName}')" };
                yield return new object[] { Guid.NewGuid(), _testClient, _testVehicle, null, default, default, null, default,
                    $"Value cannot be null. (Parameter '{typeof(DateRange).FullName}')" };
                yield return new object[] { Guid.NewGuid(), _testClient, _testVehicle, _testDateRange, default, default, null, default,
                    $"Value cannot be null. (Parameter '{typeof(Price).FullName}')" };
            }
        }

        [DataTestMethod]
        [DynamicData("ArgumentNullExceptionTestParameters")]
        public void RentalCtr_ArgumentNullException(Guid id, Client client, Vehicle vehicle, DateRange dateRange, DateTime createdDate,
            DateTime updatedDate, Price totalPrice, bool isActive, string expectedMessage)
        {
            
            ArgumentNullException ex = Assert.ThrowsException<ArgumentNullException>(() => 
                new Rental(id, client, vehicle, dateRange, totalPrice, createdDate, updatedDate, isActive));

            Assert.AreEqual(expectedMessage, ex.Message);
        }

    
        public static IEnumerable<object[]> ArgumentExceptionTestParameters
        {
            get
            {
                yield return new object[] { Guid.Empty, null, null, null, default, default, null, default,
                    $"Value cannot be default. (Parameter '{typeof(Guid).FullName}')" };
                yield return new object[] { Guid.NewGuid(), _testClient, _testVehicle, _testDateRange, default, default, (Price)50, default,
                    $"Value cannot be default. (Parameter '{typeof(DateTime).FullName}')" };
                yield return new object[] { Guid.NewGuid(), _testClient, _testVehicle, _testDateRange, DateTime.UtcNow, default, (Price)50, default,
                    $"Value cannot be default. (Parameter '{typeof(DateTime).FullName}')" };
            }
        }

        [DataTestMethod]
        [DynamicData("ArgumentExceptionTestParameters")]
        public void RentalCtr_ArgumentException(Guid id, Client client, Vehicle vehicle, DateRange dateRange, DateTime createdDate,
            DateTime updatedDate, Price totalPrice, bool isActive, string expectedMessage)
        {

            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() =>
                new Rental(id, client, vehicle, dateRange, totalPrice, createdDate, updatedDate, isActive));

            Assert.AreEqual(expectedMessage, ex.Message);
        }
        public static IEnumerable<object[]> RentalCreateTestParameters
        {
            get
            {
                yield return new object[] { Guid.Empty, null, null, null, false , "Client is required value." };
                yield return new object[] { Guid.Empty, _testInactiveClient, null, null, false, "Client should be active to create a rental." };
                yield return new object[] { Guid.Empty, _testClient, null, null, false, "Vehicle is required value." };
                yield return new object[] { Guid.Empty, _testClient, _testInactiveVehicle, null, false, "Vehicle should be active to create a rental." };
                yield return new object[] { Guid.Empty, _testClient, _testVehicle, null, false, "Date Range is required value." };
                yield return new object[] { Guid.Empty, _testClient, _testVehicle, _testDateRange, true, string.Empty };
                yield return new object[] { Guid.NewGuid(), _testClient, _testVehicle, _testDateRange, true, string.Empty };
            }
        }

        [DataTestMethod]
        [DynamicData("RentalCreateTestParameters")]
        public void RentalCreate(Guid id, Client client, Vehicle vehicle, DateRange dateRange, bool isSuccess, string expectedMessage)
        {
            Result<Rental> rental = Rental.Create(client,vehicle,dateRange,id);

            Assert.AreEqual(isSuccess, rental.IsSuccess);
            Assert.AreEqual(!isSuccess, rental.IsFailure);

            if (id != Guid.Empty)
            {
                Assert.AreEqual(id, rental.Value.Id);
            }

            if (rental.IsFailure)
            {
                Assert.AreEqual(expectedMessage, rental.Error);
            }

            if (rental.IsSuccess)
            {
                Assert.AreEqual(client, rental.Value.Client);
                Assert.AreEqual(vehicle, rental.Value.Vehicle);
                Assert.AreEqual(dateRange, rental.Value.DateRange);
                Assert.AreEqual(true, rental.Value.IsActive);
            }
        }

        [TestMethod]
        public void RentalCreate_WithRentedVehicle()
        {
        //    var dataContext = new AppContext();
        //    var unitOfWork = new UnitOfWork();
        //    var vehicleRepository = new VehicleRepository();

            //var vehicle = Vehicle.Create((VehicleMakes)"Ford", (VehicleModel)"Fiesta", 2000, (Price)50).Value;

            //Result<Rental> rental1 = Rental.Create(_testClient, vehicle, _testDateRange);
            //Result<Rental> rental2 = Rental.Create(_testClient, rental1.Value.Vehicle, _testDateRange);

            //Assert.IsTrue(rental2.IsFailure);
            //Assert.AreEqual("expectedMessage", rental2.Error);
        }

    }
}
