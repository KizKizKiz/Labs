using System;
using System.Collections.Generic;

namespace Library.Graph.Views
{
    /// <summary>
    /// Представляет контракт представления графа.
    /// </summary>
    /// <typeparam name="TViewItem">Тип представления ребер.</typeparam>
    /// <typeparam name="TValue">Тип элементов ребер.</typeparam>
    public interface IGraphView<TViewItem, TValue>
        where TValue : IEquatable<TValue>
        where TViewItem : IGraphViewItem<TValue>
    {
        /// <summary>
        /// Возвращает элементы представления доступные только для чтения.
        /// </summary>
        IReadOnlyList<TViewItem> Items { get; }
    }
}