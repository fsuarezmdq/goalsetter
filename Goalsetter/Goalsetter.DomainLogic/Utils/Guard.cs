using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goalsetter.Domains.Utils
{
    public static class Guard
    {

        public static T NotDefault<T>(T obj) where T : struct
        {
            return (object.Equals(obj, default(T))) ? throw new ArgumentException($"Value cannot be default. (Parameter '{typeof(T).FullName}')") : obj;
        }

        public static T NotNull<T>(T obj) where T : class
        {
            return obj ?? throw new ArgumentNullException(typeof(T).FullName);
        }
    }
}
