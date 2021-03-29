namespace DbIntegrationApp
{
    partial class TableViewer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._tableTabs = new System.Windows.Forms.TabControl();
            this._goodsTab = new System.Windows.Forms.TabPage();
            this._goodsTable = new System.Windows.Forms.DataGridView();
            this._goodTypesTab = new System.Windows.Forms.TabPage();
            this._typesTable = new System.Windows.Forms.DataGridView();
            this._providersTab = new System.Windows.Forms.TabPage();
            this._providersTable = new System.Windows.Forms.DataGridView();
            this._goodsAndProvidersTab = new System.Windows.Forms.TabPage();
            this._goodsProvidersTable = new System.Windows.Forms.DataGridView();
            this._menu = new System.Windows.Forms.MenuStrip();
            this._addItem = new System.Windows.Forms.ToolStripMenuItem();
            this._removeItem = new System.Windows.Forms.ToolStripMenuItem();
            this._tableTabs.SuspendLayout();
            this._goodsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._goodsTable)).BeginInit();
            this._goodTypesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._typesTable)).BeginInit();
            this._providersTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._providersTable)).BeginInit();
            this._goodsAndProvidersTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._goodsProvidersTable)).BeginInit();
            this._menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _tableTabs
            // 
            this._tableTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tableTabs.Controls.Add(this._goodsTab);
            this._tableTabs.Controls.Add(this._goodTypesTab);
            this._tableTabs.Controls.Add(this._providersTab);
            this._tableTabs.Controls.Add(this._goodsAndProvidersTab);
            this._tableTabs.Location = new System.Drawing.Point(0, 24);
            this._tableTabs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._tableTabs.Name = "_tableTabs";
            this._tableTabs.SelectedIndex = 0;
            this._tableTabs.Size = new System.Drawing.Size(859, 306);
            this._tableTabs.TabIndex = 0;
            // 
            // _goodsTab
            // 
            this._goodsTab.Controls.Add(this._goodsTable);
            this._goodsTab.Location = new System.Drawing.Point(4, 24);
            this._goodsTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._goodsTab.Name = "_goodsTab";
            this._goodsTab.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._goodsTab.Size = new System.Drawing.Size(851, 278);
            this._goodsTab.TabIndex = 0;
            this._goodsTab.Text = "Goods";
            this._goodsTab.UseVisualStyleBackColor = true;
            // 
            // _goodsTable
            // 
            this._goodsTable.AllowUserToAddRows = false;
            this._goodsTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._goodsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._goodsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this._goodsTable.Location = new System.Drawing.Point(3, 2);
            this._goodsTable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._goodsTable.Name = "_goodsTable";
            this._goodsTable.ReadOnly = true;
            this._goodsTable.RowHeadersVisible = false;
            this._goodsTable.RowHeadersWidth = 51;
            this._goodsTable.RowTemplate.Height = 29;
            this._goodsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._goodsTable.Size = new System.Drawing.Size(845, 274);
            this._goodsTable.TabIndex = 0;
            // 
            // _goodTypesTab
            // 
            this._goodTypesTab.Controls.Add(this._typesTable);
            this._goodTypesTab.Location = new System.Drawing.Point(4, 24);
            this._goodTypesTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._goodTypesTab.Name = "_goodTypesTab";
            this._goodTypesTab.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._goodTypesTab.Size = new System.Drawing.Size(851, 278);
            this._goodTypesTab.TabIndex = 1;
            this._goodTypesTab.Text = "Good types";
            this._goodTypesTab.UseVisualStyleBackColor = true;
            // 
            // _typesTable
            // 
            this._typesTable.AllowUserToAddRows = false;
            this._typesTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._typesTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._typesTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this._typesTable.Location = new System.Drawing.Point(3, 2);
            this._typesTable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._typesTable.Name = "_typesTable";
            this._typesTable.ReadOnly = true;
            this._typesTable.RowHeadersVisible = false;
            this._typesTable.RowHeadersWidth = 51;
            this._typesTable.RowTemplate.Height = 29;
            this._typesTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._typesTable.Size = new System.Drawing.Size(845, 274);
            this._typesTable.TabIndex = 1;
            // 
            // _providersTab
            // 
            this._providersTab.Controls.Add(this._providersTable);
            this._providersTab.Location = new System.Drawing.Point(4, 24);
            this._providersTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._providersTab.Name = "_providersTab";
            this._providersTab.Size = new System.Drawing.Size(851, 278);
            this._providersTab.TabIndex = 2;
            this._providersTab.Text = "Providers";
            this._providersTab.UseVisualStyleBackColor = true;
            // 
            // _providersTable
            // 
            this._providersTable.AllowUserToAddRows = false;
            this._providersTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._providersTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._providersTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this._providersTable.Location = new System.Drawing.Point(0, 0);
            this._providersTable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._providersTable.Name = "_providersTable";
            this._providersTable.ReadOnly = true;
            this._providersTable.RowHeadersVisible = false;
            this._providersTable.RowHeadersWidth = 51;
            this._providersTable.RowTemplate.Height = 29;
            this._providersTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._providersTable.Size = new System.Drawing.Size(851, 278);
            this._providersTable.TabIndex = 2;
            // 
            // _goodsAndProvidersTab
            // 
            this._goodsAndProvidersTab.Controls.Add(this._goodsProvidersTable);
            this._goodsAndProvidersTab.Location = new System.Drawing.Point(4, 24);
            this._goodsAndProvidersTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._goodsAndProvidersTab.Name = "_goodsAndProvidersTab";
            this._goodsAndProvidersTab.Size = new System.Drawing.Size(851, 278);
            this._goodsAndProvidersTab.TabIndex = 3;
            this._goodsAndProvidersTab.Text = "Goods and providers";
            this._goodsAndProvidersTab.UseVisualStyleBackColor = true;
            // 
            // _goodsProvidersTable
            // 
            this._goodsProvidersTable.AllowUserToAddRows = false;
            this._goodsProvidersTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._goodsProvidersTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._goodsProvidersTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this._goodsProvidersTable.Location = new System.Drawing.Point(0, 0);
            this._goodsProvidersTable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._goodsProvidersTable.Name = "_goodsProvidersTable";
            this._goodsProvidersTable.ReadOnly = true;
            this._goodsProvidersTable.RowHeadersVisible = false;
            this._goodsProvidersTable.RowHeadersWidth = 51;
            this._goodsProvidersTable.RowTemplate.Height = 29;
            this._goodsProvidersTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._goodsProvidersTable.Size = new System.Drawing.Size(851, 278);
            this._goodsProvidersTable.TabIndex = 3;
            // 
            // _menu
            // 
            this._menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._addItem,
            this._removeItem});
            this._menu.Location = new System.Drawing.Point(0, 0);
            this._menu.Name = "_menu";
            this._menu.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this._menu.Size = new System.Drawing.Size(859, 24);
            this._menu.TabIndex = 1;
            this._menu.Text = "Menu";
            // 
            // _addItem
            // 
            this._addItem.Name = "_addItem";
            this._addItem.Size = new System.Drawing.Size(41, 20);
            this._addItem.Tag = "ADD";
            this._addItem.Text = "&Add";
            // 
            // _removeItem
            // 
            this._removeItem.Name = "_removeItem";
            this._removeItem.Size = new System.Drawing.Size(62, 20);
            this._removeItem.Tag = "REMOVE";
            this._removeItem.Text = "&Remove";
            // 
            // TableViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 332);
            this.Controls.Add(this._menu);
            this.Controls.Add(this._tableTabs);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "TableViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database explorer: \"Sport goods\"";
            this._tableTabs.ResumeLayout(false);
            this._goodsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._goodsTable)).EndInit();
            this._goodTypesTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._typesTable)).EndInit();
            this._providersTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._providersTable)).EndInit();
            this._goodsAndProvidersTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._goodsProvidersTable)).EndInit();
            this._menu.ResumeLayout(false);
            this._menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl _tableTabs;
        private System.Windows.Forms.TabPage _goodsTab;
        private System.Windows.Forms.DataGridView _goodsTable;
        private System.Windows.Forms.TabPage _goodTypesTab;
        private System.Windows.Forms.TabPage _providersTab;
        private System.Windows.Forms.TabPage _goodsAndProvidersTab;
        private System.Windows.Forms.MenuStrip _menu;
        private System.Windows.Forms.ToolStripMenuItem _addItem;
        private System.Windows.Forms.ToolStripMenuItem _removeItem;
        private System.Windows.Forms.DataGridView _typesTable;
        private System.Windows.Forms.DataGridView _providersTable;
        private System.Windows.Forms.DataGridView _goodsProvidersTable;
    }
}

