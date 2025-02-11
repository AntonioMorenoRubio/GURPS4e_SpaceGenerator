using GeneratorLibrary.Models;
using GeneratorLibrary.Tables;

namespace GeneratorLibrary.Tests
{
    public class WorldTypeTablesTests
    {
        [Theory]
        [InlineData(1, "Hostile")]
        [InlineData(2, "Hostile")]
        [InlineData(3, "Hostile")]
        [InlineData(4, "Hostile")]
        [InlineData(5, "Hostile")]
        [InlineData(6, "Hostile")]
        [InlineData(7, "Hostile")]
        [InlineData(8, "Barren")]
        [InlineData(9, "Barren")]
        [InlineData(10, "Barren")]
        [InlineData(11, "Barren")]
        [InlineData(12, "Barren")]
        [InlineData(13, "Barren")]
        [InlineData(14, "Garden")]
        [InlineData(15, "Garden")]
        [InlineData(16, "Garden")]
        [InlineData(17, "Garden")]
        [InlineData(18, "Garden")]
        public void GetOverallType_ShouldReturnExpectedType(int diceRoll, string expectedType)
        {
            // Act
            var result = WorldTypeTables.GetOverallType(diceRoll);

            // Assert
            Assert.Equal(expectedType, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(19)]
        public void GetOverallType_InvalidDiceRoll_ShouldThrowArgumentOutOfRangeException(int diceRoll)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => WorldTypeTables.GetOverallType(diceRoll));
        }

