using System;
using System.Collections.Generic;

namespace Library.GraphTypes.Views
{
    public class OrientedAdjacensiesView<TValue> : GraphView<AdjacensyViewItem<TValue>, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        public OrientedAdjacensiesView(IEnumerable<AdjacensyViewItem<TValue>> adjacensies)
            :base(adjacensies){ }
    }
}