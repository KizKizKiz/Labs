using System;
using System.ComponentModel;
using System.Windows.Forms;

using Library.StorageProcessor;
using Library.Model;

namespace DbIntegrationApp
{
    public partial class ProviderNewItemOrUpdateView : Form
    {
        public ProviderNewItemOrUpdateView(
            IRepository<Provider> providerRepository)
        {
            _providerRepository = providerRepository ?? throw new ArgumentNullException(nameof(providerRepository));

            InitializeComponent();
        }

        private async void Save(object sender, EventArgs e)
        {
            try
            {
                var newItem = new Provider(_name.Text, _telNumber.Text, _address.Text);

                await _providerRepository.AddAsync(newItem);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.StackTrace, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddressValidating(object sender, CancelEventArgs e)
        {
            _errorProvider.ShowErrorIfNotValid(
                _address,
                () => string.IsNullOrWhiteSpace(_address.Text),
                () => e.Cancel = true,
                "Cannot be empty or has only whitespaces.");
        }

        private void NameValidating(object sender, CancelEventArgs e)
        {
            _errorProvider.ShowErrorIfNotValid(
                _name,
                () => string.IsNullOrWhiteSpace(_name.Text),
                () => e.Cancel = true,
                "Cannot be empty or has only whitespaces.");
        }

        private void TelephoneValidating(object sender, CancelEventArgs e)
        {
            _errorProvider.ShowErrorIfNotValid(
                _telNumber,
                () => _telNumber.Text.Length != 11,
                () => e.Cancel = true,
                "Should have 11 characters.");
        }

        private readonly IRepository<Provider> _providerRepository;
    }
}
