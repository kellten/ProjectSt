namespace AnalysisSt.Chart.ucChart
{
    partial class ucPrice
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
            this.chartPrice = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.splitCon1 = new System.Windows.Forms.SplitContainer();
            this.btnSaveAtt = new System.Windows.Forms.Button();
            this.propertyGrid0 = new System.Windows.Forms.PropertyGrid();
            this.splitCon0 = new System.Windows.Forms.SplitContainer();
            this.chkAttView = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.chartPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitCon1)).BeginInit();
            this.splitCon1.Panel1.SuspendLayout();
            this.splitCon1.Panel2.SuspendLayout();
            this.splitCon1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCon0)).BeginInit();
            this.splitCon0.Panel1.SuspendLayout();
            this.splitCon0.Panel2.SuspendLayout();
            this.splitCon0.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartPrice
            // 
            this.chartPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartPrice.Location = new System.Drawing.Point(0, 0);
            this.chartPrice.Name = "chartPrice";
            this.chartPrice.Size = new System.Drawing.Size(421, 391);
            this.chartPrice.TabIndex = 0;
            this.chartPrice.Text = "chart1";
            this.chartPrice.Click += new System.EventHandler(this.chartPrice_Click);
            // 
            // splitCon1
            // 
            this.splitCon1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCon1.Location = new System.Drawing.Point(0, 0);
            this.splitCon1.Name = "splitCon1";
            // 
            // splitCon1.Panel1
            // 
            this.splitCon1.Panel1.Controls.Add(this.btnSaveAtt);
            this.splitCon1.Panel1.Controls.Add(this.propertyGrid0);
            // 
            // splitCon1.Panel2
            // 
            this.splitCon1.Panel2.Controls.Add(this.chartPrice);
            this.splitCon1.Size = new System.Drawing.Size(605, 391);
            this.splitCon1.SplitterDistance = 180;
            this.splitCon1.TabIndex = 1;
            this.splitCon1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitCon1_SplitterMoved);
            // 
            // btnSaveAtt
            // 
            this.btnSaveAtt.Location = new System.Drawing.Point(79, -1);
            this.btnSaveAtt.Name = "btnSaveAtt";
            this.btnSaveAtt.Size = new System.Drawing.Size(75, 23);
            this.btnSaveAtt.TabIndex = 1;
            this.btnSaveAtt.Text = "설정저장";
            this.btnSaveAtt.UseVisualStyleBackColor = true;
            this.btnSaveAtt.Click += new System.EventHandler(this.btnSaveAtt_Click);
            // 
            // propertyGrid0
            // 
            this.propertyGrid0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid0.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.propertyGrid0.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGrid0.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid0.Name = "propertyGrid0";
            this.propertyGrid0.Size = new System.Drawing.Size(180, 391);
            this.propertyGrid0.TabIndex = 0;
            this.propertyGrid0.Click += new System.EventHandler(this.propertyGrid0_Click);
            // 
            // splitCon0
            // 
            this.splitCon0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCon0.Location = new System.Drawing.Point(0, 0);
            this.splitCon0.Name = "splitCon0";
            this.splitCon0.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitCon0.Panel1
            // 
            this.splitCon0.Panel1.Controls.Add(this.chkAttView);
            this.splitCon0.Panel1MinSize = 30;
            // 
            // splitCon0.Panel2
            // 
            this.splitCon0.Panel2.Controls.Add(this.splitCon1);
            this.splitCon0.Size = new System.Drawing.Size(605, 425);
            this.splitCon0.SplitterDistance = 30;
            this.splitCon0.TabIndex = 2;
            this.splitCon0.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitCon0_SplitterMoved);
            // 
            // chkAttView
            // 
            this.chkAttView.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkAttView.AutoSize = true;
            this.chkAttView.Location = new System.Drawing.Point(3, 2);
            this.chkAttView.Name = "chkAttView";
            this.chkAttView.Size = new System.Drawing.Size(51, 22);
            this.chkAttView.TabIndex = 0;
            this.chkAttView.Text = "설정뷰";
            this.chkAttView.UseVisualStyleBackColor = true;
            this.chkAttView.CheckedChanged += new System.EventHandler(this.chkAttView_CheckedChanged);
            // 
            // ucPrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitCon0);
            this.Name = "ucPrice";
            this.Size = new System.Drawing.Size(605, 425);
            this.Load += new System.EventHandler(this.ucPrice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartPrice)).EndInit();
            this.splitCon1.Panel1.ResumeLayout(false);
            this.splitCon1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCon1)).EndInit();
            this.splitCon1.ResumeLayout(false);
            this.splitCon0.Panel1.ResumeLayout(false);
            this.splitCon0.Panel1.PerformLayout();
            this.splitCon0.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCon0)).EndInit();
            this.splitCon0.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartPrice;
        private System.Windows.Forms.SplitContainer splitCon1;
        private System.Windows.Forms.PropertyGrid propertyGrid0;
        private System.Windows.Forms.SplitContainer splitCon0;
        private System.Windows.Forms.CheckBox chkAttView;
        private System.Windows.Forms.Button btnSaveAtt;
    }
}
