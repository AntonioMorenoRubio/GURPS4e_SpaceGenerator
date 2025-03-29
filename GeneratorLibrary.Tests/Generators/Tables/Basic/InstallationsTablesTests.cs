using GeneratorLibrary.Generators.Tables.Basic;
using GeneratorLibrary.Models.Basic;
using GeneratorLibrary.Utils;
using Moq;

namespace GeneratorLibrary.Tests.Generators.Tables.Basic
{
    public class InstallationsTablesTests
    {

        public static IEnumerable<object[]> SpaceportTestData()
        {
            // Valores límite y representativos para PR
            int[] prValues = [0, 5, 6, 10, 15];

            // Valores límite para cada roll
            int[] rollValues = [3, 8, 13, 18];

            foreach (int pr in prValues)
                foreach (int i in rollValues)
                    foreach (int j in rollValues)
                        foreach (int k in rollValues)
                            foreach (int l in rollValues)
                                foreach (int m in rollValues)
                                {
                                    int[] rolls = [i, j, k, l, m];
                                    List<SpaceportClass> spaceports = [];
                                    if (pr >= 6 && i <= pr + 2)
                                        spaceports.Add(SpaceportClass.V);
                                    if (pr >= 6 && j <= pr + 5)
                                        spaceports.Add(SpaceportClass.IV);
                                    if (k <= pr + 8)
                                        spaceports.Add(SpaceportClass.III);
                                    if (l <= pr + 7)
                                        spaceports.Add(SpaceportClass.II);
                                    if (m <= 14)
                                        spaceports.Add(SpaceportClass.I);
                                    if (spaceports.Count is 0)
                                        spaceports.Add(SpaceportClass.Ø);

                                    yield return new object[] { pr, rolls, spaceports.ToArray() };
                                }
        }

