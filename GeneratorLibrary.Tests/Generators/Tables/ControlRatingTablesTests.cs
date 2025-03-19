using GeneratorLibrary.Generators.Tables;
using GeneratorLibrary.Models;

namespace GeneratorLibrary.Tests.Generators.Tables
{
    public class ControlRatingTablesTests
    {
        public static IEnumerable<object[]> GenerateControlRatingTestData()
        {
            for (int minCR = 0; minCR <= 6; minCR++) // minCR de 0 a 6
            {
                for (int maxCR = minCR; maxCR <= 6; maxCR++) // maxCR debe ser ≥ minCR
                {
                    for (int roll = 2; roll <= 12; roll++) // Tirada de 2 a 12 (2d)
                    {
                        yield return new object[] { minCR, maxCR, roll };
                    }
                }
            }
        }

        [Theory]
        [InlineData(SocietyType.Anarchy, 0, 0)]
        [InlineData(SocietyType.AthenianDemocracy, 2, 4)]
        [InlineData(SocietyType.RepresentativeDemocracy, 2, 4)]
        [InlineData(SocietyType.ClanTribal, 3, 5)]
        [InlineData(SocietyType.Caste, 3, 6)]
        [InlineData(SocietyType.Dictatorship, 3, 6)]
        [InlineData(SocietyType.Technocracy, 3, 6)]
        [InlineData(SocietyType.Theocracy, 3, 6)]
        [InlineData(SocietyType.CorporateState, 4, 6)]
        [InlineData(SocietyType.Feudal, 4, 6)]
        public void GetControlRatingRange_SocietyType_ShouldReturnExpectedValues(SocietyType type, int expectedMin, int expectedMax)
        {
            // Act
            (int minCR, int maxCR) = ControlRatingTables.GetControlRatingRange(type);

            // Assert
            Assert.Equal(expectedMin, minCR);
            Assert.Equal(expectedMax, maxCR);
        }

        [Theory]
        [InlineData(10)] //Minimum value ouside enum
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public void GetControlRatingRange_InvalidSocietyType_ShouldThrowException(int societyType)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => ControlRatingTables.GetControlRatingRange((SocietyType)societyType));
        }

        [Theory]
        [InlineData(SpecialSociety.Bureaucracy, 4, 6)]
        [InlineData(SpecialSociety.Colony, 3, 6)]
        [InlineData(SpecialSociety.Cybercracy, 3, 6)]
        [InlineData(SpecialSociety.Matriarchy, 0, 6)]
        [InlineData(SpecialSociety.Meritocracy, 3, 6)]
        [InlineData(SpecialSociety.MilitaryGovernment, 4, 6)]
        [InlineData(SpecialSociety.Oligarchy, 3, 6)]
        [InlineData(SpecialSociety.Patriarchy, 0, 6)]
        [InlineData(SpecialSociety.Sanctuary, 0, 4)]
        [InlineData(SpecialSociety.Socialist, 3, 6)]
        [InlineData(SpecialSociety.Subjugated, 4, 6)]
        [InlineData(SpecialSociety.Utopia, 0, 3)]
        public void GetControlRatingRange_SpecialSociety_ShouldReturnExpectedValues(SpecialSociety type, int expectedMin, int expectedMax)
        {
            // Act
            (int minCR, int maxCR) = ControlRatingTables.GetControlRatingRange(type);

            // Assert
            Assert.Equal(expectedMin, minCR);
            Assert.Equal(expectedMax, maxCR);
        }

        [Theory]
        [InlineData(12)] //Minimum value ouside enum
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public void GetControlRatingRange_InvalidSpecialSocietyType_ShouldThrowException(int specialSociety)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => ControlRatingTables.GetControlRatingRange((SpecialSociety)specialSociety));
        }

        [Theory]
        [MemberData(nameof(GenerateControlRatingTestData))]
        public void GenerateControlRatingInRange_ShouldReturnValidCR(int minCR, int maxCR, int roll)
        {
            // Act
            int result = ControlRatingTables.GenerateControlRatingInRange(minCR, maxCR, roll);

            // Assert
            Assert.InRange(result, minCR, maxCR);
        }
    }
}
