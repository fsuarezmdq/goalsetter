using Goalsetter.AppServices.Rentals;
using Goalsetter.DataAccess;
using Goalsetter.DataAccess.Repositories;
using Goalsetter.Domains;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
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

            _mockVehicleRepository.Setup(p => p.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(MockedData.Vehicle);
            _mockClientRepository.Setup(p => p.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(MockedData.Client);
            _mockRentalRepository.Setup(p => p.Add(It.IsAny<Rental>()));
                
        }

        [TestMethod]
        public async Task AddRentalCommandAsync()
        {

            var clientId = MockedData.Rental.Client.Id;
            var vehicleId = MockedData.Rental.Vehicle.Id;
            var dateRange = MockedData.Rental.DateRange;

            //var command = 
            var commandHandler = new AddRentalCommand.AddRentalCommandHandler(_mockUnitOfWork.Object,
                _mockRentalRepository.Object, _mockClientRepository.Object, _mockVehicleRepository.Object);
            
            var result = await commandHandler.Handle(
                new AddRentalCommand(clientId, vehicleId, dateRange.StartDate, dateRange.EndDate));

            Assert.IsTrue(result.IsSuccess);
        }

    }
}
