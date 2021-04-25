using System;

using Library.Graph.ExampleConvertibleTypes;
using Library.Graph.Generators;
using Library.Graph.ImportersExporters;
using Library.Graph.Generators.Options;
using System.Threading.Tasks;
using Library.Graph.Types;
using Library.Graph.Extensions;
using System.Linq;

namespace Console.Graph
{
    public class Program
    {
        private static void Main()
        {
            //var graph = UnorientedGraphGenerate();
            //var graph = OrientedGraphGenerate();
            //var graph = TransportNetworkGenerate();
            ///var graph = BipartiteGraphGenerate();
            //await _exporter.ExportAsync(graph);
            var graph = FullyConnectedBipartiteGraphGenerate();
            foreach (var item in graph.LeftShare)
            {
                System.Console.WriteLine(item);
            }
            System.Console.WriteLine();
            foreach (var item in graph.RightShare)
            {
                System.Console.WriteLine(item);
            }
            (var matrix, var mapRowVertex, var mapColVertex) = graph.ToDoubleDemensionalArray();
            System.Console.WriteLine("  " + string.Join(' ', mapColVertex.Select(c => (c.Key, c.Value))));
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                System.Console.Write($"[{i}]{mapRowVertex[i]} ");
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    System.Console.Write($"{matrix[i, j]}\t");
                }
                System.Console.WriteLine();
            }

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

        private static BipartiteGraph<IntConvertible> FullyConnectedBipartiteGraphGenerate()
        {
            var generator = new FullyConnectedBipartiteGraphGenerator(
                new FullyConnectedBipartiteGraphGeneratorOptions(
                    4,
                    () => _rnd.Next(0, 8),
                    (1, 50)));
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

        private static BipartiteGraph<IntConvertible> BipartiteGraphGenerate()
        {
            var generator = new BipartiteGraphGenerator<IntConvertible>(
                new BipartiteGraphGeneratorOptions<IntConvertible>(
                    15,
                    () => _rnd.Next(0, 20)));
            var result = generator.Generate();
            return result.Graph;
        }

        private static readonly Random _rnd = new((int)DateTime.Now.Ticks & 0x0000FFFF);
        //private static readonly SpreadSheetImporterExporter _exporter = new();
    }
}