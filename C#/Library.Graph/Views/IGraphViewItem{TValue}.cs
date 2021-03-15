using System;

namespace Library.Graph.Views
{
    /// <summary>
    /// Представляет контракт ребра в представлении.
    /// </summary>
    /// <typeparam name="TView">Тип представления графа.</typeparam>
    public interface IGraphViewItem<TValue>
        where TValue : IEquatable<TValue>
    {
    }
}