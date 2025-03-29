using GeneratorLibrary.Generators.Tables.Advanced;
using GeneratorLibrary.Models.Advanced;
using GeneratorLibrary.Utils;

namespace GeneratorLibrary.Generators
{
    public class StarSystemGenerator
    {
        private readonly DiceRoller _diceRoller = new();

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

            //STEP 16: Solar Masses (S101)
            int roll1 = _diceRoller.Roll();
            int roll2 = _diceRoller.Roll();
            starSystem.Stars[0].Mass = SolarMassesTable.GetPrimaryStarMass(roll1, roll2);

            if (starSystem.Stars.Count > 1)
                for (int i = 1; i < starSystem.Stars.Count; i++)
                    starSystem.Stars[i].Mass = SolarMassesTable.GetCompanionStarMass
                        (starSystem.Stars[0].Mass,
                        _diceRoller);

            return starSystem;
        }
    }
}
