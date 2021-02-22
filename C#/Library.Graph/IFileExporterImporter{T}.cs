using System.Threading.Tasks;

namespace Library.GraphTypes
{
    public interface IFileExporterImporter<T>
    {
        Task ExportAsync(string fileName);
        Task<T> ImportAsync(string fileName);
    }
}