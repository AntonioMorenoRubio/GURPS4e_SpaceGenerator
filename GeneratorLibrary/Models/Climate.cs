namespace GeneratorLibrary.Models
{
    public record Climate
    {
        public double Kelvin { get; set; }
        public double Celsius => Math.Round(Kelvin - 273.15, 2);
        public double Farenheit => Math.Round(Celsius * (9 / 5.0) + 32, 2);
        public ClimateType ClimateType { get; set; }
        public double BlackBodyCorrection { get; set; }
        public double BlackBodyTemperature { get; set; }
    }

    public enum ClimateType
    {
        Frozen,
        VeryCold,
        Cold,
        Chilly,
        Cool,
        Normal,
        Warm,
        Tropical,
        Hot,
        VeryHot,
        Infernal
    }
}
