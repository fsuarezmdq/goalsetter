using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Goalsetter.Tests
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions options): base(options)
        {
                
        }

        
    }
}
