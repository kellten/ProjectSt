namespace AnalysisSt.Main.Mdi
{
    partial class mdiAnalysisSt
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.tsbConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemFavManage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStockWaveInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.계좌정보ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.주문ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.차트검색ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.투자분석ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbAnalysisTradeByDate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbTotalAnlysis = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbTester = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbTester01 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbChartTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbTradeQty = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbChartTest2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbBatch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbBatchTradeInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ucMain = new AnalysisSt.KiwoomVB.ucMainStockVer2();
            this.tsbToday = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbConfiguration,
            this.계좌정보ToolStripMenuItem,
            this.주문ToolStripMenuItem,
            this.차트검색ToolStripMenuItem,
            this.투자분석ToolStripMenuItem,
            this.tsbTester,
            this.tsbBatch});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1102, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // tsbConfiguration
            // 
            this.tsbConfiguration.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemFavManage,
            this.mnuStockWaveInfo});
            this.tsbConfiguration.Name = "tsbConfiguration";
            this.tsbConfiguration.Size = new System.Drawing.Size(43, 20);
            this.tsbConfiguration.Text = "설정";
            // 
            // mnuItemFavManage
            // 
            this.mnuItemFavManage.Name = "mnuItemFavManage";
            this.mnuItemFavManage.Size = new System.Drawing.Size(146, 22);
            this.mnuItemFavManage.Text = "종목관리";
            this.mnuItemFavManage.Click += new System.EventHandler(this.mnuItemFavManage_Click);
            // 
            // mnuStockWaveInfo
            // 
            this.mnuStockWaveInfo.Name = "mnuStockWaveInfo";
            this.mnuStockWaveInfo.Size = new System.Drawing.Size(146, 22);
            this.mnuStockWaveInfo.Text = "종목분석관리";
            this.mnuStockWaveInfo.Click += new System.EventHandler(this.mnuStockWaveInfo_Click);
            // 
            // 계좌정보ToolStripMenuItem
            // 
            this.계좌정보ToolStripMenuItem.Name = "계좌정보ToolStripMenuItem";
            this.계좌정보ToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.계좌정보ToolStripMenuItem.Text = "계좌정보";
            // 
            // 주문ToolStripMenuItem
            // 
            this.주문ToolStripMenuItem.Name = "주문ToolStripMenuItem";
            this.주문ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.주문ToolStripMenuItem.Text = "주문";
            // 
            // 차트검색ToolStripMenuItem
            // 
            this.차트검색ToolStripMenuItem.Name = "차트검색ToolStripMenuItem";
            this.차트검색ToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.차트검색ToolStripMenuItem.Text = "차트/검색";
            // 
            // 투자분석ToolStripMenuItem
            // 
            this.투자분석ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAnalysisTradeByDate,
            this.tsbTotalAnlysis});
            this.투자분석ToolStripMenuItem.Name = "투자분석ToolStripMenuItem";
            this.투자분석ToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.투자분석ToolStripMenuItem.Text = "투자분석";
            // 
            // tsbAnalysisTradeByDate
            // 
            this.tsbAnalysisTradeByDate.Name = "tsbAnalysisTradeByDate";
            this.tsbAnalysisTradeByDate.Size = new System.Drawing.Size(126, 22);
            this.tsbAnalysisTradeByDate.Text = "종목 분석";
            this.tsbAnalysisTradeByDate.Click += new System.EventHandler(this.tsbAnalysisTradeByDate_Click);
            // 
            // tsbTotalAnlysis
            // 
            this.tsbTotalAnlysis.Name = "tsbTotalAnlysis";
            this.tsbTotalAnlysis.Size = new System.Drawing.Size(126, 22);
            this.tsbTotalAnlysis.Text = "토탈 분석";
            this.tsbTotalAnlysis.Click += new System.EventHandler(this.tsbTotalAnlysis_Click);
            // 
            // tsbTester
            // 
            this.tsbTester.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbTester01,
            this.tsbChartTest,
            this.tsbTradeQty,
            this.tsbChartTest2,
            this.tsbToday});
            this.tsbTester.Name = "tsbTester";
            this.tsbTester.Size = new System.Drawing.Size(55, 20);
            this.tsbTester.Text = "테스트";
            // 
            // tsbTester01
            // 
            this.tsbTester01.Name = "tsbTester01";
            this.tsbTester01.Size = new System.Drawing.Size(152, 22);
            this.tsbTester01.Text = "DB접속테스트";
            this.tsbTester01.Click += new System.EventHandler(this.tsbTester01_Click);
            // 
            // tsbChartTest
            // 
            this.tsbChartTest.Name = "tsbChartTest";
            this.tsbChartTest.Size = new System.Drawing.Size(152, 22);
            this.tsbChartTest.Text = "차트테스트";
            this.tsbChartTest.Click += new System.EventHandler(this.tsbChartTest_Click);
            // 
            // tsbTradeQty
            // 
            this.tsbTradeQty.Name = "tsbTradeQty";
            this.tsbTradeQty.Size = new System.Drawing.Size(152, 22);
            this.tsbTradeQty.Text = "종목량 분석";
            this.tsbTradeQty.Click += new System.EventHandler(this.tsbTradeQty_Click);
            // 
            // tsbChartTest2
            // 
            this.tsbChartTest2.Name = "tsbChartTest2";
            this.tsbChartTest2.Size = new System.Drawing.Size(152, 22);
            this.tsbChartTest2.Text = "차트테스트2";
            this.tsbChartTest2.Click += new System.EventHandler(this.tsbChartTest2_Click);
            // 
            // tsbBatch
            // 
            this.tsbBatch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbBatchTradeInfo});
            this.tsbBatch.Name = "tsbBatch";
            this.tsbBatch.Size = new System.Drawing.Size(67, 20);
            this.tsbBatch.Text = "배치작업";
            // 
            // tsbBatchTradeInfo
            // 
            this.tsbBatchTradeInfo.Name = "tsbBatchTradeInfo";
            this.tsbBatchTradeInfo.Size = new System.Drawing.Size(190, 22);
            this.tsbBatchTradeInfo.Text = "거래원 분석 배치작업";
            this.tsbBatchTradeInfo.Click += new System.EventHandler(this.tsbBatchTradeInfo_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 527);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.Size = new System.Drawing.Size(1102, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(31, 17);
            this.toolStripStatusLabel.Text = "상태";
            // 
            // ucMain
            // 
            this.ucMain.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ucMain.Location = new System.Drawing.Point(0, 492);
            this.ucMain.LoggerStartOption = false;
            this.ucMain.Name = "ucMain";
            this.ucMain.Size = new System.Drawing.Size(842, 32);
            this.ucMain.TabIndex = 4;
            // 
            // tsbToday
            // 
            this.tsbToday.Name = "tsbToday";
            this.tsbToday.Size = new System.Drawing.Size(152, 22);
            this.tsbToday.Text = "당일 수급";
            this.tsbToday.Click += new System.EventHandler(this.tsbToday_Click);
            // 
            // mdiAnalysisSt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 549);
            this.Controls.Add(this.ucMain);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "mdiAnalysisSt";
            this.Text = "MDIParent1";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem tsbConfiguration;
        private System.Windows.Forms.ToolStripMenuItem 계좌정보ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 주문ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 차트검색ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 투자분석ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsbTester;
        private System.Windows.Forms.ToolStripMenuItem tsbTester01;
        private KiwoomVB.ucMainStockVer2 ucMain;
        private System.Windows.Forms.ToolStripMenuItem tsbBatch;
        private System.Windows.Forms.ToolStripMenuItem tsbBatchTradeInfo;
        private System.Windows.Forms.ToolStripMenuItem tsbAnalysisTradeByDate;
        private System.Windows.Forms.ToolStripMenuItem mnuItemFavManage;
        private System.Windows.Forms.ToolStripMenuItem tsbChartTest;
        private System.Windows.Forms.ToolStripMenuItem mnuStockWaveInfo;
        private System.Windows.Forms.ToolStripMenuItem tsbTradeQty;
        private System.Windows.Forms.ToolStripMenuItem tsbChartTest2;
        private System.Windows.Forms.ToolStripMenuItem tsbTotalAnlysis;
        private System.Windows.Forms.ToolStripMenuItem tsbToday;
    }
}



