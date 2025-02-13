namespace GeneratorLibrary;

public class DiceRoller
{
    private static readonly Lazy<DiceRoller> _instance = new(() => new DiceRoller());
    private readonly Random _random;
    public static DiceRoller Instance => _instance.Value;

    private DiceRoller()
    {
        _random = new Random();
    }

    public DiceRoller(int seed)
    {
        _random = new Random(seed);
    }

    public int Roll(int numberOfDice = 3, params int[] modifier)
    {
        if (numberOfDice < 1)
        {
            throw new ArgumentException("Number of dice must be at least 1", nameof(numberOfDice));
        }

        return _random.Next(numberOfDice, numberOfDice * 6 + 1) + modifier.Sum();
    }
}
