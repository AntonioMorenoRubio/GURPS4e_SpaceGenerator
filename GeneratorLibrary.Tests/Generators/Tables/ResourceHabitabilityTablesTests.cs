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

        [Theory]
        [InlineData(-100, -3)]
        [InlineData(0, -3)]
        [InlineData(2, -3)]
        [InlineData(3, -2)]
        [InlineData(4, -2)]
        [InlineData(5, -1)]
        [InlineData(6, -1)]
        [InlineData(7, -1)]
        [InlineData(8, -0)]
        [InlineData(9, -0)]
        [InlineData(10, 0)]
        [InlineData(11, 0)]
        [InlineData(12, 0)]
        [InlineData(13, 0)]
        [InlineData(14, 1)]
        [InlineData(15, 1)]
        [InlineData(16, 1)]
        [InlineData(17, 2)]
        [InlineData(18, 2)]
        [InlineData(19, 3)]
        [InlineData(20, 3)]
        [InlineData(100, 3)]
        public void ResourceValueForOtherWorlds_ReturnsCorrectModifier(int roll, int expected)
        {
            //Act
            int actual = ResourceHabitabilityTables.ResourceValueForOtherWorlds(roll);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-5, ResourceOverallValue.Worthless)]
        [InlineData(-4, ResourceOverallValue.VeryScant)]
        [InlineData(-3, ResourceOverallValue.Scant)]
        [InlineData(-2, ResourceOverallValue.VeryPoor)]
        [InlineData(-1, ResourceOverallValue.Poor)]
        [InlineData(0, ResourceOverallValue.Average)]
        [InlineData(1, ResourceOverallValue.Abundant)]
        [InlineData(2, ResourceOverallValue.VeryAbundant)]
        [InlineData(3, ResourceOverallValue.Rich)]
        [InlineData(4, ResourceOverallValue.VeryRich)]
        [InlineData(5, ResourceOverallValue.Motherlode)]
        public void GetResourceOverallValue_ReturnsCorrectValue(int resourceValueModifier, ResourceOverallValue expected)
        {
            //Act
            ResourceOverallValue actual = ResourceHabitabilityTables.GetResourceOverallValue(resourceValueModifier);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-100)]
        [InlineData(-10)]
        [InlineData(-6)]
        [InlineData(6)]
        [InlineData(10)]
        [InlineData(100)]
        public void GetResourceOverallValue_InvalidModifier_ShouldThrowArgumentOutOfRangeException(int resourceValueModifier)
        {
            //Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => ResourceHabitabilityTables.GetResourceOverallValue(resourceValueModifier));
        }
    }
}
