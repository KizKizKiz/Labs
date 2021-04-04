using Library.Graph.Views;
using System;
using System.ComponentModel;

namespace Library.Graph.Generators.Options
{
    public sealed class OrientedViewGeneratorOptions<TValue> : GeneratorViewOptions<TValue>
    {
        public ConnectivityType Connectivity { get; }

        public OrientedViewGeneratorOptions(
            int verticesCount,
            int meanCohesion,
            Func<TValue> factory,
            ConnectivityType connectivity)
            : base(verticesCount, meanCohesion, factory)
        {
            if (!Enum.IsDefined(connectivity))
            {
                throw new InvalidEnumArgumentException(nameof(connectivity), (int)connectivity, typeof(ConnectivityType));
            }
            Connectivity = connectivity;
        }
    }
}
