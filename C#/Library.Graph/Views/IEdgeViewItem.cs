using System;

namespace Library.GraphTypes.Views
{
    public interface IEdgeViewItem<TValue> : IGraphViewItem<TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
    }
}