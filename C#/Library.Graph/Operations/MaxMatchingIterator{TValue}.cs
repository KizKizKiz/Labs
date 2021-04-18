using System;
using System.Collections.Generic;
using System.Linq;

using Library.Graph.Types;

namespace Library.Graph.Operations
{
    /// <summary>
    /// Представляет алгоритм вычисления максимального паросочетания по мощности.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public class MaxMatchingCalculator<TValue>
        where TValue : notnull, new()
    {
        public MaxMatchingCalculator(BipartiteGraph<TValue> graph, Func<TValue> fakeVertexGenerator)
        {
            if (fakeVertexGenerator is null)
            {
                throw new ArgumentNullException(nameof(fakeVertexGenerator));
            }
            _graph = graph ?? throw new ArgumentNullException(nameof(graph));

            _mapVertexAndDist = graph.Items.ToDictionary(v => v.Key, _ => -1);
            while (true)
            {
                var vertex = fakeVertexGenerator();
                if (!_mapVertexAndDist.ContainsKey(vertex))
                {
                    _mapVertexAndDist[vertex] = -1;
                    break;
                }
            }

            _mapLeftVertexAndOtherVertex = graph.LeftShare.ToDictionary(c => c.Key, _ => _specialVertex);
            _mapRightVertexAndOtherVertex = graph.RightShare.ToDictionary(c => c.Key, _ => _specialVertex);
        }

        /// <summary>
        /// Возвращает максимальное по мощности паросочетание.
        /// </summary>
        /// <returns>Мощность паросочетания.</returns>
        public int MaxMatching()
        {
            var maxMatching = 0;
            while (BFS())
            {
                foreach (var leftVertex in _graph.LeftShare.Keys)
                {
                    if (_mapLeftVertexAndOtherVertex[leftVertex].Equals(_specialVertex))
                    {
                        if (DFS(leftVertex))
                        {
                            maxMatching++;
                        }
                    }
                }
            }
            return maxMatching;
        }

        private bool BFS()
        {
            var vertices = new Queue<TValue>();
            foreach (var leftVertexPair in _graph.LeftShare)
            {
                if (_mapLeftVertexAndOtherVertex[leftVertexPair.Key].Equals(_specialVertex))
                {
                    _mapVertexAndDist[leftVertexPair.Key] = 0;
                    vertices.Enqueue(leftVertexPair.Key);
                }
                else
                {
                    _mapVertexAndDist[leftVertexPair.Key] = int.MaxValue;
                }
            }

            _mapVertexAndDist[_specialVertex] = int.MaxValue;
            while (vertices.Count != 0)
            {
                var vertex = vertices.Dequeue();
                if (_mapVertexAndDist[vertex] < _mapVertexAndDist[_specialVertex])
                {
                    foreach (var item in _graph.Items[vertex].Items.Select(c => c.Target))
                    {
                        if (_mapVertexAndDist[_mapRightVertexAndOtherVertex[item]] == int.MaxValue)
                        {
                            _mapVertexAndDist[_mapRightVertexAndOtherVertex[item]] = _mapVertexAndDist[vertex] + 1;
                            vertices.Enqueue(_mapRightVertexAndOtherVertex[item]);
                        }
                    }
                }
            }
            return _mapVertexAndDist[_specialVertex] != int.MaxValue;
        }

        private bool DFS(TValue vertex)
        {
            if (!vertex.Equals(_specialVertex))
            {
                foreach (var item in _graph.Items[vertex].Items.Select(c => c.Target))
                {
                    if (_mapVertexAndDist[_mapRightVertexAndOtherVertex[item]] == _mapVertexAndDist[vertex] + 1)
                    {
                        if (DFS(_mapRightVertexAndOtherVertex[item]))
                        {
                            _mapRightVertexAndOtherVertex[item] = vertex;
                            _mapLeftVertexAndOtherVertex[vertex] = item;
                            return true;
                        }
                    }
                }
                _mapVertexAndDist[vertex] = int.MaxValue;
                return false;
            }
            return true;
        }

        private readonly BipartiteGraph<TValue> _graph;
        private readonly Dictionary<TValue, int> _mapVertexAndDist;
        private readonly Dictionary<TValue, TValue> _mapLeftVertexAndOtherVertex;
        private readonly Dictionary<TValue, TValue> _mapRightVertexAndOtherVertex;
        private readonly TValue _specialVertex = new();
    }
}
