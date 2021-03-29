
namespace DbIntegrationApp
{
    partial class GoodNewItemOrUpdateView
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
            this.label1 = new System.Windows.Forms.Label();
            this._manufacturer = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._description = new System.Windows.Forms.TextBox();
            this._possibleGoodTypes = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this._save = new System.Windows.Forms.Button();
            this._cancel = new System.Windows.Forms.Button();
            this._model = new System.Windows.Forms.TextBox();
            this._originalPrice = new System.Windows.Forms.NumericUpDown();
            this._discontPercent = new System.Windows.Forms.NumericUpDown();
            this._errorManager = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this._originalPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._discontPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._errorManager)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Manufacturer:";
            // 
            // _manufacturer
            // 
            this._manufacturer.Location = new System.Drawing.Point(12, 27);
            this._manufacturer.Mask = "LLLLLLLLLLLLLLLLLLLLLLLLLLLLLL";
            this._manufacturer.Name = "_manufacturer";
            this._manufacturer.Size = new System.Drawing.Size(159, 23);
            this._manufacturer.TabIndex = 1;
            this._manufacturer.ValidatingType = typeof(int);
            this._manufacturer.Validating += new System.ComponentModel.CancelEventHandler(this.ManufacturerValidating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Model:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Price without discont:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(196, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Description:";
            // 
            // _description
            // 
            this._description.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._description.Location = new System.Drawing.Point(196, 27);
            this._description.Multiline = true;
            this._description.Name = "_description";
            this._description.Size = new System.Drawing.Size(251, 203);
            this._description.TabIndex = 7;
            // 
            // _possibleGoodTypes
            // 
            this._possibleGoodTypes.FormattingEnabled = true;
            this._possibleGoodTypes.Location = new System.Drawing.Point(12, 163);
            this._possibleGoodTypes.Name = "_possibleGoodTypes";
            this._possibleGoodTypes.Size = new System.Drawing.Size(159, 23);
            this._possibleGoodTypes.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Type ID:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 189);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Discont percent:";
            // 
            // _save
            // 
            this._save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._save.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this._save.Location = new System.Drawing.Point(291, 236);
            this._save.Name = "_save";
            this._save.Size = new System.Drawing.Size(75, 23);
            this._save.TabIndex = 12;
            this._save.Text = "&Save";
            this._save.UseVisualStyleBackColor = true;
            this._save.Click += new System.EventHandler(this.Save);
            // 
            // _cancel
            // 
            this._cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancel.Location = new System.Drawing.Point(372, 236);
            this._cancel.Name = "_cancel";
            this._cancel.Size = new System.Drawing.Size(75, 23);
            this._cancel.TabIndex = 13;
            this._cancel.Text = "&Cancel";
            this._cancel.UseVisualStyleBackColor = true;
            // 
            // _model
            // 
            this._model.Location = new System.Drawing.Point(12, 71);
            this._model.Name = "_model";
            this._model.Size = new System.Drawing.Size(159, 23);
            this._model.TabIndex = 14;
            this._model.Validating += new System.ComponentModel.CancelEventHandler(this.ModelValidating);
            // 
            // _originalPrice
            // 
            this._originalPrice.Location = new System.Drawing.Point(12, 119);
            this._originalPrice.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this._originalPrice.Name = "_originalPrice";
            this._originalPrice.Size = new System.Drawing.Size(159, 23);
            this._originalPrice.TabIndex = 15;
            this._originalPrice.Validating += new System.ComponentModel.CancelEventHandler(this.PriceValidating);
            // 
            // _discontPercent
            // 
            this._discontPercent.Location = new System.Drawing.Point(12, 207);
            this._discontPercent.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this._discontPercent.Name = "_discontPercent";
            this._discontPercent.Size = new System.Drawing.Size(159, 23);
            this._discontPercent.TabIndex = 16;
            // 
            // _errorManager
            // 
            this._errorManager.BlinkRate = 0;
            this._errorManager.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this._errorManager.ContainerControl = this;
            // 
            // GoodNewItemOrUpdateView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 271);
            this.Controls.Add(this._discontPercent);
            this.Controls.Add(this._originalPrice);
            this.Controls.Add(this._model);
            this.Controls.Add(this._cancel);
            this.Controls.Add(this._save);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this._possibleGoodTypes);
            this.Controls.Add(this._description);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._manufacturer);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(475, 310);
            this.Name = "GoodNewItemOrUpdateView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Goods: New item";
            this.Load += new System.EventHandler(this.GoodAddOrUpdateViewLoading);
            ((System.ComponentModel.ISupportInitialize)(this._originalPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._discontPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._errorManager)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox _manufacturer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _description;
        private System.Windows.Forms.ComboBox _possibleGoodTypes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button _save;
        private System.Windows.Forms.Button _cancel;
        private System.Windows.Forms.TextBox _model;
        private System.Windows.Forms.NumericUpDown _originalPrice;
        private System.Windows.Forms.NumericUpDown _discontPercent;
        private System.Windows.Forms.ErrorProvider _errorManager;
    }
}