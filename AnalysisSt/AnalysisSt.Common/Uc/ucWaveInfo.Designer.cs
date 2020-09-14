namespace AnalysisSt.Common.Uc
{
    partial class ucWaveInfo
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
            this.lblStockName = new System.Windows.Forms.Label();
            this.lblStockCode = new System.Windows.Forms.Label();
            this.dgvSca01 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitConA)).BeginInit();
            this.splitConA.Panel1.SuspendLayout();
            this.splitConA.Panel2.SuspendLayout();
            this.splitConA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSca01)).BeginInit();
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
            this.splitConA.Panel1.Controls.Add(this.lblStockName);
            this.splitConA.Panel1.Controls.Add(this.lblStockCode);
            // 
            // splitConA.Panel2
            // 
            this.splitConA.Panel2.Controls.Add(this.dgvSca01);
            this.splitConA.Size = new System.Drawing.Size(244, 534);
            this.splitConA.SplitterDistance = 40;
            this.splitConA.TabIndex = 0;
            // 
            // lblStockName
            // 
            this.lblStockName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStockName.Location = new System.Drawing.Point(-2, 17);
            this.lblStockName.Name = "lblStockName";
            this.lblStockName.Size = new System.Drawing.Size(244, 19);
            this.lblStockName.TabIndex = 6;
            this.lblStockName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStockCode
            // 
            this.lblStockCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStockCode.Location = new System.Drawing.Point(-2, -2);
            this.lblStockCode.Name = "lblStockCode";
            this.lblStockCode.Size = new System.Drawing.Size(93, 19);
            this.lblStockCode.TabIndex = 5;
            this.lblStockCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvSca01
            // 
            this.dgvSca01.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSca01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSca01.Location = new System.Drawing.Point(0, 0);
            this.dgvSca01.Name = "dgvSca01";
            this.dgvSca01.RowTemplate.Height = 23;
            this.dgvSca01.Size = new System.Drawing.Size(240, 486);
            this.dgvSca01.TabIndex = 1;
            this.dgvSca01.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSca01_CellDoubleClick);
            // 
            // ucWaveInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitConA);
            this.Name = "ucWaveInfo";
            this.Size = new System.Drawing.Size(244, 534);
            this.Load += new System.EventHandler(this.ucWaveInfo_Load);
            this.splitConA.Panel1.ResumeLayout(false);
            this.splitConA.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitConA)).EndInit();
            this.splitConA.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSca01)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitConA;
        private System.Windows.Forms.DataGridView dgvSca01;
        internal System.Windows.Forms.Label lblStockName;
        internal System.Windows.Forms.Label lblStockCode;
    }
}
