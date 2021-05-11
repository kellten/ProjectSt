namespace Woom.Tester.Mdi
{
    partial class MdiTester
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbConnectionInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.optTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem10059 = new System.Windows.Forms.ToolStripMenuItem();
            this.opt10069ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opt10081ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.마켓분석ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbFrmStockList = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.opt20068ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.종목관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.마켓분석ToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(737, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbConnectionInfo,
            this.optTestToolStripMenuItem,
            this.종목관리ToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.testToolStripMenuItem.Text = "Test";
            // 
            // tsbConnectionInfo
            // 
            this.tsbConnectionInfo.Name = "tsbConnectionInfo";
            this.tsbConnectionInfo.Size = new System.Drawing.Size(152, 22);
            this.tsbConnectionInfo.Text = "접속정보";
            this.tsbConnectionInfo.Click += new System.EventHandler(this.tsbConnectionInfo_Click);
            // 
            // optTestToolStripMenuItem
            // 
            this.optTestToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItem10059,
            this.opt10069ToolStripMenuItem,
            this.opt10081ToolStripMenuItem,
            this.opt20068ToolStripMenuItem});
            this.optTestToolStripMenuItem.Name = "optTestToolStripMenuItem";
            this.optTestToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.optTestToolStripMenuItem.Text = "OptTest";
            // 
            // mnuItem10059
            // 
            this.mnuItem10059.Name = "mnuItem10059";
            this.mnuItem10059.Size = new System.Drawing.Size(152, 22);
            this.mnuItem10059.Text = "Opt10059";
            this.mnuItem10059.Click += new System.EventHandler(this.mnuItem10059_Click);
            // 
            // opt10069ToolStripMenuItem
            // 
            this.opt10069ToolStripMenuItem.Name = "opt10069ToolStripMenuItem";
            this.opt10069ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.opt10069ToolStripMenuItem.Text = "Opt10069";
            this.opt10069ToolStripMenuItem.Click += new System.EventHandler(this.opt10069ToolStripMenuItem_Click);
            // 
            // opt10081ToolStripMenuItem
            // 
            this.opt10081ToolStripMenuItem.Name = "opt10081ToolStripMenuItem";
            this.opt10081ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.opt10081ToolStripMenuItem.Text = "Opt10081";
            this.opt10081ToolStripMenuItem.Click += new System.EventHandler(this.opt10081ToolStripMenuItem_Click);
            // 
            // 마켓분석ToolStripMenuItem
            // 
            this.마켓분석ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFrmStockList});
            this.마켓분석ToolStripMenuItem.Name = "마켓분석ToolStripMenuItem";
            this.마켓분석ToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.마켓분석ToolStripMenuItem.Text = "마켓 분석";
            // 
            // tsbFrmStockList
            // 
            this.tsbFrmStockList.Name = "tsbFrmStockList";
            this.tsbFrmStockList.Size = new System.Drawing.Size(162, 22);
            this.tsbFrmStockList.Text = "종목별 특이사항";
            this.tsbFrmStockList.Click += new System.EventHandler(this.tsbFrmStockList_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 396);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.Size = new System.Drawing.Size(737, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(31, 17);
            this.toolStripStatusLabel.Text = "상태";
            // 
            // opt20068ToolStripMenuItem
            // 
            this.opt20068ToolStripMenuItem.Name = "opt20068ToolStripMenuItem";
            this.opt20068ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.opt20068ToolStripMenuItem.Text = "Opt20068";
            this.opt20068ToolStripMenuItem.Click += new System.EventHandler(this.opt20068ToolStripMenuItem_Click);
            // 
            // 종목관리ToolStripMenuItem
            // 
            this.종목관리ToolStripMenuItem.Name = "종목관리ToolStripMenuItem";
            this.종목관리ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.종목관리ToolStripMenuItem.Text = "종목관리";
            this.종목관리ToolStripMenuItem.Click += new System.EventHandler(this.종목관리ToolStripMenuItem_Click);
            // 
            // MdiTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 418);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MdiTester";
            this.Text = "MdiTester";
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
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsbConnectionInfo;
        private System.Windows.Forms.ToolStripMenuItem optTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuItem10059;
        private System.Windows.Forms.ToolStripMenuItem opt10069ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opt10081ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 마켓분석ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsbFrmStockList;
        private System.Windows.Forms.ToolStripMenuItem opt20068ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 종목관리ToolStripMenuItem;
    }
}



