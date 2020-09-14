namespace AnalysisSt.Chart.Forms
{
    partial class frmChartTester
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
            this.ucBaseChartTester = new AnalysisSt.Chart.Uc.ucBaseChart();
            this.ucPrice1 = new AnalysisSt.Chart.ucChart.ucPrice();
            this.SuspendLayout();
            // 
            // ucBaseChartTester
            // 
            this.ucBaseChartTester.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBaseChartTester.Location = new System.Drawing.Point(0, 0);
            this.ucBaseChartTester.Name = "ucBaseChartTester";
            this.ucBaseChartTester.Size = new System.Drawing.Size(983, 622);
            this.ucBaseChartTester.StockCode = null;
            this.ucBaseChartTester.StockName = null;
            this.ucBaseChartTester.TabIndex = 0;
            // 
            // ucPrice1
            // 
            this.ucPrice1.FromDate = null;
            this.ucPrice1.Location = new System.Drawing.Point(274, 107);
            this.ucPrice1.Name = "ucPrice1";
            this.ucPrice1.Size = new System.Drawing.Size(446, 255);
            this.ucPrice1.StockCode = null;
            this.ucPrice1.TabIndex = 1;
            this.ucPrice1.ToDate = null;
            // 
            // frmChartTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 622);
            this.Controls.Add(this.ucPrice1);
            this.Controls.Add(this.ucBaseChartTester);
            this.Name = "frmChartTester";
            this.Text = "frmChartTester";
            this.Load += new System.EventHandler(this.frmChartTester_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Uc.ucBaseChart ucBaseChartTester;
        private ucChart.ucPrice ucPrice1;
    }
}