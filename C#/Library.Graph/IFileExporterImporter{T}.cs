using System.Threading.Tasks;

namespace Library.GraphTypes
{
    /// <summary>
    /// Представляет контракт выгрузки и загрузки из файла.
    /// </summary>
    public interface IFileExporterImporter
    {
        /// <summary>
        /// Выгружает в файл.
        /// </summary>
        Task ExportAsync();

        /// <summary>
        /// Загружает из файла <paramref name="fileName"/>.
        /// </summary>
        Task ImportAsync(string fileName);
    }
}