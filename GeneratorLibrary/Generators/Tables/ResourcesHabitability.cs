namespace GeneratorLibrary.Generators.Tables
{
    public record ResourcesHabitability
    {
        //Mins and Maxs in S88.
        private const int _MIN_HABITABILITY = -2;
        private const int _MAX_HABITABILITY = 8;
        private const int _MIN_AFFINITY = -5;
        private const int _MAX_AFFINITY = 10;

        public int ResourceValueModifier { get; set; }
        public ResourceOverallValue ResourceOverall { get; set; }
        public List<int> HabitabilityModifiers { get; set; } = new List<int>();
        public int Habitability => Math.Clamp(HabitabilityModifiers.Sum(), _MIN_HABITABILITY, _MAX_HABITABILITY);
        public int Affinity => Math.Clamp(ResourceValueModifier + HabitabilityModifiers.Sum(), _MIN_AFFINITY, _MAX_AFFINITY);
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
