namespace GeneratorLibrary.Models.Basic
{
    public record Population
    {
        public double CarryingCapacity { get; set; }
        public double CurrentPopulation { get; set; }
        public int PopulationRating { get; set; }
    }
}
