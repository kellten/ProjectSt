
namespace Woom.Dart.Forms
{
    partial class FrmDartCaller
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
            this.BtnDartCall = new System.Windows.Forms.Button();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.corp_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stock_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STOCK_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnExcelExport = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnDartCall
            // 
            this.BtnDartCall.Location = new System.Drawing.Point(12, 12);
            this.BtnDartCall.Name = "BtnDartCall";
            this.BtnDartCall.Size = new System.Drawing.Size(140, 44);
            this.BtnDartCall.TabIndex = 0;
            this.BtnDartCall.Text = "고유번호 가져오기";
            this.BtnDartCall.UseVisualStyleBackColor = true;
            this.BtnDartCall.Click += new System.EventHandler(this.BtnDartCall_Click);
            // 
            // dgvList
            // 
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.corp_code,
            this.stock_code,
            this.STOCK_NAME});
            this.dgvList.Location = new System.Drawing.Point(19, 72);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowTemplate.Height = 23;
            this.dgvList.Size = new System.Drawing.Size(761, 358);
            this.dgvList.TabIndex = 1;
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            this.dgvList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellDoubleClick);
            // 
            // corp_code
            // 
            this.corp_code.HeaderText = "고유번호";
            this.corp_code.Name = "corp_code";
            // 
            // stock_code
            // 
            this.stock_code.HeaderText = "종목코드";
            this.stock_code.Name = "stock_code";
            // 
            // STOCK_NAME
            // 
            this.STOCK_NAME.HeaderText = "종목명";
            this.STOCK_NAME.Name = "STOCK_NAME";
            this.STOCK_NAME.Width = 200;
            // 
            // BtnExcelExport
            // 
            this.BtnExcelExport.Location = new System.Drawing.Point(612, 12);
            this.BtnExcelExport.Name = "BtnExcelExport";
            this.BtnExcelExport.Size = new System.Drawing.Size(168, 34);
            this.BtnExcelExport.TabIndex = 2;
            this.BtnExcelExport.Text = "ExcelExport";
            this.BtnExcelExport.UseVisualStyleBackColor = true;
            this.BtnExcelExport.Click += new System.EventHandler(this.BtnExcelExport_Click);
            // 
            // FrmDartCaller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnExcelExport);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.BtnDartCall);
            this.Name = "FrmDartCaller";
            this.Text = "FrmDartCaller";
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnDartCall;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.DataGridViewTextBoxColumn corp_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn stock_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn STOCK_NAME;
        private System.Windows.Forms.Button BtnExcelExport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}