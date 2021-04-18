using System.Collections.Generic;
using System.Linq;

using Library.Graph.Generators.Options;
using Library.Graph.Types;

namespace Library.Graph.Generators
{
    /// <summary>
    /// Представляет генератор двудольных неориентированных графов.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public sealed class BipartiteGraphGenerator<TValue> : GraphGenerator<BipartiteGraph<TValue>, TValue, BipartiteGraphGeneratorOptions<TValue>>
        where TValue : notnull
    {
        /// <summary>
        /// Конструктор генератора.
        /// </summary>
        /// <param name="options">Настройки генерации.</param>
        public BipartiteGraphGenerator(BipartiteGraphGeneratorOptions<TValue> options)
            : base(options)
        {
        }

        /// <inheritdoc/>
        protected override GraphGeneratingResult<BipartiteGraph<TValue>, TValue> BuildCore()
        {
            ClearShares();
            FillShares();
            ConnectShares();

            return new GraphGeneratingResult<BipartiteGraph<TValue>, TValue>(
                new BipartiteGraph<TValue>(
                    MapVertexAndLists.Select(c => new AdjacensyEdgeItem<TValue>(c.Key, c.Value.Items)),
                    MapVertexAndLists.Keys));
        }

        private void ConnectShares()
        {
            var primaryShareSet = _leftShare.Count >= _rightShare.Count ? _leftShare : _rightShare;
            var secondaryShareList = (_leftShare == primaryShareSet ? _rightShare : _leftShare).ToList();
            var mapVertexAndIsConnected = secondaryShareList.ToDictionary(v => v, _ => false);

            var primaryShareList = primaryShareSet.ToList();

            var firstPrimaryShare = primaryShareList[0];
            var lastPrimaryShare = primaryShareList[^1];
            var lastSecondaryShare = secondaryShareList[^1];

            var weight = Randomizer.FromRange(Options.Range.minimum, Options.Range.maximum);

            _ = MapVertexAndLists[lastPrimaryShare].Items.Add(new EdgeItem<TValue>(lastPrimaryShare, lastSecondaryShare, weight));
            _ = MapVertexAndLists[lastSecondaryShare].Items.Add(new EdgeItem<TValue>(lastSecondaryShare, lastPrimaryShare, weight));

            foreach (var secondaryVertex in secondaryShareList)
            {
                weight = Randomizer.FromRange(Options.Range.minimum, Options.Range.maximum);

                _ = MapVertexAndLists[firstPrimaryShare].Items.Add(new EdgeItem<TValue>(firstPrimaryShare, secondaryVertex, weight));
                _ = MapVertexAndLists[secondaryVertex].Items.Add(new EdgeItem<TValue>(secondaryVertex, firstPrimaryShare, weight));
            }

            foreach (var primaryVertex in primaryShareList.Skip(1).TakeWhile(v => !v.Equals(lastPrimaryShare)))
            {
                var connectVertexCount = Randomizer.FromRange(1, secondaryShareList.Count);

                while (connectVertexCount != MapVertexAndLists[primaryVertex].Items.Count)
                {
                    var secondaryVertex = GetRandomVertexFrom(secondaryShareList);
                    if (!IsContainsDuplicate(secondaryVertex, MapVertexAndLists[primaryVertex].Items.Select(c => c.Target)))
                    {
                        weight = Randomizer.FromRange(Options.Range.minimum, Options.Range.maximum);

                        _ = MapVertexAndLists[primaryVertex].Items.Add(new EdgeItem<TValue>(primaryVertex, secondaryVertex, weight));
                        _ = MapVertexAndLists[secondaryVertex].Items.Add(new EdgeItem<TValue>(secondaryVertex, primaryVertex, weight));

                        mapVertexAndIsConnected[secondaryVertex] = true;
                    }
                }
            }
        }

        private void ClearShares()
        {
            _leftShare = new();
            _rightShare = new();
        }

        private void FillShares()
        {
            var vertices = MapVertexAndLists.Keys.ToList();

            var leftShareCount = Randomizer.FromRange(1, Options.VerticesCount);
            var rightShareCount = Options.VerticesCount - leftShareCount;

            while (_leftShare.Count != leftShareCount)
            {
                _ = _leftShare.Add(GetRandomVertexFrom(vertices));
            }
            while (_rightShare.Count != rightShareCount)
            {
                var vertex = GetRandomVertexFrom(vertices);
                if (!_rightShare.Contains(vertex) && !_leftShare.Contains(vertex))
                {
                    _ = _rightShare.Add(vertex);
                }
            }
        }

        private HashSet<TValue> _leftShare = new();
        private HashSet<TValue> _rightShare = new();
    }
}
