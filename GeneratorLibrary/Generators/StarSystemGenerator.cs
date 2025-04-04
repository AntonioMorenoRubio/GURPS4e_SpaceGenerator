using GeneratorLibrary.Generators.Tables.Advanced;
using GeneratorLibrary.Models.Advanced;
using GeneratorLibrary.Utils;

namespace GeneratorLibrary.Generators
{
    public class StarSystemGenerator
    {
        private readonly bool mustHaveGardenWorld;
        private readonly DiceRoller _diceRoller = new();

        public StarSystemGenerator() { }
        public StarSystemGenerator(int seed)
        {
            _diceRoller = new DiceRoller(seed);
        }

        public StarSystemGenerator(bool mustHaveGardenWorld)
        {
            this.mustHaveGardenWorld = mustHaveGardenWorld;
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

            //STEP 17: Stellar Age (S102)
            (StellarAgePopulationType type, double age) = StellarAgeTable.DetermineStellarAge(_diceRoller, mustHaveGardenWorld);
            starSystem.StellarAge.Type = type;
            starSystem.StellarAge.Age = age;

            //STEP 18: Stellar Characteristics (S102-104)
            foreach (var star in starSystem.Stars)
            {
                StellarEvolutionData data = StellarCharacteristicsTables.GetStellarData(star.Mass);

                double maxAgeBeforeWhiteDwarf = data.MainSequenceSpan ?? double.MaxValue;
                double maxAgeAsSubgiant = data.MainSequenceSpan ?? double.MaxValue;

                if (data.SubGiantSpan.HasValue)
                {
                    maxAgeBeforeWhiteDwarf += data.SubGiantSpan.Value;
                    maxAgeAsSubgiant += data.SubGiantSpan.Value;
                }

                if (data.GiantSpan.HasValue)
                    maxAgeBeforeWhiteDwarf += data.GiantSpan.Value;

                bool isWhiteDwarf = starSystem.StellarAge.Age > maxAgeBeforeWhiteDwarf;
                bool isGiant = starSystem.StellarAge.Age > maxAgeAsSubgiant;
                bool isSubgiant = starSystem.StellarAge.Age > data.MainSequenceSpan;

                star.Type = StellarCharacteristicsTables.DetermineStarType(star.Mass);
                star.LuminosityClass = StellarCharacteristicsTables.DetermineLuminosityClass(star.Mass, starSystem.StellarAge.Age);

                if (isWhiteDwarf)
                {
                    star.Mass = StellarCharacteristicsTables.WhiteDwarfMass(_diceRoller);
                    star.Temperature = StellarCharacteristicsTables.WhiteDwarfTemperature(star.Mass);
                    star.Luminosity = StellarCharacteristicsTables.WhiteDwarfLuminosity;

                }
                else if (isGiant)
                {
                    star.Temperature = StellarCharacteristicsTables.CalculateGiantTemperature(_diceRoller);
                    star.Luminosity = StellarCharacteristicsTables.CalculateGiantLuminosity(star.Mass);
                }
                else if (isSubgiant)
                {
                    star.Temperature = StellarCharacteristicsTables.CalculateSubGiantTemperature(star.Mass, starSystem.StellarAge.Age);
                    star.Luminosity = StellarCharacteristicsTables.CalculateSubGiantLuminosity(star.Mass);
                }
                else
                {
                    star.Temperature = StellarCharacteristicsTables.CalculateMainSequenceTemperature(star.Mass);
                    star.Luminosity = StellarCharacteristicsTables.CalculateMainSequenceLuminosity(star.Mass, starSystem.StellarAge.Age);
                }
                star.Radius_AU = StellarCharacteristicsTables.DetermineStarRadiusInAu(star.LuminosityClass, star.Luminosity, star.Temperature);
            }

            return starSystem;
        }
    }
}
