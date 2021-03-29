using System;
using System.Linq;
using System.Windows.Forms;

using Library.StorageProcessor;
using Library.Model;

namespace DbIntegrationApp
{
    public partial class GoodNewItemOrUpdateView : Form
    {
        public GoodNewItemOrUpdateView(
            IRepository<GoodType> goodTypesRepository,
            IRepository<Good> goodsRepository)
        {
            _goodTypesRepository = goodTypesRepository ?? throw new ArgumentNullException(nameof(goodTypesRepository));
            _goodsRepository = goodsRepository ?? throw new ArgumentNullException(nameof(goodsRepository));

            InitializeComponent();
        }

        private async void Save(object sender, EventArgs e)
        {
            try
            {
                var id = int.Parse(_possibleGoodTypes.SelectedItem.ToString()!);
                var newItem = new Good(
                    _description.Text,
                    _manufacturer.Text,
                    _model.Text,
                    _originalPrice.Value,
                    await _goodTypesRepository.GetByIdAsync(id),
                    (short)_discontPercent.Value)
                {
                    TypeId = id
                };

                await _goodsRepository.AddAsync(newItem);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.StackTrace, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void GoodAddOrUpdateViewLoading(object sender, EventArgs e)
        {
            _possibleGoodTypes.Items.AddRange((await _goodTypesRepository.GetAllCachedAsync(gt => gt.ID)).Cast<object>().ToArray());
            if (_possibleGoodTypes.Items.Count == 0)
            {
                MessageBox.Show("The goods item cannot be added cause goods types table doesn't contain items.", Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);

                DialogResult = DialogResult.Cancel;
                Close();
            }
            else
            {
                _possibleGoodTypes.SelectedIndex = 0;
            }
        }

        private void PriceValidating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _errorManager.ShowErrorIfNotValid(
                _originalPrice, 
                () => _originalPrice.Value == 0,
                () => e.Cancel = true,
                "Cannot equals to zero.");
        }

        private void ManufacturerValidating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _errorManager.ShowErrorIfNotValid(
                _manufacturer,
                () => string.IsNullOrWhiteSpace(_manufacturer.Text),
                () => e.Cancel = true,
                "Cannot be empty or has only whitespaces.");
        }

        private void ModelValidating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _errorManager.ShowErrorIfNotValid(
                _model,
                () => string.IsNullOrWhiteSpace(_model.Text),
                () => e.Cancel = true,
                "Cannot be empty or has only whitespaces.");
        }


        private readonly IRepository<GoodType> _goodTypesRepository;
        private readonly IRepository<Good> _goodsRepository;
    }
}
