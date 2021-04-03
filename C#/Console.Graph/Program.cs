using System;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections.Generic;
using System.IO;

using CsvHelper;
using CsvHelper.Configuration;

using System.Collections.Generic;

using Library.Graph.Generators;
using Library.Graph.Generators.Options;
using Library.Graph.Views;

namespace Console.Graph
{
    class Program
    {
        static async Task Main(string[] args)
        {

            //var ll = new List<List<int>>();
            //ll[0] = new List<int>() { 1, 2, 3, 4 };

            //new OrientedViewGenerator<AdjacensiesView<Exception>, AdjacensyViewItem<Exception>, Exception>(
            //    new OrientedViewGeneratorOptions<AdjacensiesView<Exception>, AdjacensyViewItem<Exception>, Exception>(5, 5, null!, ConnectivityType.NotConnected));

            //new OrientedViewGenerator<EdgesWithWeightView<Exception>, EdgeViewItem<Exception>, Exception>(
            //    new OrientedViewGeneratorOptions<EdgesWithWeightView<Exception>, EdgeViewItem<Exception>, Exception>(5, 5, null!, ConnectivityType.NotConnected));
        }

        //private static async Task<string> GenerateAndExportGraph()
        //{
        //    var graph = OrientedAdjacensiesGraph<IntConvertible>.GenerateWithWeakCohesion(4, 2, () => new IntConvertible(_rnd.Next(0, 30)));

        //    return await graph.ExportAsync();
        //}

        private static Random _rnd = new Random();
    }
}