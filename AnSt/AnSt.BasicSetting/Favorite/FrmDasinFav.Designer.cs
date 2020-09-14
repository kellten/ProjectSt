namespace AnSt.BasicSetting.Favorite
{
    partial class FrmDasinFav
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
            this.ucDasinFav1 = new AnSt.BasicSetting.Favorite.UcDasinFav();
            this.SuspendLayout();
            // 
            // ucDasinFav1
            // 
            this.ucDasinFav1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ucDasinFav1.Location = new System.Drawing.Point(12, 12);
            this.ucDasinFav1.Name = "ucDasinFav1";
            this.ucDasinFav1.Size = new System.Drawing.Size(424, 628);
            this.ucDasinFav1.TabIndex = 0;
            // 
            // FrmDasinFav
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 636);
            this.Controls.Add(this.ucDasinFav1);
            this.Name = "FrmDasinFav";
            this.Text = "FrmDasinFav";
            this.ResumeLayout(false);

        }

        #endregion

        private UcDasinFav ucDasinFav1;
    }
}