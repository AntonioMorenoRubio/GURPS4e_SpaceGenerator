﻿using GeneratorLibrary.Generators.Tables;
using GeneratorLibrary.Models;

namespace GeneratorLibrary.Tests.Generators.Tables
{
    public class EconomicsTablesTests
    {
        public static IEnumerable<object[]> IncomeModifiersTestData()
        {
            for (int affinity = -5; affinity <= 10; affinity++)
                for (int pr = 0; pr <= 12; pr++)
                {
                    decimal expectedAffinityModifier = affinity switch
                    {
                        10 => 1.4m,
                        9 => 1.2m,
                        >= 7 and <= 8 => 1.0m,
                        >= 4 and <= 6 => 0.9m,
                        >= 1 and <= 3 => 0.8m,
                        _ => 0.7m
                    };

                    decimal expectedPopulationModifier = pr switch
                    {
                        >= 6 => 1.0m,
                        5 => 0.9m,
                        _ => 0.8m
                    };

                    yield return new object[] { affinity, pr, expectedAffinityModifier, expectedPopulationModifier };
                }
        }

        public static IEnumerable<object[]> GetFinalPerCapitaIncomeTestData()
        {
            yield return new object[] { 31_000m, new List<decimal> { 1.0m }, 1_000_000, 1_000_000, 31_000m }; // Sin modificadores
            yield return new object[] { 31_000m, new List<decimal> { 1.2m, 0.9m }, 1_000_000, 1_000_000, 33_000m }; // Modificadores combinados
            yield return new object[] { 31_000m, new List<decimal> { 0.7m }, 1_000_000, 1_000_000, 22_000m }; // Modificador negativo (-30%)
            yield return new object[] { 31_000m, new List<decimal> { 1.0m }, 1_000_000, 2_000_000, 16_000m }; // Población > Capacidad
            yield return new object[] { 31_000m, new List<decimal> { 1.4m }, 2_000_000, 1_000_000, 43_000m }; // Capacidad > Población
            yield return new object[] { 31_000m, new List<decimal> { 1.0m }, 1_000_000, 0, 31_000m }; // Población 0
        }

        [Theory]
        [InlineData(12, 130_000)]
        [InlineData(11, 97_000)]
        [InlineData(10, 67_000)]
        [InlineData(9, 43_000)]
        [InlineData(8, 31_000)]
        [InlineData(7, 25_000)]
        [InlineData(6, 19_000)]
        [InlineData(5, 13_000)]
        [InlineData(4, 9_600)]
        [InlineData(3, 8_400)]
        [InlineData(2, 8_100)]
        [InlineData(1, 7_800)]
        [InlineData(0, 7_500)]
        public void GetBasePerCapitaIncome_ShouldReturnExpectedValue(int techLevel, decimal expectedIncome)
        {
            // Act
            decimal actual = EconomicsTables.GetBasePerCapitaIncome(techLevel);

            // Assert
            Assert.Equal(expectedIncome, actual);
        }

        [Theory]
        [MemberData(nameof(IncomeModifiersTestData))]
        public void GetIncomeModifiers_ShouldReturnCorrectModifiers(int affinity, int populationRating, decimal expectedAffinity, decimal expectedPopulation)
        {
            // Act
            var result = EconomicsTables.GetIncomeModifiers(affinity, populationRating);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(expectedAffinity, result[0]);
            Assert.Equal(expectedPopulation, result[1]);
        }

        [Theory]
        [MemberData(nameof(GetFinalPerCapitaIncomeTestData))]
        public void GetFinalPerCapitaIncome_ShouldReturnExpected(decimal baseIncome, List<decimal> modifiers, double carryingCapacity, double population, decimal expected)
        {
            // Act
            var result = EconomicsTables.GetFinalPerCapitaIncome(baseIncome, modifiers, carryingCapacity, population);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(140_000, 100_000, WealthLevel.Comfortable)] // 1.4x
        [InlineData(139_999, 100_000, WealthLevel.Average)]     // Justo debajo de 1.4x
        [InlineData(100_000, 100_000, WealthLevel.Average)]     // 1.0x
        [InlineData(73_000, 100_000, WealthLevel.Average)]      // 0.73x
        [InlineData(72_999, 100_000, WealthLevel.Struggling)]   // Justo debajo de 0.73x
        [InlineData(50_000, 100_000, WealthLevel.Struggling)]   // 0.5x
        [InlineData(32_000, 100_000, WealthLevel.Struggling)]   // 0.32x
        [InlineData(31_999, 100_000, WealthLevel.Poor)]         // Justo debajo de 0.32x
        [InlineData(20_000, 100_000, WealthLevel.Poor)]         // 0.2x
        [InlineData(10_000, 100_000, WealthLevel.Poor)]         // 0.1x
        [InlineData(9_999, 100_000, WealthLevel.DeadBroke)]     // Justo debajo de 0.1x
        [InlineData(5_000, 100_000, WealthLevel.DeadBroke)]     // 0.05x
        [InlineData(0, 100_000, WealthLevel.DeadBroke)]         // 0x

        public void GetTypicalWealthLevel_ShouldReturnExpectedWealth(decimal finalPerCapitaIncome, decimal basePerCapitaIncome, WealthLevel expectedWealth)
        {
            // Act
            WealthLevel result = EconomicsTables.GetTypicalWealthLevel(finalPerCapitaIncome, basePerCapitaIncome);

            // Assert
            Assert.Equal(expectedWealth, result);
        }

    }
}
