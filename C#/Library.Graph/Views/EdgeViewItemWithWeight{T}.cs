using System;

namespace Library.GraphTypes.Views
{
    public readonly struct EdgeViewItemWithWeight<TValue> : IEdgeViewItem<TValue>
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
    }
}