using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Library.Graph.Types
{
    /// <summary>
    /// Представляет элемент представления в виде списка смежности.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов.</typeparam>
    public sealed class AdjacensyItem<TValue> : IGraphViewItem<TValue>
    {
        /// <summary>
        /// Возвращает вершину списка смежности.
        /// </summary>
        [NotNull]
        public TValue Vertex { get; }

        /// <summary>
        /// Возвращает список вершин смежных с <see cref="Vertex"/>.
        /// </summary>
        [NotNull]
        public IReadOnlyList<(double weight, TValue value)> Items { get; }

        /// <summary>
        /// Конструктор элемента списка смежности.
        /// </summary>
        /// <param name="vertex">Представляет вершину списка смежности.</param>
        /// <param name="items">Представляет список вершин смежных с <param name="vertex"/>.</param>
        public AdjacensyItem(TValue vertex, IEnumerable<(double weight, TValue value)> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            Vertex = vertex ?? throw new ArgumentNullException(nameof(vertex));
            Items = items.ToList();
        }

        public override string ToString()
            => Items.Any() ? $"{Vertex} : {string.Join(" , ", Items)}" : $"{Vertex} : none";
    }
}