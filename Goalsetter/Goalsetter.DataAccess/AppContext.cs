using Goalsetter.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goalsetter.DataAccess
{
    public class AppContext : DbContext
    {
       // private readonly CommandsConnectionString _commandsConnectionString;
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehiclePrice> VehiclePrice { get; set; }

        public AppContext()
        {

        }
        public AppContext(DbContextOptions<AppContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial " +
                "Catalog=VEHICLE_RENTAL_DEV;Integrated Security=True",
              x => x.MigrationsAssembly("Goalsetter.DataAccess"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VehicleEntityTypeConfiguration());

            AddSeedData(modelBuilder);
        }

        private static void AddSeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().HasData
            (
                Vehicle.Create((VehicleMakes)"Chevrolet", "Cruze", 2017).Value,
                Vehicle.Create((VehicleMakes)"Chevrolet", "Corsa", 2011).Value,
                Vehicle.Create((VehicleMakes)"Ford", "F-100", 2000).Value,
                Vehicle.Create((VehicleMakes)"Fiat", "Palio", 2008).Value
            );
        }
    }
}
