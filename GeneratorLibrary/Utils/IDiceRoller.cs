namespace GeneratorLibrary.Utils
{
    public interface IDiceRoller
    {
        int Roll(int numberOfDice = 3, params int[] modifiers);
    }
}
