using System;
using System.Collections.Generic;

using Library.Graph.ConvertibleTypes;
using Library.Graph.Types;
using Library.Graph.Operations;
using Library.Graph.Views;
using Library.Graph.Types.Adjacensies;
using Library.Graph.Types.Edges;

namespace Library.Graph.Extensions
{
    public static class Extensions
    {
        public static IEnumerable<TValue> SetupBFSWalking<TValue>(
            this UnorientedAdjacensiesGraph<TValue> graph)
            where TValue: IStringConvertible<TValue>, new()
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            return new BFSIterator<TValue>(graph);
        }

        public static IEnumerable<TValue> SetupDFSWalking<TValue>(
            this UnorientedAdjacensiesGraph<TValue> graph)
            where TValue : IStringConvertible<TValue>, new()
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            return new DFSIterator<TValue>(graph);
        }

        public static IEnumerable<EdgesViewItem<TValue>> SetupMSTWalking<TValue>(
            this OrientedEdgesGraph<TValue> graph)
            where TValue : IStringConvertible<TValue>, new()
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            return new MinimumSpanningTreeIterator<TValue>(graph);
        }

        public static IEnumerable<IEnumerable<TValue>> SetupSCCWalking<TValue>(
            this OrientedAdjacensiesGraph<TValue> graph)
            where TValue : IStringConvertible<TValue>, new()
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            return new StronglyConnectedComponentsIterator<TValue>(graph);
        }
    }
}