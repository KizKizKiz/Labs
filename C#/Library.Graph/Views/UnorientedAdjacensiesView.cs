using System;
using System.Collections.Generic;

namespace Library.GraphTypes.Views
{
    public class UnorientedAdjacensiesView<TValue> : GraphView<AdjacensyViewItem<TValue>, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        public UnorientedAdjacensiesView(IEnumerable<AdjacensyViewItem<TValue>> adjacensies)
            : base(adjacensies) { }
    }
}