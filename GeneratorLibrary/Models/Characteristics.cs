namespace GeneratorLibrary.Models
{
    public record Characteristics
    {
        public double Density { get; set; } //Densidades Tierra
        public double DensityGCC => Math.Round(Density * 5.52, 2);
        public double Diameter { get; set; }
        public double DiameterMiles => Math.Round(Diameter * 7930);
        public double DiameterKilometers => Math.Round(Diameter * 12756, 2);
        public double SurfaceGravity { get; set; }
        public double SurfaceDensityInMetresPerSecondSq => Math.Round(SurfaceGravity * 9.81, 2);
    }
}
