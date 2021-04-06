using System;
using System.Threading.Tasks;
using System.IO;

using Library.Graph.Views;
using Library.Graph.ImportersExporters;
using Library.Graph.ConvertibleTypes;

namespace Library.Graph.Types.Edges
{
    public abstract class ImportableExportableEdgesGraph<TValue> : EdgesGraph<TValue>
        where TValue : notnull, IStringConvertible<TValue>, new()
    {
        public ImportableExportableEdgesGraph(EdgesView<TValue> view, bool isWeighted)
            : base(view)
        { }

        public async Task ExportAsync(IGraphViewExporter exporter)
        {
            if (exporter is null)
            {
                throw new ArgumentNullException(nameof(exporter));
            }
            //await exporter.ExportAsync(View, View.IsWeighted).ConfigureAwait(false);
        }

        public async Task ImportAsync(IGraphViewImporter importer, Stream stream)
        {
            if (importer is null)
            {
                throw new ArgumentNullException(nameof(importer));
            }
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            View = await importer.ImportEdgesViewAsync<TValue>(stream).ConfigureAwait(false);
        }
    }
}