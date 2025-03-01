using GeneratorLibrary.Generators.Tables;
using GeneratorLibrary.Models;

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
    }

}
