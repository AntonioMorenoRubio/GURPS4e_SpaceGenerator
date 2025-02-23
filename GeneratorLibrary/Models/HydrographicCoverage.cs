namespace GeneratorLibrary.Models
{
    public record HydrographicCoverage
    {
        public float Coverage { get; set; }
        public List<string> Composition { get; set; } = new List<string>();
    }
}
