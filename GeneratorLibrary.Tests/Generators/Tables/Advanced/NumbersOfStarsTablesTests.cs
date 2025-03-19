using GeneratorLibrary.Generators.Tables.Advanced;

namespace GeneratorLibrary.Tests.Generators.Tables.Advanced
{
    public class NumbersOfStarsTablesTests
    {
        [Theory]
        [InlineData(3, 1)]
        [InlineData(4, 1)]
        [InlineData(5, 1)]
        [InlineData(6, 1)]
        [InlineData(7, 1)]
        [InlineData(8, 1)]
        [InlineData(9, 1)]
        [InlineData(10, 1)]
        public void Roll_3_to_10_Returns_1Star(int roll, int expectedStars)
        {
            // Act
            int stars = NumberOfStarsTable.GenerateNumberOfStars(roll);

            // Assert
            Assert.Equal(expectedStars, stars);
        }

        [Theory]
        [InlineData(11, 2)]
        [InlineData(12, 2)]
        [InlineData(13, 2)]
        [InlineData(14, 2)]
        [InlineData(15, 2)]
        public void Roll_11_to_15_Returns_2Stars(int roll, int expectedStars)
        {
            // Act
            int stars = NumberOfStarsTable.GenerateNumberOfStars(roll);

            // Assert
            Assert.Equal(expectedStars, stars);
        }

        [Theory]
        [InlineData(16, 3)]
        [InlineData(17, 3)]
        [InlineData(18, 3)]
        [InlineData(int.MaxValue, 3)]
        public void Roll_16_or_more_Returns_3Stars(int roll, int expectedStars)
        {
            // Act
            int stars = NumberOfStarsTable.GenerateNumberOfStars(roll);

            // Assert
            Assert.Equal(expectedStars, stars);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(int.MinValue)]
        public void Roll_Less_than_3_ThrowsArgumentException(int invalidRoll)
        {
            // Act y Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => NumberOfStarsTable.GenerateNumberOfStars(invalidRoll));
        }
    }
}
