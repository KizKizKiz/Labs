using System;
using System.Collections.Generic;
using System.Linq;

using Library.Graph.Extensions;
using Library.Graph.ExampleConvertibleTypes;
using Library.Graph.Types;
using System.Collections;

namespace Library.Graph.Operations
{
    public class HungarianIterator : IEnumerable<IEnumerable<IntConvertible>>
    {
        public HungarianIterator(BipartiteGraph<IntConvertible> graph)
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            if (!graph.Items.Any())
            {
                throw new ArgumentException("The graph is empty.");
            }
            if (!graph.IsWeighted)
            {
                throw new ArgumentException("For Hungarian algorithm graph must be weighted.");
            }
            if (graph.Edges.Any(v => v.Weight < 0))
            {
                throw new ArgumentException("For Hungarian algorithm negative weight is not valid.");
            }

            (_a, _mapRowVertex, _mapColumnVertex) = graph.ToDoubleDemensionalArray();

            _n = _mapRowVertex.Count;
            _m = _mapColumnVertex.Count;

            _v = new double[_n+1];
            _u = new double[_m + 1];
            _p = new int[_m + 1];
            _way = new int[_m + 1];
            _ans = new int[_n + 1];

            HungarianExecute();
        }
        private void HungarianExecute()
        {
            for (var i = 1; i <= _n; ++i)
            {
                _p[0] = i;
                var j0 = 0;
                var minv = new double[_m + 1];
                Array.Fill(minv, double.MaxValue);
                var used = new bool[_m + 1];
                Array.Fill(used, false);
                do
                {
                    used[j0] = true;
                    var i0 = _p[j0];
                    var delta = double.MaxValue;
                    var j1 = 0;
                    for (var j = 1; j <= _m; ++j)
                    {
                        if (!used[j])
                        {
                            var cur = _a[i0, j] - _u[i0] - _v[j];
                            if (cur < minv[j])
                            {
                                minv[j] = cur;
                                _way[j] = j0;
                            }
                            if (minv[j] < delta)
                            {
                                delta = minv[j];
                                j1 = j;
                            }
                        }
                    }
                    for (var j = 0; j < =_m; ++j)
                    {
                        if (used[j])
                        {
                            _u[_p[j]] += delta;
                            _v[j] -= delta;
                        }
                        else
                        {
                            minv[j] -= delta;
                        }
                    }

                    j0 = j1;
                } while (_p[j0] != 0);
                do
                {
                    var j1 = _way[j0];
                    _p[j0] = _p[j1];
                    j0 = j1;
                } while (j0 > 0);
            }
        }
        public IEnumerator<IEnumerable<IntConvertible>> GetEnumerator()
        {
            for (var j = 0; j < _m; ++j)
            {
                _ans[_p[j]] = j;
            }
            for (var j = 0; j < _m; ++j)
            {
                yield return
                    new List<IntConvertible>() { _mapRowVertex[j], _mapColumnVertex[_ans[j]] };
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private readonly double[] _u;
        private readonly double[] _v;
        private readonly int[] _p;
        private readonly int[] _way;
        private readonly int _n;
        private readonly int _m;
        private readonly double[,] _a;
        private readonly int[] _ans;
        private readonly IReadOnlyDictionary<int, IntConvertible> _mapRowVertex;
        private readonly IReadOnlyDictionary<int, IntConvertible> _mapColumnVertex;
    }
}
