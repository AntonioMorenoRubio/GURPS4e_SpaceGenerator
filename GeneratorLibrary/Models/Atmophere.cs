namespace GeneratorLibrary.Models
{
    public record Atmosphere
    {
        public double Mass { get; set; }
        public List<string>? Composition { get; set; }
        public List<AtmosphereCharacteristic>? Characteristics { get; set; }
        public MarginalAtmosphere? MarginalAtmosphere { get; set; }
        public double Pressure { get; set; }
        public PressureCategory? PressureCategory { get; set; }
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

    public enum PressureCategory
    {
        None,
        Trace,
        VeryThin,
        Thin,
        Standard,
        Dense,
        VeryDense,
        Superdense
    }
}
