using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Library.Graph.Structures
{
    internal class MinPriorityQueue<TValue> : IEnumerable<TValue>
        where TValue : IComparable<TValue>
    {
        public MinPriorityQueue(int initCapacity)
        {
            if (initCapacity < 0)
            {
                throw new ArgumentException("The capacity must be greater than zero.", nameof(initCapacity));
            }

            _items = new TValue[initCapacity + 1];
            _count = 0;
        }

        public MinPriorityQueue() : this(1) { }

        public MinPriorityQueue(IEnumerable<TValue> items)
        {
            var itemsList = items.ToList();

            _count = itemsList.Count;
            _items = new TValue[_count + 1];

            for (var i = 0; i < _count; i++)
            {
                _items[i + 1] = itemsList[i];
            }
            for (var k = _count / 2; k >= 1; k--)
            {
                Correct(k);
            }
        }

        public bool IsEmpty() => _count == 0;

        public int Count() => _count;

        public TValue TakeMin()
        {
            ThrowIfEmpty();

            return _items[1];
        }

        public void Add(TValue x)
        {
            EnsureCapacity();

            _items[++_count] = x;

            BubbleUpByIndex(_count);
        }

        public TValue DeleteMin()
        {
            ThrowIfEmpty();

            var min = _items[1];

            SwapByIndexies(1, _count--);
            Correct(1);

            _items[_count + 1] = default!;

            return min;
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            while (!IsEmpty())
            {
                yield return DeleteMin();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void EnsureCapacity()
        {
            if (_count != _items.Length - 1)
            {
                return;
            }

            var temp = new TValue[_items.Length * 2];
            for (var i = 1; i <= _count; i++)
            {
                temp[i] = _items[i];
            }
            _items = temp;
        }

        private void ThrowIfEmpty()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("The collection is empty.");
            }
        }

        private void BubbleUpByIndex(int k)
        {
            while (k > 1 && IsGreaterThanSecond(k / 2, k))
            {
                SwapByIndexies(k, k / 2);
                k /= 2;
            }
        }

        private void Correct(int k)
        {
            while (2 * k <= _count)
            {
                var j = 2 * k;
                if (j < _count && IsGreaterThanSecond(j, j + 1))
                {
                    j++;
                }
                if (!IsGreaterThanSecond(k, j))
                {
                    break;
                }

                SwapByIndexies(k, j);
                k = j;
            }
        }

        private bool IsGreaterThanSecond(int i, int j) => _items[i].CompareTo(_items[j]) > 0;

        private void SwapByIndexies(int i, int j)
        {
            var swap = _items[i];
            _items[i] = _items[j];
            _items[j] = swap;
        }

        private TValue[] _items;
        private int _count;
    }
}