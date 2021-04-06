using System.Collections.Generic;

namespace Library.Graph.Views
{
    /// <summary>
    /// Представляет контракт представления графа.
    /// </summary>
    /// <typeparam name="TViewItem">Тип элементов в представлении.</typeparam>
    /// <typeparam name="TValue">Тип элементов в элементах представления.</typeparam>
    public interface IGraphView<TViewItem, TValue>
        where TViewItem : IGraphViewItem<TValue>
    {
        /// <summary>
        /// Возвращает множество вершин.
        /// </summary>
        IReadOnlyList<TValue> Vertices { get; }

        /// <summary>
        /// Возвращает элементы представления доступные только для чтения.
        /// </summary>
        IReadOnlyList<TViewItem> Items { get; }
    }
}