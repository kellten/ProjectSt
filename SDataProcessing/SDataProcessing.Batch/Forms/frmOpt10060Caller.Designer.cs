namespace SDataProcessing.Batch.Forms
{
    partial class frmOpt10060Caller
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
            this.lblStdDate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkLastDay = new System.Windows.Forms.CheckBox();
            this.lblStockName = new System.Windows.Forms.Label();
            this.btn10060All = new System.Windows.Forms.Button();
            this.proBar10060 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lblStdDate
            // 
            this.lblStdDate.AutoSize = true;
            this.lblStdDate.Location = new System.Drawing.Point(148, 44);
            this.lblStdDate.Name = "lblStdDate";
            this.lblStdDate.Size = new System.Drawing.Size(38, 12);
            this.lblStdDate.TabIndex = 23;
            this.lblStdDate.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "기준일";
            // 
            // chkLastDay
            // 
            this.chkLastDay.AutoSize = true;
            this.chkLastDay.Location = new System.Drawing.Point(19, 44);
            this.chkLastDay.Name = "chkLastDay";
            this.chkLastDay.Size = new System.Drawing.Size(60, 16);
            this.chkLastDay.TabIndex = 21;
            this.chkLastDay.Text = "최근일";
            this.chkLastDay.UseVisualStyleBackColor = true;
            // 
            // lblStockName
            // 
            this.lblStockName.AutoSize = true;
            this.lblStockName.Location = new System.Drawing.Point(242, 45);
            this.lblStockName.Name = "lblStockName";
            this.lblStockName.Size = new System.Drawing.Size(54, 12);
            this.lblStockName.TabIndex = 20;
            this.lblStockName.Text = "Opt10060";
            // 
            // btn10060All
            // 
            this.btn10060All.Location = new System.Drawing.Point(574, 10);
            this.btn10060All.Name = "btn10060All";
            this.btn10060All.Size = new System.Drawing.Size(136, 23);
            this.btn10060All.TabIndex = 19;
            this.btn10060All.Text = "시작(10060All)";
            this.btn10060All.UseVisualStyleBackColor = true;
            this.btn10060All.Click += new System.EventHandler(this.btn10060All_Click);
            // 
            // proBar10060
            // 
            this.proBar10060.Location = new System.Drawing.Point(12, 12);
            this.proBar10060.Name = "proBar10060";
            this.proBar10060.Size = new System.Drawing.Size(556, 21);
            this.proBar10060.TabIndex = 18;
            // 
            // frmOpt10060Caller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 560);
            this.Controls.Add(this.lblStdDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkLastDay);
            this.Controls.Add(this.lblStockName);
            this.Controls.Add(this.btn10060All);
            this.Controls.Add(this.proBar10060);
            this.Name = "frmOpt10060Caller";
            this.Text = "frmOpt10060Caller";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStdDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkLastDay;
        private System.Windows.Forms.Label lblStockName;
        private System.Windows.Forms.Button btn10060All;
        private System.Windows.Forms.ProgressBar proBar10060;
    }
}