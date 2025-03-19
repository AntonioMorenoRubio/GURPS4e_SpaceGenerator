using GeneratorLibrary.Generators;
using GeneratorLibrary.Models.Basic;

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
    public void GenerateWorld_ShouldReturnWorldWithAtmosphere()
    {
        // Arrange
        var generator = new WorldGenerator();

        // Act
        var world = generator.GenerateWorld();

        // Assert
        Assert.NotNull(world.Type);
        Assert.IsType<Atmosphere>(world.Atmosphere);
    }

    [Fact]
    public void GenerateWorld_ShouldBeDeterministic()
    {
        // Arrange
        int seed = 123;
        int TL = 10;
        var generator1 = new WorldGenerator(TL, seed);
        var generator2 = new WorldGenerator(TL, seed);

        // Act
        World? world1 = generator1.GenerateWorld();
        World? world2 = generator2.GenerateWorld();

        // Assert
        Assert.Equal(world1?.Type?.Size, world2?.Type?.Size);
        Assert.Equal(world1?.Type?.SubType, world2?.Type?.SubType);
    }
}
