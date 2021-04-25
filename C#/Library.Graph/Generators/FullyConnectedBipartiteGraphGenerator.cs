using System.Collections.Generic;
using System.Linq;

using Library.Graph.ExampleConvertibleTypes;
using Library.Graph.Generators.Options;
using Library.Graph.Types;

namespace Library.Graph.Generators
{
    /// <summary>
    /// Представляет генератор полносвязных двудольных неориентированных графов.
    /// </summary>
    public sealed class FullyConnectedBipartiteGraphGenerator : GraphGenerator<BipartiteGraph<IntConvertible>, IntConvertible, FullyConnectedBipartiteGraphGeneratorOptions>
    {
        /// <summary>
        /// Конструктор генератора.
        /// </summary>
        /// <param name="options">Настройки генерации.</param>
        public FullyConnectedBipartiteGraphGenerator(FullyConnectedBipartiteGraphGeneratorOptions options)
            : base(options)
        {
        }

        /// <inheritdoc/>
        protected override GraphGeneratingResult<BipartiteGraph<IntConvertible>, IntConvertible> BuildCore()
        {
            ClearShares();
            FillShares();
            ConnectShares();

            return new GraphGeneratingResult<BipartiteGraph<IntConvertible>, IntConvertible>(
                new BipartiteGraph<IntConvertible>(
                    MapVertexAndLists.Select(c => new AdjacensyEdgeItem<IntConvertible>(c.Key, c.Value.Items)),
                    MapVertexAndLists.Keys));
        }

        private void ConnectShares()
        {
            foreach (var leftItem in _leftShare)
            {
                foreach (var rightItem in _rightShare)
                {
                    var weight = Randomizer.FromRange(Options.Range.minimum, Options.Range.maximum);

                    _ = MapVertexAndLists[leftItem].Items.Add(new EdgeItem<IntConvertible>(leftItem, rightItem, weight));
                    _ = MapVertexAndLists[rightItem].Items.Add(new EdgeItem<IntConvertible>(rightItem, leftItem, weight));
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

            var shareCount = Options.VerticesCount / 2;

            while (_leftShare.Count != shareCount)
            {
                _ = _leftShare.Add(GetRandomVertexFrom(vertices));
            }
            while (_rightShare.Count != shareCount)
            {
                var vertex = GetRandomVertexFrom(vertices);
                if (!_rightShare.Contains(vertex) && !_leftShare.Contains(vertex))
                {
                    _ = _rightShare.Add(vertex);
                }
            }
        }

        private HashSet<IntConvertible> _leftShare = new();
        private HashSet<IntConvertible> _rightShare = new();
    }
}
