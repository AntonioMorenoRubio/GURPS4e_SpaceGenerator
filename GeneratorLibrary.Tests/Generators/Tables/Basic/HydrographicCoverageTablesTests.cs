using System.Reflection;
using GeneratorLibrary.Generators.Tables.Basic;
using GeneratorLibrary.Models.Basic;
using GeneratorLibrary.Tests.Utils;
using GeneratorLibrary.Utils;
using Moq;

namespace GeneratorLibrary.Tests.Generators.Tables.Basic
{
    public class HydrographicCoverageTablesTests
    {
        public static IEnumerable<object[]> WorldsWithHydrographicCoverageTestData => new List<object[]>
        {
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

        public static IEnumerable<object[]> WorldsWithoutHydrographicCoverageTestData => new List<object[]>
        {
            new object[] { WorldSize.Special, WorldSubType.AsteroidBelt },
            new object[] { WorldSize.Tiny, WorldSubType.Rock },
            new object[] {WorldSize.Small, WorldSubType.Rock },
            new object[] {WorldSize.Tiny, WorldSubType.Ice },
            new object[] {WorldSize.Small, WorldSubType.Hadean },
            new object[] {WorldSize.Standard, WorldSubType.Hadean },
            new object[] {WorldSize.Tiny, WorldSubType.Sulfur },
            new object[] {WorldSize.Standard, WorldSubType.Chthonian },
            new object[] {WorldSize.Large, WorldSubType.Chthonian }
        };

        public static IEnumerable<object[]> CombinedWorldsWithHydrographicCoverageTestData()
        {
            foreach (var world in WorldsWithHydrographicCoverageTestData)
                foreach (var diceRoll in DiceRollerTests.AllTestDiceRolls())
                    yield return world.Append(diceRoll[0]).ToArray();
        }

        public static IEnumerable<object[]> CombinedWorldsWithoutHydrographicCoverageTestData()
        {
            foreach (var world in WorldsWithoutHydrographicCoverageTestData)
                foreach (var diceRoll in DiceRollerTests.AllTestDiceRolls())
                    yield return world.Append(diceRoll[0]).ToArray();
        }

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
        [MemberData(nameof(CombinedWorldsWithHydrographicCoverageTestData))]
        public void GenerateHydrographicCoverage_WorldsWithCoverage_ShouldReturnExpectedRange(WorldSize size, WorldSubType subType, double expectedMin, double expectedMax, int diceRoll)
        {
            //Arrange
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(It.IsAny<int>(), It.IsAny<int[]>())).Returns(diceRoll);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            double result = HydrographicCoverageTables.GenerateHydrographicCoverage(size, subType, diceRoller);

            // Assert
            Assert.InRange(result, expectedMin, expectedMax);
            mockDiceRoller.Verify(d => d.Roll(It.IsAny<int>(), It.IsAny<int[]>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CombinedWorldsWithoutHydrographicCoverageTestData))]
        public void GenerateHydrographicCoverage_WorldsWithNoCoverage_ShouldReturnZero(WorldSize size, WorldSubType subType, int diceRoll)
        {
            //Arrange
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(It.IsAny<int>(), It.IsAny<int[]>())).Returns(diceRoll);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            double result = HydrographicCoverageTables.GenerateHydrographicCoverage(size, subType, diceRoller);

            // Assert
            Assert.InRange(result, 0, 0);
            mockDiceRoller.Verify(d => d.Roll(It.IsAny<int>(), It.IsAny<int[]>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(WorldTypeTablesTests.GasGiantWithRollsTestData), MemberType = typeof(WorldTypeTablesTests))]
        public void GenerateHydrographicCoverage_GasGiant_ShouldThrowException(WorldSize size, WorldSubType subType, int diceRoll)
        {
            //Arrange
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(It.IsAny<int>(), It.IsAny<int[]>())).Returns(diceRoll);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act && Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => HydrographicCoverageTables.GenerateHydrographicCoverage(size, subType, diceRoller));
            mockDiceRoller.Verify(d => d.Roll(It.IsAny<int>(), It.IsAny<int[]>()), Times.Never);
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
