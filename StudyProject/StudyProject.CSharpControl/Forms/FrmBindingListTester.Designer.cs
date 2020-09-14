namespace StudyProject.CSharpControl.Forms
{
    partial class FrmBindingListTester
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
            this.DgvCar = new System.Windows.Forms.DataGridView();
            this.btnCall = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnAdd2 = new System.Windows.Forms.Button();
            this.btnCall2 = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DgvCar)).BeginInit();
            this.SuspendLayout();
            // 
            // DgvCar
            // 
            this.DgvCar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvCar.Location = new System.Drawing.Point(21, 22);
            this.DgvCar.Name = "DgvCar";
            this.DgvCar.RowTemplate.Height = 23;
            this.DgvCar.Size = new System.Drawing.Size(247, 393);
            this.DgvCar.TabIndex = 0;
            // 
            // btnCall
            // 
            this.btnCall.Location = new System.Drawing.Point(295, 22);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(75, 23);
            this.btnCall.TabIndex = 1;
            this.btnCall.Text = "Car  Call";
            this.btnCall.UseVisualStyleBackColor = true;
            this.btnCall.Click += new System.EventHandler(this.btnCall_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(295, 51);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Car  Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAdd2
            // 
            this.btnAdd2.Location = new System.Drawing.Point(295, 159);
            this.btnAdd2.Name = "btnAdd2";
            this.btnAdd2.Size = new System.Drawing.Size(75, 23);
            this.btnAdd2.TabIndex = 4;
            this.btnAdd2.Text = "Car  Add";
            this.btnAdd2.UseVisualStyleBackColor = true;
            this.btnAdd2.Click += new System.EventHandler(this.btnAdd2_Click);
            // 
            // btnCall2
            // 
            this.btnCall2.Location = new System.Drawing.Point(295, 114);
            this.btnCall2.Name = "btnCall2";
            this.btnCall2.Size = new System.Drawing.Size(75, 23);
            this.btnCall2.TabIndex = 3;
            this.btnCall2.Text = "Car  Call";
            this.btnCall2.UseVisualStyleBackColor = true;
            this.btnCall2.Click += new System.EventHandler(this.btnCall2_Click);
            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(295, 85);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(75, 23);
            this.btnSort.TabIndex = 5;
            this.btnSort.Text = "Car  Sort";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // FrmBindingListTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 447);
            this.Controls.Add(this.btnSort);
            this.Controls.Add(this.btnAdd2);
            this.Controls.Add(this.btnCall2);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnCall);
            this.Controls.Add(this.DgvCar);
            this.Name = "FrmBindingListTester";
            this.Text = "FrmBindingListTester";
            ((System.ComponentModel.ISupportInitialize)(this.DgvCar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DgvCar;
        private System.Windows.Forms.Button btnCall;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnAdd2;
        private System.Windows.Forms.Button btnCall2;
        private System.Windows.Forms.Button btnSort;
    }
}