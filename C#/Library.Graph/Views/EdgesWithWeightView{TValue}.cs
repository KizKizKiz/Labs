using System;
using System.Collections.Generic;

namespace Library.Graph.Views
{
    public abstract class EdgesWithWeightView<TValue> : EdgesView<EdgeViewItemWithWeight<TValue>, TValue>
        where TValue : IEquatable<TValue>
    {
        public EdgesWithWeightView(IEnumerable<EdgeViewItemWithWeight<TValue>> items)
            : base(items) { }
    }
}