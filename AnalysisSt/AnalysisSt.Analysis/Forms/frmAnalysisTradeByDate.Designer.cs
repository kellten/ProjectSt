namespace AnalysisSt.Analysis.Forms
{
    partial class frmAnalysisTradeByDate
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lblStockName = new System.Windows.Forms.Label();
            this.lblStockCode = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.dgvSca01 = new System.Windows.Forms.DataGridView();
            this.btnJobStart = new System.Windows.Forms.Button();
            this.chkPrice = new System.Windows.Forms.CheckBox();
            this.pnQtyPriceGb = new System.Windows.Forms.Panel();
            this.rdoPrice = new System.Windows.Forms.RadioButton();
            this.rdoQty = new System.Windows.Forms.RadioButton();
            this.btnView = new System.Windows.Forms.Button();
            this.txtBigFlow = new System.Windows.Forms.TextBox();
            this.lblStockCode2 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.tbConNuJuk = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgvNuJuk10059Data = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tbCon.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSca01)).BeginInit();
            this.pnQtyPriceGb.SuspendLayout();
            this.tbConNuJuk.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNuJuk10059Data)).BeginInit();
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
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1254, 591);
            this.splitContainer1.SplitterDistance = 363;
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
            this.tbCon.Size = new System.Drawing.Size(363, 591);
            this.tbCon.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ucFav);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(355, 565);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "즐겨찾기";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ucFav
            // 
            this.ucFav.BackColor = System.Drawing.SystemColors.Control;
            this.ucFav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucFav.Location = new System.Drawing.Point(3, 3);
            this.ucFav.Name = "ucFav";
            this.ucFav.Size = new System.Drawing.Size(349, 559);
            this.ucFav.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.ucStockList);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(355, 565);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "전체";
            // 
            // ucStockList
            // 
            this.ucStockList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucStockList.Location = new System.Drawing.Point(3, 3);
            this.ucStockList.Name = "ucStockList";
            this.ucStockList.Size = new System.Drawing.Size(349, 559);
            this.ucStockList.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lblStockName);
            this.splitContainer2.Panel1.Controls.Add(this.lblStockCode);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(887, 591);
            this.splitContainer2.SplitterDistance = 34;
            this.splitContainer2.TabIndex = 0;
            // 
            // lblStockName
            // 
            this.lblStockName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStockName.Location = new System.Drawing.Point(65, 7);
            this.lblStockName.Name = "lblStockName";
            this.lblStockName.Size = new System.Drawing.Size(283, 19);
            this.lblStockName.TabIndex = 6;
            this.lblStockName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStockCode
            // 
            this.lblStockCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStockCode.Location = new System.Drawing.Point(3, 7);
            this.lblStockCode.Name = "lblStockCode";
            this.lblStockCode.Size = new System.Drawing.Size(61, 19);
            this.lblStockCode.TabIndex = 5;
            this.lblStockCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tbConNuJuk);
            this.splitContainer3.Size = new System.Drawing.Size(887, 553);
            this.splitContainer3.SplitterDistance = 133;
            this.splitContainer3.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.dgvSca01);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.btnJobStart);
            this.splitContainer4.Panel2.Controls.Add(this.chkPrice);
            this.splitContainer4.Panel2.Controls.Add(this.pnQtyPriceGb);
            this.splitContainer4.Panel2.Controls.Add(this.btnView);
            this.splitContainer4.Panel2.Controls.Add(this.txtBigFlow);
            this.splitContainer4.Panel2.Controls.Add(this.lblStockCode2);
            this.splitContainer4.Panel2.Controls.Add(this.dtpToDate);
            this.splitContainer4.Panel2.Controls.Add(this.dtpFromDate);
            this.splitContainer4.Size = new System.Drawing.Size(887, 133);
            this.splitContainer4.SplitterDistance = 505;
            this.splitContainer4.TabIndex = 0;
            // 
            // dgvSca01
            // 
            this.dgvSca01.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSca01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSca01.Location = new System.Drawing.Point(0, 0);
            this.dgvSca01.Name = "dgvSca01";
            this.dgvSca01.RowTemplate.Height = 23;
            this.dgvSca01.Size = new System.Drawing.Size(501, 129);
            this.dgvSca01.TabIndex = 1;
            this.dgvSca01.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSca01_CellDoubleClick);
            // 
            // btnJobStart
            // 
            this.btnJobStart.Location = new System.Drawing.Point(8, 59);
            this.btnJobStart.Name = "btnJobStart";
            this.btnJobStart.Size = new System.Drawing.Size(75, 23);
            this.btnJobStart.TabIndex = 18;
            this.btnJobStart.Text = "작업";
            this.btnJobStart.UseVisualStyleBackColor = true;
            this.btnJobStart.Click += new System.EventHandler(this.btnJobStart_Click);
            // 
            // chkPrice
            // 
            this.chkPrice.AutoSize = true;
            this.chkPrice.Location = new System.Drawing.Point(170, 35);
            this.chkPrice.Name = "chkPrice";
            this.chkPrice.Size = new System.Drawing.Size(54, 16);
            this.chkPrice.TabIndex = 17;
            this.chkPrice.Text = "100만";
            this.chkPrice.UseVisualStyleBackColor = true;
            // 
            // pnQtyPriceGb
            // 
            this.pnQtyPriceGb.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnQtyPriceGb.Controls.Add(this.rdoPrice);
            this.pnQtyPriceGb.Controls.Add(this.rdoQty);
            this.pnQtyPriceGb.Location = new System.Drawing.Point(3, 30);
            this.pnQtyPriceGb.Name = "pnQtyPriceGb";
            this.pnQtyPriceGb.Size = new System.Drawing.Size(116, 23);
            this.pnQtyPriceGb.TabIndex = 16;
            // 
            // rdoPrice
            // 
            this.rdoPrice.AutoSize = true;
            this.rdoPrice.Location = new System.Drawing.Point(3, 2);
            this.rdoPrice.Name = "rdoPrice";
            this.rdoPrice.Size = new System.Drawing.Size(47, 16);
            this.rdoPrice.TabIndex = 18;
            this.rdoPrice.TabStop = true;
            this.rdoPrice.Text = "금액";
            this.rdoPrice.UseVisualStyleBackColor = true;
            // 
            // rdoQty
            // 
            this.rdoQty.AutoSize = true;
            this.rdoQty.Location = new System.Drawing.Point(60, 1);
            this.rdoQty.Name = "rdoQty";
            this.rdoQty.Size = new System.Drawing.Size(47, 16);
            this.rdoQty.TabIndex = 17;
            this.rdoQty.TabStop = true;
            this.rdoQty.Text = "수량";
            this.rdoQty.UseVisualStyleBackColor = true;
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(125, 30);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(39, 23);
            this.btnView.TabIndex = 15;
            this.btnView.Text = "!";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // txtBigFlow
            // 
            this.txtBigFlow.Location = new System.Drawing.Point(65, 3);
            this.txtBigFlow.Name = "txtBigFlow";
            this.txtBigFlow.Size = new System.Drawing.Size(37, 21);
            this.txtBigFlow.TabIndex = 14;
            // 
            // lblStockCode2
            // 
            this.lblStockCode2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStockCode2.Location = new System.Drawing.Point(3, 4);
            this.lblStockCode2.Name = "lblStockCode2";
            this.lblStockCode2.Size = new System.Drawing.Size(61, 19);
            this.lblStockCode2.TabIndex = 13;
            this.lblStockCode2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(217, 4);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(102, 21);
            this.dtpToDate.TabIndex = 12;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(108, 3);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(102, 21);
            this.dtpFromDate.TabIndex = 11;
            // 
            // tbConNuJuk
            // 
            this.tbConNuJuk.Controls.Add(this.tabPage3);
            this.tbConNuJuk.Controls.Add(this.tabPage4);
            this.tbConNuJuk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbConNuJuk.Location = new System.Drawing.Point(0, 0);
            this.tbConNuJuk.Name = "tbConNuJuk";
            this.tbConNuJuk.SelectedIndex = 0;
            this.tbConNuJuk.Size = new System.Drawing.Size(883, 412);
            this.tbConNuJuk.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(875, 386);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "그래프";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgvNuJuk10059Data);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(875, 386);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "데이터";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgvNuJuk10059Data
            // 
            this.dgvNuJuk10059Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNuJuk10059Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNuJuk10059Data.Location = new System.Drawing.Point(3, 3);
            this.dgvNuJuk10059Data.Name = "dgvNuJuk10059Data";
            this.dgvNuJuk10059Data.RowTemplate.Height = 23;
            this.dgvNuJuk10059Data.Size = new System.Drawing.Size(869, 380);
            this.dgvNuJuk10059Data.TabIndex = 2;
            // 
            // frmAnalysisTradeByDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 591);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmAnalysisTradeByDate";
            this.Text = "frmAnalysisTradeByDate";
            this.Load += new System.EventHandler(this.frmAnalysisTradeByDate_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tbCon.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSca01)).EndInit();
            this.pnQtyPriceGb.ResumeLayout(false);
            this.pnQtyPriceGb.PerformLayout();
            this.tbConNuJuk.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNuJuk10059Data)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tbCon;
        private System.Windows.Forms.TabPage tabPage1;
        private Common.Uc.ucFav ucFav;
        private System.Windows.Forms.TabPage tabPage2;
        private Common.Uc.ucStockList ucStockList;
        private System.Windows.Forms.SplitContainer splitContainer2;
        internal System.Windows.Forms.Label lblStockName;
        internal System.Windows.Forms.Label lblStockCode;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.DataGridView dgvSca01;
        private System.Windows.Forms.DataGridView dgvNuJuk10059Data;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.TextBox txtBigFlow;
        internal System.Windows.Forms.Label lblStockCode2;
        private System.Windows.Forms.TabControl tbConNuJuk;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Panel pnQtyPriceGb;
        private System.Windows.Forms.RadioButton rdoPrice;
        private System.Windows.Forms.RadioButton rdoQty;
        private System.Windows.Forms.CheckBox chkPrice;
        private System.Windows.Forms.Button btnJobStart;

    }
}