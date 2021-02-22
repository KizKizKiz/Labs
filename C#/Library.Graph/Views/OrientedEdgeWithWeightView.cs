using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Library.GraphTypes.Views
{
    public class OrientedEdgeWithWeightView<TValue> : EdgesWithWeightView<TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        public OrientedEdgeWithWeightView(IEnumerable<EdgeViewItemWithWeight<TValue>> items)
            : base(items) { }
    }
}