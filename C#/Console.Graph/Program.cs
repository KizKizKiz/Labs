using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using static System.Console;

using Library.GraphTypes;
using Library.GraphTypes.Operations;
using Library.GraphTypes.Views;

namespace Console.Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            // var fileName = "graph.csv";

            // var graph = UnOrientedGraph.Generate(10, 3);

            // graph.AdjacensyList.DumpToCSV(fileName);

            // WriteLine(File.ReadAllText(fileName));
            var index = 0;
            var aa = UnorientedAdjacensiesGraph<string>.GenerateInCoherent(10, 3, () => $"Some {index++}");
            foreach (var item in new DFSIterator<string>(aa))
            {
                WriteLine(item);
            }

        }
        private static Random _rnd = new Random();
    }
}