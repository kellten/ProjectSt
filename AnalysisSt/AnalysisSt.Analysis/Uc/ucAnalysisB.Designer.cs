namespace AnalysisSt.Analysis.Uc
{
    partial class ucAnalysisB
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ucPrice0 = new AnalysisSt.Chart.ucChart.ucPrice();
            this.ucVolume0 = new AnalysisSt.Chart.ucChart.ucVolume();
            this.ucPrice1 = new AnalysisSt.Chart.ucChart.ucPrice();
            this.ucVolume1 = new AnalysisSt.Chart.ucChart.ucVolume();
            this.ucPrice2 = new AnalysisSt.Chart.ucChart.ucPrice();
            this.ucVolume2 = new AnalysisSt.Chart.ucChart.ucVolume();
            this.ucPrice3 = new AnalysisSt.Chart.ucChart.ucPrice();
            this.ucVolume3 = new AnalysisSt.Chart.ucChart.ucVolume();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoScroll = true;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.ucVolume3, 0, 7);
            this.tableLayoutPanel.Controls.Add(this.ucPrice3, 0, 6);
            this.tableLayoutPanel.Controls.Add(this.ucVolume2, 0, 5);
            this.tableLayoutPanel.Controls.Add(this.ucPrice2, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.ucVolume1, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.ucPrice1, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.ucPrice0, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.ucVolume0, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 8;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(798, 595);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // ucPrice0
            // 
            this.ucPrice0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPrice0.FromDate = null;
            this.ucPrice0.Location = new System.Drawing.Point(3, 3);
            this.ucPrice0.Name = "ucPrice0";
            this.ucPrice0.Size = new System.Drawing.Size(792, 68);
            this.ucPrice0.StockCode = null;
            this.ucPrice0.TabIndex = 0;
            this.ucPrice0.ToDate = null;
            // 
            // ucVolume0
            // 
            this.ucVolume0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucVolume0.FromDate = null;
            this.ucVolume0.Location = new System.Drawing.Point(3, 77);
            this.ucVolume0.Name = "ucVolume0";
            this.ucVolume0.Size = new System.Drawing.Size(792, 68);
            this.ucVolume0.StockCode = null;
            this.ucVolume0.TabIndex = 1;
            this.ucVolume0.ToDate = null;
            // 
            // ucPrice1
            // 
            this.ucPrice1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPrice1.FromDate = null;
            this.ucPrice1.Location = new System.Drawing.Point(3, 151);
            this.ucPrice1.Name = "ucPrice1";
            this.ucPrice1.Size = new System.Drawing.Size(792, 68);
            this.ucPrice1.StockCode = null;
            this.ucPrice1.TabIndex = 2;
            this.ucPrice1.ToDate = null;
            // 
            // ucVolume1
            // 
            this.ucVolume1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucVolume1.FromDate = null;
            this.ucVolume1.Location = new System.Drawing.Point(3, 225);
            this.ucVolume1.Name = "ucVolume1";
            this.ucVolume1.Size = new System.Drawing.Size(792, 68);
            this.ucVolume1.StockCode = null;
            this.ucVolume1.TabIndex = 3;
            this.ucVolume1.ToDate = null;
            // 
            // ucPrice2
            // 
            this.ucPrice2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPrice2.FromDate = null;
            this.ucPrice2.Location = new System.Drawing.Point(3, 299);
            this.ucPrice2.Name = "ucPrice2";
            this.ucPrice2.Size = new System.Drawing.Size(792, 68);
            this.ucPrice2.StockCode = null;
            this.ucPrice2.TabIndex = 4;
            this.ucPrice2.ToDate = null;
            // 
            // ucVolume2
            // 
            this.ucVolume2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucVolume2.FromDate = null;
            this.ucVolume2.Location = new System.Drawing.Point(3, 373);
            this.ucVolume2.Name = "ucVolume2";
            this.ucVolume2.Size = new System.Drawing.Size(792, 68);
            this.ucVolume2.StockCode = null;
            this.ucVolume2.TabIndex = 5;
            this.ucVolume2.ToDate = null;
            // 
            // ucPrice3
            // 
            this.ucPrice3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPrice3.FromDate = null;
            this.ucPrice3.Location = new System.Drawing.Point(3, 447);
            this.ucPrice3.Name = "ucPrice3";
            this.ucPrice3.Size = new System.Drawing.Size(792, 68);
            this.ucPrice3.StockCode = null;
            this.ucPrice3.TabIndex = 6;
            this.ucPrice3.ToDate = null;
            // 
            // ucVolume3
            // 
            this.ucVolume3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucVolume3.FromDate = null;
            this.ucVolume3.Location = new System.Drawing.Point(3, 521);
            this.ucVolume3.Name = "ucVolume3";
            this.ucVolume3.Size = new System.Drawing.Size(792, 71);
            this.ucVolume3.StockCode = null;
            this.ucVolume3.TabIndex = 7;
            this.ucVolume3.ToDate = null;
            // 
            // ucAnalysisB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "ucAnalysisB";
            this.Size = new System.Drawing.Size(798, 595);
            this.tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private Chart.ucChart.ucVolume ucVolume3;
        private Chart.ucChart.ucPrice ucPrice3;
        private Chart.ucChart.ucVolume ucVolume2;
        private Chart.ucChart.ucPrice ucPrice2;
        private Chart.ucChart.ucVolume ucVolume1;
        private Chart.ucChart.ucPrice ucPrice1;
        private Chart.ucChart.ucPrice ucPrice0;
        private Chart.ucChart.ucVolume ucVolume0;
    }
}
