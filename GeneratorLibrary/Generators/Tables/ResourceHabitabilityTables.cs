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


    }
}
