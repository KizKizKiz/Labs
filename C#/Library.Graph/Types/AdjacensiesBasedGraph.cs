using System.Collections.Generic;

using Library.Graph.Generators;
using Library.Graph.Converter;
using Library.Graph.Generators.Options;
using Library.Graph.Helpers;

namespace Library.Graph.Types
{
    public class AdjacensiesBasedGraph<TValue> : Graph<AdjacensyGraphItem<TValue>, TValue>
        where TValue : notnull
    {
        public AdjacensiesBasedGraph(
            IEnumerable<AdjacensyGraphItem<TValue>> adjacensies,
            IEnumerable<TValue> vertices,
            bool isOriented)
            : base(adjacensies, vertices, isOriented)
        { }

        public AdjacensiesBasedGraph(
            IEnumerable<AdjacensyGraphItem<TValue>> adjacensies,
            IEnumerable<TValue> vertices,
            bool isOriented,
            ConnectivityType connectivity,
            bool needToValidateConnectivityType = false)
            : base(adjacensies, vertices, isOriented, connectivity, needToValidateConnectivityType)
        {
        }

        protected override ConnectivityType DeterminateConnectivityType()
            => _graphDeterminant.DeterminateConnectivity(this);

        protected override bool DeterminateIsDirected()
            => _graphDeterminant.DeterminateIsDirected(this);

        private static readonly GraphDeterminantHelper<TValue> _graphDeterminant
            = new(new GraphConverter(DefaultRandomizer.Randomizer));
    }
}