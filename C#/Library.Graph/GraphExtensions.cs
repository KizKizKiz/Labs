using System;
using System.Linq;
using System.Collections.Generic;

using Library.Graph.ExampleConvertibleTypes;
using Library.Graph.Types;

namespace Library.Graph.Extensions
{
    public static class GraphExtensions
    {
        public static (double[,], IReadOnlyDictionary<int, IntConvertible>, IReadOnlyDictionary<int, IntConvertible>) ToDoubleDemensionalArray(this BipartiteGraph<IntConvertible> graph)
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            if (!graph.AreSharesHaveSameSizeAndFullyConnected)
            {
                throw new ArgumentException("Graph doesn't contain fully connected shares with same size.", nameof(graph));
            }
            if (!graph.IsWeighted)
            {
                throw new ArgumentException("Graph have to be weighted.", nameof(graph));
            }
            var result = new double[graph.LeftShare.Count + 1, graph.RightShare.Count + 1];
            var rowCount = 1;
            var columnCount = 1;
            var mapRowIndexAndVertex = graph.LeftShare.ToDictionary(_ => rowCount++, c => c.Key);
            var mapColumnIndexAndVertex = graph.RightShare.ToDictionary(_ => columnCount++, c => c.Key);

            foreach (var rowIndexAndVertex in mapRowIndexAndVertex)
            {
                foreach (var colIndexAndVertex in mapColumnIndexAndVertex)
                {
                    var edge = graph.Items[rowIndexAndVertex.Value].Items.First(c => c.Target.Equals(colIndexAndVertex.Value));
                    result[rowIndexAndVertex.Key, colIndexAndVertex.Key] = edge.Weight!.Value;
                }
            }
            return (result, mapRowIndexAndVertex, mapColumnIndexAndVertex);
        }
    }
}
