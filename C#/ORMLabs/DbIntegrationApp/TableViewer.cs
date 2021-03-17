using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;

using Library.StorageProcessor;
using Library.StorageProcessor.Model;

namespace DbIntegrationApp
{
    public partial class TableViewer : Form
    {
        public TableViewer(ModelsContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _container.EntitiesLoaded += TableLoadingAsync;
            InitializeComponent();
        }

        private async Task TableLoadingAsync()
        {
            _goodsTable.DataSource = new BindingList<Good>((await _container.GoodsWorker.GetAllAsync()).ToList());
        }

        private readonly ModelsContainer _container;
    }
}