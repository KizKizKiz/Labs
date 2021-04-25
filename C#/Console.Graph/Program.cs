using System;

using Library.Graph.ExampleConvertibleTypes;
using Library.Graph.Generators;
using Library.Graph.Generators.Options;
using Library.Graph.Types;
using Library.Graph.Extensions;
using System.Linq;
using Library.Graph.Operations.Extensions;

namespace Console.Graph
{
    public class Program
    {
        private static void Main()
        {
            //var graph = UnorientedGraphGenerate();
            //var graph = OrientedGraphGenerate();
            //var graph = TransportNetworkGenerate();
            //var graph = BipartiteGraphGenerate();
            //await _exporter.ExportAsync(graph);
            //var graph = FullyConnectedBipartiteGraphGenerate();
            var graph = new BipartiteGraph<IntConvertible>(
                new AdjacensyEdgeItem<IntConvertible>[]
                {
                    new(4, new EdgeItem<IntConvertible>[] { new(4, 5, 31), new(4, 3, 45), new(4, 0, 28)}),
                    new(7, new EdgeItem<IntConvertible>[] { new(7, 5, 8), new(7, 3, 19), new(7, 0, 26)}),
                    new(2, new EdgeItem<IntConvertible>[] { new(2, 5, 30), new(2, 3, 2), new(2, 0, 47)}),

                    new(5, new EdgeItem<IntConvertible>[] { new(5, 4, 31), new(5, 7, 8), new(5, 2, 30)}),
                    new(0, new EdgeItem<IntConvertible>[] { new(0, 4, 28), new(0, 7, 26), new(0, 2, 47)}),
                    new(3, new EdgeItem<IntConvertible>[] { new(3, 4, 45), new(3, 7, 19), new(3, 2, 2)}),
                },
                new IntConvertible[] { 4, 7, 2, 3, 5, 0 });
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
            foreach(var pair in graph.SetupHungarianAlgorithm())
            {
                System.Console.WriteLine(string.Join("->",pair));
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
                    3,
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