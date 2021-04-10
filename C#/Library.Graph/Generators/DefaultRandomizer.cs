using System;

namespace Library.Graph.Generators
{
    public class DefaultRandomizer : IRandomizer
    {
        public static IRandomizer Randomizer { get; } = new DefaultRandomizer();

        public int FromRange(int min, int max) => _random.Next(min, max);

        public int FromRange(int max) => _random.Next(max);

        private DefaultRandomizer() { }

        private static readonly Random _random = new((int)DateTime.Now.Ticks & 0x0000FFFF);
    }
}
