﻿using System.Collections.Generic;

namespace Library.Graph.Views
{
    /// <summary>
    /// Представляет контракт представления графа.
    /// </summary>
    /// <typeparam name="TViewItem">Тип представления ребер.</typeparam>
    /// <typeparam name="TValue">Тип элементов ребер.</typeparam>
    public interface IGraphView<TViewItem, TValue> : IGraphView<TViewItem>
        where TViewItem : IGraphViewItem<TValue>
    {
        /// <summary>
        /// Возвращает множество вершин.
        /// </summary>
        IReadOnlyList<TValue> Vertices { get; }
    }
}