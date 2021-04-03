using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;

using Library.StorageProcessor;
using Library.Model;

namespace DbIntegrationApp
{
    public partial class TableViewer : Form
    {
        public TableViewer(ModelsContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _container.EntitiesLoaded += TableLoadingAsync;

            InitializeComponent();

            _menu.Items["_removeItem"].Click += RemoveItem;
            _menu.Items["_addItem"].Click += AddItem;
        }

        private Task TableLoadingAsync()
        {
            Invoke(new Action(async () =>
            {
                _goodsTable.DataSource = new BindingList<Good>((await _container.GoodsRepository.GetAllCachedAsync()).ToList());
                _typesTable.DataSource = new BindingList<GoodType>((await _container.GoodTypesRepository.GetAllCachedAsync()).ToList());
                _goodsProvidersTable.DataSource = new BindingList<GoodProvider>((await _container.GoodsProvidersRepository.GetAllCachedAsync()).ToList());
                _providersTable.DataSource = new BindingList<Provider>((await _container.ProvidersRepository.GetAllCachedAsync()).ToList());
            }));
            return Task.CompletedTask;
        }

        private async void AddItem(object? sender, EventArgs e)
        {
            DialogResult saveResult = default;
            switch (_tableTabs.SelectedTab.Name)
            {
                case "_goodsTab":
                    saveResult = new GoodNewItemOrUpdateView(_container.GoodTypesRepository, _container.GoodsRepository).ShowDialog();
                    break;
                case "_goodTypesTab":
                    saveResult = new TypeNewItemOrUpdateView(_container.GoodTypesRepository).ShowDialog();
                    break;
                case "_providersTab":
                    saveResult = new ProviderNewItemOrUpdateView(_container.ProvidersRepository).ShowDialog();
                    break;
                case "_goodsAndProvidersTab":
                    saveResult = new GoodsProvidersNewItemOrUpdate(_container.GoodsRepository, _container.ProvidersRepository, _container.GoodsProvidersRepository).ShowDialog();
                    break;
                default:
                    throw new InvalidOperationException($"Invoked add on not registered tab (Name: {_tableTabs.SelectedTab.Name}).");
            }

            if (saveResult == DialogResult.Yes)
            {
                await TableLoadingAsync();
            }
        }

        private async void RemoveItem(object? sender, EventArgs e)
        {
            var wantToDelete = MessageBox.Show("Do you really want to delete item?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (wantToDelete == DialogResult.No)
            {
                return;
            }

            switch (_tableTabs.SelectedTab.Name)
            {
                case "_goodsTab":
                    if (GiveMessageOrSelected<Good>(_goodsTable) is Good item)
                    {
                        await _container.GoodsRepository.RemoveAsync(item);
                    }
                    break;
                case "_goodTypesTab":
                    if (GiveMessageOrSelected<GoodType>(_typesTable) is GoodType goodType)
                    {
                        await _container.GoodTypesRepository.RemoveAsync(goodType);
                    }
                    break;
                case "_providersTab":
                    if (GiveMessageOrSelected<Provider>(_providersTable) is Provider provider)
                    {
                        await _container.ProvidersRepository.RemoveAsync(provider);
                    }
                    break;
                case "_goodsAndProvidersTab":
                    if (GiveMessageOrSelected<GoodProvider>(_goodsProvidersTable) is GoodProvider goodProvider)
                    {
                        await _container.GoodsProvidersRepository.RemoveAsync(goodProvider);
                    }
                    break;
                default:
                    throw new InvalidOperationException($"Invoked add on not registered tab (Name: {_tableTabs.SelectedTab.Name}).");
            }
            await TableLoadingAsync();
        }

        private T? GiveMessageOrSelected<T>(DataGridView dwg)
            where T : class
        {
            if (dwg.SelectedRows.Count == 0)
            {
                MessageBox.Show("Cannot remove item cause it is not selected.", Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return null;
            }
            return dwg.SelectedRows[0].DataBoundItem as T ?? throw new InvalidOperationException("Passed type doesn't belong to data grid source type.");
        }

        private readonly ModelsContainer _container;
    }
}