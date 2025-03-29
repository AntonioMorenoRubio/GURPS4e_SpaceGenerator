using GeneratorLibrary.Models.Basic;

namespace GeneratorLibrary.Generators.Tables.Basic
{
    public static class CharacteristicsTables
    {
        public static double GenerateWorldDensity(WorldSize size, WorldSubType subType, int roll)
        {
            double variation = Random.Shared.NextDouble() * 0.1 - 0.05;

            (double density, double minDensity, double maxDensity) = (size, subType) switch
            {
                (WorldSize.Tiny, WorldSubType.Ice) or
                (WorldSize.Tiny, WorldSubType.Sulfur) or
                (WorldSize.Small, WorldSubType.Hadean) or
                (WorldSize.Small, WorldSubType.Ice) or
                (WorldSize.Standard, WorldSubType.Hadean) or
                (WorldSize.Standard, WorldSubType.Ammonia) or
                (WorldSize.Large, WorldSubType.Ammonia)
                    => (GetIcyCoreDensity(roll), 0.3, 0.7),

                (WorldSize.Tiny, WorldSubType.Rock) or
                (WorldSize.Small, WorldSubType.Rock)
                    => (GetSmallIronCoreDensity(roll), 0.6, 1.0),

                _ => (GetLargeIronCoreDensity(roll), 0.8, 1.2)
            };

            // Aplicar la variación
            density += variation;

            // Asegurar que la densidad esté dentro de los valores permitidos
            return Math.Clamp(density, minDensity, maxDensity);
        }

        private static double GetIcyCoreDensity(int roll) => roll switch
        {
            <= 6 => 0.3,
            <= 10 => 0.4,
            <= 14 => 0.5,
            <= 17 => 0.6,
            18 => 0.7,
            _ => throw new ArgumentOutOfRangeException(nameof(roll), "Roll value is out of expected range.")
        };

        private static double GetSmallIronCoreDensity(int roll) => roll switch
        {
            <= 6 => 0.6,
            <= 10 => 0.7,
            <= 14 => 0.8,
            <= 17 => 0.9,
            18 => 1.0,
            _ => throw new ArgumentOutOfRangeException(nameof(roll), "Roll value is out of expected range.")
        };

        private static double GetLargeIronCoreDensity(int roll) => roll switch
        {
            <= 6 => 0.8,
            <= 10 => 0.9,
            <= 14 => 1.0,
            <= 17 => 1.1,
            18 => 1.2,
            _ => throw new ArgumentOutOfRangeException(nameof(roll), "Roll value is out of expected range.")
        };

        public static double GenerateWorldDiameter(WorldSize size, double blackbodyTemperature, double density, int roll)
        {
            // Obtener los valores de la tabla de restricciones de tamaño
            (double minSize, double maxSize) = size switch
            {
                WorldSize.Large => (0.065, 0.091),
                WorldSize.Standard => (0.030, 0.065),
                WorldSize.Small => (0.024, 0.030),
                WorldSize.Tiny => (0.004, 0.024),
                _ => throw new ArgumentOutOfRangeException(nameof(size), "Invalid world size.")
            };

            // Calcular el diámetro mínimo y máximo
            double factor = Math.Sqrt(blackbodyTemperature / density);
            double minDiameter = factor * minSize;
            double maxDiameter = factor * maxSize;

            // Calcular un diámetro aleatorio dentro del rango
            double rollFactor = roll * (0.1 * (maxDiameter - minDiameter));
            double diameter = minDiameter + rollFactor;

            // Aplicar variación aleatoria de ±5% del rango permitido
            double variation = (Random.Shared.NextDouble() * 0.1 - 0.05) * (maxDiameter - minDiameter);
            diameter = Math.Clamp(diameter + variation, minDiameter, maxDiameter);

            return diameter;
        }

        public static double GenerateWorldSurfaceGravity(double diameter, double density)
        {
            return density * diameter;
        }
    }
}
