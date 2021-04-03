using System;
using Library.Graph.Generators;
using Library.Graph.Views;

namespace Library.Graph.Types
{
    /// <summary>
    /// Представляет базовую реализацию всех типов графов.
    /// </summary>
    /// <typeparam name="TView">Тип представления графа.</typeparam>
    /// <typeparam name="TViewItem">Тип элемента представления графа.</typeparam>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public abstract class Graph<TView, TViewItem, TValue>
        where TViewItem : IGraphViewItem<TValue>
        where TView : IGraphView<TViewItem, TValue>
    {
        /// <summary>
        /// Возвращает контракт представления графа.
        /// </summary>
        public TView View { get; protected set; }

        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="view">Контракт представления графа.</param>
        public Graph(TView view)
        {
            View = view ?? throw new ArgumentNullException(nameof(view));
        }

        public Graph(ViewGeneratingResult<TView, TViewItem, TValue> viewGeneratingResult)
        {
            if (viewGeneratingResult is null)
            {
                throw new ArgumentNullException(nameof(viewGeneratingResult));
            }
            View = viewGeneratingResult.View;
        }
    }
}