using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goalsetter.Domains
{
    public class VehiclePrice: AuditedEntity
    {
        public Vehicle Vehicle { get; } 
        public Price Price { get; } 
    }
}
