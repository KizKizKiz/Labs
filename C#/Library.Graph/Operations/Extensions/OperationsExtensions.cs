using System;
using System.Collections.Generic;

using Library.Graph.ExampleConvertibleTypes;
using Library.Graph.Types;

namespace Library.Graph.Operations.Extensions
{
    public static class OperationsExtensions
    {
        /// <summary>
        /// Возвращает итератор обхода в ширину.
        /// </summary>
        /// <typeparam name="TValue">Тип элементов графа.</typeparam>
        /// <param name="graph">Граф.</param>
        public static IEnumerable<TValue> SetupBFSWalking<TValue>(
            this Graph<TValue> graph)
            where TValue : notnull, new()
        {
            return graph is null ?
                throw new ArgumentNullException(nameof(graph))
                : new BFSIterator<TValue>(graph);
        }

        /// <summary>
        /// Возвращает итератор обхода в глубину.
        /// </summary>
        /// <typeparam name="TValue">Тип элементов графа.</typeparam>
        /// <param name="graph">Граф.</param>
        public static IEnumerable<TValue> SetupDFSWalking<TValue>(
            this Graph<TValue> graph)
            where TValue : notnull, new()
        {
            return graph is null ?
                throw new ArgumentNullException(nameof(graph))
                : new DFSIterator<TValue>(graph);
        }

        /// <summary>
        /// Возвращает итератор ребер минимального остовного дерева.
        /// </summary>
        /// <typeparam name="TValue">Тип элементов графа.</typeparam>
        /// <param name="graph">Граф.</param>
        public static IEnumerable<EdgeItem<TValue>> SetupMSTWalking<TValue>(
            this Graph<TValue> graph)
            where TValue : notnull, new()
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            return new MinimumSpanningTreeIterator<TValue>(graph);
        }

        /// <summary>
        /// Возвращает итератор максимально связных компонент.
        /// </summary>
        /// <typeparam name="TValue">Тип элементов графа.</typeparam>
        /// <param name="graph">Граф.</param>
        public static IEnumerable<IEnumerable<TValue>> SetupSCCWalking<TValue>(
            this Graph<TValue> graph)
            where TValue : notnull, new()
        {
            return graph is null ? throw new ArgumentNullException(nameof(graph))
                : new StronglyConnectedComponentsIterator<TValue>(graph);
        }

        /// <summary>
        /// Возвращает величину максимального потока в графе.
        /// </summary>
        /// <typeparam name="TValue">Тип элементов графа.</typeparam>
        /// <param name="graph">Граф.</param>
        public static double CalculateMaxFlow<TValue>(
            this TransportNetworkGraph<TValue> graph)
            where TValue : notnull, new()
        {
            return graph is null ? throw new ArgumentNullException(nameof(graph))
                : new MaxFlowCalculator<TValue>(graph).Calculate();
        }

        /// <summary>
        /// Возвращает итератор кратчайшего пути.
        /// </summary>
        /// <typeparam name="TValue">Тип элементов графа.</typeparam>
        /// <param name="graph">Граф.</param>
        /// <returns></returns>
        public static IEnumerable<EdgeItem<TValue>> SetupDijkstraWalking<TValue>(
            this TransportNetworkGraph<TValue> graph)
            where TValue : notnull, IComparable<TValue>, new()
        {
            return graph is null ? throw new ArgumentNullException(nameof(graph))
                : new DijkstraShortestPathIterator<TValue>(graph);
        }
        /// <summary>
        /// Возвращает итератор кратчайшего пути от одной вершины к другой.
        /// </summary>
        /// <param name="graph">Граф.</param>
        /// <param name="s">Начальная вершина.</param>
        /// <param name="t">Конечная вершина.</param>
        public static IEnumerable<IntConvertible> SetupFloydWarshallWalkingFromSourceToTarget(
           this Graph<IntConvertible> graph, int s, int t)
        {
            return graph is null ? throw new ArgumentNullException(nameof(graph))
                : new FloydWarshallIterator(graph).GetIteratorFromSourceToTarget(s, t);
        }
        /// <summary>
        /// Возвращает итератор всех кратчайших путей.
        /// </summary>
        /// <param name="graph">Граф.</param>
        public static IEnumerable<IEnumerable<IntConvertible>> SetupFloydWarshallWalking(
           this Graph<IntConvertible> graph)
        {
            return graph is null ? throw new ArgumentNullException(nameof(graph))
                : new FloydWarshallIterator(graph);
        }
    }
}