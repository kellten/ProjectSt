namespace AnalysisSt.Chart.Uc
{
    partial class ucTradeAnalyChart
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series13 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series14 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartTradeAnaly = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnView = new System.Windows.Forms.Button();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblStockName = new System.Windows.Forms.Label();
            this.lblStockCode = new System.Windows.Forms.Label();
            this.chkGain = new System.Windows.Forms.CheckBox();
            this.chkFore = new System.Windows.Forms.CheckBox();
            this.chkGigan = new System.Windows.Forms.CheckBox();
            this.chkGumy = new System.Windows.Forms.CheckBox();
            this.chkBohum = new System.Windows.Forms.CheckBox();
            this.chkTosin = new System.Windows.Forms.CheckBox();
            this.chkGita = new System.Windows.Forms.CheckBox();
            this.chkBank = new System.Windows.Forms.CheckBox();
            this.chkYeon = new System.Windows.Forms.CheckBox();
            this.chkSamo = new System.Windows.Forms.CheckBox();
            this.chkBubin = new System.Windows.Forms.CheckBox();
            this.chkIoFore = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.chartTradeAnaly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartTradeAnaly
            // 
            this.chartTradeAnaly.BackColor = System.Drawing.SystemColors.Control;
            chartArea1.InnerPlotPosition.Auto = false;
            chartArea1.InnerPlotPosition.Height = 100F;
            chartArea1.InnerPlotPosition.Width = 93.10076F;
            chartArea1.InnerPlotPosition.X = 6.26558F;
            chartArea1.Name = "Trade";
            chartArea2.Name = "Price";
            chartArea3.Name = "TradeQty";
            this.chartTradeAnaly.ChartAreas.Add(chartArea1);
            this.chartTradeAnaly.ChartAreas.Add(chartArea2);
            this.chartTradeAnaly.ChartAreas.Add(chartArea3);
            this.chartTradeAnaly.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartTradeAnaly.Legends.Add(legend1);
            this.chartTradeAnaly.Location = new System.Drawing.Point(0, 0);
            this.chartTradeAnaly.Name = "chartTradeAnaly";
            series1.ChartArea = "Trade";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "개인";
            series2.ChartArea = "Trade";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "외국인";
            series3.ChartArea = "Trade";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "기관";
            series4.ChartArea = "Trade";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend1";
            series4.Name = "금융";
            series5.ChartArea = "Trade";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Legend = "Legend1";
            series5.Name = "보험";
            series6.ChartArea = "Trade";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Legend = "Legend1";
            series6.Name = "투신";
            series7.ChartArea = "Trade";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Legend = "Legend1";
            series7.Name = "기타금융";
            series8.ChartArea = "Trade";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series8.Legend = "Legend1";
            series8.Name = "은행";
            series9.ChartArea = "Trade";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series9.Legend = "Legend1";
            series9.Name = "연기금";
            series10.ChartArea = "Trade";
            series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series10.Legend = "Legend1";
            series10.Name = "사모";
            series11.ChartArea = "Trade";
            series11.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series11.Legend = "Legend1";
            series11.Name = "기법";
            series12.ChartArea = "Trade";
            series12.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series12.Legend = "Legend1";
            series12.Name = "기외";
            series13.ChartArea = "Price";
            series13.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series13.Legend = "Legend1";
            series13.Name = "Price";
            series13.YValuesPerPoint = 4;
            series14.ChartArea = "TradeQty";
            series14.Legend = "Legend1";
            series14.Name = "거래량";
            this.chartTradeAnaly.Series.Add(series1);
            this.chartTradeAnaly.Series.Add(series2);
            this.chartTradeAnaly.Series.Add(series3);
            this.chartTradeAnaly.Series.Add(series4);
            this.chartTradeAnaly.Series.Add(series5);
            this.chartTradeAnaly.Series.Add(series6);
            this.chartTradeAnaly.Series.Add(series7);
            this.chartTradeAnaly.Series.Add(series8);
            this.chartTradeAnaly.Series.Add(series9);
            this.chartTradeAnaly.Series.Add(series10);
            this.chartTradeAnaly.Series.Add(series11);
            this.chartTradeAnaly.Series.Add(series12);
            this.chartTradeAnaly.Series.Add(series13);
            this.chartTradeAnaly.Series.Add(series14);
            this.chartTradeAnaly.Size = new System.Drawing.Size(1424, 614);
            this.chartTradeAnaly.TabIndex = 0;
            this.chartTradeAnaly.Text = "chart1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chkIoFore);
            this.splitContainer1.Panel1.Controls.Add(this.chkBubin);
            this.splitContainer1.Panel1.Controls.Add(this.chkSamo);
            this.splitContainer1.Panel1.Controls.Add(this.chkYeon);
            this.splitContainer1.Panel1.Controls.Add(this.chkBank);
            this.splitContainer1.Panel1.Controls.Add(this.chkGita);
            this.splitContainer1.Panel1.Controls.Add(this.chkTosin);
            this.splitContainer1.Panel1.Controls.Add(this.chkBohum);
            this.splitContainer1.Panel1.Controls.Add(this.chkGumy);
            this.splitContainer1.Panel1.Controls.Add(this.chkGigan);
            this.splitContainer1.Panel1.Controls.Add(this.chkFore);
            this.splitContainer1.Panel1.Controls.Add(this.chkGain);
            this.splitContainer1.Panel1.Controls.Add(this.btnView);
            this.splitContainer1.Panel1.Controls.Add(this.dtpToDate);
            this.splitContainer1.Panel1.Controls.Add(this.dtpFromDate);
            this.splitContainer1.Panel1.Controls.Add(this.lblStockName);
            this.splitContainer1.Panel1.Controls.Add(this.lblStockCode);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chartTradeAnaly);
            this.splitContainer1.Size = new System.Drawing.Size(1428, 656);
            this.splitContainer1.SplitterDistance = 34;
            this.splitContainer1.TabIndex = 1;
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(430, 2);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 15;
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(322, 3);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(102, 21);
            this.dtpToDate.TabIndex = 14;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(214, 3);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(102, 21);
            this.dtpFromDate.TabIndex = 13;
            // 
            // lblStockName
            // 
            this.lblStockName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStockName.Location = new System.Drawing.Point(65, 5);
            this.lblStockName.Name = "lblStockName";
            this.lblStockName.Size = new System.Drawing.Size(143, 19);
            this.lblStockName.TabIndex = 8;
            this.lblStockName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStockCode
            // 
            this.lblStockCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStockCode.Location = new System.Drawing.Point(3, 5);
            this.lblStockCode.Name = "lblStockCode";
            this.lblStockCode.Size = new System.Drawing.Size(61, 19);
            this.lblStockCode.TabIndex = 7;
            this.lblStockCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkGain
            // 
            this.chkGain.AutoSize = true;
            this.chkGain.Checked = true;
            this.chkGain.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGain.Location = new System.Drawing.Point(511, 7);
            this.chkGain.Name = "chkGain";
            this.chkGain.Size = new System.Drawing.Size(48, 16);
            this.chkGain.TabIndex = 16;
            this.chkGain.Text = "개인";
            this.chkGain.UseVisualStyleBackColor = true;
            this.chkGain.CheckedChanged += new System.EventHandler(this.chkIoFore_CheckedChanged);
            // 
            // chkFore
            // 
            this.chkFore.AutoSize = true;
            this.chkFore.Checked = true;
            this.chkFore.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFore.Location = new System.Drawing.Point(565, 7);
            this.chkFore.Name = "chkFore";
            this.chkFore.Size = new System.Drawing.Size(60, 16);
            this.chkFore.TabIndex = 17;
            this.chkFore.Text = "외국인";
            this.chkFore.UseVisualStyleBackColor = true;
            this.chkFore.CheckedChanged += new System.EventHandler(this.chkIoFore_CheckedChanged);
            // 
            // chkGigan
            // 
            this.chkGigan.AutoSize = true;
            this.chkGigan.Checked = true;
            this.chkGigan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGigan.Location = new System.Drawing.Point(631, 6);
            this.chkGigan.Name = "chkGigan";
            this.chkGigan.Size = new System.Drawing.Size(48, 16);
            this.chkGigan.TabIndex = 18;
            this.chkGigan.Text = "기관";
            this.chkGigan.UseVisualStyleBackColor = true;
            this.chkGigan.CheckedChanged += new System.EventHandler(this.chkIoFore_CheckedChanged);
            // 
            // chkGumy
            // 
            this.chkGumy.AutoSize = true;
            this.chkGumy.Checked = true;
            this.chkGumy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGumy.Location = new System.Drawing.Point(685, 6);
            this.chkGumy.Name = "chkGumy";
            this.chkGumy.Size = new System.Drawing.Size(48, 16);
            this.chkGumy.TabIndex = 19;
            this.chkGumy.Text = "금융";
            this.chkGumy.UseVisualStyleBackColor = true;
            this.chkGumy.CheckedChanged += new System.EventHandler(this.chkIoFore_CheckedChanged);
            // 
            // chkBohum
            // 
            this.chkBohum.AutoSize = true;
            this.chkBohum.Checked = true;
            this.chkBohum.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBohum.Location = new System.Drawing.Point(739, 6);
            this.chkBohum.Name = "chkBohum";
            this.chkBohum.Size = new System.Drawing.Size(48, 16);
            this.chkBohum.TabIndex = 20;
            this.chkBohum.Text = "보험";
            this.chkBohum.UseVisualStyleBackColor = true;
            this.chkBohum.CheckedChanged += new System.EventHandler(this.chkIoFore_CheckedChanged);
            // 
            // chkTosin
            // 
            this.chkTosin.AutoSize = true;
            this.chkTosin.Checked = true;
            this.chkTosin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTosin.Location = new System.Drawing.Point(793, 5);
            this.chkTosin.Name = "chkTosin";
            this.chkTosin.Size = new System.Drawing.Size(48, 16);
            this.chkTosin.TabIndex = 21;
            this.chkTosin.Text = "투신";
            this.chkTosin.UseVisualStyleBackColor = true;
            this.chkTosin.CheckedChanged += new System.EventHandler(this.chkIoFore_CheckedChanged);
            // 
            // chkGita
            // 
            this.chkGita.AutoSize = true;
            this.chkGita.Checked = true;
            this.chkGita.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGita.Location = new System.Drawing.Point(847, 5);
            this.chkGita.Name = "chkGita";
            this.chkGita.Size = new System.Drawing.Size(72, 16);
            this.chkGita.TabIndex = 22;
            this.chkGita.Text = "기타금융";
            this.chkGita.UseVisualStyleBackColor = true;
            this.chkGita.CheckedChanged += new System.EventHandler(this.chkIoFore_CheckedChanged);
            // 
            // chkBank
            // 
            this.chkBank.AutoSize = true;
            this.chkBank.Checked = true;
            this.chkBank.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBank.Location = new System.Drawing.Point(920, 5);
            this.chkBank.Name = "chkBank";
            this.chkBank.Size = new System.Drawing.Size(48, 16);
            this.chkBank.TabIndex = 23;
            this.chkBank.Text = "은행";
            this.chkBank.UseVisualStyleBackColor = true;
            this.chkBank.CheckedChanged += new System.EventHandler(this.chkIoFore_CheckedChanged);
            // 
            // chkYeon
            // 
            this.chkYeon.AutoSize = true;
            this.chkYeon.Checked = true;
            this.chkYeon.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkYeon.Location = new System.Drawing.Point(974, 5);
            this.chkYeon.Name = "chkYeon";
            this.chkYeon.Size = new System.Drawing.Size(60, 16);
            this.chkYeon.TabIndex = 24;
            this.chkYeon.Text = "연기금";
            this.chkYeon.UseVisualStyleBackColor = true;
            this.chkYeon.CheckedChanged += new System.EventHandler(this.chkIoFore_CheckedChanged);
            // 
            // chkSamo
            // 
            this.chkSamo.AutoSize = true;
            this.chkSamo.Checked = true;
            this.chkSamo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSamo.Location = new System.Drawing.Point(1040, 5);
            this.chkSamo.Name = "chkSamo";
            this.chkSamo.Size = new System.Drawing.Size(48, 16);
            this.chkSamo.TabIndex = 25;
            this.chkSamo.Text = "사모";
            this.chkSamo.UseVisualStyleBackColor = true;
            this.chkSamo.CheckedChanged += new System.EventHandler(this.chkIoFore_CheckedChanged);
            // 
            // chkBubin
            // 
            this.chkBubin.AutoSize = true;
            this.chkBubin.Checked = true;
            this.chkBubin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBubin.Location = new System.Drawing.Point(1094, 5);
            this.chkBubin.Name = "chkBubin";
            this.chkBubin.Size = new System.Drawing.Size(48, 16);
            this.chkBubin.TabIndex = 26;
            this.chkBubin.Text = "기법";
            this.chkBubin.UseVisualStyleBackColor = true;
            this.chkBubin.CheckedChanged += new System.EventHandler(this.chkIoFore_CheckedChanged);
            // 
            // chkIoFore
            // 
            this.chkIoFore.AutoSize = true;
            this.chkIoFore.Checked = true;
            this.chkIoFore.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIoFore.Location = new System.Drawing.Point(1148, 5);
            this.chkIoFore.Name = "chkIoFore";
            this.chkIoFore.Size = new System.Drawing.Size(48, 16);
            this.chkIoFore.TabIndex = 27;
            this.chkIoFore.Text = "기외";
            this.chkIoFore.UseVisualStyleBackColor = true;
            this.chkIoFore.CheckedChanged += new System.EventHandler(this.chkIoFore_CheckedChanged);
            // 
            // ucTradeAnalyChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucTradeAnalyChart";
            this.Size = new System.Drawing.Size(1428, 656);
            ((System.ComponentModel.ISupportInitialize)(this.chartTradeAnaly)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartTradeAnaly;
        private System.Windows.Forms.SplitContainer splitContainer1;
        internal System.Windows.Forms.Label lblStockName;
        internal System.Windows.Forms.Label lblStockCode;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.CheckBox chkIoFore;
        private System.Windows.Forms.CheckBox chkBubin;
        private System.Windows.Forms.CheckBox chkSamo;
        private System.Windows.Forms.CheckBox chkYeon;
        private System.Windows.Forms.CheckBox chkBank;
        private System.Windows.Forms.CheckBox chkGita;
        private System.Windows.Forms.CheckBox chkTosin;
        private System.Windows.Forms.CheckBox chkBohum;
        private System.Windows.Forms.CheckBox chkGumy;
        private System.Windows.Forms.CheckBox chkGigan;
        private System.Windows.Forms.CheckBox chkFore;
        private System.Windows.Forms.CheckBox chkGain;
    }
}
