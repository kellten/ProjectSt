namespace AnSt.Test
{
    partial class FrmWaveInfoTester
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
            this.ucWaveInfo1 = new AnSt.BasicSetting.WaveInfo.UcWaveInfo();
            this.SuspendLayout();
            // 
            // ucWaveInfo1
            // 
            this.ucWaveInfo1.Font = new System.Drawing.Font("돋움", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ucWaveInfo1.Location = new System.Drawing.Point(21, 11);
            this.ucWaveInfo1.Name = "ucWaveInfo1";
            this.ucWaveInfo1.Size = new System.Drawing.Size(432, 407);
            this.ucWaveInfo1.TabIndex = 0;
            // 
            // FrmWaveInfoTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 11F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 488);
            this.Controls.Add(this.ucWaveInfo1);
            this.Font = new System.Drawing.Font("돋움", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "FrmWaveInfoTester";
            this.Text = "FrmWaveInfoTester";
            this.ResumeLayout(false);

        }

        #endregion

        private BasicSetting.WaveInfo.UcWaveInfo ucWaveInfo1;
    }
}