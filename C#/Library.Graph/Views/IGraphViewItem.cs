using System;

namespace Library.GraphTypes.Views
{
    public interface IGraphViewItem<TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
    }
}