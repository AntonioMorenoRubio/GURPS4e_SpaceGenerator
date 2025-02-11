using GeneratorLibrary.Generators;
using GeneratorLibrary.Models;

namespace GeneratorLibrary.Tests;

public class WorldGeneratorTests
{
    [Fact]
    public void GenerateWorld_ShouldReturnNonNullWorld()
    {
        // Arrange
        var generator = new WorldGenerator();

        // Act
        var world = generator.GenerateWorld();

        // Assert
        Assert.NotNull(world);
    }

    [Fact]
    public void GenerateWorld_ShouldReturnWorld()
    {
        // Arrange
        var generator = new WorldGenerator();

        // Act
        var world = generator.GenerateWorld();

        // Assert
        Assert.NotNull(world);
    }

    [Fact]
    public void GenerateWorld_ShouldReturnWorldWithSizeAndSubtype()
    {
        // Arrange
        var generator = new WorldGenerator();

        // Act
        var world = generator.GenerateWorld();

        // Assert
        Assert.NotNull(world.Type);
        Assert.IsType<WorldType>(world.Type);
        Assert.IsType<WorldSize>(world.Type.Size);
        Assert.IsType<WorldSubType>(world.Type.SubType);
    }

    [Fact]
    public void GenerateWorld_ShouldBeDeterministic()
    {
        // Arrange
        var seed = 123;
        var generator1 = new WorldGenerator(seed);
        var generator2 = new WorldGenerator(seed);

        // Act
        var world1 = generator1.GenerateWorld();
        var world2 = generator2.GenerateWorld();

        // Assert
        Assert.Equal(world1?.Type?.Size, world2?.Type?.Size);
        Assert.Equal(world1?.Type?.SubType, world2?.Type?.SubType);
    }
}
