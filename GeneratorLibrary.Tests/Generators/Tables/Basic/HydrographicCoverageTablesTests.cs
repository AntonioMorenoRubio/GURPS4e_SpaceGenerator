using GeneratorLibrary.Generators.Tables.Basic;
using GeneratorLibrary.Models.Basic;

namespace GeneratorLibrary.Tests.Generators.Tables.Basic
{
    public class HydrographicCoverageTablesTests
    {
        public static IEnumerable<object[]> HydrographicCoverageTestData => new List<object[]>
        {
            new object[] { WorldSize.Special, WorldSubType.AsteroidBelt, 0.0, 0.0 },
            new object[] { WorldSize.Tiny, WorldSubType.Rock, 0.0, 0.0 },
            new object[] { WorldSize.Small, WorldSubType.Rock, 0.0, 0.0 },

            new object[] { WorldSize.Tiny, WorldSubType.Ice, 0.0 , 0.0 },
            new object[] { WorldSize.Small, WorldSubType.Hadean, 0.0 , 0.0 },
            new object[] { WorldSize.Standard, WorldSubType.Hadean, 0.0 , 0.0 },

            new object[] { WorldSize.Tiny, WorldSubType.Sulfur, 0.0 , 0.0 },

            new object[] { WorldSize.Standard, WorldSubType.Chthonian, 0.0 , 0.0 },
            new object[] { WorldSize.Large, WorldSubType.Chthonian, 0.0 , 0.0 },

            new object[] { WorldSize.Small, WorldSubType.Ice, 30.0, 80.0 },
            new object[] { WorldSize.Standard, WorldSubType.Ammonia, 50.0, 100.0 },
            new object[] { WorldSize.Large, WorldSubType.Ammonia, 50.0, 100.0 },
            new object[] { WorldSize.Standard, WorldSubType.Ice, 0.0, 20.0 },
            new object[] { WorldSize.Large, WorldSubType.Ice, 0.0, 20.0 },
            new object[] { WorldSize.Standard, WorldSubType.Ocean, 50.0, 100.0 },
            new object[] { WorldSize.Standard, WorldSubType.Garden, 50.0, 100.0 },
            new object[] { WorldSize.Large, WorldSubType.Ocean, 50.0, 100.0 },
            new object[] { WorldSize.Large, WorldSubType.Garden, 50.0, 100.0 },
            new object[] { WorldSize.Standard, WorldSubType.Greenhouse, 0.0, 50.0 },
            new object[] { WorldSize.Large, WorldSubType.Greenhouse, 0.0, 50.0 }
        };

        public static IEnumerable<object[]> HydrographicCompositionTestData => new List<object[]>
        {
            new object[] { WorldSize.Special, WorldSubType.AsteroidBelt, new List<string>() },
            new object[] { WorldSize.Tiny, WorldSubType.Rock, new List<string>() },
            new object[] { WorldSize.Small, WorldSubType.Rock, new List<string>() },
            new object[] { WorldSize.Tiny, WorldSubType.Ice, new List<string>() },
            new object[] { WorldSize.Small, WorldSubType.Hadean, new List<string>() },
            new object[] { WorldSize.Standard, WorldSubType.Hadean, new List<string>() },
            new object[] { WorldSize.Tiny, WorldSubType.Sulfur, new List<string>() },

            new object[] { WorldSize.Small, WorldSubType.Ice, new List<string> { "Liquid Hydrocarbons" } },
            new object[] { WorldSize.Standard, WorldSubType.Ammonia, new List<string> { "Ammonia", "Water" } },
            new object[] { WorldSize.Large, WorldSubType.Ammonia, new List<string> { "Ammonia", "Water" } },
            new object[] { WorldSize.Standard, WorldSubType.Ice, new List<string> { "Water", "Salts", "Seasonal Lakes" } },
            new object[] { WorldSize.Large, WorldSubType.Ice, new List<string> { "Water", "Salts", "Seasonal Lakes" } },
            new object[] { WorldSize.Standard, WorldSubType.Ocean, new List<string> { "Liquid Water" } },
            new object[] { WorldSize.Standard, WorldSubType.Garden, new List<string> { "Liquid Water" } },
            new object[] { WorldSize.Large, WorldSubType.Ocean, new List<string> { "Liquid Water" } },
            new object[] { WorldSize.Large, WorldSubType.Garden, new List<string> { "Liquid Water" } },
            new object[] { WorldSize.Standard, WorldSubType.Greenhouse, new List<string> { "Sulfuric Acid", "Toxic Oceans" } },
            new object[] { WorldSize.Large, WorldSubType.Greenhouse, new List<string> { "Sulfuric Acid", "Toxic Oceans" } },
            new object[] { WorldSize.Standard, WorldSubType.Chthonian, new List<string>() { "Possible Lava Lakes and Rivers" } },
            new object[] { WorldSize.Large, WorldSubType.Chthonian, new List<string>() { "Possible Lava Lakes and Rivers" } }
        };

        [Theory]
        [MemberData(nameof(HydrographicCoverageTestData))]
        public void GenerateHydrographicCoverage_ShouldReturnExpectedRange(WorldSize size, WorldSubType subType, double expectedMin, double expectedMax)
        {
            // Act
            double result = HydrographicCoverageTables.GenerateHydrographicCoverage(size, subType);

            // Assert
            Assert.InRange(result, expectedMin, expectedMax);
        }

        [Theory]
        [MemberData(nameof(HydrographicCompositionTestData))]
        public void GetHydrographicComposition_ShouldReturnCorrectComposition(WorldSize size, WorldSubType subType, List<string> expectedComposition)
        {
            // Act
            var result = HydrographicCoverageTables.GetHydrographicComposition(size, subType);

            // Assert
            Assert.Equal(expectedComposition, result);
        }
    }
}
