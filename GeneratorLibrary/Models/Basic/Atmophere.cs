namespace GeneratorLibrary.Models.Basic
{
    public record Atmosphere
    {
        public double Mass { get; set; }
        public List<string> Composition { get; set; } = new();
        public List<AtmosphereCharacteristic> Characteristics { get; set; } = new();
        public MarginalAtmosphere MarginalAtmosphere { get; set; }
        public double Pressure { get; set; }
        public PressureCategory PressureCategory { get; set; }
    }

    public enum AtmosphereCharacteristic
    {
        Suffocating,
        Corrosive,
        MildlyToxic,
        HighlyToxic,
        LethallyToxic,
        Marginal
    }

    public enum MarginalAtmosphere
    {
        None,
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
