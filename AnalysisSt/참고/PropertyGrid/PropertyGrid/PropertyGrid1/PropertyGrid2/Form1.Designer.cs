namespace PropertyGrid2
{
    partial class Form1
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
            this.btnAssign = new System.Windows.Forms.Button();
            this.prpG = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // btnAssign
            // 
            this.btnAssign.Location = new System.Drawing.Point(12, 12);
            this.btnAssign.Name = "btnAssign";
            this.btnAssign.Size = new System.Drawing.Size(113, 23);
            this.btnAssign.TabIndex = 0;
            this.btnAssign.Text = "Assign Class";
            this.btnAssign.UseVisualStyleBackColor = true;
            this.btnAssign.Click += new System.EventHandler(this.button1_Click);
            // 
            // prpG
            // 
            this.prpG.Location = new System.Drawing.Point(12, 50);
            this.prpG.Name = "prpG";
            this.prpG.Size = new System.Drawing.Size(338, 193);
            this.prpG.TabIndex = 1;
            this.prpG.Click += new System.EventHandler(this.prpG_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 255);
            this.Controls.Add(this.prpG);
            this.Controls.Add(this.btnAssign);
            this.Name = "Form1";
            this.Text = "PropertyGrid demo";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAssign;
        private System.Windows.Forms.PropertyGrid prpG;
    }
}

