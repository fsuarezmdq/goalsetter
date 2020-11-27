using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goalsetter.AppServices.Dtos
{
    public class VehicleDto
    {
        public Guid Id { get; set; }
        public string Makes { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }
}
