using GeneratorLibrary.Generators.Tables;

namespace GeneratorLibrary.Tests.Generators.Tables
{
    public class ResourceHabitabilityTablesTests
    {
        [Theory]
        [InlineData(3, -5)]
        [InlineData(4, -4)]
        [InlineData(5, -3)]
        [InlineData(6, -2)]
        [InlineData(7, -2)]
        [InlineData(8, -1)]
        [InlineData(9, -1)]
        [InlineData(10, 0)]
        [InlineData(11, 0)]
        [InlineData(12, 1)]
        [InlineData(13, 1)]
        [InlineData(14, 2)]
        [InlineData(15, 2)]
        [InlineData(16, 3)]
        [InlineData(17, 4)]
        [InlineData(18, 5)]
        public void ResourceValueForAsteroidBelts_ReturnsCorrectModifier(int roll, int expected)
        {
            //Act
            int actual = ResourceHabitabilityTables.ResourceValueForAsteroidBelts(roll);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
