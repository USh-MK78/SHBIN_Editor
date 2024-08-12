namespace SHBIN_Editor
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSHBINToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSHBINToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DVLE_ListBox = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.DVLE_Main_PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.CompiledShaderBinaryBlobPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.CompiledShaderBinaryBlobListBox = new System.Windows.Forms.ListBox();
            this.OperandDescriptorTable_PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.OperandDescriptorListBox = new System.Windows.Forms.ListBox();
            this.DVLP_TabControl = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.OperandDescriptorTabSplitContainer = new System.Windows.Forms.SplitContainer();
            this.CompiledShaderBinaryBlobSplitContainer = new System.Windows.Forms.SplitContainer();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.DVLP_TabControl.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OperandDescriptorTabSplitContainer)).BeginInit();
            this.OperandDescriptorTabSplitContainer.Panel1.SuspendLayout();
            this.OperandDescriptorTabSplitContainer.Panel2.SuspendLayout();
            this.OperandDescriptorTabSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CompiledShaderBinaryBlobSplitContainer)).BeginInit();
            this.CompiledShaderBinaryBlobSplitContainer.Panel1.SuspendLayout();
            this.CompiledShaderBinaryBlobSplitContainer.Panel2.SuspendLayout();
            this.CompiledShaderBinaryBlobSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(578, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSHBINToolStripMenuItem,
            this.saveSHBINToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openSHBINToolStripMenuItem
            // 
            this.openSHBINToolStripMenuItem.Name = "openSHBINToolStripMenuItem";
            this.openSHBINToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.openSHBINToolStripMenuItem.Text = "Open SHBIN";
            this.openSHBINToolStripMenuItem.Click += new System.EventHandler(this.Open_DVLB);
            // 
            // saveSHBINToolStripMenuItem
            // 
            this.saveSHBINToolStripMenuItem.Name = "saveSHBINToolStripMenuItem";
            this.saveSHBINToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.saveSHBINToolStripMenuItem.Text = "Save SHBIN";
            // 
            // DVLE_ListBox
            // 
            this.DVLE_ListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DVLE_ListBox.FormattingEnabled = true;
            this.DVLE_ListBox.ItemHeight = 12;
            this.DVLE_ListBox.Location = new System.Drawing.Point(0, 0);
            this.DVLE_ListBox.Name = "DVLE_ListBox";
            this.DVLE_ListBox.Size = new System.Drawing.Size(188, 479);
            this.DVLE_ListBox.TabIndex = 6;
            this.DVLE_ListBox.SelectedIndexChanged += new System.EventHandler(this.DVLE_LIstBox_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(578, 481);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(570, 485);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "DVLE";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DVLE_ListBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.DVLE_Main_PropertyGrid);
            this.splitContainer1.Size = new System.Drawing.Size(564, 479);
            this.splitContainer1.SplitterDistance = 188;
            this.splitContainer1.TabIndex = 1;
            // 
            // DVLE_Main_PropertyGrid
            // 
            this.DVLE_Main_PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DVLE_Main_PropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.DVLE_Main_PropertyGrid.Name = "DVLE_Main_PropertyGrid";
            this.DVLE_Main_PropertyGrid.Size = new System.Drawing.Size(372, 479);
            this.DVLE_Main_PropertyGrid.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DVLP_TabControl);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(570, 455);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "DVLP";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // CompiledShaderBinaryBlobPropertyGrid
            // 
            this.CompiledShaderBinaryBlobPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CompiledShaderBinaryBlobPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.CompiledShaderBinaryBlobPropertyGrid.Name = "CompiledShaderBinaryBlobPropertyGrid";
            this.CompiledShaderBinaryBlobPropertyGrid.Size = new System.Drawing.Size(363, 417);
            this.CompiledShaderBinaryBlobPropertyGrid.TabIndex = 14;
            // 
            // CompiledShaderBinaryBlobListBox
            // 
            this.CompiledShaderBinaryBlobListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CompiledShaderBinaryBlobListBox.FormattingEnabled = true;
            this.CompiledShaderBinaryBlobListBox.ItemHeight = 12;
            this.CompiledShaderBinaryBlobListBox.Location = new System.Drawing.Point(0, 0);
            this.CompiledShaderBinaryBlobListBox.Name = "CompiledShaderBinaryBlobListBox";
            this.CompiledShaderBinaryBlobListBox.Size = new System.Drawing.Size(183, 417);
            this.CompiledShaderBinaryBlobListBox.TabIndex = 13;
            this.CompiledShaderBinaryBlobListBox.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // OperandDescriptorTable_PropertyGrid
            // 
            this.OperandDescriptorTable_PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OperandDescriptorTable_PropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.OperandDescriptorTable_PropertyGrid.Name = "OperandDescriptorTable_PropertyGrid";
            this.OperandDescriptorTable_PropertyGrid.Size = new System.Drawing.Size(363, 417);
            this.OperandDescriptorTable_PropertyGrid.TabIndex = 4;
            // 
            // OperandDescriptorListBox
            // 
            this.OperandDescriptorListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OperandDescriptorListBox.FormattingEnabled = true;
            this.OperandDescriptorListBox.ItemHeight = 12;
            this.OperandDescriptorListBox.Location = new System.Drawing.Point(0, 0);
            this.OperandDescriptorListBox.Name = "OperandDescriptorListBox";
            this.OperandDescriptorListBox.Size = new System.Drawing.Size(183, 417);
            this.OperandDescriptorListBox.TabIndex = 3;
            this.OperandDescriptorListBox.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // DVLP_TabControl
            // 
            this.DVLP_TabControl.Controls.Add(this.tabPage3);
            this.DVLP_TabControl.Controls.Add(this.tabPage4);
            this.DVLP_TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DVLP_TabControl.Location = new System.Drawing.Point(3, 3);
            this.DVLP_TabControl.Name = "DVLP_TabControl";
            this.DVLP_TabControl.SelectedIndex = 0;
            this.DVLP_TabControl.Size = new System.Drawing.Size(564, 449);
            this.DVLP_TabControl.TabIndex = 15;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.OperandDescriptorTabSplitContainer);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(556, 423);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "OperandDescriptorTable";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.CompiledShaderBinaryBlobSplitContainer);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(556, 423);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "CompiledShaderBinaryBlobTable";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // OperandDescriptorTabSplitContainer
            // 
            this.OperandDescriptorTabSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OperandDescriptorTabSplitContainer.Location = new System.Drawing.Point(3, 3);
            this.OperandDescriptorTabSplitContainer.Name = "OperandDescriptorTabSplitContainer";
            // 
            // OperandDescriptorTabSplitContainer.Panel1
            // 
            this.OperandDescriptorTabSplitContainer.Panel1.Controls.Add(this.OperandDescriptorListBox);
            // 
            // OperandDescriptorTabSplitContainer.Panel2
            // 
            this.OperandDescriptorTabSplitContainer.Panel2.Controls.Add(this.OperandDescriptorTable_PropertyGrid);
            this.OperandDescriptorTabSplitContainer.Size = new System.Drawing.Size(550, 417);
            this.OperandDescriptorTabSplitContainer.SplitterDistance = 183;
            this.OperandDescriptorTabSplitContainer.TabIndex = 0;
            // 
            // CompiledShaderBinaryBlobSplitContainer
            // 
            this.CompiledShaderBinaryBlobSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CompiledShaderBinaryBlobSplitContainer.Location = new System.Drawing.Point(3, 3);
            this.CompiledShaderBinaryBlobSplitContainer.Name = "CompiledShaderBinaryBlobSplitContainer";
            // 
            // CompiledShaderBinaryBlobSplitContainer.Panel1
            // 
            this.CompiledShaderBinaryBlobSplitContainer.Panel1.Controls.Add(this.CompiledShaderBinaryBlobListBox);
            // 
            // CompiledShaderBinaryBlobSplitContainer.Panel2
            // 
            this.CompiledShaderBinaryBlobSplitContainer.Panel2.Controls.Add(this.CompiledShaderBinaryBlobPropertyGrid);
            this.CompiledShaderBinaryBlobSplitContainer.Size = new System.Drawing.Size(550, 417);
            this.CompiledShaderBinaryBlobSplitContainer.SplitterDistance = 183;
            this.CompiledShaderBinaryBlobSplitContainer.TabIndex = 0;
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // textToolStripMenuItem
            // 
            this.textToolStripMenuItem.Name = "textToolStripMenuItem";
            this.textToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.textToolStripMenuItem.Text = "Text";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 505);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "DVLB Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.DVLP_TabControl.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.OperandDescriptorTabSplitContainer.Panel1.ResumeLayout(false);
            this.OperandDescriptorTabSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OperandDescriptorTabSplitContainer)).EndInit();
            this.OperandDescriptorTabSplitContainer.ResumeLayout(false);
            this.CompiledShaderBinaryBlobSplitContainer.Panel1.ResumeLayout(false);
            this.CompiledShaderBinaryBlobSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CompiledShaderBinaryBlobSplitContainer)).EndInit();
            this.CompiledShaderBinaryBlobSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSHBINToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSHBINToolStripMenuItem;
        private System.Windows.Forms.ListBox DVLE_ListBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PropertyGrid OperandDescriptorTable_PropertyGrid;
        private System.Windows.Forms.ListBox OperandDescriptorListBox;
        private System.Windows.Forms.ListBox CompiledShaderBinaryBlobListBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid DVLE_Main_PropertyGrid;
        private System.Windows.Forms.PropertyGrid CompiledShaderBinaryBlobPropertyGrid;
        private System.Windows.Forms.TabControl DVLP_TabControl;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.SplitContainer OperandDescriptorTabSplitContainer;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.SplitContainer CompiledShaderBinaryBlobSplitContainer;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textToolStripMenuItem;
    }
}

