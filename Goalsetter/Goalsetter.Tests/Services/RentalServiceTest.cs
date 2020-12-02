using CSharpFunctionalExtensions;
using Goalsetter.AppServices;
using Goalsetter.AppServices.Dtos;
using Goalsetter.AppServices.Rentals;
using Goalsetter.AppServices.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Goalsetter.Tests.Services
{
    [TestClass]
    public class RentalServiceTest
    {
        private static IRentalService _rentalService;
        private Mock<IMessages> _mockMessages;
        public RentalServiceTest()
        { 
            _mockMessages = new Mock<IMessages>();
            _rentalService = new RentalService(_mockMessages.Object);
            _mockMessages.Setup(p => p.Dispatch(It.IsAny<ICommand>())).ReturnsAsync(Result.Success);
        }

        [TestMethod]
        public async Task AddRentalAsync()
        {
            var result = await _rentalService.AddRentalAsync(new NewRentalDto());
            Assert.AreEqual(Result.Success(), result);
        }
    }
}
