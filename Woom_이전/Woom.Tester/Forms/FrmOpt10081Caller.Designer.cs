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
            this.lblStockName.Size = new System.Drawing.Size(54, 12);
            this.lblStockName.TabIndex = 28;
            this.lblStockName.Text = "Opt10060";
            // 
            // FrmOpt10081Caller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 67);
            this.Controls.Add(this.lblStockName);
            this.Controls.Add(this.btn10081);
            this.Controls.Add(this.proBar10081);
            this.Name = "FrmOpt10081Caller";
            this.Text = "frmOpt10081";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn10081;
        private System.Windows.Forms.ProgressBar proBar10081;
        private System.Windows.Forms.Label lblStockName;
    }
}