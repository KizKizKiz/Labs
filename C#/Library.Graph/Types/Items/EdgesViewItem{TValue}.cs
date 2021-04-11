using System;
using System.Diagnostics.CodeAnalysis;

namespace Library.Graph.Types
{
    /// <summary>
    /// Представляет элемент графа в виде ребра.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов ребра.</typeparam>
    public sealed class EdgeItem<TValue> : IGraphViewItem<TValue>, IEquatable<EdgeItem<TValue>>, IComparable<EdgeItem<TValue>>
    {
        /// <summary>
        /// Вершина - начало ребра.
        /// </summary>
        [NotNull]
        public TValue Source { get; }

        /// <summary>
        /// Вершина - конец ребра.
        /// </summary>
        [NotNull]
        public TValue Target { get; }

        /// <summary>
        /// Вес ребра (может быть не задан).
        /// </summary>
        public double? Weight { get; }

        /// <summary>
        /// Конструктор ребра.
        /// </summary>
        /// <param name="source">Вершина - начало ребра.</param>
        /// <param name="target">Вершина - конец ребра.</param>
        public EdgeItem(TValue source, TValue target)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Target = target ?? throw new ArgumentNullException(nameof(target));
        }

        /// <summary>
        /// Конструктор ребра.
        /// </summary>
        /// <param name="source">Вершина - начало ребра.</param>
        /// <param name="target">Вершина - конец ребра.</param>
        /// <param name="target">Вес ребра.</param>
        public EdgeItem(TValue source, TValue target, double weight)
            : this(source, target)
        {
            Weight = weight;
        }

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Source, Target, Weight);

        /// <inheritdoc/>
        public int CompareTo(EdgeItem<TValue>? other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            if (Weight > other.Weight)
            {
                return 1;
            }
            else if (Weight < other.Weight)
            {
                return -1;
            }
            return 0;
        }

        /// <inheritdoc/>
        public override string ToString()
            => $"{Source} -> {Target} ({Weight})";

        /// <inheritdoc/>
        public bool Equals(EdgeItem<TValue>? other)
            => other is not null && Source.Equals(other.Source) && Target.Equals(other.Target) && Weight == other.Weight;
    }
}