using System;
using System.Collections.Generic;

namespace Library.Graph.Views
{
    public class UnorientedAdjacensiesView<TValue> : GraphView<AdjacensyViewItem<TValue>, TValue>
        where TValue : IEquatable<TValue>
    {
        public UnorientedAdjacensiesView(IEnumerable<AdjacensyViewItem<TValue>> adjacensies)
            : base(adjacensies) { }
    }
}