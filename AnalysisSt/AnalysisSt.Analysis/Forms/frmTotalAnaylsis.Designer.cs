namespace AnalysisSt.Analysis.Forms
{
    partial class frmTotalAnaylsis
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
            this.splitConA = new System.Windows.Forms.SplitContainer();
            this.pnA = new System.Windows.Forms.Panel();
            this.btnAllWave = new System.Windows.Forms.Button();
            this.btnAllAnalysisA = new System.Windows.Forms.Button();
            this.lblToDate = new System.Windows.Forms.Label();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.chkLeftView = new System.Windows.Forms.CheckBox();
            this.splitConB = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tbConA = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ucFav0 = new AnalysisSt.Common.Uc.ucFav();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ucStockList0 = new AnalysisSt.Common.Uc.ucStockList();
            this.ucWaveInfo0 = new AnalysisSt.Common.Uc.ucWaveInfo();
            this.tbConB = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.ucAnalysisA0 = new AnalysisSt.Analysis.Uc.ucAnalysisA();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnTodayVolumeView = new System.Windows.Forms.Button();
            this.lblSGroupCode = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitConA)).BeginInit();
            this.splitConA.Panel1.SuspendLayout();
            this.splitConA.Panel2.SuspendLayout();
            this.splitConA.SuspendLayout();
            this.pnA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitConB)).BeginInit();
            this.splitConB.Panel1.SuspendLayout();
            this.splitConB.Panel2.SuspendLayout();
            this.splitConB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tbConA.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tbConB.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
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
            this.splitConA.Panel1.Controls.Add(this.pnA);
            // 
            // splitConA.Panel2
            // 
            this.splitConA.Panel2.Controls.Add(this.splitConB);
            this.splitConA.Size = new System.Drawing.Size(1164, 845);
            this.splitConA.SplitterDistance = 48;
            this.splitConA.TabIndex = 0;
            // 
            // pnA
            // 
            this.pnA.Controls.Add(this.lblSGroupCode);
            this.pnA.Controls.Add(this.btnTodayVolumeView);
            this.pnA.Controls.Add(this.btnAllWave);
            this.pnA.Controls.Add(this.btnAllAnalysisA);
            this.pnA.Controls.Add(this.lblToDate);
            this.pnA.Controls.Add(this.lblFromDate);
            this.pnA.Controls.Add(this.chkLeftView);
            this.pnA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnA.Location = new System.Drawing.Point(0, 0);
            this.pnA.Name = "pnA";
            this.pnA.Size = new System.Drawing.Size(1160, 44);
            this.pnA.TabIndex = 0;
            // 
            // btnAllWave
            // 
            this.btnAllWave.Location = new System.Drawing.Point(503, 6);
            this.btnAllWave.Name = "btnAllWave";
            this.btnAllWave.Size = new System.Drawing.Size(131, 23);
            this.btnAllWave.TabIndex = 9;
            this.btnAllWave.Text = "Wave전체기간보기";
            this.btnAllWave.UseVisualStyleBackColor = true;
            this.btnAllWave.Click += new System.EventHandler(this.btnAllWave_Click);
            // 
            // btnAllAnalysisA
            // 
            this.btnAllAnalysisA.Location = new System.Drawing.Point(366, 7);
            this.btnAllAnalysisA.Name = "btnAllAnalysisA";
            this.btnAllAnalysisA.Size = new System.Drawing.Size(131, 23);
            this.btnAllAnalysisA.TabIndex = 8;
            this.btnAllAnalysisA.Text = "Wave정보모두보기";
            this.btnAllAnalysisA.UseVisualStyleBackColor = true;
            this.btnAllAnalysisA.Click += new System.EventHandler(this.btnAllAnalysisA_Click);
            // 
            // lblToDate
            // 
            this.lblToDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblToDate.Location = new System.Drawing.Point(164, 9);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(93, 19);
            this.lblToDate.TabIndex = 7;
            this.lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFromDate
            // 
            this.lblFromDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFromDate.Location = new System.Drawing.Point(70, 9);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(93, 19);
            this.lblFromDate.TabIndex = 6;
            this.lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkLeftView
            // 
            this.chkLeftView.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkLeftView.AutoSize = true;
            this.chkLeftView.Location = new System.Drawing.Point(7, 7);
            this.chkLeftView.Name = "chkLeftView";
            this.chkLeftView.Size = new System.Drawing.Size(57, 22);
            this.chkLeftView.TabIndex = 0;
            this.chkLeftView.Text = "LeView";
            this.chkLeftView.UseVisualStyleBackColor = true;
            this.chkLeftView.CheckedChanged += new System.EventHandler(this.chkLeftView_CheckedChanged);
            // 
            // splitConB
            // 
            this.splitConB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitConB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitConB.Location = new System.Drawing.Point(0, 0);
            this.splitConB.Name = "splitConB";
            // 
            // splitConB.Panel1
            // 
            this.splitConB.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitConB.Panel2
            // 
            this.splitConB.Panel2.Controls.Add(this.tbConB);
            this.splitConB.Size = new System.Drawing.Size(1164, 793);
            this.splitConB.SplitterDistance = 303;
            this.splitConB.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tbConA);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ucWaveInfo0);
            this.splitContainer1.Size = new System.Drawing.Size(299, 789);
            this.splitContainer1.SplitterDistance = 457;
            this.splitContainer1.TabIndex = 0;
            // 
            // tbConA
            // 
            this.tbConA.Controls.Add(this.tabPage1);
            this.tbConA.Controls.Add(this.tabPage2);
            this.tbConA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbConA.Location = new System.Drawing.Point(0, 0);
            this.tbConA.Name = "tbConA";
            this.tbConA.SelectedIndex = 0;
            this.tbConA.Size = new System.Drawing.Size(299, 457);
            this.tbConA.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ucFav0);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(291, 431);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "즐겨찾기";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ucFav0
            // 
            this.ucFav0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucFav0.Location = new System.Drawing.Point(3, 3);
            this.ucFav0.Name = "ucFav0";
            this.ucFav0.Size = new System.Drawing.Size(285, 425);
            this.ucFav0.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ucStockList0);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(291, 431);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "전체";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ucStockList0
            // 
            this.ucStockList0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucStockList0.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ucStockList0.Location = new System.Drawing.Point(3, 3);
            this.ucStockList0.Name = "ucStockList0";
            this.ucStockList0.Size = new System.Drawing.Size(285, 425);
            this.ucStockList0.TabIndex = 0;
            // 
            // ucWaveInfo0
            // 
            this.ucWaveInfo0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucWaveInfo0.FromDate = null;
            this.ucWaveInfo0.FromDate0 = null;
            this.ucWaveInfo0.FromDate1 = null;
            this.ucWaveInfo0.FromDate2 = null;
            this.ucWaveInfo0.FromDate3 = null;
            this.ucWaveInfo0.Last_FromDate = null;
            this.ucWaveInfo0.Last_ToDate = null;
            this.ucWaveInfo0.Location = new System.Drawing.Point(0, 0);
            this.ucWaveInfo0.Name = "ucWaveInfo0";
            this.ucWaveInfo0.Size = new System.Drawing.Size(299, 328);
            this.ucWaveInfo0.StockCode = null;
            this.ucWaveInfo0.TabIndex = 0;
            this.ucWaveInfo0.ToDate = null;
            this.ucWaveInfo0.ToDate0 = null;
            this.ucWaveInfo0.ToDate1 = null;
            this.ucWaveInfo0.ToDate2 = null;
            this.ucWaveInfo0.ToDate3 = null;
            // 
            // tbConB
            // 
            this.tbConB.Controls.Add(this.tabPage3);
            this.tbConB.Controls.Add(this.tabPage4);
            this.tbConB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbConB.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tbConB.Location = new System.Drawing.Point(0, 0);
            this.tbConB.Name = "tbConB";
            this.tbConB.SelectedIndex = 0;
            this.tbConB.Size = new System.Drawing.Size(853, 789);
            this.tbConB.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ucAnalysisA0);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(845, 763);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // ucAnalysisA0
            // 
            this.ucAnalysisA0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAnalysisA0.FromDate = null;
            this.ucAnalysisA0.Location = new System.Drawing.Point(3, 3);
            this.ucAnalysisA0.Name = "ucAnalysisA0";
            this.ucAnalysisA0.Size = new System.Drawing.Size(839, 757);
            this.ucAnalysisA0.StockCode = null;
            this.ucAnalysisA0.TabIndex = 0;
            this.ucAnalysisA0.ToDate = null;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(845, 763);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnTodayVolumeView
            // 
            this.btnTodayVolumeView.Location = new System.Drawing.Point(639, 7);
            this.btnTodayVolumeView.Name = "btnTodayVolumeView";
            this.btnTodayVolumeView.Size = new System.Drawing.Size(131, 23);
            this.btnTodayVolumeView.TabIndex = 10;
            this.btnTodayVolumeView.Text = "그룹거래정보보기";
            this.btnTodayVolumeView.UseVisualStyleBackColor = true;
            this.btnTodayVolumeView.Click += new System.EventHandler(this.btnTodayVolumeView_Click);
            // 
            // lblSGroupCode
            // 
            this.lblSGroupCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSGroupCode.Location = new System.Drawing.Point(258, 9);
            this.lblSGroupCode.Name = "lblSGroupCode";
            this.lblSGroupCode.Size = new System.Drawing.Size(93, 19);
            this.lblSGroupCode.TabIndex = 11;
            this.lblSGroupCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmTotalAnaylsis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 845);
            this.Controls.Add(this.splitConA);
            this.Name = "frmTotalAnaylsis";
            this.Text = "frmTotalAnaylsis";
            this.splitConA.Panel1.ResumeLayout(false);
            this.splitConA.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitConA)).EndInit();
            this.splitConA.ResumeLayout(false);
            this.pnA.ResumeLayout(false);
            this.pnA.PerformLayout();
            this.splitConB.Panel1.ResumeLayout(false);
            this.splitConB.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitConB)).EndInit();
            this.splitConB.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tbConA.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tbConB.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitConA;
        private System.Windows.Forms.Panel pnA;
        private System.Windows.Forms.CheckBox chkLeftView;
        private System.Windows.Forms.SplitContainer splitConB;
        private System.Windows.Forms.TabControl tbConA;
        private System.Windows.Forms.TabPage tabPage1;
        private Common.Uc.ucFav ucFav0;
        private System.Windows.Forms.TabPage tabPage2;
        private Common.Uc.ucStockList ucStockList0;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Common.Uc.ucWaveInfo ucWaveInfo0;
        internal System.Windows.Forms.Label lblToDate;
        internal System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.TabControl tbConB;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private Uc.ucAnalysisA ucAnalysisA0;
        private System.Windows.Forms.Button btnAllAnalysisA;
        private System.Windows.Forms.Button btnAllWave;
        private System.Windows.Forms.Button btnTodayVolumeView;
        internal System.Windows.Forms.Label lblSGroupCode;
    }
}