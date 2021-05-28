namespace Woom.Tester.Forms
{
    partial class FrmOpt10081Caller
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
            this.btn10081 = new System.Windows.Forms.Button();
            this.proBar10081 = new System.Windows.Forms.ProgressBar();
            this.lblStockName = new System.Windows.Forms.Label();
            this.chk100 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.chkSpeedOn = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpStdDate = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // btn10081
            // 
            this.btn10081.Location = new System.Drawing.Point(564, 12);
            this.btn10081.Name = "btn10081";
            this.btn10081.Size = new System.Drawing.Size(114, 23);
            this.btn10081.TabIndex = 27;
            this.btn10081.Text = "시작(10081)";
            this.btn10081.UseVisualStyleBackColor = true;
            this.btn10081.Click += new System.EventHandler(this.btn10081_Click);
            // 
            // proBar10081
            // 
            this.proBar10081.Location = new System.Drawing.Point(2, 12);
            this.proBar10081.Name = "proBar10081";
            this.proBar10081.Size = new System.Drawing.Size(556, 21);
            this.proBar10081.TabIndex = 26;
            // 
            // lblStockName
            // 
            this.lblStockName.AutoSize = true;
            this.lblStockName.Location = new System.Drawing.Point(0, 46);
            this.lblStockName.Name = "lblStockName";
            this.lblStockName.Size = new System.Drawing.Size(81, 12);
            this.lblStockName.TabIndex = 28;
            this.lblStockName.Text = "종목 진행사항";
            // 
            // chk100
            // 
            this.chk100.AutoSize = true;
            this.chk100.Location = new System.Drawing.Point(530, 42);
            this.chk100.Name = "chk100";
            this.chk100.Size = new System.Drawing.Size(152, 16);
            this.chk100.TabIndex = 29;
            this.chk100.Text = "현재일 기준(100거래일)";
            this.chk100.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(399, 42);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(104, 16);
            this.checkBox1.TabIndex = 30;
            this.checkBox1.Text = "종목 순환 방식";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // chkSpeedOn
            // 
            this.chkSpeedOn.AutoSize = true;
            this.chkSpeedOn.Location = new System.Drawing.Point(294, 42);
            this.chkSpeedOn.Name = "chkSpeedOn";
            this.chkSpeedOn.Size = new System.Drawing.Size(72, 16);
            this.chkSpeedOn.TabIndex = 31;
            this.chkSpeedOn.Text = "스피드온";
            this.chkSpeedOn.UseVisualStyleBackColor = true;
            this.chkSpeedOn.CheckedChanged += new System.EventHandler(this.chkSpeedOn_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 32;
            this.label1.Text = "기준일자 : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 33;
            // 
            // dtpStdDate
            // 
            this.dtpStdDate.Location = new System.Drawing.Point(77, 72);
            this.dtpStdDate.Name = "dtpStdDate";
            this.dtpStdDate.Size = new System.Drawing.Size(175, 21);
            this.dtpStdDate.TabIndex = 34;
            this.dtpStdDate.ValueChanged += new System.EventHandler(this.dtpStdDate_ValueChanged);
            // 
            // FrmOpt10081Caller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 102);
            this.Controls.Add(this.dtpStdDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkSpeedOn);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.chk100);
            this.Controls.Add(this.lblStockName);
            this.Controls.Add(this.btn10081);
            this.Controls.Add(this.proBar10081);
            this.Name = "FrmOpt10081Caller";
            this.Text = "주식일봉차트조회(frmOpt10081)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn10081;
        private System.Windows.Forms.ProgressBar proBar10081;
        private System.Windows.Forms.Label lblStockName;
        private System.Windows.Forms.CheckBox chk100;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox chkSpeedOn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpStdDate;
    }
}