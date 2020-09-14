namespace SDataProcessing.Batch.Forms
{
    partial class FrmVolumeCollection
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.proBar10059Qty = new System.Windows.Forms.ProgressBar();
            this.proBar10059Price = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.proBar10081 = new System.Windows.Forms.ProgressBar();
            this.btn10081Start = new System.Windows.Forms.Button();
            this.lblStockName = new System.Windows.Forms.Label();
            this.lblStockName2 = new System.Windows.Forms.Label();
            this.lblStockName3 = new System.Windows.Forms.Label();
            this.rcReport = new System.Windows.Forms.RichTextBox();
            this.btnStart10059Price = new System.Windows.Forms.Button();
            this.btn10059QtyStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Opt10059Qty";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Opt10059Price=CpSvr7254";
            // 
            // proBar10059Qty
            // 
            this.proBar10059Qty.Location = new System.Drawing.Point(169, 9);
            this.proBar10059Qty.Name = "proBar10059Qty";
            this.proBar10059Qty.Size = new System.Drawing.Size(556, 21);
            this.proBar10059Qty.TabIndex = 2;
            // 
            // proBar10059Price
            // 
            this.proBar10059Price.Location = new System.Drawing.Point(169, 37);
            this.proBar10059Price.Name = "proBar10059Price";
            this.proBar10059Price.Size = new System.Drawing.Size(556, 21);
            this.proBar10059Price.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Opt10081";
            // 
            // proBar10081
            // 
            this.proBar10081.Location = new System.Drawing.Point(169, 64);
            this.proBar10081.Name = "proBar10081";
            this.proBar10081.Size = new System.Drawing.Size(556, 21);
            this.proBar10081.TabIndex = 5;
            // 
            // btn10081Start
            // 
            this.btn10081Start.Location = new System.Drawing.Point(470, 91);
            this.btn10081Start.Name = "btn10081Start";
            this.btn10081Start.Size = new System.Drawing.Size(136, 23);
            this.btn10081Start.TabIndex = 6;
            this.btn10081Start.Text = "시작(10081 & 10059Qty)";
            this.btn10081Start.UseVisualStyleBackColor = true;
            this.btn10081Start.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblStockName
            // 
            this.lblStockName.AutoSize = true;
            this.lblStockName.Location = new System.Drawing.Point(731, 9);
            this.lblStockName.Name = "lblStockName";
            this.lblStockName.Size = new System.Drawing.Size(73, 12);
            this.lblStockName.TabIndex = 7;
            this.lblStockName.Text = "Opt10059Qty";
            // 
            // lblStockName2
            // 
            this.lblStockName2.AutoSize = true;
            this.lblStockName2.Location = new System.Drawing.Point(731, 42);
            this.lblStockName2.Name = "lblStockName2";
            this.lblStockName2.Size = new System.Drawing.Size(73, 12);
            this.lblStockName2.TabIndex = 8;
            this.lblStockName2.Text = "Opt10059Qty";
            // 
            // lblStockName3
            // 
            this.lblStockName3.AutoSize = true;
            this.lblStockName3.Location = new System.Drawing.Point(731, 73);
            this.lblStockName3.Name = "lblStockName3";
            this.lblStockName3.Size = new System.Drawing.Size(73, 12);
            this.lblStockName3.TabIndex = 9;
            this.lblStockName3.Text = "Opt10059Qty";
            // 
            // rcReport
            // 
            this.rcReport.Location = new System.Drawing.Point(16, 123);
            this.rcReport.Name = "rcReport";
            this.rcReport.Size = new System.Drawing.Size(817, 217);
            this.rcReport.TabIndex = 10;
            this.rcReport.Text = "";
            // 
            // btnStart10059Price
            // 
            this.btnStart10059Price.Location = new System.Drawing.Point(328, 91);
            this.btnStart10059Price.Name = "btnStart10059Price";
            this.btnStart10059Price.Size = new System.Drawing.Size(136, 23);
            this.btnStart10059Price.TabIndex = 11;
            this.btnStart10059Price.Text = "시작(10059Price)";
            this.btnStart10059Price.UseVisualStyleBackColor = true;
            this.btnStart10059Price.Click += new System.EventHandler(this.btnStart10059Price_Click);
            // 
            // btn10059QtyStart
            // 
            this.btn10059QtyStart.Location = new System.Drawing.Point(612, 91);
            this.btn10059QtyStart.Name = "btn10059QtyStart";
            this.btn10059QtyStart.Size = new System.Drawing.Size(136, 23);
            this.btn10059QtyStart.TabIndex = 12;
            this.btn10059QtyStart.Text = "시작(10059Qty)";
            this.btn10059QtyStart.UseVisualStyleBackColor = true;
            this.btn10059QtyStart.Click += new System.EventHandler(this.btn10059QtyStart_Click);
            // 
            // FrmVolumeCollection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 352);
            this.Controls.Add(this.btn10059QtyStart);
            this.Controls.Add(this.btnStart10059Price);
            this.Controls.Add(this.rcReport);
            this.Controls.Add(this.lblStockName3);
            this.Controls.Add(this.lblStockName2);
            this.Controls.Add(this.lblStockName);
            this.Controls.Add(this.btn10081Start);
            this.Controls.Add(this.proBar10081);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.proBar10059Price);
            this.Controls.Add(this.proBar10059Qty);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FrmVolumeCollection";
            this.Text = "FrmVolumeCollection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar proBar10059Qty;
        private System.Windows.Forms.ProgressBar proBar10059Price;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar proBar10081;
        private System.Windows.Forms.Button btn10081Start;
        private System.Windows.Forms.Label lblStockName;
        private System.Windows.Forms.Label lblStockName2;
        private System.Windows.Forms.Label lblStockName3;
        private System.Windows.Forms.RichTextBox rcReport;
        private System.Windows.Forms.Button btnStart10059Price;
        private System.Windows.Forms.Button btn10059QtyStart;
    }
}