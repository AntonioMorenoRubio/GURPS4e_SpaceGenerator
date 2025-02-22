using GeneratorLibrary.Generators;
using GeneratorLibrary.Models;

WorldGenerator worldGenerator = new();

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

foreach (World w in worlds)
{
    Console.WriteLine(w.ToString());
    Console.WriteLine();
    Console.WriteLine("-----------------------------");
}

Console.WriteLine("Press any key to exit...");
