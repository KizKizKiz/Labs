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
            public IEnumerable<int> Items { get; }
            public AdjacencyListItem(IEnumerable<int> items)
            {
                Items = items ?? throw new ArgumentNullException(nameof(items));
            }

            public IEnumerator<int> GetEnumerator() => Items.GetEnumerator();
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
        public static UnOrientedGraph Generate(int vertices)
        {
            if (vertices <= 0)
            {
                throw new ArgumentException($"The \"{nameof(vertices)}\" must be greater than zero.", nameof(vertices));
            }
            if (MAX_VERTEXES < vertices)
            {
                throw new ArgumentOutOfRangeException(nameof(vertices), $"The \"{nameof(vertices)}\" must be less or equal to {MAX_VERTEXES}.");
            }

            return GenerateCore(vertices);
        }

        private static UnOrientedGraph GenerateCore(int vertices)
        {
            int MinEdges() => vertices / 2;

            var adjList = Enumerable.Range(0, vertices)
                .Select(_ => new List<int>())
                .ToList();

            var minEdges = MinEdges();
            var index = 0;
            foreach (var list in adjList)
            {
                var elements = _random.Next(vertices);
                if (elements < minEdges)
                {
                    elements = minEdges;
                }
                _ = Enumerable
                    .Range(0, elements)
                    .Aggregate((s, r) =>
                    {
                        while (true)
                        {
                            var addedElement = _random.Next(vertices);
                            if (!list.Contains(addedElement) && addedElement != index)
                            {
                                list.Add(addedElement);
                                break;
                            }
                        }
                        return r;
                    });
                index++;
            }

            return new UnOrientedGraph(adjList.Select(item => new AdjacencyListItem(item)));
        }

        private static Random _random = new Random();
    }
}