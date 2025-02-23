using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorLibrary.Generators.Tables;
using GeneratorLibrary.Models;

namespace GeneratorLibrary.Tests.Generators.Tables
{
    public class ClimateTablesTests
    {
        [Theory]
        [InlineData(WorldSize.Special, WorldSubType.AsteroidBelt, 140f, 500f)]
        [InlineData(WorldSize.Tiny, WorldSubType.Ice, 80f, 140f)]
        [InlineData(WorldSize.Tiny, WorldSubType.Sulfur, 80f, 140f)]
        [InlineData(WorldSize.Tiny, WorldSubType.Rock, 140f, 500f)]
        [InlineData(WorldSize.Small, WorldSubType.Hadean, 50f, 80f)]
        [InlineData(WorldSize.Small, WorldSubType.Ice, 80f, 140f)]
        [InlineData(WorldSize.Small, WorldSubType.Rock, 140f, 500f)]
        [InlineData(WorldSize.Standard, WorldSubType.Hadean, 50f, 80f)]
        [InlineData(WorldSize.Standard, WorldSubType.Ammonia, 140f, 215f)]
        [InlineData(WorldSize.Standard, WorldSubType.Ice, 80f, 230f)]
        [InlineData(WorldSize.Standard, WorldSubType.Ocean, 250f, 340f)]
        [InlineData(WorldSize.Standard, WorldSubType.Garden, 250f, 340f)]
        [InlineData(WorldSize.Standard, WorldSubType.Greenhouse, 500f, 950f)]
        [InlineData(WorldSize.Standard, WorldSubType.Chthonian, 500f, 950f)]
        [InlineData(WorldSize.Large, WorldSubType.Ammonia, 140f, 215f)]
        [InlineData(WorldSize.Large, WorldSubType.Ice, 80f, 230f)]
        [InlineData(WorldSize.Large, WorldSubType.Ocean, 250f, 340f)]
        [InlineData(WorldSize.Large, WorldSubType.Garden, 250f, 340f)]
        [InlineData(WorldSize.Large, WorldSubType.Greenhouse, 500f, 950f)]
        [InlineData(WorldSize.Large, WorldSubType.Chthonian, 500f, 950f)]
        public void GenerateAverageSurfaceTemperatureInKelvinsByWorldType_ShouldReturnValidRange(WorldSize size, WorldSubType subType, float minExpected, float maxExpected)
        {
            // Act
            float result = ClimateTables.GenerateAverageSurfaceTemperatureInKelvinsByWorldType(size, subType);

            // Assert
            Assert.InRange(result, minExpected, maxExpected);
        }

    }
}
