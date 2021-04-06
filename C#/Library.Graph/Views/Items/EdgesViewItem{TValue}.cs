using System;
using System.Diagnostics.CodeAnalysis;

namespace Library.Graph.Views
{
    /// <summary>
    /// Представляет элемент представления в виде списка смежности.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов ребра.</typeparam>
    public sealed class EdgesViewItem<TValue> : IGraphViewItem<TValue>, IEquatable<EdgesViewItem<TValue>>, IComparable<EdgesViewItem<TValue>>
    {
        [NotNull]
        public TValue First { get; }

        public TValue? Second { get; }

        public double? Weight { get; }

        public EdgesViewItem(TValue first)
        {
            First = first ?? throw new ArgumentNullException(nameof(first));
        }

        public EdgesViewItem(TValue first, TValue second)
        {
            First = first ?? throw new ArgumentNullException(nameof(first));
            Second = second ?? throw new ArgumentNullException(nameof(second));
        }

        public EdgesViewItem(TValue first, TValue second, double weight)
            :this(first, second)
        {
            Weight = weight;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(First, Second, Weight);
        }

        public int CompareTo(EdgesViewItem<TValue>? other)
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

        public override string ToString()
            => $"{First} -> {Second} ({(Weight.HasValue ? Weight.Value : "None")})";
        

        public bool Equals(EdgesViewItem<TValue>? y)
            => y is not null && First.Equals(y.First) && Second is not null && Second.Equals(y.Second) && Weight == y.Weight;
    }
}