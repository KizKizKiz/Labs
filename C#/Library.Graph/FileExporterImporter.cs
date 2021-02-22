using System;
using System.Threading.Tasks;

namespace Library.GraphTypes
{
    public abstract class FileExporterImporter<TValue> : IFileExporterImporter<TValue>
    {
        public async Task ExportAsync(string fileName)
        {
            ValidateFileName(fileName);

            await ExportCoreAsync(fileName);
        }
        public async Task<TValue> ImportAsync(string fileName)
        {
            ValidateFileName(fileName);

            return await ImportCoreAsync(fileName);
        }

        protected abstract Task ExportCoreAsync(string fileName);
        protected abstract Task<TValue> ImportCoreAsync(string fileName);

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