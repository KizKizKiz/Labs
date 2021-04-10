using System.Collections.Generic;
using System.Linq;

using Library.Graph.Types;
using Library.Graph.Generators.Options;

namespace Library.Graph.Generators
{
    public sealed class TransportNetworkGraphGenerator<TValue> : GraphGenerator<TransportNetworkGraph<TValue>, AdjacensyGraphItem<TValue>, TValue, TransportNetworkGraphGeneratorOptions<TValue>>
        where TValue : notnull
    {
        public TransportNetworkGraphGenerator(TransportNetworkGraphGeneratorOptions<TValue> options)
            : base(options)
        { }

        protected override GraphGeneratingResult<TransportNetworkGraph<TValue>, AdjacensyGraphItem<TValue>, TValue> BuildCore()
        {
            _vertexReachTargetCount = 0; //every build need to wipe count

            _mapVertexAndIsReached = MapVertexAndLists.Keys.ToDictionary(c => c, (_) => false);
            _vertices = MapVertexAndLists.Keys.ToList();
            DeterminateSourceAndTarget();
            InitSource();
            InitBody();

            return new GraphGeneratingResult<TransportNetworkGraph<TValue>, AdjacensyGraphItem<TValue>, TValue>(
                new TransportNetworkGraph<TValue>(
                    MapVertexAndLists.Select(kv => new AdjacensyGraphItem<TValue>(kv.Key, kv.Value.Items)),
                    MapVertexAndLists.Keys));
        }

        private void DeterminateSourceAndTarget()
        {
            _source = GetRandomVertexFrom(_vertices);
            _target = GetRandomVertexFrom(_vertices);
            while (_target.Equals(_source))
            {
                _target = GetRandomVertexFrom(_vertices);
            }
        }

        private void InitBody()
        {
            var stronglyConnectedBody = MapVertexAndLists.Where(c => !c.Key.Equals(_source) && !c.Key.Equals(_target));
            _ = stronglyConnectedBody.Aggregate((f, s) =>
              {
                  _ = f.Value.Items.Add(s.Key);
                  return s;
              });

            _ = stronglyConnectedBody.Last().Value.Items.Add(stronglyConnectedBody.First().Key);

            foreach (var kv in stronglyConnectedBody)
            {
                var _verticesCount = kv.Value.Count > Options.VerticesCount - 2 ? Options.VerticesCount - 2 : kv.Value.Count; //_verticesCount - 2 (_source, _target)
                while (_verticesCount > kv.Value.Items.Count)
                {
                    var vertex = GetRandomVertexFrom(_vertices);
                    if (!IsLoop(vertex, kv.Key)
                        && !IsContainsDuplicate(vertex, kv.Value.Items)
                        && !vertex.Equals(_source))
                    {
                        _mapVertexAndIsReached[vertex] = true;
                        _ = kv.Value.Items.Add(vertex);
                    }
                    if (vertex.Equals(_target))
                    {
                        _vertexReachTargetCount++;
                    }
                }
            }

            while (_vertexReachTargetCount < Options.TargetMinInVertices)
            {
                var vertex = GetRandomVertexFrom(_vertices);
                if (!vertex.Equals(_target) && !MapVertexAndLists[vertex].Items.Contains(_target))
                {
                    _ = MapVertexAndLists[vertex].Items.Add(_target);
                    _vertexReachTargetCount++;
                }
            }
        }

        private void InitSource()
        {
            _mapVertexAndIsReached[_source] = true;
            _mapVertexAndIsReached[_target] = true;
            while (MapVertexAndLists[_source].Items.Count != Options.SourceOutVertices)
            {
                var vertex = GetRandomVertexFrom(_vertices);
                if (!IsLoop(vertex, _source)
                    && !IsContainsDuplicate(vertex, MapVertexAndLists[_source].Items)
                    && !vertex.Equals(_target))
                {
                    _mapVertexAndIsReached[vertex] = true;
                    _ = MapVertexAndLists[_source].Items.Add(vertex);
                }
            }
        }

        private int _vertexReachTargetCount;
        private Dictionary<TValue, bool> _mapVertexAndIsReached = new();
        private List<TValue> _vertices = new();
        private TValue _source = default!;
        private TValue _target = default!;
    }
}
