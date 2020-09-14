namespace AnSt.Chart.Forms
{
    partial class FrmAnStChart
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
            this.ucPriceChart1 = new AnSt.Chart.Uc.UcPriceChart();
            this.splitConA = new System.Windows.Forms.SplitContainer();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblStockName = new System.Windows.Forms.Label();
            this.lblStockCode = new System.Windows.Forms.Label();
            this.btnReflesh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitConA)).BeginInit();
            this.splitConA.Panel1.SuspendLayout();
            this.splitConA.Panel2.SuspendLayout();
            this.splitConA.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucPriceChart1
            // 
            this.ucPriceChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPriceChart1.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ucPriceChart1.Location = new System.Drawing.Point(0, 0);
            this.ucPriceChart1.Name = "ucPriceChart1";
            this.ucPriceChart1.Size = new System.Drawing.Size(972, 683);
            this.ucPriceChart1.TabIndex = 0;
            // 
            // splitConA
            // 
            this.splitConA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitConA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitConA.Location = new System.Drawing.Point(0, 0);
            this.splitConA.Name = "splitConA";
            this.splitConA.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitConA.Panel1
            // 
            this.splitConA.Panel1.Controls.Add(this.btnReflesh);
            this.splitConA.Panel1.Controls.Add(this.dtpToDate);
            this.splitConA.Panel1.Controls.Add(this.dtpFromDate);
            this.splitConA.Panel1.Controls.Add(this.lblStockName);
            this.splitConA.Panel1.Controls.Add(this.lblStockCode);
            this.splitConA.Panel1MinSize = 30;
            // 
            // splitConA.Panel2
            // 
            this.splitConA.Panel2.Controls.Add(this.ucPriceChart1);
            this.splitConA.Size = new System.Drawing.Size(976, 725);
            this.splitConA.SplitterDistance = 30;
            this.splitConA.SplitterWidth = 8;
            this.splitConA.TabIndex = 1;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(288, 2);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(102, 22);
            this.dtpToDate.TabIndex = 16;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(180, 2);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(102, 22);
            this.dtpFromDate.TabIndex = 15;
            // 
            // lblStockName
            // 
            this.lblStockName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStockName.Location = new System.Drawing.Point(55, 2);
            this.lblStockName.Name = "lblStockName";
            this.lblStockName.Size = new System.Drawing.Size(123, 22);
            this.lblStockName.TabIndex = 8;
            this.lblStockName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStockCode
            // 
            this.lblStockCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStockCode.Location = new System.Drawing.Point(3, 2);
            this.lblStockCode.Name = "lblStockCode";
            this.lblStockCode.Size = new System.Drawing.Size(52, 22);
            this.lblStockCode.TabIndex = 7;
            this.lblStockCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnReflesh
            // 
            this.btnReflesh.Location = new System.Drawing.Point(396, 1);
            this.btnReflesh.Name = "btnReflesh";
            this.btnReflesh.Size = new System.Drawing.Size(75, 23);
            this.btnReflesh.TabIndex = 17;
            this.btnReflesh.Text = "조회";
            this.btnReflesh.UseVisualStyleBackColor = true;
            this.btnReflesh.Click += new System.EventHandler(this.btnReflesh_Click);
            // 
            // FrmAnStChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 725);
            this.Controls.Add(this.splitConA);
            this.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.KeyPreview = true;
            this.Name = "FrmAnStChart";
            this.Text = "FrmAnStChart";
            this.splitConA.Panel1.ResumeLayout(false);
            this.splitConA.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitConA)).EndInit();
            this.splitConA.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Uc.UcPriceChart ucPriceChart1;
        private System.Windows.Forms.SplitContainer splitConA;
        internal System.Windows.Forms.Label lblStockName;
        internal System.Windows.Forms.Label lblStockCode;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Button btnReflesh;
    }
}