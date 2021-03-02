using System;
using System.Collections.Generic;

namespace Library.Views
{
    public class UnorientedEdgeWithWeightView<TValue> : EdgesWithWeightView<TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        public UnorientedEdgeWithWeightView(IEnumerable<EdgeViewItemWithWeight<TValue>> items)
            : base(items) { }
    }
}