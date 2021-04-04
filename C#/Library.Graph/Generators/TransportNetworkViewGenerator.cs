using System;

using Library.Graph.Views;
using Library.Graph.Generators.Options;

namespace Library.Graph.Generators
{
    public sealed class TransportNetworkViewGenerator<TValue> : GraphViewGenerator<TValue>
        where TValue : notnull
    {
        public TransportNetworkViewGenerator(TransportNetworkViewGeneratorOptions<TValue> options)
            : base(options)
        {
        }

        protected override ViewGeneratingResult<TValue> BuildCore()
        {
            throw new NotImplementedException();
        }
    }
}
