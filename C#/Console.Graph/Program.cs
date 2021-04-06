using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Library.Graph.Generators;
using Library.Graph.ConvertibleTypes;
using Library.Graph.Generators.Options;
using Library.Graph.Types.Adjacensies;
using Library.Graph.ImportersExporters;

namespace Console.Graph
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            var generator = new TransportNetworkViewGenerator<IntConvertible>(
                new TransportNetworkViewGeneratorOptions<IntConvertible>(
                8,
                2,
                () => rnd.Next(10),
                3,
                3));

            var result = generator.Generate();

            var orientedGraph = new OrientedAdjacensiesGraph<IntConvertible>(result.View);

            await orientedGraph.ExportAsync(new SpreadSheetImporterExporter());
        }
    }
}