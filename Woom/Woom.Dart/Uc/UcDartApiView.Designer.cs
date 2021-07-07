namespace Woom.Dart.Uc
{
    partial class UcDartApiView
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
            this.dgvDartView = new System.Windows.Forms.DataGridView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.chkAddSearch = new System.Windows.Forms.CheckBox();
            this.stock_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rcept_dt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.report_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rcept_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stock_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.corp_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDartView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvDartView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1073, 690);
            this.splitContainer1.SplitterDistance = 442;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvDartView
            // 
            this.dgvDartView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDartView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stock_name,
            this.rcept_dt,
            this.report_nm,
            this.rcept_no,
            this.stock_code,
            this.corp_code});
            this.dgvDartView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDartView.Location = new System.Drawing.Point(0, 0);
            this.dgvDartView.Name = "dgvDartView";
            this.dgvDartView.RowTemplate.Height = 23;
            this.dgvDartView.Size = new System.Drawing.Size(440, 688);
            this.dgvDartView.TabIndex = 0;
            this.dgvDartView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDartView_CellDoubleClick);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.chkAddSearch);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.webBrowser1);
            this.splitContainer2.Size = new System.Drawing.Size(627, 690);
            this.splitContainer2.SplitterDistance = 45;
            this.splitContainer2.TabIndex = 0;
            // 
            // chkAddSearch
            // 
            this.chkAddSearch.AutoSize = true;
            this.chkAddSearch.Location = new System.Drawing.Point(3, 15);
            this.chkAddSearch.Name = "chkAddSearch";
            this.chkAddSearch.Size = new System.Drawing.Size(86, 16);
            this.chkAddSearch.TabIndex = 17;
            this.chkAddSearch.Text = "AddSearch";
            this.chkAddSearch.UseVisualStyleBackColor = true;
            // 
            // stock_name
            // 
            this.stock_name.HeaderText = "종목명";
            this.stock_name.Name = "stock_name";
            // 
            // rcept_dt
            // 
            this.rcept_dt.HeaderText = "공시일";
            this.rcept_dt.Name = "rcept_dt";
            this.rcept_dt.Width = 70;
            // 
            // report_nm
            // 
            this.report_nm.HeaderText = "공시명";
            this.report_nm.Name = "report_nm";
            this.report_nm.Width = 300;
            // 
            // rcept_no
            // 
            this.rcept_no.HeaderText = "고유번호";
            this.rcept_no.Name = "rcept_no";
            // 
            // stock_code
            // 
            this.stock_code.HeaderText = "stock_code";
            this.stock_code.Name = "stock_code";
            // 
            // corp_code
            // 
            this.corp_code.HeaderText = "corp_code";
            this.corp_code.Name = "corp_code";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(625, 639);
            this.webBrowser1.TabIndex = 0;
            // 
            // UcDartApiView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UcDartApiView";
            this.Size = new System.Drawing.Size(1073, 690);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDartView)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvDartView;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.CheckBox chkAddSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn stock_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn rcept_dt;
        private System.Windows.Forms.DataGridViewTextBoxColumn report_nm;
        private System.Windows.Forms.DataGridViewTextBoxColumn rcept_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn stock_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn corp_code;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}
