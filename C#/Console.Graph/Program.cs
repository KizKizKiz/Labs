using System;
using System.Threading.Tasks;

using Library.Graph.ImportersExporters;
using Library.Graph.Generators;
using Library.Graph.Types;
using Library.Graph.Generators.Options;
using Library.Graph.ExampleConvertibleTypes;
using System.Linq;

namespace Console.Graph
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

            var generator = new TransportNetworkGraphGenerator<IntConvertible>(
                new TransportNetworkGraphGeneratorOptions<IntConvertible>(
                10_0000,
                1000,
                () => rnd.Next(100000000),
                1000,
                1000));

            foreach (var item in Enumerable.Range(0, 10))
            {
                var result = generator.Generate();
                if (result.Graph.ConnectivityType != ConnectivityType.WeaklyOrJustConnected)
                {
                    throw new Exception();
                }
            }
            //var graph = new AdjacensiesBasedGraph<int>(
            //    new[]
            //    {
            //        new AdjacensyGraphItem<int>(3, new []{ 7, 8 }),
            //        new AdjacensyGraphItem<int>(8, Array.Empty<int>()),
            //        new AdjacensyGraphItem<int>(7, new []{ 6, 8 }),
            //        new AdjacensyGraphItem<int>(6, new []{ 3 }),
            //        new AdjacensyGraphItem<int>(2, new []{ 6, 7, 3 }),
            //    },
            //    new[] { 3, 8, 7, 6, 2 },
            //    true,
            //    ConnectivityType.WeaklyOrJustConnected);

            //var expImp = new SpreadSheetImporterExporter(new Library.Graph.Converter.GraphConverter(DefaultRandomizer.Randomizer));

            //await expImp.ExportAsync(result.Graph).ConfigureAwait(false);
        }
    }
}