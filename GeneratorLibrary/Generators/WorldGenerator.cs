using GeneratorLibrary.Models;
using GeneratorLibrary.Tables;

namespace GeneratorLibrary.Generators
{
    public class WorldGenerator
    {
        private readonly DiceRoller _diceRoller = DiceRoller.Instance;

        public WorldGenerator() { }

        public WorldGenerator(int seed)
        {
            _diceRoller = new(seed);
        }

        public World GenerateWorld()
        {
            World world = new();

            int roll1, roll2;

            //STEP 2: Generate World Type
            roll1 = _diceRoller.Roll();
            roll2 = _diceRoller.Roll();
            string overallType = WorldTypeTables.GetOverallType(roll1);
            (WorldSize size, WorldSubType subType) = WorldTypeTables.GenerateWorldType(overallType, roll2);
            world.Type = new WorldType(size, subType);

            return world;
        }
    }
}
