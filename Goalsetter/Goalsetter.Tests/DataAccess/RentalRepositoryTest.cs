using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Goalsetter.DataAccess;
using Goalsetter.DataAccess.Repositories;
using Goalsetter.Domains;
using Goalsetter.Domains.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppContext = Goalsetter.DataAccess.AppContext;

namespace Goalsetter.Tests.DataAccess
{
    [TestClass]
    public class RentalRepositoryTest : TestsBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IRentalRepository _rentalRepository;

        public RentalRepositoryTest()
        : base (new DbContextOptionsBuilder<AppContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options)
        {
            _unitOfWork = new UnitOfWork(AppContext);
            _rentalRepository = new RentalRepository(_unitOfWork);

            InsertEntities(MockedData.RentalItems);
            InsertEntities(MockedData.VehicleItems);
            SaveChanges();
        }
        
        [TestMethod]
        public async Task GetAsync()
        {
            var rentals =  await _rentalRepository.GetAsync();

            Assert.IsTrue(rentals.Any());
        }

        [TestMethod]
        public async Task GetByIdAsync()
        {
            var rental = await _rentalRepository.GetByIdAsync(MockedData.Rental.Id);
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

            _rentalRepository.Add(rental);

            await _unitOfWork.Commit();

            var storedRental = await _rentalRepository.GetByIdAsync(id);

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
