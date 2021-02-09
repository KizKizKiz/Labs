using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using CsvHelper;
using CsvHelper.Configuration;

namespace ConsoleApp.Extensions
{
    public static class Extensions
    {
        public static bool IsSorted<T>(this IEnumerable<T> source)
            where T : IComparable<T>
        {
            using var enumerator = source.GetEnumerator();
            var current = default(T);
            if (enumerator.MoveNext())
                current = enumerator.Current;
            enumerator.Reset();
            var result = true;
            while (enumerator.MoveNext())
            {
                if (current.CompareTo(enumerator.Current) > 0)
                {
                    result = false;
                    break;
                }
                current = enumerator.Current;
            }
            return result;
        }

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
            var config = new CsvConfiguration(CultureInfo.CurrentCulture, delimiter: ",", hasHeaderRecord: false);

            filePath = filePath is not null ? filePath : fileName;

            using var streamWriter = new StreamWriter(filePath);
            using var csvWriter = new CsvWriter(streamWriter, config);
            csvWriter.WriteRecords(sequence);
        }
    }
}
