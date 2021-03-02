using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Library.Views;

namespace Library.GraphTypes
{
    /// <summary>
    /// Представляет реализацию ориентированного графа на массиве ребер.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public sealed class OrientedEdgeWithWeightGraph<TValue> : EdgeWithWeightGraph<TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>, IStringConvertible<TValue>, new()
    {
        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="edgeType">Тип ребер графа.</param>
        public OrientedEdgeWithWeightGraph(EdgeType edgeType)
            : base(edgeType) { }

        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="view">Представления ребер на массиве ребер.</param>
        /// <param name="edgeType">Тип ребер графа.</param>
        public OrientedEdgeWithWeightGraph(OrientedEdgeWithWeightView<TValue> view, EdgeType edgeType)
            : base(view, edgeType)
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

        protected override async Task ExportCoreAsync(string fileName)
        {
            using var writer = new StreamWriter(new FileStream(fileName, FileMode.Create), Encoding.Unicode);

            writer.WriteLine($"Label;Source;Target;Weight;Type");
            var i = 0;
            foreach (var item in View.Items)
            {
                await writer.WriteLineAsync(string.Join(';', $"v{i++}", item.First, item.Second, item.Weight, EdgeType));
            }
        }

        protected override async Task ImportCoreAsync(string fileName)
        {
            using var reader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read), Encoding.Unicode);
            _ = await reader.ReadLineAsync();

            var edges = new List<EdgeViewItemWithWeight<TValue>>();
            while(!reader.EndOfStream)
            {
                edges.Add(ParseLine(await reader.ReadLineAsync()));
            }
            View = new OrientedEdgeWithWeightView<TValue>(edges);
        }

        private EdgeViewItemWithWeight<TValue> ParseLine(string edge)
        {
            var blocks = edge.Split(';');
            if (blocks.Length != 5)
            {
                throw new InvalidOperationException("Received invalid line that doesn't match to 'Source;Target;Weight;Type' pattern.");
            }
            var entity = new TValue();

            var firstVertex = entity.ConvertFromString(blocks[1]);
            var secondVertex = entity.ConvertFromString(blocks[2]);
            var weight = Convert.ToDouble(blocks[3]);
            var edgeType = Enum.Parse<EdgeType>(blocks[4]);

            if (edgeType != EdgeType.Directed)
            {
                throw new InvalidOperationException($"Received invalid edge type '{edgeType}'.");
            }

            return new EdgeViewItemWithWeight<TValue>(firstVertex, secondVertex, weight);
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
                    GetIterator().SelectMany(seq => seq).Distinct().ToList()),
                    EdgeType.Directed);
        }
    }
}