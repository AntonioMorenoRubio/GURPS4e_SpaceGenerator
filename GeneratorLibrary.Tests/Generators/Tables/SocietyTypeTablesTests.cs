using GeneratorLibrary.Generators.Tables;
using GeneratorLibrary.Models;

namespace GeneratorLibrary.Tests.Generators.Tables
{
    public class SocietyTypeTablesTests
    {
        [Theory]
        //Diffuse
        [InlineData(1, 0, WorldUnity.Diffuse)]
        [InlineData(1, 1, WorldUnity.Diffuse)]
        [InlineData(1, 2, WorldUnity.Diffuse)]
        [InlineData(1, 3, WorldUnity.Diffuse)]
        [InlineData(1, 4, WorldUnity.Diffuse)]
        [InlineData(1, 5, WorldUnity.Diffuse)]
        [InlineData(2, 5, WorldUnity.Diffuse)]
        [InlineData(1, 6, WorldUnity.Diffuse)]
        [InlineData(2, 6, WorldUnity.Diffuse)]
        [InlineData(3, 6, WorldUnity.Diffuse)]
        [InlineData(1, 7, WorldUnity.Diffuse)]
        [InlineData(2, 7, WorldUnity.Diffuse)]
        [InlineData(3, 7, WorldUnity.Diffuse)]
        [InlineData(4, 7, WorldUnity.Diffuse)]
        [InlineData(5, 8, WorldUnity.Diffuse)]
        [InlineData(5, 9, WorldUnity.Diffuse)]
        [InlineData(5, 10, WorldUnity.Diffuse)]
        [InlineData(5, 11, WorldUnity.Diffuse)]
        [InlineData(5, 12, WorldUnity.Diffuse)]
        //Factionalized
        [InlineData(2, 0, WorldUnity.Factionalized)]
        [InlineData(2, 1, WorldUnity.Factionalized)]
        [InlineData(2, 2, WorldUnity.Factionalized)]
        [InlineData(2, 3, WorldUnity.Factionalized)]
        [InlineData(2, 4, WorldUnity.Factionalized)]
        [InlineData(3, 5, WorldUnity.Factionalized)]
        [InlineData(4, 6, WorldUnity.Factionalized)]
        [InlineData(5, 7, WorldUnity.Factionalized)]
        [InlineData(6, 8, WorldUnity.Factionalized)]
        [InlineData(6, 9, WorldUnity.Factionalized)]
        [InlineData(6, 10, WorldUnity.Factionalized)]
        [InlineData(6, 11, WorldUnity.Factionalized)]
        [InlineData(6, 12, WorldUnity.Factionalized)]
        //Coalition
        [InlineData(3, 0, WorldUnity.Coalition)]
        [InlineData(3, 1, WorldUnity.Coalition)]
        [InlineData(3, 2, WorldUnity.Coalition)]
        [InlineData(3, 3, WorldUnity.Coalition)]
        [InlineData(3, 4, WorldUnity.Coalition)]
        [InlineData(4, 5, WorldUnity.Coalition)]
        [InlineData(5, 6, WorldUnity.Coalition)]
        [InlineData(6, 7, WorldUnity.Coalition)]
        [InlineData(7, 8, WorldUnity.Coalition)]
        [InlineData(7, 9, WorldUnity.Coalition)]
        [InlineData(7, 10, WorldUnity.Coalition)]
        [InlineData(7, 11, WorldUnity.Coalition)]
        [InlineData(7, 12, WorldUnity.Coalition)]
        //World Government(Special)
        [InlineData(4, 0, WorldUnity.WorldGovernment_Special)]
        [InlineData(4, 1, WorldUnity.WorldGovernment_Special)]
        [InlineData(4, 2, WorldUnity.WorldGovernment_Special)]
        [InlineData(4, 3, WorldUnity.WorldGovernment_Special)]
        [InlineData(4, 4, WorldUnity.WorldGovernment_Special)]
        [InlineData(5, 5, WorldUnity.WorldGovernment_Special)]
        [InlineData(6, 6, WorldUnity.WorldGovernment_Special)]
        [InlineData(7, 7, WorldUnity.WorldGovernment_Special)]
        [InlineData(8, 8, WorldUnity.WorldGovernment_Special)]
        [InlineData(8, 9, WorldUnity.WorldGovernment_Special)]
        [InlineData(8, 10, WorldUnity.WorldGovernment_Special)]
        [InlineData(8, 11, WorldUnity.WorldGovernment_Special)]
        [InlineData(8, 12, WorldUnity.WorldGovernment_Special)]
        //World Government
        [InlineData(5, 0, WorldUnity.WorldGovernment)]
        [InlineData(5, 1, WorldUnity.WorldGovernment)]
        [InlineData(5, 2, WorldUnity.WorldGovernment)]
        [InlineData(5, 3, WorldUnity.WorldGovernment)]
        [InlineData(5, 4, WorldUnity.WorldGovernment)]
        [InlineData(6, 5, WorldUnity.WorldGovernment)]
        [InlineData(7, 6, WorldUnity.WorldGovernment)]
        [InlineData(8, 7, WorldUnity.WorldGovernment)]
        [InlineData(9, 8, WorldUnity.WorldGovernment)]
        [InlineData(10, 8, WorldUnity.WorldGovernment)]
        [InlineData(11, 8, WorldUnity.WorldGovernment)]
        [InlineData(12, 8, WorldUnity.WorldGovernment)]
        [InlineData(12, 9, WorldUnity.WorldGovernment)]
        [InlineData(12, 12, WorldUnity.WorldGovernment)]
        public void GenerateWorldUnity_ReturnsCorrectWorldUnity(int roll, int populationRating, WorldUnity expected)
        {
            // Act
            WorldUnity result = SocietyTypeTables.GenerateWorldUnity(roll, populationRating);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, 0, WorldUnity.Diffuse)]
        [InlineData(0, 1, WorldUnity.Diffuse)]
        [InlineData(0, 2, WorldUnity.Diffuse)]
        [InlineData(0, 3, WorldUnity.Diffuse)]
        [InlineData(0, 4, WorldUnity.Diffuse)]
        [InlineData(0, 5, WorldUnity.Diffuse)]
        [InlineData(0, 6, WorldUnity.Diffuse)]
        [InlineData(0, 7, WorldUnity.Diffuse)]
        [InlineData(0, 8, WorldUnity.Diffuse)]
        [InlineData(0, 9, WorldUnity.Diffuse)]
        [InlineData(0, 10, WorldUnity.Diffuse)]
        [InlineData(0, 11, WorldUnity.Diffuse)]
        [InlineData(0, 12, WorldUnity.Diffuse)]
        [InlineData(-1, 0, WorldUnity.Diffuse)]
        public void GenerateWorldUnity_ZeroOrNegativeRoll_ReturnsDiffuseWorldUnity(int roll, int populationRating, WorldUnity expected)
        {
            // Act
            WorldUnity result = SocietyTypeTables.GenerateWorldUnity(roll, populationRating);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
