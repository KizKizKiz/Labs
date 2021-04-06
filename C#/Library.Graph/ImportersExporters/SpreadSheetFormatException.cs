using System;

namespace Library.Graph.ImportersExporters
{
    public sealed class SpreadSheetFormatException : FormatException
    {
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
