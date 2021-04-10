using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using OfficeOpenXml;

using Library.Graph.Types;
using Library.Graph.Converter;

namespace Library.Graph.ImportersExporters
{
    public sealed class SpreadSheetImporterExporter : IGraphExporter, IGraphImporter
    {
        public string? FolderPath { get; }

        public SpreadSheetImporterExporter(GraphConverter graphConverter)
        {
            _converter = graphConverter ?? throw new ArgumentNullException(nameof(graphConverter));
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public SpreadSheetImporterExporter(GraphConverter graphConverter, string folderPath)
            : this(graphConverter)
        {
            if (folderPath is null)
            {
                throw new ArgumentNullException(nameof(folderPath));
            }
            if (string.IsNullOrWhiteSpace(folderPath))
            {
                throw new ArgumentException("The folder path cannot be empty or consist of whitespaces.", nameof(folderPath));
            }
            if (Directory.Exists(folderPath))
            {
                throw new ArgumentException("The folder path doesn't exist on disk.");
            }
            FolderPath = folderPath;
        }

        public async Task ExportAsync<TValue>(AdjacensiesBasedGraph<TValue> graph)
            where TValue : notnull, IStringConvertible<TValue>
        {
            ValidateGraph(graph);

            var edgesView = _converter.Convert(graph, false);

            await ExportAsync(edgesView).ConfigureAwait(false);
        }

        public async Task ExportAsync<TValue>(EdgesBasedGraph<TValue> graph)
            where TValue : notnull, IStringConvertible<TValue>
        {
            ValidateGraph(graph);

            var path = CreateFullPath();
            using var package = new ExcelPackage(new FileInfo(path));
            var worksheet = package.Workbook.Worksheets.Add("GRAPH_DUMP");

            worksheet.Cells[1, 1].Value = "Source";
            worksheet.Cells[1, 2].Value = "Target";
            worksheet.Cells[1, 3].Value = "Weight";
            worksheet.Cells[1, 4].Value = "Type";
            worksheet.Cells[1, 5].Value = "Label";

            for (var i = 0; i < graph.Items.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = graph.Items[i].First.ToString();
                if (graph.Items[i].Second is not null && graph.Items[i].Second!.IsNotDefaultValue)
                {
                    worksheet.Cells[i + 2, 2].Value = graph.Items[i].Second!.ToString();
                    worksheet.Cells[i + 2, 3].Value = graph.Items[i].Weight.HasValue ? graph.Items[i].Weight : 0;
                    worksheet.Cells[i + 2, 4].Value = graph.IsOriented ? "Directed" : "Undirected";
                    worksheet.Cells[i + 2, 5].Value = $"'{graph.Items[i].First}' -> '{graph.Items[i].Second}'";
                }
            }

            await package.SaveAsync().ConfigureAwait(false);
        }

        public Task<AdjacensiesBasedGraph<TValue>> ImportAdjacensiesViewAsync<TValue>(Stream stream)
            where TValue : notnull, IStringConvertible<TValue>, new()
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            stream.Position = 0;

            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets.Count != 0 ?
                throw new InvalidOperationException("Received workbook without worksheets.") :
                package.Workbook.Worksheets[0];

            VerifyHeaders(worksheet);

            return Task.FromResult(_converter.Convert(CreateEdgesGraph<TValue>(worksheet)));
        }

        public Task<EdgesBasedGraph<TValue>> ImportEdgesViewAsync<TValue>(Stream stream)
            where TValue : notnull, IStringConvertible<TValue>, new()
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            stream.Position = 0;

            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets.Count != 0 ?
                throw new InvalidOperationException("Received workbook without worksheets.") :
                package.Workbook.Worksheets[0];

            VerifyHeaders(worksheet);

            return Task.FromResult(CreateEdgesGraph<TValue>(worksheet));
        }

        private EdgesBasedGraph<TValue> CreateEdgesGraph<TValue>(ExcelWorksheet worksheet)
            where TValue : notnull, IStringConvertible<TValue>, new()
        {
            var edges = new List<EdgesViewItem<TValue>>();
            var vertices = new HashSet<TValue>();
            string edgeType = null!;
            for (var i = 2; i <= worksheet.Dimension.End.Row; i++) // int i = 2 Skip headers
            {
                var value = new TValue();
                var first = GetSource(worksheet, i, value);
                var second = GetTarget(worksheet, i, value);
                var weight = GetWeight(worksheet, i);

                edgeType = GetEdgeType(worksheet, edgeType, i);

                edges.Add(EqualityComparer<TValue>.Default.Equals(second, default) ?
                    new EdgesViewItem<TValue>(first) :
                    new EdgesViewItem<TValue>(first, second, weight));

                _ = vertices.Add(first);
                if (!EqualityComparer<TValue>.Default.Equals(second, default))
                {
                    _ = vertices.Add(second);
                }
            }

            return new EdgesBasedGraph<TValue>(
                edges,
                vertices,
                !edges.All(c => c.Weight == default),
                _mapEdgeTypeAndBool[edgeType]);
        }

