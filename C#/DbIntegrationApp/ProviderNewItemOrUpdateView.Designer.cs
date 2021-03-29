
namespace DbIntegrationApp
{
    partial class ProviderNewItemOrUpdateView
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
            this._save = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._address = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._cancel = new System.Windows.Forms.Button();
            this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this._telNumber = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._name = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // _save
            // 
            this._save.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this._save.Location = new System.Drawing.Point(10, 138);
            this._save.Name = "_save";
            this._save.Size = new System.Drawing.Size(75, 23);
            this._save.TabIndex = 3;
            this._save.Text = "&Save";
            this._save.UseVisualStyleBackColor = true;
            this._save.Click += new System.EventHandler(this.Save);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Telephone number:";
            // 
            // _address
            // 
            this._address.Location = new System.Drawing.Point(10, 109);
            this._address.Name = "_address";
            this._address.Size = new System.Drawing.Size(247, 23);
            this._address.TabIndex = 2;
            this._address.Validating += new System.ComponentModel.CancelEventHandler(this.AddressValidating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Address:";
            // 
            // _cancel
            // 
            this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancel.Location = new System.Drawing.Point(91, 138);
            this._cancel.Name = "_cancel";
            this._cancel.Size = new System.Drawing.Size(75, 23);
            this._cancel.TabIndex = 4;
            this._cancel.Text = "&Cancel";
            this._cancel.UseVisualStyleBackColor = true;
            // 
            // _errorProvider
            // 
            this._errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this._errorProvider.ContainerControl = this;
            // 
            // _telNumber
            // 
            this._telNumber.Location = new System.Drawing.Point(125, 65);
            this._telNumber.Mask = "00000000000";
            this._telNumber.Name = "_telNumber";
            this._telNumber.Size = new System.Drawing.Size(132, 23);
            this._telNumber.TabIndex = 1;
            this._telNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._telNumber.Validating += new System.ComponentModel.CancelEventHandler(this.TelephoneValidating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Name:";
            // 
            // _name
            // 
            this._name.Location = new System.Drawing.Point(10, 30);
            this._name.Name = "_name";
            this._name.Size = new System.Drawing.Size(247, 23);
            this._name.TabIndex = 0;
            this._name.Validating += new System.ComponentModel.CancelEventHandler(this.NameValidating);
            // 
            // ProviderNewItemOrUpdateView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 170);
            this.Controls.Add(this._name);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._telNumber);
            this.Controls.Add(this._cancel);
            this.Controls.Add(this._address);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._save);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ProviderNewItemOrUpdateView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Providers: New item";
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _address;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button _cancel;
        private System.Windows.Forms.ErrorProvider _errorProvider;
        private System.Windows.Forms.MaskedTextBox _telNumber;
        private System.Windows.Forms.TextBox _name;
        private System.Windows.Forms.Label label3;
    }
}