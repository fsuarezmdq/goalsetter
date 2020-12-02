using CSharpFunctionalExtensions;
using Goalsetter.AppServices.Dtos;
using Goalsetter.AppServices.Rentals;
using Goalsetter.AppServices.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace Goalsetter.Tests.Services
{
    [TestClass]
    public class RentalServiceTest
    {
        private readonly Mock<IMessages> _mockMessages;
        private readonly IRentalService _rentalService;
        public RentalServiceTest()
        { 
            _mockMessages = new Mock<IMessages>();
            _mockMessages.Setup(p => p.Dispatch(It.IsAny<ICommand>())).ReturnsAsync(Result.Success);

            _rentalService = new RentalService(_mockMessages.Object);
        }

        [TestMethod]
        public async Task AddRentalAsync()
        {
            var result = await _rentalService.AddRentalAsync(new NewRentalDto());
            Assert.AreEqual(Result.Success(), result);
        }

        [TestMethod]
        public async Task RemoveRentalAsync()
        {
            var result = await _rentalService.RemoveRentalAsync(Guid.NewGuid());
            Assert.AreEqual(Result.Success(), result);
        }
    }
}
