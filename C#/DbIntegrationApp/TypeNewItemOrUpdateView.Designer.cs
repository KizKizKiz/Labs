
namespace DbIntegrationApp
{
    partial class TypeNewItemOrUpdateView
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
            this._typeName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._save = new System.Windows.Forms.Button();
            this._cancel = new System.Windows.Forms.Button();
            this._errorManager = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this._errorManager)).BeginInit();
            this.SuspendLayout();
            // 
            // _typeName
            // 
            this._typeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._typeName.Location = new System.Drawing.Point(8, 26);
            this._typeName.Name = "_typeName";
            this._typeName.Size = new System.Drawing.Size(261, 23);
            this._typeName.TabIndex = 0;
            this._typeName.Validating += new System.ComponentModel.CancelEventHandler(this.NameValidating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Type name:";
            // 
            // _save
            // 
            this._save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._save.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this._save.Location = new System.Drawing.Point(301, 26);
            this._save.Name = "_save";
            this._save.Size = new System.Drawing.Size(75, 23);
            this._save.TabIndex = 2;
            this._save.Text = "&Save";
            this._save.UseVisualStyleBackColor = true;
            this._save.Click += new System.EventHandler(this.Save);
            // 
            // _cancel
            // 
            this._cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancel.Location = new System.Drawing.Point(382, 26);
            this._cancel.Name = "_cancel";
            this._cancel.Size = new System.Drawing.Size(75, 23);
            this._cancel.TabIndex = 3;
            this._cancel.Text = "&Cancel";
            this._cancel.UseVisualStyleBackColor = true;
            // 
            // _errorManager
            // 
            this._errorManager.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this._errorManager.ContainerControl = this;
            // 
            // TypeNewItemOrUpdateView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 59);
            this.Controls.Add(this._cancel);
            this.Controls.Add(this._save);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._typeName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimumSize = new System.Drawing.Size(16, 98);
            this.Name = "TypeNewItemOrUpdateView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Types: New item";
            ((System.ComponentModel.ISupportInitialize)(this._errorManager)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _typeName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _save;
        private System.Windows.Forms.Button _cancel;
        private System.Windows.Forms.ErrorProvider _errorManager;
    }
}