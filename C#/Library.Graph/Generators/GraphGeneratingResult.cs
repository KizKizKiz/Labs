using System;
using System.Linq;

using Library.Graph.Types;

namespace Library.Graph.Generators
{
    public sealed class GraphGeneratingResult<TGraph, TViewItem, TValue>
        where TValue : notnull
        where TViewItem : IGraphViewItem<TValue>
        where TGraph : IGraph<TViewItem, TValue>
    {
        public TGraph Graph { get; }

        public GraphGeneratingResult(TGraph graph)
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            if (!graph.Items.Any())
            {
                throw new ArgumentException("The items collection is empty.", nameof(graph.Items));
            }
            Graph = graph;
        }
    }
}