
namespace Woom.Tester.Forms
{
    partial class FrmOpt10001Caller
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
            this.chkSpeedOn = new System.Windows.Forms.CheckBox();
            this.chk100 = new System.Windows.Forms.CheckBox();
            this.lblStdDate = new System.Windows.Forms.Label();
            this.lblStockName = new System.Windows.Forms.Label();
            this.btn10001 = new System.Windows.Forms.Button();
            this.proBar10001 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // chkSpeedOn
            // 
            this.chkSpeedOn.AutoSize = true;
            this.chkSpeedOn.Location = new System.Drawing.Point(484, 41);
            this.chkSpeedOn.Name = "chkSpeedOn";
            this.chkSpeedOn.Size = new System.Drawing.Size(72, 16);
            this.chkSpeedOn.TabIndex = 45;
            this.chkSpeedOn.Text = "스피드온";
            this.chkSpeedOn.UseVisualStyleBackColor = true;
            // 
            // chk100
            // 
            this.chk100.AutoSize = true;
            this.chk100.Location = new System.Drawing.Point(575, 40);
            this.chk100.Name = "chk100";
            this.chk100.Size = new System.Drawing.Size(152, 16);
            this.chk100.TabIndex = 44;
            this.chk100.Text = "현재일 기준(100거래일)";
            this.chk100.UseVisualStyleBackColor = true;
            // 
            // lblStdDate
            // 
            this.lblStdDate.AutoSize = true;
            this.lblStdDate.Location = new System.Drawing.Point(105, 42);
            this.lblStdDate.Name = "lblStdDate";
            this.lblStdDate.Size = new System.Drawing.Size(38, 12);
            this.lblStdDate.TabIndex = 43;
            this.lblStdDate.Text = "label2";
            // 
            // lblStockName
            // 
            this.lblStockName.AutoSize = true;
            this.lblStockName.Location = new System.Drawing.Point(6, 42);
            this.lblStockName.Name = "lblStockName";
            this.lblStockName.Size = new System.Drawing.Size(54, 12);
            this.lblStockName.TabIndex = 42;
            this.lblStockName.Text = "Opt10001";
            // 
            // btn10001
            // 
            this.btn10001.Location = new System.Drawing.Point(570, 8);
            this.btn10001.Name = "btn10001";
            this.btn10001.Size = new System.Drawing.Size(114, 23);
            this.btn10001.TabIndex = 41;
            this.btn10001.Text = "Opt10001 시작";
            this.btn10001.UseVisualStyleBackColor = true;
            // 
            // proBar10001
            // 
            this.proBar10001.Location = new System.Drawing.Point(8, 8);
            this.proBar10001.Name = "proBar10001";
            this.proBar10001.Size = new System.Drawing.Size(556, 21);
            this.proBar10001.TabIndex = 40;
            // 
            // FrmOpt10001Caller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 72);
            this.Controls.Add(this.chkSpeedOn);
            this.Controls.Add(this.chk100);
            this.Controls.Add(this.lblStdDate);
            this.Controls.Add(this.lblStockName);
            this.Controls.Add(this.btn10001);
            this.Controls.Add(this.proBar10001);
            this.Name = "FrmOpt10001Caller";
            this.Text = "FrmOpt10001";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkSpeedOn;
        private System.Windows.Forms.CheckBox chk100;
        private System.Windows.Forms.Label lblStdDate;
        private System.Windows.Forms.Label lblStockName;
        private System.Windows.Forms.Button btn10001;
        private System.Windows.Forms.ProgressBar proBar10001;
    }
}