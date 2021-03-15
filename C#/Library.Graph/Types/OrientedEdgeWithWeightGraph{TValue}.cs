using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using OfficeOpenXml;

using Library.Graph.ConvertibleTypes;
using Library.Graph.Views;

namespace Library.Graph.Types
{
    /// <summary>
    /// Представляет реализацию ориентированного графа на массиве ребер.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public sealed class OrientedEdgeWithWeightGraph<TValue> : EdgeWithWeightGraph<TValue>
        where TValue : IEquatable<TValue>, IStringConvertible<TValue>, new()
    {
        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="edgeType">Тип ребер графа.</param>
        public OrientedEdgeWithWeightGraph()
            : base(EdgeType.Directed) { }

        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="view">Представления ребер на массиве ребер.</param>
        /// <param name="edgeType">Тип ребер графа.</param>
        public OrientedEdgeWithWeightGraph(OrientedEdgeWithWeightView<TValue> view)
            : base(view, EdgeType.Directed)
        { }

        /// <summary>
        /// Возвращает сгенерированный слабо связный ориентированный граф с количеством вершин равным <paramref name="vertices"/>
        /// и средней степенью вершин равной <paramref name="meanCohesionPower"/>.
        /// </summary>
        /// <param name="vertices">Количество вершин.</param>
        /// <param name="meanCohesionPower">Средняя степень вершин.</param>
        /// <param name="factory">Фабрика элементов.</param>
        public static OrientedEdgeWithWeightGraph<TValue> GenerateWithWeakCohesion(int vertices, int meanCohesionPower, Func<TValue> factory)
        {
            InitializeVerticesSetAndMap(vertices, meanCohesionPower, factory);
            InitializeCoherentMapCore();
            return Build();
        }

        /// <summary>
        /// Возвращает сгенерированный несвязный ориентированный граф с количеством вершин равным <paramref name="vertices"/>
        /// и средней степенью вершин равной <paramref name="meanCohesionPower"/>.
        /// </summary>
        /// <param name="vertices">Количество вершин.</param>
        /// <param name="meanCohesionPower">Средняя степень вершин.</param>
        /// <param name="factory">Фабрика элементов.</param>
        public static OrientedEdgeWithWeightGraph<TValue> GenerateInCoherent(int vertices, int meanCohesionPower, Func<TValue> factory)
        {
            InitializeVerticesSetAndMap(vertices, meanCohesionPower, factory);
            InitializeInCoherentMapCore();
            return Build();
        }

        protected override async Task<string> ExportCoreAsync(string fileName)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage(new FileInfo(fileName));
            var worksheet = package.Workbook.Worksheets.Add("GRAPH_DUMP");

            worksheet.Cells[1, 1].Value = "Source";
            worksheet.Cells[1, 2].Value = "Target";
            worksheet.Cells[1, 3].Value = "Weight";
            worksheet.Cells[1, 4].Value = "Type";
            worksheet.Cells[1, 5].Value = "Label";

            for (int i = 0; i < View.Items.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = View.Items[i].First.ToString();
                worksheet.Cells[i + 2, 2].Value = View.Items[i].Second.ToString();
                worksheet.Cells[i + 2, 3].Value = View.Items[i].Weight;
                worksheet.Cells[i + 2, 4].Value = EdgeType;
                worksheet.Cells[i + 2, 5].Value = $"From '{View.Items[i].First}' to '{View.Items[i].Second}'";
            }

            await package.SaveAsync();

            return fileName;
        }

        protected override async Task ImportCoreAsync(string fileName)
        {
            await Task.Yield();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage(new FileInfo(fileName));
            var worksheet = package.Workbook.Worksheets[0];

            var edges = new List<EdgeViewItemWithWeight<TValue>>();
            var vertexSet = new HashSet<TValue>();
            for (int i = 2; i <= worksheet.Dimension.End.Row; i++) // int i = 1 Skip headers
            {
                var entity = new TValue();
                var edge = new EdgeViewItemWithWeight<TValue>(
                    entity.ConvertFromString(worksheet.Cells[i, 1].Value.ToString()),
                    entity.ConvertFromString(worksheet.Cells[i, 2].Value.ToString()),
                    Convert.ToDouble(worksheet.Cells[i, 3].Value.ToString()));
                _ = vertexSet.Add(edge.First);
                _ = vertexSet.Add(edge.Second);
                edges.Add(edge);
            }
            VerticesSet = vertexSet.ToList();
            View = new OrientedEdgeWithWeightView<TValue>(edges);
        }

        private static void InitializeCoherentMapCore()
        {
            _ = MapVertexAndLists
                .Aggregate((f, s) =>
                {
                    s.Value.Items.Add(f.Key);
                    return s;
                });
            foreach (var pair in MapVertexAndLists)
            {
                _ = Enumerable
                    .Range(0, pair.Value.Count) //не генерируем для последней вершины, т.к. нужен слабо связный граф
                    .Aggregate((ff, ss) =>
                    {
                        while (pair.Value.Items.Count < pair.Value.Count)
                        {
                            var addedVertex = VerticesSet[RandomGenerator.Next(VerticesSet.Count)];

                            if (!pair.Value.Items.Contains(addedVertex) && !addedVertex.Equals(pair.Key))
                            {
                                _ = pair.Value.Items.Add(addedVertex);
                            }
                        }
                        return ss;
                    });
            }
        }

        private static void InitializeInCoherentMapCore()
        {
            var skipVertex = VerticesSet[RandomGenerator.Next(VerticesSet.Count)];

            foreach (var pair in MapVertexAndLists)
            {
                if (pair.Key.Equals(skipVertex))
                {
                    continue;
                }

                _ = Enumerable
                    .Range(0, pair.Value.Count + 1)
                    .Aggregate((ff, ss) =>
                    {
                        while (pair.Value.Items.Count < pair.Value.Count)
                        {
                            var addedVertex = VerticesSet[RandomGenerator.Next(VerticesSet.Count)];

                            if (!addedVertex.Equals(skipVertex) && !pair.Value.Items.Contains(addedVertex) && !addedVertex.Equals(pair.Key))
                            {
                                _ = pair.Value.Items.Add(addedVertex);
                            }
                        }
                        return ss;
                    });
            }
        }

        private static OrientedEdgeWithWeightGraph<TValue> Build()
        {
            static IEnumerable<IEnumerable<EdgeViewItemWithWeight<TValue>>> GetIterator()
            {
                foreach(var pair in MapVertexAndLists)
                {
                    yield return pair.Value.Items.Select(val => new EdgeViewItemWithWeight<TValue>(pair.Key, val, GenerateWeight()));
                }
            }

            return new OrientedEdgeWithWeightGraph<TValue>(
                new OrientedEdgeWithWeightView<TValue>(
                    GetIterator().SelectMany(seq => seq).ToList()));
        }
    }
}