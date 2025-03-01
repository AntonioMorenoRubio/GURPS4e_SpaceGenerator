using System.Linq;
using GeneratorLibrary.Models;
using GeneratorLibrary.Utils;

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

        public static List<decimal> GetIncomeModifiers(int affinity, int populationRating)
        {
            List<decimal> modifiers = new();

            decimal affinityModifier = affinity switch
            {
                10 => 1.4m,  // +40%
                9 => 1.2m,   // +20%
                >= 7 and <= 8 => 1.0m, // +0%
                >= 4 and <= 6 => 0.9m, // -10%
                >= 1 and <= 3 => 0.8m, // -20%
                <= 0 => 0.7m,  // -30%
                _ => 1.0m
            };

            // Aplicar modificadores según PR
            decimal populationModifier = populationRating switch
            {
                >= 6 => 1.0m, // +0%
                5 => 0.9m,    // -10%
                <= 4 => 0.8m, // -20%
            };

            modifiers.Add(affinityModifier);
            modifiers.Add(populationModifier);

            return modifiers;
        }

        public static decimal GetFinalPerCapitaIncome(decimal basePerCapitaIncome, List<decimal> modifiers, double carryingCapacity, double population)
        {
            decimal finalPerCapitaIncome = modifiers.Aggregate(basePerCapitaIncome, (income, modifier) => income * modifier);

            if (carryingCapacity < population)
                finalPerCapitaIncome *= (decimal)(carryingCapacity / population);

            return finalPerCapitaIncome.RoundToSignificantFigures(2);
        }

        public static WealthLevel GetTypicalWealthLevel(decimal finalPerCapitaIncome, decimal basePerCapitaIncome) => (finalPerCapitaIncome / basePerCapitaIncome) switch
        {
            >= 1.4m => WealthLevel.Comfortable,
            >= 0.73m => WealthLevel.Average,
            >= 0.32m => WealthLevel.Struggling,
            >= 0.1m => WealthLevel.Poor,
            _ => WealthLevel.DeadBroke
        };

        public static decimal CalculateEconomicVolume(decimal finalPerCapitaIncome, double population)
        {
            return (finalPerCapitaIncome * (decimal)population).RoundToSignificantFigures(2);
        }

        public static decimal CalculateTradeVolumeInTrillionsOfMoney(decimal factor, decimal world1EconomicVolume, decimal world2EconomicVolume, double distance)
        {
            decimal world1Trillions = world1EconomicVolume / 1_000_000_000_000m;
            decimal world2Trillions = world2EconomicVolume / 1_000_000_000_000m;

            return factor * world1Trillions * world2Trillions / (decimal)distance;
        }
    }
}
