﻿
namespace Woom.Tester.Forms
{
    partial class FrmOpt10005Caller
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
            this.btn10005 = new System.Windows.Forms.Button();
            this.proBar10005 = new System.Windows.Forms.ProgressBar();
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
            // btn10005
            // 
            this.btn10005.Location = new System.Drawing.Point(574, 12);
            this.btn10005.Name = "btn10005";
            this.btn10005.Size = new System.Drawing.Size(114, 23);
            this.btn10005.TabIndex = 30;
            this.btn10005.Text = "시작(10005)";
            this.btn10005.UseVisualStyleBackColor = true;
            this.btn10005.Click += new System.EventHandler(this.btn10005_Click_1);
            // 
            // proBar10005
            // 
            this.proBar10005.Location = new System.Drawing.Point(12, 12);
            this.proBar10005.Name = "proBar10005";
            this.proBar10005.Size = new System.Drawing.Size(556, 21);
            this.proBar10005.TabIndex = 29;
            // 
            // FrmOpt10005Caller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 68);
            this.Controls.Add(this.lblStockName);
            this.Controls.Add(this.btn10005);
            this.Controls.Add(this.proBar10005);
            this.Name = "FrmOpt10005Caller";
            this.Text = "FrmOpt10050Caller";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStockName;
        private System.Windows.Forms.Button btn10005;
        private System.Windows.Forms.ProgressBar proBar10005;
    }
}