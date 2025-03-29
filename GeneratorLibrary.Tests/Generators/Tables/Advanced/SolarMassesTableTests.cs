using GeneratorLibrary.Generators.Tables.Advanced;
using GeneratorLibrary.Utils;
using Moq;

namespace GeneratorLibrary.Tests.Generators.Tables.Advanced
{
    public class SolarMassesTableTests
    {
        public static IEnumerable<object[]> GetMassTableTestData()
        {
            var testData = new List<object[]>();

            for (int i = 3; i <= 18; i++)
            {
                for (int j = 3; j <= 18; j++)
                {
                    double massValue = GetExpectedMass(i, j);
                    testData.Add(new object[] { i, j, massValue });
                }
            }

            return testData;
        }

        private static double GetExpectedMass(int i, int j)
        {
            if (i == 3)
            {
                if (j >= 3 && j <= 10)
                    return 2.00;
                if (j >= 11 && j <= 18)
                    return 1.90;
            }
            else if (i == 4)
            {
                if (j >= 3 && j <= 8)
                    return 1.80;
                if (j >= 9 && j <= 11)
                    return 1.70;
                if (j >= 12 && j <= 18)
                    return 1.60;
            }
            else if (i == 5)
            {
                if (j >= 3 && j <= 7)
                    return 1.50;
                if (j >= 8 && j <= 10)
                    return 1.45;
                if (j >= 11 && j <= 12)
                    return 1.40;
                if (j >= 13 && j <= 18)
                    return 1.35;
            }
            else if (i == 6)
            {
                if (j >= 3 && j <= 7)
                    return 1.30;
                if (j >= 8 && j <= 9)
                    return 1.25;
                if (j == 10)
                    return 1.20;
                if (j >= 11 && j <= 12)
                    return 1.15;
                if (j >= 13 && j <= 18)
                    return 1.10;
            }
            else if (i == 7)
            {
                if (j >= 3 && j <= 7)
                    return 1.05;
                if (j >= 8 && j <= 9)
                    return 1.00;
                if (j == 10)
                    return 0.95;
                if (j >= 11 && j <= 12)
                    return 0.90;
                if (j >= 13 && j <= 18)
                    return 0.85;
            }
            else if (i == 8)
            {
                if (j >= 3 && j <= 7)
                    return 0.80;
                if (j >= 8 && j <= 9)
                    return 0.75;
                if (j == 10)
                    return 0.70;
                if (j >= 11 && j <= 12)
                    return 0.65;
                if (j >= 13 && j <= 18)
                    return 0.60;
            }
            else if (i == 9)
            {
                if (j >= 3 && j <= 8)
                    return 0.55;
                if (j >= 9 && j <= 11)
                    return 0.50;
                if (j >= 12 && j <= 18)
                    return 0.45;
            }
            else if (i == 10)
            {
                if (j >= 3 && j <= 8)
                    return 0.40;
                if (j >= 9 && j <= 11)
                    return 0.35;
                if (j >= 12 && j <= 18)
                    return 0.30;
            }
            else if (i == 11)
                return 0.25;
            else if (i == 12)
                return 0.20;
            else if (i == 13)
                return 0.15;
            else
                return 0.10; // Para i >= 14

            throw new KeyNotFoundException($"No existe un valor en massTable para ({i}, {j})");
        }

        [Theory]
        [MemberData(nameof(GetMassTableTestData))]
        public void GetPrimaryStarMass_ShouldReturn_CorrectValue(int roll1, int roll2, double expectedMass)
        {
            double actualMass = SolarMassesTable.GetPrimaryStarMass(roll1, roll2);
            Assert.Equal(expectedMass, actualMass);
        }

        [Theory]
        [InlineData(2, 3)]  // Fuera del rango permitido (menor que 3)
        [InlineData(19, 5)] // Fuera del rango permitido (mayor que 18)
        [InlineData(5, 19)] // Fuera del rango permitido (mayor que 18)
        public void GetPrimaryStarMass_ShouldThrow_KeyNotFoundException(int roll1, int roll2)
        {
            Assert.Throws<KeyNotFoundException>(() => SolarMassesTable.GetPrimaryStarMass(roll1, roll2));
        }

        [Fact]
        public void GetCompanionStarMass_ReturnsSame_WhenStepRollIsZero()
        {
            //Arrange
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(It.IsAny<int>(), It.IsAny<int[]>())).Returns(0);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            //Act
            double primaryMass = SolarMassesTable.GetPrimaryStarMass(5, 5);
            double companionMass = SolarMassesTable.GetCompanionStarMass(primaryMass, diceRoller);

            //Assert
            Assert.Equal(primaryMass, companionMass);
            mockDiceRoller.Verify(d => d.Roll(It.IsAny<int>(), It.IsAny<int[]>()), Times.Once);
        }

        [Fact]
        public void GetCompanionStarMass_ReturnsMinimum_WhenPrimaryMassIsMinimum()
        {
            //Arrange
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(It.IsAny<int>(), It.IsAny<int[]>())).Returns(3);
            IDiceRoller diceRoller = mockDiceRoller.Object;
            double primaryMass = 0.10;

            //Act
            double companionMass = SolarMassesTable.GetCompanionStarMass(primaryMass, diceRoller);

            Assert.Equal(0.10, companionMass);
            mockDiceRoller.Verify(d => d.Roll(It.IsAny<int>(), It.IsAny<int[]>()), Times.Never);
        }

        [Fact]
        public void GetCompanionStarMass_Decreases_WhenStepRollIsPositive()
        {
            //Arrange
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(It.IsAny<int>(), It.IsAny<int[]>())).Returns(1);
            IDiceRoller diceRoller = mockDiceRoller.Object;
            double primaryMass = SolarMassesTable.GetPrimaryStarMass(6, 5);

            //Act
            double companionMass = SolarMassesTable.GetCompanionStarMass(primaryMass, diceRoller);

            //Assert
            Assert.True(companionMass < primaryMass);
            mockDiceRoller.Verify(d => d.Roll(It.IsAny<int>(), It.IsAny<int[]>()), Times.AtLeastOnce);
        }
    }
}
