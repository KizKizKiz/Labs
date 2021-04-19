using System;

using Library.Graph.ExampleConvertibleTypes;
using Library.Graph.Generators;
using Library.Graph.ImportersExporters;
using Library.Graph.Generators.Options;
using System.Threading.Tasks;
using Library.Graph.Types;

namespace Console.Graph
{
    public class Program
    {
        private static async Task Main()
        {
            //var graph = UnorientedGraphGenerate();
            var graph = OrientedGraphGenerate();
            //var graph = TransportNetworkGenerate();
            //var graph = BipartiteGraphGenerate();
            await _exporter.ExportAsync(graph);
        }

        private static Graph<IntConvertible> UnorientedGraphGenerate()
        {
            var generator = new UnorientedGraphGenerator<IntConvertible>(
                new UnorientedGraphGeneratorOptions<IntConvertible>(
                    15,
                    5,
                    () => _rnd.Next(0, 20),
                    (0, 0),
                    true));
            var result = generator.Generate();
            return result.Graph;
        }

        private static Graph<IntConvertible> OrientedGraphGenerate()
        {
            var generator = new OrientedGraphGenerator<IntConvertible>(
                new OrientedGraphGeneratorOptions<IntConvertible>(
                    10,
                    5,
                    () => _rnd.Next(0, 10),
                    ConnectivityType.WeaklyOrJustConnected,
                    (0, 50)));
            var result = generator.Generate();
            return result.Graph;
        }

        private static Graph<IntConvertible> TransportNetworkGenerate()
        {
            var generator = new TransportNetworkGraphGenerator<IntConvertible>(
                new TransportNetworkGraphGeneratorOptions<IntConvertible>(
                    15,
                    5,
                    () => _rnd.Next(0, 20),
                    5,
                    5,
                    (0, 50)));
            var result = generator.Generate();
            return result.Graph;
        }

        private static Graph<IntConvertible> BipartiteGraphGenerate()
        {
            var generator = new BipartiteGraphGenerator<IntConvertible>(
                new BipartiteGraphGeneratorOptions<IntConvertible>(
                    15,
                    () => _rnd.Next(0, 20)));
            var result = generator.Generate();
            return result.Graph;
        }

        private static readonly Random _rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        private static readonly SpreadSheetImporterExporter _exporter = new();
    }
}