using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Goalsetter.DataAccess;
using Goalsetter.DataAccess.Repositories;
using Goalsetter.Domains;
using Goalsetter.Domains.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppContext = Goalsetter.DataAccess.AppContext;

namespace Goalsetter.Tests.Domains
{
    [TestClass]
    public class RentalTest : TestsBase
    {
        private static Client _testInactiveClient;
        private static Vehicle _testInactiveVehicle;

        public RentalTest()
        : base (new DbContextOptionsBuilder<AppContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options)
        {
            _testInactiveVehicle = Vehicle.Create((VehicleMakes)"Ford", (VehicleModel)"Fiesta", 2000, (Price)50).Value;
            _testInactiveVehicle.Remove();
            _testInactiveClient = Client.Create((ClientName)"FakeClient", (Email)"FakeMail@mail.com").Value;
            _testInactiveClient.Remove();

            InsertEntities(MockedData.RentalItems);
            InsertEntities(MockedData.VehicleItems);
            SaveChanges();
        }
        

        public static IEnumerable<object[]> ArgumentNullExceptionTestParameters
        {
            get
            {
                yield return new object[] { Guid.NewGuid(), null, null, null, default, default, null, default,
                    $"Value cannot be null. (Parameter '{typeof(Client).FullName}')" };
                yield return new object[] { Guid.NewGuid(), MockedData.Client, null, null, default, default, null, default,
                    $"Value cannot be null. (Parameter '{typeof(Vehicle).FullName}')" };
                yield return new object[] { Guid.NewGuid(), MockedData.Client, MockedData.Vehicle, null, default, default, null, default,
                    $"Value cannot be null. (Parameter '{typeof(DateRange).FullName}')" };
                yield return new object[] { Guid.NewGuid(), MockedData.Client, MockedData.Vehicle, MockedData.DateRange, default, default, null, default,
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
                yield return new object[] { Guid.NewGuid(), MockedData.Client, MockedData.Vehicle, MockedData.DateRange, default, default, (Price)50, default,
                    $"Value cannot be default. (Parameter '{typeof(DateTime).FullName}')" };
                yield return new object[] { Guid.NewGuid(), MockedData.Client, MockedData.Vehicle, MockedData.DateRange, DateTime.UtcNow, default, (Price)50, default,
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
                yield return new object[] { Guid.Empty, MockedData.Client, null, null, false, "Vehicle is required value." };
                yield return new object[] { Guid.Empty, MockedData.Client, _testInactiveVehicle, null, false, "Vehicle should be active to create a rental." };
                yield return new object[] { Guid.Empty, MockedData.Client, MockedData.Vehicle, null, false, "Date Range is required value." };
                yield return new object[] { Guid.Empty, MockedData.Client, MockedData.VehicleTwo, MockedData.DateRange, true, string.Empty };
                yield return new object[] { Guid.NewGuid(), MockedData.Client, MockedData.VehicleTwo, MockedData.DateRange, true, string.Empty };
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
        public async Task RentalCreate_WithRentedVehicleAsync()
        {
                var unitOWork = new UnitOfWork(AppContext);
                var vehicleRepository = new VehicleRepository(unitOWork);
                var vehicle = await vehicleRepository.GetByIdAsync(MockedData.Vehicle.Id);

                var dateRange = DateRange.Create(new DateTime(2020, 1, 1), new DateTime(2020, 1, 2)).Value;

                Result<Rental> rental = Rental.Create(MockedData.Client, vehicle, dateRange);

                Assert.IsTrue(rental.IsFailure);
                Assert.AreEqual("The Vehicle is not available in that period of time.", rental.Error);

                unitOWork.Dispose();
        }

        [TestMethod]
        public void RentalRemove()
        {
            var dateRange = DateRange.Create(new DateTime(2000, 1, 1), new DateTime(2000, 1, 2)).Value;
            var rental = Rental.Create(MockedData.Client, MockedData.Vehicle, dateRange).Value;

            rental.Remove();
            Assert.IsFalse(rental.IsActive);
        }

        [TestMethod]
        public void RentalCanRemove()
        {
            var dateRange = DateRange.Create(new DateTime(2000, 1, 1), new DateTime(2000, 1, 2)).Value;
            var rental = Rental.Create(MockedData.Client, MockedData.Vehicle, dateRange).Value;

            var canRemoveActive = rental.CanRemove();
            rental.Remove();
            var canRemoveRemoved = rental.CanRemove();

            Assert.AreEqual(string.Empty, canRemoveActive);
            Assert.AreEqual("The Rental is already removed.", canRemoveRemoved);
        }
    }
}
