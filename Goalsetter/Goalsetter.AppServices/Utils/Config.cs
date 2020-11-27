using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goalsetter.AppServices.Utils
{
    public sealed class Config
    {
        public int NumberOfDatabaseRetries { get; }

        public Config(int numberOfDatabaseRetries)
        {
            NumberOfDatabaseRetries = numberOfDatabaseRetries;
        }
    }
}
