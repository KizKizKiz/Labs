using System;

namespace Library.Graph.Generators.Options
{
    public sealed class UnorientedViewGeneratorOptions<TValue> : GeneratorViewOptions<TValue>
    {
        public bool IsConnected { get; }

        public UnorientedViewGeneratorOptions(
            int verticesCount,
            int meanCohesion,
            Func<TValue> factory,
            bool isConnected = true)
            : base(verticesCount, meanCohesion, factory)
        {
            IsConnected = isConnected;
        }
    }
}
