using Library.Graph.Views;
using Library.Graph.ConvertibleTypes;
using Library.Graph.Generators;

namespace Library.Graph.Types.Edges
{
    /// <summary>
    /// Представляет реализацию неориентированного графа на массиве ребер.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public sealed class UnorientedEdgeGraph<TValue> : EdgesGraph<TValue>
        where TValue : IStringConvertible<TValue>, new()
    {
        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="view">Представления ребер на массиве ребер.</param>
        public UnorientedEdgeGraph(EdgesView<TValue> view)
            : base(view)
        { }

        public UnorientedEdgeGraph(ViewGeneratingResult<TValue> viewGeneratingResult, bool isWeighted)
            : this(ToEdgesViewFromResult(viewGeneratingResult, isWeighted))
        { }
    }
}