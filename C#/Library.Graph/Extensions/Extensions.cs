using System;
using System.Collections.Generic;

using Library.Graph.ConvertibleTypes;
using Library.Graph.Types;
using Library.Graph.Operations;
using Library.Graph.Views;

namespace Library.Graph.Extensions
{
    public static class Extensions
    {
        public static IEnumerable<TValue> SetupBFSWalking<TValue>(
            this UnorientedAdjacensiesGraph<TValue> graph)
            where TValue: IStringConvertible<TValue>
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            return new BFSIterator<TValue>(graph);
        }

        public static IEnumerable<TValue> SetupDFSWalking<TValue>(
            this UnorientedAdjacensiesGraph<TValue> graph)
            where TValue : IStringConvertible<TValue>
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            return new DFSIterator<TValue>(graph);
        }

        public static IEnumerable<EdgeViewItem<TValue>> SetupMSTWalking<TValue>(
            this OrientedEdgeWithWeightGraph<TValue> graph)
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