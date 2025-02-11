namespace GeneratorLibrary.Models
{
    public record WorldType(WorldSize Size, WorldSubType SubType);

    public enum WorldSize
    {
        Tiny,
        Small,
        Standard,
        Large,
        Special // For Asteroid Belt and Gas Giant
    }

    public enum WorldSubType
    {
        Chthonian,
        Greenhouse,
        Hadean,
        Ice,
        Rock,
        Sulfur,
        Ammonia,
        Ocean,
        Garden,
        AsteroidBelt,
        GasGiant
    }
}