        private static string GetEdgeType(ExcelWorksheet worksheet, string edgeType, int i)
        {
            var value = worksheet.Cells[i, 4].Value;

            if (string.IsNullOrWhiteSpace(value.ToString())
                || !_mapEdgeTypeAndBool.ContainsKey(value.ToString()!))
            {
                throw new SpreadSheetFormatException($"Received a bad edge type. Possible edge types: {string.Join(',', _mapEdgeTypeAndBool.Keys)}");
            }
            if (edgeType is not null && !edgeType.Equals(value.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                throw new SpreadSheetFormatException($"Edge type has changed. Please make sure that all edges have the same type (Received edge type: '{value}').");
            }
            edgeType = value.ToString()!;
            return edgeType;
        }

        private static double GetWeight(ExcelWorksheet worksheet, int i)
        {
            var value = worksheet.Cells[i, 3].Value;

            if (string.IsNullOrWhiteSpace(value.ToString())
                || double.TryParse(value.ToString()!, out var weight))
            {
                throw new SpreadSheetFormatException($"Received a bad weight. Try to specify default value to weight (Column index is 3, row index is {i}.");
            }

            return weight;
        }

        private static TValue GetTarget<TValue>(ExcelWorksheet worksheet, int i, TValue converterValue)
            where TValue : notnull, IStringConvertible<TValue>, new()
        {
            var value = worksheet.Cells[i, 2].Value;

            TValue second = default!;
            if (!string.IsNullOrWhiteSpace(value.ToString()))
            {
                second = converterValue.ConvertFromString(value.ToString()!);
            }

            return second;
        }

        private static TValue GetSource<TValue>(ExcelWorksheet worksheet, int i, TValue converterValue)
            where TValue : notnull, IStringConvertible<TValue>, new()
        {
            var value = worksheet.Cells[i, 1].Value;
            if (string.IsNullOrWhiteSpace(value.ToString()))
            {
                throw new SpreadSheetFormatException("Value from 'Source' column is null or empty or has only whitespaces.");
            }
            var first = converterValue.ConvertFromString(value.ToString()!);

            return first;
        }

        private static void VerifyHeaders(ExcelWorksheet worksheet)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(worksheet.Cells[1, 1].Value.ToString())
                    || worksheet.Cells[1, 1].Value.ToString()!.Equals("Source", StringComparison.Ordinal))
                {
                    throw new SpreadSheetFormatException("'Source' column is not presented.");
                }
                if (string.IsNullOrWhiteSpace(worksheet.Cells[1, 2].Value.ToString())
                    || worksheet.Cells[1, 2].Value.ToString()!.Equals("Target", StringComparison.Ordinal))
                {
                    throw new SpreadSheetFormatException("'Target' column is not presented.");
                }
                if (string.IsNullOrWhiteSpace(worksheet.Cells[1, 3].Value.ToString())
                    || worksheet.Cells[1, 3].Value.ToString()!.Equals("Weight", StringComparison.Ordinal))
                {
                    throw new SpreadSheetFormatException("'Weight' column is not presented.");
                }
                if (string.IsNullOrWhiteSpace(worksheet.Cells[1, 4].Value.ToString())
                    || worksheet.Cells[1, 4].Value.ToString()!.Equals("Type", StringComparison.Ordinal))
                {
                    throw new SpreadSheetFormatException("'Type' column is not presented.");
                }
            }
            catch (SpreadSheetFormatException exception)
            {
                throw new SpreadSheetFormatException("Required order non compliance.\nOrder: Source, Target, Weight, Type.", exception);
            }
        }

        private string CreateFullPath()
        {
            var fileName = $"graph-{DateTime.Now:HH-MM-ss}.xlsx";
            var path = FolderPath is not null ? Path.Combine(FolderPath, fileName) : fileName;
            return path;
        }

        private static void ValidateGraph<TViewItem, TValue>(IGraph<TViewItem, TValue> graph)
            where TValue : notnull
            where TViewItem : IGraphViewItem<TValue>
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            if (graph.Items is null)
            {
                throw new ArgumentException(nameof(graph.Items));
            }
            if (!graph.Items.Any())
            {
                throw new ArgumentException("The items collections is empty.", nameof(graph));
            }
        }

        private static readonly Dictionary<string, bool> _mapEdgeTypeAndBool = new()
        {
            { "Directed", true },
            { "Undirected", false },
        };
        private readonly GraphConverter _converter;
    }
}