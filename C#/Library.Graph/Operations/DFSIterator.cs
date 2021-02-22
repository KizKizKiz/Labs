using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Library.GraphTypes.Operations
{
    public class DFSIterator<TValue> : IEnumerable<TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        public DFSIterator(UnorientedAdjacensiesGraph<TValue> graph)
        {
            _graph = graph ?? throw new ArgumentNullException(nameof(graph));
        }

        public IEnumerator<TValue> GetEnumerator() => SetupDFS().GetEnumerator();
        private IEnumerable<TValue> SetupDFS()
        {
            var mapVertexAndIsMarked = _graph.View.Items.ToDictionary(kv => kv.Vertex, _ => false);
            var passedVertices = new List<TValue>();

            WalkBasedOn(_graph.View.Items[0].Vertex);

            return passedVertices;

            void WalkBasedOn(TValue vertex)
            {
                passedVertices.Add(vertex);
                mapVertexAndIsMarked[vertex] = true;

                foreach (var item in _graph.View.Items.First(c => c.Vertex.Equals(vertex)).Items)
                {
                    if (!mapVertexAndIsMarked[item])
                    {
                        WalkBasedOn(item);
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private readonly UnorientedAdjacensiesGraph<TValue> _graph;
    }
}