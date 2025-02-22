using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorLibrary.Models
{
    public record Atmosphere
    {
        public double Mass { get; set; }
        public List<string>? Composition { get; set; }
        public List<AtmosphereCharacteristic>? Characteristics { get; set; }
        public MarginalAtmosphere? MarginalAtmosphere { get; set; }
    }

    public enum AtmosphereCharacteristic
    {
        None,
        Suffocating,
        Corrosive,
        MildlyToxic,
        HighlyToxic,
        LethallyToxic,
        Marginal
    }

    public enum MarginalAtmosphere
    {
        ChlorineOrFluorine,
        SulphurCompounds,
        NitrogenCompounds,
        OrganicToxins,
        LowOxygen,
        Pollutants,
        HighCarbonDioxide,
        HighOxygen,
        InertGases
    }
}
