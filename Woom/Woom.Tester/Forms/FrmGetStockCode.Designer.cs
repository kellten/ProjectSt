namespace Woom.Tester.Forms
{
    partial class FrmGetStockCode
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
            this.dgv0 = new System.Windows.Forms.DataGridView();
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.STOCK_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STOCK_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnStartSync = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.dgv0);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgv1);
            this.splitContainer1.Size = new System.Drawing.Size(794, 796);
            this.splitContainer1.SplitterDistance = 375;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgv0
            // 
            this.dgv0.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv0.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STOCK_CODE,
            this.STOCK_NAME});
            this.dgv0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv0.Location = new System.Drawing.Point(0, 0);
            this.dgv0.Name = "dgv0";
            this.dgv0.RowTemplate.Height = 23;
            this.dgv0.Size = new System.Drawing.Size(375, 796);
            this.dgv0.TabIndex = 0;
            // 
            // dgv1
            // 
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv1.Location = new System.Drawing.Point(0, 0);
            this.dgv1.Name = "dgv1";
            this.dgv1.RowTemplate.Height = 23;
            this.dgv1.Size = new System.Drawing.Size(415, 796);
            this.dgv1.TabIndex = 0;
            // 
            // STOCK_CODE
            // 
            this.STOCK_CODE.HeaderText = "종목코드";
            this.STOCK_CODE.Name = "STOCK_CODE";
            // 
            // STOCK_NAME
            // 
            this.STOCK_NAME.HeaderText = "STOCK_NAME";
            this.STOCK_NAME.Name = "STOCK_NAME";
            // 
            // BtnStartSync
            // 
            this.BtnStartSync.Location = new System.Drawing.Point(3, 3);
            this.BtnStartSync.Name = "BtnStartSync";
            this.BtnStartSync.Size = new System.Drawing.Size(95, 25);
            this.BtnStartSync.TabIndex = 1;
            this.BtnStartSync.Text = "신규종목입력";
            this.BtnStartSync.UseVisualStyleBackColor = true;
            this.BtnStartSync.Click += new System.EventHandler(this.BtnStartSync_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.BtnStartSync);
            this.splitContainer2.Panel1MinSize = 20;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer2.Size = new System.Drawing.Size(794, 834);
            this.splitContainer2.SplitterDistance = 34;
            this.splitContainer2.TabIndex = 2;
            // 
            // FrmGetStockCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 834);
            this.Controls.Add(this.splitContainer2);
            this.Name = "FrmGetStockCode";
            this.Text = "종목관리(FrmGetStockCode)";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgv0;
        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.DataGridViewTextBoxColumn STOCK_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn STOCK_NAME;
        private System.Windows.Forms.Button BtnStartSync;
        private System.Windows.Forms.SplitContainer splitContainer2;
    }
}