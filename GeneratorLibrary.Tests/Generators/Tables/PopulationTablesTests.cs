using GeneratorLibrary.Generators.Tables;

namespace GeneratorLibrary.Tests.Generators.Tables
{
    public class PopulationTablesTests
    {
        [Theory]
        // Cinturón de Asteroides (multiplicador especial)
        [InlineData(8, 5, 15_000_000_000)] // TL8, Afinidad 5, Asteroides → 15B
        [InlineData(9, -2, 188_000_000)]   // TL9, Afinidad -2, Asteroides → 188M
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
    }
}
