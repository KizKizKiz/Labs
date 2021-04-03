using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using OfficeOpenXml;

using Library.Graph.Views;
using Library.Graph.ConvertibleTypes;
using Library.Graph.Generators;

namespace Library.Graph.Types
{
    /// <summary>
    /// Представляет реализацию неориентированного графа на массиве ребер.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public sealed class UnorientedEdgeWithWeightGraph<TValue> : EdgeWithWeightGraph<TValue>
        where TValue : IStringConvertible<TValue>, new()
    {
        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="view">Представления ребер на массиве ребер.</param>
        public UnorientedEdgeWithWeightGraph(EdgesWithWeightView<TValue> view)
            : base(view)
        { }

        public UnorientedEdgeWithWeightGraph(ViewGeneratingResult<EdgesWithWeightView<TValue>, EdgeViewItem<TValue>, TValue> viewGeneratingResult)
            : base(viewGeneratingResult)
        { }
        //protected override async Task<string> ExportCoreAsync(string fileName)
        //{
        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        //    using var package = new ExcelPackage(new FileInfo(fileName));
        //    var worksheet = package.Workbook.Worksheets.Add("GRAPH_DUMP");

        //    worksheet.Cells[1, 1].Value = "Source";
        //    worksheet.Cells[1, 2].Value = "Target";
        //    worksheet.Cells[1, 3].Value = "Weight";
        //    worksheet.Cells[1, 4].Value = "Type";
        //    worksheet.Cells[1, 5].Value = "Label";

        //    for (int i = 0; i < View.Items.Count; i++)
        //    {
        //        worksheet.Cells[i + 2, 1].Value = View.Items[i].First.ToString();
        //        worksheet.Cells[i + 2, 2].Value = View.Items[i].Second.ToString();
        //        worksheet.Cells[i + 2, 3].Value = View.Items[i].Weight;
        //        worksheet.Cells[i + 2, 4].Value = EdgeType;
        //        worksheet.Cells[i + 2, 5].Value = $"From '{View.Items[i].First}' to '{View.Items[i].Second}'";
        //    }

        //    await package.SaveAsync().ConfigureAwait(false);

        //    return fileName;
        //}

        //protected override async Task ImportCoreAsync(string fileName)
        //{
        //    await Task.Yield();

        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        //    using var package = new ExcelPackage(new FileInfo(fileName));
        //    var worksheet = package.Workbook.Worksheets[0];

        //    var edges = new List<EdgeViewItem<TValue>>();
        //    var vertexSet = new HashSet<TValue>();
        //    for (int i = 2; i <= worksheet.Dimension.End.Row; i++) // int i = 1 Skip headers
        //    {
        //        var entity = new TValue();
        //        var edge = new EdgeViewItem<TValue>(
        //            entity.ConvertFromString(worksheet.Cells[i, 1].Value.ToString()),
        //            entity.ConvertFromString(worksheet.Cells[i, 2].Value.ToString()),
        //            Convert.ToDouble(worksheet.Cells[i, 3].Value.ToString()));

        //        _ = vertexSet.Add(edge.First);
        //        _ = vertexSet.Add(edge.Second);

        //        edges.Add(edge);
        //    }

        //    VerticesSet = vertexSet.ToList();
        //    View = new EdgesWithWeightView<TValue>(edges);
        //}

        //private static void InitializeCoherentMapCore()
        //{
        //    _ = MapVertexAndLists
        //        .Aggregate((f, s) =>
        //        {
        //            s.Value.Items.Add(f.Key);
        //            return s;
        //        });
        //    foreach (var pair in MapVertexAndLists)
        //    {
        //        _ = Enumerable
        //            .Range(0, pair.Value.Count) //не генерируем для последней вершины, т.к. нужен слабо связный граф
        //            .Aggregate((ff, ss) =>
        //            {
        //                while (pair.Value.Items.Count < pair.Value.Count)
        //                {
        //                    var addedVertex = VerticesSet[RandomGenerator.Next(VerticesSet.Count)];
        //                    if (!pair.Value.Items.Contains(addedVertex) && !addedVertex.Equals(pair.Key))
        //                    {
        //                        _ = pair.Value.Items.Add(addedVertex);

        //                        if (!MapVertexAndLists[addedVertex].Items.Contains(pair.Key))
        //                        {
        //                            _ = MapVertexAndLists[addedVertex].Items.Add(pair.Key);
        //                        }
        //                    }
        //                }
        //                return ss;
        //            });
        //    }
        //}

        //private static void InitializeInCoherentMapCore()
        //{
        //    var skipVertex = VerticesSet[RandomGenerator.Next(VerticesSet.Count)];

        //    foreach (var pair in MapVertexAndLists)
        //    {
        //        if (pair.Key.Equals(skipVertex))
        //        {
        //            continue;
        //        }

        //        _ = Enumerable
        //            .Range(0, pair.Value.Count + 1)
        //            .Aggregate((ff, ss) =>
        //            {
        //                while (pair.Value.Items.Count < pair.Value.Count)
        //                {
        //                    var addedVertex = VerticesSet[RandomGenerator.Next(VerticesSet.Count)];

        //                    if (!addedVertex.Equals(skipVertex) && !pair.Value.Items.Contains(addedVertex) && !addedVertex.Equals(pair.Key))
        //                    {
        //                        _ = pair.Value.Items.Add(addedVertex);

        //                        if (!MapVertexAndLists[addedVertex].Items.Contains(pair.Key))
        //                        {
        //                            _ = MapVertexAndLists[addedVertex].Items.Add(pair.Key);
        //                        }
        //                    }
        //                }
        //                return ss;
        //            });
        //    }
        //}

        //private static UnorientedEdgeWithWeightGraph<TValue> Build()
        //{
        //    static IEnumerable<EdgeViewItem<TValue>> GetEdges()
        //    {
        //        var edges = new HashSet<EdgeViewItem<TValue>>();
        //        foreach(var pair in MapVertexAndLists)
        //        {
        //            foreach (var item in pair.Value.Items)
        //            {
        //                var weight = GenerateWeight();

        //                if (!edges.Any(e => e.First.Equals(pair.Key) && e.Second.Equals(item)))
        //                {
        //                    edges.Add(new EdgeViewItem<TValue>(pair.Key, item, weight));
        //                }

        //                if (MapVertexAndLists[item].Items.TryGetValue(pair.Key, out var elem))
        //                {
        //                    if (!edges.Any(e => e.First.Equals(item) && e.Second.Equals(elem)))
        //                    {
        //                        edges.Add(new EdgeViewItem<TValue>(item, elem, weight));
        //                    }
        //                }
        //            }
        //        }
        //        return edges;
        //    }

        //    return new UnorientedEdgeWithWeightGraph<TValue>(
        //        new EdgesWithWeightView<TValue>(
        //            GetEdges()));
        //}
    }
}