using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorLibrary.Models;

namespace GeneratorLibrary.Generators.Tables
{
    public static class CharacteristicsTables
    {
        public static double GenerateWorldDensity(WorldSize size, WorldSubType subType)
        {
            int roll = DiceRoller.Instance.Roll(3);
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

    }
}
