using System;
using System.Collections.Generic;

namespace Library.Graph.Generators.Options
{
    /// <summary>
    /// Представляет настройки для генерации двудольных графов.
    /// </summary>
    public sealed class BipartiteGraphGeneratorOptions<TValue> : GeneratorGraphOptions<TValue>
        where TValue : IEqualityComparer<TValue>, IEquatable<TValue>
    {
        /// <summary>
        /// Конструктор настроек.
        /// </summary>
        /// <param name="verticesCount">Количество вершин.</param>
        /// <param name="factory">Фабрика создания вершин.</param>
        /// <param name="range">Диапазон генерации весов.</param>
        public BipartiteGraphGeneratorOptions(
            int verticesCount,
            Func<TValue> factory,
            (int min, int max) range = default)
            : base(verticesCount, verticesCount / 2, factory, range)
        {
        }
    }
}
