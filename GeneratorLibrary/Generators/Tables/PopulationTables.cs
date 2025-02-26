namespace GeneratorLibrary.Generators.Tables
{
    public static class PopulationTables
    {
        public static double CalculateAsteroidCarryingCapacity(int techLevel, int affinity)
        {
            if (affinity <= 3 && techLevel <= 7)
                return 0;

            double baseCapacity = techLevel switch
            {
                0 => 10_000,
                1 => 100_000,
                2 => 500_000,
                3 => 600_000,
                4 => 700_000,
                5 => 2_500_000,
                6 => 5_000_000,
                7 => 7_500_000,
                8 => 10_000_000,
                9 => 15_000_000,
                10 => 20_000_000,
                _ => 50_000_000 // TL11+ es decisión del GM, pero ponemos un valor base
            };

            double affinityMultiplier = affinity switch
            {
                10 => 1_000,
                9 => 500,
                8 => 250,
                7 => 130,
                6 => 60,
                5 => 30,
                4 => 15,
                3 => 8,
                2 => 4,
                1 => 2,
                0 => 1,
                -1 => 0.5,
                -2 => 0.25,
                -3 => 0.13,
                -4 => 0.06,
                -5 => 0.03,
                _ => throw new ArgumentOutOfRangeException(nameof(affinity), "Invalid affinity score")
            };

            double carryingCapacity = baseCapacity * affinityMultiplier * 50;
            return RoundToThousandsOrMillions(carryingCapacity);
        }

        public static double CalculateWorldCarryingCapacity(int techLevel, int affinity, double? diameter)
        {
            if (affinity <= 3 && techLevel <= 7)
                return 0;

            double baseCapacity = techLevel switch
            {
                0 => 10_000,
                1 => 100_000,
                2 => 500_000,
                3 => 600_000,
                4 => 700_000,
                5 => 2_500_000,
                6 => 5_000_000,
                7 => 7_500_000,
                8 => 10_000_000,
                9 => 15_000_000,
                10 => 20_000_000,
                _ => 50_000_000 // TL11+ es decisión del GM, pero ponemos un valor base
            };

            double affinityMultiplier = affinity switch
            {
                10 => 1_000,
                9 => 500,
                8 => 250,
                7 => 130,
                6 => 60,
                5 => 30,
                4 => 15,
                3 => 8,
                2 => 4,
                1 => 2,
                0 => 1,
                -1 => 0.5,
                -2 => 0.25,
                -3 => 0.13,
                -4 => 0.06,
                -5 => 0.03,
                _ => throw new ArgumentOutOfRangeException(nameof(affinity), "Invalid affinity score")
            };

            double carryingCapacity = baseCapacity * affinityMultiplier;

            if (diameter is not null)
                carryingCapacity *= Math.Pow((double)diameter, 2);

            return RoundToThousandsOrMillions(carryingCapacity);
        }

        private static double RoundToThousandsOrMillions(double value)
        {
            if (value == 0)
                return 0;

            double magnitude = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(value))));

            double roundingFactor = (magnitude >= 1_000_000) ? 1_000_000 : 1_000;
            return Math.Round(value / roundingFactor) * roundingFactor;
        }

    }
}
