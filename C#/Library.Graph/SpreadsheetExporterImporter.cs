using System;
using System.Threading.Tasks;

namespace Library.GraphTypes
{
    public sealed class SpreadsheetExporterImporter<TValue> : FileExporterImporter<TValue>
    {
        protected override Task ExportCoreAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        protected override Task<TValue> ImportCoreAsync(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}