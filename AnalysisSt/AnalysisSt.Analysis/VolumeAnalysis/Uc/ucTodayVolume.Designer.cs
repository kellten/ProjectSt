namespace AnalysisSt.Analysis.VolumeAnalysis.Uc
{
    partial class ucTodayVolume
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
            this.dgvTodayVolume0 = new System.Windows.Forms.DataGridView();
            this.splitCon0 = new System.Windows.Forms.SplitContainer();
            this.btnPrv = new System.Windows.Forms.Button();
            this.dtpStdDate = new System.Windows.Forms.DateTimePicker();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTodayVolume0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitCon0)).BeginInit();
            this.splitCon0.Panel1.SuspendLayout();
            this.splitCon0.Panel2.SuspendLayout();
            this.splitCon0.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvTodayVolume0
            // 
            this.dgvTodayVolume0.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTodayVolume0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTodayVolume0.Location = new System.Drawing.Point(0, 0);
            this.dgvTodayVolume0.Name = "dgvTodayVolume0";
            this.dgvTodayVolume0.RowTemplate.Height = 23;
            this.dgvTodayVolume0.Size = new System.Drawing.Size(674, 279);
            this.dgvTodayVolume0.TabIndex = 0;
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
            this.splitCon0.Panel1.Controls.Add(this.btnPar);
            this.splitCon0.Panel1.Controls.Add(this.btnNext);
            this.splitCon0.Panel1.Controls.Add(this.dtpStdDate);
            this.splitCon0.Panel1.Controls.Add(this.btnPrv);
            // 
            // splitCon0.Panel2
            // 
            this.splitCon0.Panel2.Controls.Add(this.dgvTodayVolume0);
            this.splitCon0.Size = new System.Drawing.Size(674, 309);
            this.splitCon0.SplitterDistance = 26;
            this.splitCon0.TabIndex = 1;
            // 
            // btnPrv
            // 
            this.btnPrv.Location = new System.Drawing.Point(3, 3);
            this.btnPrv.Name = "btnPrv";
            this.btnPrv.Size = new System.Drawing.Size(50, 21);
            this.btnPrv.TabIndex = 0;
            this.btnPrv.Text = "◀";
            this.btnPrv.UseVisualStyleBackColor = true;
            this.btnPrv.Click += new System.EventHandler(this.btnPrv_Click);
            // 
            // dtpStdDate
            // 
            this.dtpStdDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStdDate.Location = new System.Drawing.Point(59, 3);
            this.dtpStdDate.Name = "dtpStdDate";
            this.dtpStdDate.Size = new System.Drawing.Size(102, 21);
            this.dtpStdDate.TabIndex = 12;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(167, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(50, 21);
            this.btnNext.TabIndex = 13;
            this.btnNext.Text = "▶";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPar
            // 
            this.btnPar.Location = new System.Drawing.Point(223, 3);
            this.btnPar.Name = "btnPar";
            this.btnPar.Size = new System.Drawing.Size(90, 21);
            this.btnPar.TabIndex = 14;
            this.btnPar.Text = "병렬조회";
            this.btnPar.UseVisualStyleBackColor = true;
            this.btnPar.Click += new System.EventHandler(this.btnPar_Click);
            // 
            // ucTodayVolume
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitCon0);
            this.Name = "ucTodayVolume";
            this.Size = new System.Drawing.Size(674, 309);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTodayVolume0)).EndInit();
            this.splitCon0.Panel1.ResumeLayout(false);
            this.splitCon0.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCon0)).EndInit();
            this.splitCon0.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTodayVolume0;
        private System.Windows.Forms.SplitContainer splitCon0;
        private System.Windows.Forms.Button btnPrv;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.DateTimePicker dtpStdDate;
        private System.Windows.Forms.Button btnPar;
    }
}
