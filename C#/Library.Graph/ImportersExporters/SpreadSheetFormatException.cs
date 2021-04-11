using System;

namespace Library.Graph.ImportersExporters
{
    /// <summary>
    /// Представляет класс, сигнализирующий об ошибках чтения графа из файлов в формате SpreadSheet.
    /// </summary>
    public sealed class SpreadSheetFormatException : FormatException
    {
        public SpreadSheetFormatException()
        {
        }

        public SpreadSheetFormatException(string message)
            : base(message)
        {
        }

        public SpreadSheetFormatException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