        [Theory]
        [InlineData(3, WorldSize.Standard, WorldSubType.Chthonian)]
        [InlineData(4, WorldSize.Standard, WorldSubType.Chthonian)]
        [InlineData(5, WorldSize.Standard, WorldSubType.Greenhouse)]
        [InlineData(6, WorldSize.Standard, WorldSubType.Greenhouse)]
        [InlineData(7, WorldSize.Tiny, WorldSubType.Sulfur)]
        [InlineData(8, WorldSize.Tiny, WorldSubType.Sulfur)]
        [InlineData(9, WorldSize.Tiny, WorldSubType.Sulfur)]
        [InlineData(10, WorldSize.Standard, WorldSubType.Ammonia)]
        [InlineData(11, WorldSize.Standard, WorldSubType.Ammonia)]
        [InlineData(12, WorldSize.Standard, WorldSubType.Ammonia)]
        [InlineData(13, WorldSize.Large, WorldSubType.Ammonia)]
        [InlineData(14, WorldSize.Large, WorldSubType.Ammonia)]
        [InlineData(15, WorldSize.Large, WorldSubType.Greenhouse)]
        [InlineData(16, WorldSize.Large, WorldSubType.Greenhouse)]
        [InlineData(17, WorldSize.Large, WorldSubType.Chthonian)]
        [InlineData(18, WorldSize.Large, WorldSubType.Chthonian)]
        public void GenerateHostileWorld_ShouldReturnValidSizeAndSubType(int diceRoll, WorldSize expectedSize, WorldSubType expectedSubType)
        {
            // Act
            var result = WorldTypeTables.GenerateHostileWorld(diceRoll);

            // Assert
            Assert.Equal(expectedSize, result.Size);
            Assert.Equal(expectedSubType, result.SubType);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(19)]
        public void GenerateHostileWorld_InvalidDiceRoll_ShouldThrowArgumentOutOfRangeException(int diceRoll)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => WorldTypeTables.GenerateHostileWorld(diceRoll));
        }

        [Theory]
        [InlineData(3, WorldSize.Small, WorldSubType.Hadean)]
        [InlineData(4, WorldSize.Small, WorldSubType.Ice)]
        [InlineData(5, WorldSize.Small, WorldSubType.Rock)]
        [InlineData(6, WorldSize.Small, WorldSubType.Rock)]
        [InlineData(7, WorldSize.Tiny, WorldSubType.Rock)]
        [InlineData(8, WorldSize.Tiny, WorldSubType.Rock)]
        [InlineData(9, WorldSize.Tiny, WorldSubType.Ice)]
        [InlineData(10, WorldSize.Tiny, WorldSubType.Ice)]
        [InlineData(11, WorldSize.Special, WorldSubType.AsteroidBelt)]
        [InlineData(12, WorldSize.Special, WorldSubType.AsteroidBelt)]
        [InlineData(13, WorldSize.Standard, WorldSubType.Ocean)]
        [InlineData(14, WorldSize.Standard, WorldSubType.Ocean)]
        [InlineData(15, WorldSize.Standard, WorldSubType.Ice)]
        [InlineData(16, WorldSize.Standard, WorldSubType.Hadean)]
        [InlineData(17, WorldSize.Large, WorldSubType.Ocean)]
        [InlineData(18, WorldSize.Large, WorldSubType.Ice)]
        public void GenerateBarrenWorld_ShouldReturnValidSizeAndSubType(int diceRoll, WorldSize expectedSize, WorldSubType expectedSubType)
        {
            // Act
            var result = WorldTypeTables.GenerateBarrenWorld(diceRoll);

            // Assert
            Assert.Equal(expectedSize, result.Size);
            Assert.Equal(expectedSubType, result.SubType);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(19)]
        public void GenerateBarrenWorld_InvalidDiceRoll_ShouldThrowArgumentOutOfRangeException(int diceRoll)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => WorldTypeTables.GenerateBarrenWorld(diceRoll));
        }

        [Theory]
        [InlineData(3, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData(4, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData(5, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData(6, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData(7, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData(8, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData(9, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData(10, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData(11, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData(12, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData(13, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData(14, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData(15, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData(16, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData(17, WorldSize.Large, WorldSubType.Garden)]
        [InlineData(18, WorldSize.Large, WorldSubType.Garden)]
        public void GenerateGardenWorld_ShouldReturnValidSizeAndSubType(int diceRoll, WorldSize expectedSize, WorldSubType expectedSubType)
        {
            // Act
            var result = WorldTypeTables.GenerateGardenWorld(diceRoll);

            // Assert
            Assert.Equal(expectedSize, result.Size);
            Assert.Equal(expectedSubType, result.SubType);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(19)]
        public void GenerateGardenWorld_InvalidDiceRoll_ShouldThrowArgumentOutOfRangeException(int diceRoll)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => WorldTypeTables.GenerateGardenWorld(diceRoll));
        }

        [Theory]
        [InlineData("Hostile", 3, WorldSize.Standard, WorldSubType.Chthonian)]
        [InlineData("Hostile", 4, WorldSize.Standard, WorldSubType.Chthonian)]
        [InlineData("Hostile", 5, WorldSize.Standard, WorldSubType.Greenhouse)]
        [InlineData("Hostile", 6, WorldSize.Standard, WorldSubType.Greenhouse)]
        [InlineData("Hostile", 7, WorldSize.Tiny, WorldSubType.Sulfur)]
        [InlineData("Hostile", 8, WorldSize.Tiny, WorldSubType.Sulfur)]
        [InlineData("Hostile", 9, WorldSize.Tiny, WorldSubType.Sulfur)]
        [InlineData("Hostile", 10, WorldSize.Standard, WorldSubType.Ammonia)]
        [InlineData("Hostile", 11, WorldSize.Standard, WorldSubType.Ammonia)]
        [InlineData("Hostile", 12, WorldSize.Standard, WorldSubType.Ammonia)]
        [InlineData("Hostile", 13, WorldSize.Large, WorldSubType.Ammonia)]
        [InlineData("Hostile", 14, WorldSize.Large, WorldSubType.Ammonia)]
        [InlineData("Hostile", 15, WorldSize.Large, WorldSubType.Greenhouse)]
        [InlineData("Hostile", 16, WorldSize.Large, WorldSubType.Greenhouse)]
        [InlineData("Hostile", 17, WorldSize.Large, WorldSubType.Chthonian)]
        [InlineData("Hostile", 18, WorldSize.Large, WorldSubType.Chthonian)]
        [InlineData("Barren", 3, WorldSize.Small, WorldSubType.Hadean)]
        [InlineData("Barren", 4, WorldSize.Small, WorldSubType.Ice)]
        [InlineData("Barren", 5, WorldSize.Small, WorldSubType.Rock)]
        [InlineData("Barren", 6, WorldSize.Small, WorldSubType.Rock)]
        [InlineData("Barren", 7, WorldSize.Tiny, WorldSubType.Rock)]
        [InlineData("Barren", 8, WorldSize.Tiny, WorldSubType.Rock)]
        [InlineData("Barren", 9, WorldSize.Tiny, WorldSubType.Ice)]
        [InlineData("Barren", 10, WorldSize.Tiny, WorldSubType.Ice)]
        [InlineData("Barren", 11, WorldSize.Special, WorldSubType.AsteroidBelt)]
        [InlineData("Barren", 12, WorldSize.Special, WorldSubType.AsteroidBelt)]
        [InlineData("Barren", 13, WorldSize.Standard, WorldSubType.Ocean)]
        [InlineData("Barren", 14, WorldSize.Standard, WorldSubType.Ocean)]
        [InlineData("Barren", 15, WorldSize.Standard, WorldSubType.Ice)]
        [InlineData("Barren", 16, WorldSize.Standard, WorldSubType.Hadean)]
        [InlineData("Barren", 17, WorldSize.Large, WorldSubType.Ocean)]
        [InlineData("Barren", 18, WorldSize.Large, WorldSubType.Ice)]
        [InlineData("Garden", 3, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData("Garden", 4, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData("Garden", 5, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData("Garden", 6, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData("Garden", 7, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData("Garden", 8, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData("Garden", 9, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData("Garden", 10, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData("Garden", 11, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData("Garden", 12, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData("Garden", 13, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData("Garden", 14, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData("Garden", 15, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData("Garden", 16, WorldSize.Standard, WorldSubType.Garden)]
        [InlineData("Garden", 17, WorldSize.Large, WorldSubType.Garden)]
        [InlineData("Garden", 18, WorldSize.Large, WorldSubType.Garden)]
        public void GenerateWorldType_ShouldReturnCorrectSizeAndSubType(string overallType, int diceRoll, WorldSize expectedSize, WorldSubType expectedSubType)
        {
            // Act
            var result = WorldTypeTables.GenerateWorldType(overallType, diceRoll);

            // Assert
            Assert.Equal(expectedSize, result.Size);
            Assert.Equal(expectedSubType, result.SubType);
        }

        [Theory]
        [InlineData("InvalidType", 5)]
        [InlineData("UnknownType", 10)]
        public void GenerateWorldType_InvalidOverallType_ShouldThrowArgumentOutOfRangeException(string overallType, int diceRoll)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => WorldTypeTables.GenerateWorldType(overallType, diceRoll));
        }
    }
}
