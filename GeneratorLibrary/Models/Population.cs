using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorLibrary.Models
{
    public record Population
    {
        public double CarryingCapacity { get; set; }
        public double CurrentPopulation { get; set; }
    }
}
