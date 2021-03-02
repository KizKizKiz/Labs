using System;
using System.Linq;
using System.Threading.Tasks;
using Library.GraphTypes;

namespace Console.Graph
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // var fileName = "graph.csv";

            // var graph = UnOrientedGraph.Generate(10, 3);

            // graph.AdjacensyList.DumpToCSV(fileName);

            // WriteLine(File.ReadAllText(fileName));
            // var index = 0;
            // var aa = OrientedEdgeWithWeightGraph<string>.GenerateInCoherent(400, 20, () => $"Some {index++}");
            // foreach (var item in aa.View.Items)
            // {
            //     System.Console.WriteLine($"{item.First} -> {item.Second} : {item.Weight}");
            // }
            
            var graph = OrientedEdgeWithWeightGraph<IntConvertible>.GenerateWithWeakCohesion(10, 4, () => new IntConvertible(_rnd.Next(0, 30)));
            await graph.ExportAsync();
        }
        private static Random _rnd = new Random();
    }

}