namespace GeneratorLibrary.Models
{
    public record WorldType
    {
        public WorldSize Size { get; set; }
        public WorldSubType SubType { get; set; }
    }

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
