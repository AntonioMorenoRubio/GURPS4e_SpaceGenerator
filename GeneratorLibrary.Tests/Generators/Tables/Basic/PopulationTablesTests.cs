using GeneratorLibrary.Generators.Tables.Basic;
using GeneratorLibrary.Tests.Utils;
using GeneratorLibrary.Utils;
using Moq;

namespace GeneratorLibrary.Tests.Generators.Tables.Basic
{
    public class PopulationTablesTests
    {
        const double MAX_POPULATION_CONSIDERED = 100_000_000_000_000d;  //100 Trillion (UK/US) | 100 Billion (EU)
        const int MIN_TECH_FOR_POPULATION = 7;
        const int MIN_AFFINITY_FOR_POPULATION = 3;

        public static IEnumerable<object[]> PopulationRatingTestData()
        {
            for (double i = 1; i < MAX_POPULATION_CONSIDERED; i *= 10)
            {
                yield return new object[] { i, (int)Math.Log10(i) };
                yield return new object[] { i * 10 - 1, (int)Math.Log10(i * 10 - 1) };
            }
        }

        [Theory]
        [InlineData(0, 0)]
        [MemberData(nameof(PopulationRatingTestData))]
        public void CalculatePopulationRating_ReturnsCorrectPopulationRating(double population, int expected)
        {
            //Act
            int actual = PopulationTables.CalculatePopulationRating(population);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 10_000)]
        [InlineData(1, 100_000)]
        [InlineData(2, 500_000)]
        [InlineData(3, 600_000)]
        [InlineData(4, 700_000)]
        [InlineData(5, 2_500_000)]
        [InlineData(6, 5_000_000)]
        [InlineData(7, 7_500_000)]
        [InlineData(8, 10_000_000)]
        [InlineData(9, 15_000_000)]
        [InlineData(10, 20_000_000)]
        [InlineData(11, 50_000_000)]
        [InlineData(12, 50_000_000)]
        public void GetBaseCapacityBasedOnTL_ReturnsCorrectValue(int techLevel, double expected)
        {
            //Act
            double actual = PopulationTables.GetBaseCapacityBasedOnTL(techLevel);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(10, 1_000)]
        [InlineData(9, 500)]
        [InlineData(8, 250)]
        [InlineData(7, 130)]
        [InlineData(6, 60)]
        [InlineData(5, 30)]
        [InlineData(4, 15)]
        [InlineData(3, 8)]
        [InlineData(2, 4)]
        [InlineData(1, 2)]
        [InlineData(0, 1)]
        [InlineData(-1, 0.5)]
        [InlineData(-2, 0.25)]
        [InlineData(-3, 0.13)]
        [InlineData(-4, 0.06)]
        [InlineData(-5, 0.03)]
        public void GetAffinityMultiplier_ReturnsCorrectValue(int affinity, double expected)
        {
            //Act
            double actual = PopulationTables.GetAffinityMultiplier(affinity);

            //Assert
            Assert.Equal(expected, actual);
        }


        public static IEnumerable<object[]> AsteroidBeltCarryingCapacityTestData()
        {
            for (int i = TechLevelTablesTests.MIN_TL; i <= TechLevelTablesTests.MAX_TL; i++)
                for (int j = ResourceHabitabilityTablesTests.MINIMUM_AFFINITY; j < ResourceHabitabilityTablesTests.MAXIMUM_AFFINITY; j++)
                {
                    if (i <= MIN_TECH_FOR_POPULATION && j <= MIN_AFFINITY_FOR_POPULATION)
                        yield return new object[] { i, j, 0 };
                    else
                        yield return new object[] {
                        i,
                        j,
                        PopulationTables.RoundToThousands(PopulationTables.GetBaseCapacityBasedOnTL(i) * PopulationTables.GetAffinityMultiplier(j) * 50)
                    };
                }
        }

