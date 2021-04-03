using System;

using Library.Graph.ConvertibleTypes;
using Library.Graph.Generators;
using Library.Graph.Views;

namespace Library.Graph.Types
{
    /// <summary>
    /// Представляет базовую реализацию всех типов графов, основанных на массивах ребер.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public abstract class EdgeWithWeightGraph<TValue> : ImportableExportableGraph<EdgesWithWeightView<TValue>, EdgeViewItem<TValue>, TValue>
        where TValue : IStringConvertible<TValue>
    {
        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="view">Представление графа на основе ребер.</param>
        /// <param name="edgeType">Тип ребер графа.</param>
        public EdgeWithWeightGraph(EdgesWithWeightView<TValue> view)
            : base(view)
        { }

        public EdgeWithWeightGraph(ViewGeneratingResult<EdgesWithWeightView<TValue>, EdgeViewItem<TValue>, TValue> viewGeneratingResult)
            : base(viewGeneratingResult)
        { }
    }
}