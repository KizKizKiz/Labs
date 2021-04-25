using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Library.Graph.ExampleConvertibleTypes;
using Library.Graph.Types;

namespace Library.Graph.Operations
{
    public class HungarianIterator
    {
        public HungarianIterator(Graph<IntConvertible> graph)
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
            _n = graph.Vertices.Count;
            //_a = graph;
            _v = new int[_n + 1];
            _u = new int[_n + 1];
            _p = new int[_n + 1];
            _way = new int[_n + 1];
            HungarianExecute();
        }
        private void HungarianExecute()
        {
            for (var i = 1; i <= _n; ++i)
            {
                _p[0] = i;
                var j0 = 0;
                var minv = new int[_n + 1];
                Array.Fill(minv, int.MaxValue);
                var used = new bool[_n + 1];
                Array.Fill(used, false);
                do
                {
                    used[j0] = true;
                    var i0 = _p[j0];
                    var delta = int.MaxValue;
                    var j1 = 0;
                    for (var j = 1; j <= _n; ++j)
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
                    for (var j = 0; j <= _n; ++j)
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
        private int[] _u;
        private int[] _v;
        private int[] _p;
        private int[] _way;
        private int _n;
        private int[,] _a;
    }
}
