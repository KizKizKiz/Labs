using System;
using System.Collections.Generic;

namespace Library.Graph.Views
{
    public class OrientedEdgeWithWeightView<TValue> : EdgesWithWeightView<TValue>
        where TValue : IEquatable<TValue>
    {
        public OrientedEdgeWithWeightView(IEnumerable<EdgeViewItemWithWeight<TValue>> items)
            : base(items) { }
    }
}