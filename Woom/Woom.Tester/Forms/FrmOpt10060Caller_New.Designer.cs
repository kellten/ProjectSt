namespace Woom.Tester.Forms
{
    partial class FrmOpt10060Caller_New
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
            this.lblStockName = new System.Windows.Forms.Label();
            this.btn10060All = new System.Windows.Forms.Button();
            this.proBar10060 = new System.Windows.Forms.ProgressBar();
            this.chkSpeedOn = new System.Windows.Forms.CheckBox();
            this.chk100 = new System.Windows.Forms.CheckBox();
            this.dtpStdDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblStdDate
            // 
            this.lblStdDate.AutoSize = true;
            this.lblStdDate.Location = new System.Drawing.Point(273, 74);
            this.lblStdDate.Name = "lblStdDate";
            this.lblStdDate.Size = new System.Drawing.Size(38, 12);
            this.lblStdDate.TabIndex = 35;
            this.lblStdDate.Text = "label2";
            // 
            // lblStockName
            // 
            this.lblStockName.AutoSize = true;
            this.lblStockName.Location = new System.Drawing.Point(10, 46);
            this.lblStockName.Name = "lblStockName";
            this.lblStockName.Size = new System.Drawing.Size(54, 12);
            this.lblStockName.TabIndex = 32;
            this.lblStockName.Text = "Opt10060";
            // 
            // btn10060All
            // 
            this.btn10060All.Location = new System.Drawing.Point(574, 12);
            this.btn10060All.Name = "btn10060All";
            this.btn10060All.Size = new System.Drawing.Size(114, 23);
            this.btn10060All.TabIndex = 31;
            this.btn10060All.Text = "시작(10060All)";
            this.btn10060All.UseVisualStyleBackColor = true;
            this.btn10060All.Click += new System.EventHandler(this.btn10060All_Click);
            // 
            // proBar10060
            // 
            this.proBar10060.Location = new System.Drawing.Point(12, 12);
            this.proBar10060.Name = "proBar10060";
            this.proBar10060.Size = new System.Drawing.Size(556, 21);
            this.proBar10060.TabIndex = 30;
            // 
            // chkSpeedOn
            // 
            this.chkSpeedOn.AutoSize = true;
            this.chkSpeedOn.Location = new System.Drawing.Point(488, 45);
            this.chkSpeedOn.Name = "chkSpeedOn";
            this.chkSpeedOn.Size = new System.Drawing.Size(72, 16);
            this.chkSpeedOn.TabIndex = 37;
            this.chkSpeedOn.Text = "스피드온";
            this.chkSpeedOn.UseVisualStyleBackColor = true;
            // 
            // chk100
            // 
            this.chk100.AutoSize = true;
            this.chk100.Location = new System.Drawing.Point(579, 44);
            this.chk100.Name = "chk100";
            this.chk100.Size = new System.Drawing.Size(152, 16);
            this.chk100.TabIndex = 36;
            this.chk100.Text = "현재일 기준(100거래일)";
            this.chk100.UseVisualStyleBackColor = true;
            // 
            // dtpStdDate
            // 
            this.dtpStdDate.Location = new System.Drawing.Point(82, 65);
            this.dtpStdDate.Name = "dtpStdDate";
            this.dtpStdDate.Size = new System.Drawing.Size(175, 21);
            this.dtpStdDate.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 38;
            this.label1.Text = "기준일자 : ";
            // 
            // FrmOpt10060Caller_New
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 97);
            this.Controls.Add(this.dtpStdDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkSpeedOn);
            this.Controls.Add(this.chk100);
            this.Controls.Add(this.lblStdDate);
            this.Controls.Add(this.lblStockName);
            this.Controls.Add(this.btn10060All);
            this.Controls.Add(this.proBar10060);
            this.Name = "FrmOpt10060Caller_New";
            this.Text = "종목별 투자자기관별차트요청(FrmOpt10060Caller_New)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStdDate;
        private System.Windows.Forms.Label lblStockName;
        private System.Windows.Forms.Button btn10060All;
        private System.Windows.Forms.ProgressBar proBar10060;
        private System.Windows.Forms.CheckBox chkSpeedOn;
        private System.Windows.Forms.CheckBox chk100;
        private System.Windows.Forms.DateTimePicker dtpStdDate;
        private System.Windows.Forms.Label label1;
    }
}