namespace Woom.Tester.Forms
{
    partial class FrmOpt10015Caller
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
            this.lblStockName = new System.Windows.Forms.Label();
            this.btn10015 = new System.Windows.Forms.Button();
            this.proBar10015 = new System.Windows.Forms.ProgressBar();
            this.chk100 = new System.Windows.Forms.CheckBox();
            this.chkSpeedOn = new System.Windows.Forms.CheckBox();
            this.dtpStdDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTimePerCount = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblStockName
            // 
            this.lblStockName.AutoSize = true;
            this.lblStockName.Location = new System.Drawing.Point(10, 46);
            this.lblStockName.Name = "lblStockName";
            this.lblStockName.Size = new System.Drawing.Size(54, 12);
            this.lblStockName.TabIndex = 31;
            this.lblStockName.Text = "Opt10060";
            // 
            // btn10015
            // 
            this.btn10015.Location = new System.Drawing.Point(574, 12);
            this.btn10015.Name = "btn10015";
            this.btn10015.Size = new System.Drawing.Size(114, 23);
            this.btn10015.TabIndex = 30;
            this.btn10015.Text = "시작(10015)";
            this.btn10015.UseVisualStyleBackColor = true;
            this.btn10015.Click += new System.EventHandler(this.btn10015_Click);
            // 
            // proBar10015
            // 
            this.proBar10015.Location = new System.Drawing.Point(12, 12);
            this.proBar10015.Name = "proBar10015";
            this.proBar10015.Size = new System.Drawing.Size(556, 21);
            this.proBar10015.TabIndex = 29;
            // 
            // chk100
            // 
            this.chk100.AutoSize = true;
            this.chk100.Location = new System.Drawing.Point(551, 39);
            this.chk100.Name = "chk100";
            this.chk100.Size = new System.Drawing.Size(152, 16);
            this.chk100.TabIndex = 32;
            this.chk100.Text = "현재일 기준(100거래일)";
            this.chk100.UseVisualStyleBackColor = true;
            // 
            // chkSpeedOn
            // 
            this.chkSpeedOn.AutoSize = true;
            this.chkSpeedOn.Location = new System.Drawing.Point(473, 39);
            this.chkSpeedOn.Name = "chkSpeedOn";
            this.chkSpeedOn.Size = new System.Drawing.Size(72, 16);
            this.chkSpeedOn.TabIndex = 33;
            this.chkSpeedOn.Text = "스피드온";
            this.chkSpeedOn.UseVisualStyleBackColor = true;
            this.chkSpeedOn.CheckedChanged += new System.EventHandler(this.chkSpeedOn_CheckedChanged);
            // 
            // dtpStdDate
            // 
            this.dtpStdDate.Location = new System.Drawing.Point(89, 68);
            this.dtpStdDate.Name = "dtpStdDate";
            this.dtpStdDate.Size = new System.Drawing.Size(175, 21);
            this.dtpStdDate.TabIndex = 36;
            this.dtpStdDate.ValueChanged += new System.EventHandler(this.dtpStdDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 35;
            this.label1.Text = "기준일자 : ";
            // 
            // lblTimePerCount
            // 
            this.lblTimePerCount.AutoSize = true;
            this.lblTimePerCount.Location = new System.Drawing.Point(311, 77);
            this.lblTimePerCount.Name = "lblTimePerCount";
            this.lblTimePerCount.Size = new System.Drawing.Size(38, 12);
            this.lblTimePerCount.TabIndex = 37;
            this.lblTimePerCount.Text = "label2";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(311, 108);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(38, 12);
            this.lblTime.TabIndex = 38;
            this.lblTime.Text = "label2";
            // 
            // FrmOpt10015Caller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 129);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblTimePerCount);
            this.Controls.Add(this.dtpStdDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkSpeedOn);
            this.Controls.Add(this.chk100);
            this.Controls.Add(this.lblStockName);
            this.Controls.Add(this.btn10015);
            this.Controls.Add(this.proBar10015);
            this.Name = "FrmOpt10015Caller";
            this.Text = "일별거래상세요청 (FrmOpt10015Caller)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStockName;
        private System.Windows.Forms.Button btn10015;
        private System.Windows.Forms.ProgressBar proBar10015;
        private System.Windows.Forms.CheckBox chk100;
        private System.Windows.Forms.CheckBox chkSpeedOn;
        private System.Windows.Forms.DateTimePicker dtpStdDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTimePerCount;
        private System.Windows.Forms.Label lblTime;
    }
}