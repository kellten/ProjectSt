namespace AnSt.BasicSetting.WaveInfo
{
    partial class UcWaveInfo
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
            this.splitConWave = new System.Windows.Forms.SplitContainer();
            this.lblStockName = new System.Windows.Forms.Label();
            this.lblStockCode = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvSca01 = new System.Windows.Forms.DataGridView();
            this.dgvSca02 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitConWave)).BeginInit();
            this.splitConWave.Panel1.SuspendLayout();
            this.splitConWave.Panel2.SuspendLayout();
            this.splitConWave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSca01)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSca02)).BeginInit();
            this.SuspendLayout();
            // 
            // splitConWave
            // 
            this.splitConWave.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitConWave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitConWave.Location = new System.Drawing.Point(0, 0);
            this.splitConWave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitConWave.Name = "splitConWave";
            this.splitConWave.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitConWave.Panel1
            // 
            this.splitConWave.Panel1.Controls.Add(this.lblStockName);
            this.splitConWave.Panel1.Controls.Add(this.lblStockCode);
            this.splitConWave.Panel1MinSize = 30;
            // 
            // splitConWave.Panel2
            // 
            this.splitConWave.Panel2.Controls.Add(this.splitContainer1);
            this.splitConWave.Size = new System.Drawing.Size(424, 525);
            this.splitConWave.SplitterDistance = 35;
            this.splitConWave.SplitterWidth = 9;
            this.splitConWave.TabIndex = 0;
            // 
            // lblStockName
            // 
            this.lblStockName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStockName.Location = new System.Drawing.Point(55, 5);
            this.lblStockName.Name = "lblStockName";
            this.lblStockName.Size = new System.Drawing.Size(243, 22);
            this.lblStockName.TabIndex = 6;
            this.lblStockName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStockCode
            // 
            this.lblStockCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStockCode.Location = new System.Drawing.Point(2, 5);
            this.lblStockCode.Name = "lblStockCode";
            this.lblStockCode.Size = new System.Drawing.Size(52, 22);
            this.lblStockCode.TabIndex = 5;
            this.lblStockCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvSca01);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvSca02);
            this.splitContainer1.Size = new System.Drawing.Size(424, 481);
            this.splitContainer1.SplitterDistance = 301;
            this.splitContainer1.SplitterWidth = 9;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvSca01
            // 
            this.dgvSca01.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSca01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSca01.Location = new System.Drawing.Point(0, 0);
            this.dgvSca01.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvSca01.Name = "dgvSca01";
            this.dgvSca01.RowTemplate.Height = 23;
            this.dgvSca01.Size = new System.Drawing.Size(420, 297);
            this.dgvSca01.TabIndex = 0;
            this.dgvSca01.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSca01_CellDoubleClick);
            // 
            // dgvSca02
            // 
            this.dgvSca02.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSca02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSca02.Location = new System.Drawing.Point(0, 0);
            this.dgvSca02.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvSca02.Name = "dgvSca02";
            this.dgvSca02.RowTemplate.Height = 23;
            this.dgvSca02.Size = new System.Drawing.Size(420, 167);
            this.dgvSca02.TabIndex = 1;
            // 
            // UcWaveInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitConWave);
            this.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UcWaveInfo";
            this.Size = new System.Drawing.Size(424, 525);
            this.splitConWave.Panel1.ResumeLayout(false);
            this.splitConWave.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitConWave)).EndInit();
            this.splitConWave.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSca01)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSca02)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitConWave;
        internal System.Windows.Forms.Label lblStockName;
        internal System.Windows.Forms.Label lblStockCode;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvSca01;
        private System.Windows.Forms.DataGridView dgvSca02;
    }
}
