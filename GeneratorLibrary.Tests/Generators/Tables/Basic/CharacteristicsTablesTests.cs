using GeneratorLibrary.Generators.Tables.Basic;
using GeneratorLibrary.Models.Basic;

namespace GeneratorLibrary.Tests.Generators.Tables.Basic
{
    public class CharacteristicsTablesTests
    {
        [Theory]
        [InlineData(WorldSize.Tiny, WorldSubType.Ice, 0.3d, 0.7d)]
        [InlineData(WorldSize.Tiny, WorldSubType.Sulfur, 0.3d, 0.7d)]
        [InlineData(WorldSize.Small, WorldSubType.Hadean, 0.3d, 0.7d)]
        [InlineData(WorldSize.Small, WorldSubType.Ice, 0.3d, 0.7d)]
        [InlineData(WorldSize.Standard, WorldSubType.Hadean, 0.3d, 0.7d)]
        [InlineData(WorldSize.Standard, WorldSubType.Ammonia, 0.3d, 0.7d)]
        [InlineData(WorldSize.Large, WorldSubType.Ammonia, 0.3d, 0.7d)]
        [InlineData(WorldSize.Tiny, WorldSubType.Rock, 0.6d, 1.0d)]
        [InlineData(WorldSize.Small, WorldSubType.Rock, 0.6d, 1.0d)]
        [InlineData(WorldSize.Standard, WorldSubType.Ocean, 0.8d, 1.2d)]
        [InlineData(WorldSize.Standard, WorldSubType.Garden, 0.8d, 1.2d)]
        [InlineData(WorldSize.Standard, WorldSubType.Greenhouse, 0.8d, 1.2d)]
        [InlineData(WorldSize.Standard, WorldSubType.Chthonian, 0.8d, 1.2d)]
        [InlineData(WorldSize.Large, WorldSubType.Ocean, 0.8d, 1.2d)]
        [InlineData(WorldSize.Large, WorldSubType.Garden, 0.8d, 1.2d)]
        [InlineData(WorldSize.Large, WorldSubType.Greenhouse, 0.8d, 1.2d)]
        [InlineData(WorldSize.Large, WorldSubType.Chthonian, 0.8d, 1.2d)]
        public void GenerateWorldDensity_ReturnsCorrectDensityRange(WorldSize size, WorldSubType subType, double minDensity, double maxDensity)
        {
            // Act
            double density = CharacteristicsTables.GenerateWorldDensity(size, subType);

            // Assert
            Assert.InRange(density, minDensity, maxDensity);
        }

        [Theory]
        [InlineData(WorldSize.Large, 288, 0.9, 0.065, 0.091)]
        [InlineData(WorldSize.Standard, 255, 1.0, 0.030, 0.065)]
        [InlineData(WorldSize.Small, 200, 0.8, 0.024, 0.030)]
        [InlineData(WorldSize.Tiny, 150, 0.5, 0.004, 0.024)]
        public void GenerateWorldDiameter_ShouldReturnValidRange(
            WorldSize size, double blackbodyTemperature, double density, double minSize, double maxSize)
        {
            // Act
            double result = CharacteristicsTables.GenerateWorldDiameter(size, blackbodyTemperature, density);

            // Assert
            Assert.InRange(result, Math.Sqrt(blackbodyTemperature / density) * minSize,
                                     Math.Sqrt(blackbodyTemperature / density) * maxSize);
        }

        [Theory]
        [InlineData(0.5, 0.9, 0.45)]
        [InlineData(1.0, 1.0, 1.00)]
        [InlineData(1.2, 0.8, 0.96)]
        [InlineData(0.3, 1.2, 0.36)]
        public void GenerateWorldSurfaceGravity_ShouldReturnExpectedValue(double diameter, double density, double expected)
        {
            // Act
            double result = CharacteristicsTables.GenerateWorldSurfaceGravity(diameter, density);

            // Assert
            Assert.Equal(expected, result, precision: 2);
        }
    }
}
