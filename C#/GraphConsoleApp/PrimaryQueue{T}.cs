using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.Graph
{
    /// <summary>
    /// Представляет интерфейс очереди с приоритетом.
    /// </summary>
    /// <typeparam name="T">Тип данных очереди.</typeparam>
    public class PrimaryQueue<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Возвращает количество элементов.
        /// </summary>
        public int Count => _items.Count;

        /// <summary>
        /// Возвращает коллекцию элементов только для чтения.
        /// </summary>
        public IReadOnlyList<T> Items => _items;

        public static PrimaryQueue<T> CreateMinPrimaryQueue() => new PrimaryQueue<T>(
            (parentIndex, childIndex) => _items[parentIndex].CompareTo(_items[childIndex]) > 0);
        public static PrimaryQueue<T> CreateMaxPrimaryQueue() => new PrimaryQueue<T>(
            (parentIndex, childIndex) => _items[parentIndex].CompareTo(_items[childIndex]) < 0);

        public static PrimaryQueue<T> CreateMinPrimaryQueue(IEnumerable<T> sequence)
        {
            ValidateSequence(sequence);

            var pq = CreateMinPrimaryQueue();

            foreach (var item in sequence)
            {
                pq.Add(item);
            }

            return pq;
        }

        public static PrimaryQueue<T> CreateMaxPrimaryQueue(IEnumerable<T> sequence)
        {
            ValidateSequence(sequence);

            var pq = CreateMaxPrimaryQueue();

            foreach (var item in sequence)
            {
                pq.Add(item);
            }

            return pq;
        }

        /// <summary>
        /// Добавляет элемент в случае если его нет в коллекции, иначе перезаписывает существующее.
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            _items.Add(item);
            _lastAddedItemIndex++;

            Correct();
        }

        /// <summary>
        /// Возвращает итератор очереди с приоритетом.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void Correct()
        {
            var lastAddedItemIndex = _lastAddedItemIndex;
            var parentIndex = (lastAddedItemIndex - 1) / 2;

            while (_comparer(parentIndex, lastAddedItemIndex))
            {
                Swap(parentIndex, lastAddedItemIndex);
                lastAddedItemIndex = parentIndex;
                parentIndex = (parentIndex - 1) / 2;
            }
        }

        private PrimaryQueue(Func<int, int, bool> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        private void Swap(int parentIndex, int lastAddedItemIndex)
        {
            var temp = _items[lastAddedItemIndex];
            _items[lastAddedItemIndex] = _items[parentIndex];
            _items[parentIndex] = temp;
        }

        private static void ValidateSequence(IEnumerable<T> sequence)
        {
            if (sequence is null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }
            if (!sequence.Any())
            {
                throw new ArgumentException("The sequence is empty", nameof(sequence));
            }
        }

        private int _lastAddedItemIndex = -1;
        private readonly Func<int, int, bool> _comparer;
        private readonly static List<T> _items = new List<T>();
    }
}