using System;

namespace Library.Graph.Views
{
    public readonly struct EdgeViewItem<TValue> : IEdgeViewItem<TValue>
        where TValue : IEquatable<TValue>
    {
        public TValue First { get; }
        public TValue Second { get; }
        public EdgeViewItem(TValue first, TValue second)
        {
            First = first;
            Second = second;
        }
    }
}