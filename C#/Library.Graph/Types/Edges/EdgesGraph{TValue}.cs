using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Library.Graph.Generators;
using Library.Graph.Views;

namespace Library.Graph.Types.Edges
{
    /// <summary>
    /// Представляет базовую реализацию всех типов графов, основанных на массивах ребер.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public abstract class EdgesGraph<TValue> : Graph<EdgesView<TValue>, EdgesViewItem<TValue>, TValue>
        where TValue : notnull
    {
        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="view">Представление графа на основе ребер.</param>
        /// <param name="edgeType">Тип ребер графа.</param>
        public EdgesGraph(EdgesView<TValue> view)
            : base(view)
        { }

        protected static EdgesView<TValue> ToEdgesViewFromResult(ViewGeneratingResult<TValue> generatingResult, bool isWeighted)
        {
            if (generatingResult is null)
            {
                throw new ArgumentNullException(nameof(generatingResult));
            }
            return new EdgesView<TValue>(CreateEdges(), generatingResult.View.Vertices, isWeighted);

            IEnumerable<EdgesViewItem<TValue>> CreateEdges()
            {
                var random = new Random((int)DateTime.Now.Ticks & 0x0000ffff);
                var edges = new List<EdgesViewItem<TValue>>();
                foreach (var item in generatingResult.View.Items)
                {
                    if (!item.Items.Any())
                    {
                        edges.Add(new EdgesViewItem<TValue>(item.Vertex));
                    }
                    else
                    {
                        edges.AddRange(item.Items.Select(i => isWeighted ? 
                            new EdgesViewItem<TValue>(item.Vertex, i, random.Next(-100, 100)):
                            new EdgesViewItem<TValue>(item.Vertex, i)));
                    }
                }
                return edges;
            }
        }
    }
}