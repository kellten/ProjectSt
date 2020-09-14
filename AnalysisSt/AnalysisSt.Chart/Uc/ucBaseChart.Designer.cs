namespace AnalysisSt.Chart.Uc
{
    partial class ucBaseChart
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
            this.spC0 = new System.Windows.Forms.SplitContainer();
            this.propBasechart = new System.Windows.Forms.PropertyGrid();
            this.spC1 = new System.Windows.Forms.SplitContainer();
            this.btnStore = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnDisplayChart = new System.Windows.Forms.Button();
            this.BaseChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tbConA = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvTradeInfoA = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvTradeInfoB = new System.Windows.Forms.DataGridView();
            this.dgvTradeInfoC = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.spC0)).BeginInit();
            this.spC0.Panel1.SuspendLayout();
            this.spC0.Panel2.SuspendLayout();
            this.spC0.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spC1)).BeginInit();
            this.spC1.Panel1.SuspendLayout();
            this.spC1.Panel2.SuspendLayout();
            this.spC1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BaseChart)).BeginInit();
            this.tbConA.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTradeInfoA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTradeInfoB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTradeInfoC)).BeginInit();
            this.SuspendLayout();
            // 
            // spC0
            // 
            this.spC0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.spC0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spC0.Location = new System.Drawing.Point(0, 0);
            this.spC0.Name = "spC0";
            // 
            // spC0.Panel1
            // 
            this.spC0.Panel1.Controls.Add(this.tbConA);
            // 
            // spC0.Panel2
            // 
            this.spC0.Panel2.Controls.Add(this.spC1);
            this.spC0.Size = new System.Drawing.Size(1188, 643);
            this.spC0.SplitterDistance = 238;
            this.spC0.TabIndex = 31;
            // 
            // propBasechart
            // 
            this.propBasechart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propBasechart.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propBasechart.Location = new System.Drawing.Point(3, 3);
            this.propBasechart.Name = "propBasechart";
            this.propBasechart.Size = new System.Drawing.Size(220, 607);
            this.propBasechart.TabIndex = 0;
            this.propBasechart.Click += new System.EventHandler(this.propBasechart_Click);
            // 
            // spC1
            // 
            this.spC1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.spC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spC1.Location = new System.Drawing.Point(0, 0);
            this.spC1.Name = "spC1";
            this.spC1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spC1.Panel1
            // 
            this.spC1.Panel1.Controls.Add(this.btnStore);
            this.spC1.Panel1.Controls.Add(this.btnReload);
            this.spC1.Panel1.Controls.Add(this.btnDisplayChart);
            // 
            // spC1.Panel2
            // 
            this.spC1.Panel2.Controls.Add(this.BaseChart);
            this.spC1.Size = new System.Drawing.Size(946, 643);
            this.spC1.SplitterDistance = 39;
            this.spC1.TabIndex = 0;
            // 
            // btnStore
            // 
            this.btnStore.Location = new System.Drawing.Point(199, 1);
            this.btnStore.Name = "btnStore";
            this.btnStore.Size = new System.Drawing.Size(133, 19);
            this.btnStore.TabIndex = 2;
            this.btnStore.Text = "환경설정 저장";
            this.btnStore.UseVisualStyleBackColor = true;
            this.btnStore.Click += new System.EventHandler(this.btnStore_Click);
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(75, 1);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(118, 19);
            this.btnReload.TabIndex = 1;
            this.btnReload.Text = "Property리로드";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnDisplayChart
            // 
            this.btnDisplayChart.Location = new System.Drawing.Point(12, 1);
            this.btnDisplayChart.Name = "btnDisplayChart";
            this.btnDisplayChart.Size = new System.Drawing.Size(57, 19);
            this.btnDisplayChart.TabIndex = 0;
            this.btnDisplayChart.Text = "조회";
            this.btnDisplayChart.UseVisualStyleBackColor = true;
            this.btnDisplayChart.Click += new System.EventHandler(this.btnDisplayChart_Click);
            // 
            // BaseChart
            // 
            this.BaseChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BaseChart.Location = new System.Drawing.Point(0, 0);
            this.BaseChart.Name = "BaseChart";
            this.BaseChart.Size = new System.Drawing.Size(942, 596);
            this.BaseChart.TabIndex = 0;
            this.BaseChart.Text = "chart1";
            // 
            // tbConA
            // 
            this.tbConA.Controls.Add(this.tabPage1);
            this.tbConA.Controls.Add(this.tabPage2);
            this.tbConA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbConA.Location = new System.Drawing.Point(0, 0);
            this.tbConA.Name = "tbConA";
            this.tbConA.SelectedIndex = 0;
            this.tbConA.Size = new System.Drawing.Size(234, 639);
            this.tbConA.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.propBasechart);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(226, 613);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(226, 613);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvTradeInfoA
            // 
            this.dgvTradeInfoA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTradeInfoA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTradeInfoA.Location = new System.Drawing.Point(0, 0);
            this.dgvTradeInfoA.Name = "dgvTradeInfoA";
            this.dgvTradeInfoA.RowTemplate.Height = 23;
            this.dgvTradeInfoA.Size = new System.Drawing.Size(220, 171);
            this.dgvTradeInfoA.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvTradeInfoA);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(220, 607);
            this.splitContainer1.SplitterDistance = 171;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvTradeInfoB);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgvTradeInfoC);
            this.splitContainer2.Size = new System.Drawing.Size(220, 432);
            this.splitContainer2.SplitterDistance = 197;
            this.splitContainer2.TabIndex = 0;
            // 
            // dgvTradeInfoB
            // 
            this.dgvTradeInfoB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTradeInfoB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTradeInfoB.Location = new System.Drawing.Point(0, 0);
            this.dgvTradeInfoB.Name = "dgvTradeInfoB";
            this.dgvTradeInfoB.RowTemplate.Height = 23;
            this.dgvTradeInfoB.Size = new System.Drawing.Size(220, 197);
            this.dgvTradeInfoB.TabIndex = 1;
            // 
            // dgvTradeInfoC
            // 
            this.dgvTradeInfoC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTradeInfoC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTradeInfoC.Location = new System.Drawing.Point(0, 0);
            this.dgvTradeInfoC.Name = "dgvTradeInfoC";
            this.dgvTradeInfoC.RowTemplate.Height = 23;
            this.dgvTradeInfoC.Size = new System.Drawing.Size(220, 231);
            this.dgvTradeInfoC.TabIndex = 1;
            // 
            // ucBaseChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spC0);
            this.Name = "ucBaseChart";
            this.Size = new System.Drawing.Size(1188, 643);
            this.Load += new System.EventHandler(this.ucBaseChart_Load);
            this.spC0.Panel1.ResumeLayout(false);
            this.spC0.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spC0)).EndInit();
            this.spC0.ResumeLayout(false);
            this.spC1.Panel1.ResumeLayout(false);
            this.spC1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spC1)).EndInit();
            this.spC1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BaseChart)).EndInit();
            this.tbConA.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTradeInfoA)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTradeInfoB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTradeInfoC)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer spC0;
        private System.Windows.Forms.SplitContainer spC1;
        private System.Windows.Forms.DataVisualization.Charting.Chart BaseChart;
        private System.Windows.Forms.PropertyGrid propBasechart;
        private System.Windows.Forms.Button btnDisplayChart;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnStore;
        private System.Windows.Forms.TabControl tbConA;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvTradeInfoA;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgvTradeInfoB;
        private System.Windows.Forms.DataGridView dgvTradeInfoC;

    }
}
