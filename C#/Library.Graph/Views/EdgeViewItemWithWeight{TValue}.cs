using System;

namespace Library.Views
{
    /// <summary>
    /// Представляет элемент представления в виде списка смежности.
    /// </summary>
    /// <typeparam name="TView">Тип представления графа.</typeparam>
    public readonly struct EdgeViewItemWithWeight<TValue> : IEdgeViewItem<TValue>, IEquatable<EdgeViewItemWithWeight<TValue>>, IComparable<EdgeViewItemWithWeight<TValue>>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        public TValue First { get; }
        public TValue Second { get; }
        public double Weight { get; }
        public EdgeViewItemWithWeight(TValue first, TValue second, double weight)
        {
            First = first;
            Second = second;
            Weight = weight;
        }

        public readonly bool Equals(EdgeViewItemWithWeight<TValue> other)
            => First.Equals(other.First) && Second.Equals(other.Second) && Weight == other.Weight;

        public int CompareTo(EdgeViewItemWithWeight<TValue> other)
        {
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
        {
            return $"{First} -> {Second} ({Weight})";
        }
    }
}