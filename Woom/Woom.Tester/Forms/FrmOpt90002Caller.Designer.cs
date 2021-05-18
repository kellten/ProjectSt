
namespace Woom.Tester.Forms
{
    partial class FrmOpt90002Caller
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
            this.btn90002 = new System.Windows.Forms.Button();
            this.proBar90002 = new System.Windows.Forms.ProgressBar();
            this.lblStockName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn90002
            // 
            this.btn90002.Location = new System.Drawing.Point(574, 12);
            this.btn90002.Name = "btn90002";
            this.btn90002.Size = new System.Drawing.Size(114, 23);
            this.btn90002.TabIndex = 32;
            this.btn90002.Text = "시작(90002)";
            this.btn90002.UseVisualStyleBackColor = true;
            this.btn90002.Click += new System.EventHandler(this.btn90002_Click);
            // 
            // proBar90002
            // 
            this.proBar90002.Location = new System.Drawing.Point(12, 12);
            this.proBar90002.Name = "proBar90002";
            this.proBar90002.Size = new System.Drawing.Size(556, 21);
            this.proBar90002.TabIndex = 31;
            // 
            // lblStockName
            // 
            this.lblStockName.AutoSize = true;
            this.lblStockName.Location = new System.Drawing.Point(12, 38);
            this.lblStockName.Name = "lblStockName";
            this.lblStockName.Size = new System.Drawing.Size(54, 12);
            this.lblStockName.TabIndex = 33;
            this.lblStockName.Text = "Opt10060";
            // 
            // FrmOpt90002Caller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 55);
            this.Controls.Add(this.lblStockName);
            this.Controls.Add(this.btn90002);
            this.Controls.Add(this.proBar90002);
            this.Name = "FrmOpt90002Caller";
            this.Text = "테마그룹별요청 (FrmOpt90002Caller)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn90002;
        private System.Windows.Forms.ProgressBar proBar90002;
        private System.Windows.Forms.Label lblStockName;
    }
}