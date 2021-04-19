using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using OfficeOpenXml;

using Library.Graph.Types;

namespace Library.Graph.ImportersExporters
{
    /// <summary>
    /// Представляет реализацию экспорта и импорта графа из\в SpreadSheet.
    /// </summary>
    public sealed class SpreadSheetImporterExporter : IGraphExporter, IGraphImporter
    {
        /// <summary>
        /// Путь до папки, содержащей графы.
        /// </summary>
        public string? FolderPath { get; }

        /// <summary>
        /// Конструктор экспорта и импорта графа.
        /// </summary>
        public SpreadSheetImporterExporter()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        /// <summary>
        /// Конструктор экспорта и импорта графа.
        /// </summary>
        /// <param name="folderPath">Путь до папки, содержащей графы.</param>
        public SpreadSheetImporterExporter(string folderPath)
            : this()
        {
            if (folderPath is null)
            {
                throw new ArgumentNullException(nameof(folderPath));
            }
            if (string.IsNullOrWhiteSpace(folderPath))
            {
                throw new ArgumentException("The folder path cannot be empty or consist of whitespaces.", nameof(folderPath));
            }
            if (!Directory.Exists(folderPath))
            {
                throw new ArgumentException("The folder path doesn't exist on disk.");
            }
            FolderPath = folderPath;
        }

        /// <inheritdoc/>
        public async Task ExportAsync<TValue>(Graph<TValue> graph)
            where TValue : notnull, IEqualityComparer<TValue>, IEquatable<TValue>, IStringConvertible<TValue>
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

            var rowsProcessed = 0;
            for (var i = 0; ;)
            {
                var adjacensies = graph.Items.Values.ToList();
                if (adjacensies[rowsProcessed].Items.Any())
                {
                    foreach (var edge in adjacensies[rowsProcessed].Items)
                    {
                        worksheet.Cells[i + 2, 1].Value = edge.Source.ToString();
                        worksheet.Cells[i + 2, 2].Value = edge.Target.ToString();
                        worksheet.Cells[i + 2, 3].Value = edge.Weight ?? 0;
                        worksheet.Cells[i + 2, 4].Value = graph.IsOriented ? "Directed" : "Undirected";
                        worksheet.Cells[i++ + 2, 5].Value = $"'{edge.Source}' -> '{edge.Target}'";
                    }
                }
                else
                {
                    worksheet.Cells[i++ + 2, 1].Value = adjacensies[rowsProcessed].Vertex.ToString();
                }
                if (graph.Items.Count == ++rowsProcessed)
                {
                    break;
                }
            }

            await package.SaveAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<TransportNetworkGraph<TValue>> ImportTransportNetworkAsync<TValue>(Stream stream)
            where TValue : notnull, IStringConvertible<TValue>, IEqualityComparer<TValue>, IEquatable<TValue>, new()
        {
            var graph = await ImportGraphAsync<TValue>(stream);
            return new TransportNetworkGraph<TValue>(graph.Items.Values, graph.Vertices);
        }

        /// <inheritdoc/>
        public async Task<BipartiteGraph<TValue>> ImportBipartiteGraphAsync<TValue>(Stream stream)
            where TValue : notnull, IStringConvertible<TValue>, IEqualityComparer<TValue>, IEquatable<TValue>, new()
        {
            var graph = await ImportGraphAsync<TValue>(stream);
            return new BipartiteGraph<TValue>(graph.Items.Values, graph.Vertices);
        }

        /// <inheritdoc/>
        public Task<Graph<TValue>> ImportGraphAsync<TValue>(Stream stream)
            where TValue : notnull, IStringConvertible<TValue>, IEqualityComparer<TValue>, IEquatable<TValue>, new()
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            using (stream)
            {
                stream.Position = 0;

                using var package = new ExcelPackage(stream);
                var worksheet = package.Workbook.Worksheets.Count == 0 ?
                    throw new InvalidOperationException("Received workbook without worksheets.") :
                    package.Workbook.Worksheets[0];

                VerifyHeaders(worksheet);

                return Task.FromResult(CreateGraph<TValue>(worksheet));
            }
        }

