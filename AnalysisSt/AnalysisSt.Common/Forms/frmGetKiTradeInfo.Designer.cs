namespace AnalysisSt.Common.Forms
{
    partial class frmGetKiTradeInfo
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
            this.lblStockCode = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblStockName
            // 
            this.lblStockName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStockName.Location = new System.Drawing.Point(74, 9);
            this.lblStockName.Name = "lblStockName";
            this.lblStockName.Size = new System.Drawing.Size(283, 19);
            this.lblStockName.TabIndex = 6;
            this.lblStockName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStockCode
            // 
            this.lblStockCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStockCode.Location = new System.Drawing.Point(12, 9);
            this.lblStockCode.Name = "lblStockCode";
            this.lblStockCode.Size = new System.Drawing.Size(61, 19);
            this.lblStockCode.TabIndex = 5;
            this.lblStockCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(363, 9);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "작업";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(12, 29);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(38, 12);
            this.lblMsg.TabIndex = 8;
            this.lblMsg.Text = "label1";
            // 
            // frmGetKiTradeInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 50);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblStockName);
            this.Controls.Add(this.lblStockCode);
            this.Name = "frmGetKiTradeInfo";
            this.Text = "frmGetKiTradeInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblStockName;
        internal System.Windows.Forms.Label lblStockCode;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblMsg;
    }
}