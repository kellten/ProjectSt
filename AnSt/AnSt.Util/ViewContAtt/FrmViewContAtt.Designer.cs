namespace AnSt.Util.ViewContAtt
{
    partial class FrmViewContAtt
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
            this.ucViewContAttribute1 = new AnSt.Util.ViewContAtt.UcViewContAttribute();
            this.SuspendLayout();
            // 
            // ucViewContAttribute1
            // 
            this.ucViewContAttribute1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucViewContAttribute1.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ucViewContAttribute1.Location = new System.Drawing.Point(0, 0);
            this.ucViewContAttribute1.Name = "ucViewContAttribute1";
            this.ucViewContAttribute1.Size = new System.Drawing.Size(301, 648);
            this.ucViewContAttribute1.TabIndex = 0;
            // 
            // FrmViewContAtt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 648);
            this.Controls.Add(this.ucViewContAttribute1);
            this.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "FrmViewContAtt";
            this.Text = "FrmViewContAtt";
            this.ResumeLayout(false);

        }

        #endregion

        private UcViewContAttribute ucViewContAttribute1;
    }
}