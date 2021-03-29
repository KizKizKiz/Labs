using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Library.StorageProcessor;
using Library.Model;

namespace DbIntegrationApp
{
    public partial class GoodsProvidersNewItemOrUpdate : Form
    {
        public GoodsProvidersNewItemOrUpdate(
            IRepository<Good> goodsRepository,
            IRepository<Provider> providersRepository,
            IRepository<GoodProvider> goodsProvidesRepository)
        {
            _goodsRepository = goodsRepository ?? throw new ArgumentNullException(nameof(goodsRepository));
            _providersRepository = providersRepository ?? throw new ArgumentNullException(nameof(providersRepository));
            _goodsProvidesRepository = goodsProvidesRepository ?? throw new ArgumentNullException(nameof(goodsProvidesRepository));

            InitializeComponent();
        }

        private async void Save(object sender, EventArgs e)
        {
            try
            {
                var newItem = new GoodProvider()
                {
                    DeliveryDate = _deliveryDate.Value,
                    GoodId = (_goodsId.SelectedItem as Good)!.ID,
                    ProviderId = (_providersId.SelectedItem as Provider)!.ID,
                    Quantity = (int)_quantity.Value,
                    Good = (_goodsId.SelectedItem as Good)!,
                    Provider = (_providersId.SelectedItem as Provider)!,
                };

                await _goodsProvidesRepository.AddAsync(newItem);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.StackTrace, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Loading(object sender, EventArgs e)
        {
            (var goods, var providers) = await GetGoodsAndProviders();
            if (!goods.Any() || !providers.Any())
            {
                Close();
                return;
            }
            _goodsId.DataSource = goods.ToList();
            _goodsId.DisplayMember = nameof(Good.ID);

            _providersId.DataSource = providers.ToList();
            _providersId.DisplayMember = nameof(Provider.ID);

            _goodsId.SelectedIndex = 0;
            _providersId.SelectedIndex = 0;
        }

        private async Task<(IEnumerable<Good> goods, IEnumerable<Provider> providers)> GetGoodsAndProviders()
        {
            var goods = await _goodsRepository.GetAllCachedAsync();
            if (!goods.Any())
            {
                MessageBox.Show("The item cannot be added cause goods table doesn't contain items.", Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            var providers = await _providersRepository.GetAllCachedAsync();
            if (!providers.Any())
            {
                MessageBox.Show("The item cannot be added cause providers table doesn't contain items.", Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return (goods, providers);
        }

        private readonly IRepository<Good> _goodsRepository;
        private readonly IRepository<Provider> _providersRepository;
        private readonly IRepository<GoodProvider> _goodsProvidesRepository;
    }
}