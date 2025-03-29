using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorLibrary.Models.Advanced
{
    public class StellarAge
    {
        public StellarAgePopulationType Type { get; set; }
        public double Age { get; set; } //In billions (UK/US) / thousands of millions (EU) years.
    }

    public enum StellarAgePopulationType
    {
        ExtremePopulationI,
        YoungPopulationI,
        IntermediatePopulationI,
        OldPopulationI,
        IntermediatePopulationII,
        ExtremePopulationII
    }
}
