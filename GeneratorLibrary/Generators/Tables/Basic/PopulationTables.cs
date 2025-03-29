namespace GeneratorLibrary.Generators.Tables.Basic
{
    public static class PopulationTables
    {
        public static double GetBaseCapacityBasedOnTL(int techLevel)
        {
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
            return baseCapacity;
        }

        public static double GetAffinityMultiplier(int affinity)
        {
            return affinity switch
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
        }

        public static double CalculateAsteroidCarryingCapacity(int techLevel, int affinity)
        {
            if (affinity <= 3 && techLevel <= 7)
                return 0;

            double baseCapacity = GetBaseCapacityBasedOnTL(techLevel);

            double affinityMultiplier = GetAffinityMultiplier(affinity);

            double carryingCapacity = baseCapacity * affinityMultiplier * 50;
            return RoundToThousands(carryingCapacity);
        }

        public static double CalculateWorldCarryingCapacity(int techLevel, int affinity, double? diameter)
        {
            if (affinity <= 3 && techLevel <= 7)
                return 0;

            double baseCapacity = GetBaseCapacityBasedOnTL(techLevel);

            double affinityMultiplier = GetAffinityMultiplier(affinity);

            double carryingCapacity = baseCapacity * affinityMultiplier;

            if (diameter is not null)
                carryingCapacity *= Math.Pow((double)diameter, 2);

            return RoundToThousands(carryingCapacity);
        }

        public static double RoundToThousands(double value)
        {
            const double _TOLERANCE = 1e-10;
            if (Math.Abs(value) < _TOLERANCE)
                return 0;

            return Math.Round(value / 1000) * 1000;
        }

        public static double GenerateHomeworldPopulation(double carryingCapacity, int techLevel, int roll)
        {
            if (techLevel <= 4)
            {
                // TL4 o menor: población entre 50% y 150% de la capacidad de carga: ((2d + 3) / 10) * Capacidad
                double factor = (roll + 3) / 10.0;
                return carryingCapacity * factor;
            }
            else
            {
                // TL5 o mayor: Capacidad * 10 dividido por la tirada (2d)
                return RoundToThousands(carryingCapacity * 10 / roll);
            }
        }

        private static readonly Dictionary<int, double> _colonyBasicPopulation = new()
        {
            { 25, 10_000 },
            { 26, 13_000 },
            { 27, 15_000 },
            { 28, 20_000 },
            { 29, 25_000 },
            { 30, 30_000 },
            { 31, 40_000 },
            { 32, 50_000 },
            { 33, 60_000 },
            { 34, 80_000 }
        };

        public static Dictionary<int, double> GetColonyBasicPopulation() => _colonyBasicPopulation;

        public static double GenerateColonyPopulation(int affinity, int roll, int yearsSinceFounded = 0)
        {
            // Modificador: +3 * affinity +1 por cada 10 años de la colonia
            int modifier = affinity * 3 + yearsSinceFounded / 10;
            int finalRoll = roll + modifier;

            if (finalRoll <= 25)
                return 10_000;

            if (_colonyBasicPopulation.TryGetValue(finalRoll, out double value))
                return value;

            int e10 = 0;

            while (!_colonyBasicPopulation.ContainsKey(finalRoll))
            {
                e10++;
                finalRoll -= 10;
            }

            return _colonyBasicPopulation[finalRoll] * Math.Pow(10, e10);
        }

        private static readonly Dictionary<int, double> _outpostBasicPopulation = new()
        {
            { 3 , 100 },
            { 4 , 150 },
            { 5 , 250 },
            { 6 , 400 },
            { 7 , 600 },
            { 8 , 1_000 },
            { 9 , 1_500 },
            { 10 , 2_500 },
            { 11 , 4_000 },
            { 12 , 6_000 },
            { 13 , 10_000 },
            { 14 , 15_000 },
            { 15 , 25_000 },
            { 16 , 40_000 },
            { 17 , 60_000 },
            { 18 , 100_000 }
        };

        public static Dictionary<int, double> GetOutpostBasicPopulation() => _outpostBasicPopulation;

        public static double GenerateOutpostPopulation(int roll, double variationFactor)
        {
            double basePopulation = roll switch
            {
                3 => 100,
                4 => 150,
                5 => 250,
                6 => 400,
                7 => 600,
                8 => 1_000,
                9 => 1_500,
                10 => 2_500,
                11 => 4_000,
                12 => 6_000,
                13 => 10_000,
                14 => 15_000,
                15 => 25_000,
                16 => 40_000,
                17 => 60_000,
                18 => 100_000,
                _ => throw new ArgumentOutOfRangeException($"Invalid roll for outpost population generation: {roll}")
            };

            double variedPopulation = Math.Round(basePopulation * (1 + variationFactor), 0);

            // Asegurar que la población nunca sea menor que 0
            return Math.Max(0, variedPopulation);
        }

        public static int CalculatePopulationRating(double population)
        {
            int counter = 0;

            while (population >= 10)
            {
                counter++;
                population /= 10;
            }

            return counter;
        }
    }
}
