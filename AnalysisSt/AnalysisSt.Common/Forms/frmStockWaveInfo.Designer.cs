namespace AnalysisSt.Common.Forms
{
    partial class frmStockWaveInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tbCon = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ucFav = new AnalysisSt.Common.Uc.ucFav();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ucStockList = new AnalysisSt.Common.Uc.ucStockList();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.lblStockName = new System.Windows.Forms.Label();
            this.lblStockCode = new System.Windows.Forms.Label();
            this.gb0 = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.dgvSca01 = new System.Windows.Forms.DataGridView();
            this.dgvSca02 = new System.Windows.Forms.DataGridView();
            this.pnSca01 = new System.Windows.Forms.Panel();
            this.mskEndDate = new System.Windows.Forms.MaskedTextBox();
            this.mskStartDate = new System.Windows.Forms.MaskedTextBox();
            this.lblHighPrice = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.btnSca01Add = new System.Windows.Forms.Button();
            this.lblLowPrice = new System.Windows.Forms.Label();
            this.lblStarDate = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.btnGetMaxMinSca01 = new System.Windows.Forms.Button();
            this.txtBigFlow = new System.Windows.Forms.TextBox();
            this.lblStockCode2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tbCon.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.gb0.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSca01)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSca02)).BeginInit();
            this.pnSca01.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tbCon);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer1.Size = new System.Drawing.Size(1186, 573);
            this.splitContainer1.SplitterDistance = 445;
            this.splitContainer1.TabIndex = 0;
            // 
            // tbCon
            // 
            this.tbCon.Controls.Add(this.tabPage1);
            this.tbCon.Controls.Add(this.tabPage2);
            this.tbCon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCon.Location = new System.Drawing.Point(0, 0);
            this.tbCon.Name = "tbCon";
            this.tbCon.SelectedIndex = 0;
            this.tbCon.Size = new System.Drawing.Size(445, 573);
            this.tbCon.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.ucFav);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(437, 547);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "즐겨찾기";
            // 
            // ucFav
            // 
            this.ucFav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucFav.Location = new System.Drawing.Point(3, 3);
            this.ucFav.Name = "ucFav";
            this.ucFav.Size = new System.Drawing.Size(431, 541);
            this.ucFav.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.ucStockList);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(437, 547);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "전체";
            // 
            // ucStockList
            // 
            this.ucStockList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucStockList.Location = new System.Drawing.Point(3, 3);
            this.ucStockList.Name = "ucStockList";
            this.ucStockList.Size = new System.Drawing.Size(431, 541);
            this.ucStockList.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.lblStockName);
            this.splitContainer4.Panel1.Controls.Add(this.lblStockCode);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.gb0);
            this.splitContainer4.Size = new System.Drawing.Size(737, 573);
            this.splitContainer4.SplitterDistance = 36;
            this.splitContainer4.TabIndex = 0;
            // 
            // lblStockName
            // 
            this.lblStockName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStockName.Location = new System.Drawing.Point(65, 9);
            this.lblStockName.Name = "lblStockName";
            this.lblStockName.Size = new System.Drawing.Size(283, 19);
            this.lblStockName.TabIndex = 4;
            this.lblStockName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStockCode
            // 
            this.lblStockCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStockCode.Location = new System.Drawing.Point(3, 9);
            this.lblStockCode.Name = "lblStockCode";
            this.lblStockCode.Size = new System.Drawing.Size(61, 19);
            this.lblStockCode.TabIndex = 3;
            this.lblStockCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gb0
            // 
            this.gb0.Controls.Add(this.splitContainer2);
            this.gb0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb0.Location = new System.Drawing.Point(0, 0);
            this.gb0.Name = "gb0";
            this.gb0.Size = new System.Drawing.Size(737, 533);
            this.gb0.TabIndex = 5;
            this.gb0.TabStop = false;
            this.gb0.Text = "흐름정보";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 17);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pnSca01);
            this.splitContainer2.Size = new System.Drawing.Size(731, 513);
            this.splitContainer2.SplitterDistance = 325;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.dgvSca01);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.dgvSca02);
            this.splitContainer3.Size = new System.Drawing.Size(325, 513);
            this.splitContainer3.SplitterDistance = 206;
            this.splitContainer3.TabIndex = 0;
            // 
            // dgvSca01
            // 
            this.dgvSca01.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSca01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSca01.Location = new System.Drawing.Point(0, 0);
            this.dgvSca01.Name = "dgvSca01";
            this.dgvSca01.RowTemplate.Height = 23;
            this.dgvSca01.Size = new System.Drawing.Size(325, 206);
            this.dgvSca01.TabIndex = 0;
            this.dgvSca01.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSca01_CellDoubleClick);
            // 
            // dgvSca02
            // 
            this.dgvSca02.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSca02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSca02.Location = new System.Drawing.Point(0, 0);
            this.dgvSca02.Name = "dgvSca02";
            this.dgvSca02.RowTemplate.Height = 23;
            this.dgvSca02.Size = new System.Drawing.Size(325, 303);
            this.dgvSca02.TabIndex = 1;
            this.dgvSca02.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSca02_CellContentClick);
            // 
            // pnSca01
            // 
            this.pnSca01.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnSca01.Controls.Add(this.mskEndDate);
            this.pnSca01.Controls.Add(this.mskStartDate);
            this.pnSca01.Controls.Add(this.lblHighPrice);
            this.pnSca01.Controls.Add(this.lblEndDate);
            this.pnSca01.Controls.Add(this.btnSca01Add);
            this.pnSca01.Controls.Add(this.lblLowPrice);
            this.pnSca01.Controls.Add(this.lblStarDate);
            this.pnSca01.Controls.Add(this.dtpToDate);
            this.pnSca01.Controls.Add(this.dtpFromDate);
            this.pnSca01.Controls.Add(this.btnGetMaxMinSca01);
            this.pnSca01.Controls.Add(this.txtBigFlow);
            this.pnSca01.Controls.Add(this.lblStockCode2);
            this.pnSca01.Location = new System.Drawing.Point(3, 3);
            this.pnSca01.Name = "pnSca01";
            this.pnSca01.Size = new System.Drawing.Size(396, 103);
            this.pnSca01.TabIndex = 0;
            // 
            // mskEndDate
            // 
            this.mskEndDate.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.mskEndDate.Location = new System.Drawing.Point(77, 73);
            this.mskEndDate.Mask = "####.##.##";
            this.mskEndDate.Name = "mskEndDate";
            this.mskEndDate.Size = new System.Drawing.Size(68, 21);
            this.mskEndDate.TabIndex = 17;
            // 
            // mskStartDate
            // 
            this.mskStartDate.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.mskStartDate.Location = new System.Drawing.Point(3, 73);
            this.mskStartDate.Mask = "####.##.##";
            this.mskStartDate.Name = "mskStartDate";
            this.mskStartDate.Size = new System.Drawing.Size(68, 21);
            this.mskStartDate.TabIndex = 16;
            // 
            // lblHighPrice
            // 
            this.lblHighPrice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblHighPrice.Location = new System.Drawing.Point(65, 51);
            this.lblHighPrice.Name = "lblHighPrice";
            this.lblHighPrice.Size = new System.Drawing.Size(61, 19);
            this.lblHighPrice.TabIndex = 15;
            this.lblHighPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEndDate
            // 
            this.lblEndDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEndDate.Location = new System.Drawing.Point(3, 51);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(61, 19);
            this.lblEndDate.TabIndex = 14;
            this.lblEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSca01Add
            // 
            this.btnSca01Add.Location = new System.Drawing.Point(325, 63);
            this.btnSca01Add.Name = "btnSca01Add";
            this.btnSca01Add.Size = new System.Drawing.Size(63, 33);
            this.btnSca01Add.TabIndex = 13;
            this.btnSca01Add.Text = "입력";
            this.btnSca01Add.UseVisualStyleBackColor = true;
            this.btnSca01Add.Click += new System.EventHandler(this.btnSca01Add_Click);
            // 
            // lblLowPrice
            // 
            this.lblLowPrice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLowPrice.Location = new System.Drawing.Point(65, 30);
            this.lblLowPrice.Name = "lblLowPrice";
            this.lblLowPrice.Size = new System.Drawing.Size(61, 19);
            this.lblLowPrice.TabIndex = 12;
            this.lblLowPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStarDate
            // 
            this.lblStarDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStarDate.Location = new System.Drawing.Point(3, 30);
            this.lblStarDate.Name = "lblStarDate";
            this.lblStarDate.Size = new System.Drawing.Size(61, 19);
            this.lblStarDate.TabIndex = 11;
            this.lblStarDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(217, 8);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(102, 21);
            this.dtpToDate.TabIndex = 10;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(108, 7);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(102, 21);
            this.dtpFromDate.TabIndex = 9;
            // 
            // btnGetMaxMinSca01
            // 
            this.btnGetMaxMinSca01.Location = new System.Drawing.Point(325, 4);
            this.btnGetMaxMinSca01.Name = "btnGetMaxMinSca01";
            this.btnGetMaxMinSca01.Size = new System.Drawing.Size(63, 33);
            this.btnGetMaxMinSca01.TabIndex = 6;
            this.btnGetMaxMinSca01.Text = "가져오기";
            this.btnGetMaxMinSca01.UseVisualStyleBackColor = true;
            this.btnGetMaxMinSca01.Click += new System.EventHandler(this.btnGetMaxMinSca01_Click);
            // 
            // txtBigFlow
            // 
            this.txtBigFlow.Location = new System.Drawing.Point(65, 7);
            this.txtBigFlow.Name = "txtBigFlow";
            this.txtBigFlow.Size = new System.Drawing.Size(37, 21);
            this.txtBigFlow.TabIndex = 5;
            // 
            // lblStockCode2
            // 
            this.lblStockCode2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStockCode2.Location = new System.Drawing.Point(3, 8);
            this.lblStockCode2.Name = "lblStockCode2";
            this.lblStockCode2.Size = new System.Drawing.Size(61, 19);
            this.lblStockCode2.TabIndex = 4;
            this.lblStockCode2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmStockWaveInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1186, 573);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmStockWaveInfo";
            this.Text = "frmStockWaveInfo";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tbCon.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.gb0.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSca01)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSca02)).EndInit();
            this.pnSca01.ResumeLayout(false);
            this.pnSca01.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tbCon;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Uc.ucFav ucFav;
        private Uc.ucStockList ucStockList;
        internal System.Windows.Forms.Label lblStockName;
        internal System.Windows.Forms.Label lblStockCode;
        private System.Windows.Forms.GroupBox gb0;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataGridView dgvSca01;
        private System.Windows.Forms.DataGridView dgvSca02;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Panel pnSca01;
        private System.Windows.Forms.TextBox txtBigFlow;
        internal System.Windows.Forms.Label lblStockCode2;
        private System.Windows.Forms.Button btnGetMaxMinSca01;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Button btnSca01Add;
        internal System.Windows.Forms.Label lblLowPrice;
        internal System.Windows.Forms.Label lblStarDate;
        internal System.Windows.Forms.Label lblHighPrice;
        internal System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.MaskedTextBox mskEndDate;
        private System.Windows.Forms.MaskedTextBox mskStartDate;
    }
}