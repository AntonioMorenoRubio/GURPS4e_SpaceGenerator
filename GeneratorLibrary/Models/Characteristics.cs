namespace GeneratorLibrary.Models
{
    public record Characteristics
    {
        public double Density { get; set; } //Densidades Tierra
        public double DensityGCC => Math.Round(Density * 5.52, 2);
    }

}
