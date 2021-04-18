using System;
using System.Collections.Generic;

namespace Library.Graph.Generators.Options
{
    /// <summary>
    /// Представляет настройки для генерации транспортных сетей.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов транспортной сети.</typeparam>
    public sealed class TransportNetworkGraphGeneratorOptions<TValue> : GeneratorGraphOptions<TValue>
        where TValue : IEqualityComparer<TValue>, IEquatable<TValue>
    {
        /// <summary>
        /// Количество вершин исходящих из истока.
        /// </summary>
        public int SourceOutVertices { get; }

        /// <summary>
        /// Минимальное количество вершин входящих в сток.
        /// </summary
        public int TargetMinInVertices { get; }

        /// <summary>
        /// Конструктор настроек.
        /// </summary>
        /// <param name="verticesCount">Количество вершин.</param>
        /// <param name="meanConnectivity">Средняя степень связности.</param>
        /// <param name="factory">Фабрика создания вершин.</param>
        /// <param name="range">Диапазон генерации весов.</param>
        /// <param name="sourceOutVertices">Количество вершин исходящих из истока.</param>
        /// <param name="targetMinInVertices">Минимальное количество вершин входящих в сток.</param>
        public TransportNetworkGraphGeneratorOptions(
            int verticesCount,
            int meanConnectivity,
            Func<TValue> factory,
            int sourceOutVertices,
            int targetMinInVertices,
            (int min, int max) range)
            : base(verticesCount, meanConnectivity, factory, range)
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
