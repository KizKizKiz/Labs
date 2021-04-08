using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Graph.Structures
{
    internal class UnionFindStructure<TValue>
        where TValue : notnull
    {
        public UnionFindStructure(IEnumerable<TValue> elements)
        {
            if (elements is null)
            {
                throw new ArgumentNullException(nameof(elements));
            }
            if (!elements.Any())
            {
                throw new ArgumentException("Received empty sequence.", nameof(elements));
            }
            _mapVertexAndRank = elements
                .ToDictionary(
                    elem => elem,
                    elem => (elem, 0));
        }

        public bool TryUnion(TValue value1, TValue value2)
        {
            ValidateUnionings(value1, value2);

            var foundValue1 = Find(value1);
            var foundValue2 = Find(value2);

            if (foundValue1.Equals(foundValue2))
            {
                return false;
            }

            if (_mapVertexAndRank[foundValue1].Rank < _mapVertexAndRank[foundValue2].Rank)
            {
                _mapVertexAndRank[foundValue1] = (foundValue2, _mapVertexAndRank[foundValue1].Rank);
            }
            else if (_mapVertexAndRank[foundValue1].Rank > _mapVertexAndRank[foundValue2].Rank)
            {
                _mapVertexAndRank[foundValue2] = (foundValue1, _mapVertexAndRank[foundValue2].Rank);
            }
            else
            {
                _mapVertexAndRank[foundValue2] = (foundValue1, _mapVertexAndRank[foundValue2].Rank);
                _mapVertexAndRank[foundValue1] = (foundValue1, _mapVertexAndRank[foundValue1].Rank + 1);
            }

            return true;
        }

        private static void ValidateUnionings(TValue value1, TValue value2)
        {
            if (value1 is null)
            {
                throw new ArgumentNullException(nameof(value1));
            }
            if (value2 is null)
            {
                throw new ArgumentNullException(nameof(value2));
            }
        }

        private TValue Find(TValue value)
        {
            var result = value;
            while (!value.Equals(_mapVertexAndRank[value].Vertex))
            {
                _mapVertexAndRank[value] = (_mapVertexAndRank[_mapVertexAndRank[value].Vertex].Vertex, _mapVertexAndRank[value].Rank);
                value = _mapVertexAndRank[value].Vertex;
            }
            return result;
        }

        private readonly Dictionary<TValue, (TValue Vertex, int Rank)> _mapVertexAndRank = new Dictionary<TValue, (TValue Vertex, int Rank)>();
    }
}