namespace AnSt.BasicSetting.Favorite
{
    partial class UcFav
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
            this.splitConMain = new System.Windows.Forms.SplitContainer();
            this.ucStockList1 = new AnSt.BasicSetting.StockList.UcStockList();
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
            ((System.ComponentModel.ISupportInitialize)(this.splitConMain)).BeginInit();
            this.splitConMain.Panel1.SuspendLayout();
            this.splitConMain.Panel2.SuspendLayout();
            this.splitConMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitConFav
            // 
            this.splitConFav.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitConFav.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
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
            this.splitConFav.Size = new System.Drawing.Size(370, 477);
            this.splitConFav.SplitterDistance = 112;
            this.splitConFav.SplitterWidth = 9;
            this.splitConFav.TabIndex = 0;
            // 
            // dgvFCode
            // 
            this.dgvFCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFCode.Location = new System.Drawing.Point(0, 0);
            this.dgvFCode.Name = "dgvFCode";
            this.dgvFCode.RowTemplate.Height = 23;
            this.dgvFCode.Size = new System.Drawing.Size(366, 108);
            this.dgvFCode.TabIndex = 1;
            this.dgvFCode.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFCode_CellDoubleClick);
            this.dgvFCode.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFCode_RowEnter);
            this.dgvFCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvFCode_KeyDown);
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
            this.splitConMenu.Size = new System.Drawing.Size(370, 356);
            this.splitConMenu.SplitterDistance = 25;
            this.splitConMenu.SplitterWidth = 9;
            this.splitConMenu.TabIndex = 0;
            // 
            // pnMenu
            // 
            this.pnMenu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnMenu.Controls.Add(this.lblSGroupName);
            this.pnMenu.Controls.Add(this.btnUpDown);
            this.pnMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMenu.Location = new System.Drawing.Point(0, 0);
            this.pnMenu.Name = "pnMenu";
            this.pnMenu.Size = new System.Drawing.Size(366, 21);
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
            this.btnUpDown.Click += new System.EventHandler(this.btnUpDown_Click);
            // 
            // dgvStockList
            // 
            this.dgvStockList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStockList.Location = new System.Drawing.Point(0, 0);
            this.dgvStockList.Name = "dgvStockList";
            this.dgvStockList.RowTemplate.Height = 23;
            this.dgvStockList.Size = new System.Drawing.Size(366, 318);
            this.dgvStockList.TabIndex = 0;
            this.dgvStockList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStockList_CellDoubleClick);
            this.dgvStockList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvStockList_KeyDown);
            // 
            // splitConMain
            // 
            this.splitConMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitConMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitConMain.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.splitConMain.IsSplitterFixed = true;
            this.splitConMain.Location = new System.Drawing.Point(0, 0);
            this.splitConMain.Name = "splitConMain";
            // 
            // splitConMain.Panel1
            // 
            this.splitConMain.Panel1.Controls.Add(this.splitConFav);
            this.splitConMain.Panel1MinSize = 370;
            // 
            // splitConMain.Panel2
            // 
            this.splitConMain.Panel2.Controls.Add(this.ucStockList1);
            this.splitConMain.Panel2MinSize = 220;
            this.splitConMain.Size = new System.Drawing.Size(600, 477);
            this.splitConMain.SplitterDistance = 370;
            this.splitConMain.SplitterWidth = 8;
            this.splitConMain.TabIndex = 1;
            // 
            // ucStockList1
            // 
            this.ucStockList1.AutoSize = true;
            this.ucStockList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucStockList1.Font = new System.Drawing.Font("돋움", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ucStockList1.Location = new System.Drawing.Point(0, 0);
            this.ucStockList1.Name = "ucStockList1";
            this.ucStockList1.SGroupCode = "";
            this.ucStockList1.Size = new System.Drawing.Size(218, 473);
            this.ucStockList1.TabIndex = 0;
            // 
            // UcFav
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitConMain);
            this.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "UcFav";
            this.Size = new System.Drawing.Size(600, 477);
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
            this.splitConMain.Panel1.ResumeLayout(false);
            this.splitConMain.Panel2.ResumeLayout(false);
            this.splitConMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitConMain)).EndInit();
            this.splitConMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitConFav;
        private System.Windows.Forms.DataGridView dgvFCode;
        private System.Windows.Forms.SplitContainer splitConMenu;
        private System.Windows.Forms.Panel pnMenu;
        private System.Windows.Forms.Button btnUpDown;
        private System.Windows.Forms.DataGridView dgvStockList;
        private System.Windows.Forms.SplitContainer splitConMain;
        private StockList.UcStockList ucStockList1;
        internal System.Windows.Forms.Label lblSGroupName;
    }
}
