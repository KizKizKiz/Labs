using System.Collections.Generic;
using System.Linq;

using Library.Graph.Types;
using Library.Graph.Generators.Options;

namespace Library.Graph.Generators
{
    /// <summary>
    /// Представляет генератор транспортных сетей.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public sealed class TransportNetworkGraphGenerator<TValue> : GraphGenerator<TransportNetworkGraph<TValue>, TValue, TransportNetworkGraphGeneratorOptions<TValue>>
        where TValue : notnull
    {
        /// <summary>
        /// Конструктор генератора.
        /// </summary>
        /// <param name="options">Настройки генерации.</param>
        public TransportNetworkGraphGenerator(TransportNetworkGraphGeneratorOptions<TValue> options)
            : base(options)
        { }

        /// <inheritdoc/>
        protected override GraphGeneratingResult<TransportNetworkGraph<TValue>, TValue> BuildCore()
        {
            _vertexReachTargetCount = 0; //every build need to wipe count

            _mapVertexAndIsReached = MapVertexAndLists.Keys.ToDictionary(c => c, (_) => false);
            _vertices = MapVertexAndLists.Keys.ToList();
            DeterminateSourceAndTarget();
            InitSource();
            InitBody();

            return new GraphGeneratingResult<TransportNetworkGraph<TValue>, TValue>(
                new TransportNetworkGraph<TValue>(
                    MapVertexAndLists.Select(kv => new AdjacensyEdgeItem<TValue>(kv.Key, kv.Value.Items)),
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
                  var weight = Randomizer.FromRange(Options.Range.minimum, Options.Range.maximum);
                  _ = f.Value.Items.Add(new EdgeItem<TValue>(f.Key, s.Key, weight));
                  return s;
              });
            var weight = Randomizer.FromRange(Options.Range.minimum, Options.Range.maximum);

            var lastPair = stronglyConnectedBody.Last();
            _ = lastPair.Value.Items.Add(new EdgeItem<TValue>(lastPair.Key, stronglyConnectedBody.First().Key, weight));

            foreach (var kv in stronglyConnectedBody)
            {
                var _verticesCount = kv.Value.Count > Options.VerticesCount - 2 ? Options.VerticesCount - 2 : kv.Value.Count; //_verticesCount - 2 (_source, _target)
                while (_verticesCount > kv.Value.Items.Count)
                {
                    var vertex = GetRandomVertexFrom(_vertices);
                    if (!IsLoop(vertex, kv.Key)
                        && !IsContainsDuplicate(vertex, kv.Value.Items.Select(c => c.Target))
                        && !vertex.Equals(_source))
                    {
                        _mapVertexAndIsReached[vertex] = true;
                        weight = Randomizer.FromRange(Options.Range.minimum, Options.Range.maximum);
                        _ = kv.Value.Items.Add(new EdgeItem<TValue>(kv.Key, vertex, weight));
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
                if (!vertex.Equals(_target) && !MapVertexAndLists[vertex].Items.Select(c => c.Target).Contains(_target))
                {
                    weight = Randomizer.FromRange(Options.Range.minimum, Options.Range.maximum);
                    _ = MapVertexAndLists[vertex].Items.Add(new EdgeItem<TValue>(vertex, _target, weight));
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
                    && !IsContainsDuplicate(vertex, MapVertexAndLists[_source].Items.Select(c => c.Target))
                    && !vertex.Equals(_target))
                {
                    _mapVertexAndIsReached[vertex] = true;
                    var weight = Randomizer.FromRange(Options.Range.minimum, Options.Range.maximum);
                    _ = MapVertexAndLists[_source].Items.Add(new EdgeItem<TValue>(_source, vertex, weight));
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
