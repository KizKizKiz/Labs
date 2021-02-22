using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using CsvHelper;
using CsvHelper.Configuration;

namespace Library.GraphTypes
{
    public static class Extensions
    {
        /// <summary>
        /// Создает и записывает в файл находящийся по пути <paramref name="filePath"/> последовательность в формате CSV.
        /// </summary>
        /// <param name="filePath">Путь до файла.</param>
        public static void DumpToCSV<T>(this IEnumerable<T> sequence, string filePath = null)
        {
            if (sequence is null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }
            if (!sequence.Any())
            {
                throw new ArgumentException("Sequence is empty.", nameof(sequence));
            }

            var fileName = $"Sequence_{DateTime.UtcNow.ToString("HH-mm-ss")}.csv";
            var config = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = false
            };

            filePath = filePath is not null ? filePath : fileName;

            using var streamWriter = new StreamWriter(filePath);
            using var csvWriter = new CsvWriter(streamWriter, config);
            csvWriter.WriteRecords(sequence);
        }
    }
}
