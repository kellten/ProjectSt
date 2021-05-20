namespace Woom.Volume.Forms
{
    partial class FrmStockList
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
            this.dgv1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgv0 = new System.Windows.Forms.DataGridView();
            this.STOCK_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UPDOWN_RATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LAST_PRICE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.START_PRICE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HIGH_PRICE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOW_PRICE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STOCK_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ChkOption1 = new System.Windows.Forms.CheckBox();
            this.BtnGiganUpDowndSearch = new System.Windows.Forms.Button();
            this.numUpDownRate = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dgvGiganUpDown = new System.Windows.Forms.DataGridView();
            this.dgv1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv0)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiganUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv1
            // 
            this.dgv1.Controls.Add(this.tabPage1);
            this.dgv1.Controls.Add(this.tabPage2);
            this.dgv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv1.Location = new System.Drawing.Point(0, 0);
            this.dgv1.Name = "dgv1";
            this.dgv1.SelectedIndex = 0;
            this.dgv1.Size = new System.Drawing.Size(1642, 977);
            this.dgv1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgv0);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1634, 951);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "섹터별 상위상승 종목";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgv0
            // 
            this.dgv0.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv0.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STOCK_NAME,
            this.UPDOWN_RATE,
            this.LAST_PRICE,
            this.START_PRICE,
            this.HIGH_PRICE,
            this.LOW_PRICE,
            this.STOCK_CODE});
            this.dgv0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv0.Location = new System.Drawing.Point(3, 3);
            this.dgv0.Name = "dgv0";
            this.dgv0.RowTemplate.Height = 23;
            this.dgv0.Size = new System.Drawing.Size(1628, 945);
            this.dgv0.TabIndex = 0;
            // 
            // STOCK_NAME
            // 
            this.STOCK_NAME.HeaderText = "종목명";
            this.STOCK_NAME.Name = "STOCK_NAME";
            // 
            // UPDOWN_RATE
            // 
            this.UPDOWN_RATE.HeaderText = "상승률";
            this.UPDOWN_RATE.Name = "UPDOWN_RATE";
            // 
            // LAST_PRICE
            // 
            this.LAST_PRICE.HeaderText = "종가";
            this.LAST_PRICE.Name = "LAST_PRICE";
            // 
            // START_PRICE
            // 
            this.START_PRICE.HeaderText = "시작가";
            this.START_PRICE.Name = "START_PRICE";
            // 
            // HIGH_PRICE
            // 
            this.HIGH_PRICE.HeaderText = "고가";
            this.HIGH_PRICE.Name = "HIGH_PRICE";
            // 
            // LOW_PRICE
            // 
            this.LOW_PRICE.HeaderText = "저가";
            this.LOW_PRICE.Name = "LOW_PRICE";
            // 
            // STOCK_CODE
            // 
            this.STOCK_CODE.HeaderText = "종목코드";
            this.STOCK_CODE.Name = "STOCK_CODE";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1634, 951);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "기간별 상승 종목";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ChkOption1);
            this.splitContainer1.Panel1.Controls.Add(this.BtnGiganUpDowndSearch);
            this.splitContainer1.Panel1.Controls.Add(this.numUpDownRate);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.dtpStartDate);
            this.splitContainer1.Panel1.Controls.Add(this.dtpEndDate);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvGiganUpDown);
            this.splitContainer1.Size = new System.Drawing.Size(1628, 945);
            this.splitContainer1.SplitterDistance = 38;
            this.splitContainer1.TabIndex = 0;
            // 
            // ChkOption1
            // 
            this.ChkOption1.AutoSize = true;
            this.ChkOption1.Checked = true;
            this.ChkOption1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkOption1.Location = new System.Drawing.Point(532, 13);
            this.ChkOption1.Name = "ChkOption1";
            this.ChkOption1.Size = new System.Drawing.Size(84, 16);
            this.ChkOption1.TabIndex = 40;
            this.ChkOption1.Text = "상승단위로";
            this.ChkOption1.UseVisualStyleBackColor = true;
            // 
            // BtnGiganUpDowndSearch
            // 
            this.BtnGiganUpDowndSearch.Location = new System.Drawing.Point(451, 9);
            this.BtnGiganUpDowndSearch.Name = "BtnGiganUpDowndSearch";
            this.BtnGiganUpDowndSearch.Size = new System.Drawing.Size(75, 23);
            this.BtnGiganUpDowndSearch.TabIndex = 39;
            this.BtnGiganUpDowndSearch.Text = "조회";
            this.BtnGiganUpDowndSearch.UseVisualStyleBackColor = true;
            this.BtnGiganUpDowndSearch.Click += new System.EventHandler(this.BtnGiganUpDowndSearch_Click);
            // 
            // numUpDownRate
            // 
            this.numUpDownRate.Location = new System.Drawing.Point(402, 10);
            this.numUpDownRate.Name = "numUpDownRate";
            this.numUpDownRate.Size = new System.Drawing.Size(43, 21);
            this.numUpDownRate.TabIndex = 38;
            this.numUpDownRate.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(194, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 12);
            this.label1.TabIndex = 37;
            this.label1.Text = " ~ ";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(13, 9);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(175, 21);
            this.dtpStartDate.TabIndex = 36;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(222, 9);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(174, 21);
            this.dtpEndDate.TabIndex = 35;
            // 
            // dgvGiganUpDown
            // 
            this.dgvGiganUpDown.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGiganUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGiganUpDown.Location = new System.Drawing.Point(0, 0);
            this.dgvGiganUpDown.Name = "dgvGiganUpDown";
            this.dgvGiganUpDown.RowTemplate.Height = 23;
            this.dgvGiganUpDown.Size = new System.Drawing.Size(1624, 899);
            this.dgvGiganUpDown.TabIndex = 0;
            // 
            // FrmStockList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1642, 977);
            this.Controls.Add(this.dgv1);
            this.Name = "FrmStockList";
            this.Text = "종목별 특징사항(FrmStockList)";
            this.dgv1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv0)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiganUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl dgv1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgv0;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridViewTextBoxColumn STOCK_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn UPDOWN_RATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn LAST_PRICE;
        private System.Windows.Forms.DataGridViewTextBoxColumn START_PRICE;
        private System.Windows.Forms.DataGridViewTextBoxColumn HIGH_PRICE;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOW_PRICE;
        private System.Windows.Forms.DataGridViewTextBoxColumn STOCK_CODE;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvGiganUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.NumericUpDown numUpDownRate;
        private System.Windows.Forms.Button BtnGiganUpDowndSearch;
        private System.Windows.Forms.CheckBox ChkOption1;
    }
}