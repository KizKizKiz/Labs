using System;
using System.Linq;
using System.Threading.Tasks;

using Library.Views;

namespace Library.GraphTypes
{
    /// <summary>
    /// Представляет реализацию неориентированного графа на списках смежности.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public sealed class UnorientedAdjacensiesGraph<TValue> : ImportableExportableGraph<UnorientedAdjacensiesView<TValue>, AdjacensyViewItem<TValue>, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>, IStringConvertible<TValue>
    {
        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="edgeType">Тип ребер графа.</param>
        public UnorientedAdjacensiesGraph(EdgeType edgeType)
            : base(edgeType) { }

        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="view">Представления ребер на списках смежности.</param>
        /// <param name="edgeType">Тип ребер графа.</param>
        public UnorientedAdjacensiesGraph(UnorientedAdjacensiesView<TValue> view, EdgeType edgeType)
            : base(view, edgeType)
        { }

        /// <summary>
        /// Возвращает сгенерированный неориентированный связный граф с количеством вершин равным <paramref name="vertices"/>
        /// и средней степенью вершин равной <paramref name="meanCohesionPower"/>.
        /// </summary>
        /// <param name="vertices">Количество вершин.</param>
        /// <param name="meanCohesionPower">Средняя степень вершин.</param>
        /// <param name="factory">Фабрика элементов.</param>
        public static UnorientedAdjacensiesGraph<TValue> Generate(int vertices, int meanCohesionPower, Func<TValue> factory)
        {
            InitializeVerticesSetAndMap(vertices, meanCohesionPower, factory);
            InitializeCoherentMapCore();
            return Build();
        }

        /// <summary>
        /// Возвращает сгенерированный неориентированный несвязный граф с количеством вершин равным <paramref name="vertices"/>
        /// и средней степенью вершин равной <paramref name="meanCohesionPower"/>.
        /// </summary>
        /// <param name="vertices">Количество вершин.</param>
        /// <param name="meanCohesionPower">Средняя степень вершин.</param>
        /// <param name="factory">Фабрика элементов.</param>
        public static UnorientedAdjacensiesGraph<TValue> GenerateInCoherent(int vertices, int meanCohesionPower, Func<TValue> factory)
        {
            InitializeVerticesSetAndMap(vertices, meanCohesionPower, factory);
            InitializeInCoherentMapCore();
            return Build();
        }

        protected override Task ExportCoreAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        protected override Task ImportCoreAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        private static void InitializeCoherentMapCore()
        {
            foreach (var pair in MapVertexAndLists)
            {
                _ = Enumerable
                    .Range(0, pair.Value.Count + 1)
                    .Aggregate((ff, ss) =>
                    {
                        while (pair.Value.Items.Count < pair.Value.Count)
                        {
                            var addedVertex = VerticesSet[RandomGenerator.Next(VerticesSet.Count)];

                            if (!pair.Value.Items.Contains(addedVertex) && !addedVertex.Equals(pair.Key))
                            {
                                _ = pair.Value.Items.Add(addedVertex);

                                if (!MapVertexAndLists[addedVertex].Items.Contains(pair.Key))
                                {
                                    _ = MapVertexAndLists[addedVertex].Items.Add(pair.Key);
                                }
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

                                if (!MapVertexAndLists[addedVertex].Items.Contains(pair.Key))
                                {
                                    _ = MapVertexAndLists[addedVertex].Items.Add(pair.Key);
                                }
                            }
                        }
                        return ss;
                    });
            }
        }

        private static UnorientedAdjacensiesGraph<TValue> Build()
            => new UnorientedAdjacensiesGraph<TValue>(
                new UnorientedAdjacensiesView<TValue>(
                    MapVertexAndLists
                    .Select(kv => new AdjacensyViewItem<TValue>(kv.Key, kv.Value.Items))
                    .ToList()),
                    EdgeType.Undirected);
    }
}