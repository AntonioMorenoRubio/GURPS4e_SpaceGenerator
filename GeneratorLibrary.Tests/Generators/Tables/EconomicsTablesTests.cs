using GeneratorLibrary.Generators.Tables;

namespace GeneratorLibrary.Tests.Generators.Tables
{
    public class EconomicsTablesTests
    {
        public static IEnumerable<object[]> IncomeModifiersTestData()
        {
            for (int affinity = -5; affinity <= 10; affinity++)
                for (int pr = 0; pr <= 12; pr++)
                {
                    double expectedAffinityModifier = affinity switch
                    {
                        10 => 1.4,
                        9 => 1.2,
                        >= 7 and <= 8 => 1.0,
                        >= 4 and <= 6 => 0.9,
                        >= 1 and <= 3 => 0.8,
                        _ => 0.7
                    };

                    double expectedPopulationModifier = pr switch
                    {
                        >= 6 => 1.0,
                        5 => 0.9,
                        _ => 0.8
                    };

                    yield return new object[] { affinity, pr, expectedAffinityModifier, expectedPopulationModifier };
                }
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
        public void GetIncomeModifiers_ShouldReturnCorrectModifiers(int affinity, int populationRating, double expectedAffinity, double expectedPopulation)
        {
            // Act
            var result = EconomicsTables.GetIncomeModifiers(affinity, populationRating);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(expectedAffinity, result[0]);
            Assert.Equal(expectedPopulation, result[1]);
        }
    }
}
