using GeneratorLibrary.Generators.Tables;
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

        [Theory]
        [InlineData(10_000, 1_000_000, 10_000_000_000)]  // 10k * 1M → 10B
        [InlineData(50_000, 2_000_000, 100_000_000_000)] // 50k * 2M → 100B
        [InlineData(100_000, 10_000_000, 1_000_000_000_000)] // 100k * 10M → 1T
        [InlineData(250_000, 500_000, 120_000_000_000)] // 250k * 500k → 120B
        [InlineData(1_000_000, 100_000, 100_000_000_000)] // 1M * 100k → 100B
        [InlineData(10_000, 10, 100_000)] // 10k * 10 → 100k
        [InlineData(5_000, 50, 250_000)] // 5k * 50 → 250k
        [InlineData(0, 1_000_000, 0)] // Ingreso 0 → Volumen 0
        [InlineData(10_000, 0, 0)] // Población 0 → Volumen 0
        [InlineData(75_000, 4_000_000, 300_000_000_000)] // 75k * 4M → 300B

        public void GetEconomicVolume_ShouldReturnExpectedValue(decimal finalPerCapitaIncome, double population, decimal expectedVolume)
        {
            // Act
            decimal result = EconomicsTables.CalculateEconomicVolume(finalPerCapitaIncome, population);

            // Assert
            Assert.Equal(expectedVolume, result);
        }

        [Theory]
        ///// FACTOR 0 (Siempre 0) /////
        [InlineData(0, 1E12, 1E12, 10, 0)] // 1T y 1T con distancia 10
        [InlineData(0, 1E13, 1E13, 100, 0)] // 10T y 10T con distancia 100

        /////// FACTOR 0.01 /////
        [InlineData(0.01, 1E12, 2E12, 10, 2E-3)] // Distancia corta
        [InlineData(0.01, 5E12, 5E12, 50, 5E-3)] // Distancia media
        [InlineData(0.01, 1E13, 1E13, 1000, 1E-3)] // Distancia muy larga

        /////// FACTOR 0.1 /////
        [InlineData(0.1, 5E11, 1E12, 1, 5E-2)] // Volumen medio, distancia corta
        [InlineData(0.1, 5E11, 1E12, 10, 5E-3)] // Volumen medio, distancia corta
        [InlineData(0.1, 5E12, 5E12, 50, 5E-2)] // Distancia media
        [InlineData(0.1, 1E13, 1E13, 1000, 1E-2)] // Distancia muy larga

        ///// FACTOR 1 /////
        [InlineData(1, 1E11, 1E11, 1, 1E-2)] // Pequeña economía, corta distancia
        [InlineData(1, 1E11, 1E11, 5, 2E-3)] // Pequeña economía, corta distancia
        [InlineData(1, 1E12, 1E11, 1, 1E-1)] // Pequeña economía, corta distancia
        [InlineData(1, 1E11, 1E12, 1, 1E-1)] // Pequeña economía, corta distancia
        [InlineData(1, 1E12, 1E12, 1, 1E0)] // Pequeña economía, corta distancia
        [InlineData(1, 1E12, 1E12, 10, 1E-1)] // Economía media, corta distancia
        [InlineData(1, 1E13, 1E13, 100, 1E0)] // Gran economía, media distancia
        [InlineData(1, 1E13, 1E13, 1000, 1E-1)] // Gran economía, distancia extrema
        public void CalculateTradeVolumeInTrillionsOfMoney_ShouldReturnExpectedValue(
    decimal factor, decimal world1EconomicVolume, decimal world2EconomicVolume, double distance, decimal expectedTradeVolume)
        {
            // Act
            decimal result = EconomicsTables.CalculateTradeVolumeInTrillionsOfMoney(factor, world1EconomicVolume, world2EconomicVolume, distance);

            // Assert
            Assert.Equal(expectedTradeVolume, result);
        }

    }
}
