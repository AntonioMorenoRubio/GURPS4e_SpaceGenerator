using GeneratorLibrary.Generators.Tables;

namespace GeneratorLibrary.Tests.Generators.Tables
{
    public class EconomicsTablesTests
    {
        [Theory]
        [InlineData(12, 130_000)]
        [InlineData(11, 97_000)]
        [InlineData(10, 67_000)]
        [InlineData(9, 43_000)]
        [InlineData(8, 31_000)]
        [InlineData(7, 25_000)]
        [InlineData(6, 19_000)]
        [InlineData(5, 13_000)]
        [InlineData(4, 9_600)]
        [InlineData(3, 8_400)]
        [InlineData(2, 8_100)]
        [InlineData(1, 7_800)]
        [InlineData(0, 7_500)]
        public void GetBasePerCapitaIncome_ShouldReturnExpectedValue(int techLevel, decimal expectedIncome)
        {
            // Act
            decimal actual = EconomicsTables.GetBasePerCapitaIncome(techLevel);

            // Assert
            Assert.Equal(expectedIncome, actual);
        }
    }
}
