using System;
using System.Collections;
using System.Collections.Generic;
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
        static void Main(string[] args)
        {
            var fileName = "graph.csv";

            var graph = UnOrientedGraph.Generate(10, 3);

            graph.AdjacensyList.DumpToCSV(fileName);

            WriteLine(File.ReadAllText(fileName));
        }
    }
}