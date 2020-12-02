using Goalsetter.AppServices.Rentals;
using Goalsetter.DataAccess;
using Goalsetter.DataAccess.Repositories;
using Goalsetter.Domains;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Goalsetter.Tests.Services
{
    [TestClass]
    public class RentalCommandsTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IClientRepository> _mockClientRepository;
        private readonly Mock<IVehicleRepository> _mockVehicleRepository;
        private readonly Mock<IRentalRepository> _mockRentalRepository;
        public RentalCommandsTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockVehicleRepository = new Mock<IVehicleRepository>();
            _mockClientRepository = new Mock<IClientRepository>();
            _mockRentalRepository = new Mock<IRentalRepository>();

            _mockRentalRepository.Setup(p => p.Add(It.IsAny<Rental>()));

        }

        [TestMethod]
        public async Task AddRentalCommandAsync()
        {
            var dateRange = MockedData.Rental.DateRange;

            _mockVehicleRepository.Setup(p => p.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(MockedData.Vehicle);
            _mockClientRepository.Setup(p => p.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(MockedData.Client);
            
            var commandHandler = new AddRentalCommand.AddRentalCommandHandler(_mockUnitOfWork.Object,
                _mockRentalRepository.Object, _mockClientRepository.Object, _mockVehicleRepository.Object);
            
            var result = await commandHandler.Handle(
                new AddRentalCommand(MockedData.Rental.Client.Id, MockedData.Rental.Vehicle.Id, dateRange.StartDate, 
                    dateRange.EndDate));

            Assert.IsTrue(result.IsSuccess);
        }

        public static IEnumerable<object[]> TestParameters
        {
            get
            {
                yield return new object[] { MockedData.Vehicle, (Client)null, new DateTime(2020, 1, 1), new DateTime(2020, 1, 15), "The selected client does not exists." };
                yield return new object[] { (Vehicle)null, MockedData.Client, new DateTime(2020, 1, 1), new DateTime(2020, 1, 15), "The selected vehicle does not exists." };
                yield return new object[] { MockedData.Vehicle, MockedData.Client, new DateTime(2020, 1, 15), new DateTime(2020, 1, 1), "Start Date should be less than End Date" };
                yield return new object[] { MockedData.VehicleRemoved, MockedData.Client, new DateTime(2020, 1, 1), new DateTime(2020, 1, 15),
                    "Could not create the Rental. Vehicle should be active to create a rental. " };
            }
        }

        [DataTestMethod]
        [DynamicData("TestParameters")]
        public async Task AddRentalCommandAsync_NonexistentEntity(Vehicle vehicle, Client client, DateTime startDate, DateTime endDate, string error)
        {
            _mockVehicleRepository.Setup(p => p.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(vehicle);
            _mockClientRepository.Setup(p => p.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(client);
            

            var commandHandler = new AddRentalCommand.AddRentalCommandHandler(_mockUnitOfWork.Object,
                _mockRentalRepository.Object, _mockClientRepository.Object, _mockVehicleRepository.Object);

            var result = await commandHandler.Handle(
                new AddRentalCommand(MockedData.Client.Id, MockedData.Vehicle.Id, startDate, endDate));

            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(error,result.Error);
        }
    }
}
