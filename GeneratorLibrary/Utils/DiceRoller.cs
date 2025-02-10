namespace GeneratorLibrary;

public class DiceRoller
{
    private Random _random = new();

    public DiceRoller() {}

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
