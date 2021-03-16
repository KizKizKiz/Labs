using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

using MathNet.Numerics.Distributions;

using Library.Graph.Views;

namespace Library.Graph.Types
{
    /// <summary>
    /// Представляет базовую реализацию всех типов графов.
    /// </summary>
    /// <typeparam name="TView">Тип представления графа.</typeparam>
    /// <typeparam name="TViewItem">Тип элемента представления графа.</typeparam>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public abstract class Graph<TView, TViewItem, TValue>
        where TViewItem : IGraphViewItem<TValue>
        where TView : IGraphView<TViewItem, TValue>
        where TValue : IEquatable<TValue>
    {
        /// <summary>
        /// Возвращает контракт представления графа.
        /// </summary>
        public TView View { get; protected set; }

        /// <summary>
        /// Возвращает тип ребер графа.
        /// </summary>
        public EdgeType EdgeType { get; }

        /// <summary>
        /// Возвращает все вершины графа только для чтения.
        /// </summary>
        public IReadOnlyList<TValue> Vertices => _verticesSet;

        public Graph(EdgeType edgeType)
        {
            if (!Enum.IsDefined(edgeType))
            {
                throw new InvalidEnumArgumentException(nameof(edgeType), (int)edgeType, typeof(EdgeType));
            }
            EdgeType = edgeType;
        }

        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="view">Контракт представления графа.</param>
        /// <param name="edgeType">Тип ребер графа.</param>
        public Graph(TView view, EdgeType edgeType)
        {
            if (!Enum.IsDefined(edgeType))
            {
                throw new InvalidEnumArgumentException(nameof(edgeType), (int)edgeType, typeof(EdgeType));
            }
            EdgeType = edgeType;
            View = view ?? throw new ArgumentNullException(nameof(view));
        }

        /// <summary>
        /// Представляет словарь, в котором ключом является вершина,
        /// а значением является кортеж - количество элементов для генерации и пустая хэш-таблица элементов.
        /// </summary>
        protected static Dictionary<TValue, (int Count, HashSet<TValue> Items)> MapVertexAndLists => _mapVertexAndLists;

        /// <summary>
        /// Представляет множество вершин графа.
        /// </summary>
        protected static IReadOnlyList<TValue> VerticesSet 
        { 
            get => _verticesSet;
            set => _verticesSet = value.ToList();
        }

        protected static Random RandomGenerator => _random;

        protected static void InitializeVerticesSetAndMap(int verticesCount, int meanCohesion, Func<TValue> factory)
        {
            var vertices = new HashSet<TValue>(verticesCount);
            _ = Enumerable
                .Range(0, verticesCount + 1)
                .Aggregate((f, s) =>
                {
                    while (!vertices.Add(factory()))
                    { }
                    return s;
                });
            _verticesSet.AddRange(vertices);

            _verticesSet.ForEach((v =>
            {
                var elements = Poisson.Sample(_random, meanCohesion);

                elements = elements == 0 ? 1 : elements;
                elements = elements >= verticesCount - 1 ? verticesCount - 1 : elements;

                _mapVertexAndLists.Add(v, (Count: elements, Items: new HashSet<TValue>()));
            }));
        }

        private readonly static Dictionary<TValue, (int Count, HashSet<TValue> Items)> _mapVertexAndLists
            = new Dictionary<TValue, (int Count, HashSet<TValue> Items)>();
        private static List<TValue> _verticesSet = new List<TValue>();
        private readonly static Random _random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
    }
}