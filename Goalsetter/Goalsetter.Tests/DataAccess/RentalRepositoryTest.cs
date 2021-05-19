using Goalsetter.AppServices;
using Goalsetter.DataAccess;
using Goalsetter.DataAccess.Repositories;
using Goalsetter.Domains;
using Goalsetter.Domains.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using ApplicationContext = Goalsetter.DataAccess.ApplicationContext;

namespace Goalsetter.Tests.DataAccess
{
    [TestClass]
    public class RentalRepositoryTest : TestsBase
    {
        private readonly UnitOfWork _unitOfWork;

        public RentalRepositoryTest()
        : base (new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options)
        {
            _unitOfWork = new UnitOfWork(AppContext);           


            InsertRentals(MockedData.RentalItems);
            InsertVehicles(MockedData.VehicleItems);
            SaveChanges();
        }
        
        [TestMethod]
        public async Task GetAsync()
        {
            var rentals =  await _unitOfWork.RentalRepository.GetAsync();

            Assert.IsTrue(rentals.Any());
        }

        [TestMethod]
        public async Task GetByIdAsync()
        {
            var rental = await _unitOfWork.RentalRepository.GetByIdAsync(MockedData.Rental.Id);
            Assert.AreEqual(MockedData.Rental.Id, rental.Id);
        }

        [TestMethod]
        public async Task Add()
        {
            var id = Guid.NewGuid();
            var rental = Rental.Create
            (
                MockedData.Client,
                MockedData.Vehicle4,
                DateRange.Create(new DateTime(2019, 1, 1), new DateTime(2019, 1, 15)).Value,
                id
            ).Value;

            _unitOfWork.RentalRepository.Add(rental);

            await _unitOfWork.CommitAsync();

            var storedRental = await _unitOfWork.RentalRepository.GetByIdAsync(id);

            Assert.AreEqual(id, storedRental.Id);
            Assert.AreEqual(rental.Vehicle.Id, storedRental.Vehicle.Id);
            Assert.AreEqual(rental.Client.Id, storedRental.Client.Id);
            Assert.AreEqual(rental.DateRange.StartDate, storedRental.DateRange.StartDate);
            Assert.AreEqual(rental.DateRange.EndDate, storedRental.DateRange.EndDate);
        }

         ~RentalRepositoryTest()
        {
            _unitOfWork.Dispose();
        }
    }
}
