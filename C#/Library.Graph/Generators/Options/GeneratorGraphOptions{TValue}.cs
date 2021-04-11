using System;

namespace Library.Graph.Generators.Options
{
    /// <summary>
    /// Представляет базовые настройки генерации графа.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public abstract class GeneratorGraphOptions<TValue>
    {
        /// <summary>
        /// Количество вершин.
        /// </summary>
        public int VerticesCount { get; }

        /// <summary>
        /// Средняя степень связности.
        /// </summary>
        public int MeanConnectivity { get; }

        /// <summary>
        /// Фабрика создания вершин.
        /// </summary>
        public Func<TValue> VerticiesFactory { get; }

        /// <summary>
        /// Диапазон генерации весов.
        /// </summary>
        public (int minimum, int maximum) Range { get; }

        /// <summary>
        /// Конструктор настроек.
        /// </summary>
        /// <param name="verticesCount">Количество вершин.</param>
        /// <param name="meanConnectivity">Средняя степень связности.</param>
        /// <param name="factory">Фабрика создания вершин.</param>
        /// <param name="range">Диапазон генерации весов.</param>
        protected GeneratorGraphOptions(
            int verticesCount,
            int meanConnectivity,
            Func<TValue> factory,
            (int min, int max) range)
        {
            if (verticesCount <= 0)
            {
                throw new ArgumentException("Vertices count must be greater than zero.", nameof(verticesCount));
            }
            if (meanConnectivity <= 0 || verticesCount <= meanConnectivity)
            {
                throw new ArgumentException($"Mean connectivity must be greater than zero and less than {nameof(verticesCount)}.", nameof(meanConnectivity));
            }
            if (range.min >= range.max)
            {
                throw new ArgumentException("Range must contains minimum, which less than maximum.", nameof(range));
            }
            VerticesCount = verticesCount;
            MeanConnectivity = meanConnectivity;
            VerticiesFactory = factory ?? throw new ArgumentNullException(nameof(factory));
            Range = range;
        }
    }
}
