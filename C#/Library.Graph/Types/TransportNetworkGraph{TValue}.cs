using System;
using System.Collections.Generic;
using System.Linq;

using Library.Graph.Generators.Options;

namespace Library.Graph.Types
{
    /// <summary>
    /// Представляет транспортную сеть с элементами типа <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов транспортной сети.</typeparam>
    public class TransportNetworkGraph<TValue> : Graph<TValue>
        where TValue : notnull
    {
        /// <summary>
        /// Вершина - исток.
        /// </summary>
        public TValue Source { get; private set; } = default!;

        /// <summary>
        /// Вершина - сток.
        /// </summary>
        public TValue Target { get; private set; } = default!;

        /// <summary>
        /// Конструктор транспортной сети.
        /// </summary>
        /// <param name="items">Элементы транспортной сети.</param>
        /// <param name="vertices">Множество вершин.</param>
        /// <param name="needToValidateConnectivityType">Флаг, нужно ли проверять тип связности графа (по умолчанию - не проверять).</param>
        public TransportNetworkGraph(
            IEnumerable<AdjacensyEdgeItem<TValue>> items,
            IEnumerable<TValue> vertices,
            bool needToValidateConnectivityType = false)
            : base(items, vertices, true, ConnectivityType.WeaklyOrJustConnected, needToValidateConnectivityType)
        {
            ValidateItemsAndSetSourceAndTarget();
        }

        private void ValidateItemsAndSetSourceAndTarget()
        {
            var targets = Items.Where(c => !c.Items.Any()).ToHashSet();
            if (targets.Count != 1)
            {
                throw new InvalidOperationException($"The algorithm support transport network graph with only one 'TARGET' (Detected: {targets.Count})");
            }
            var vertices = Items.Select(c => c.Items.Select(c => c.Target)).SelectMany(c => c).ToHashSet();
            if (vertices.Count != Vertices.Count - 1)
            {
                throw new InvalidOperationException($"The algorithm support transport network graph with only one 'SOURCE' (Detected: {vertices.Count})");
            }

            Target = targets.Single().Vertex;
            Source = Vertices.Except(vertices.Select(c => c)).Single();
        }
    }
}