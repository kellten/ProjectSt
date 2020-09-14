namespace AnalysisSt.Analysis.Forms
{
    partial class frmAnalysisA
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
            this.ucAnalysisA0 = new AnalysisSt.Analysis.Uc.ucAnalysisA();
            this.SuspendLayout();
            // 
            // ucAnalysisA0
            // 
            this.ucAnalysisA0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAnalysisA0.FromDate = null;
            this.ucAnalysisA0.Location = new System.Drawing.Point(0, 0);
            this.ucAnalysisA0.Name = "ucAnalysisA0";
            this.ucAnalysisA0.Size = new System.Drawing.Size(729, 540);
            this.ucAnalysisA0.StockCode = null;
            this.ucAnalysisA0.TabIndex = 0;
            this.ucAnalysisA0.ToDate = null;
            // 
            // frmAnalysisA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(729, 540);
            this.Controls.Add(this.ucAnalysisA0);
            this.Name = "frmAnalysisA";
            this.Text = "frmAnalysisA";
            this.ResumeLayout(false);

        }

        #endregion

        private Uc.ucAnalysisA ucAnalysisA0;
    }
}