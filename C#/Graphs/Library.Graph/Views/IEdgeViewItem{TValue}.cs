using System;

namespace Library.Graph.Views
{
    /// <summary>
    /// Представляет контракт элемента в представлении, основанного на массиве ребер.
    /// </summary>
    /// <typeparam name="TView">Тип представления графа.</typeparam>
    public interface IEdgeViewItem<TValue> : IGraphViewItem<TValue>
        where TValue : IEquatable<TValue>
    {
    }
}