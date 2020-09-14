namespace AnalysisSt.Analysis.Forms
{
    partial class frmTodayVolume
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
            this.ucTodayVolume0 = new AnalysisSt.Analysis.VolumeAnalysis.Uc.ucTodayVolume();
            this.SuspendLayout();
            // 
            // ucTodayVolume0
            // 
            this.ucTodayVolume0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTodayVolume0.Location = new System.Drawing.Point(0, 0);
            this.ucTodayVolume0.Name = "ucTodayVolume0";
            this.ucTodayVolume0.SGroupCode = null;
            this.ucTodayVolume0.Size = new System.Drawing.Size(994, 530);
            this.ucTodayVolume0.TabIndex = 0;
            this.ucTodayVolume0.TradeDate = null;
            // 
            // frmTodayVolume
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 530);
            this.Controls.Add(this.ucTodayVolume0);
            this.Name = "frmTodayVolume";
            this.Text = "frmTodayVolume";
            this.ResumeLayout(false);

        }

        #endregion

        private VolumeAnalysis.Uc.ucTodayVolume ucTodayVolume0;
    }
}