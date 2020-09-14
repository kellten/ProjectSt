namespace AnSt.Chart.Uc
{
    partial class UcPriceChart
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
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.ChartPrice = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.splitConA = new System.Windows.Forms.SplitContainer();
            this.splitConB = new System.Windows.Forms.SplitContainer();
            this.ppGridMenu = new System.Windows.Forms.PropertyGrid();
            this.ucWaveInfo1 = new AnSt.BasicSetting.WaveInfo.UcWaveInfo();
            ((System.ComponentModel.ISupportInitialize)(this.ChartPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitConA)).BeginInit();
            this.splitConA.Panel1.SuspendLayout();
            this.splitConA.Panel2.SuspendLayout();
            this.splitConA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitConB)).BeginInit();
            this.splitConB.Panel1.SuspendLayout();
            this.splitConB.Panel2.SuspendLayout();
            this.splitConB.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChartPrice
            // 
            this.ChartPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.ChartPrice.Legends.Add(legend1);
            this.ChartPrice.Location = new System.Drawing.Point(0, 0);
            this.ChartPrice.Name = "ChartPrice";
            this.ChartPrice.Size = new System.Drawing.Size(372, 664);
            this.ChartPrice.TabIndex = 0;
            this.ChartPrice.Text = "chart1";
            // 
            // splitConA
            // 
            this.splitConA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitConA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitConA.Location = new System.Drawing.Point(0, 0);
            this.splitConA.Name = "splitConA";
            // 
            // splitConA.Panel1
            // 
            this.splitConA.Panel1.Controls.Add(this.splitConB);
            // 
            // splitConA.Panel2
            // 
            this.splitConA.Panel2.Controls.Add(this.ChartPrice);
            this.splitConA.Size = new System.Drawing.Size(771, 668);
            this.splitConA.SplitterDistance = 387;
            this.splitConA.SplitterWidth = 8;
            this.splitConA.TabIndex = 1;
            // 
            // splitConB
            // 
            this.splitConB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitConB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitConB.Location = new System.Drawing.Point(0, 0);
            this.splitConB.Name = "splitConB";
            this.splitConB.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitConB.Panel1
            // 
            this.splitConB.Panel1.Controls.Add(this.ppGridMenu);
            // 
            // splitConB.Panel2
            // 
            this.splitConB.Panel2.Controls.Add(this.ucWaveInfo1);
            this.splitConB.Size = new System.Drawing.Size(387, 668);
            this.splitConB.SplitterDistance = 356;
            this.splitConB.SplitterWidth = 8;
            this.splitConB.TabIndex = 0;
            // 
            // ppGridMenu
            // 
            this.ppGridMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ppGridMenu.LineColor = System.Drawing.SystemColors.ControlDark;
            this.ppGridMenu.Location = new System.Drawing.Point(0, 0);
            this.ppGridMenu.Name = "ppGridMenu";
            this.ppGridMenu.Size = new System.Drawing.Size(383, 352);
            this.ppGridMenu.TabIndex = 0;
            // 
            // ucWaveInfo1
            // 
            this.ucWaveInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucWaveInfo1.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ucWaveInfo1.Location = new System.Drawing.Point(0, 0);
            this.ucWaveInfo1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucWaveInfo1.Name = "ucWaveInfo1";
            this.ucWaveInfo1.Size = new System.Drawing.Size(383, 300);
            this.ucWaveInfo1.TabIndex = 0;
            // 
            // UcPriceChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitConA);
            this.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "UcPriceChart";
            this.Size = new System.Drawing.Size(771, 668);
            ((System.ComponentModel.ISupportInitialize)(this.ChartPrice)).EndInit();
            this.splitConA.Panel1.ResumeLayout(false);
            this.splitConA.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitConA)).EndInit();
            this.splitConA.ResumeLayout(false);
            this.splitConB.Panel1.ResumeLayout(false);
            this.splitConB.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitConB)).EndInit();
            this.splitConB.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart ChartPrice;
        private System.Windows.Forms.SplitContainer splitConA;
        private System.Windows.Forms.PropertyGrid ppGridMenu;
        private System.Windows.Forms.SplitContainer splitConB;
        private BasicSetting.WaveInfo.UcWaveInfo ucWaveInfo1;
    }
}
