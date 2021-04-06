using System;

namespace Library.Graph.Generators.Options
{
    public sealed class TransportNetworkViewGeneratorOptions<TValue> : GeneratorViewOptions<TValue>
    {
        public int SourceOutVertices { get; }

        public int TargetMinInVertices { get; }

        public TransportNetworkViewGeneratorOptions(
            int verticesCount,
            int meanCohesion,
            Func<TValue> factory,
            int sourceOutVertices,
            int targetMinInVertices)
            : base(verticesCount, meanCohesion, factory)
        {
            if (sourceOutVertices <= 0 && sourceOutVertices >= verticesCount)
            {
                throw new ArgumentException("The number of output vertices from 'SOURCE' must be greater than zero and less than vertices count.", nameof(sourceOutVertices));
            }
            if (targetMinInVertices <= 0 && targetMinInVertices >= verticesCount)
            {
                throw new ArgumentException("The number of input vertices to 'TARGET' must be greater than zero and less than vertices count.", nameof(targetMinInVertices));
            }
            SourceOutVertices = sourceOutVertices;
            TargetMinInVertices = targetMinInVertices;
        }
    }
}
