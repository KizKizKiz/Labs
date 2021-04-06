using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Library.Graph.ConvertibleTypes;
using Library.Graph.Views;
using OfficeOpenXml;

namespace Library.Graph.ImportersExporters
{
    public sealed class SpreadSheetImporterExporter : IGraphViewExporter, IGraphViewImporter
    {
        public string? FolderPath { get; }

        public SpreadSheetImporterExporter()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public SpreadSheetImporterExporter(string folderPath)
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

        public async Task ExportAsync<TValue>(AdjacensiesView<TValue> view, bool isOriented)
            where TValue : notnull, IStringConvertible<TValue>
        {
            ValidateView(view);

            var edgesView = ToEdgesView(view);

            await ExportAsync(edgesView, false).ConfigureAwait(false);
        }

        public async Task ExportAsync<TValue>(EdgesView<TValue> view, bool isOriented)
            where TValue : notnull, IStringConvertible<TValue>
        {
            ValidateView(view);

            string path = CreateFullPath();
            using var package = new ExcelPackage(new FileInfo(path));
            var worksheet = package.Workbook.Worksheets.Add("GRAPH_DUMP");

            worksheet.Cells[1, 1].Value = "Source";
            worksheet.Cells[1, 2].Value = "Target";
            worksheet.Cells[1, 3].Value = "Weight";
            worksheet.Cells[1, 4].Value = "Type";
            worksheet.Cells[1, 5].Value = "Label";

            for (int i = 0; i < view.Items.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = view.Items[i].First.ToString();
                if (view.Items[i].Second is not null && view.Items[i].Second!.IsNotDefaultValue)
                {
                    worksheet.Cells[i + 2, 2].Value = view.Items[i].Second!.ToString();
                    worksheet.Cells[i + 2, 3].Value = view.Items[i].Weight.HasValue ? view.Items[i].Weight : 0;
                    worksheet.Cells[i + 2, 4].Value = isOriented ? "Directed" : "Undirected";
                    worksheet.Cells[i + 2, 5].Value = $"'{view.Items[i].First}' -> '{view.Items[i].Second}'";
                }
            }

            await package.SaveAsync().ConfigureAwait(false);
        }

        public Task<AdjacensiesView<TValue>> ImportAdjacensiesViewAsync<TValue>(Stream stream)
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

            return Task.FromResult(ToAdjacensiesView(CreateEdgesView<TValue>(worksheet)));
        }

        public Task<EdgesView<TValue>> ImportEdgesViewAsync<TValue>(Stream stream)
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

