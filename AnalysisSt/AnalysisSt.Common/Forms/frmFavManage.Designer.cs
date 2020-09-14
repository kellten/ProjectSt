namespace AnalysisSt.Common.Forms
{
    partial class frmFavManage
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
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgvAllStockList = new System.Windows.Forms.DataGridView();
            this.tbCon = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvFCode = new System.Windows.Forms.DataGridView();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.chkGet10060 = new System.Windows.Forms.CheckBox();
            this.dtpTradeDate = new System.Windows.Forms.DateTimePicker();
            this.btnViewToday = new System.Windows.Forms.Button();
            this.btnChgSeqNo = new System.Windows.Forms.Button();
            this.btnNewSGroup = new System.Windows.Forms.Button();
            this.btnChgSGroupName = new System.Windows.Forms.Button();
            this.txtSGroupName = new System.Windows.Forms.TextBox();
            this.lblSGroupCode = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.dgvFsa01 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.dgvTodayTrade = new System.Windows.Forms.DataGridView();
            this.btnViewTodayVer2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllStockList)).BeginInit();
            this.tbCon.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFsa01)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTodayTrade)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbCon);
            this.splitContainer1.Size = new System.Drawing.Size(1027, 626);
            this.splitContainer1.SplitterDistance = 230;
            this.splitContainer1.TabIndex = 0;
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
            this.splitContainer4.Panel1.Controls.Add(this.label2);
            this.splitContainer4.Panel1.Controls.Add(this.txtSearch);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.dgvAllStockList);
            this.splitContainer4.Size = new System.Drawing.Size(230, 626);
            this.splitContainer4.SplitterDistance = 36;
            this.splitContainer4.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "검색어";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(72, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(155, 21);
            this.txtSearch.TabIndex = 4;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // dgvAllStockList
            // 
            this.dgvAllStockList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllStockList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAllStockList.Location = new System.Drawing.Point(0, 0);
            this.dgvAllStockList.Name = "dgvAllStockList";
            this.dgvAllStockList.RowTemplate.Height = 23;
            this.dgvAllStockList.Size = new System.Drawing.Size(230, 586);
            this.dgvAllStockList.TabIndex = 0;
            this.dgvAllStockList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAllStockList_CellDoubleClick);
            // 
            // tbCon
            // 
            this.tbCon.Controls.Add(this.tabPage1);
            this.tbCon.Controls.Add(this.tabPage2);
            this.tbCon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCon.Location = new System.Drawing.Point(0, 0);
            this.tbCon.Name = "tbCon";
            this.tbCon.SelectedIndex = 0;
            this.tbCon.Size = new System.Drawing.Size(793, 626);
            this.tbCon.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.splitContainer2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(785, 600);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Manage";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvFCode);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(779, 594);
            this.splitContainer2.SplitterDistance = 259;
            this.splitContainer2.TabIndex = 0;
            // 
            // dgvFCode
            // 
            this.dgvFCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFCode.Location = new System.Drawing.Point(0, 0);
            this.dgvFCode.Name = "dgvFCode";
            this.dgvFCode.RowTemplate.Height = 23;
            this.dgvFCode.Size = new System.Drawing.Size(259, 594);
            this.dgvFCode.TabIndex = 0;
            this.dgvFCode.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFCode_CellDoubleClick);
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
            this.splitContainer3.Panel1.Controls.Add(this.btnViewTodayVer2);
            this.splitContainer3.Panel1.Controls.Add(this.chkGet10060);
            this.splitContainer3.Panel1.Controls.Add(this.dtpTradeDate);
            this.splitContainer3.Panel1.Controls.Add(this.btnViewToday);
            this.splitContainer3.Panel1.Controls.Add(this.btnChgSeqNo);
            this.splitContainer3.Panel1.Controls.Add(this.btnNewSGroup);
            this.splitContainer3.Panel1.Controls.Add(this.btnChgSGroupName);
            this.splitContainer3.Panel1.Controls.Add(this.txtSGroupName);
            this.splitContainer3.Panel1.Controls.Add(this.lblSGroupCode);
            this.splitContainer3.Panel1.Controls.Add(this.Label1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.dgvFsa01);
            this.splitContainer3.Size = new System.Drawing.Size(516, 594);
            this.splitContainer3.SplitterDistance = 92;
            this.splitContainer3.TabIndex = 0;
            // 
            // chkGet10060
            // 
            this.chkGet10060.AutoSize = true;
            this.chkGet10060.Location = new System.Drawing.Point(334, 33);
            this.chkGet10060.Name = "chkGet10060";
            this.chkGet10060.Size = new System.Drawing.Size(86, 16);
            this.chkGet10060.TabIndex = 9;
            this.chkGet10060.Text = "checkBox1";
            this.chkGet10060.UseVisualStyleBackColor = true;
            // 
            // dtpTradeDate
            // 
            this.dtpTradeDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTradeDate.Location = new System.Drawing.Point(114, 29);
            this.dtpTradeDate.Name = "dtpTradeDate";
            this.dtpTradeDate.Size = new System.Drawing.Size(102, 21);
            this.dtpTradeDate.TabIndex = 8;
            // 
            // btnViewToday
            // 
            this.btnViewToday.Location = new System.Drawing.Point(222, 29);
            this.btnViewToday.Name = "btnViewToday";
            this.btnViewToday.Size = new System.Drawing.Size(106, 23);
            this.btnViewToday.TabIndex = 7;
            this.btnViewToday.Text = "당일내역보기";
            this.btnViewToday.UseVisualStyleBackColor = true;
            this.btnViewToday.Click += new System.EventHandler(this.btnViewToday_Click);
            // 
            // btnChgSeqNo
            // 
            this.btnChgSeqNo.Location = new System.Drawing.Point(434, 60);
            this.btnChgSeqNo.Name = "btnChgSeqNo";
            this.btnChgSeqNo.Size = new System.Drawing.Size(75, 23);
            this.btnChgSeqNo.TabIndex = 6;
            this.btnChgSeqNo.Text = "순서변경";
            this.btnChgSeqNo.UseVisualStyleBackColor = true;
            this.btnChgSeqNo.Click += new System.EventHandler(this.btnChgSeqNo_Click);
            // 
            // btnNewSGroup
            // 
            this.btnNewSGroup.Location = new System.Drawing.Point(12, 29);
            this.btnNewSGroup.Name = "btnNewSGroup";
            this.btnNewSGroup.Size = new System.Drawing.Size(75, 23);
            this.btnNewSGroup.TabIndex = 5;
            this.btnNewSGroup.Text = "신규";
            this.btnNewSGroup.UseVisualStyleBackColor = true;
            this.btnNewSGroup.Click += new System.EventHandler(this.btnNewSGroup_Click);
            // 
            // btnChgSGroupName
            // 
            this.btnChgSGroupName.Location = new System.Drawing.Point(367, 6);
            this.btnChgSGroupName.Name = "btnChgSGroupName";
            this.btnChgSGroupName.Size = new System.Drawing.Size(37, 23);
            this.btnChgSGroupName.TabIndex = 4;
            this.btnChgSGroupName.Text = "수정";
            this.btnChgSGroupName.UseVisualStyleBackColor = true;
            this.btnChgSGroupName.Click += new System.EventHandler(this.btnChgSGroupName_Click);
            // 
            // txtSGroupName
            // 
            this.txtSGroupName.Location = new System.Drawing.Point(142, 6);
            this.txtSGroupName.Name = "txtSGroupName";
            this.txtSGroupName.Size = new System.Drawing.Size(222, 21);
            this.txtSGroupName.TabIndex = 3;
            // 
            // lblSGroupCode
            // 
            this.lblSGroupCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSGroupCode.Location = new System.Drawing.Point(80, 7);
            this.lblSGroupCode.Name = "lblSGroupCode";
            this.lblSGroupCode.Size = new System.Drawing.Size(61, 19);
            this.lblSGroupCode.TabIndex = 2;
            this.lblSGroupCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label1
            // 
            this.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label1.Location = new System.Drawing.Point(12, 7);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(67, 19);
            this.Label1.TabIndex = 1;
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
            this.dgvFsa01.Size = new System.Drawing.Size(516, 498);
            this.dgvFsa01.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer5);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(785, 600);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ToDayTradeInfo";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(3, 3);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.dgvTodayTrade);
            this.splitContainer5.Size = new System.Drawing.Size(779, 594);
            this.splitContainer5.SplitterDistance = 480;
            this.splitContainer5.TabIndex = 0;
            // 
            // dgvTodayTrade
            // 
            this.dgvTodayTrade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTodayTrade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTodayTrade.Location = new System.Drawing.Point(0, 0);
            this.dgvTodayTrade.Name = "dgvTodayTrade";
            this.dgvTodayTrade.RowTemplate.Height = 23;
            this.dgvTodayTrade.Size = new System.Drawing.Size(779, 110);
            this.dgvTodayTrade.TabIndex = 1;
            // 
            // btnViewTodayVer2
            // 
            this.btnViewTodayVer2.Location = new System.Drawing.Point(222, 58);
            this.btnViewTodayVer2.Name = "btnViewTodayVer2";
            this.btnViewTodayVer2.Size = new System.Drawing.Size(106, 23);
            this.btnViewTodayVer2.TabIndex = 10;
            this.btnViewTodayVer2.Text = "당일내역보기";
            this.btnViewTodayVer2.UseVisualStyleBackColor = true;
            this.btnViewTodayVer2.Click += new System.EventHandler(this.btnViewTodayVer2_Click);
            // 
            // frmFavManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 626);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmFavManage";
            this.Text = "frmFavManage";
            this.Load += new System.EventHandler(this.frmFavManage_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllStockList)).EndInit();
            this.tbCon.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFCode)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFsa01)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTodayTrade)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvAllStockList;
        private System.Windows.Forms.TabControl tbCon;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgvFCode;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataGridView dgvFsa01;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnNewSGroup;
        private System.Windows.Forms.Button btnChgSGroupName;
        private System.Windows.Forms.TextBox txtSGroupName;
        internal System.Windows.Forms.Label lblSGroupCode;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Button btnChgSeqNo;
        private System.Windows.Forms.SplitContainer splitContainer4;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.DataGridView dgvTodayTrade;
        private System.Windows.Forms.Button btnViewToday;
        private System.Windows.Forms.DateTimePicker dtpTradeDate;
        private System.Windows.Forms.CheckBox chkGet10060;
        private System.Windows.Forms.Button btnViewTodayVer2;
    }
}