namespace GeneratorLibrary.Utils
{
    public interface IRandomProvider
    {
        double NextDouble(); // Devuelve un número aleatorio entre 0.0 y 1.0
    }

    // Implementación real para producción
    public class RandomProvider : IRandomProvider
    {
        public double NextDouble() => Random.Shared.NextDouble();
    }

}
