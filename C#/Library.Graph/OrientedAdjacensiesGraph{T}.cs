using System;
using System.Collections.Generic;
using System.Linq;
using Library.GraphTypes.Views;
using MathNet.Numerics.Distributions;

namespace Library.GraphTypes
{
    public sealed class OrientedAdjacensiesGraph<TValue> : Graph<OrientedAdjacensiesView<TValue>, AdjacensyViewItem<TValue>, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        public static OrientedAdjacensiesGraph<TValue> Generate(int vertices, int meanCohesionPower, Func<TValue> factory)
        {
            InitializeVerticesSetAndMap(vertices, meanCohesionPower, factory);

            return GenerateCore();
        }

        private static OrientedAdjacensiesGraph<TValue> GenerateCore()
        {
            _ = _mapVertexAndLists
                .Aggregate((f, s) =>
                {
                    s.Value.Items.Add(f.Key);
                    return s;
                });
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
                            }
                        }
                        return ss;
                    });
            }
            return new OrientedAdjacensiesGraph<TValue>(
                new OrientedAdjacensiesView<TValue>(
                    _mapVertexAndLists.Select(kv => new AdjacensyViewItem<TValue>(kv.Key, kv.Value.Items))));
        }

        public static OrientedAdjacensiesGraph<TValue> GenerateInCoherent(int vertices, int meanCohesionPower, Func<TValue> factory)
        {
            throw new NotImplementedException();
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

        private OrientedAdjacensiesGraph(OrientedAdjacensiesView<TValue> view)
            : base(view)
        { }

        private static Dictionary<TValue, (int Count, HashSet<TValue> Items)> _mapVertexAndLists;
        private static List<TValue> _verticesSet;
        private static Random _random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
    }
}