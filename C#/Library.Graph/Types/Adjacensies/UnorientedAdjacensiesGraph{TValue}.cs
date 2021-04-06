using System;

using Library.Graph.ConvertibleTypes;
using Library.Graph.Generators;
using Library.Graph.Views;

namespace Library.Graph.Types.Adjacensies
{
    /// <summary>
    /// Представляет реализацию неориентированного графа на списках смежности.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public sealed class UnorientedAdjacensiesGraph<TValue> : ImportableExportableAdjacensiesGraph<TValue>
        where TValue : IStringConvertible<TValue>, new()
    {
        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="view">Представления ребер на списках смежности.</param>
        /// <param name="edgeType">Тип ребер графа.</param>
        public UnorientedAdjacensiesGraph(AdjacensiesView<TValue> view)
            : base(view)
        { }

        public UnorientedAdjacensiesGraph(ViewGeneratingResult<TValue> viewGeneratingResult)
            : this(viewGeneratingResult?.View ?? throw new ArgumentNullException(nameof(viewGeneratingResult)))
        { }
    }
}