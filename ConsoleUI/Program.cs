using GeneratorLibrary.Generators;

WorldGenerator worldGenerator = new();

Console.WriteLine("Welcome to the World Generator!");
Console.WriteLine("-----------------------------");
Console.WriteLine("Generating world...");

var world = worldGenerator.GenerateWorld();

Console.WriteLine("World generated!");
Console.WriteLine();

Console.WriteLine(world.ToString());

Console.WriteLine();
Console.WriteLine("-----------------------------");
Console.WriteLine("Press any key to exit...");
