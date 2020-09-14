namespace AnSt.Volume.Uc
{
    partial class UcVolumeInfo
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnA = new System.Windows.Forms.Panel();
            this.chkVolumeCalQtyYeungGiGum = new System.Windows.Forms.CheckBox();
            this.chkVolume = new System.Windows.Forms.CheckBox();
            this.chkGomeado = new System.Windows.Forms.CheckBox();
            this.chkVolumeCalQtyGigan = new System.Windows.Forms.CheckBox();
            this.chkVolumeCalQtySamoFund = new System.Windows.Forms.CheckBox();
            this.chkVolumeCalQtyTusin = new System.Windows.Forms.CheckBox();
            this.chkVolumeCalQtyNation = new System.Windows.Forms.CheckBox();
            this.chkVolumeCalQtyIOFor = new System.Windows.Forms.CheckBox();
            this.chkVolumeCalQtyGumWoong = new System.Windows.Forms.CheckBox();
            this.chkVolumeCalQtyGitaGumWoong = new System.Windows.Forms.CheckBox();
            this.chkVolumeCalQtyGaeIn = new System.Windows.Forms.CheckBox();
            this.chkVolumeCalQtyFore = new System.Windows.Forms.CheckBox();
            this.chkVolumeCalQtyBohum = new System.Windows.Forms.CheckBox();
            this.chkVolumeCalQtyBank = new System.Windows.Forms.CheckBox();
            this.chkVolumeCalQtyGitaBubin = new System.Windows.Forms.CheckBox();
            this.pnInfo = new System.Windows.Forms.Panel();
            this.DgvVolumeList = new System.Windows.Forms.DataGridView();
            this.chartVolume = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnA.SuspendLayout();
            this.pnInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvVolumeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartVolume)).BeginInit();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.pnA);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnInfo);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(946, 695);
            this.splitContainer1.SplitterDistance = 29;
            this.splitContainer1.TabIndex = 0;
            // 
            // pnA
            // 
            this.pnA.Controls.Add(this.chkVolumeCalQtyYeungGiGum);
            this.pnA.Controls.Add(this.chkVolume);
            this.pnA.Controls.Add(this.chkGomeado);
            this.pnA.Controls.Add(this.chkVolumeCalQtyGigan);
            this.pnA.Controls.Add(this.chkVolumeCalQtySamoFund);
            this.pnA.Controls.Add(this.chkVolumeCalQtyTusin);
            this.pnA.Controls.Add(this.chkVolumeCalQtyNation);
            this.pnA.Controls.Add(this.chkVolumeCalQtyIOFor);
            this.pnA.Controls.Add(this.chkVolumeCalQtyGumWoong);
            this.pnA.Controls.Add(this.chkVolumeCalQtyGitaGumWoong);
            this.pnA.Controls.Add(this.chkVolumeCalQtyGaeIn);
            this.pnA.Controls.Add(this.chkVolumeCalQtyFore);
            this.pnA.Controls.Add(this.chkVolumeCalQtyBohum);
            this.pnA.Controls.Add(this.chkVolumeCalQtyBank);
            this.pnA.Controls.Add(this.chkVolumeCalQtyGitaBubin);
            this.pnA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnA.Location = new System.Drawing.Point(0, 0);
            this.pnA.Name = "pnA";
            this.pnA.Size = new System.Drawing.Size(942, 25);
            this.pnA.TabIndex = 0;
            // 
            // chkVolumeCalQtyYeungGiGum
            // 
            this.chkVolumeCalQtyYeungGiGum.AutoSize = true;
            this.chkVolumeCalQtyYeungGiGum.Checked = true;
            this.chkVolumeCalQtyYeungGiGum.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVolumeCalQtyYeungGiGum.Location = new System.Drawing.Point(530, 6);
            this.chkVolumeCalQtyYeungGiGum.Name = "chkVolumeCalQtyYeungGiGum";
            this.chkVolumeCalQtyYeungGiGum.Size = new System.Drawing.Size(57, 15);
            this.chkVolumeCalQtyYeungGiGum.TabIndex = 39;
            this.chkVolumeCalQtyYeungGiGum.Text = "연기금";
            this.chkVolumeCalQtyYeungGiGum.UseVisualStyleBackColor = true;
            // 
            // chkVolume
            // 
            this.chkVolume.AutoSize = true;
            this.chkVolume.Checked = true;
            this.chkVolume.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVolume.Location = new System.Drawing.Point(3, 6);
            this.chkVolume.Name = "chkVolume";
            this.chkVolume.Size = new System.Drawing.Size(66, 15);
            this.chkVolume.TabIndex = 30;
            this.chkVolume.Text = "Volume";
            this.chkVolume.UseVisualStyleBackColor = true;
            // 
            // chkGomeado
            // 
            this.chkGomeado.AutoSize = true;
            this.chkGomeado.Checked = true;
            this.chkGomeado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGomeado.Location = new System.Drawing.Point(862, 6);
            this.chkGomeado.Name = "chkGomeado";
            this.chkGomeado.Size = new System.Drawing.Size(57, 15);
            this.chkGomeado.TabIndex = 44;
            this.chkGomeado.Text = "공매도";
            this.chkGomeado.UseVisualStyleBackColor = true;
            // 
            // chkVolumeCalQtyGigan
            // 
            this.chkVolumeCalQtyGigan.AutoSize = true;
            this.chkVolumeCalQtyGigan.Checked = true;
            this.chkVolumeCalQtyGigan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVolumeCalQtyGigan.Location = new System.Drawing.Point(196, 6);
            this.chkVolumeCalQtyGigan.Name = "chkVolumeCalQtyGigan";
            this.chkVolumeCalQtyGigan.Size = new System.Drawing.Size(57, 15);
            this.chkVolumeCalQtyGigan.TabIndex = 33;
            this.chkVolumeCalQtyGigan.Text = "기관합";
            this.chkVolumeCalQtyGigan.UseVisualStyleBackColor = true;
            // 
            // chkVolumeCalQtySamoFund
            // 
            this.chkVolumeCalQtySamoFund.AutoSize = true;
            this.chkVolumeCalQtySamoFund.Checked = true;
            this.chkVolumeCalQtySamoFund.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVolumeCalQtySamoFund.Location = new System.Drawing.Point(598, 6);
            this.chkVolumeCalQtySamoFund.Name = "chkVolumeCalQtySamoFund";
            this.chkVolumeCalQtySamoFund.Size = new System.Drawing.Size(68, 15);
            this.chkVolumeCalQtySamoFund.TabIndex = 40;
            this.chkVolumeCalQtySamoFund.Text = "사모펀드";
            this.chkVolumeCalQtySamoFund.UseVisualStyleBackColor = true;
            // 
            // chkVolumeCalQtyTusin
            // 
            this.chkVolumeCalQtyTusin.AutoSize = true;
            this.chkVolumeCalQtyTusin.Checked = true;
            this.chkVolumeCalQtyTusin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVolumeCalQtyTusin.Location = new System.Drawing.Point(370, 6);
            this.chkVolumeCalQtyTusin.Name = "chkVolumeCalQtyTusin";
            this.chkVolumeCalQtyTusin.Size = new System.Drawing.Size(46, 15);
            this.chkVolumeCalQtyTusin.TabIndex = 36;
            this.chkVolumeCalQtyTusin.Text = "투신";
            this.chkVolumeCalQtyTusin.UseVisualStyleBackColor = true;
            // 
            // chkVolumeCalQtyNation
            // 
            this.chkVolumeCalQtyNation.AutoSize = true;
            this.chkVolumeCalQtyNation.Checked = true;
            this.chkVolumeCalQtyNation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVolumeCalQtyNation.Location = new System.Drawing.Point(676, 6);
            this.chkVolumeCalQtyNation.Name = "chkVolumeCalQtyNation";
            this.chkVolumeCalQtyNation.Size = new System.Drawing.Size(46, 15);
            this.chkVolumeCalQtyNation.TabIndex = 41;
            this.chkVolumeCalQtyNation.Text = "국가";
            this.chkVolumeCalQtyNation.UseVisualStyleBackColor = true;
            // 
            // chkVolumeCalQtyIOFor
            // 
            this.chkVolumeCalQtyIOFor.AutoSize = true;
            this.chkVolumeCalQtyIOFor.Checked = true;
            this.chkVolumeCalQtyIOFor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVolumeCalQtyIOFor.Location = new System.Drawing.Point(784, 6);
            this.chkVolumeCalQtyIOFor.Name = "chkVolumeCalQtyIOFor";
            this.chkVolumeCalQtyIOFor.Size = new System.Drawing.Size(68, 15);
            this.chkVolumeCalQtyIOFor.TabIndex = 43;
            this.chkVolumeCalQtyIOFor.Text = "내외국인";
            this.chkVolumeCalQtyIOFor.UseVisualStyleBackColor = true;
            // 
            // chkVolumeCalQtyGumWoong
            // 
            this.chkVolumeCalQtyGumWoong.AutoSize = true;
            this.chkVolumeCalQtyGumWoong.Checked = true;
            this.chkVolumeCalQtyGumWoong.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVolumeCalQtyGumWoong.Location = new System.Drawing.Point(262, 6);
            this.chkVolumeCalQtyGumWoong.Name = "chkVolumeCalQtyGumWoong";
            this.chkVolumeCalQtyGumWoong.Size = new System.Drawing.Size(46, 15);
            this.chkVolumeCalQtyGumWoong.TabIndex = 34;
            this.chkVolumeCalQtyGumWoong.Text = "금융";
            this.chkVolumeCalQtyGumWoong.UseVisualStyleBackColor = true;
            // 
            // chkVolumeCalQtyGitaGumWoong
            // 
            this.chkVolumeCalQtyGitaGumWoong.AutoSize = true;
            this.chkVolumeCalQtyGitaGumWoong.Checked = true;
            this.chkVolumeCalQtyGitaGumWoong.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVolumeCalQtyGitaGumWoong.Location = new System.Drawing.Point(424, 6);
            this.chkVolumeCalQtyGitaGumWoong.Name = "chkVolumeCalQtyGitaGumWoong";
            this.chkVolumeCalQtyGitaGumWoong.Size = new System.Drawing.Size(46, 15);
            this.chkVolumeCalQtyGitaGumWoong.TabIndex = 37;
            this.chkVolumeCalQtyGitaGumWoong.Text = "기금";
            this.chkVolumeCalQtyGitaGumWoong.UseVisualStyleBackColor = true;
            // 
            // chkVolumeCalQtyGaeIn
            // 
            this.chkVolumeCalQtyGaeIn.AutoSize = true;
            this.chkVolumeCalQtyGaeIn.Checked = true;
            this.chkVolumeCalQtyGaeIn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVolumeCalQtyGaeIn.Location = new System.Drawing.Point(142, 6);
            this.chkVolumeCalQtyGaeIn.Name = "chkVolumeCalQtyGaeIn";
            this.chkVolumeCalQtyGaeIn.Size = new System.Drawing.Size(46, 15);
            this.chkVolumeCalQtyGaeIn.TabIndex = 32;
            this.chkVolumeCalQtyGaeIn.Text = "개인";
            this.chkVolumeCalQtyGaeIn.UseVisualStyleBackColor = true;
            // 
            // chkVolumeCalQtyFore
            // 
            this.chkVolumeCalQtyFore.AutoSize = true;
            this.chkVolumeCalQtyFore.Checked = true;
            this.chkVolumeCalQtyFore.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVolumeCalQtyFore.Location = new System.Drawing.Point(76, 6);
            this.chkVolumeCalQtyFore.Name = "chkVolumeCalQtyFore";
            this.chkVolumeCalQtyFore.Size = new System.Drawing.Size(57, 15);
            this.chkVolumeCalQtyFore.TabIndex = 31;
            this.chkVolumeCalQtyFore.Text = "외국인";
            this.chkVolumeCalQtyFore.UseVisualStyleBackColor = true;
            // 
            // chkVolumeCalQtyBohum
            // 
            this.chkVolumeCalQtyBohum.AutoSize = true;
            this.chkVolumeCalQtyBohum.Checked = true;
            this.chkVolumeCalQtyBohum.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVolumeCalQtyBohum.Location = new System.Drawing.Point(316, 6);
            this.chkVolumeCalQtyBohum.Name = "chkVolumeCalQtyBohum";
            this.chkVolumeCalQtyBohum.Size = new System.Drawing.Size(46, 15);
            this.chkVolumeCalQtyBohum.TabIndex = 35;
            this.chkVolumeCalQtyBohum.Text = "보험";
            this.chkVolumeCalQtyBohum.UseVisualStyleBackColor = true;
            // 
            // chkVolumeCalQtyBank
            // 
            this.chkVolumeCalQtyBank.AutoSize = true;
            this.chkVolumeCalQtyBank.Checked = true;
            this.chkVolumeCalQtyBank.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVolumeCalQtyBank.Location = new System.Drawing.Point(478, 6);
            this.chkVolumeCalQtyBank.Name = "chkVolumeCalQtyBank";
            this.chkVolumeCalQtyBank.Size = new System.Drawing.Size(46, 15);
            this.chkVolumeCalQtyBank.TabIndex = 38;
            this.chkVolumeCalQtyBank.Text = "은행";
            this.chkVolumeCalQtyBank.UseVisualStyleBackColor = true;
            // 
            // chkVolumeCalQtyGitaBubin
            // 
            this.chkVolumeCalQtyGitaBubin.AutoSize = true;
            this.chkVolumeCalQtyGitaBubin.Checked = true;
            this.chkVolumeCalQtyGitaBubin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVolumeCalQtyGitaBubin.Location = new System.Drawing.Point(730, 6);
            this.chkVolumeCalQtyGitaBubin.Name = "chkVolumeCalQtyGitaBubin";
            this.chkVolumeCalQtyGitaBubin.Size = new System.Drawing.Size(46, 15);
            this.chkVolumeCalQtyGitaBubin.TabIndex = 42;
            this.chkVolumeCalQtyGitaBubin.Text = "기법";
            this.chkVolumeCalQtyGitaBubin.UseVisualStyleBackColor = true;
            // 
            // pnInfo
            // 
            this.pnInfo.AutoScroll = true;
            this.pnInfo.Controls.Add(this.DgvVolumeList);
            this.pnInfo.Controls.Add(this.chartVolume);
            this.pnInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnInfo.Location = new System.Drawing.Point(0, 0);
            this.pnInfo.Name = "pnInfo";
            this.pnInfo.Size = new System.Drawing.Size(942, 658);
            this.pnInfo.TabIndex = 0;
            // 
            // DgvVolumeList
            // 
            this.DgvVolumeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvVolumeList.Location = new System.Drawing.Point(3, 3);
            this.DgvVolumeList.Name = "DgvVolumeList";
            this.DgvVolumeList.RowTemplate.Height = 23;
            this.DgvVolumeList.Size = new System.Drawing.Size(938, 198);
            this.DgvVolumeList.TabIndex = 1;
            // 
            // chartVolume
            // 
            chartArea1.Name = "ChartArea1";
            this.chartVolume.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartVolume.Legends.Add(legend1);
            this.chartVolume.Location = new System.Drawing.Point(3, 207);
            this.chartVolume.Name = "chartVolume";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartVolume.Series.Add(series1);
            this.chartVolume.Size = new System.Drawing.Size(916, 1127);
            this.chartVolume.TabIndex = 0;
            this.chartVolume.Text = "chart1";
            // 
            // UcVolumeInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 11F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "UcVolumeInfo";
            this.Size = new System.Drawing.Size(946, 695);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnA.ResumeLayout(false);
            this.pnA.PerformLayout();
            this.pnInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvVolumeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartVolume)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnA;
        internal System.Windows.Forms.CheckBox chkVolumeCalQtyYeungGiGum;
        internal System.Windows.Forms.CheckBox chkGomeado;
        internal System.Windows.Forms.CheckBox chkVolume;
        internal System.Windows.Forms.CheckBox chkVolumeCalQtyTusin;
        internal System.Windows.Forms.CheckBox chkVolumeCalQtyIOFor;
        internal System.Windows.Forms.CheckBox chkVolumeCalQtyGitaGumWoong;
        internal System.Windows.Forms.CheckBox chkVolumeCalQtyFore;
        internal System.Windows.Forms.CheckBox chkVolumeCalQtyBank;
        internal System.Windows.Forms.CheckBox chkVolumeCalQtyGitaBubin;
        internal System.Windows.Forms.CheckBox chkVolumeCalQtyBohum;
        internal System.Windows.Forms.CheckBox chkVolumeCalQtyGaeIn;
        internal System.Windows.Forms.CheckBox chkVolumeCalQtyGumWoong;
        internal System.Windows.Forms.CheckBox chkVolumeCalQtyNation;
        internal System.Windows.Forms.CheckBox chkVolumeCalQtySamoFund;
        internal System.Windows.Forms.CheckBox chkVolumeCalQtyGigan;
        private System.Windows.Forms.Panel pnInfo;
        private System.Windows.Forms.DataGridView DgvVolumeList;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartVolume;
    }
}
