using System;
using System.Linq;
using System.Threading.Tasks;

using Library.Graph.ConvertibleTypes;
using Library.Graph.Generators;
using Library.Graph.Views;

namespace Library.Graph.Types
{
    /// <summary>
    /// Представляет реализацию неориентированного графа на списках смежности.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public sealed class UnorientedAdjacensiesGraph<TValue> : ImportableExportableGraph<AdjacensiesView<TValue>, AdjacensyViewItem<TValue>, TValue>
        where TValue : IStringConvertible<TValue>
    {
        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="view">Представления ребер на списках смежности.</param>
        /// <param name="edgeType">Тип ребер графа.</param>
        public UnorientedAdjacensiesGraph(AdjacensiesView<TValue> view)
            : base(view)
        { }

        public UnorientedAdjacensiesGraph(ViewGeneratingResult<AdjacensiesView<TValue>, AdjacensyViewItem<TValue>, TValue> viewGeneratingResult)
            : base(viewGeneratingResult)
        { }

        //private static void InitializeCoherentMapCore()
        //{
        //    foreach (var pair in MapVertexAndLists)
        //    {
        //        _ = Enumerable
        //            .Range(0, pair.Value.Count + 1)
        //            .Aggregate((ff, ss) =>
        //            {
        //                while (pair.Value.Items.Count < pair.Value.Count)
        //                {
        //                    var addedVertex = VerticesSet[RandomGenerator.Next(VerticesSet.Count)];

        //                    if (!pair.Value.Items.Contains(addedVertex) && !addedVertex.Equals(pair.Key))
        //                    {
        //                        _ = pair.Value.Items.Add(addedVertex);

        //                        if (!MapVertexAndLists[addedVertex].Items.Contains(pair.Key))
        //                        {
        //                            _ = MapVertexAndLists[addedVertex].Items.Add(pair.Key);
        //                        }
        //                    }
        //                }
        //                return ss;
        //            });
        //    }
        //}

        //private static void InitializeInCoherentMapCore()
        //{
        //    var skipVertex = VerticesSet[RandomGenerator.Next(VerticesSet.Count)];

        //    foreach (var pair in MapVertexAndLists)
        //    {
        //        if (pair.Key.Equals(skipVertex))
        //        {
        //            continue;
        //        }

        //        _ = Enumerable
        //            .Range(0, pair.Value.Count + 1)
        //            .Aggregate((ff, ss) =>
        //            {
        //                while (pair.Value.Items.Count < pair.Value.Count)
        //                {
        //                    var addedVertex = VerticesSet[RandomGenerator.Next(VerticesSet.Count)];

        //                    if (!addedVertex.Equals(skipVertex) && !pair.Value.Items.Contains(addedVertex) && !addedVertex.Equals(pair.Key))
        //                    {
        //                        _ = pair.Value.Items.Add(addedVertex);

        //                        if (!MapVertexAndLists[addedVertex].Items.Contains(pair.Key))
        //                        {
        //                            _ = MapVertexAndLists[addedVertex].Items.Add(pair.Key);
        //                        }
        //                    }
        //                }
        //                return ss;
        //            });
        //    }
        //}

        //private static UnorientedAdjacensiesGraph<TValue> Build()
        //    => new UnorientedAdjacensiesGraph<TValue>(
        //        new AdjacensiesView<TValue>(
        //            MapVertexAndLists
        //            .Select(kv => new AdjacensyViewItem<TValue>(kv.Key, kv.Value.Items))
        //            .ToList(), false));
    }
}