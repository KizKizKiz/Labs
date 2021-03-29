
namespace DbIntegrationApp
{
    partial class GoodsProvidersNewItemOrUpdate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._providersId = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this._save = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this._goodsId = new System.Windows.Forms.ComboBox();
            this._cancel = new System.Windows.Forms.Button();
            this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this._deliveryDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._quantity = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._quantity)).BeginInit();
            this.SuspendLayout();
            // 
            // _providersId
            // 
            this._providersId.FormattingEnabled = true;
            this._providersId.Location = new System.Drawing.Point(12, 27);
            this._providersId.Name = "_providersId";
            this._providersId.Size = new System.Drawing.Size(286, 23);
            this._providersId.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "ID of provider:";
            // 
            // _save
            // 
            this._save.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this._save.Location = new System.Drawing.Point(12, 193);
            this._save.Name = "_save";
            this._save.Size = new System.Drawing.Size(75, 23);
            this._save.TabIndex = 2;
            this._save.Text = "&Save";
            this._save.UseVisualStyleBackColor = true;
            this._save.Click += new System.EventHandler(this.Save);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "ID of goods item:";
            // 
            // _goodsId
            // 
            this._goodsId.FormattingEnabled = true;
            this._goodsId.Location = new System.Drawing.Point(12, 71);
            this._goodsId.Name = "_goodsId";
            this._goodsId.Size = new System.Drawing.Size(286, 23);
            this._goodsId.TabIndex = 3;
            // 
            // _cancel
            // 
            this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancel.Location = new System.Drawing.Point(93, 193);
            this._cancel.Name = "_cancel";
            this._cancel.Size = new System.Drawing.Size(75, 23);
            this._cancel.TabIndex = 5;
            this._cancel.Text = "&Cancel";
            this._cancel.UseVisualStyleBackColor = true;
            // 
            // _errorProvider
            // 
            this._errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this._errorProvider.ContainerControl = this;
            // 
            // _deliveryDate
            // 
            this._deliveryDate.Location = new System.Drawing.Point(12, 164);
            this._deliveryDate.Name = "_deliveryDate";
            this._deliveryDate.Size = new System.Drawing.Size(286, 23);
            this._deliveryDate.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Delivery date:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Quantity:";
            // 
            // _quantity
            // 
            this._quantity.Location = new System.Drawing.Point(12, 120);
            this._quantity.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this._quantity.Name = "_quantity";
            this._quantity.Size = new System.Drawing.Size(286, 23);
            this._quantity.TabIndex = 9;
            // 
            // GoodsProvidersNewItemOrUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 225);
            this.Controls.Add(this._quantity);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._deliveryDate);
            this.Controls.Add(this._cancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._goodsId);
            this.Controls.Add(this._save);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._providersId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "GoodsProvidersNewItemOrUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Goods and providers: New item";
            this.Load += new System.EventHandler(this.Loading);
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._quantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox _providersId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _save;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _goodsId;
        private System.Windows.Forms.Button _cancel;
        private System.Windows.Forms.ErrorProvider _errorProvider;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker _deliveryDate;
        private System.Windows.Forms.NumericUpDown _quantity;
        private System.Windows.Forms.Label label4;
    }
}