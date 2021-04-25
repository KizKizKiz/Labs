using System;
using System.Collections.Generic;
using System.Linq;

using Library.Graph.Generators.Options;

namespace Library.Graph.Types
{
    /// <summary>
    /// Представляет двудольный граф с элементами типа <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public sealed class BipartiteGraph<TValue> : Graph<TValue>
        where TValue : notnull, IEqualityComparer<TValue>, IEquatable<TValue>
    {
        /// <summary>
        /// Левая доля.
        /// </summary>
        public IReadOnlyDictionary<TValue, AdjacensyEdgeItem<TValue>> LeftShare { get; private set; } = default!;

        /// <summary>
        /// Правая доля.
        /// </summary>
        public IReadOnlyDictionary<TValue, AdjacensyEdgeItem<TValue>> RightShare { get; private set; } = default!;

        /// <summary>
        /// Флаг, является ли граф полносвязным с одинаковым количество элементов в долях.
        /// </summary>
        public bool AreSharesHaveSameSizeAndFullyConnected { get; private set; }

        /// <summary>
        /// Конструктор двудольного графа.
        /// </summary>
        /// <param name="items">Список смежностей, который состоит из ребер.</param>
        /// <param name="vertices">Множество вершин.</param>
        public BipartiteGraph(
            IEnumerable<AdjacensyEdgeItem<TValue>> items,
            IEnumerable<TValue> vertices)
            : base(items, vertices, false, ConnectivityType.WeaklyOrJustConnected, true)
        {
            VerifyBipartiteAndSetShares();
            VerifyAreFullyConnectedAndHaveSameSize();
        }

        private void VerifyAreFullyConnectedAndHaveSameSize()
        {
            if (RightShare.Count == LeftShare.Count
                && LeftShare.All(c => c.Value.Items.Select(v => v.Target).ToHashSet().Count == RightShare.Count))
            {
                AreSharesHaveSameSizeAndFullyConnected = true;
            }
        }

        private void VerifyBipartiteAndSetShares()
        {
            var mapVertexAndIsColored = Items.ToDictionary(
                v => v.Key,
                v => (Color: new byte?(), Items: v.Value));

            DFS(mapVertexAndIsColored.First().Key, 1);

            LeftShare = mapVertexAndIsColored
                .Where(c => c.Value.Color == 1)
                .ToDictionary(c => c.Key, c => c.Value.Items);

            RightShare = mapVertexAndIsColored
                .Where(c => c.Value.Color == 2)
                .ToDictionary(c => c.Key, c => c.Value.Items);

            void DFS(TValue vertex, byte color)
            {
                mapVertexAndIsColored[vertex] = (color, mapVertexAndIsColored[vertex].Items);

                foreach (var item in mapVertexAndIsColored[vertex].Items.Items.Select(c => c.Target))
                {
                    if (mapVertexAndIsColored[item].Color is null)
                    {
                        DFS(item, color == 1 ? (byte)2 : (byte)1);
                    }
                    else if (mapVertexAndIsColored[item].Color == color)
                    {
                        throw new ArgumentException("Graph is not bipartite.");
                    }
                }
            }
        }
    }
}
