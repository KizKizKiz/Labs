using System;
using System.Collections.Generic;
using System.Linq;

using Library.Graph.Types;

namespace Library.Graph.Generators
{
    /// <summary>
    /// Представляет результат генерации графа.
    /// </summary>
    /// <typeparam name="TGraph">Тип графа.</typeparam>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public sealed class GraphGeneratingResult<TGraph, TValue>
        where TValue : notnull, IEqualityComparer<TValue>, IEquatable<TValue>
        where TGraph : Graph<TValue>
    {
        /// <summary>
        /// Граф.
        /// </summary>
        public TGraph Graph { get; }

        /// <summary>
        /// Конструктор результата.
        /// </summary>
        /// <param name="graph">Граф.</param>
        public GraphGeneratingResult(TGraph graph)
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            if (!graph.Items.Any())
            {
                throw new ArgumentException("The items collection is empty.", nameof(graph));
            }
            Graph = graph;
        }
    }
}