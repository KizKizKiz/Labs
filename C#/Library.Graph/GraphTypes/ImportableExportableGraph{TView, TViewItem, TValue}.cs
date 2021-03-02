using System;
using System.Threading.Tasks;

using Library.Views;

namespace Library.GraphTypes
{
    /// <summary>
    /// Представляет базовую реализацию всех импортируемых и экспортируемых типов графов.
    /// </summary>
    /// <typeparam name="TView">Тип представления графа.</typeparam>
    /// <typeparam name="TViewItem">Тип представления элемента графа.</typeparam>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public abstract class ImportableExportableGraph<TView, TViewItem, TValue> : Graph<TView, TViewItem, TValue>, IFileExporterImporter
        where TViewItem : IGraphViewItem<TValue>
        where TView : IGraphView<TViewItem, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>, IStringConvertible<TValue>
    {
        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="edgeType">Тип ребер графа.</param>
        public ImportableExportableGraph(EdgeType edgeType)
            : base(edgeType) { }

        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="view">Контракт представления графа.</param>
        /// <param name="edgeType">Тип ребер графа.</param>
        public ImportableExportableGraph(TView view, EdgeType edgeType)
            : base(view, edgeType)
        { }

        /// <inheritdoc/>
        public async Task ExportAsync()
        {
            var fileName = $"graph-dump-{DateTime.Now.ToString("HH-mm-ss")}.xls";

            await ExportCoreAsync(fileName);
        }

        /// <inheritdoc/>
        public async Task ImportAsync(string fileName)
        {
            ValidateFileName(fileName);

            await ImportCoreAsync(fileName);
        }

        protected abstract Task ExportCoreAsync(string fileName);
        protected abstract Task ImportCoreAsync(string fileName);

        private void ValidateFileName(string fileName)
        {
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("File name cannot be empty or has only whitespaces.", nameof(fileName));
            }
        }
    }
}