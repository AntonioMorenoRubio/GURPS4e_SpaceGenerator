namespace GeneratorLibrary.Utils;

public class DiceRoller : IDiceRoller
{
    private readonly Random _random;

    public DiceRoller()
    {
        _random = new Random();
    }

    public DiceRoller(int seed)
    {
        _random = new Random(seed);
    }

    public DiceRoller(Random random)
    {
        _random = random ?? throw new ArgumentNullException(nameof(random));
    }

    public int Roll(int numberOfDice = 3, params int[] modifiers)
    {
        if (numberOfDice < 1)
        {
            throw new ArgumentException("Number of dice must be at least 1", nameof(numberOfDice));
        }

        return _random.Next(numberOfDice, numberOfDice * 6 + 1) + modifiers.Sum();
    }
}
