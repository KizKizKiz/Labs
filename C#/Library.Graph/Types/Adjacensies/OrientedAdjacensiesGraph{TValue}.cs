using System;

using Library.Graph.Views;
using Library.Graph.Generators;
using Library.Graph.ConvertibleTypes;

namespace Library.Graph.Types.Adjacensies
{
    /// <summary>
    /// Представляет реализацию ориентированного графа на списках смежности.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public class OrientedAdjacensiesGraph<TValue> : ImportableExportableAdjacensiesGraph<TValue>
        where TValue : notnull, IStringConvertible<TValue>, new()
    {
        public OrientedAdjacensiesGraph(AdjacensiesView<TValue> view)
            : base(view)
        {
        }

        public OrientedAdjacensiesGraph(ViewGeneratingResult<TValue> viewGeneratingResult)
            : this(viewGeneratingResult?.View ?? throw new ArgumentNullException(nameof(viewGeneratingResult)))
        { }
    }
}