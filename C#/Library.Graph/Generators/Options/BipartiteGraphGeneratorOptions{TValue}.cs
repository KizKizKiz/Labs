using System;
using System.Collections.Generic;

namespace Library.Graph.Generators.Options
{
    public sealed class BipartiteGraphGeneratorOptions<TValue> : GeneratorGraphOptions<TValue>
        where TValue : IEqualityComparer<TValue>, IEquatable<TValue>
    {
        /// <summary>
        /// Конструктор настроек.
        /// </summary>
        /// <param name="verticesCount">Количество вершин.</param>
        /// <param name="meanConnectivity">Средняя степень связности.</param>
        /// <param name="factory">Фабрика создания вершин.</param>
        /// <param name="range">Диапазон генерации весов.</param>
        /// <param name="isConnected">Флаг, является ли граф связным (по умолчанию граф связный).</param>
        public BipartiteGraphGeneratorOptions(
            int verticesCount,
            Func<TValue> factory)
            : base(verticesCount, verticesCount / 2, factory, (0, 0))
        {
        }
    }
}
