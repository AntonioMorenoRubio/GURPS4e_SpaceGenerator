namespace GeneratorLibrary.Generators.Tables
{
    public record ResourcesHabitability
    {
        public int ResourceValueModifier { get; set; }
        public ResourceOverallValue ResourceOverall { get; set; }
    }

    public enum ResourceOverallValue
    {
        Worthless,
        VeryScant,
        Scant,
        VeryPoor,
        Poor,
        Average,
        Abundant,
        VeryAbundant,
        Rich,
        VeryRich,
        Motherlode
    }
}
