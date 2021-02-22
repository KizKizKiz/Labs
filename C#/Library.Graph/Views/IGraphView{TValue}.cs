using System;
using System.Collections.Generic;

namespace Library.GraphTypes.Views
{
    public interface IGraphView<out TViewItem, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
        where TViewItem : IGraphViewItem<TValue>
    {
        IReadOnlyList<TViewItem> Items { get; }
    }
}