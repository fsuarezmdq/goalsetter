using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppContext = Goalsetter.DataAccess.AppContext;


namespace Goalsetter.Tests
{
    public class TestsBase
    {
        protected AppContext AppContext { get; }
        protected DbContextOptions<AppContext> ContextOptions { get; }

        protected TestsBase(DbContextOptions<AppContext> contextOptions)
        {
            ContextOptions = contextOptions;
            AppContext = new AppContext(ContextOptions);
            Seed();
        }

        private void Seed()
        {
            using (var context = new AppContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                //context.AddRange(MockedData.VehicleItems);
                //context.AddRange(MockedData.RentalItems);

                //context.SaveChanges();
            }
        }

        public void InsertEntities<T>(IEnumerable<T> entities) where T : class
        {
            AppContext.Set<T>().AddRange(entities);
        }

        public void SaveChanges()
        {
            AppContext.SaveChanges();
        }
    }
}
