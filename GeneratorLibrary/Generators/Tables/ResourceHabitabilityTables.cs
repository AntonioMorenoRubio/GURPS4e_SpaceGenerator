namespace GeneratorLibrary.Generators.Tables
{
    public static class ResourceHabitabilityTables
    {
        public static int ResourceValueForAsteroidBelts(int roll) => roll switch
        {
            3 => -5,
            4 => -4,
            5 => -3,
            6 or 7 => -2,
            8 or 9 => -1,
            10 or 11 => 0,
            12 or 13 => 1,
            14 or 15 => 2,
            16 => 3,
            17 => 4,
            18 => 5,
            _ => throw new ArgumentOutOfRangeException($"No rule for roll value: {roll}")
        };

        public static int ResourceValueForOtherWorlds(int roll) => roll switch
        {
            < 3 => -3,
            3 or 4 => -2,
            5 or 6 or 7 => -1,
            >= 8 and <= 13 => 0,
            14 or 15 or 16 => 1,
            17 or 18 => 2,
            >= 19 => 3
        };

        public static ResourceOverallValue GetResourceOverallValue(int resourceValueModifier) => resourceValueModifier switch
        {
            -5 => ResourceOverallValue.Worthless,
            -4 => ResourceOverallValue.VeryScant,
            -3 => ResourceOverallValue.Scant,
            -2 => ResourceOverallValue.VeryPoor,
            -1 => ResourceOverallValue.Poor,
            0 => ResourceOverallValue.Average,
            1 => ResourceOverallValue.Abundant,
            2 => ResourceOverallValue.VeryAbundant,
            3 => ResourceOverallValue.Rich,
            4 => ResourceOverallValue.VeryRich,
            5 => ResourceOverallValue.Motherlode,
            _ => throw new ArgumentOutOfRangeException($"No rule for resourceValueModifier: {resourceValueModifier}.")
        };
    }
}
