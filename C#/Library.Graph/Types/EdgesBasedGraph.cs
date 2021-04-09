using System.Collections.Generic;

using Library.Graph.Converter;
using Library.Graph.Generators;
using Library.Graph.Generators.Options;
using Library.Graph.Helpers;

namespace Library.Graph.Types
{
    public class EdgesBasedGraph<TValue> : Graph<EdgesViewItem<TValue>, TValue>
        where TValue : notnull
    {
        public bool IsWeighted { get; }

        public EdgesBasedGraph(
            IEnumerable<EdgesViewItem<TValue>> items, 
            IEnumerable<TValue> vertices, 
            bool isWeighted,
            bool isOriented,
            ConnectivityType connectivityType,
            bool needToValidateConnectivityType = false)
            : base(items, vertices, isOriented, connectivityType, needToValidateConnectivityType)
        {
            IsWeighted = isWeighted;
        }

        public EdgesBasedGraph(
            IEnumerable<EdgesViewItem<TValue>> items,
            IEnumerable<TValue> vertices,
            bool isWeighted,
            bool isOriented)
            : base(items, vertices, isOriented)
        {
            IsWeighted = isWeighted;
        }

        protected override ConnectivityType DeterminateConnectivityType()
            => _graphDeterminant.DeterminateConnectivity(this);

        protected override bool DeterminateIsDirected()
            => _graphDeterminant.DeterminateIsDirected(this);

        private static readonly GraphDeterminantHelper<TValue> _graphDeterminant
            = new GraphDeterminantHelper<TValue>(
                new GraphConverter(DefaultRandomizer.Randomizer));
    }
}