            return Task.FromResult(CreateEdgesView<TValue>(worksheet));
        }

        private static EdgesView<TValue> CreateEdgesView<TValue>(ExcelWorksheet worksheet)
            where TValue : notnull, IStringConvertible<TValue>, new()
        {
            var edges = new List<EdgesViewItem<TValue>>();
            var vertices = new HashSet<TValue>();
            string edgeType = null!;
            for (int i = 2; i <= worksheet.Dimension.End.Row; i++) // int i = 1 Skip headers
            {
                var value = new TValue();
                TValue first = GetSource(worksheet, i, value);
                TValue second = GetTarget(worksheet, i, value);
                double weight = GetWeight(worksheet, i);

                edgeType = GetEdgeType(worksheet, edgeType, i);

                edges.Add(EqualityComparer<TValue>.Default.Equals(second, default) ?
                    new EdgesViewItem<TValue>(first) :
                    new EdgesViewItem<TValue>(first, second, weight));

                vertices.Add(first);
                if (!EqualityComparer<TValue>.Default.Equals(second, default))
                {
                    vertices.Add(second);
                }
            }

            return new EdgesView<TValue>(edges, vertices, _mapEdgeTypeAndBool[edgeType]);
        }

        private static string GetEdgeType(ExcelWorksheet worksheet, string edgeType, int i)
        {
            var value = worksheet.Cells[i, 4].Value;

            if (string.IsNullOrWhiteSpace(value.ToString())
                || !_mapEdgeTypeAndBool.ContainsKey(value.ToString()!))
            {
                throw new SpreadSheetFormatException($"Received a bad edge type. Possible edge types: {string.Join(',', _mapEdgeTypeAndBool.Keys)}");
            }
            if (edgeType is not null && !edgeType.Equals(value.ToString(), StringComparison.InvariantCultureIgnoreCase))
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
                    || worksheet.Cells[1, 1].Value.ToString()!.Equals("Source"))
                {
                    throw new SpreadSheetFormatException("'Source' column is not presented.");
                }
                if (string.IsNullOrWhiteSpace(worksheet.Cells[1, 2].Value.ToString())
                    || worksheet.Cells[1, 2].Value.ToString()!.Equals("Target"))
                {
                    throw new SpreadSheetFormatException("'Target' column is not presented.");
                }
                if (string.IsNullOrWhiteSpace(worksheet.Cells[1, 3].Value.ToString())
                    || worksheet.Cells[1, 3].Value.ToString()!.Equals("Weight"))
                {
                    throw new SpreadSheetFormatException("'Weight' column is not presented.");
                }
                if (string.IsNullOrWhiteSpace(worksheet.Cells[1, 4].Value.ToString())
                    || worksheet.Cells[1, 4].Value.ToString()!.Equals("Type"))
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
            var fileName = $"graph-{DateTime.Now.ToString("HH-MM-ss")}.xlsx";
            var path = FolderPath is not null ? Path.Combine(FolderPath, fileName) : fileName;
            return path;
        }

        private EdgesView<TValue> ToEdgesView<TValue>(AdjacensiesView<TValue> view)
            where TValue : notnull
        {
            if (view is null)
            {
                throw new ArgumentNullException(nameof(view));
            }
            return CreateEdgesView();

            EdgesView<TValue> CreateEdgesView()
            {
                var edges = new List<EdgesViewItem<TValue>>();
                foreach (var item in view.Items)
                {
                    if (!item.Items.Any())
                    {
                        edges.Add(new EdgesViewItem<TValue>(item.Vertex));
                    }
                    else
                    {
                        edges.AddRange(item.Items.Select(i => new EdgesViewItem<TValue>(item.Vertex, i)));
                    }
                }
                return new EdgesView<TValue>(edges, view.Vertices, false);
            }
        }

        private AdjacensiesView<TValue> ToAdjacensiesView<TValue>(EdgesView<TValue> view)
            where TValue : notnull
        {
            if (view is null)
            {
                throw new ArgumentNullException(nameof(view));
            }
            return CreateAdjacensiesView();

            AdjacensiesView<TValue> CreateAdjacensiesView()
            {
                var mapVertexAndList = new Dictionary<TValue, List<TValue>>();
                foreach (var item in view.Items)
                {
                    if (!mapVertexAndList.ContainsKey(item.First))
                    {
                        mapVertexAndList[item.First] = new List<TValue>();
                    }
                    if (item.Second is not null)
                    {
                        mapVertexAndList[item.First].Add(item.Second!);
                    }
                }
                return new AdjacensiesView<TValue>(
                    mapVertexAndList.Select(
                        kv => new AdjacensyViewItem<TValue>(kv.Key, kv.Value)), 
                    view.Vertices);
            }
        }

        private static void ValidateView<TViewItem, TValue>(IGraphView<TViewItem, TValue> view)
            where TValue : notnull
            where TViewItem : IGraphViewItem<TValue>
        {
            if (view is null)
            {
                throw new ArgumentNullException(nameof(view));
            }
            if (view.Items is null)
            {
                throw new ArgumentNullException(nameof(view.Items));
            }
            if (!view.Items.Any())
            {
                throw new ArgumentException("The items collections is empty.", nameof(view.Items));
            }
        }

        private static Dictionary<string, bool> _mapEdgeTypeAndBool = new()
        {
            { "Directed", true },
            { "Undirected", false },
        };
    }
}
