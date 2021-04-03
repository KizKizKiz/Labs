using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using OfficeOpenXml;

using Library.Graph.Views;
using Library.Graph.ConvertibleTypes;
using Library.Graph.Generators;

namespace Library.Graph.Types
{
    /// <summary>
    /// Представляет реализацию ориентированного графа на списках смежности.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public sealed class OrientedAdjacensiesGraph<TValue> : ImportableExportableGraph<AdjacensiesView<TValue>, AdjacensyViewItem<TValue>, TValue>
        where TValue : IStringConvertible<TValue>, new()
    {
        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="view">Представления ребер на списках смежности.</param>
        /// <param name="edgeType">Тип ребер графа.</param>
        public OrientedAdjacensiesGraph(AdjacensiesView<TValue> view)
            : base(view)
        {
        }

        public OrientedAdjacensiesGraph(ViewGeneratingResult<AdjacensiesView<TValue>, AdjacensyViewItem<TValue>, TValue> viewGeneratingResult)
            : base(viewGeneratingResult)
        { }

        //private static void InitializeCoherentMapCore()
        //{
        //    _ = MapVertexAndLists
        //        .Aggregate((f, s) =>
        //        {
        //            s.Value.Items.Add(f.Key);
        //            return s;
        //        });
        //    foreach (var pair in MapVertexAndLists)
        //    {
        //        _ = Enumerable
        //            .Range(0, pair.Value.Count)
        //            .Aggregate((ff, ss) =>
        //            {
        //                while (pair.Value.Items.Count < pair.Value.Count)
        //                {
        //                    var addedVertex = VerticesSet[RandomGenerator.Next(VerticesSet.Count)];

        //                    if (!pair.Value.Items.Contains(addedVertex) && !addedVertex.Equals(pair.Key))
        //                    {
        //                        _ = pair.Value.Items.Add(addedVertex);
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
        //                    }
        //                }
        //                return ss;
        //            });
        //    }
        //}
    }
}