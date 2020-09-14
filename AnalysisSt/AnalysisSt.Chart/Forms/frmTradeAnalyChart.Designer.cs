namespace AnalysisSt.Chart.Forms
{
    partial class frmTradeAnalyChart
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tbCon = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ucFav = new AnalysisSt.Common.Uc.ucFav();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ucStockList = new AnalysisSt.Common.Uc.ucStockList();
            this.ucTradeAnalyChart = new AnalysisSt.Chart.Uc.ucTradeAnalyChart();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.ucBaseChart = new AnalysisSt.Chart.Uc.ucBaseChart();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tbCon.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tbCon);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1452, 654);
            this.splitContainer1.SplitterDistance = 359;
            this.splitContainer1.TabIndex = 1;
            // 
            // tbCon
            // 
            this.tbCon.Controls.Add(this.tabPage1);
            this.tbCon.Controls.Add(this.tabPage2);
            this.tbCon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCon.Location = new System.Drawing.Point(0, 0);
            this.tbCon.Name = "tbCon";
            this.tbCon.SelectedIndex = 0;
            this.tbCon.Size = new System.Drawing.Size(359, 654);
            this.tbCon.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.ucFav);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(351, 628);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "즐겨찾기";
            // 
            // ucFav
            // 
            this.ucFav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucFav.Location = new System.Drawing.Point(3, 3);
            this.ucFav.Name = "ucFav";
            this.ucFav.Size = new System.Drawing.Size(345, 622);
            this.ucFav.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.ucStockList);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(351, 628);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "전체";
            // 
            // ucStockList
            // 
            this.ucStockList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucStockList.Location = new System.Drawing.Point(3, 3);
            this.ucStockList.Name = "ucStockList";
            this.ucStockList.Size = new System.Drawing.Size(345, 622);
            this.ucStockList.TabIndex = 0;
            // 
            // ucTradeAnalyChart
            // 
            this.ucTradeAnalyChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTradeAnalyChart.Location = new System.Drawing.Point(3, 3);
            this.ucTradeAnalyChart.Name = "ucTradeAnalyChart";
            this.ucTradeAnalyChart.Size = new System.Drawing.Size(1075, 622);
            this.ucTradeAnalyChart.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1089, 654);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ucBaseChart);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1081, 628);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.ucTradeAnalyChart);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1081, 628);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // ucBaseChart
            // 
            this.ucBaseChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBaseChart.Location = new System.Drawing.Point(3, 3);
            this.ucBaseChart.Name = "ucBaseChart";
            this.ucBaseChart.Size = new System.Drawing.Size(1075, 622);
            this.ucBaseChart.StockCode = null;
            this.ucBaseChart.StockName = null;
            this.ucBaseChart.TabIndex = 0;
            // 
            // frmTradeAnalyChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1452, 654);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmTradeAnalyChart";
            this.Text = "frmTradeAnalyChart";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tbCon.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Uc.ucTradeAnalyChart ucTradeAnalyChart;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tbCon;
        private System.Windows.Forms.TabPage tabPage1;
        private Common.Uc.ucFav ucFav;
        private System.Windows.Forms.TabPage tabPage2;
        private Common.Uc.ucStockList ucStockList;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private Uc.ucBaseChart ucBaseChart;
        private System.Windows.Forms.TabPage tabPage4;
    }
}