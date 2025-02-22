using GeneratorLibrary.Utils;

namespace GeneratorLibrary.Tests.Utils
{
    public class TestRandomProvider : IRandomProvider
    {
        private readonly double _fixedValue;

        public TestRandomProvider(double fixedValue)
        {
            _fixedValue = fixedValue;
        }

        public double NextDouble() => _fixedValue;
    }
}
