namespace AnSt.BasicSetting.Favorite
{
    partial class UcSimpleFav
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
            this.splitConFav = new System.Windows.Forms.SplitContainer();
            this.dgvFCode = new System.Windows.Forms.DataGridView();
            this.splitConMenu = new System.Windows.Forms.SplitContainer();
            this.pnMenu = new System.Windows.Forms.Panel();
            this.lblSGroupName = new System.Windows.Forms.Label();
            this.btnUpDown = new System.Windows.Forms.Button();
            this.dgvStockList = new System.Windows.Forms.DataGridView();
            this.btnSend = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitConFav)).BeginInit();
            this.splitConFav.Panel1.SuspendLayout();
            this.splitConFav.Panel2.SuspendLayout();
            this.splitConFav.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitConMenu)).BeginInit();
            this.splitConMenu.Panel1.SuspendLayout();
            this.splitConMenu.Panel2.SuspendLayout();
            this.splitConMenu.SuspendLayout();
            this.pnMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockList)).BeginInit();
            this.SuspendLayout();
            // 
            // splitConFav
            // 
            this.splitConFav.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitConFav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitConFav.Location = new System.Drawing.Point(0, 0);
            this.splitConFav.Name = "splitConFav";
            this.splitConFav.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitConFav.Panel1
            // 
            this.splitConFav.Panel1.Controls.Add(this.dgvFCode);
            this.splitConFav.Panel1.Font = new System.Drawing.Font("돋움", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            // 
            // splitConFav.Panel2
            // 
            this.splitConFav.Panel2.Controls.Add(this.splitConMenu);
            this.splitConFav.Size = new System.Drawing.Size(358, 608);
            this.splitConFav.SplitterDistance = 142;
            this.splitConFav.SplitterWidth = 9;
            this.splitConFav.TabIndex = 1;
            // 
            // dgvFCode
            // 
            this.dgvFCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFCode.Location = new System.Drawing.Point(0, 0);
            this.dgvFCode.Name = "dgvFCode";
            this.dgvFCode.RowTemplate.Height = 23;
            this.dgvFCode.Size = new System.Drawing.Size(354, 138);
            this.dgvFCode.TabIndex = 1;
            this.dgvFCode.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFCode_CellDoubleClick);
            // 
            // splitConMenu
            // 
            this.splitConMenu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitConMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitConMenu.Location = new System.Drawing.Point(0, 0);
            this.splitConMenu.Name = "splitConMenu";
            this.splitConMenu.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitConMenu.Panel1
            // 
            this.splitConMenu.Panel1.Controls.Add(this.pnMenu);
            // 
            // splitConMenu.Panel2
            // 
            this.splitConMenu.Panel2.Controls.Add(this.dgvStockList);
            this.splitConMenu.Size = new System.Drawing.Size(358, 457);
            this.splitConMenu.SplitterDistance = 32;
            this.splitConMenu.SplitterWidth = 9;
            this.splitConMenu.TabIndex = 0;
            // 
            // pnMenu
            // 
            this.pnMenu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnMenu.Controls.Add(this.btnSend);
            this.pnMenu.Controls.Add(this.lblSGroupName);
            this.pnMenu.Controls.Add(this.btnUpDown);
            this.pnMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMenu.Location = new System.Drawing.Point(0, 0);
            this.pnMenu.Name = "pnMenu";
            this.pnMenu.Size = new System.Drawing.Size(354, 28);
            this.pnMenu.TabIndex = 0;
            // 
            // lblSGroupName
            // 
            this.lblSGroupName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSGroupName.Location = new System.Drawing.Point(49, 0);
            this.lblSGroupName.Name = "lblSGroupName";
            this.lblSGroupName.Size = new System.Drawing.Size(163, 18);
            this.lblSGroupName.TabIndex = 9;
            this.lblSGroupName.Text = "검색어";
            this.lblSGroupName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnUpDown
            // 
            this.btnUpDown.Location = new System.Drawing.Point(-1, -2);
            this.btnUpDown.Name = "btnUpDown";
            this.btnUpDown.Size = new System.Drawing.Size(51, 25);
            this.btnUpDown.TabIndex = 0;
            this.btnUpDown.UseVisualStyleBackColor = true;
            // 
            // dgvStockList
            // 
            this.dgvStockList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStockList.Location = new System.Drawing.Point(0, 0);
            this.dgvStockList.Name = "dgvStockList";
            this.dgvStockList.RowTemplate.Height = 23;
            this.dgvStockList.Size = new System.Drawing.Size(354, 412);
            this.dgvStockList.TabIndex = 0;
            this.dgvStockList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStockList_CellDoubleClick);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(218, 0);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(51, 25);
            this.btnSend.TabIndex = 10;
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // UcSimpleFav
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitConFav);
            this.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "UcSimpleFav";
            this.Size = new System.Drawing.Size(358, 608);
            this.splitConFav.Panel1.ResumeLayout(false);
            this.splitConFav.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitConFav)).EndInit();
            this.splitConFav.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFCode)).EndInit();
            this.splitConMenu.Panel1.ResumeLayout(false);
            this.splitConMenu.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitConMenu)).EndInit();
            this.splitConMenu.ResumeLayout(false);
            this.pnMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitConFav;
        private System.Windows.Forms.DataGridView dgvFCode;
        private System.Windows.Forms.SplitContainer splitConMenu;
        private System.Windows.Forms.Panel pnMenu;
        internal System.Windows.Forms.Label lblSGroupName;
        private System.Windows.Forms.Button btnUpDown;
        private System.Windows.Forms.DataGridView dgvStockList;
        private System.Windows.Forms.Button btnSend;
    }
}
