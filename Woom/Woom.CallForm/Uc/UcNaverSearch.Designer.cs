
namespace Woom.CallForm.Uc
{
    partial class UcNaverSearch
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
            this.label3 = new System.Windows.Forms.Label();
            this.trackBarDisplayCounts = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxKeyword = new System.Windows.Forms.TextBox();
            this.chkStockName = new System.Windows.Forms.CheckBox();
            this.txtAddWord = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvNaverSearch = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.제목 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.본문 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.링크 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDisplayCounts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNaverSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "검색 결과 출력 개수";
            // 
            // trackBarDisplayCounts
            // 
            this.trackBarDisplayCounts.Location = new System.Drawing.Point(122, 9);
            this.trackBarDisplayCounts.Maximum = 100;
            this.trackBarDisplayCounts.Minimum = 10;
            this.trackBarDisplayCounts.Name = "trackBarDisplayCounts";
            this.trackBarDisplayCounts.Size = new System.Drawing.Size(147, 45);
            this.trackBarDisplayCounts.TabIndex = 8;
            this.trackBarDisplayCounts.TickFrequency = 10;
            this.trackBarDisplayCounts.Value = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(287, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "키워드";
            // 
            // textBoxKeyword
            // 
            this.textBoxKeyword.Location = new System.Drawing.Point(334, 10);
            this.textBoxKeyword.Name = "textBoxKeyword";
            this.textBoxKeyword.Size = new System.Drawing.Size(122, 21);
            this.textBoxKeyword.TabIndex = 11;
            this.textBoxKeyword.Text = "속보 경제";
            // 
            // chkStockName
            // 
            this.chkStockName.AutoSize = true;
            this.chkStockName.Location = new System.Drawing.Point(748, 12);
            this.chkStockName.Name = "chkStockName";
            this.chkStockName.Size = new System.Drawing.Size(112, 16);
            this.chkStockName.TabIndex = 12;
            this.chkStockName.Text = "종목명으로 조회";
            this.chkStockName.UseVisualStyleBackColor = true;
            // 
            // txtAddWord
            // 
            this.txtAddWord.Location = new System.Drawing.Point(509, 9);
            this.txtAddWord.Name = "txtAddWord";
            this.txtAddWord.Size = new System.Drawing.Size(100, 21);
            this.txtAddWord.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(462, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "붙임글";
            // 
            // dgvNaverSearch
            // 
            this.dgvNaverSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNaverSearch.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgvNaverSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNaverSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.제목,
            this.본문,
            this.링크});
            this.dgvNaverSearch.Location = new System.Drawing.Point(3, 60);
            this.dgvNaverSearch.Name = "dgvNaverSearch";
            this.dgvNaverSearch.RowTemplate.Height = 23;
            this.dgvNaverSearch.Size = new System.Drawing.Size(857, 616);
            this.dgvNaverSearch.TabIndex = 15;
            this.dgvNaverSearch.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNaverSearch_CellDoubleClick);
            // 
            // No
            // 
            this.No.HeaderText = "No.";
            this.No.Name = "No";
            this.No.Width = 30;
            // 
            // 제목
            // 
            this.제목.HeaderText = "제목";
            this.제목.Name = "제목";
            this.제목.Width = 300;
            // 
            // 본문
            // 
            this.본문.HeaderText = "본문";
            this.본문.Name = "본문";
            this.본문.Width = 600;
            // 
            // 링크
            // 
            this.링크.HeaderText = "링크";
            this.링크.Name = "링크";
            // 
            // UcNaverSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvNaverSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAddWord);
            this.Controls.Add(this.chkStockName);
            this.Controls.Add(this.textBoxKeyword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trackBarDisplayCounts);
            this.Name = "UcNaverSearch";
            this.Size = new System.Drawing.Size(863, 679);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDisplayCounts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNaverSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBarDisplayCounts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxKeyword;
        private System.Windows.Forms.CheckBox chkStockName;
        private System.Windows.Forms.TextBox txtAddWord;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvNaverSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn 제목;
        private System.Windows.Forms.DataGridViewTextBoxColumn 본문;
        private System.Windows.Forms.DataGridViewTextBoxColumn 링크;
    }
}
