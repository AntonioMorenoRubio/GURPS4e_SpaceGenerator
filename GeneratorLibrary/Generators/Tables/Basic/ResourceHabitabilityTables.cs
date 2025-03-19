using GeneratorLibrary.Models.Basic;

namespace GeneratorLibrary.Generators.Tables.Basic
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
            <= 2 => -3,
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

        public static List<int> GetHabitabilityModifiers(World world)
        {
            List<int> modifiers = new();

            // 1. Modificadores por Atmósfera
            if (world.Atmosphere is null ||
                world.Atmosphere.PressureCategory == PressureCategory.Trace)
            {
                modifiers.Add(0); // No atmósfera o Trace
            }
            else if (world.Atmosphere.Composition?.Contains("Oxygen") == false) //Atmósfera NO respirable
            {
                bool hasCorrosive = world.Atmosphere.Characteristics.Contains(AtmosphereCharacteristic.Corrosive);
                bool hasToxic = world.Atmosphere.Characteristics.Contains(AtmosphereCharacteristic.MildlyToxic) ||
                                world.Atmosphere.Characteristics.Contains(AtmosphereCharacteristic.HighlyToxic) ||
                                world.Atmosphere.Characteristics.Contains(AtmosphereCharacteristic.LethallyToxic);
                bool hasSuffocating = world.Atmosphere.Characteristics.Contains(AtmosphereCharacteristic.Suffocating);

                modifiers.Add((hasSuffocating, hasToxic, hasCorrosive) switch
                {
                    (true, true, true) => -2,  // Asfixiante, tóxica y corrosiva
                    (true, true, false) => -1, // Asfixiante y tóxica
                    (true, false, false) => 0, // Sólo asfixiante
                    _ => 0                     // Ninguna de las tres
                });
            }
            else // Atmósfera respirable
            {
                modifiers.Add(world.Atmosphere.PressureCategory switch
                {
                    PressureCategory.VeryThin => 1,
                    PressureCategory.Thin => 2,
                    PressureCategory.Standard or PressureCategory.Dense => 3,
                    PressureCategory.VeryDense or PressureCategory.Superdense => 1,
                    _ => 0
                });

                if (world.Atmosphere.MarginalAtmosphere == MarginalAtmosphere.None)
                {
                    modifiers.Add(1); // No marginal → +1
                }
            }

            // 2️. Modificadores por Hidrografía (Solo agua líquida)
            if (world.HydrographicCoverage is null || world.HydrographicCoverage.Coverage == 0d ||
                world.HydrographicCoverage.Composition.Contains("Liquid Water") != true)
                modifiers.Add(0);
            else
                modifiers.Add(world.HydrographicCoverage?.Coverage switch
                {
                    <= 59 => 1,
                    >= 60 and <= 90 => 2,
                    >= 91 and < 100 => 1,
                    100 => 0,
                    _ => 0
                });

            // 3️. Modificadores por Clima (Solo si la atmósfera es respirable)
            if (world.Atmosphere is not null &&
                world.Atmosphere.PressureCategory is not PressureCategory.Trace &&
                world.Atmosphere.Composition?.Contains("Oxygen") == true)
            {
                modifiers.Add(world.Climate?.ClimateType switch
                {
                    ClimateType.Frozen or ClimateType.VeryCold => 0,
                    ClimateType.Cold => 1,
                    ClimateType.Chilly or ClimateType.Cool or ClimateType.Normal or ClimateType.Warm or ClimateType.Tropical => 2,
                    ClimateType.Hot => 1,
                    ClimateType.VeryHot or ClimateType.Infernal => 0,
                    _ => 0
                });
            }

            return modifiers;
        }

    }
}
