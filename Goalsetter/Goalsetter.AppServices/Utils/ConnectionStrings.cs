using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goalsetter.AppServices.Utils
{
    public sealed class CommandsConnectionString
    {
        public string Value { get; }

        public CommandsConnectionString(string value)
        {
            Value = value;
        }
    }

    public sealed class QueriesConnectionString
    {
        public string Value { get; }

        public QueriesConnectionString(string value)
        {
            Value = value;
        }
    }
}
