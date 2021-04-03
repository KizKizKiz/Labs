//using System;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//using Library.Graph.Views;
//using OfficeOpenXml;

//namespace Library.Graph.ImportersExporters
//{
//    public sealed class SpreadSheetImporterExporter : IGraphViewImporter, IGraphExporter
//    {
//        public SpreadSheetImporterExporter()
//        {
//            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//        }

//        public async Task ExportAsync<TView, TViewItem, TValue>(TView view)
//            where TView : IGraphView<TViewItem>
//            where TViewItem : IGraphViewItem<TValue>
//            where TValue : notnull
//        {
//            if (view is null)
//            {
//                throw new ArgumentNullException(nameof(view));
//            }

//            var exportTask = view switch
//            {
//                AdjacensiesView<TValue> adjView => ExportAdjacensiesView(adjView),
//                EdgesWithWeightView<TValue> edgeView => ExportEdgesView(edgeView),
//                _ => throw new InvalidOperationException("Received not supported graph view.")
//            };
//            await exportTask.ConfigureAwait(false);
//        }

//        public Task<TView> ImportAsync<TView, TViewItem, TValue>()
//            where TView : IGraphView<TViewItem>
//            where TViewItem : IGraphViewItem<TValue>
//        {
//            //var exportTask = typeof(TView) switch
//            //{
//            //    AdjacensiesView<TValue> adjView => ExportAdjacensiesView(adjView),
//            //    EdgesWithWeightView<TValue> edgeView => ExportEdgesView(edgeView),
//            //    _ => throw new InvalidOperationException("Received not supported graph view.")
//            //};
//            //await exportTask.ConfigureAwait(false);
//            throw new NotImplementedException();
//        }

//        private async Task ExportAdjacensiesView<TValue>(AdjacensiesView<TValue> view)
//        {
//            var edges = view.Items.Select(
//                c => c.Items.Select(
//                    v => new EdgeViewItem<TValue>(c.Vertex, v)))
//                .SelectMany(s => s)
//                .ToList()!;

//            await ExportEdgesView(new EdgesWithWeightView<TValue>(edges));
//        }

//        private Task ExportEdgesView<TValue>(EdgesWithWeightView<TValue> view)
//        {
//            throw new NotImplementedException();

//            //using var package = new ExcelPackage(new FileInfo(null!));
//            //var worksheet = package.Workbook.Worksheets.Add("GRAPH_DUMP");

//            //worksheet.Cells[1, 1].Value = "Source";
//            //worksheet.Cells[1, 2].Value = "Target";
//            //worksheet.Cells[1, 4].Value = "Type";
//            //worksheet.Cells[1, 5].Value = "Label";

//            //for (int i = 0; i < view.Items.Count; i++)
//            //{
//            //    worksheet.Cells[i + 2, 1].Value = view.Items[i].First.ToString();
//            //    worksheet.Cells[i + 2, 2].Value = view.Items[i].Second.ToString();
//            //    worksheet.Cells[i + 2, 4].Value = view.IsDirected ? "Directed" : "Undirected";
//            //    worksheet.Cells[i + 2, 5].Value = $"From '{view.Items[i].First}' to '{view.Items[i].Second}'";
//            //}

//            //await package.SaveAsync().ConfigureAwait(false);
//        }


//        //protected async Task ImportCoreAsync(string fileName)
//        //{
//        //    await Task.Yield();

//        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

//        //    using var package = new ExcelPackage(new FileInfo(fileName));
//        //    var worksheet = package.Workbook.Worksheets[0];

//        //    var mapVertexAndItems = new Dictionary<TValue, List<TValue>>();

//        //    for (int i = 2; i <= worksheet.Dimension.End.Row; i++) // int i = 1 Skip headers
//        //    {
//        //        var entity = new TValue();
//        //        var edge = new EdgeViewItem<TValue>(
//        //            entity.ConvertFromString(worksheet.Cells[i, 1].Value.ToString()),
//        //            entity.ConvertFromString(worksheet.Cells[i, 2].Value.ToString()));

//        //        if (!mapVertexAndItems.ContainsKey(edge.First))
//        //        {
//        //            mapVertexAndItems.Add(edge.First, new List<TValue>());
//        //        }
//        //        else
//        //        {
//        //            mapVertexAndItems[edge.First].Add(edge.Second);
//        //        }
//        //    }
//        //    VerticesSet = mapVertexAndItems.Keys.ToList();

//        //    View = new AdjacensiesView<TValue>(
//        //        mapVertexAndItems.Select(item => new AdjacensyViewItem<TValue>(item.Key, item.Value)));
//        //}
//    }
//}
