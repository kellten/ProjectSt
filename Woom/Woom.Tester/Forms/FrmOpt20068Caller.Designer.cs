namespace Woom.Tester.Forms
{
    partial class FrmOpt20068Caller
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
            this.btn20068 = new System.Windows.Forms.Button();
            this.proBar20068 = new System.Windows.Forms.ProgressBar();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblStockName
            // 
            this.lblStockName.AutoSize = true;
            this.lblStockName.Location = new System.Drawing.Point(10, 77);
            this.lblStockName.Name = "lblStockName";
            this.lblStockName.Size = new System.Drawing.Size(54, 12);
            this.lblStockName.TabIndex = 31;
            this.lblStockName.Text = "Opt20068";
            // 
            // btn20068
            // 
            this.btn20068.Location = new System.Drawing.Point(574, 43);
            this.btn20068.Name = "btn20068";
            this.btn20068.Size = new System.Drawing.Size(114, 23);
            this.btn20068.TabIndex = 30;
            this.btn20068.Text = "시작(20068)";
            this.btn20068.UseVisualStyleBackColor = true;
            this.btn20068.Click += new System.EventHandler(this.btn20068_Click_1);
            // 
            // proBar20068
            // 
            this.proBar20068.Location = new System.Drawing.Point(12, 43);
            this.proBar20068.Name = "proBar20068";
            this.proBar20068.Size = new System.Drawing.Size(556, 21);
            this.proBar20068.TabIndex = 29;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(254, 12);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(200, 21);
            this.dtpEndDate.TabIndex = 32;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(12, 12);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 21);
            this.dtpStartDate.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(223, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 12);
            this.label1.TabIndex = 34;
            this.label1.Text = " ~ ";
            // 
            // FrmOpt20068Caller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 113);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.lblStockName);
            this.Controls.Add(this.btn20068);
            this.Controls.Add(this.proBar20068);
            this.Name = "FrmOpt20068Caller";
            this.Text = "FrmOpt20068Caller";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStockName;
        private System.Windows.Forms.Button btn20068;
        private System.Windows.Forms.ProgressBar proBar20068;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label1;
    }
}