using System;

using Library.Graph.Generators;
using Library.Graph.Generators.Options;
using Library.Graph.ExampleConvertibleTypes;
using Library.Graph.Operations.Extensions;

namespace Console.Graph
{
    public class Program
    {
        private static void Main()
        {
            var rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

            var generator = new TransportNetworkGraphGenerator<IntConvertible>(
                new TransportNetworkGraphGeneratorOptions<IntConvertible>(
                5,
                2,
                () => rnd.Next(10),
                2,
                2,
                (0, 15)));
            var result = generator.Generate();

            foreach (var item in result.Graph.Items.Values)
            {
                System.Console.WriteLine(item);
            }
            System.Console.WriteLine();

            foreach (var item in result.Graph.Edges)
            {
                System.Console.WriteLine(item);
            }

            foreach (var item in result.Graph.Adjacensies)
            {
                System.Console.WriteLine(item);
            }

            foreach (var e in result.Graph.SetupDijkstraWalking())
            {
                System.Console.WriteLine(e.Source + "->" + e.Target + "==" + e.Weight);
            }
        }
    }
}