using System;
using System.Collections.Generic;
using System.Linq;

using MathNet.Numerics.Distributions;

using Library.GraphTypes.Views;

namespace Library.GraphTypes
{
    public sealed class UnorientedAdjacensiesGraph<TValue> : Graph<UnorientedAdjacensiesView<TValue>, AdjacensyViewItem<TValue>, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        public static UnorientedAdjacensiesGraph<TValue> Generate(int vertices, int meanCohesionPower, Func<TValue> factory)
        {
            InitializeVerticesSetAndMap(vertices, meanCohesionPower, factory);

            return GenerateCore();
        }

        public static UnorientedAdjacensiesGraph<TValue> GenerateInCoherent(int vertices, int meanCohesionPower, Func<TValue> factory)
        {
            InitializeVerticesSetAndMap(vertices, meanCohesionPower, factory);

            return GenerateInCoherentCore();
        }
        private static UnorientedAdjacensiesGraph<TValue> GenerateCore()
        {
            foreach (var pair in _mapVertexAndLists)
            {
                _ = Enumerable
                    .Range(0, pair.Value.Count + 1)
                    .Aggregate((ff, ss) =>
                    {
                        while (pair.Value.Items.Count < pair.Value.Count)
                        {
                            var addedVertex = _verticesSet[_random.Next(_verticesSet.Count)];

                            if (!pair.Value.Items.Contains(addedVertex) && !addedVertex.Equals(pair.Key))
                            {
                                _ = pair.Value.Items.Add(addedVertex);

                                if (!_mapVertexAndLists[addedVertex].Items.Contains(pair.Key))
                                {
                                    _ = _mapVertexAndLists[addedVertex].Items.Add(pair.Key);
                                }
                            }
                        }
                        return ss;
                    });
            }

            return new UnorientedAdjacensiesGraph<TValue>(
                new UnorientedAdjacensiesView<TValue>(
                    _mapVertexAndLists.Select(kv => new AdjacensyViewItem<TValue>(kv.Key, kv.Value.Items))));
        }

        private static UnorientedAdjacensiesGraph<TValue> GenerateInCoherentCore()
        {
            var skipVertex = _verticesSet[_random.Next(_verticesSet.Count)];

            foreach (var pair in _mapVertexAndLists)
            {
                if (pair.Key.Equals(skipVertex))
                {
                    continue;
                }

                _ = Enumerable
                    .Range(0, pair.Value.Count + 1)
                    .Aggregate((ff, ss) =>
                    {
                        while (pair.Value.Items.Count < pair.Value.Count)
                        {
                            var addedVertex = _verticesSet[_random.Next(_verticesSet.Count)];

                            if (!addedVertex.Equals(skipVertex) && !pair.Value.Items.Contains(addedVertex) && !addedVertex.Equals(pair.Key))
                            {
                                _ = pair.Value.Items.Add(addedVertex);

                                if (!_mapVertexAndLists[addedVertex].Items.Contains(pair.Key))
                                {
                                    _ = _mapVertexAndLists[addedVertex].Items.Add(pair.Key);
                                }
                            }
                        }
                        return ss;
                    });
            }

            return new UnorientedAdjacensiesGraph<TValue>(
                new UnorientedAdjacensiesView<TValue>(
                    _mapVertexAndLists.Select(kv => new AdjacensyViewItem<TValue>(kv.Key, kv.Value.Items))));
        }

        private static void InitializeVerticesSetAndMap(int verticesCount, int meanCohesion, Func<TValue> factory)
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
            _verticesSet = vertices.ToList();

            _mapVertexAndLists = _verticesSet.ToDictionary(v => v,
                v =>
                {
                    var elements = Poisson.Sample(_random, meanCohesion);
                    elements = elements == 0 ? 1 : elements;

                    return (Count: elements, Items: new HashSet<TValue>(elements));
                });
        }

        private UnorientedAdjacensiesGraph(UnorientedAdjacensiesView<TValue> view)
            : base(view)
        { }

        private static Dictionary<TValue, (int Count, HashSet<TValue> Items)> _mapVertexAndLists;
        private static List<TValue> _verticesSet;
        private static Random _random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
    }
}