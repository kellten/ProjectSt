namespace Woom.CallForm.Uc
{
    partial class UcStockList
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
            this.label2 = new System.Windows.Forms.Label();
            this.splitConA = new System.Windows.Forms.SplitContainer();
            this.pnSearch = new System.Windows.Forms.Panel();
            this.chkEditMode = new System.Windows.Forms.CheckBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgvAllStockList = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitConA)).BeginInit();
            this.splitConA.Panel1.SuspendLayout();
            this.splitConA.Panel2.SuspendLayout();
            this.splitConA.SuspendLayout();
            this.pnSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllStockList)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(1, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "검색어";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.splitConA.Panel1.Controls.Add(this.pnSearch);
            this.splitConA.Panel1.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.splitConA.Panel1MinSize = 50;
            // 
            // splitConA.Panel2
            // 
            this.splitConA.Panel2.Controls.Add(this.dgvAllStockList);
            this.splitConA.Size = new System.Drawing.Size(571, 906);
            this.splitConA.SplitterDistance = 52;
            this.splitConA.SplitterWidth = 7;
            this.splitConA.TabIndex = 1;
            // 
            // pnSearch
            // 
            this.pnSearch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnSearch.Controls.Add(this.chkEditMode);
            this.pnSearch.Controls.Add(this.txtSearch);
            this.pnSearch.Controls.Add(this.label2);
            this.pnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnSearch.Location = new System.Drawing.Point(0, 0);
            this.pnSearch.Name = "pnSearch";
            this.pnSearch.Size = new System.Drawing.Size(567, 48);
            this.pnSearch.TabIndex = 0;
            // 
            // chkEditMode
            // 
            this.chkEditMode.AutoSize = true;
            this.chkEditMode.Location = new System.Drawing.Point(3, 23);
            this.chkEditMode.Name = "chkEditMode";
            this.chkEditMode.Size = new System.Drawing.Size(76, 17);
            this.chkEditMode.TabIndex = 10;
            this.chkEditMode.Text = "EditMode";
            this.chkEditMode.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(71, 1);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(140, 22);
            this.txtSearch.TabIndex = 9;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // dgvAllStockList
            // 
            this.dgvAllStockList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllStockList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAllStockList.Location = new System.Drawing.Point(0, 0);
            this.dgvAllStockList.Name = "dgvAllStockList";
            this.dgvAllStockList.RowTemplate.Height = 23;
            this.dgvAllStockList.Size = new System.Drawing.Size(567, 843);
            this.dgvAllStockList.TabIndex = 0;
            this.dgvAllStockList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAllStockList_CellDoubleClick);
            // 
            // UcStockList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitConA);
            this.Name = "UcStockList";
            this.Size = new System.Drawing.Size(571, 906);
            this.splitConA.Panel1.ResumeLayout(false);
            this.splitConA.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitConA)).EndInit();
            this.splitConA.ResumeLayout(false);
            this.pnSearch.ResumeLayout(false);
            this.pnSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllStockList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitConA;
        private System.Windows.Forms.Panel pnSearch;
        private System.Windows.Forms.CheckBox chkEditMode;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView dgvAllStockList;
    }
}
