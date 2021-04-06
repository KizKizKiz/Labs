using Library.Graph.ConvertibleTypes;
using Library.Graph.Views;
using Library.Graph.Generators;

namespace Library.Graph.Types.Edges
{
    /// <summary>
    /// Представляет реализацию ориентированного графа на массиве ребер.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public sealed class OrientedEdgesGraph<TValue> : EdgesGraph<TValue>
        where TValue : IStringConvertible<TValue>, new()
    {
        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="view">Представления ребер на массиве ребер.</param>
        /// <param name="edgeType">Тип ребер графа.</param>
        public OrientedEdgesGraph(EdgesView<TValue> view)
            : base(view)
        { }

        public OrientedEdgesGraph(ViewGeneratingResult<TValue> viewGeneratingResult, bool isWeighted)
            : this(ToEdgesViewFromResult(viewGeneratingResult, isWeighted))
        { }
    }
}