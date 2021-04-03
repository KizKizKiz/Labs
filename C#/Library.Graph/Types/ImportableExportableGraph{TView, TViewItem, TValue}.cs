using System;
using System.Threading.Tasks;

using Library.Graph.Generators;
using Library.Graph.ImportersExporters;
using Library.Graph.Views;

namespace Library.Graph.Types
{
    /// <summary>
    /// Представляет базовую реализацию всех импортируемых и экспортируемых типов графов.
    /// </summary>
    /// <typeparam name="TView">Тип представления графа.</typeparam>
    /// <typeparam name="TViewItem">Тип представления элемента графа.</typeparam>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public abstract class ImportableExportableGraph<TView, TViewItem, TValue> : Graph<TView, TViewItem, TValue>
        where TViewItem : IGraphViewItem<TValue>
        where TView : IGraphView<TViewItem, TValue>
        where TValue : notnull
    {
        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="view">Контракт представления графа.</param>
        /// <param name="edgeType">Тип ребер графа.</param>
        public ImportableExportableGraph(TView view)
            : base(view)
        { }

        public ImportableExportableGraph(ViewGeneratingResult<TView, TViewItem, TValue> viewGeneratingResult)
            : base(viewGeneratingResult)
        { }

        /// <inheritdoc/>
        public async Task ExportAsync(IGraphExporter exporter)
        {
            if (exporter is null)
            {
                throw new ArgumentNullException(nameof(exporter));
            }
            await exporter.ExportAsync<TView, TViewItem, TValue>(View ?? throw new ArgumentNullException(nameof(exporter)));
        }

        /// <inheritdoc/>
        public async Task ImportAsync(IGraphViewImporter importer)
        {
            if (importer is null)
            {
                throw new ArgumentNullException(nameof(importer));
            }
            View = await importer.ImportAsync<TView, TViewItem, TValue>();
        }
    }
}