namespace ZabisProgram.GOF.싱글톤
{
    partial class FrmSingleton1
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
            this.btnSingleton = new System.Windows.Forms.Button();
            this.rcText = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnSingleton
            // 
            this.btnSingleton.Location = new System.Drawing.Point(2, 1);
            this.btnSingleton.Name = "btnSingleton";
            this.btnSingleton.Size = new System.Drawing.Size(75, 23);
            this.btnSingleton.TabIndex = 0;
            this.btnSingleton.Text = "가져오기";
            this.btnSingleton.UseVisualStyleBackColor = true;
            this.btnSingleton.Click += new System.EventHandler(this.btnSingleton_Click);
            // 
            // rcText
            // 
            this.rcText.Location = new System.Drawing.Point(2, 30);
            this.rcText.Name = "rcText";
            this.rcText.Size = new System.Drawing.Size(316, 220);
            this.rcText.TabIndex = 1;
            this.rcText.Text = "";
            // 
            // FrmSingleton1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 256);
            this.Controls.Add(this.rcText);
            this.Controls.Add(this.btnSingleton);
            this.Name = "FrmSingleton1";
            this.Text = "싱글톤(FrmSingleton1)";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSingleton;
        private System.Windows.Forms.RichTextBox rcText;
    }
}