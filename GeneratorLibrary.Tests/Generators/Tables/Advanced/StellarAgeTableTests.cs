using GeneratorLibrary.Generators.Tables.Advanced;
using GeneratorLibrary.Models.Advanced;
using GeneratorLibrary.Utils;
using Moq;

namespace GeneratorLibrary.Tests.Generators.Tables.Advanced
{
    public class StellarAgeTableTests
    {
        [Fact]
        public void DetermineStellarAge_CategoryRoll3_ReturnsExtremePopulationI()
        {
            // Arrange
            var mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(3)).Returns(3);

            // Act
            var (populationType, age) = StellarAgeTable.DetermineStellarAge(mockDiceRoller.Object);

            // Assert
            Assert.Equal(StellarAgePopulationType.ExtremePopulationI, populationType);
            Assert.Equal(0.0, age);
            mockDiceRoller.Verify(d => d.Roll(3), Times.Once);
            mockDiceRoller.Verify(d => d.Roll(1, -1), Times.Never);
        }

        [Fact]
        public void DetermineStellarAge_MustHaveGardenWorld_UsesCorrectRoll()
        {
            // Arrange
            var mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(2, 2)).Returns(3);

            // Act
            var (populationType, age) = StellarAgeTable.DetermineStellarAge(mockDiceRoller.Object, true);

            // Assert
            Assert.Equal(StellarAgePopulationType.ExtremePopulationI, populationType);
            Assert.Equal(0.0, age);
            mockDiceRoller.Verify(d => d.Roll(2, 2), Times.Once);
        }

        [Theory]
        [InlineData(4, 0, 0, StellarAgePopulationType.YoungPopulationI, 0.1)]
        [InlineData(5, 1, 1, StellarAgePopulationType.YoungPopulationI, 0.45)]
        [InlineData(6, 0, 1, StellarAgePopulationType.YoungPopulationI, 0.15)]
        [InlineData(7, 0, 0, StellarAgePopulationType.IntermediatePopulationI, 2.0)]
        [InlineData(9, 1, 1, StellarAgePopulationType.IntermediatePopulationI, 2.7)]
        [InlineData(10, 0, 1, StellarAgePopulationType.IntermediatePopulationI, 2.1)]
        [InlineData(11, 0, 0, StellarAgePopulationType.OldPopulationI, 5.6)]
        [InlineData(13, 1, 1, StellarAgePopulationType.OldPopulationI, 6.3)]
        [InlineData(14, 0, 1, StellarAgePopulationType.OldPopulationI, 5.7)]
        [InlineData(15, 0, 0, StellarAgePopulationType.IntermediatePopulationII, 8.0)]
        [InlineData(16, 1, 1, StellarAgePopulationType.IntermediatePopulationII, 8.7)]
        [InlineData(17, 0, 1, StellarAgePopulationType.IntermediatePopulationII, 8.1)]
        [InlineData(18, 0, 0, StellarAgePopulationType.ExtremePopulationII, 10.0)]
        [InlineData(18, 1, 1, StellarAgePopulationType.ExtremePopulationII, 10.7)]
        [InlineData(18, 0, 1, StellarAgePopulationType.ExtremePopulationII, 10.1)]
        public void DetermineStellarAge_CalculatesCorrectAge(int categoryRoll, int stepARoll, int stepBRoll, StellarAgePopulationType expectedType, double expectedAge)
        {
            // Arrange
            var mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(3)).Returns(categoryRoll);
            mockDiceRoller.Setup(d => d.Roll(1, -1)).Returns(stepARoll)
                .Callback(() =>
                mockDiceRoller.Setup(d => d.Roll(1, -1)).Returns(stepBRoll));

            // Act
            var (populationType, age) = StellarAgeTable.DetermineStellarAge(mockDiceRoller.Object);

            // Assert
            Assert.Equal(expectedType, populationType);
            Assert.Equal(expectedAge, age);
            mockDiceRoller.Verify(d => d.Roll(3), Times.Once);
            mockDiceRoller.Verify(d => d.Roll(1, -1), Times.Exactly(2));
        }

        [Fact]
        public void DetermineStellarAge_InvalidCategoryRoll_ThrowsArgumentException()
        {
            // Arrange
            var mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(3)).Returns(19); // Valor inválido

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => StellarAgeTable.DetermineStellarAge(mockDiceRoller.Object));
            Assert.Contains("Dice Roller somehow rolled less than 3 or more than 18", exception.Message);
            mockDiceRoller.Verify(d => d.Roll(3), Times.Once);
            mockDiceRoller.Verify(d => d.Roll(1, -1), Times.Exactly(2));
        }
    }
}
