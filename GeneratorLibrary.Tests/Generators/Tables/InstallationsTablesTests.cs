using GeneratorLibrary.Generators.Tables;
using GeneratorLibrary.Models;

namespace GeneratorLibrary.Tests.Generators.Tables
{
    public class InstallationsTablesTests
    {
        [Theory]
        // PR 0
        [InlineData(0, 3, SpaceportClass.III)]
        [InlineData(0, 4, SpaceportClass.III)]
        [InlineData(0, 5, SpaceportClass.III)]
        [InlineData(0, 6, SpaceportClass.III)]
        [InlineData(0, 7, SpaceportClass.III)]
        [InlineData(0, 8, SpaceportClass.III)]
        [InlineData(0, 9, SpaceportClass.I)]
        [InlineData(0, 10, SpaceportClass.I)]
        [InlineData(0, 11, SpaceportClass.I)]
        [InlineData(0, 12, SpaceportClass.I)]
        [InlineData(0, 13, SpaceportClass.I)]
        [InlineData(0, 14, SpaceportClass.I)]
        [InlineData(0, 15, SpaceportClass.Ø)]
        [InlineData(0, 16, SpaceportClass.Ø)]
        [InlineData(0, 17, SpaceportClass.Ø)]
        [InlineData(0, 18, SpaceportClass.Ø)]

        //// PR 3
        [InlineData(3, 3, SpaceportClass.III)]
        [InlineData(3, 4, SpaceportClass.III)]
        [InlineData(3, 5, SpaceportClass.III)]
        [InlineData(3, 6, SpaceportClass.III)]
        [InlineData(3, 7, SpaceportClass.III)]
        [InlineData(3, 8, SpaceportClass.III)]
        [InlineData(3, 9, SpaceportClass.III)]
        [InlineData(3, 10, SpaceportClass.III)]
        [InlineData(3, 11, SpaceportClass.III)]
        [InlineData(3, 12, SpaceportClass.I)]
        [InlineData(3, 13, SpaceportClass.I)]
        [InlineData(3, 14, SpaceportClass.I)]
        [InlineData(3, 15, SpaceportClass.Ø)]
        [InlineData(3, 16, SpaceportClass.Ø)]
        [InlineData(3, 17, SpaceportClass.Ø)]
        [InlineData(3, 18, SpaceportClass.Ø)]

        //// PR 6
        [InlineData(6, 3, SpaceportClass.V)]
        [InlineData(6, 4, SpaceportClass.V)]
        [InlineData(6, 5, SpaceportClass.V)]
        [InlineData(6, 6, SpaceportClass.V)]
        [InlineData(6, 7, SpaceportClass.V)]
        [InlineData(6, 8, SpaceportClass.V)]
        [InlineData(6, 9, SpaceportClass.IV)]
        [InlineData(6, 10, SpaceportClass.IV)]
        [InlineData(6, 11, SpaceportClass.IV)]
        [InlineData(6, 12, SpaceportClass.III)]
        [InlineData(6, 13, SpaceportClass.III)]
        [InlineData(6, 14, SpaceportClass.III)]
        [InlineData(6, 15, SpaceportClass.Ø)]
        [InlineData(6, 16, SpaceportClass.Ø)]
        [InlineData(6, 17, SpaceportClass.Ø)]
        [InlineData(6, 18, SpaceportClass.Ø)]

        // PR 9
        [InlineData(9, 3, SpaceportClass.V)]
        [InlineData(9, 4, SpaceportClass.V)]
        [InlineData(9, 5, SpaceportClass.V)]
        [InlineData(9, 6, SpaceportClass.V)]
        [InlineData(9, 7, SpaceportClass.V)]
        [InlineData(9, 8, SpaceportClass.V)]
        [InlineData(9, 9, SpaceportClass.V)]
        [InlineData(9, 10, SpaceportClass.V)]
        [InlineData(9, 11, SpaceportClass.V)]
        [InlineData(9, 12, SpaceportClass.IV)]
        [InlineData(9, 13, SpaceportClass.IV)]
        [InlineData(9, 14, SpaceportClass.IV)]
        [InlineData(9, 15, SpaceportClass.III)]
        [InlineData(9, 16, SpaceportClass.III)]
        [InlineData(9, 17, SpaceportClass.III)]
        [InlineData(9, 18, SpaceportClass.Ø)]

        public void DetermineSpaceportClass_ShouldReturnExpectedClass(int populationRating, int roll, SpaceportClass expected)
        {
            // Act
            var result = InstallationsTables.DetermineSpaceportClass(populationRating, roll);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(-1, 10)] // PR negativo
        [InlineData(5, 2)]   // Roll fuera del rango (menor a 3)
        [InlineData(5, 19)]  // Roll fuera del rango (mayor a 18)
        public void DetermineSpaceportClass_InvalidInputs_ShouldThrowException(int populationRating, int roll)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => InstallationsTables.DetermineSpaceportClass(populationRating, roll));
        }
    }

}
