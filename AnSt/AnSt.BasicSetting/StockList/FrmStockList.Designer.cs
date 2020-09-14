namespace AnSt.BasicSetting.StockList
{
    partial class FrmStockList
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
            this.ucStockList1 = new AnSt.BasicSetting.StockList.UcStockList();
            this.SuspendLayout();
            // 
            // ucStockList1
            // 
            this.ucStockList1.AutoSize = true;
            this.ucStockList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucStockList1.Font = new System.Drawing.Font("돋움", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ucStockList1.Location = new System.Drawing.Point(0, 0);
            this.ucStockList1.Name = "ucStockList1";
            this.ucStockList1.SGroupCode = "";
            this.ucStockList1.Size = new System.Drawing.Size(238, 680);
            this.ucStockList1.TabIndex = 0;
            // 
            // FrmStockList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 11F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 680);
            this.Controls.Add(this.ucStockList1);
            this.Font = new System.Drawing.Font("돋움", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "FrmStockList";
            this.Text = "FrmStockList";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UcStockList ucStockList1;
    }
}