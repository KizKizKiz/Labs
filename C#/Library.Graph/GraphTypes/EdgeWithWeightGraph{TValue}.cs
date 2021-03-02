using System;

using Library.Views;

namespace Library.GraphTypes
{
    /// <summary>
    /// Представляет базовую реализацию всех типов графов, основанных на массивах ребер.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public abstract class EdgeWithWeightGraph<TValue> : ImportableExportableGraph<EdgesWithWeightView<TValue>, EdgeViewItemWithWeight<TValue>, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>, IStringConvertible<TValue>
    {
        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="edgeType">Тип ребер графа.</param>
        public EdgeWithWeightGraph(EdgeType edgeType)
            : base(edgeType) { }

        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="view">Представление графа на основе ребер.</param>
        /// <param name="edgeType">Тип ребер графа.</param>
        public EdgeWithWeightGraph(EdgesWithWeightView<TValue> view, EdgeType edgeType)
            : base(view, edgeType)
        { }

        protected static double GenerateWeight() 
            => RandomGenerator.Next(RANGE.MIN_WEIGHT, RANGE.MAX_WEIGHT);

        private static (int MIN_WEIGHT, int MAX_WEIGHT) RANGE = (-100, 100);
    }
}