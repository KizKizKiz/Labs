using System;
using System.Threading.Tasks;

using Library.Graph.Views;
using Library.Graph.Generators;
using Library.Graph.ImportersExporters;
using Library.Graph.ConvertibleTypes;
using System.IO;

namespace Library.Graph.Types.Adjacensies
{
    public abstract class ImportableExportableAdjacensiesGraph<TValue> : Graph<AdjacensiesView<TValue>, AdjacensyViewItem<TValue>, TValue>
        where TValue : notnull, IStringConvertible<TValue>, new()
    {
        public ImportableExportableAdjacensiesGraph(AdjacensiesView<TValue> view)
            : base(view)
        { }

        public ImportableExportableAdjacensiesGraph(ViewGeneratingResult<TValue> viewGeneratingResult)
            : this(viewGeneratingResult?.View ?? throw new ArgumentNullException(nameof(viewGeneratingResult)))
        { }

        public async Task ExportAsync(IGraphViewExporter exporter)
        {
            if (exporter is null)
            {
                throw new ArgumentNullException(nameof(exporter));
            }
            await exporter.ExportAsync(View, false).ConfigureAwait(false);
        }

        public async Task ImportAsync(IGraphViewImporter importer, Stream stream)
        {
            if (importer is null)
            {
                throw new ArgumentNullException(nameof(importer));
            }
            View = await importer.ImportAdjacensiesViewAsync<TValue>(stream).ConfigureAwait(false);
        }
    }
}