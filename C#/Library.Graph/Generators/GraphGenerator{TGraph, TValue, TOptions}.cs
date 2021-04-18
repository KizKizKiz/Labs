using System;
using System.Collections.Generic;
using System.Linq;

using Library.Graph.Generators.Options;
using Library.Graph.Types;

using MathNet.Numerics.Distributions;

namespace Library.Graph.Generators
{
    /// <summary>
    /// Представляет базовую реализацию генерации графа.
    /// </summary>
    /// <typeparam name="TGraph">Тип графа.</typeparam>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    /// <typeparam name="TOptions">Тип настроек для генерации графа.</typeparam>
    public abstract class GraphGenerator<TGraph, TValue, TOptions> : IGraphGenerator<TGraph, TValue>
        where TValue : notnull
        where TGraph : Graph<TValue>
        where TOptions : GeneratorGraphOptions<TValue>
    {
        /// <summary>
        /// Представляет реализацию по умолчанию для вычисления распределения.
        /// </summary>
        private class DefaultDistributionCalculator : IDistributionCalculator
        {
            /// <summary>
            /// Конструктор реализации вычисления распределения.
            /// </summary>
            /// <param name="lambda">Лямбда (lambda > 0) </param>
            public DefaultDistributionCalculator(double lambda)
            {
                _lambda = lambda;
            }

            /// <inheritdoc/>
            public double GetDistribution() => Poisson.Sample(_lambda);

            private readonly double _lambda;
        }

        /// <summary>
        /// Представляет реализацию вычисления случайного числа.
        /// </summary>
        private class DefaultRandomizer : IRandomizer
        {
            /// <summary>
            /// Возвращает генератор случайных чисел.
            /// </summary>
            public static IRandomizer Randomizer { get; } = new DefaultRandomizer();

            /// <inheritdoc/>
            public int FromRange(int min, int max) => _random.Next(min, max);

            /// <inheritdoc/>
            public int FromRange(int max) => _random.Next(max);

            private DefaultRandomizer() { }

            private static readonly Random _random = new((int)DateTime.Now.Ticks & 0x0000FFFF);
        }

        /// <summary>
        /// Конструктор генератора.
        /// </summary>
        /// <param name="options">Настройки генерации.</param>
        /// <param name="randomizer">Генератор случайных чисел (по умолчанию не задан).</param>
        /// <param name="distributionCalculator">Калькулятор распределения (по умолчанию не задан).</param>
        protected GraphGenerator(TOptions options, IRandomizer? randomizer = null, IDistributionCalculator? distributionCalculator = null)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
            if (randomizer is not null)
            {
                Randomizer = randomizer;
            }
            DistributionCalculator = distributionCalculator is not null ?
                distributionCalculator
                : new DefaultDistributionCalculator(options.MeanConnectivity);
        }

        /// <inheritdoc/>
        public GraphGeneratingResult<TGraph, TValue> Generate()
        {
            MapVertexAndLists = Prepare();
            return BuildCore();
        }

        /// <summary>
        /// Возвращает генератор случайных чисел.
        /// </summary>
        protected IRandomizer Randomizer { get; } = DefaultRandomizer.Randomizer;

        /// <summary>
        /// Калькулятор распределения.
        /// </summary>
        protected IDistributionCalculator DistributionCalculator { get; private set; }

        /// <summary>
        /// Настройки генерации.
        /// </summary>
        protected TOptions Options { get; set; }

        /// <summary>
        /// Словарь, в котором ключом выступает вершина, а значением является кортеж из количества элементов и контейнера элементов.
        /// </summary>
        protected IReadOnlyDictionary<TValue, (int Count, HashSet<EdgeItem<TValue>> Items)> MapVertexAndLists { get; private set; } = default!;

        /// <summary>
        /// Строит граф на основе словаря <see cref="MapVertexAndLists"/> и возвращает результат генерации.
        /// </summary>
        /// <returns>Результат генерации.</returns>
        protected abstract GraphGeneratingResult<TGraph, TValue> BuildCore();

        /// <summary>
        /// Подготавливает словарь <see cref="MapVertexAndLists"/> для построения графа.
        /// </summary>
        protected virtual Dictionary<TValue, (int count, HashSet<EdgeItem<TValue>> items)> Prepare()
        {
            var mapVertexAndTuple = new Dictionary<TValue, (int count, HashSet<EdgeItem<TValue>> items)>();

            var anomalyDetected = 10_000_000;
            while (mapVertexAndTuple.Count != Options.VerticesCount)
            {
                if (anomalyDetected-- == 0)
                {
                    throw new InvalidOperationException("Detected anomaly, cause received bad vertex factory.");
                }
                var vertex = Options.VerticiesFactory();
                if (!mapVertexAndTuple.ContainsKey(vertex))
                {
                    var elements = (int)DistributionCalculator.GetDistribution();

                    elements = elements <= 0 ? 1 : (elements >= Options.VerticesCount ? Options.VerticesCount - 1 : elements);

                    mapVertexAndTuple.Add(vertex, (count: elements, items: new HashSet<EdgeItem<TValue>>()));
                }
            }

            return mapVertexAndTuple;
        }

        protected bool IsLoop(TValue vertexFrom, TValue vertexTo)
            => vertexFrom.Equals(vertexTo);

        protected bool IsContainsDuplicate(TValue vertex, IEnumerable<TValue> items)
            => items is not null ?
            items.Contains(vertex)
            : throw new ArgumentNullException(nameof(items));

        protected TValue GetRandomVertexFrom(IReadOnlyList<TValue> vertices)
            => vertices is not null ?
            vertices[Randomizer.FromRange(vertices.Count)]
            : throw new ArgumentNullException(nameof(vertices));
    }
}
