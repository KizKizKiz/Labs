using System;

using Library.Graph.ExampleConvertibleTypes;

namespace Library.Graph.Generators.Options
{
    /// <summary>
    /// Представляет настройки для генерации полносвязных двудольных графов.
    /// </summary>
    public class FullyConnectedBipartiteGraphGeneratorOptions : GeneratorGraphOptions<IntConvertible>
    {
        /// <summary>
        /// Конструктор настроек.
        /// </summary>
        /// <param name="verticesCount">Количество вершин.</param>
        /// <param name="factory">Фабрика создания вершин.</param>
        /// <param name="range">Диапазон генерации весов.</param>
        public FullyConnectedBipartiteGraphGeneratorOptions(
            int verticesCount,
            Func<IntConvertible> factory,
            (int min, int max) range)
            : base(verticesCount, verticesCount / 2, factory, range)
        {
            if (verticesCount % 2 != 0)
            {
                throw new ArgumentException("Vertices count should be even number.", nameof(verticesCount));
            }
        }
    }
}
