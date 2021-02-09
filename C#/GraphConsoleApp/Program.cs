using System.IO;
using System.Linq;
using System.Threading.Tasks;

using ConsoleApp.Extensions;
using ConsoleApp.Graph;

using static System.Console;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var fileName = "graph.csv";

            //var graph = UnOrientedGraph.Generate(6);

            //graph.AdjacensyList.DumpToCSV(fileName);

            //WriteLine(await File.ReadAllTextAsync(fileName));

            //foreach (var item in graph.SetupDFSWalking())
            //{
            //    Write(item + " ");
            //}
            //WriteLine("");

            //foreach (var item in graph.SetupBFSWalking())
            //{
            //    Write(item + " ");
            //}

            //WriteLine();

            foreach (var item in PrimaryQueue<int>.CreateMinPrimaryQueue(Enumerable.Range(0, 16).Reverse()))
            {
                WriteLine(item);
            }
        }
    }
}