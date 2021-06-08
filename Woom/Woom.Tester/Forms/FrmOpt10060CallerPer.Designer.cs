namespace Woom.Tester.Forms
{
    partial class FrmOpt10060CallerPer
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
            this.dtpStdDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.chkSpeedOn = new System.Windows.Forms.CheckBox();
            this.chk100 = new System.Windows.Forms.CheckBox();
            this.lblStdDate = new System.Windows.Forms.Label();
            this.lblStockName = new System.Windows.Forms.Label();
            this.btn10060PriceBuyJob = new System.Windows.Forms.Button();
            this.proBar10060PriceBuy = new System.Windows.Forms.ProgressBar();
            this.proBar10060PriceSell = new System.Windows.Forms.ProgressBar();
            this.proBar10060QtyBuy = new System.Windows.Forms.ProgressBar();
            this.proBar10060QtySell = new System.Windows.Forms.ProgressBar();
            this.Btn10060PriceSellJob = new System.Windows.Forms.Button();
            this.Btn10060QtyBuyJob = new System.Windows.Forms.Button();
            this.Btn10060QtySellJob = new System.Windows.Forms.Button();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblTimePerCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dtpStdDate
            // 
            this.dtpStdDate.Location = new System.Drawing.Point(84, 147);
            this.dtpStdDate.Name = "dtpStdDate";
            this.dtpStdDate.Size = new System.Drawing.Size(175, 21);
            this.dtpStdDate.TabIndex = 49;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 48;
            this.label1.Text = "기준일자 : ";
            // 
            // chkSpeedOn
            // 
            this.chkSpeedOn.AutoSize = true;
            this.chkSpeedOn.Location = new System.Drawing.Point(490, 127);
            this.chkSpeedOn.Name = "chkSpeedOn";
            this.chkSpeedOn.Size = new System.Drawing.Size(72, 16);
            this.chkSpeedOn.TabIndex = 47;
            this.chkSpeedOn.Text = "스피드온";
            this.chkSpeedOn.UseVisualStyleBackColor = true;
            // 
            // chk100
            // 
            this.chk100.AutoSize = true;
            this.chk100.Location = new System.Drawing.Point(581, 126);
            this.chk100.Name = "chk100";
            this.chk100.Size = new System.Drawing.Size(152, 16);
            this.chk100.TabIndex = 46;
            this.chk100.Text = "현재일 기준(100거래일)";
            this.chk100.UseVisualStyleBackColor = true;
            // 
            // lblStdDate
            // 
            this.lblStdDate.AutoSize = true;
            this.lblStdDate.Location = new System.Drawing.Point(275, 156);
            this.lblStdDate.Name = "lblStdDate";
            this.lblStdDate.Size = new System.Drawing.Size(38, 12);
            this.lblStdDate.TabIndex = 45;
            this.lblStdDate.Text = "label2";
            // 
            // lblStockName
            // 
            this.lblStockName.AutoSize = true;
            this.lblStockName.Location = new System.Drawing.Point(12, 128);
            this.lblStockName.Name = "lblStockName";
            this.lblStockName.Size = new System.Drawing.Size(54, 12);
            this.lblStockName.TabIndex = 44;
            this.lblStockName.Text = "Opt10060";
            // 
            // btn10060PriceBuyJob
            // 
            this.btn10060PriceBuyJob.Enabled = false;
            this.btn10060PriceBuyJob.Location = new System.Drawing.Point(690, 12);
            this.btn10060PriceBuyJob.Name = "btn10060PriceBuyJob";
            this.btn10060PriceBuyJob.Size = new System.Drawing.Size(169, 23);
            this.btn10060PriceBuyJob.TabIndex = 43;
            this.btn10060PriceBuyJob.Text = "시작(10060Price 매수 작업)";
            this.btn10060PriceBuyJob.UseVisualStyleBackColor = true;
            this.btn10060PriceBuyJob.Click += new System.EventHandler(this.btn10060PriceBuyJob_Click);
            // 
            // proBar10060PriceBuy
            // 
            this.proBar10060PriceBuy.Location = new System.Drawing.Point(16, 12);
            this.proBar10060PriceBuy.Name = "proBar10060PriceBuy";
            this.proBar10060PriceBuy.Size = new System.Drawing.Size(668, 21);
            this.proBar10060PriceBuy.TabIndex = 42;
            // 
            // proBar10060PriceSell
            // 
            this.proBar10060PriceSell.Location = new System.Drawing.Point(16, 39);
            this.proBar10060PriceSell.Name = "proBar10060PriceSell";
            this.proBar10060PriceSell.Size = new System.Drawing.Size(668, 21);
            this.proBar10060PriceSell.TabIndex = 50;
            // 
            // proBar10060QtyBuy
            // 
            this.proBar10060QtyBuy.Location = new System.Drawing.Point(16, 66);
            this.proBar10060QtyBuy.Name = "proBar10060QtyBuy";
            this.proBar10060QtyBuy.Size = new System.Drawing.Size(668, 21);
            this.proBar10060QtyBuy.TabIndex = 51;
            // 
            // proBar10060QtySell
            // 
            this.proBar10060QtySell.Location = new System.Drawing.Point(16, 93);
            this.proBar10060QtySell.Name = "proBar10060QtySell";
            this.proBar10060QtySell.Size = new System.Drawing.Size(668, 21);
            this.proBar10060QtySell.TabIndex = 52;
            // 
            // Btn10060PriceSellJob
            // 
            this.Btn10060PriceSellJob.Enabled = false;
            this.Btn10060PriceSellJob.Location = new System.Drawing.Point(690, 39);
            this.Btn10060PriceSellJob.Name = "Btn10060PriceSellJob";
            this.Btn10060PriceSellJob.Size = new System.Drawing.Size(169, 23);
            this.Btn10060PriceSellJob.TabIndex = 53;
            this.Btn10060PriceSellJob.Text = "시작(10060Price 매도 작업)";
            this.Btn10060PriceSellJob.UseVisualStyleBackColor = true;
            this.Btn10060PriceSellJob.Click += new System.EventHandler(this.Btn10060PriceSellJob_Click);
            // 
            // Btn10060QtyBuyJob
            // 
            this.Btn10060QtyBuyJob.Enabled = false;
            this.Btn10060QtyBuyJob.Location = new System.Drawing.Point(690, 68);
            this.Btn10060QtyBuyJob.Name = "Btn10060QtyBuyJob";
            this.Btn10060QtyBuyJob.Size = new System.Drawing.Size(169, 23);
            this.Btn10060QtyBuyJob.TabIndex = 54;
            this.Btn10060QtyBuyJob.Text = "시작(10060Qty 매수 작업)";
            this.Btn10060QtyBuyJob.UseVisualStyleBackColor = true;
            this.Btn10060QtyBuyJob.Click += new System.EventHandler(this.Btn10060QtyBuyJob_Click);
            // 
            // Btn10060QtySellJob
            // 
            this.Btn10060QtySellJob.Enabled = false;
            this.Btn10060QtySellJob.Location = new System.Drawing.Point(690, 97);
            this.Btn10060QtySellJob.Name = "Btn10060QtySellJob";
            this.Btn10060QtySellJob.Size = new System.Drawing.Size(169, 23);
            this.Btn10060QtySellJob.TabIndex = 55;
            this.Btn10060QtySellJob.Text = "시작(10060Qty 매도 작업)";
            this.Btn10060QtySellJob.UseVisualStyleBackColor = true;
            this.Btn10060QtySellJob.Click += new System.EventHandler(this.Btn10060QtySellJob_Click);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(275, 220);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(38, 12);
            this.lblTime.TabIndex = 57;
            this.lblTime.Text = "label2";
            // 
            // lblTimePerCount
            // 
            this.lblTimePerCount.AutoSize = true;
            this.lblTimePerCount.Location = new System.Drawing.Point(275, 189);
            this.lblTimePerCount.Name = "lblTimePerCount";
            this.lblTimePerCount.Size = new System.Drawing.Size(38, 12);
            this.lblTimePerCount.TabIndex = 56;
            this.lblTimePerCount.Text = "label2";
            // 
            // FrmOpt10060CallerPer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 274);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblTimePerCount);
            this.Controls.Add(this.Btn10060QtySellJob);
            this.Controls.Add(this.Btn10060QtyBuyJob);
            this.Controls.Add(this.Btn10060PriceSellJob);
            this.Controls.Add(this.proBar10060QtySell);
            this.Controls.Add(this.proBar10060QtyBuy);
            this.Controls.Add(this.proBar10060PriceSell);
            this.Controls.Add(this.dtpStdDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkSpeedOn);
            this.Controls.Add(this.chk100);
            this.Controls.Add(this.lblStdDate);
            this.Controls.Add(this.lblStockName);
            this.Controls.Add(this.btn10060PriceBuyJob);
            this.Controls.Add(this.proBar10060PriceBuy);
            this.Name = "FrmOpt10060CallerPer";
            this.Text = "FrmOpt10060PriceMaeSuCaller";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dtpStdDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkSpeedOn;
        private System.Windows.Forms.CheckBox chk100;
        private System.Windows.Forms.Label lblStdDate;
        private System.Windows.Forms.Label lblStockName;
        private System.Windows.Forms.Button btn10060PriceBuyJob;
        private System.Windows.Forms.ProgressBar proBar10060PriceBuy;
        private System.Windows.Forms.ProgressBar proBar10060PriceSell;
        private System.Windows.Forms.ProgressBar proBar10060QtyBuy;
        private System.Windows.Forms.ProgressBar proBar10060QtySell;
        private System.Windows.Forms.Button Btn10060PriceSellJob;
        private System.Windows.Forms.Button Btn10060QtyBuyJob;
        private System.Windows.Forms.Button Btn10060QtySellJob;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblTimePerCount;
    }
}