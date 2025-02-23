using GeneratorLibrary.Generators.Tables;
using GeneratorLibrary.Models;

namespace GeneratorLibrary.Tests.Generators.Tables
{
    public class HydrographicCoverageTablesTests
    {
        public static IEnumerable<object[]> HydrographicCoverageTestData => new List<object[]>
        {
            new object[] { WorldSize.Special, WorldSubType.AsteroidBelt, 0f, 0f },
            new object[] { WorldSize.Tiny, WorldSubType.Rock, 0f, 0f },
            new object[] { WorldSize.Small, WorldSubType.Rock, 0f, 0f },

            new object[] { WorldSize.Tiny, WorldSubType.Ice, 0f , 0f },
            new object[] { WorldSize.Small, WorldSubType.Hadean, 0f , 0f },
            new object[] { WorldSize.Standard, WorldSubType.Hadean, 0f , 0f },

            new object[] { WorldSize.Tiny, WorldSubType.Sulfur, 0f , 0f },

            new object[] { WorldSize.Standard, WorldSubType.Chthonian, 0f , 0f },
            new object[] { WorldSize.Large, WorldSubType.Chthonian, 0f , 0f },

            new object[] { WorldSize.Small, WorldSubType.Ice, 30f, 80f },
            new object[] { WorldSize.Standard, WorldSubType.Ammonia, 50f, 100f },
            new object[] { WorldSize.Large, WorldSubType.Ammonia, 50f, 100f },
            new object[] { WorldSize.Standard, WorldSubType.Ice, 0f, 20f },
            new object[] { WorldSize.Large, WorldSubType.Ice, 0f, 20f },
            new object[] { WorldSize.Standard, WorldSubType.Ocean, 50f, 100f },
            new object[] { WorldSize.Standard, WorldSubType.Garden, 50f, 100f },
            new object[] { WorldSize.Large, WorldSubType.Ocean, 50f, 100f },
            new object[] { WorldSize.Large, WorldSubType.Garden, 50f, 100f },
            new object[] { WorldSize.Standard, WorldSubType.Greenhouse, 0f, 50f },
            new object[] { WorldSize.Large, WorldSubType.Greenhouse, 0f, 50f }
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
        public void GenerateHydrographicCoverage_ShouldReturnExpectedRange(WorldSize size, WorldSubType subType, float expectedMin, float expectedMax)
        {
            // Act
            float result = HydrographicCoverageTables.GenerateHydrographicCoverage(size, subType);

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
