using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Library.Graph.Generators.Options;

namespace Library.Graph.Types
{
    public abstract class Graph<TViewItem, TValue> : IGraph<TViewItem, TValue>
        where TViewItem : IGraphViewItem<TValue>
        where TValue : notnull
    {
        public ConnectivityType ConnectivityType { get; }

        public bool IsOriented { get; }

        public IReadOnlyList<TViewItem> Items { get; }

        public IReadOnlyList<TValue> Vertices { get; }

        public Graph(
            IEnumerable<TViewItem> items, 
            IEnumerable<TValue> vertices, 
            bool isOriented)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            if (vertices is null)
            {
                throw new ArgumentNullException(nameof(vertices));
            }
            if (!items.Any())
            {
                throw new ArgumentException("The items collection is empty.", nameof(items));
            }
            if (!vertices.Any())
            {
                throw new ArgumentException("The vertices collection is empty.", nameof(vertices));
            }
            Vertices = vertices.ToList();
            Items = items.ToList();

            if (isOriented != DeterminateIsDirected())
            {
                throw new ArgumentException($"Graph is not a '{(isOriented ? "Directed" : "Undirected")}'.", nameof(isOriented));
            }
            IsOriented = isOriented;
        }

        public Graph(
            IEnumerable<TViewItem> items,
            IEnumerable<TValue> vertices,
            bool isOriented,
            ConnectivityType connectivityType)
            : this(items, vertices, isOriented)
        {
            if (!Enum.IsDefined(connectivityType))
            {
                throw new InvalidEnumArgumentException(nameof(connectivityType), (int)connectivityType, typeof(ConnectivityType));
            }
            if (!isOriented && connectivityType == ConnectivityType.StronglyConnected)
            {
                throw new ArgumentException($"The undirected graph can be only '{ConnectivityType.NotConnected}' or {ConnectivityType.WeaklyOrJustConnected}.");
            }

            ConnectivityType = DeterminateConnectivityType();
            if (connectivityType != ConnectivityType)
            {
                throw new ArgumentException($"Connectivity type mismatch.\nReceived '{connectivityType}', but detected '{ConnectivityType}'.", nameof(connectivityType));
            }
        }

        public Graph(
            IEnumerable<TViewItem> items,
            IEnumerable<TValue> vertices,
            bool isOriented,
            ConnectivityType connectivityType,
            bool needToValidateConnectivityType = false)
            : this(items, vertices, isOriented)
        {
            if (!Enum.IsDefined(connectivityType))
            {
                throw new InvalidEnumArgumentException(nameof(connectivityType), (int)connectivityType, typeof(ConnectivityType));
            }
            if (!isOriented && connectivityType == ConnectivityType.StronglyConnected)
            {
                throw new ArgumentException($"The undirected graph can be only '{ConnectivityType.NotConnected}' or {ConnectivityType.WeaklyOrJustConnected}.");
            }

            ConnectivityType = connectivityType;
            if (needToValidateConnectivityType)
            {
                var determinateConnectivity = DeterminateConnectivityType();
                if (connectivityType != determinateConnectivity)
                {
                    throw new ArgumentException($"Connectivity type mismatch.\nReceived '{connectivityType}', but detected '{determinateConnectivity}'.", nameof(connectivityType));
                }
            }
        }

        protected abstract bool DeterminateIsDirected();

        protected abstract ConnectivityType DeterminateConnectivityType();
    }
}