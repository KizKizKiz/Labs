using System;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections.Generic;
using System.IO;

using CsvHelper;
using CsvHelper.Configuration;

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
            System.Console.WriteLine("\n'Strongly connected components' iterator output:");
            var index = 1;
            foreach (var item in graph.SetupSCCWalking())
            {
                System.Console.WriteLine(string.Join(" -> ", item));
                DumpToCSV(item, $"SCC_{index}({fileName})");
            }
        }

        public static void DumpToCSV<T>(IEnumerable<T> source, string fileName)
        {
            using StreamWriter streamReader = new StreamWriter(new FileStream(fileName, FileMode.Create));
            using CsvWriter writer = new CsvWriter(streamReader, new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                Delimiter = ","
            });
            writer.WriteRecords(source);

            System.Console.WriteLine($"\nDumped successfully to '{fileName}'.");
        }

        private static async Task<string> GenerateAndExportGraph()
        {
            var graph = OrientedAdjacensiesGraph<IntConvertible>.GenerateInCoherent(7, 3, () => new IntConvertible(_rnd.Next(0, 30)));

            return await graph.ExportAsync();
        }

        private static Random _rnd = new Random();
    }
}