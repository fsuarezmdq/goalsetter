using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Engine.ClientProtocol;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Goalsetter.Tests.Services
{
    public class TestsBase
    {
        public readonly TestContext TestContext;

        public TestsBase()
        {
            var options = new DbContextOptionsBuilder<TestContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            TestContext = new TestContext(options);
        }
    }
}
