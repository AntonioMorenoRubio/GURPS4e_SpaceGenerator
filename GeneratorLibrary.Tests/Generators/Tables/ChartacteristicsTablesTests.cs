using GeneratorLibrary.Generators.Tables;
using GeneratorLibrary.Models;

namespace GeneratorLibrary.Tests.Generators.Tables
{
    public class ChartacteristicsTablesTests
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
    }
}
