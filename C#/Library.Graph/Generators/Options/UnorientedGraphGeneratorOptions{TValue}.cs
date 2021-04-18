using System;
using System.Collections.Generic;

namespace Library.Graph.Generators.Options
{
    /// <summary>
    /// Представляет настройки для генерации неориентированных графов.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public sealed class UnorientedGraphGeneratorOptions<TValue> : GeneratorGraphOptions<TValue>
        where TValue : IEqualityComparer<TValue>, IEquatable<TValue>
    {
        /// <summary>
        /// Флаг, является ли граф связным.
        /// </summary>
        public bool IsConnected { get; }

        /// <summary>
        /// Конструктор настроек.
        /// </summary>
        /// <param name="verticesCount">Количество вершин.</param>
        /// <param name="meanConnectivity">Средняя степень связности.</param>
        /// <param name="factory">Фабрика создания вершин.</param>
        /// <param name="range">Диапазон генерации весов.</param>
        /// <param name="isConnected">Флаг, является ли граф связным (по умолчанию граф связный).</param>
        public UnorientedGraphGeneratorOptions(
            int verticesCount,
            int meanConnectivity,
            Func<TValue> factory,
            (int min, int max) range,
            bool isConnected = true)
            : base(verticesCount, meanConnectivity, factory, range)
        {
            IsConnected = isConnected;
        }
    }
}