        [Theory]
        [MemberData(nameof(AsteroidBeltCarryingCapacityTestData))]
        public void CalculateAsteroidCarryingCapacity_ShouldReturnExpectedValue(int techLevel, int affinity, double expected)
        {
            // Act
            double result = PopulationTables.CalculateAsteroidCarryingCapacity(techLevel, affinity);

            // Assert
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> Worlds_NoAsteroid_CarryingCapacityTestData()
        {
            for (int i = TechLevelTablesTests.MIN_TL; i <= TechLevelTablesTests.MAX_TL; i++)
                for (int j = ResourceHabitabilityTablesTests.MINIMUM_AFFINITY; j <= ResourceHabitabilityTablesTests.MAXIMUM_AFFINITY; j++)
                    for (double k = 0.0; k <= 2.0; k += 0.1)
                    {
                        if (i <= MIN_TECH_FOR_POPULATION && j <= MIN_AFFINITY_FOR_POPULATION)
                            yield return new object[] { i, j, k, 0 };
                        else
                            yield return new object[] {
                            i,
                            j,
                            k,
                            PopulationTables.RoundToThousands(
                                PopulationTables.GetBaseCapacityBasedOnTL(i) *
                                PopulationTables.GetAffinityMultiplier(j) *
                                Math.Pow(k, 2))
                        };
                    }
        }

        [Theory]
        [MemberData(nameof(Worlds_NoAsteroid_CarryingCapacityTestData))]
        public void CalculateCarryingCapacity_ShouldReturnExpectedValue(int techLevel, int affinity, double diameter, double expected)
        {
            // Act
            double result = PopulationTables.CalculateWorldCarryingCapacity(techLevel, affinity, diameter);

            // Assert
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> HomeworldPopulationTL4OrLessTestData()
        {
            for (double carryingCapacity = 1_000; carryingCapacity <= MAX_POPULATION_CONSIDERED; carryingCapacity *= 10)
                for (int techLevel = TechLevelTablesTests.MIN_TL; techLevel <= 4; techLevel++)
                    foreach (object[] roll in DiceRollerTests.Valid2dDiceRollValues())
                        yield return new object[] {
                            carryingCapacity,
                            techLevel,
                            roll[0],
                            ((int)roll[0] + 3) / 10.0 * carryingCapacity };
        }

        public static IEnumerable<object[]> HomeworldPopulationTL5OrMoreTestData()
        {
            for (double carryingCapacity = 1_000; carryingCapacity <= MAX_POPULATION_CONSIDERED; carryingCapacity *= 10)
                for (int techLevel = 5; techLevel <= TechLevelTablesTests.MAX_TL; techLevel++)
                    foreach (object[] roll in DiceRollerTests.Valid2dDiceRollValues())
                        yield return new object[] {
                            carryingCapacity,
                            techLevel,
                            roll[0],
                            PopulationTables.RoundToThousands(carryingCapacity * 10 / (int)roll[0]) };
        }

        [Theory]
        [MemberData(nameof(HomeworldPopulationTL4OrLessTestData))]
        [MemberData(nameof(HomeworldPopulationTL5OrMoreTestData))]
        public void GenerateHomeworldPopulation_ShouldReturnValidPopulation(double carryingCapacity, int techLevel, int roll, double expectedPopulation)
        {
            // Act
            double result = PopulationTables.GenerateHomeworldPopulation(carryingCapacity, techLevel, roll);

            // Assert
            Assert.Equal(expectedPopulation, result);
        }

        public static IEnumerable<object[]> ColonyPopulationTestData()
        {
            foreach (object[] roll in DiceRollerTests.Valid3dDiceRollValues())
                for (int affinity = ResourceHabitabilityTablesTests.MINIMUM_AFFINITY; affinity <= ResourceHabitabilityTablesTests.MAXIMUM_AFFINITY; affinity++)
                    for (int yearsSinceFounded = 0; yearsSinceFounded <= 250; yearsSinceFounded += 50)
                    {
                        int modifier = affinity * 3 + yearsSinceFounded / 10;
                        int finalRoll = (int)roll[0] + modifier;

                        if (finalRoll <= 25)
                            yield return new object[] {
                            affinity,
                            roll[0],
                            yearsSinceFounded,
                            10_000 };
                        else if (PopulationTables.GetColonyBasicPopulation().TryGetValue(finalRoll, out double value))
                            yield return new object[] {
                            affinity,
                            roll[0],
                            yearsSinceFounded,
                            value };
                        else
                        {
                            int e10 = 0;

                            while (!PopulationTables.GetColonyBasicPopulation().ContainsKey(finalRoll))
                            {
                                e10++;
                                finalRoll -= 10;
                            }
                            yield return new object[] {
                            affinity,
                            roll[0],
                            yearsSinceFounded,
                            PopulationTables.GetColonyBasicPopulation()[finalRoll] * Math.Pow(10, e10) };
                        }
                    }

        }

        [Theory]
        [MemberData(nameof(ColonyPopulationTestData))]
        public void GenerateColonyPopulation_ShouldReturnExpectedPopulation(int affinity, int roll, int yearsSinceFounded, double expectedPopulation)
        {
            // Act
            double result = PopulationTables.GenerateColonyPopulation(affinity, roll, yearsSinceFounded);

            // Assert
            Assert.Equal(expectedPopulation, result);
        }

        public static IEnumerable<object[]> OutpostPopulationTestData()
        {
            foreach (object[] roll in DiceRollerTests.Valid3dDiceRollValues())
                for (double i = -0.25; i <= 0.25; i += 0.01)
                {
                    PopulationTables.GetOutpostBasicPopulation().TryGetValue((int)roll[0], out double baseValue);
                    yield return new object[] {
                        roll[0],
                        i,
                        Math.Round(baseValue * 0.75,0),
                        Math.Round(baseValue * 1.25,0)
                    };
                }
        }

        [Theory]
        [MemberData(nameof(OutpostPopulationTestData))]
        public void GenerateOutpostPopulation_ShouldApplyVariationAndClampCorrectly(int roll, double variationFactor, double minExpected, double maxExpected)
        {
            // Act
            double result = PopulationTables.GenerateOutpostPopulation(roll, variationFactor);

            // Assert
            Assert.InRange(result, minExpected, maxExpected);
        }
    }
}
