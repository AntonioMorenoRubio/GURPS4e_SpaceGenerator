using GeneratorLibrary.Generators.Tables;

namespace GeneratorLibrary.Tests.Generators.Tables
{
    public class PopulationTablesTests
    {
        [Theory]
        [InlineData(1, 0)]
        [InlineData(9, 0)]
        [InlineData(10, 1)]
        [InlineData(99, 1)]
        [InlineData(100, 2)]
        [InlineData(999, 2)]
        [InlineData(1_000, 3)]
        [InlineData(9_999, 3)]
        [InlineData(10_000, 4)]
        [InlineData(99_999, 4)]
        [InlineData(100_000, 5)]
        [InlineData(999_999, 5)]
        [InlineData(1_000_000, 6)]
        [InlineData(9_999_999, 6)]
        [InlineData(10_000_000, 7)]
        [InlineData(99_999_999, 7)]
        [InlineData(100_000_000, 8)]
        [InlineData(999_999_999, 8)]
        [InlineData(1_000_000_000, 9)]
        [InlineData(9_999_999_999, 9)]
        [InlineData(10_000_000_000, 10)]
        [InlineData(99_999_999_999, 10)]
        [InlineData(100_000_000_000, 11)]
        [InlineData(999_999_999_999, 11)]
        [InlineData(1_000_000_000_000, 12)]
        [InlineData(9_999_999_999_999, 12)]
        public void CalculatePopulationRating_ReturnsCorrectPopulationRating(double population, int expected)
        {
            //Act
            int actual = PopulationTables.CalculatePopulationRating(population);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        // Cinturón de Asteroides (multiplicador especial)
        [InlineData(8, 5, 15_000_000_000)] // TL8, Afinidad 5, Asteroides → 15B
        [InlineData(9, -2, 187_500_000)]   // TL9, Afinidad -2, Asteroides → 187.5M
        public void CalculateAsteroidCarryingCapacity_ShouldReturnExpectedValue(int techLevel, int affinity, double expected)
        {
            // Act
            double result = PopulationTables.CalculateAsteroidCarryingCapacity(techLevel, affinity);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        // Casos con Carrying Capacity 0
        [InlineData(7, 3, 1.0, 0)]
        [InlineData(6, 2, 1.0, 0)]

        // Casos normales con diferentes TL, afinidades y diámetros
        [InlineData(8, 5, 1.0, 300_000_000)]  // TL8, Afinidad 5, Diámetro 1.0 → 300M
        [InlineData(9, 6, 1.2, 1_296_000_000)] // TL9, Afinidad 6, Diámetro 1.2 → 1.3B
        [InlineData(10, 4, 0.8, 192_000_000)]  // TL10, Afinidad 4, Diámetro 0.8 → 192M
        [InlineData(10, 0, 1.0, 20_000_000)]  // TL10, Afinidad 0, Diámetro 1.0 → 20M
        public void CalculateCarryingCapacity_ShouldReturnExpectedValue(int techLevel, int affinity, double diameter, double expected)
        {
            // Act
            double result = PopulationTables.CalculateWorldCarryingCapacity(techLevel, affinity, diameter);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1_000_000, 4, 2, 500_000)]   // TL4, Mínimo dado (50% de la capacidad)
        [InlineData(1_000_000, 4, 12, 1_500_000)] // TL4, Máximo dado (150% de la capacidad)
        [InlineData(10_000_000, 8, 2, 50_000_000)] // TL8, Mínimo dado (Capacidad * 10 / roll)
        [InlineData(10_000_000, 8, 12, 8_333_000)] // TL8, Máximo dado (Capacidad * 10 / roll)
        public void GenerateHomeworldPopulation_ShouldReturnValidPopulation(double carryingCapacity, int techLevel, int roll, double expectedPopulation)
        {
            // Act
            double result = PopulationTables.GenerateHomeworldPopulation(carryingCapacity, techLevel, roll);

            // Assert
            Assert.Equal(expectedPopulation, result);
        }

        [Theory]
        [InlineData(1_000_000, 3, 3, 50, 10_000)]  // Baja afinidad, mínimo dado, colonia reciente
        [InlineData(1_000_000, 3, 18, 50, 50_000)] // Baja afinidad, máximo dado
        [InlineData(10_000_000, 7, 3, 500, 800_000_000)]  // Alta afinidad, mínimo dado, colonia antigua
        [InlineData(10_000_000, 7, 18, 500, 25_000_000_000)] // Alta afinidad, máximo dado
        public void GenerateColonyPopulation_ShouldReturnExpectedPopulation(double carryingCapacity, int affinity, int roll, int yearsSinceFounded, double expectedPopulation)
        {
            // Act
            double result = PopulationTables.GenerateColonyPopulation(carryingCapacity, affinity, roll, yearsSinceFounded);

            // Assert
            Assert.Equal(expectedPopulation, result);
        }


        [Theory]
        [InlineData(3, 75, 125)]
        [InlineData(4, 113, 188)]
        [InlineData(5, 188, 313)]
        [InlineData(6, 300, 500)]
        [InlineData(7, 450, 750)]
        [InlineData(8, 750, 1_250)]
        [InlineData(9, 1_125, 1_875)]
        [InlineData(10, 1_875, 3_125)]
        [InlineData(11, 3_000, 5_000)]
        [InlineData(12, 4_500, 7_500)]
        [InlineData(13, 7_500, 12_500)]
        [InlineData(14, 11_250, 18_750)]
        [InlineData(15, 18_750, 31_250)]
        [InlineData(16, 30_000, 50_000)]
        [InlineData(17, 45_000, 75_000)]
        [InlineData(18, 75_000, 125_000)]

        public void GenerateOutpostPopulation_ShouldApplyVariationAndClampCorrectly(int roll, double minExpected, double maxExpected)
        {
            // Act
            double result = PopulationTables.GenerateOutpostPopulation(roll);

            // Assert
            Assert.InRange(result, minExpected, maxExpected);
        }
    }
}
