using GeneratorLibrary.Generators.Tables.Advanced;
using GeneratorLibrary.Models.Advanced;

namespace GeneratorLibrary.Generators
{
    public class StarSystemGenerator
    {
        private readonly DiceRoller _diceRoller = DiceRoller.Instance;

        public StarSystemGenerator() { }
        public StarSystemGenerator(int seed)
        {
            _diceRoller = new DiceRoller(seed);
        }

        public StarSystem CreateStarSystem(bool isOpenCluster = false)
        {
            StarSystem starSystem = new StarSystem();

            //STEP 15: Number of Stars (S100)
            int numberOfStars;
            if (isOpenCluster)
                numberOfStars = NumberOfStarsTable.GenerateNumberOfStars(_diceRoller.Roll(3, 3));
            else
                numberOfStars = NumberOfStarsTable.GenerateNumberOfStars(_diceRoller.Roll(3));

            for (int i = 0; i < numberOfStars; i++)
                starSystem.Stars.Add(new Star());

            return starSystem;
        }
    }
}