        private Graph<TValue> CreateGraph<TValue>(ExcelWorksheet worksheet)
            where TValue : notnull, IStringConvertible<TValue>, IEqualityComparer<TValue>, IEquatable<TValue>, new()
        {
            var mapVertexAndEdges = new Dictionary<TValue, List<EdgeItem<TValue>>>();
            var edgeType = string.Empty;

            for (var i = 2; i <= worksheet.Dimension.End.Row; i++) // int i = 2 Skip headers
            {
                var value = new TValue();
                var first = GetSource(worksheet, i, value);
                var valueAndIsDefault = GetTarget(worksheet, i, value);
                var weight = GetWeight(worksheet, i);

                edgeType = GetEdgeType(worksheet, edgeType, i);

                if (!mapVertexAndEdges.ContainsKey(first))
                {
                    mapVertexAndEdges[first] = new();
                }
                if (!valueAndIsDefault.isDefault)
                {
                    if (!mapVertexAndEdges.ContainsKey(valueAndIsDefault.value))
                    {
                        mapVertexAndEdges[valueAndIsDefault.value] = new();
                    }
                    mapVertexAndEdges[first].Add(new EdgeItem<TValue>(first, valueAndIsDefault.value, weight));
                }
            }

            return new Graph<TValue>(
                mapVertexAndEdges.Select(c => new AdjacensyEdgeItem<TValue>(c.Key, c.Value)),
                mapVertexAndEdges.Keys,
                _mapEdgeTypeAndBool[edgeType]);
        }

        private static string GetEdgeType(ExcelWorksheet worksheet, string edgeType, int i)
        {
            var value = worksheet.Cells[i, 4].Value;

            if (worksheet.Cells[i, 2].Value is not null
                && (string.IsNullOrWhiteSpace(value.ToString()) || !_mapEdgeTypeAndBool.ContainsKey(value.ToString()!)))
            {
                throw new SpreadSheetFormatException($"Received a bad edge type. Possible edge types: {string.Join(',', _mapEdgeTypeAndBool.Keys)}");
            }
            if (worksheet.Cells[i, 2].Value is null && !string.IsNullOrEmpty(value?.ToString()))
            {
                throw new SpreadSheetFormatException($"Target is empty, but edge type is '{value}'.");
            }
            if (worksheet.Cells[i, 2].Value is not null &&
                !string.IsNullOrEmpty(edgeType) && !edgeType.Equals(value?.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                throw new SpreadSheetFormatException($"Edge type has changed. Please make sure that all edges have the same type (Received edge type: '{value}').");
            }

            edgeType = value is not null ? value.ToString()! : edgeType;
            return edgeType;
        }

        private static double GetWeight(ExcelWorksheet worksheet, int i)
        {
            var value = worksheet.Cells[i, 3].Value;

            var weight = 0.0;
            if (worksheet.Cells[i, 2].Value is not null
                && (string.IsNullOrWhiteSpace(value?.ToString()) || !double.TryParse(value.ToString()!, out weight)))
            {
                throw new SpreadSheetFormatException($"Received a bad weight. Try to specify default value to weight (Column index is 3, row index is {i}.");
            }

            return weight;
        }

        private static (TValue value, bool isDefault) GetTarget<TValue>(ExcelWorksheet worksheet, int i, TValue converterValue)
            where TValue : notnull, IStringConvertible<TValue>, new()
        {
            var value = worksheet.Cells[i, 2].Value;

            if (!string.IsNullOrWhiteSpace(value?.ToString()))
            {
                return (converterValue.ConvertFromString(value.ToString()!), false);
            }

            return (default!, true);
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
                    || !worksheet.Cells[1, 1].Value.ToString()!.Equals("Source", StringComparison.Ordinal))
                {
                    throw new SpreadSheetFormatException("'Source' column is not presented.");
                }
                if (string.IsNullOrWhiteSpace(worksheet.Cells[1, 2].Value.ToString())
                    || !worksheet.Cells[1, 2].Value.ToString()!.Equals("Target", StringComparison.Ordinal))
                {
                    throw new SpreadSheetFormatException("'Target' column is not presented.");
                }
                if (string.IsNullOrWhiteSpace(worksheet.Cells[1, 3].Value.ToString())
                    || !worksheet.Cells[1, 3].Value.ToString()!.Equals("Weight", StringComparison.Ordinal))
                {
                    throw new SpreadSheetFormatException("'Weight' column is not presented.");
                }
                if (string.IsNullOrWhiteSpace(worksheet.Cells[1, 4].Value.ToString())
                    || !worksheet.Cells[1, 4].Value.ToString()!.Equals("Type", StringComparison.Ordinal))
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
            var fileName = $"graph-{DateTime.Now:HH-mm-ss}.xlsx";
            var path = FolderPath is not null ? Path.Combine(FolderPath, fileName) : fileName;
            return path;
        }

        private static void ValidateGraph<TValue>(Graph<TValue> graph)
            where TValue : notnull, IStringConvertible<TValue>, IEqualityComparer<TValue>, IEquatable<TValue>
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
    }
}