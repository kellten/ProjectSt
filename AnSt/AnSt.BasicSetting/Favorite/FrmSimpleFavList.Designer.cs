namespace AnSt.BasicSetting.Favorite
{
    partial class FrmSimpleFavList
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
            this.ucSimpleFav1 = new AnSt.BasicSetting.Favorite.UcSimpleFav();
            this.SuspendLayout();
            // 
            // ucSimpleFav1
            // 
            this.ucSimpleFav1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSimpleFav1.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ucSimpleFav1.Location = new System.Drawing.Point(0, 0);
            this.ucSimpleFav1.Name = "ucSimpleFav1";
            this.ucSimpleFav1.Size = new System.Drawing.Size(327, 660);
            this.ucSimpleFav1.TabIndex = 0;
            // 
            // FrmSimpleFavList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 660);
            this.Controls.Add(this.ucSimpleFav1);
            this.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "FrmSimpleFavList";
            this.Text = "FrmSimpleFavList";
            this.ResumeLayout(false);

        }

        #endregion

        private UcSimpleFav ucSimpleFav1;
    }
}