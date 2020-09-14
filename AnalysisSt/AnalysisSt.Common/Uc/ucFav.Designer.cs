namespace AnalysisSt.Common.Uc
{
    partial class ucFav
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
            this.dgvFCode = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.txtSGroupName = new System.Windows.Forms.TextBox();
            this.lblSGroupCode = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.dgvFsa01 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFsa01)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvFCode
            // 
            this.dgvFCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFCode.Location = new System.Drawing.Point(0, 0);
            this.dgvFCode.Name = "dgvFCode";
            this.dgvFCode.RowTemplate.Height = 23;
            this.dgvFCode.Size = new System.Drawing.Size(444, 118);
            this.dgvFCode.TabIndex = 1;
            this.dgvFCode.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFCode_CellDoubleClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvFCode);
            this.splitContainer1.Panel1.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(444, 585);
            this.splitContainer1.SplitterDistance = 118;
            this.splitContainer1.TabIndex = 2;
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
            this.splitContainer2.Panel1.Controls.Add(this.txtSGroupName);
            this.splitContainer2.Panel1.Controls.Add(this.lblSGroupCode);
            this.splitContainer2.Panel1.Controls.Add(this.Label1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgvFsa01);
            this.splitContainer2.Size = new System.Drawing.Size(444, 463);
            this.splitContainer2.SplitterDistance = 25;
            this.splitContainer2.TabIndex = 3;
            // 
            // txtSGroupName
            // 
            this.txtSGroupName.Location = new System.Drawing.Point(133, -1);
            this.txtSGroupName.Name = "txtSGroupName";
            this.txtSGroupName.Size = new System.Drawing.Size(222, 21);
            this.txtSGroupName.TabIndex = 6;
            // 
            // lblSGroupCode
            // 
            this.lblSGroupCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSGroupCode.Location = new System.Drawing.Point(71, 0);
            this.lblSGroupCode.Name = "lblSGroupCode";
            this.lblSGroupCode.Size = new System.Drawing.Size(61, 19);
            this.lblSGroupCode.TabIndex = 5;
            this.lblSGroupCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label1
            // 
            this.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label1.Location = new System.Drawing.Point(3, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(67, 19);
            this.Label1.TabIndex = 4;
            this.Label1.Text = "그룹코드";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvFsa01
            // 
            this.dgvFsa01.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFsa01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFsa01.Location = new System.Drawing.Point(0, 0);
            this.dgvFsa01.Name = "dgvFsa01";
            this.dgvFsa01.RowTemplate.Height = 23;
            this.dgvFsa01.Size = new System.Drawing.Size(444, 434);
            this.dgvFsa01.TabIndex = 2;
            this.dgvFsa01.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFsa01_CellDoubleClick);
            // 
            // ucFav
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucFav";
            this.Size = new System.Drawing.Size(444, 585);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFCode)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFsa01)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFCode;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvFsa01;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox txtSGroupName;
        internal System.Windows.Forms.Label lblSGroupCode;
        internal System.Windows.Forms.Label Label1;
    }
}
