using Goalsetter.AppServices;
using Goalsetter.Domains;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ApplicationContext = Goalsetter.DataAccess.ApplicationContext;


namespace Goalsetter.Tests
{
    public class TestsBase
    {
        protected IApplicationContext AppContext { get; }
        protected DbContextOptions<ApplicationContext> ContextOptions { get; }

        protected TestsBase(DbContextOptions<ApplicationContext> contextOptions)
        {
            ContextOptions = contextOptions;
            AppContext = new ApplicationContext(ContextOptions);
            Seed();
        }

        private void Seed()
        {
            using (var context = new ApplicationContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                //context.AddRange(MockedData.VehicleItems);
                //context.AddRange(MockedData.RentalItems);

                //context.SaveChanges();
            }
        }

        public void InsertClients(IEnumerable<Client> entities)
        {
            AppContext.Clients.AddRange(entities);
        }

        public void InsertVehicles(IEnumerable<Vehicle> entities)
        {
            AppContext.Vehicles.AddRange(entities);
        }

        public void InsertVehiclePrice(IEnumerable<VehiclePrice> entities)
        {
            AppContext.VehiclePrices.AddRange(entities);
        }

        public void InsertRentals(IEnumerable<Rental> entities)
        {
            AppContext.Rentals.AddRange(entities);
        }

        public void SaveChanges()
        {
            AppContext.SaveChangesAsync();
        }
    }
}
