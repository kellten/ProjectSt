﻿namespace AnSt.BasicSetting.Favorite
{
    partial class FrmFavList
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
            this.ucFav1 = new AnSt.BasicSetting.Favorite.UcFav();
            this.SuspendLayout();
            // 
            // ucFav1
            // 
            this.ucFav1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucFav1.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ucFav1.Location = new System.Drawing.Point(0, 0);
            this.ucFav1.Name = "ucFav1";
            this.ucFav1.Size = new System.Drawing.Size(722, 540);
            this.ucFav1.TabIndex = 0;
            // 
            // FrmFavList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 544);
            this.Controls.Add(this.ucFav1);
            this.Name = "FrmFavList";
            this.Text = "FrmFavList";
            this.ResumeLayout(false);

        }

        #endregion

        private UcFav ucFav1;
    }
}