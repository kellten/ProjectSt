namespace AnSt.Analysis.Forms
{
    partial class frmAnStockInfo
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnStockInfo = new System.Windows.Forms.Panel();
            this.pnStockDetailInfo = new System.Windows.Forms.Panel();
            this.TbCon = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ucStockInfo1 = new AnSt.Analysis.Uc.UcStockInfo();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnStockDetailInfo.SuspendLayout();
            this.TbCon.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnStockInfo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnStockDetailInfo);
            this.splitContainer1.Size = new System.Drawing.Size(1059, 730);
            this.splitContainer1.SplitterDistance = 43;
            this.splitContainer1.TabIndex = 0;
            // 
            // pnStockInfo
            // 
            this.pnStockInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnStockInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnStockInfo.Location = new System.Drawing.Point(0, 0);
            this.pnStockInfo.Name = "pnStockInfo";
            this.pnStockInfo.Size = new System.Drawing.Size(1055, 39);
            this.pnStockInfo.TabIndex = 0;
            // 
            // pnStockDetailInfo
            // 
            this.pnStockDetailInfo.AutoScroll = true;
            this.pnStockDetailInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnStockDetailInfo.Controls.Add(this.TbCon);
            this.pnStockDetailInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnStockDetailInfo.Location = new System.Drawing.Point(0, 0);
            this.pnStockDetailInfo.Name = "pnStockDetailInfo";
            this.pnStockDetailInfo.Size = new System.Drawing.Size(1055, 674);
            this.pnStockDetailInfo.TabIndex = 0;
            // 
            // TbCon
            // 
            this.TbCon.Controls.Add(this.tabPage1);
            this.TbCon.Controls.Add(this.tabPage2);
            this.TbCon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TbCon.Location = new System.Drawing.Point(0, 0);
            this.TbCon.Name = "TbCon";
            this.TbCon.SelectedIndex = 0;
            this.TbCon.Size = new System.Drawing.Size(1051, 670);
            this.TbCon.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ucStockInfo1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1043, 645);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "기본정보";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1043, 645);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "볼륨";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ucStockInfo1
            // 
            this.ucStockInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucStockInfo1.Font = new System.Drawing.Font("굴림체", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ucStockInfo1.Location = new System.Drawing.Point(3, 3);
            this.ucStockInfo1.Name = "ucStockInfo1";
            this.ucStockInfo1.Size = new System.Drawing.Size(1037, 639);
            this.ucStockInfo1.TabIndex = 0;
            // 
            // frmAnStockInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 11F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1059, 730);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "frmAnStockInfo";
            this.Text = "frmAnStockInfo";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnStockDetailInfo.ResumeLayout(false);
            this.TbCon.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnStockInfo;
        private System.Windows.Forms.Panel pnStockDetailInfo;
        private System.Windows.Forms.TabControl TbCon;
        private System.Windows.Forms.TabPage tabPage1;
        private Uc.UcStockInfo ucStockInfo1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}