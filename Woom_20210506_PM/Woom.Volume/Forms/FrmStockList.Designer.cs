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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgv0 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.STOCK_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LAST_PRICE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STOCK_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv0)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1642, 977);
            this.tabControl1.TabIndex = 0;
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
            this.LAST_PRICE,
            this.STOCK_CODE});
            this.dgv0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv0.Location = new System.Drawing.Point(3, 3);
            this.dgv0.Name = "dgv0";
            this.dgv0.RowTemplate.Height = 23;
            this.dgv0.Size = new System.Drawing.Size(1628, 945);
            this.dgv0.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1634, 951);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // STOCK_NAME
            // 
            this.STOCK_NAME.HeaderText = "종목명";
            this.STOCK_NAME.Name = "STOCK_NAME";
            // 
            // LAST_PRICE
            // 
            this.LAST_PRICE.HeaderText = "종가";
            this.LAST_PRICE.Name = "LAST_PRICE";
            // 
            // STOCK_CODE
            // 
            this.STOCK_CODE.HeaderText = "종목코드";
            this.STOCK_CODE.Name = "STOCK_CODE";
            // 
            // FrmStockList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1642, 977);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmStockList";
            this.Text = "종목별 특징사항(FrmStockList)";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv0)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgv0;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridViewTextBoxColumn STOCK_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn LAST_PRICE;
        private System.Windows.Forms.DataGridViewTextBoxColumn STOCK_CODE;
    }
}