        [Theory]
        [MemberData(nameof(SpaceportTestData))]
        public void DetermineSpaceportClass_ShouldReturnExpectedClasses(int populationRating, int[] rolls, SpaceportClass[] expected)
        {
            // Act
            var result = InstallationsTables.DetermineSpaceportClasses(populationRating, rolls);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(-1, new int[] { 10, 10, 10, 10, 10 })] // PR negativo
        [InlineData(5, new int[] { 2, 10, 10, 10, 10 })]   // Roll fuera del rango (menor a 3)
        [InlineData(5, new int[] { 10, 10, 19, 10, 10 })]  // Roll fuera del rango (mayor a 18)
        [InlineData(5, new int[] { 10, 10, 10, 10 })]      // Faltan posiciones en el array
        [InlineData(5, new int[] { 10, 10, 10, 10, 10, 10 })] // Exceso de posiciones en el array
        public void DetermineSpaceportClass_InvalidInputs_ShouldThrowException(int populationRating, int[] rolls)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => InstallationsTables.DetermineSpaceportClasses(populationRating, rolls));
        }

        [Theory]
        [InlineData(FacilityType.AlienEnclave, false)]
        [InlineData(FacilityType.BlackMarket, false)]
        [InlineData(FacilityType.ColonialOffice, false)]
        [InlineData(FacilityType.CorporateHeadquarters, true)]
        [InlineData(FacilityType.CriminalBase, true)]
        [InlineData(FacilityType.EspionageFacility, true)]
        [InlineData(FacilityType.GovernmentResearchStation, true)]
        [InlineData(FacilityType.MercenaryBase, true)]
        [InlineData(FacilityType.NaturePreserve, false)]
        [InlineData(FacilityType.NavalBase, true)]
        [InlineData(FacilityType.PatrolBase, true)]
        [InlineData(FacilityType.PirateBase, true)]
        [InlineData(FacilityType.Prison, true)]
        [InlineData(FacilityType.PrivateResearchCenter, true)]
        [InlineData(FacilityType.RebelOrTerroristBase, true)]
        [InlineData(FacilityType.RefugeeCamp, true)]
        [InlineData(FacilityType.ReligiousCenter, true)]
        [InlineData(FacilityType.SpecialJusticeGroupOffice, true)]
        [InlineData(FacilityType.SurveyBase, true)]
        [InlineData(FacilityType.University, true)]
        public void IsFacilityWithPR_ReturnsCorrectValue(FacilityType facilityType, bool expected)
        {
            //Act
            bool actual = InstallationsTables.IsFacilityWithPR(facilityType);

            //Assert
            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void CheckAndAddAlienEnclave_Rolls6OrLess_ShouldAddAlienEnclave(int roll)
        {
            //Arrange
            List<Facility> facilities = new List<Facility>();
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(It.IsAny<int>(), It.IsAny<int[]>())).Returns(roll);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            InstallationsTables.CheckAndAddAlienEnclave(facilities, diceRoller);

            // Assert
            Assert.Equal(FacilityType.AlienEnclave, facilities[0].Type);
            mockDiceRoller.Verify(d => d.Roll(It.IsAny<int>(), It.IsAny<int[]>()), Times.Once());
        }

        [Theory]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        [InlineData(14)]
        [InlineData(15)]
        [InlineData(16)]
        [InlineData(17)]
        [InlineData(18)]
        public void CheckAndAddAlienEnclave_Rolls7OrMore_ShouldNotAddAlienEnclave(int roll)
        {
            //Arrange
            List<Facility> facilities = new List<Facility>();
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(It.IsAny<int>(), It.IsAny<int[]>())).Returns(roll);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            InstallationsTables.CheckAndAddAlienEnclave(facilities, diceRoller);

            // Assert
            Assert.Empty(facilities);
            mockDiceRoller.Verify(d => d.Roll(It.IsAny<int>(), It.IsAny<int[]>()), Times.Once());
        }

        [Theory]
        [InlineData(2, 7)]
        [InlineData(1, 8)]
        [InlineData(0, 9)]
        public void CheckAndAddBlackMarket_RollsLessThanOrEqualTo9MinusControlRating_ShouldAddBlackMarket(int controlRating, int roll)
        {
            // Arrange
            List<Facility> facilities = new List<Facility>();
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(It.IsAny<int>(), It.IsAny<int>())).Returns(roll);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            InstallationsTables.CheckAndAddBlackMarket(facilities, controlRating, diceRoller);

            // Assert
            Assert.Equal(FacilityType.BlackMarket, facilities[0].Type);
            mockDiceRoller.Verify(d => d.Roll(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        }

        [Theory]
        [InlineData(2, 8)]
        [InlineData(1, 9)]
        [InlineData(0, 10)]
        public void CheckAndAddBlackMarket_RollsMoreThan9MinusControlRating_ShouldNotAddBlackMarket(int controlRating, int roll)
        {
            // Arrange
            List<Facility> facilities = new List<Facility>();
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(It.IsAny<int>(), It.IsAny<int>())).Returns(roll);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            InstallationsTables.CheckAndAddBlackMarket(facilities, controlRating, diceRoller);

            // Assert
            Assert.Empty(facilities);
            mockDiceRoller.Verify(d => d.Roll(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        }

        [Theory]
        [InlineData(3, 7)]
        [InlineData(4, 8)]
        [InlineData(5, 9)]
        public void CheckAndAddColonialOffice_PopulationAtLeast3AndRollsLessThanOrEqualToPopulationPlus4_ShouldAddColonialOffice(int populationRating, int roll)
        {
            // Arrange
            List<Facility> facilities = new List<Facility>();
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(It.IsAny<int>(), It.IsAny<int>())).Returns(roll);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            InstallationsTables.CheckAndAddColonialOffice(facilities, populationRating, diceRoller);

            // Assert
            Assert.Equal(FacilityType.ColonialOffice, facilities[0].Type);
            mockDiceRoller.Verify(d => d.Roll(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        }

        [Theory]
        [InlineData(2, 7)] // Population too low
        [InlineData(3, 8)] // Population ok but roll too high
        [InlineData(4, 9)] // Population ok but roll too high
        public void CheckAndAddColonialOffice_PopulationTooLowOrRollTooHigh_ShouldNotAddColonialOffice(int populationRating, int roll)
        {
            // Arrange
            List<Facility> facilities = new List<Facility>();
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(It.IsAny<int>(), It.IsAny<int>())).Returns(roll);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            InstallationsTables.CheckAndAddColonialOffice(facilities, populationRating, diceRoller);

            // Assert
            Assert.Empty(facilities);
            mockDiceRoller.Verify(d => d.Roll(It.IsAny<int>(), It.IsAny<int>()), Times.AtMostOnce()); // Could be not called at all if population is too low
        }

        [Theory]
        [InlineData(6, 7, 9, 1)]
        [InlineData(7, 8, 10, 2)]
        [InlineData(8, 9, 11, 3)]
        public void CheckAndAddCorporateHeadquarters_MetRequirementsAndRollsLessThanOrEqualToPopulationPlus3_ShouldAddCorporateHQ(
    int populationRating, int techLevel, int roll, int prRoll)
        {
            // Arrange
            List<Facility> facilities = new List<Facility>();
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(3, 0)).Returns(roll);
            mockDiceRoller.Setup(d => d.Roll(1, -3)).Returns(prRoll);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            InstallationsTables.CheckAndAddCorporateHeadquarters(facilities, populationRating, techLevel, diceRoller);

            // Assert
            Assert.Equal(FacilityType.CorporateHeadquarters, facilities[0].Type);
            // Verificar PR según la lógica de GetFacilityPR (esta implementación puede variar)
            mockDiceRoller.Verify(d => d.Roll(3, 0), Times.Once());
            mockDiceRoller.Verify(d => d.Roll(1, -3), Times.Once());
        }

        [Theory]
        [InlineData(5, 7, 9)] // Population too low
        [InlineData(6, 6, 9)] // Tech level too low
        [InlineData(6, 7, 10)] // Requirements met but roll too high
        public void CheckAndAddCorporateHeadquarters_RequirementsNotMetOrRollTooHigh_ShouldNotAddCorporateHQ(
            int populationRating, int techLevel, int roll)
        {
            // Arrange
            List<Facility> facilities = new List<Facility>();
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(3, 0)).Returns(roll);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            InstallationsTables.CheckAndAddCorporateHeadquarters(facilities, populationRating, techLevel, diceRoller);

            // Assert
            Assert.Empty(facilities);
            mockDiceRoller.Verify(d => d.Roll(3, 0), Times.AtMostOnce);
        }

        [Theory]
        [InlineData(4, 7, 2)]
        [InlineData(5, 8, 3)]
        [InlineData(6, 9, 1)]
        public void CheckAndAddCriminalBase_RollsLessThanOrEqualToPopulationPlus3_ShouldAddCriminalBase(
    int populationRating, int roll, int prRoll)
        {
            // Arranget
            List<Facility> facilities = new List<Facility>();
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(3, 0)).Returns(roll);
            mockDiceRoller.Setup(d => d.Roll(1, -3)).Returns(prRoll);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            InstallationsTables.CheckAndAddCriminalBase(facilities, populationRating, diceRoller);

            // Assert
            Assert.Equal(FacilityType.CriminalBase, facilities[0].Type);
            mockDiceRoller.Verify(d => d.Roll(3, 0), Times.Once());
            mockDiceRoller.Verify(d => d.Roll(1, -3), Times.Once());
        }

        [Theory]
        [InlineData(4, 8)]
        [InlineData(5, 9)]
        [InlineData(6, 10)]
        public void CheckAndAddCriminalBase_RollsGreaterThanPopulationPlus3_ShouldNotAddCriminalBase(int populationRating, int roll)
        {
            // Arrange
            List<Facility> facilities = new List<Facility>();
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(3, 0)).Returns(roll);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            InstallationsTables.CheckAndAddCriminalBase(facilities, populationRating, diceRoller);

            // Assert
            Assert.Empty(facilities);
            mockDiceRoller.Verify(d => d.Roll(3, 0), Times.Once());
            mockDiceRoller.Verify(d => d.Roll(1, -3), Times.Never());
        }

        [Theory]
        [InlineData(2, 10)]
        [InlineData(3, 9)]
        [InlineData(4, 8)]
        public void CheckAndAddNaturePreserve_RollsLessThanOrEqualTo12MinusPopulationRating_ShouldAddNaturePreserve(int populationRating, int roll)
        {
            // Arrange
            List<Facility> facilities = new List<Facility>();
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(It.IsAny<int>(), It.IsAny<int>())).Returns(roll);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            InstallationsTables.CheckAndAddNaturePreserve(facilities, populationRating, diceRoller);

            // Assert
            Assert.Equal(FacilityType.NaturePreserve, facilities[0].Type);
            mockDiceRoller.Verify(d => d.Roll(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        }

        [Theory]
        [InlineData(2, 11)]
        [InlineData(3, 10)]
        [InlineData(4, 9)]
        public void CheckAndAddNaturePreserve_RollsGreaterThan12MinusPopulationRating_ShouldNotAddNaturePreserve(int populationRating, int roll)
        {
            // Arrange
            List<Facility> facilities = new List<Facility>();
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(It.IsAny<int>(), It.IsAny<int>())).Returns(roll);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            InstallationsTables.CheckAndAddNaturePreserve(facilities, populationRating, diceRoller);

            // Assert
            Assert.Empty(facilities);
            mockDiceRoller.Verify(d => d.Roll(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        }

        [Theory]
        [InlineData(2, 6, 1)]
        [InlineData(1, 7, 2)]
        [InlineData(0, 8, 3)]
        public void CheckAndAddPirateBase_RollsLessThanOrEqualTo8MinusControlRating_ShouldAddPirateBase(
    int controlRating, int roll, int prRoll)
        {
            // Arrange
            List<Facility> facilities = new List<Facility>();
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(3, 0)).Returns(roll);
            mockDiceRoller.Setup(d => d.Roll(1, -3)).Returns(prRoll);
            int populationRating = 5; // valor arbitrario para el test
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            InstallationsTables.CheckAndAddPirateBase(facilities, populationRating, controlRating, diceRoller);

            // Assert
            Assert.Equal(FacilityType.PirateBase, facilities[0].Type);
            mockDiceRoller.Verify(d => d.Roll(3, 0), Times.Once());
            mockDiceRoller.Verify(d => d.Roll(1, -3), Times.Once());
        }

        [Theory]
        [InlineData(2, 7)]
        [InlineData(1, 8)]
        [InlineData(0, 9)]
        public void CheckAndAddPirateBase_RollsGreaterThan8MinusControlRating_ShouldNotAddPirateBase(int controlRating, int roll)
        {
            // Arrange
            List<Facility> facilities = new List<Facility>();
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(3, 0)).Returns(roll);
            int populationRating = 5; // valor arbitrario para el test
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            InstallationsTables.CheckAndAddPirateBase(facilities, populationRating, controlRating, diceRoller);

            // Assert
            Assert.Empty(facilities);
            mockDiceRoller.Verify(d => d.Roll(3, 0), Times.Once());
            mockDiceRoller.Verify(d => d.Roll(1, -3), Times.Never());
        }

        [Theory]
        [InlineData(1, 9, 2, true)]
        [InlineData(2, 8, 1, true)]
        public void CheckAndAddPrison_EmptyFacilitiesAndRollsLessThanOrEqualTo10MinusPopulation_ShouldAddPrison(
    int populationRating, int roll, int prRoll, bool emptyFacilities)
        {
            // Arrange
            List<Facility> facilities = new List<Facility>();
            if (!emptyFacilities)
            {
                facilities.Add(new Facility { Type = FacilityType.NavalBase });
            }

            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(3, 0)).Returns(roll);
            mockDiceRoller.Setup(d => d.Roll(1, -3)).Returns(prRoll);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            InstallationsTables.CheckAndAddPrison(facilities, populationRating, diceRoller);

            // Assert
            Assert.Contains(facilities, f => f.Type == FacilityType.Prison);
            mockDiceRoller.Verify(d => d.Roll(3, 0), Times.Once());
            mockDiceRoller.Verify(d => d.Roll(1, -3), Times.Once());
        }

        [Theory]
        [InlineData(1, 10)] // Roll too high
        public void CheckAndAddPrison_RollTooHigh_ShouldNotAddPrison(int populationRating, int roll)
        {
            // Arrange
            List<Facility> facilities = new List<Facility>();
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(3, 0)).Returns(roll);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            InstallationsTables.CheckAndAddPrison(facilities, populationRating, diceRoller);

            // Assert
            Assert.Empty(facilities);
            mockDiceRoller.Verify(d => d.Roll(3, 0), Times.Once());
        }

        [Fact]
        public void CheckAndAddPrison_TooManyFacilities_ShouldNotAddPrison()
        {
            // Arrange
            List<Facility> facilities = new List<Facility>
    {
        new Facility { Type = FacilityType.BlackMarket },
        new Facility { Type = FacilityType.ColonialOffice },
        new Facility { Type = FacilityType.CorporateHeadquarters }
    };

            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            InstallationsTables.CheckAndAddPrison(facilities, 1, diceRoller);

            // Assert
            Assert.Equal(3, facilities.Count);
            Assert.DoesNotContain(facilities, f => f.Type == FacilityType.Prison);
            mockDiceRoller.Verify(d => d.Roll(3, 0), Times.Never());
        }

        [Theory]
        [InlineData(4, 7, 2)]
        public void CheckAndAddNavalBase_RollsLessThanOrEqualToPopulationPlus3_ShouldAddNavalBase(
    int populationRating, int roll, int prRoll)
        {
            // Arrange
            List<Facility> facilities = new List<Facility>();
            List<SpaceportClass> spaceports = new List<SpaceportClass> { SpaceportClass.III };
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(3, 0)).Returns(roll);
            mockDiceRoller.Setup(d => d.Roll(1, -1)).Returns(prRoll);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            InstallationsTables.CheckAndAddNavalBase(facilities, populationRating, spaceports, diceRoller);

            // Assert
            Assert.Equal(FacilityType.NavalBase, facilities[0].Type);
            mockDiceRoller.Verify(d => d.Roll(3, 0), Times.Once());
            mockDiceRoller.Verify(d => d.Roll(1, -1), Times.Once());
        }

        [Fact]
        public void CheckAndAddNavalBase_HasSpaceportV_ShouldAddNavalBase()
        {
            // Arrange
            List<Facility> facilities = new List<Facility>();
            List<SpaceportClass> spaceports = new List<SpaceportClass> { SpaceportClass.V };
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(1, -1)).Returns(2);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            InstallationsTables.CheckAndAddNavalBase(facilities, 1, spaceports, diceRoller);

            // Assert
            Assert.Equal(FacilityType.NavalBase, facilities[0].Type);
            mockDiceRoller.Verify(d => d.Roll(3, 0), Times.Never());
            mockDiceRoller.Verify(d => d.Roll(1, -1), Times.Once());
        }

        [Theory]
        [InlineData(4, 8)]
        public void CheckAndAddNavalBase_NoSpaceportVAndRollTooHigh_ShouldNotAddNavalBase(int populationRating, int roll)
        {
            // Arrange
            List<Facility> facilities = new List<Facility>();
            List<SpaceportClass> spaceports = new List<SpaceportClass> { SpaceportClass.III };
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(3, 0)).Returns(roll);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            InstallationsTables.CheckAndAddNavalBase(facilities, populationRating, spaceports, diceRoller);

            // Assert
            Assert.Empty(facilities);
            mockDiceRoller.Verify(d => d.Roll(3, 0), Times.Once());
            mockDiceRoller.Verify(d => d.Roll(1, -1), Times.Never());
        }

        [Theory]
        [InlineData(9, 3, 4)]
        [InlineData(10, 4, 5)]
        public void CheckAndAddUniversity_RollsLessThanOrEqualToPopulationMinus6_ShouldAddUniversity(
    int populationRating, int roll, int prRoll)
        {
            // Arrange
            List<Facility> facilities = new List<Facility>();
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(3, 0)).Returns(roll);
            mockDiceRoller.Setup(d => d.Roll(1, 0)).Returns(prRoll);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            InstallationsTables.CheckAndAddUniversity(facilities, populationRating, diceRoller);

            // Assert
            Assert.Equal(FacilityType.University, facilities[0].Type);
            mockDiceRoller.Verify(d => d.Roll(3, 0), Times.Once());
            mockDiceRoller.Verify(d => d.Roll(1, 0), Times.Once());
        }

        [Theory]
        [InlineData(9, 4)]
        [InlineData(10, 5)]
        [InlineData(7, 2)]  // Population no es suficiente (7-6=1 < roll=2)
        public void CheckAndAddUniversity_RollsGreaterThanPopulationMinus6_ShouldNotAddUniversity(int populationRating, int roll)
        {
            // Arrange
            List<Facility> facilities = new List<Facility>();
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(3, 0)).Returns(roll);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            InstallationsTables.CheckAndAddUniversity(facilities, populationRating, diceRoller);

            // Assert
            Assert.Empty(facilities);
            mockDiceRoller.Verify(d => d.Roll(3, 0), Times.Once());
            mockDiceRoller.Verify(d => d.Roll(1, 0), Times.Never());
        }

        [Fact]
        public void ApplyPRRules_WithMultipleHighPRFacilities_ShouldKeepOnlyOne()
        {
            // Arrange
            var facilities = new List<Facility>
            {
                new Facility { PR = 10 },
                new Facility { PR = 10 },
                new Facility { PR = 10 }
            };

            // Usamos un Random con semilla fija para pruebas deterministas
            var mockRandom = new Random(42);
            int populationRating = 10;

            // Act
            InstallationsTables.ApplyPRRules(facilities, populationRating, mockRandom);

            // Assert
            Assert.Equal(1, facilities.Count(f => f.PR == 10));
            Assert.Equal(2, facilities.Count(f => f.PR == 9));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void AddEspionageFacilities_WhenPopulationRatingPlusSixLessThan18_ShouldUseCorrectLoopCondition(int populationRating)
        {
            // Arrange
            var mockDiceRoller = new Mock<IDiceRoller>();
            // Setup for facility creation
            mockDiceRoller.Setup(d => d.Roll(1, 0)).Returns(3); // Civilian type
            mockDiceRoller.Setup(d => d.Roll(1, -4)).Returns(2); // PR roll for civilian

            // Setup for loops condition check (should exit the loop after adding the second facility)
            mockDiceRoller.SetupSequence(d => d.Roll(3, 0))
                .Returns(populationRating + 5)
                .Returns(populationRating + 7);

            // Act
            var facilities = InstallationsTables.AddEspionageFacilities(populationRating, mockDiceRoller.Object).ToList();

            // Assert
            Assert.Equal(2, facilities.Count);
            mockDiceRoller.Verify(d => d.Roll(3, 0), Times.Exactly(2)); // Continuation + exit
        }

        [Theory]
        [InlineData(12)]
        [InlineData(15)]
        public void AddEspionageFacilities_WhenPopulationRatingPlusSixGreaterOrEqual18_ShouldUseAlternativeLoopCondition(int populationRating)
        {
            // Arrange
            var mockDiceRoller = new Mock<IDiceRoller>();
            // Setup for facility creation
            mockDiceRoller.Setup(d => d.Roll(1, 0)).Returns(6); // Enemy Military type
            mockDiceRoller.Setup(d => d.Roll(1, -2)).Returns(3); // PR roll for military

            // Setup for loop conditions
            mockDiceRoller.SetupSequence(d => d.Roll(3, 0))
                .Returns(16) // Continue loop
                .Returns(18); // Exit loop

            // Act
            var facilities = InstallationsTables.AddEspionageFacilities(populationRating, mockDiceRoller.Object).ToList();

            // Assert
            Assert.Equal(2, facilities.Count);
            mockDiceRoller.Verify(d => d.Roll(3, 0), Times.Exactly(2));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        [InlineData(14)]
        [InlineData(15)]
        public void AddEspionageFacilities_ShouldReturnAtLeastOneFacility(int populationRating)
        {
            //Arrange
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.SetupSequence(d => d.Roll(3, 0))
                .Returns(1)
                .Returns(2)
                .Returns(3)
                .Returns(4)
                .Returns(5)
                .Returns(6)
                .Returns(7)
                .Returns(8)
                .Returns(9)
                .Returns(10)
                .Returns(11)
                .Returns(12)
                .Returns(13)
                .Returns(14)
                .Returns(15)
                .Returns(16)
                .Returns(17)
                .Returns(18);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            List<Facility> facilities = InstallationsTables.AddEspionageFacilities(populationRating, diceRoller).ToList();

            // Assert
            Assert.NotEmpty(facilities);
            mockDiceRoller.Verify(d => d.Roll(It.IsAny<int>(), It.IsAny<int[]>()), Times.AtLeastOnce);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        [InlineData(14)]
        [InlineData(15)]
        public void AddEspionageFacilities_ShouldReturnValidFacilities(int populationRating)
        {
            //Arrange
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.SetupSequence(d => d.Roll(3, 0))
                .Returns(1)
                .Returns(2)
                .Returns(3)
                .Returns(4)
                .Returns(5)
                .Returns(6)
                .Returns(7)
                .Returns(8)
                .Returns(9)
                .Returns(10)
                .Returns(11)
                .Returns(12)
                .Returns(13)
                .Returns(14)
                .Returns(15)
                .Returns(16)
                .Returns(17)
                .Returns(18);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            List<Facility> facilities = InstallationsTables.AddEspionageFacilities(populationRating, diceRoller).ToList();

            // Assert
            Assert.All(facilities, facility =>
            {
                Assert.Equal(FacilityType.EspionageFacility, facility.Type);
                Assert.InRange(facility.PR ?? 0, 1, populationRating);
                Assert.IsType<EspionageFacility>(facility);
                Assert.True(Enum.IsDefined(typeof(EspionageFacilityType), ((EspionageFacility)facility).SubType));
            });
            mockDiceRoller.Verify(d => d.Roll(It.IsAny<int>(), It.IsAny<int[]>()), Times.AtLeastOnce);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        [InlineData(14)]
        [InlineData(15)]
        public void AddPrivateResearchCenters_ShouldReturnAtLeastOneFacility(int populationRating)
        {
            //Arrange
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.SetupSequence(d => d.Roll(3, 0))
                .Returns(1)
                .Returns(2)
                .Returns(3)
                .Returns(4)
                .Returns(5)
                .Returns(6)
                .Returns(7)
                .Returns(8)
                .Returns(9)
                .Returns(10)
                .Returns(11)
                .Returns(12)
                .Returns(13)
                .Returns(14)
                .Returns(15)
                .Returns(16)
                .Returns(17)
                .Returns(18);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            List<Facility> facilities = InstallationsTables.AddPrivateResearchCenters(populationRating, diceRoller).ToList();

            // Assert
            Assert.NotEmpty(facilities);
            mockDiceRoller.Verify(d => d.Roll(It.IsAny<int>(), It.IsAny<int[]>()), Times.AtLeastOnce);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        [InlineData(14)]
        [InlineData(15)]
        public void AddPrivateResearchCenters_ShouldReturnValidFacilities(int populationRating)
        {
            //Arrange
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.SetupSequence(d => d.Roll(3, 0))
                .Returns(1)
                .Returns(2)
                .Returns(3)
                .Returns(4)
                .Returns(5)
                .Returns(6)
                .Returns(7)
                .Returns(8)
                .Returns(9)
                .Returns(10)
                .Returns(11)
                .Returns(12)
                .Returns(13)
                .Returns(14)
                .Returns(15)
                .Returns(16)
                .Returns(17)
                .Returns(18);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            List<Facility> facilities = InstallationsTables.AddPrivateResearchCenters(populationRating, diceRoller).ToList();

            // Assert
            Assert.All(facilities, facility =>
            {
                Assert.Equal(FacilityType.PrivateResearchCenter, facility.Type);
                Assert.InRange(facility.PR ?? 0, 1, populationRating);
            });
            mockDiceRoller.Verify(d => d.Roll(It.IsAny<int>(), It.IsAny<int[]>()), Times.AtLeastOnce);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        [InlineData(14)]
        [InlineData(15)]
        public void AddRefugeeCamps_ShouldReturnAtLeastOneFacility(int populationRating)
        {
            //Arrange
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.SetupSequence(d => d.Roll(3, 0))
                .Returns(1)
                .Returns(2)
                .Returns(3)
                .Returns(4)
                .Returns(5)
                .Returns(6)
                .Returns(7)
                .Returns(8)
                .Returns(9)
                .Returns(10)
                .Returns(11)
                .Returns(12)
                .Returns(13)
                .Returns(14)
                .Returns(15)
                .Returns(16)
                .Returns(17)
                .Returns(18);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            List<Facility> facilities = InstallationsTables.AddRefugeeCamps(populationRating, diceRoller).ToList();

            // Assert
            Assert.NotEmpty(facilities);
            mockDiceRoller.Verify(d => d.Roll(It.IsAny<int>(), It.IsAny<int[]>()), Times.AtLeastOnce);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        [InlineData(14)]
        [InlineData(15)]
        public void AddRefugeeCamps_ShouldReturnValidFacilities(int populationRating)
        {
            //Arrange
            Mock<IDiceRoller> mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.SetupSequence(d => d.Roll(3, 0))
                .Returns(1)
                .Returns(2)
                .Returns(3)
                .Returns(4)
                .Returns(5)
                .Returns(6)
                .Returns(7)
                .Returns(8)
                .Returns(9)
                .Returns(10)
                .Returns(11)
                .Returns(12)
                .Returns(13)
                .Returns(14)
                .Returns(15)
                .Returns(16)
                .Returns(17)
                .Returns(18);
            IDiceRoller diceRoller = mockDiceRoller.Object;

            // Act
            List<Facility> facilities = InstallationsTables.AddRefugeeCamps(populationRating, diceRoller).ToList();

            // Assert
            Assert.All(facilities, facility =>
            {
                Assert.Equal(FacilityType.RefugeeCamp, facility.Type);
                Assert.InRange(facility.PR ?? 0, 1, populationRating);
            });
            mockDiceRoller.Verify(d => d.Roll(It.IsAny<int>(), It.IsAny<int[]>()), Times.AtLeastOnce);
        }

        [Theory]
        [InlineData(5, 4, 4)]
        [InlineData(5, 5, 5)]
        [InlineData(5, 8, 5)]
        [InlineData(5, 10, 5)]
        [InlineData(3, 7, 3)]
        [InlineData(2, 6, 2)]
        [InlineData(1, 6, 1)]
        [InlineData(10, 3, 3)]
        [InlineData(10, 2, 2)]
        public void GetFacilityPR_ReturnsClampedValue(int worldPR, int roll, int expected)
        {
            // Act
            int result = InstallationsTables.GetFacilityPR(worldPR, roll);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetFacilityPR_WithWorldPRZero_ReturnsMinimumZero()
        {
            // Arrange
            int worldPR = 0;
            int roll = 5;

            // Act
            int result = InstallationsTables.GetFacilityPR(worldPR, roll);

            // Assert
            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData(1, 3)]
        [InlineData(2, 3)]
        [InlineData(3, 4)]
        [InlineData(4, 4)]
        [InlineData(5, 5)]
        [InlineData(6, 5)]
        public void GetUniversityPR_ReturnsCorrectValue(int roll, int expected)
        {
            // Act
            int result = InstallationsTables.GetUniversityPR(roll);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetUniversityPR_WithInvalidRoll_HandlesDefault()
        {
            // This test verifies that values outside the expected range are handled
            // by the default case in the switch statement

            // Act
            int result = InstallationsTables.GetUniversityPR(7);

            // Assert
            Assert.Equal(5, result);
        }
    }

}
