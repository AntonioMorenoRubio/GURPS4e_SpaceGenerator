using GeneratorLibrary.Models.Basic;

namespace GeneratorLibrary.Generators.Tables.Basic
{
    public static class ClimateTables
    {
        public static double GenerateAverageSurfaceTemperatureInKelvinsByWorldType(WorldSize size, WorldSubType subType, int roll)
        {
            // Definir rangos y pasos según el tipo de mundo
            (double min, double max, double step) = (size, subType) switch
            {
                (WorldSize.Special, WorldSubType.AsteroidBelt) => (140.0, 500.0, 24.0),
                (WorldSize.Tiny, WorldSubType.Ice) => (80.0, 140.0, 4.0),
                (WorldSize.Tiny, WorldSubType.Sulfur) => (80.0, 140.0, 4.0),
                (WorldSize.Tiny, WorldSubType.Rock) => (140.0, 500.0, 24.0),
                (WorldSize.Small, WorldSubType.Hadean) => (50.0, 80.0, 2.0),
                (WorldSize.Small, WorldSubType.Ice) => (80.0, 140.0, 4.0),
                (WorldSize.Small, WorldSubType.Rock) => (140.0, 500.0, 24.0),
                (WorldSize.Standard, WorldSubType.Hadean) => (50.0, 80.0, 2.0),
                (WorldSize.Standard, WorldSubType.Ammonia) => (140.0, 215.0, 5.0),
                (WorldSize.Standard, WorldSubType.Ice) => (80.0, 230.0, 10.0),
                (WorldSize.Standard, WorldSubType.Ocean) => (250.0, 340.0, 6.0),
                (WorldSize.Standard, WorldSubType.Garden) => (250.0, 340.0, 6.0),
                (WorldSize.Standard, WorldSubType.Greenhouse) => (500.0, 950.0, 30.0),
                (WorldSize.Standard, WorldSubType.Chthonian) => (500.0, 950.0, 30.0),
                (WorldSize.Large, WorldSubType.Ammonia) => (140.0, 215.0, 5.0),
                (WorldSize.Large, WorldSubType.Ice) => (80.0, 230.0, 10.0),
                (WorldSize.Large, WorldSubType.Ocean) => (250.0, 340.0, 6.0),
                (WorldSize.Large, WorldSubType.Garden) => (250.0, 340.0, 6.0),
                (WorldSize.Large, WorldSubType.Greenhouse) => (500.0, 950.0, 30.0),
                (WorldSize.Large, WorldSubType.Chthonian) => (500.0, 950.0, 30.0),

                _ => throw new ArgumentOutOfRangeException($"No temperature rule for {size} {subType}")
            };

            double temperature = min + roll * step;
            double variation = Random.Shared.NextDouble() * step - step / 2;
            temperature += variation;

            return Math.Clamp(temperature, min, max);
        }

        public static ClimateType GetClimateTypeBasedOnKelvinTemperature(double kelvins) => kelvins switch
        {
            < 244 => ClimateType.Frozen,
            >= 244 and < 255 => ClimateType.VeryCold,
            >= 255 and < 266 => ClimateType.Cold,
            >= 266 and < 278 => ClimateType.Chilly,
            >= 278 and < 289 => ClimateType.Cool,
            >= 289 and < 300 => ClimateType.Normal,
            >= 300 and < 311 => ClimateType.Warm,
            >= 311 and < 322 => ClimateType.Tropical,
            >= 322 and < 333 => ClimateType.Hot,
            >= 333 and <= 344 => ClimateType.VeryHot,
            > 344 => ClimateType.Infernal,
            _ => throw new ArgumentOutOfRangeException($"No rule for {kelvins}")
        };

        private static readonly Dictionary<WorldSubType, double> AbsorptionFactorsTinyWorld = new()
        {
            { WorldSubType.Ice, 0.86 },
            { WorldSubType.Rock, 0.97 },
            { WorldSubType.Sulfur, 0.77 }
        };

        private static readonly Dictionary<WorldSubType, double> AbsorptionFactorsSmallWorld = new()
        {
            { WorldSubType.Hadean, 0.67 },
            { WorldSubType.Ice, 0.93 },
            { WorldSubType.Rock, 0.96 }
        };

        private static readonly Dictionary<WorldSubType, double> AbsorptionFactorsStandardOrLargeWorld = new()
        {
            { WorldSubType.Hadean, 0.67 },
            { WorldSubType.Ammonia, 0.84 },
            { WorldSubType.Ice, 0.86 },
            { WorldSubType.Greenhouse, 0.77 },
            { WorldSubType.Chthonian, 0.97 }
        };

        public static double GenerateBlackbodyCorrection(WorldSize size, WorldSubType subtype, double atmosphereMass = 0f, double hydrographicCoverage = 0f)
        {
            double absorptionFactor = GetAbsorptionFactor(size, subtype, hydrographicCoverage);
            double greenhouseFactor = GetGreenhouseFactor(size, subtype);
            return Math.Round(absorptionFactor * (1 + atmosphereMass * greenhouseFactor), 2);
        }

        public static double GetAbsorptionFactor(WorldSize size, WorldSubType subtype, double hydrographicCoverage)
        {
            if (subtype is WorldSubType.Garden or WorldSubType.Ocean)
                return GetOceanGardenAbsorptionFactor(hydrographicCoverage);

            return size switch
            {
                WorldSize.Special when subtype == WorldSubType.AsteroidBelt => 0.97, // Asteroid Belt
                WorldSize.Tiny => AbsorptionFactorsTinyWorld.GetValueOrDefault(subtype),
                WorldSize.Small => AbsorptionFactorsSmallWorld.GetValueOrDefault(subtype),
                WorldSize.Standard or WorldSize.Large => AbsorptionFactorsStandardOrLargeWorld.GetValueOrDefault(subtype),
                _ => throw new ArgumentOutOfRangeException($"Cannot get absorption factor based on world type: {size}."),
            };
        }

        public static double GetOceanGardenAbsorptionFactor(double hydrographicCoverage)
        {
            return hydrographicCoverage switch
            {
                < 21.0 => 0.95,
                < 51.0 => 0.92,
                < 91.0 => 0.88,
                _ => 0.84
            };
        }

        public static double GetGreenhouseFactor(WorldSize size, WorldSubType subType)
        {
            return (size, subType) switch
            {
                (WorldSize.Small, WorldSubType.Ice) => 0.1,
                (WorldSize.Standard, WorldSubType.Ammonia) or (WorldSize.Large, WorldSubType.Ammonia) or
                (WorldSize.Standard, WorldSubType.Ice) or (WorldSize.Large, WorldSubType.Ice) => 0.20,
                (WorldSize.Standard, WorldSubType.Ocean) or (WorldSize.Large, WorldSubType.Ocean) or
                (WorldSize.Standard, WorldSubType.Garden) or (WorldSize.Large, WorldSubType.Garden) => 0.16,
                (WorldSize.Standard, WorldSubType.Greenhouse) or (WorldSize.Large, WorldSubType.Greenhouse) => 2.0,
                _ => 0.0
            };
        }
    }
}
