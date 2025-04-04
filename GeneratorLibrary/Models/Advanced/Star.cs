namespace GeneratorLibrary.Models.Advanced
{
    public record Star
    {
        public double Mass { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Temperature { get; set; }
        public double Luminosity { get; set; }
        public string LuminosityClass { get; set; } = string.Empty;
        public double Radius_AU { get; set; }
        public double Radius_Solar => Math.Round(Radius_AU, 2);
    }
}
