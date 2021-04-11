using System;
using System.ComponentModel;

namespace Library.Graph.Generators.Options
{
    /// <summary>
    /// Представляет настройки для генерации ориентированных графов.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public sealed class OrientedGraphGeneratorOptions<TValue> : GeneratorGraphOptions<TValue>
    {
        /// <summary>
        /// Тип связности.
        /// </summary>
        public ConnectivityType Connectivity { get; }

        /// <summary>
        /// Конструктор настроек.
        /// </summary>
        /// <param name="verticesCount">Количество вершин.</param>
        /// <param name="meanConnectivity">Средняя степень связности.</param>
        /// <param name="factory">Фабрика создания вершин.</param>
        /// <param name="range">Диапазон генерации весов.</param>
        /// <param name="connectivity">Тип связности</param>
        public OrientedGraphGeneratorOptions(
            int verticesCount,
            int meanConnectivity,
            Func<TValue> factory,
            ConnectivityType connectivity,
            (int min, int max) range)
            : base(verticesCount, meanConnectivity, factory, range)
        {
            if (!Enum.IsDefined(connectivity))
            {
                throw new InvalidEnumArgumentException(nameof(connectivity), (int)connectivity, typeof(ConnectivityType));
            }
            Connectivity = connectivity;
        }
    }
}
