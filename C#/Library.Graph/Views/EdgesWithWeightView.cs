using System;
using System.Collections.Generic;

namespace Library.GraphTypes.Views
{
    public abstract class EdgesWithWeightView<TValue> : EdgesView<EdgeViewItemWithWeight<TValue>, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        public EdgesWithWeightView(IEnumerable<EdgeViewItemWithWeight<TValue>> items)
            : base(items) { }
    }
}