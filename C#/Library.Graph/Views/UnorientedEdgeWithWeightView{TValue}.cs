using System;
using System.Collections.Generic;

namespace Library.Graph.Views
{
    public class UnorientedEdgeWithWeightView<TValue> : EdgesWithWeightView<TValue>
        where TValue : IEquatable<TValue>
    {
        public UnorientedEdgeWithWeightView(IEnumerable<EdgeViewItemWithWeight<TValue>> items)
            : base(items) { }
    }
}