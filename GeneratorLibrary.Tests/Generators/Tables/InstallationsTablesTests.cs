using GeneratorLibrary.Generators;
using GeneratorLibrary.Generators.Tables;
using GeneratorLibrary.Models;
using GeneratorLibrary.Utils;

namespace GeneratorLibrary.Tests.Generators.Tables
{
    public class InstallationsTablesTests
    {

        [Theory]
        // PR 0
        [InlineData(0, new int[] { 3, 6, 9, 7, 14 }, new SpaceportClass[] { SpaceportClass.II, SpaceportClass.I })]
        [InlineData(0, new int[] { 15, 16, 17, 18, 18 }, new SpaceportClass[] { SpaceportClass.Ø })]

        // PR 3
        [InlineData(3, new int[] { 3, 8, 11, 10, 14 }, new SpaceportClass[] { SpaceportClass.III, SpaceportClass.II, SpaceportClass.I })]
        [InlineData(3, new int[] { 15, 16, 17, 18, 18 }, new SpaceportClass[] { SpaceportClass.Ø })]

        // PR 6
        [InlineData(6, new int[] { 3, 9, 12, 13, 14 }, new SpaceportClass[] { SpaceportClass.V, SpaceportClass.IV, SpaceportClass.III, SpaceportClass.II, SpaceportClass.I })]
        [InlineData(6, new int[] { 15, 16, 17, 18, 18 }, new SpaceportClass[] { SpaceportClass.Ø })]

        // PR 9
        [InlineData(9, new int[] { 3, 12, 15, 16, 14 }, new SpaceportClass[] { SpaceportClass.V, SpaceportClass.IV, SpaceportClass.III, SpaceportClass.II, SpaceportClass.I })]
        [InlineData(9, new int[] { 15, 16, 17, 18, 18 }, new SpaceportClass[] { SpaceportClass.III })]
        [InlineData(9, new int[] { 15, 16, 18, 18, 18 }, new SpaceportClass[] { SpaceportClass.Ø })]

        // PR 12
        [InlineData(12, new int[] { 3, 14, 17, 17, 14 }, new SpaceportClass[] { SpaceportClass.V, SpaceportClass.IV, SpaceportClass.III, SpaceportClass.II, SpaceportClass.I })]
        [InlineData(12, new int[] { 15, 16, 17, 18, 18 }, new SpaceportClass[] { SpaceportClass.IV, SpaceportClass.III, SpaceportClass.II })]

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
        [InlineData(0, 6, 10, new SpaceportClass[] { SpaceportClass.Ø })] // PR 0: Pocas instalaciones
        [InlineData(3, 4, 8, new SpaceportClass[] { SpaceportClass.I })]  // PR 3: Colonial Office posible
        [InlineData(6, 3, 9, new SpaceportClass[] { SpaceportClass.II, SpaceportClass.III })] // PR 6: Varias instalaciones
        [InlineData(9, 2, 11, new SpaceportClass[] { SpaceportClass.IV, SpaceportClass.V })] // PR 9: Muchas instalaciones
        public void GenerateFacilities_ShouldReturnValidFacilities(int populationRating, int controlRating, int techLevel, SpaceportClass[] spaceports)
        {
            // Act
            var facilities = InstallationsTables.GenerateFacilities(populationRating, controlRating, techLevel, spaceports.ToList());

            // Assert
            Assert.All(facilities, facility =>
            {
                if (InstallationsTables.IsFacilityWithPR(facility.Type))
                {
                    // PR de la instalación no debe superar el Population Rating del mundo
                    if (populationRating == 0)
                        Assert.InRange(facility.PR ?? 0, 0, populationRating);
                    else
                        Assert.InRange(facility.PR ?? 0, 1, populationRating);
                }

                // Asegurar que la prisión no aparece si hay otras instalaciones prohibidas
                if (facility.Type == FacilityType.Prison)
                {
                    Assert.DoesNotContain(facilities, f => f.Type != FacilityType.NavalBase && f.Type != FacilityType.PatrolBase);
                }
            });
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
        public void AddEspionageFacilities_ShouldReturnAtLeastOneFacility(int populationRating)
        {
            // Act
            List<Facility> facilities = InstallationsTables.AddEspionageFacilities(populationRating).ToList();

            // Assert
            Assert.NotEmpty(facilities);
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
        public void AddEspionageFacilities_ShouldReturnValidFacilities(int populationRating)
        {
            // Act
            List<Facility> facilities = InstallationsTables.AddEspionageFacilities(populationRating).ToList();

            // Assert
            Assert.All(facilities, facility =>
            {
                Assert.Equal(FacilityType.EspionageFacility, facility.Type);
                Assert.InRange(facility.PR ?? 0, 1, populationRating);
                Assert.IsType<EspionageFacility>(facility);
                Assert.True(Enum.IsDefined(typeof(EspionageFacilityType), ((EspionageFacility)facility).SubType));
            });
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
        public void AddPrivateResearchCenters_ShouldReturnAtLeastOneFacility(int populationRating)
        {
            // Act
            List<Facility> facilities = InstallationsTables.AddPrivateResearchCenters(populationRating).ToList();

            // Assert
            Assert.NotEmpty(facilities);
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
        public void AddPrivateResearchCenters_ShouldReturnValidFacilities(int populationRating)
        {
            // Act
            List<Facility> facilities = InstallationsTables.AddPrivateResearchCenters(populationRating).ToList();

            // Assert
            Assert.All(facilities, facility =>
            {
                Assert.Equal(FacilityType.PrivateResearchCenter, facility.Type);
                Assert.InRange(facility.PR ?? 0, 1, populationRating);
            });
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
        public void AddRefugeeCamps_ShouldReturnAtLeastOneFacility(int populationRating)
        {
            // Act
            List<Facility> facilities = InstallationsTables.AddRefugeeCamps(populationRating).ToList();

            // Assert
            Assert.NotEmpty(facilities);
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
        public void AddRefugeeCamps_ShouldReturnValidFacilities(int populationRating)
        {
            // Act
            List<Facility> facilities = InstallationsTables.AddRefugeeCamps(populationRating).ToList();

            // Assert
            Assert.All(facilities, facility =>
            {
                Assert.Equal(FacilityType.RefugeeCamp, facility.Type);
                Assert.InRange(facility.PR ?? 0, 1, populationRating);
            });
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
        public void GetUniversityPR_WithRollOneOrTwo_ReturnsThree(int roll, int expected)
        {
            // Act
            int result = InstallationsTables.GetUniversityPR(roll);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(3, 4)]
        [InlineData(4, 4)]
        public void GetUniversityPR_WithRollThreeOrFour_ReturnsFour(int roll, int expected)
        {
            // Act
            int result = InstallationsTables.GetUniversityPR(roll);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, 5)]
        [InlineData(6, 5)]
        public void GetUniversityPR_WithRollFiveOrSix_ReturnsFive(int roll, int expected)
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
