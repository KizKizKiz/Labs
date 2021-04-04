using Library.Graph.ConvertibleTypes;
using Library.Graph.Views;
using Library.Graph.Generators;

namespace Library.Graph.Types
{
    /// <summary>
    /// Представляет реализацию ориентированного графа на массиве ребер.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public sealed class OrientedEdgeWithWeightGraph<TValue> : EdgeWithWeightGraph<TValue>
        where TValue : IStringConvertible<TValue>, new()
    {
        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="view">Представления ребер на массиве ребер.</param>
        /// <param name="edgeType">Тип ребер графа.</param>
        public OrientedEdgeWithWeightGraph(EdgesWithWeightView<TValue> view)
            : base(view)
        { }

        public OrientedEdgeWithWeightGraph(ViewGeneratingResult<TValue> viewGeneratingResult)
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
        //            .Range(0, pair.Value.Count) //не генерируем для последней вершины, т.к. нужен слабо связный граф
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