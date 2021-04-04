
using Library.Graph.Views;
using Library.Graph.Generators.Options;
using System.Collections.Generic;

namespace Library.Graph.Generators
{
    public class UnorientedViewGenerator<TValue> : GraphViewGenerator<TValue>
        where TValue : notnull
    {
        public UnorientedViewGenerator(UnorientedViewGeneratorOptions<TValue> orientedView)
            : base(orientedView)
        { }

        protected override ViewGeneratingResult<TValue> BuildCore()
        {
            var options = (UnorientedViewGeneratorOptions<TValue>)Options;

            IEnumerable<AdjacensyViewItem<TValue>> items = null!;
            if (options.IsConnected)
            {

            }
            else
            {

            }
            return ViewGeneratingResult<TValue>.Create(
                new AdjacensiesView<TValue>(items, MapVertexAndLists.Keys));
        }
    }
}
