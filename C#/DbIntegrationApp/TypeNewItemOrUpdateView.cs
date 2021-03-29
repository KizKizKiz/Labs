using Library.StorageProcessor;
using Library.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DbIntegrationApp
{
    public partial class TypeNewItemOrUpdateView : Form
    {
        public TypeNewItemOrUpdateView(
            IRepository<GoodType> goodTypesRepository)
        {
            _goodTypesRepository = goodTypesRepository ?? throw new ArgumentNullException(nameof(goodTypesRepository));

            InitializeComponent();
        }

        private async void Save(object sender, EventArgs e)
        {
            try
            {
                var item = new GoodType(_typeName.Text);

                await _goodTypesRepository.AddAsync(item);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.StackTrace, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NameValidating(object sender, CancelEventArgs e)
        {
            _errorManager.ShowErrorIfNotValid(
                _typeName, 
                () => string.IsNullOrWhiteSpace(_typeName.Text),
                () => e.Cancel = true, 
                "Cannot be empty or has only whitespaces.");
        }

        private readonly IRepository<GoodType> _goodTypesRepository;
    }
}