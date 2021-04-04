using System;
using System.Diagnostics;

using Library.Graph.Views;
using Library.Graph.Generators.Options;
using System.Collections.Generic;

namespace Library.Graph.Generators
{
    public class OrientedViewGenerator<TValue> : GraphViewGenerator<AdjacensiesView<TValue>, AdjacensyViewItem<TValue>, TValue>
        where TValue : notnull
    {
        public OrientedViewGenerator(OrientedViewGeneratorOptions<TValue> orientedView)
            : base(orientedView)
        { }

        protected override ViewGeneratingResult<TValue> BuildCore()
        {
            var options = (OrientedViewGeneratorOptions<TValue>)Options;

            Debug.Assert(!Enum.IsDefined(options.Connectivity), "Fail never happens.");

            var items = options.Connectivity switch
            {
                ConnectivityType.NotConnected => CreateNotConnected(),
                ConnectivityType.WeaklyConnected => CreateWeaklyConnected(),
                ConnectivityType.StronglyConnected => CreateStronglyConnected(),
                _ => throw new InvalidOperationException($"Received unknown connectivity type '{options.Connectivity}'.")
            };

            return ViewGeneratingResult<TValue>.Create(
                new AdjacensiesView<TValue>(items, MapVertexAndLists.Keys));
        }

        private IEnumerable<AdjacensyViewItem<TValue>> CreateNotConnected()
        {
            throw new NotImplementedException();
        }
        private IEnumerable<AdjacensyViewItem<TValue>> CreateWeaklyConnected()
        {
            throw new NotImplementedException();
        }
        private IEnumerable<AdjacensyViewItem<TValue>> CreateStronglyConnected()
        {
            throw new NotImplementedException();
        }
    }
}
