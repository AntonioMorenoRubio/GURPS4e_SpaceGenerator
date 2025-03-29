using GeneratorLibrary.Models.Advanced;
using GeneratorLibrary.Utils;

namespace GeneratorLibrary.Generators.Tables.Advanced
{
    public static class StellarAgeTable
    {
        public static (StellarAgePopulationType, double) DetermineStellarAge(IDiceRoller diceRoller, bool mustHaveGardenWorld = false)
        {
            int categoryRoll;
            if (mustHaveGardenWorld)
                categoryRoll = diceRoller.Roll(2, 2);
            else
                categoryRoll = diceRoller.Roll(3);

            if (categoryRoll == 3)
                return (StellarAgePopulationType.ExtremePopulationI, 0.0);

            int stepARoll, stepBRoll;
            stepARoll = diceRoller.Roll(1, -1);
            stepBRoll = diceRoller.Roll(1, -1);

            if (categoryRoll >= 4 && categoryRoll <= 6)
                return (StellarAgePopulationType.YoungPopulationI, Math.Round(0.1 + stepARoll * 0.3 + stepBRoll * 0.05, 2));
            if (categoryRoll >= 7 && categoryRoll <= 10)
                return (StellarAgePopulationType.IntermediatePopulationI, Math.Round(2 + stepARoll * 0.6 + stepBRoll * 0.1, 2));
            if (categoryRoll >= 11 && categoryRoll <= 14)
                return (StellarAgePopulationType.OldPopulationI, Math.Round(5.6 + stepARoll * 0.6 + stepBRoll * 0.1, 2));
            if (categoryRoll >= 15 && categoryRoll <= 17)
                return (StellarAgePopulationType.IntermediatePopulationII, Math.Round(8.0 + stepARoll * 0.6 + stepBRoll * 0.1, 2));
            if (categoryRoll == 18)
                return (StellarAgePopulationType.ExtremePopulationII, Math.Round(10.0 + stepARoll * 0.6 + stepBRoll * 0.1, 2));

            throw new ArgumentException($"Dice Roller somehow rolled less than 3 or more than 18 for the categoryRoll. categoryRoll:{categoryRoll}");
        }
    }
}
