namespace AnalysisSt.Analysis.VolumeAnalysis.Uc
{
    partial class ucVolumeAnalysis0
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
            this.splitCon0 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMinTradeDaeGum = new System.Windows.Forms.Label();
            this.splitCon1 = new System.Windows.Forms.SplitContainer();
            this.pnVolume0Data = new System.Windows.Forms.Panel();
            this.dgvVolume1 = new System.Windows.Forms.DataGridView();
            this.주체1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.매도1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.매수1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.범위1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.주체2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.매도2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.매수2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.범위2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvVolume0 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitCon0)).BeginInit();
            this.splitCon0.Panel1.SuspendLayout();
            this.splitCon0.Panel2.SuspendLayout();
            this.splitCon0.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCon1)).BeginInit();
            this.splitCon1.Panel1.SuspendLayout();
            this.splitCon1.Panel2.SuspendLayout();
            this.splitCon1.SuspendLayout();
            this.pnVolume0Data.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVolume1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVolume0)).BeginInit();
            this.SuspendLayout();
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
            this.splitCon0.Panel1.Controls.Add(this.label2);
            this.splitCon0.Panel1.Controls.Add(this.lblMinTradeDaeGum);
            // 
            // splitCon0.Panel2
            // 
            this.splitCon0.Panel2.Controls.Add(this.splitCon1);
            this.splitCon0.Size = new System.Drawing.Size(866, 681);
            this.splitCon0.SplitterDistance = 281;
            this.splitCon0.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(43, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "최저거래금액";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMinTradeDaeGum
            // 
            this.lblMinTradeDaeGum.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMinTradeDaeGum.Location = new System.Drawing.Point(125, 169);
            this.lblMinTradeDaeGum.Name = "lblMinTradeDaeGum";
            this.lblMinTradeDaeGum.Size = new System.Drawing.Size(102, 19);
            this.lblMinTradeDaeGum.TabIndex = 7;
            this.lblMinTradeDaeGum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitCon1
            // 
            this.splitCon1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitCon1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCon1.Location = new System.Drawing.Point(0, 0);
            this.splitCon1.Name = "splitCon1";
            this.splitCon1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitCon1.Panel1
            // 
            this.splitCon1.Panel1.Controls.Add(this.pnVolume0Data);
            // 
            // splitCon1.Panel2
            // 
            this.splitCon1.Panel2.Controls.Add(this.dgvVolume0);
            this.splitCon1.Size = new System.Drawing.Size(866, 396);
            this.splitCon1.SplitterDistance = 169;
            this.splitCon1.TabIndex = 0;
            // 
            // pnVolume0Data
            // 
            this.pnVolume0Data.Controls.Add(this.dgvVolume1);
            this.pnVolume0Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnVolume0Data.Location = new System.Drawing.Point(0, 0);
            this.pnVolume0Data.Name = "pnVolume0Data";
            this.pnVolume0Data.Size = new System.Drawing.Size(862, 165);
            this.pnVolume0Data.TabIndex = 0;
            // 
            // dgvVolume1
            // 
            this.dgvVolume1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVolume1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.주체1,
            this.매도1,
            this.매수1,
            this.범위1,
            this.주체2,
            this.매도2,
            this.매수2,
            this.범위2});
            this.dgvVolume1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVolume1.Location = new System.Drawing.Point(0, 0);
            this.dgvVolume1.Name = "dgvVolume1";
            this.dgvVolume1.RowHeadersVisible = false;
            this.dgvVolume1.RowTemplate.Height = 23;
            this.dgvVolume1.Size = new System.Drawing.Size(862, 165);
            this.dgvVolume1.TabIndex = 0;
            // 
            // 주체1
            // 
            this.주체1.HeaderText = "주체1";
            this.주체1.Name = "주체1";
            this.주체1.Width = 60;
            // 
            // 매도1
            // 
            this.매도1.HeaderText = "매도1";
            this.매도1.Name = "매도1";
            // 
            // 매수1
            // 
            this.매수1.HeaderText = "매수1";
            this.매수1.Name = "매수1";
            // 
            // 범위1
            // 
            this.범위1.HeaderText = "범위1";
            this.범위1.Name = "범위1";
            // 
            // 주체2
            // 
            this.주체2.HeaderText = "주체2";
            this.주체2.Name = "주체2";
            this.주체2.Width = 60;
            // 
            // 매도2
            // 
            this.매도2.HeaderText = "매도2";
            this.매도2.Name = "매도2";
            // 
            // 매수2
            // 
            this.매수2.HeaderText = "매수2";
            this.매수2.Name = "매수2";
            // 
            // 범위2
            // 
            this.범위2.HeaderText = "범위2";
            this.범위2.Name = "범위2";
            // 
            // dgvVolume0
            // 
            this.dgvVolume0.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVolume0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVolume0.Location = new System.Drawing.Point(0, 0);
            this.dgvVolume0.Name = "dgvVolume0";
            this.dgvVolume0.RowTemplate.Height = 23;
            this.dgvVolume0.Size = new System.Drawing.Size(862, 219);
            this.dgvVolume0.TabIndex = 0;
            // 
            // ucVolumeAnalysis0
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitCon0);
            this.Name = "ucVolumeAnalysis0";
            this.Size = new System.Drawing.Size(866, 681);
            this.Load += new System.EventHandler(this.ucVolumeAnalysis0_Load);
            this.splitCon0.Panel1.ResumeLayout(false);
            this.splitCon0.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCon0)).EndInit();
            this.splitCon0.ResumeLayout(false);
            this.splitCon1.Panel1.ResumeLayout(false);
            this.splitCon1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCon1)).EndInit();
            this.splitCon1.ResumeLayout(false);
            this.pnVolume0Data.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVolume1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVolume0)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitCon0;
        private System.Windows.Forms.DataGridView dgvVolume0;
        private System.Windows.Forms.SplitContainer splitCon1;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label lblMinTradeDaeGum;
        private System.Windows.Forms.Panel pnVolume0Data;
        private System.Windows.Forms.DataGridView dgvVolume1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 주체1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 매도1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 매수1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 범위1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 주체2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 매도2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 매수2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 범위2;
    }
}
