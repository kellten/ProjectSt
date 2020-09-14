namespace SDataProcessing.Batch.Forms
{
    partial class FrmVolume10060Collection
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
            this.proBar10060 = new System.Windows.Forms.ProgressBar();
            this.btn10060All = new System.Windows.Forms.Button();
            this.lblStockName = new System.Windows.Forms.Label();
            this.chkLastDay = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStdDate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // proBar10060
            // 
            this.proBar10060.Location = new System.Drawing.Point(12, 7);
            this.proBar10060.Name = "proBar10060";
            this.proBar10060.Size = new System.Drawing.Size(556, 21);
            this.proBar10060.TabIndex = 3;
            this.proBar10060.Click += new System.EventHandler(this.proBar10060_Click);
            // 
            // btn10060All
            // 
            this.btn10060All.Location = new System.Drawing.Point(574, 5);
            this.btn10060All.Name = "btn10060All";
            this.btn10060All.Size = new System.Drawing.Size(136, 23);
            this.btn10060All.TabIndex = 13;
            this.btn10060All.Text = "시작(10060All)";
            this.btn10060All.UseVisualStyleBackColor = true;
            this.btn10060All.Click += new System.EventHandler(this.btn10060All_Click);
            // 
            // lblStockName
            // 
            this.lblStockName.AutoSize = true;
            this.lblStockName.Location = new System.Drawing.Point(242, 40);
            this.lblStockName.Name = "lblStockName";
            this.lblStockName.Size = new System.Drawing.Size(54, 12);
            this.lblStockName.TabIndex = 14;
            this.lblStockName.Text = "Opt10060";
            this.lblStockName.Click += new System.EventHandler(this.lblStockName_Click);
            // 
            // chkLastDay
            // 
            this.chkLastDay.AutoSize = true;
            this.chkLastDay.Location = new System.Drawing.Point(19, 39);
            this.chkLastDay.Name = "chkLastDay";
            this.chkLastDay.Size = new System.Drawing.Size(60, 16);
            this.chkLastDay.TabIndex = 15;
            this.chkLastDay.Text = "최근일";
            this.chkLastDay.UseVisualStyleBackColor = true;
            this.chkLastDay.CheckedChanged += new System.EventHandler(this.chkLastDay_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "기준일";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblStdDate
            // 
            this.lblStdDate.AutoSize = true;
            this.lblStdDate.Location = new System.Drawing.Point(148, 39);
            this.lblStdDate.Name = "lblStdDate";
            this.lblStdDate.Size = new System.Drawing.Size(38, 12);
            this.lblStdDate.TabIndex = 17;
            this.lblStdDate.Text = "label2";
            this.lblStdDate.Click += new System.EventHandler(this.lblStdDate_Click);
            // 
            // FrmVolume10060Collection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 74);
            this.Controls.Add(this.lblStdDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkLastDay);
            this.Controls.Add(this.lblStockName);
            this.Controls.Add(this.btn10060All);
            this.Controls.Add(this.proBar10060);
            this.Name = "FrmVolume10060Collection";
            this.Text = "FrmVolume10060Collection";
            this.Load += new System.EventHandler(this.FrmVolume10060Collection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar proBar10060;
        private System.Windows.Forms.Button btn10060All;
        private System.Windows.Forms.Label lblStockName;
        private System.Windows.Forms.CheckBox chkLastDay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStdDate;
    }
}