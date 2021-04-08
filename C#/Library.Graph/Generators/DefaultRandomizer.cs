using System;

namespace Library.Graph.Generators
{
    public class DefaultRandomizer : IRandomizer
    {
        public static IRandomizer Randomizer => _randomizer;

        public int FromRange(int min, int max) => _random.Next(min, max);

        public int FromRange(int max) => _random.Next(max);

        private DefaultRandomizer() { }

        private static IRandomizer _randomizer = new DefaultRandomizer();

        private static Random _random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
    }
}
