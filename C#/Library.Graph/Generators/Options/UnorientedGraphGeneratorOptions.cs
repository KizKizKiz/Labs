using System;

namespace Library.Graph.Generators.Options
{
    public sealed class UnorientedGraphGeneratorOptions<TValue> : GeneratorGraphOptions<TValue>
    {
        public bool IsConnected { get; }

        public UnorientedGraphGeneratorOptions(
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
