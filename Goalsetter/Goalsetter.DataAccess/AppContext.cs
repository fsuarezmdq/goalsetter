using Goalsetter.DataAccess.EntityConfiguration;
using Goalsetter.Domains;
using Goalsetter.Domains.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;

namespace Goalsetter.DataAccess
{
    public class AppContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<VehiclePrice> VehiclePrices { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        public AppContext()//(IConfiguration configuration)
        {
            //_configuration = configuration;
        }
        public AppContext(DbContextOptions<AppContext> options)
        : base(options)
        {
            
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial " +
        //    //    "Catalog=VEHICLE_RENTAL_DEV;Integrated Security=True",
        //    //  x => x.MigrationsAssembly("Goalsetter.DataAccess"));
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyBuilderConfiguration(modelBuilder);

            AddSeedData(modelBuilder);
        }

        public static void ApplyBuilderConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VehicleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClientEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new VehiclePriceEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RentalEntityTypeConfiguration());
        }

        private static void AddSeedData(ModelBuilder modelBuilder)
        {
            //Anonymous types required to create an object without Vehicle price.
            var vehicles = new []
            {
                new { Id = Guid.NewGuid(), CreatedDate = DateTime.UtcNow, IsActive = true, Makes = (VehicleMakes)"Chevrolet",
                    Model = (VehicleModel)"Cruze", UpdatedDate = DateTime.UtcNow, Year = 2017 }, 
                new { Id = Guid.NewGuid(), CreatedDate = DateTime.UtcNow, IsActive = true, Makes = (VehicleMakes)"Chevrolet",
                    Model = (VehicleModel)"Corsa", UpdatedDate = DateTime.UtcNow, Year = 2011},
                new { Id = Guid.NewGuid(), CreatedDate = DateTime.UtcNow, IsActive = true, Makes = (VehicleMakes)"Ford", 
                    Model = (VehicleModel)"F-100", UpdatedDate = DateTime.UtcNow, Year = 2000 },
                new { Id = Guid.NewGuid(), CreatedDate = DateTime.UtcNow, IsActive = true, Makes = (VehicleMakes)"Fiat", 
                    Model = (VehicleModel)"Palio", UpdatedDate = DateTime.UtcNow, Year = 2008 }
            };

            modelBuilder.Entity<Vehicle>().HasData(vehicles);

            modelBuilder.Entity<VehiclePrice>().HasData
            (
                VehiclePrice.Create(vehicles[0].Id, (Price)150).Value,
                VehiclePrice.Create(vehicles[1].Id, (Price)100).Value,
                VehiclePrice.Create(vehicles[2].Id, (Price)120).Value,
                VehiclePrice.Create(vehicles[3].Id, (Price)80).Value
            );

            modelBuilder.Entity<Client>().HasData
            (
                Client.Create((ClientName)"José de San Martin", (Email)"jose@sanmartin.com").Value,
                Client.Create((ClientName)"Mariano Moreno", (Email)"marian@moreno.com").Value,
                Client.Create((ClientName)"Juan José Castelli", (Email)"jose@castelli.com").Value,
                Client.Create((ClientName)"Domingo Faustino Sarmiento", (Email)"domi@sarmiento.com").Value
            );
        }
    }
}
