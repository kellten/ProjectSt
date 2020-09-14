namespace AnalysisSt.Analysis.Uc
{
    partial class ucAnalysisA
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
            this.splitConA = new System.Windows.Forms.SplitContainer();
            this.ucPrice0 = new AnalysisSt.Chart.ucChart.ucPrice();
            this.ucVolume0 = new AnalysisSt.Chart.ucChart.ucVolume();
            ((System.ComponentModel.ISupportInitialize)(this.splitConA)).BeginInit();
            this.splitConA.Panel1.SuspendLayout();
            this.splitConA.Panel2.SuspendLayout();
            this.splitConA.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitConA
            // 
            this.splitConA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitConA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitConA.Location = new System.Drawing.Point(0, 0);
            this.splitConA.Name = "splitConA";
            this.splitConA.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitConA.Panel1
            // 
            this.splitConA.Panel1.Controls.Add(this.ucPrice0);
            // 
            // splitConA.Panel2
            // 
            this.splitConA.Panel2.Controls.Add(this.ucVolume0);
            this.splitConA.Size = new System.Drawing.Size(798, 595);
            this.splitConA.SplitterDistance = 279;
            this.splitConA.TabIndex = 0;
            // 
            // ucPrice0
            // 
            this.ucPrice0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPrice0.FromDate = null;
            this.ucPrice0.Location = new System.Drawing.Point(0, 0);
            this.ucPrice0.Name = "ucPrice0";
            this.ucPrice0.Size = new System.Drawing.Size(794, 275);
            this.ucPrice0.StockCode = null;
            this.ucPrice0.TabIndex = 0;
            this.ucPrice0.ToDate = null;
            // 
            // ucVolume0
            // 
            this.ucVolume0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucVolume0.FromDate = null;
            this.ucVolume0.Location = new System.Drawing.Point(0, 0);
            this.ucVolume0.Name = "ucVolume0";
            this.ucVolume0.Size = new System.Drawing.Size(794, 308);
            this.ucVolume0.StockCode = null;
            this.ucVolume0.TabIndex = 0;
            this.ucVolume0.ToDate = null;
            // 
            // ucAnalysisA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitConA);
            this.Name = "ucAnalysisA";
            this.Size = new System.Drawing.Size(798, 595);
            this.splitConA.Panel1.ResumeLayout(false);
            this.splitConA.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitConA)).EndInit();
            this.splitConA.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitConA;
        private Chart.ucChart.ucPrice ucPrice0;
        private Chart.ucChart.ucVolume ucVolume0;
    }
}
