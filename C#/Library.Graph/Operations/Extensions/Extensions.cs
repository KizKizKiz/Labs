using System;
using System.Collections.Generic;

using Library.Graph.Types;

namespace Library.Graph.Operations.Extensions
{
    public static class OperationsExtensions
    {
        public static IEnumerable<TValue> SetupBFSWalking<TValue>(
            this AdjacensiesBasedGraph<TValue> graph)
            where TValue : notnull, new()
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            return new BFSIterator<TValue>(graph);
        }

        public static IEnumerable<TValue> SetupDFSWalking<TValue>(
            this AdjacensiesBasedGraph<TValue> graph)
            where TValue : notnull, new()
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            return new DFSIterator<TValue>(graph);
        }

        public static IEnumerable<EdgesViewItem<TValue>> SetupMSTWalking<TValue>(
            this EdgesBasedGraph<TValue> graph)
            where TValue : notnull, new()
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            return new MinimumSpanningTreeIterator<TValue>(graph);
        }

        public static IEnumerable<IEnumerable<TValue>> SetupSCCWalking<TValue>(
            this AdjacensiesBasedGraph<TValue> graph)
            where TValue : notnull, new()
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            return new StronglyConnectedComponentsIterator<TValue>(graph);
        }
    }
}