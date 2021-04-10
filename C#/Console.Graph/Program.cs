using System;
using System.Linq;

using Library.Graph.Generators;
using Library.Graph.Generators.Options;
using Library.Graph.ExampleConvertibleTypes;

namespace Console.Graph
{
    public class Program
    {
        private static void Main()
        {
            var rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

            var generator = new TransportNetworkGraphGenerator<IntConvertible>(
                new TransportNetworkGraphGeneratorOptions<IntConvertible>(
                100,
                10,
                () => rnd.Next(100),
                5,
                5));

            foreach (var item in Enumerable.Range(0, 10))
            {
                var result = generator.Generate();
                if (result.Graph.ConnectivityType != ConnectivityType.WeaklyOrJustConnected)
                {
                    throw new InvalidOperationException("Trash");
                }
            }
        }
    }
}