using System;

namespace Library.Graph.Generators.Options
{
    public sealed class TransportNetworkGraphGeneratorOptions<TValue> : GeneratorGraphOptions<TValue>
    {
        public int SourceOutVertices { get; }

        public int TargetMinInVertices { get; }

        public TransportNetworkGraphGeneratorOptions(
            int verticesCount,
            int meanCohesion,
            Func<TValue> factory,
            int sourceOutVertices,
            int targetMinInVertices)
            : base(verticesCount, meanCohesion, factory)
        {
            if (verticesCount - 2 == 0)
            {
                throw new ArgumentException("Minimum vertices count is 3.", nameof(verticesCount));
            }
            if (sourceOutVertices <= 0 || sourceOutVertices >= verticesCount)
            {
                throw new ArgumentException("The number of output vertices from 'SOURCE' must be greater than zero and less than vertices count.", nameof(sourceOutVertices));
            }
            if (targetMinInVertices <= 0 || targetMinInVertices >= verticesCount)
            {
                throw new ArgumentException("The number of input vertices to 'TARGET' must be greater than zero and less than vertices count.", nameof(targetMinInVertices));
            }
            SourceOutVertices = sourceOutVertices;
            TargetMinInVertices = targetMinInVertices;
        }
    }
}
