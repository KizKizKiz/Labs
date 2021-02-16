using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

using CsvHelper;
using CsvHelper.Configuration;

namespace ConsoleApp.Graph
{
    /// <summary>
    /// Представляет неориентированный граф.
    /// </summary>
    public sealed class UnOrientedGraph
    {
        /// <summary>
        /// Представляет список смежности.
        /// </summary>
        public sealed class AdjacencyListItem
        {
            public int Vertex { get; }
            public IEnumerable<int> Adjacencies { get; }
            public AdjacencyListItem(int vertex, IEnumerable<int> adjacencies)
            {
                Adjacencies = adjacencies ?? throw new ArgumentNullException(nameof(adjacencies));
                Vertex = vertex;
            }

            public IEnumerator<int> GetEnumerator() => Adjacencies.GetEnumerator();
        }

        public sealed class DFSWalker : IEnumerable<int>
        {
            public DFSWalker(UnOrientedGraph graph)
            {
                _graph = graph;
            }
            /// <summary>
            /// Возвращает итератор обхода графа в глубину. 
            /// </summary>
            /// <returns>Итератор обхода графа.</returns>
            public IEnumerator<int> GetEnumerator()
                => SetupDFS().GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();

            private IEnumerable<int> SetupDFS()
            {
                var marked = new bool[_graph.AdjacensyList.Count];
                var passedVertices = new List<int>();

                WalkBasedOn(0);

                return passedVertices;

                void WalkBasedOn(int vertex)
                {
                    passedVertices.Add(vertex);
                    marked[vertex] = true;

                    foreach (var item in _graph.AdjacensyList[vertex])
                    {
                        if (!marked[item])
                        {
                            WalkBasedOn(item);
                        }
                    }
                }
            }
            private UnOrientedGraph _graph;
        }

        public sealed class BFSWalker : IEnumerable<int>
        {
            public BFSWalker(UnOrientedGraph graph)
            {
                _graph = graph;
            }
            /// <summary>
            /// Возвращает итератор обхода графа в ширину. 
            /// </summary>
            /// <returns>Итератор обхода графа.</returns>
            public IEnumerator<int> GetEnumerator()
                => SetupBFSCore().GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();

            private IEnumerable<int> SetupBFSCore()
            {
                var marked = new bool[_graph.AdjacensyList.Count];
                var verticesQueue = new Queue<int>();

                marked[0] = true;
                verticesQueue.Enqueue(0);
                while (verticesQueue.Any())
                {
                    var vertex = verticesQueue.Dequeue();

                    yield return vertex;

                    foreach (var item in _graph.AdjacensyList[vertex])
                    {
                        if (!marked[item])
                        {
                            marked[item] = true;
                            verticesQueue.Enqueue(item);
                        }
                    }
                }
            }

            private UnOrientedGraph _graph;
        }

        /// <summary>
        /// Максимальное количество вершин графа.
        /// </summary>
        public const int MAX_VERTEXES = 2000;

        /// <summary>
        /// Возвращает итератор обхода графа в глубину.
        /// </summary>
        /// <returns>Итератор обхода графа.</returns>
        public IEnumerable<int> SetupDFSWalking() => new DFSWalker(this);

        /// <summary>
        /// Возвращает итератор обхода графа в ширину.
        /// </summary>
        /// <returns>Итератор обхода графа.</returns>
        public IEnumerable<int> SetupBFSWalking() => new BFSWalker(this);

        /// <summary>
        /// Последовательность списков смежности.
        /// </summary>
        public IReadOnlyList<AdjacencyListItem> AdjacensyList { get; }

        public UnOrientedGraph(IEnumerable<AdjacencyListItem> adjList)
        {
            if (adjList is null)
            {
                throw new ArgumentNullException(nameof(adjList));
            }
            if (!adjList.Any())
            {
                throw new ArgumentException("The adjacensy list is empty.", nameof(adjList));
            }

            AdjacensyList = adjList.ToList();
        }

        public static UnOrientedGraph CreateFromCSV(string filePath)
        {
            if (filePath is null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException($"The '{nameof(filePath)}' is empty or has only whitespaces.");
            }

            var config = new CsvConfiguration(
                CultureInfo.CurrentCulture,
                delimiter: ",",
                hasHeaderRecord: false);

            using var streamReader = new StreamReader(filePath);
            using var csvReader = new CsvReader(streamReader, config);

            return new UnOrientedGraph(csvReader.GetRecords<AdjacencyListItem>());
        }

        /// <summary>
        /// Возвращает сгенерированный граф из количества вершин равной <paramref name="vertices"/>.
        /// </summary>
        /// <param name="vertices">Количество вершин.</param>
        public static UnOrientedGraph Generate(int vertices, int minCohesionPow)
        {
            if (vertices <= 0)
            {
                throw new ArgumentException($"The \"{nameof(vertices)}\" must be greater than zero.", nameof(vertices));
            }
            if (MAX_VERTEXES < vertices)
            {
                throw new ArgumentOutOfRangeException(nameof(vertices), $"The \"{nameof(vertices)}\" must be less or equal to {MAX_VERTEXES}.");
            }
            if (minCohesionPow >= vertices)
            {
                throw new ArgumentException($"The '{nameof(minCohesionPow)} must be less than {vertices}'.", nameof(minCohesionPow));
            }

            return GenerateCore(vertices, minCohesionPow);
        }

        private static UnOrientedGraph GenerateCore(int vertices, int minCohesionPow)
        {
            var mapVertexAndLists = Enumerable
                .Range(0, vertices)
                .ToDictionary(v => v,
                v =>
                {
                    var elements = Convert.ToInt32(_random.NextDouble() * (vertices - 1));
                    elements = elements < minCohesionPow ? minCohesionPow : elements;

                    return (elements, Items : new HashSet<int>(elements));
                });

            foreach (var pair in mapVertexAndLists)
            {
                _ = Enumerable
                    .Range(0, pair.Value.elements + 1)
                    .Aggregate((ff, ss) =>
                    {
                        while (pair.Value.Items.Count < pair.Value.elements)
                        {
                            var addedVertex = Convert.ToInt32(_random.NextDouble() * (vertices - 1));

                            if (!pair.Value.Items.Contains(addedVertex) && !addedVertex.Equals(pair.Key))
                            {
                                _ = pair.Value.Items.Add(addedVertex);

                                if (!mapVertexAndLists[addedVertex].Items.Contains(pair.Key))
                                {
                                    _ = mapVertexAndLists[addedVertex].Items.Add(pair.Key);
                                }
                            }
                        }
                        return ss;
                    });
            }

            return new UnOrientedGraph(mapVertexAndLists.Select(kv => new AdjacencyListItem(kv.Key, kv.Value.Items)));
        }
        private static Random _random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
    }
}