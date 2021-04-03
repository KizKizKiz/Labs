using System;
using System.Diagnostics.CodeAnalysis;

namespace Library.Graph.Views
{
    /// <summary>
    /// Представляет элемент представления в виде списка смежности.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов ребра.</typeparam>
    public sealed class EdgeViewItem<TValue> : IEdgeViewItem<TValue>, IEquatable<EdgeViewItem<TValue>>, IComparable<EdgeViewItem<TValue>>
    {
        [NotNull]
        public TValue First { get; }

        [NotNull]
        public TValue Second { get; }

        public double? Weight { get; }

        public EdgeViewItem(TValue first, TValue second)
        {
            First = first ?? throw new ArgumentNullException(nameof(first));
            Second = second ?? throw new ArgumentNullException(nameof(second));
            Weight = null;
        }

        public EdgeViewItem(TValue first, TValue second, double weight)
            :this(first, second)
        {
            Weight = weight;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(First, Second, Weight);
        }

        public int CompareTo(EdgeViewItem<TValue>? other)
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
        

        public bool Equals(EdgeViewItem<TValue>? y)
            => y is not null && First.Equals(y.First) && Second.Equals(y.Second) && Weight == y.Weight;
    }
}