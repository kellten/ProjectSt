namespace StudyProject.DesignPattern.Forms
{
    partial class FrmFactoryMethod1
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
            this.rcText = new System.Windows.Forms.RichTextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rcText
            // 
            this.rcText.Location = new System.Drawing.Point(1, 39);
            this.rcText.Name = "rcText";
            this.rcText.Size = new System.Drawing.Size(295, 263);
            this.rcText.TabIndex = 3;
            this.rcText.Text = "";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(1, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(115, 34);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // FrmFactoryMethod1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 304);
            this.Controls.Add(this.rcText);
            this.Controls.Add(this.btnStart);
            this.Name = "FrmFactoryMethod1";
            this.Text = "팩토리메소드(FrmFactoryMethod1)";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rcText;
        private System.Windows.Forms.Button btnStart;
    }
}