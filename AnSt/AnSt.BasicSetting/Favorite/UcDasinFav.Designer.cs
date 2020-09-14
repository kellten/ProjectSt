namespace AnSt.BasicSetting.Favorite
{
    partial class UcDasinFav
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.PnList = new System.Windows.Forms.Panel();
            this.DvGroupList = new System.Windows.Forms.DataGridView();
            this.GroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrv = new System.Windows.Forms.Button();
            this.btnFav11 = new System.Windows.Forms.Button();
            this.btnFav10 = new System.Windows.Forms.Button();
            this.btnFav6 = new System.Windows.Forms.Button();
            this.btnFav9 = new System.Windows.Forms.Button();
            this.btnFav8 = new System.Windows.Forms.Button();
            this.btnFav7 = new System.Windows.Forms.Button();
            this.btnFav5 = new System.Windows.Forms.Button();
            this.btnFav4 = new System.Windows.Forms.Button();
            this.btnFav3 = new System.Windows.Forms.Button();
            this.btnFav2 = new System.Windows.Forms.Button();
            this.btnFav1 = new System.Windows.Forms.Button();
            this.btnFav0 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblSGroupName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvStockList = new System.Windows.Forms.DataGridView();
            this.STOCK_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STOCK_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.대신종목코드 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.PnList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DvGroupList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockList)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.PnList);
            this.splitContainer1.Panel1.Controls.Add(this.txtPath);
            this.splitContainer1.Panel1.Controls.Add(this.btnNext);
            this.splitContainer1.Panel1.Controls.Add(this.btnPrv);
            this.splitContainer1.Panel1.Controls.Add(this.btnFav11);
            this.splitContainer1.Panel1.Controls.Add(this.btnFav10);
            this.splitContainer1.Panel1.Controls.Add(this.btnFav6);
            this.splitContainer1.Panel1.Controls.Add(this.btnFav9);
            this.splitContainer1.Panel1.Controls.Add(this.btnFav8);
            this.splitContainer1.Panel1.Controls.Add(this.btnFav7);
            this.splitContainer1.Panel1.Controls.Add(this.btnFav5);
            this.splitContainer1.Panel1.Controls.Add(this.btnFav4);
            this.splitContainer1.Panel1.Controls.Add(this.btnFav3);
            this.splitContainer1.Panel1.Controls.Add(this.btnFav2);
            this.splitContainer1.Panel1.Controls.Add(this.btnFav1);
            this.splitContainer1.Panel1.Controls.Add(this.btnFav0);
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            this.splitContainer1.Panel1.Controls.Add(this.lblSGroupName);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvStockList);
            this.splitContainer1.Size = new System.Drawing.Size(346, 628);
            this.splitContainer1.SplitterDistance = 190;
            this.splitContainer1.TabIndex = 0;
            // 
            // PnList
            // 
            this.PnList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PnList.Controls.Add(this.DvGroupList);
            this.PnList.Location = new System.Drawing.Point(3, 29);
            this.PnList.Name = "PnList";
            this.PnList.Size = new System.Drawing.Size(530, 150);
            this.PnList.TabIndex = 27;
            // 
            // DvGroupList
            // 
            this.DvGroupList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DvGroupList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GroupName,
            this.GroupCode});
            this.DvGroupList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DvGroupList.Location = new System.Drawing.Point(0, 0);
            this.DvGroupList.Name = "DvGroupList";
            this.DvGroupList.RowTemplate.Height = 23;
            this.DvGroupList.Size = new System.Drawing.Size(526, 146);
            this.DvGroupList.TabIndex = 1;
            this.DvGroupList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DvGroupList_CellDoubleClick);
            // 
            // GroupName
            // 
            this.GroupName.HeaderText = "그룹명";
            this.GroupName.Name = "GroupName";
            this.GroupName.Width = 400;
            // 
            // GroupCode
            // 
            this.GroupCode.HeaderText = "그룹코드";
            this.GroupCode.Name = "GroupCode";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(188, 6);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(158, 20);
            this.txtPath.TabIndex = 26;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(381, 4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(35, 23);
            this.btnNext.TabIndex = 25;
            this.btnNext.Text = "▶";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrv
            // 
            this.btnPrv.Location = new System.Drawing.Point(347, 4);
            this.btnPrv.Name = "btnPrv";
            this.btnPrv.Size = new System.Drawing.Size(35, 23);
            this.btnPrv.TabIndex = 24;
            this.btnPrv.Text = "◀";
            this.btnPrv.UseVisualStyleBackColor = true;
            this.btnPrv.Click += new System.EventHandler(this.btnPrv_Click);
            // 
            // btnFav11
            // 
            this.btnFav11.Location = new System.Drawing.Point(801, 3);
            this.btnFav11.Name = "btnFav11";
            this.btnFav11.Size = new System.Drawing.Size(35, 23);
            this.btnFav11.TabIndex = 23;
            this.btnFav11.Text = "711";
            this.btnFav11.UseVisualStyleBackColor = true;
            this.btnFav11.Click += new System.EventHandler(this.btnFav11_Click);
            // 
            // btnFav10
            // 
            this.btnFav10.Location = new System.Drawing.Point(766, 3);
            this.btnFav10.Name = "btnFav10";
            this.btnFav10.Size = new System.Drawing.Size(35, 23);
            this.btnFav10.TabIndex = 22;
            this.btnFav10.Text = "710";
            this.btnFav10.UseVisualStyleBackColor = true;
            this.btnFav10.Click += new System.EventHandler(this.btnFav10_Click);
            // 
            // btnFav6
            // 
            this.btnFav6.Location = new System.Drawing.Point(627, 3);
            this.btnFav6.Name = "btnFav6";
            this.btnFav6.Size = new System.Drawing.Size(35, 23);
            this.btnFav6.TabIndex = 21;
            this.btnFav6.Text = "706";
            this.btnFav6.UseVisualStyleBackColor = true;
            this.btnFav6.Click += new System.EventHandler(this.btnFav6_Click);
            // 
            // btnFav9
            // 
            this.btnFav9.Location = new System.Drawing.Point(732, 3);
            this.btnFav9.Name = "btnFav9";
            this.btnFav9.Size = new System.Drawing.Size(35, 23);
            this.btnFav9.TabIndex = 20;
            this.btnFav9.Text = "709";
            this.btnFav9.UseVisualStyleBackColor = true;
            this.btnFav9.Click += new System.EventHandler(this.btnFav9_Click);
            // 
            // btnFav8
            // 
            this.btnFav8.Location = new System.Drawing.Point(697, 3);
            this.btnFav8.Name = "btnFav8";
            this.btnFav8.Size = new System.Drawing.Size(35, 23);
            this.btnFav8.TabIndex = 19;
            this.btnFav8.Text = "708";
            this.btnFav8.UseVisualStyleBackColor = true;
            this.btnFav8.Click += new System.EventHandler(this.btnFav8_Click);
            // 
            // btnFav7
            // 
            this.btnFav7.Location = new System.Drawing.Point(662, 3);
            this.btnFav7.Name = "btnFav7";
            this.btnFav7.Size = new System.Drawing.Size(35, 23);
            this.btnFav7.TabIndex = 18;
            this.btnFav7.Text = "707";
            this.btnFav7.UseVisualStyleBackColor = true;
            this.btnFav7.Click += new System.EventHandler(this.btnFav7_Click);
            // 
            // btnFav5
            // 
            this.btnFav5.Location = new System.Drawing.Point(593, 3);
            this.btnFav5.Name = "btnFav5";
            this.btnFav5.Size = new System.Drawing.Size(35, 23);
            this.btnFav5.TabIndex = 17;
            this.btnFav5.Text = "705";
            this.btnFav5.UseVisualStyleBackColor = true;
            this.btnFav5.Click += new System.EventHandler(this.btnFav5_Click);
            // 
            // btnFav4
            // 
            this.btnFav4.Location = new System.Drawing.Point(559, 3);
            this.btnFav4.Name = "btnFav4";
            this.btnFav4.Size = new System.Drawing.Size(35, 23);
            this.btnFav4.TabIndex = 16;
            this.btnFav4.Text = "704";
            this.btnFav4.UseVisualStyleBackColor = true;
            this.btnFav4.Click += new System.EventHandler(this.btnFav4_Click);
            // 
            // btnFav3
            // 
            this.btnFav3.Location = new System.Drawing.Point(524, 3);
            this.btnFav3.Name = "btnFav3";
            this.btnFav3.Size = new System.Drawing.Size(35, 23);
            this.btnFav3.TabIndex = 15;
            this.btnFav3.Text = "703";
            this.btnFav3.UseVisualStyleBackColor = true;
            this.btnFav3.Click += new System.EventHandler(this.btnFav3_Click);
            // 
            // btnFav2
            // 
            this.btnFav2.Location = new System.Drawing.Point(490, 3);
            this.btnFav2.Name = "btnFav2";
            this.btnFav2.Size = new System.Drawing.Size(35, 23);
            this.btnFav2.TabIndex = 14;
            this.btnFav2.Text = "702";
            this.btnFav2.UseVisualStyleBackColor = true;
            this.btnFav2.Click += new System.EventHandler(this.btnFav2_Click);
            // 
            // btnFav1
            // 
            this.btnFav1.Location = new System.Drawing.Point(456, 3);
            this.btnFav1.Name = "btnFav1";
            this.btnFav1.Size = new System.Drawing.Size(35, 23);
            this.btnFav1.TabIndex = 13;
            this.btnFav1.Tag = "";
            this.btnFav1.Text = "701";
            this.btnFav1.UseVisualStyleBackColor = true;
            this.btnFav1.Click += new System.EventHandler(this.btnFav1_Click);
            // 
            // btnFav0
            // 
            this.btnFav0.Location = new System.Drawing.Point(422, 3);
            this.btnFav0.Name = "btnFav0";
            this.btnFav0.Size = new System.Drawing.Size(35, 23);
            this.btnFav0.TabIndex = 12;
            this.btnFav0.Tag = "";
            this.btnFav0.Text = "700";
            this.btnFav0.UseVisualStyleBackColor = true;
            this.btnFav0.Click += new System.EventHandler(this.btnFav0_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(35, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(63, 20);
            this.textBox1.TabIndex = 11;
            // 
            // lblSGroupName
            // 
            this.lblSGroupName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSGroupName.Location = new System.Drawing.Point(99, 7);
            this.lblSGroupName.Name = "lblSGroupName";
            this.lblSGroupName.Size = new System.Drawing.Size(88, 18);
            this.lblSGroupName.TabIndex = 10;
            this.lblSGroupName.Text = "검색어";
            this.lblSGroupName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 11);
            this.label1.TabIndex = 0;
            this.label1.Text = "그룹";
            // 
            // dgvStockList
            // 
            this.dgvStockList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STOCK_NAME,
            this.STOCK_CODE,
            this.대신종목코드});
            this.dgvStockList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStockList.Location = new System.Drawing.Point(0, 0);
            this.dgvStockList.Name = "dgvStockList";
            this.dgvStockList.RowTemplate.Height = 23;
            this.dgvStockList.Size = new System.Drawing.Size(342, 430);
            this.dgvStockList.TabIndex = 0;
            this.dgvStockList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStockList_CellDoubleClick);
            // 
            // STOCK_NAME
            // 
            this.STOCK_NAME.HeaderText = "종목명";
            this.STOCK_NAME.Name = "STOCK_NAME";
            this.STOCK_NAME.Width = 300;
            // 
            // STOCK_CODE
            // 
            this.STOCK_CODE.HeaderText = "종목코드";
            this.STOCK_CODE.Name = "STOCK_CODE";
            // 
            // 대신종목코드
            // 
            this.대신종목코드.HeaderText = "DSTOCK_CODE";
            this.대신종목코드.Name = "대신종목코드";
            // 
            // UcDasinFav
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UcDasinFav";
            this.Size = new System.Drawing.Size(346, 628);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.PnList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DvGroupList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrv;
        private System.Windows.Forms.Button btnFav11;
        private System.Windows.Forms.Button btnFav10;
        private System.Windows.Forms.Button btnFav6;
        private System.Windows.Forms.Button btnFav9;
        private System.Windows.Forms.Button btnFav8;
        private System.Windows.Forms.Button btnFav7;
        private System.Windows.Forms.Button btnFav5;
        private System.Windows.Forms.Button btnFav4;
        private System.Windows.Forms.Button btnFav3;
        private System.Windows.Forms.Button btnFav2;
        private System.Windows.Forms.Button btnFav1;
        private System.Windows.Forms.Button btnFav0;
        private System.Windows.Forms.TextBox textBox1;
        internal System.Windows.Forms.Label lblSGroupName;
        private System.Windows.Forms.DataGridView dgvStockList;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn STOCK_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn STOCK_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn 대신종목코드;
        private System.Windows.Forms.Panel PnList;
        private System.Windows.Forms.DataGridView DvGroupList;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupCode;
    }
}
