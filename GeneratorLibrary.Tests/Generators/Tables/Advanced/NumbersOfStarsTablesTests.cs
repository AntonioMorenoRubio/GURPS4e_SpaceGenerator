using GeneratorLibrary.Generators.Tables.Advanced;

namespace GeneratorLibrary.Tests.Generators.Tables.Advanced
{
    public class NumbersOfStarsTablesTests
    {
        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void Roll_3_to_10_Returns_1Star(int roll)
        {
            // Act
            int stars = NumberOfStarsTable.GenerateNumberOfStars(roll);

            // Assert
            Assert.Equal(1, stars);
        }

        [Theory]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        [InlineData(14)]
        [InlineData(15)]
        public void Roll_11_to_15_Returns_2Stars(int roll)
        {
            // Act
            int stars = NumberOfStarsTable.GenerateNumberOfStars(roll);

            // Assert
            Assert.Equal(2, stars);
        }

        [Theory]
        [InlineData(16)]
        [InlineData(17)]
        [InlineData(18)]
        [InlineData(int.MaxValue)]
        public void Roll_16_or_more_Returns_3Stars(int roll)
        {
            // Act
            int stars = NumberOfStarsTable.GenerateNumberOfStars(roll);

            // Assert
            Assert.Equal(3, stars);
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
