﻿namespace Woom.CallForm.Forms
{
    partial class FrmCallOptLastData
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
            this.BtnStart = new System.Windows.Forms.Button();
            this.proBar10081 = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.proBar10060 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.proBar10015 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lblStockName
            // 
            this.lblStockName.AutoSize = true;
            this.lblStockName.Location = new System.Drawing.Point(209, 87);
            this.lblStockName.Name = "lblStockName";
            this.lblStockName.Size = new System.Drawing.Size(54, 12);
            this.lblStockName.TabIndex = 45;
            this.lblStockName.Text = "Opt10060";
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(773, 3);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(69, 76);
            this.BtnStart.TabIndex = 44;
            this.BtnStart.Text = "시작";
            this.BtnStart.UseVisualStyleBackColor = true;
            // 
            // proBar10081
            // 
            this.proBar10081.Location = new System.Drawing.Point(211, 58);
            this.proBar10081.Name = "proBar10081";
            this.proBar10081.Size = new System.Drawing.Size(556, 21);
            this.proBar10081.TabIndex = 43;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 12);
            this.label3.TabIndex = 42;
            this.label3.Text = "주식일봉차트조회(Opt10081)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 12);
            this.label2.TabIndex = 41;
            this.label2.Text = "종목별투자자기관별요청(Opt10060)";
            // 
            // proBar10060
            // 
            this.proBar10060.Location = new System.Drawing.Point(211, 31);
            this.proBar10060.Name = "proBar10060";
            this.proBar10060.Size = new System.Drawing.Size(556, 21);
            this.proBar10060.TabIndex = 40;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 12);
            this.label1.TabIndex = 39;
            this.label1.Text = "일별거래상세요청(Opt10015)";
            // 
            // proBar10015
            // 
            this.proBar10015.Location = new System.Drawing.Point(211, 4);
            this.proBar10015.Name = "proBar10015";
            this.proBar10015.Size = new System.Drawing.Size(556, 21);
            this.proBar10015.TabIndex = 38;
            // 
            // FrmCallOptLastData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 106);
            this.Controls.Add(this.lblStockName);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.proBar10081);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.proBar10060);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.proBar10015);
            this.Name = "FrmCallOptLastData";
            this.Text = "최근 내역 가져오기(FrmCallOptLastData)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStockName;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.ProgressBar proBar10081;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar proBar10060;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar proBar10015;
    }
}