namespace GeneratorLibrary.Generators.Tables
{
    public static class EconomicsTables
    {
        public static decimal GetBasePerCapitaIncome(int techLevel) => techLevel switch
        {
            >= 12 => 130_000,
            11 => 97_000,
            10 => 67_000,
            9 => 43_000,
            8 => 31_000,
            7 => 25_000,
            6 => 19_000,
            5 => 13_000,
            4 => 9_600,
            3 => 8_400,
            2 => 8_100,
            1 => 7_800,
            _ => 7_500
        };

        public static List<double> GetIncomeModifiers(int affinity, int populationRating)
        {
            List<double> modifiers = new();

            double affinityModifier = affinity switch
            {
                10 => 1.4,  // +40%
                9 => 1.2,   // +20%
                >= 7 and <= 8 => 1.0, // +0%
                >= 4 and <= 6 => 0.9, // -10%
                >= 1 and <= 3 => 0.8, // -20%
                <= 0 => 0.7,  // -30%
                _ => 1.0
            };

            // Aplicar modificadores según PR
            double populationModifier = populationRating switch
            {
                >= 6 => 1.0, // +0%
                5 => 0.9,    // -10%
                <= 4 => 0.8, // -20%
            };

            modifiers.Add(affinityModifier);
            modifiers.Add(populationModifier);

            return modifiers;
        }
    }
}
