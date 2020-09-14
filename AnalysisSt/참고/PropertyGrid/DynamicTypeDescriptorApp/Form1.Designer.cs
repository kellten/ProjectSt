namespace DynamicTypeDescriptorApp
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
      if (disposing && (components != null))
      {
        components.Dispose( );
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
      this.components = new System.ComponentModel.Container( );
      this.lablel1 = new System.Windows.Forms.Label( );
      this.cboSortByGrid = new System.Windows.Forms.ComboBox( );
      this.propertyGrid1 = new System.Windows.Forms.PropertyGrid( );
      this.ctxReset = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuReset = new System.Windows.Forms.ToolStripMenuItem( );
      this.cboSortByProperty = new System.Windows.Forms.ComboBox( );
      this.label2 = new System.Windows.Forms.Label( );
      this.cboSortByCategory = new System.Windows.Forms.ComboBox( );
      this.label3 = new System.Windows.Forms.Label( );
      this.label4 = new System.Windows.Forms.Label( );
      this.cboLang = new System.Windows.Forms.ComboBox( );
      this.splitContainer1 = new System.Windows.Forms.SplitContainer( );
      this.label1 = new System.Windows.Forms.Label( );
      this.label5 = new System.Windows.Forms.Label( );
      this.propertyGrid2 = new System.Windows.Forms.PropertyGrid( );
      this.ctxReset.SuspendLayout( );
      this.splitContainer1.Panel1.SuspendLayout( );
      this.splitContainer1.Panel2.SuspendLayout( );
      this.splitContainer1.SuspendLayout( );
      this.SuspendLayout( );
      // 
      // lablel1
      // 
      this.lablel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.lablel1.AutoSize = true;
      this.lablel1.Location = new System.Drawing.Point(12, 604);
      this.lablel1.Name = "lablel1";
      this.lablel1.Size = new System.Drawing.Size(126, 13);
      this.lablel1.TabIndex = 1;
      this.lablel1.Text = "PropertyGrid.PropertySort";
      // 
      // cboSortByGrid
      // 
      this.cboSortByGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cboSortByGrid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboSortByGrid.FormattingEnabled = true;
      this.cboSortByGrid.Location = new System.Drawing.Point(144, 601);
      this.cboSortByGrid.Name = "cboSortByGrid";
      this.cboSortByGrid.Size = new System.Drawing.Size(123, 21);
      this.cboSortByGrid.TabIndex = 2;
      this.cboSortByGrid.SelectedIndexChanged += new System.EventHandler(this.cboSortBy_SelectedIndexChanged);
      // 
      // propertyGrid1
      // 
      this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.propertyGrid1.ContextMenuStrip = this.ctxReset;
      this.propertyGrid1.Location = new System.Drawing.Point(15, 54);
      this.propertyGrid1.Margin = new System.Windows.Forms.Padding(0);
      this.propertyGrid1.Name = "propertyGrid1";
      this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.propertyGrid1.Size = new System.Drawing.Size(405, 482);
      this.propertyGrid1.TabIndex = 3;
      this.propertyGrid1.ToolbarVisible = false;
      this.propertyGrid1.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.propertyGrid1_SelectedGridItemChanged);
      // 
      // ctxReset
      // 
      this.ctxReset.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuReset});
      this.ctxReset.Name = "ctxReset";
      this.ctxReset.Size = new System.Drawing.Size(103, 26);
      this.ctxReset.Opening += new System.ComponentModel.CancelEventHandler(this.ctxReset_Opening);
      // 
      // mnuReset
      // 
      this.mnuReset.Name = "mnuReset";
      this.mnuReset.Size = new System.Drawing.Size(102, 22);
      this.mnuReset.Text = "Reset";
      this.mnuReset.Click += new System.EventHandler(this.mnuReset_Click);
      // 
      // cboSortByProperty
      // 
      this.cboSortByProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cboSortByProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboSortByProperty.FormattingEnabled = true;
      this.cboSortByProperty.Location = new System.Drawing.Point(144, 550);
      this.cboSortByProperty.Name = "cboSortByProperty";
      this.cboSortByProperty.Size = new System.Drawing.Size(123, 21);
      this.cboSortByProperty.TabIndex = 5;
      this.cboSortByProperty.SelectedIndexChanged += new System.EventHandler(this.cboSortByProperty_SelectedIndexChanged);
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 553);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(82, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Property Sort by";
      // 
      // cboSortByCategory
      // 
      this.cboSortByCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cboSortByCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboSortByCategory.FormattingEnabled = true;
      this.cboSortByCategory.Location = new System.Drawing.Point(144, 574);
      this.cboSortByCategory.Name = "cboSortByCategory";
      this.cboSortByCategory.Size = new System.Drawing.Size(123, 21);
      this.cboSortByCategory.TabIndex = 7;
      this.cboSortByCategory.SelectedIndexChanged += new System.EventHandler(this.cboSortByCategory_SelectedIndexChanged);
      // 
      // label3
      // 
      this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 577);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(88, 13);
      this.label3.TabIndex = 6;
      this.label3.Text = "Category Sort by:";
      // 
      // label4
      // 
      this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(12, 631);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(55, 13);
      this.label4.TabIndex = 8;
      this.label4.Text = "Language";
      // 
      // cboLang
      // 
      this.cboLang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cboLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboLang.FormattingEnabled = true;
      this.cboLang.Items.AddRange(new object[] {
            "English (US)",
            "Danish (Denmark)",
            "Arabic"});
      this.cboLang.Location = new System.Drawing.Point(144, 628);
      this.cboLang.Name = "cboLang";
      this.cboLang.Size = new System.Drawing.Size(123, 21);
      this.cboLang.TabIndex = 9;
      this.cboLang.SelectedIndexChanged += new System.EventHandler(this.cboLang_SelectedIndexChanged);
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.label1);
      this.splitContainer1.Panel1.Controls.Add(this.propertyGrid1);
      this.splitContainer1.Panel1.Controls.Add(this.label3);
      this.splitContainer1.Panel1.Controls.Add(this.cboSortByGrid);
      this.splitContainer1.Panel1.Controls.Add(this.label4);
      this.splitContainer1.Panel1.Controls.Add(this.cboSortByCategory);
      this.splitContainer1.Panel1.Controls.Add(this.lablel1);
      this.splitContainer1.Panel1.Controls.Add(this.cboSortByProperty);
      this.splitContainer1.Panel1.Controls.Add(this.label2);
      this.splitContainer1.Panel1.Controls.Add(this.cboLang);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.label5);
      this.splitContainer1.Panel2.Controls.Add(this.propertyGrid2);
      this.splitContainer1.Size = new System.Drawing.Size(851, 657);
      this.splitContainer1.SplitterDistance = 432;
      this.splitContainer1.TabIndex = 14;
      // 
      // label1
      // 
      this.label1.Dock = System.Windows.Forms.DockStyle.Top;
      this.label1.Location = new System.Drawing.Point(0, 0);
      this.label1.Name = "label1";
      this.label1.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
      this.label1.Size = new System.Drawing.Size(432, 54);
      this.label1.TabIndex = 14;
      this.label1.Text = "NOTE: Read the property descriptions for the purpose of the property.";
      // 
      // label5
      // 
      this.label5.Dock = System.Windows.Forms.DockStyle.Top;
      this.label5.Location = new System.Drawing.Point(0, 0);
      this.label5.Name = "label5";
      this.label5.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
      this.label5.Size = new System.Drawing.Size(415, 54);
      this.label5.TabIndex = 15;
      this.label5.Text = "NOTE: Use this grid to modify the selected property on the left grid at run-time." +
          "  Read the property descriptions for the purpose of the property.";
      // 
      // propertyGrid2
      // 
      this.propertyGrid2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.propertyGrid2.ContextMenuStrip = this.ctxReset;
      this.propertyGrid2.Location = new System.Drawing.Point(5, 54);
      this.propertyGrid2.Margin = new System.Windows.Forms.Padding(0);
      this.propertyGrid2.Name = "propertyGrid2";
      this.propertyGrid2.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.propertyGrid2.Size = new System.Drawing.Size(404, 593);
      this.propertyGrid2.TabIndex = 4;
      this.propertyGrid2.ToolbarVisible = false;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(851, 657);
      this.Controls.Add(this.splitContainer1);
      this.Name = "Form1";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Form1";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.ctxReset.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout( );
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lablel1;
    private System.Windows.Forms.ComboBox cboSortByGrid;
    private System.Windows.Forms.PropertyGrid propertyGrid1;
    private System.Windows.Forms.ComboBox cboSortByProperty;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cboSortByCategory;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox cboLang;
    private System.Windows.Forms.ContextMenuStrip ctxReset;
    private System.Windows.Forms.ToolStripMenuItem mnuReset;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.PropertyGrid propertyGrid2;
  }
}

