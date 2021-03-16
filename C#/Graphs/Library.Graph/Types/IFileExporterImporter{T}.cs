using System.Threading.Tasks;

namespace Library.Graph.Types
{
    /// <summary>
    /// Представляет контракт выгрузки и загрузки из файла.
    /// </summary>
    public interface IFileExporterImporter
    {
        /// <summary>
        /// Выгружает в файл и возвращает название файла.
        /// </summary>
        Task<string> ExportAsync();

        /// <summary>
        /// Загружает из файла <paramref name="fileName"/>.
        /// </summary>
        Task ImportAsync(string fileName);
    }
}