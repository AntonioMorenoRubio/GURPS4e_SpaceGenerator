using GeneratorLibrary.Generators;
using GeneratorLibrary.Models.Advanced;
using GeneratorLibrary.Models.Basic;

WorldGenerator worldGenerator = new();
StarSystemGenerator starSystemGenerator = new();

bool isGeneratingStarSystems = true;

if (isGeneratingStarSystems)
    GenerateStarSystems(starSystemGenerator);
else
    GenerateWorlds(worldGenerator);

Console.WriteLine("Press any key to exit...");

static void GenerateStarSystems(StarSystemGenerator starSystemGenerator)
{
    Console.WriteLine("Welcome to the Star System Generator!");
    Console.WriteLine("-----------------------------");
    Console.WriteLine("Generating Star Systems...");

    List<StarSystem> systems = new();

    for (int i = 0; i < 1000; i++)
    {
        systems.Add(starSystemGenerator.CreateStarSystem());
    }

    Console.WriteLine("Star systems generated!");
    Console.WriteLine();

    foreach (StarSystem s in systems)
    {
        Console.WriteLine(s.ToString());
        Console.WriteLine();
        Console.WriteLine("-----------------------------");
    }
}

static void GenerateWorlds(WorldGenerator worldGenerator)
{
    Console.WriteLine("Welcome to the World Generator!");
    Console.WriteLine("-----------------------------");
    Console.WriteLine("Generating world...");

    List<World> worlds = new();

    for (int i = 0; i < 1000; i++)
    {
        worlds.Add(worldGenerator.GenerateWorld());
    }

    Console.WriteLine("Worlds generated!");
    Console.WriteLine();

    foreach (World w in worlds.Where(x => x.Population?.CurrentPopulation > x.Population?.CarryingCapacity))
    {
        Console.WriteLine(w.ToString());
        Console.WriteLine();
        Console.WriteLine("-----------------------------");
    }
}
