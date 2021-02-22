using System;
using System.Collections.Generic;

using Library.GraphTypes.Views;

namespace Library.GraphTypes
{
    public abstract class Graph<TView, TViewItem, TValue>
        where TViewItem : IGraphViewItem<TValue>
        where TView : IGraphView<TViewItem, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        public TView View { get; }

        public Graph(TView view)
        {
            View = view ?? throw new ArgumentNullException(nameof(view));
        }
    }
}