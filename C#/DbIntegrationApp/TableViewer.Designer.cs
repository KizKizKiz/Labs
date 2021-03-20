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
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this._providersTab = new System.Windows.Forms.TabPage();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this._goodsAndProvidersTab = new System.Windows.Forms.TabPage();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this._menu = new System.Windows.Forms.MenuStrip();
            this._addItem = new System.Windows.Forms.ToolStripMenuItem();
            this._removeItem = new System.Windows.Forms.ToolStripMenuItem();
            this._tableTabs.SuspendLayout();
            this._goodsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._goodsTable)).BeginInit();
            this._goodTypesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this._providersTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this._goodsAndProvidersTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
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
            this._tableTabs.Location = new System.Drawing.Point(0, 31);
            this._tableTabs.Name = "_tableTabs";
            this._tableTabs.SelectedIndex = 0;
            this._tableTabs.Size = new System.Drawing.Size(1087, 582);
            this._tableTabs.TabIndex = 0;
            // 
            // _goodsTab
            // 
            this._goodsTab.Controls.Add(this._goodsTable);
            this._goodsTab.Location = new System.Drawing.Point(4, 29);
            this._goodsTab.Name = "_goodsTab";
            this._goodsTab.Padding = new System.Windows.Forms.Padding(3);
            this._goodsTab.Size = new System.Drawing.Size(1079, 549);
            this._goodsTab.TabIndex = 0;
            this._goodsTab.Text = "Goods";
            this._goodsTab.UseVisualStyleBackColor = true;
            // 
            // _goodsTable
            // 
            this._goodsTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._goodsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._goodsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this._goodsTable.Location = new System.Drawing.Point(3, 3);
            this._goodsTable.Name = "_goodsTable";
            this._goodsTable.RowHeadersVisible = false;
            this._goodsTable.RowHeadersWidth = 51;
            this._goodsTable.RowTemplate.Height = 29;
            this._goodsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._goodsTable.Size = new System.Drawing.Size(1073, 543);
            this._goodsTable.TabIndex = 0;
            // 
            // _goodTypesTab
            // 
            this._goodTypesTab.Controls.Add(this.dataGridView2);
            this._goodTypesTab.Location = new System.Drawing.Point(4, 29);
            this._goodTypesTab.Name = "_goodTypesTab";
            this._goodTypesTab.Padding = new System.Windows.Forms.Padding(3);
            this._goodTypesTab.Size = new System.Drawing.Size(1079, 549);
            this._goodTypesTab.TabIndex = 1;
            this._goodTypesTab.Text = "Good types";
            this._goodTypesTab.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(3, 3);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 29;
            this.dataGridView2.Size = new System.Drawing.Size(1073, 543);
            this.dataGridView2.TabIndex = 1;
            // 
            // _providersTab
            // 
            this._providersTab.Controls.Add(this.dataGridView3);
            this._providersTab.Location = new System.Drawing.Point(4, 29);
            this._providersTab.Name = "_providersTab";
            this._providersTab.Size = new System.Drawing.Size(1079, 549);
            this._providersTab.TabIndex = 2;
            this._providersTab.Text = "Providers";
            this._providersTab.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView3.Location = new System.Drawing.Point(0, 0);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.RowTemplate.Height = 29;
            this.dataGridView3.Size = new System.Drawing.Size(1079, 549);
            this.dataGridView3.TabIndex = 1;
            // 
            // _goodsAndProvidersTab
            // 
            this._goodsAndProvidersTab.Controls.Add(this.dataGridView4);
            this._goodsAndProvidersTab.Location = new System.Drawing.Point(4, 29);
            this._goodsAndProvidersTab.Name = "_goodsAndProvidersTab";
            this._goodsAndProvidersTab.Size = new System.Drawing.Size(1079, 549);
            this._goodsAndProvidersTab.TabIndex = 3;
            this._goodsAndProvidersTab.Text = "Goods and providers";
            this._goodsAndProvidersTab.UseVisualStyleBackColor = true;
            // 
            // dataGridView4
            // 
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView4.Location = new System.Drawing.Point(0, 0);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.RowHeadersWidth = 51;
            this.dataGridView4.RowTemplate.Height = 29;
            this.dataGridView4.Size = new System.Drawing.Size(1079, 549);
            this.dataGridView4.TabIndex = 1;
            // 
            // _menu
            // 
            this._menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._addItem,
            this._removeItem});
            this._menu.Location = new System.Drawing.Point(0, 0);
            this._menu.Name = "_menu";
            this._menu.Size = new System.Drawing.Size(1087, 28);
            this._menu.TabIndex = 1;
            this._menu.Text = "Menu";
            // 
            // _addItem
            // 
            this._addItem.Name = "_addItem";
            this._addItem.Size = new System.Drawing.Size(51, 24);
            this._addItem.Tag = "ADD";
            this._addItem.Text = "&Add";
            // 
            // _removeItem
            // 
            this._removeItem.Name = "_removeItem";
            this._removeItem.Size = new System.Drawing.Size(77, 24);
            this._removeItem.Tag = "REMOVE";
            this._removeItem.Text = "&Remove";
            // 
            // TableViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 613);
            this.Controls.Add(this._menu);
            this.Controls.Add(this._tableTabs);
            this.Name = "TableViewer";
            this.Text = "Database explorer: \"Sport goods\"";
            this._tableTabs.ResumeLayout(false);
            this._goodsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._goodsTable)).EndInit();
            this._goodTypesTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this._providersTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this._goodsAndProvidersTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
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
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TabPage _providersTab;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.TabPage _goodsAndProvidersTab;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.MenuStrip _menu;
        private System.Windows.Forms.ToolStripMenuItem _addItem;
        private System.Windows.Forms.ToolStripMenuItem _removeItem;
    }
}

