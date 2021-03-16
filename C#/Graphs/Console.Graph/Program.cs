using System;
using System.Threading.Tasks;

using Library.Graph.Types;
using Library.Graph.Extensions;
using Library.Graph.ConvertibleTypes;

namespace Console.Graph
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var fileName = await GenerateAndExportGraph();

            var graph = new OrientedAdjacensiesGraph<IntConvertible>();
            await graph.ImportAsync(fileName);

            foreach (var seq in graph.View.Items)
            {
                System.Console.WriteLine(seq);
            }
        }

        private static async Task<string> GenerateAndExportGraph()
        {
            var graph = OrientedAdjacensiesGraph<IntConvertible>.GenerateWithWeakCohesion(4, 2, () => new IntConvertible(_rnd.Next(0, 30)));

            return await graph.ExportAsync();
        }

        private static Random _rnd = new Random();
    }
}