﻿namespace PaikRichStockMain.Mdi
{
    partial class PaikRichStockMdi
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ucMainStockVer2 = new PaikRichStock.Common.ucMainStockVer2();
            this.pnMain = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsMenuItem1000 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem1100 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem1110 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem1200 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem2000 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem2100 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem2200 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem2210 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem2220 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem3000 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem3100 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem4000 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItem4100 = new System.Windows.Forms.ToolStripMenuItem();
            this.pnStockList = new System.Windows.Forms.Panel();
            this.tcStockList = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ucFavList = new PaikRichStock.UcForm.ucFavList();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ucCondition = new PaikRichStock.UcForm.ucCondition();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnLeft = new System.Windows.Forms.ToolStripButton();
            this.tsBtnRight = new System.Windows.Forms.ToolStripButton();
            this.tsCbFormOpenType = new System.Windows.Forms.ToolStripComboBox();
            this.tsBtnView = new System.Windows.Forms.ToolStripButton();
            this.mnuItemTest = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemTestTask = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.pnMain.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.pnStockList.SuspendLayout();
            this.tcStockList.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 657);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.Size = new System.Drawing.Size(1392, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(31, 17);
            this.toolStripStatusLabel.Text = "상태";
            // 
            // ucMainStockVer2
            // 
            this.ucMainStockVer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMainStockVer2.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ucMainStockVer2.Location = new System.Drawing.Point(0, 0);
            this.ucMainStockVer2.LoggerStartOption = false;
            this.ucMainStockVer2.Name = "ucMainStockVer2";
            this.ucMainStockVer2.Size = new System.Drawing.Size(1392, 34);
            this.ucMainStockVer2.TabIndex = 0;
            // 
            // pnMain
            // 
            this.pnMain.Controls.Add(this.ucMainStockVer2);
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnMain.Location = new System.Drawing.Point(0, 24);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(1392, 34);
            this.pnMain.TabIndex = 6;
            this.pnMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnMain_Paint);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuItem1000,
            this.tsMenuItem2000,
            this.tsMenuItem3000,
            this.tsMenuItem4000,
            this.mnuItemTest});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1392, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsMenuItem1000
            // 
            this.tsMenuItem1000.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuItem1100,
            this.tsMenuItem1200});
            this.tsMenuItem1000.Name = "tsMenuItem1000";
            this.tsMenuItem1000.Size = new System.Drawing.Size(43, 20);
            this.tsMenuItem1000.Text = "기능";
            // 
            // tsMenuItem1100
            // 
            this.tsMenuItem1100.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuItem1110});
            this.tsMenuItem1100.Name = "tsMenuItem1100";
            this.tsMenuItem1100.Size = new System.Drawing.Size(122, 22);
            this.tsMenuItem1100.Text = "배치작업";
            // 
            // tsMenuItem1110
            // 
            this.tsMenuItem1110.Name = "tsMenuItem1110";
            this.tsMenuItem1110.Size = new System.Drawing.Size(122, 22);
            this.tsMenuItem1110.Text = "매동분석";
            this.tsMenuItem1110.Click += new System.EventHandler(this.tsMenuItem1110_Click);
            // 
            // tsMenuItem1200
            // 
            this.tsMenuItem1200.Name = "tsMenuItem1200";
            this.tsMenuItem1200.Size = new System.Drawing.Size(122, 22);
            this.tsMenuItem1200.Text = "관심종목";
            this.tsMenuItem1200.Click += new System.EventHandler(this.tsMenuItem1200_Click);
            // 
            // tsMenuItem2000
            // 
            this.tsMenuItem2000.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuItem2100,
            this.tsMenuItem2200});
            this.tsMenuItem2000.Name = "tsMenuItem2000";
            this.tsMenuItem2000.Size = new System.Drawing.Size(43, 20);
            this.tsMenuItem2000.Text = "분석";
            // 
            // tsMenuItem2100
            // 
            this.tsMenuItem2100.Name = "tsMenuItem2100";
            this.tsMenuItem2100.Size = new System.Drawing.Size(122, 22);
            this.tsMenuItem2100.Text = "금융정보";
            this.tsMenuItem2100.Click += new System.EventHandler(this.tsMenuItem2100_Click);
            // 
            // tsMenuItem2200
            // 
            this.tsMenuItem2200.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuItem2210,
            this.tsMenuItem2220});
            this.tsMenuItem2200.Name = "tsMenuItem2200";
            this.tsMenuItem2200.Size = new System.Drawing.Size(122, 22);
            this.tsMenuItem2200.Text = "매동";
            // 
            // tsMenuItem2210
            // 
            this.tsMenuItem2210.Name = "tsMenuItem2210";
            this.tsMenuItem2210.Size = new System.Drawing.Size(158, 22);
            this.tsMenuItem2210.Text = "매동분석";
            this.tsMenuItem2210.Click += new System.EventHandler(this.tsMenuItem2210_Click);
            // 
            // tsMenuItem2220
            // 
            this.tsMenuItem2220.Name = "tsMenuItem2220";
            this.tsMenuItem2220.Size = new System.Drawing.Size(158, 22);
            this.tsMenuItem2220.Text = "조건별매동분석";
            this.tsMenuItem2220.Click += new System.EventHandler(this.tsMenuItem2220_Click);
            // 
            // tsMenuItem3000
            // 
            this.tsMenuItem3000.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuItem3100});
            this.tsMenuItem3000.Name = "tsMenuItem3000";
            this.tsMenuItem3000.Size = new System.Drawing.Size(43, 20);
            this.tsMenuItem3000.Text = "주문";
            // 
            // tsMenuItem3100
            // 
            this.tsMenuItem3100.Name = "tsMenuItem3100";
            this.tsMenuItem3100.Size = new System.Drawing.Size(152, 22);
            this.tsMenuItem3100.Text = "NewsFinder";
            this.tsMenuItem3100.Click += new System.EventHandler(this.tsMenuItem3100_Click);
            // 
            // tsMenuItem4000
            // 
            this.tsMenuItem4000.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuItem4100});
            this.tsMenuItem4000.Name = "tsMenuItem4000";
            this.tsMenuItem4000.Size = new System.Drawing.Size(43, 20);
            this.tsMenuItem4000.Text = "차트";
            // 
            // tsMenuItem4100
            // 
            this.tsMenuItem4100.Name = "tsMenuItem4100";
            this.tsMenuItem4100.Size = new System.Drawing.Size(152, 22);
            this.tsMenuItem4100.Text = "차트(시영)";
            this.tsMenuItem4100.Click += new System.EventHandler(this.tsMenuItem4100_Click);
            // 
            // pnStockList
            // 
            this.pnStockList.Controls.Add(this.tcStockList);
            this.pnStockList.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnStockList.Location = new System.Drawing.Point(0, 83);
            this.pnStockList.Name = "pnStockList";
            this.pnStockList.Size = new System.Drawing.Size(360, 574);
            this.pnStockList.TabIndex = 10;
            // 
            // tcStockList
            // 
            this.tcStockList.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tcStockList.Controls.Add(this.tabPage1);
            this.tcStockList.Controls.Add(this.tabPage2);
            this.tcStockList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcStockList.Location = new System.Drawing.Point(0, 0);
            this.tcStockList.Name = "tcStockList";
            this.tcStockList.SelectedIndex = 0;
            this.tcStockList.Size = new System.Drawing.Size(360, 574);
            this.tcStockList.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ucFavList);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(352, 545);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "관심";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ucFavList
            // 
            this.ucFavList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucFavList.Font = new System.Drawing.Font("굴림체", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ucFavList.Location = new System.Drawing.Point(3, 3);
            this.ucFavList.Name = "ucFavList";
            this.ucFavList.Size = new System.Drawing.Size(346, 539);
            this.ucFavList.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ucCondition);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(352, 545);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "조건";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ucCondition
            // 
            this.ucCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCondition.Font = new System.Drawing.Font("굴림체", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ucCondition.Location = new System.Drawing.Point(3, 3);
            this.ucCondition.Name = "ucCondition";
            this.ucCondition.Size = new System.Drawing.Size(346, 539);
            this.ucCondition.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnLeft,
            this.tsBtnRight,
            this.tsCbFormOpenType,
            this.tsBtnView});
            this.toolStrip1.Location = new System.Drawing.Point(0, 58);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1392, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsBtnLeft
            // 
            this.tsBtnLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnLeft.Name = "tsBtnLeft";
            this.tsBtnLeft.Size = new System.Drawing.Size(23, 22);
            this.tsBtnLeft.Text = "◀";
            this.tsBtnLeft.Click += new System.EventHandler(this.tsBtnLeft_Click);
            // 
            // tsBtnRight
            // 
            this.tsBtnRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnRight.Name = "tsBtnRight";
            this.tsBtnRight.Size = new System.Drawing.Size(23, 22);
            this.tsBtnRight.Text = "▶";
            this.tsBtnRight.Click += new System.EventHandler(this.tsBtnRight_Click);
            // 
            // tsCbFormOpenType
            // 
            this.tsCbFormOpenType.Items.AddRange(new object[] {
            "MDI",
            "독립실행"});
            this.tsCbFormOpenType.Name = "tsCbFormOpenType";
            this.tsCbFormOpenType.Size = new System.Drawing.Size(121, 25);
            // 
            // tsBtnView
            // 
            this.tsBtnView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnView.Name = "tsBtnView";
            this.tsBtnView.Size = new System.Drawing.Size(37, 22);
            this.tsBtnView.Text = "View";
            // 
            // mnuItemTest
            // 
            this.mnuItemTest.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemTestTask});
            this.mnuItemTest.Name = "mnuItemTest";
            this.mnuItemTest.Size = new System.Drawing.Size(55, 20);
            this.mnuItemTest.Text = "테스트";
            // 
            // mnuItemTestTask
            // 
            this.mnuItemTestTask.Name = "mnuItemTestTask";
            this.mnuItemTestTask.Size = new System.Drawing.Size(152, 22);
            this.mnuItemTestTask.Text = "Task테스트";
            this.mnuItemTestTask.Click += new System.EventHandler(this.mnuItemTestTask_Click);
            // 
            // PaikRichStockMdi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1392, 679);
            this.Controls.Add(this.pnStockList);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Name = "PaikRichStockMdi";
            this.Text = "MDIParent1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PaikRichStockMdi_KeyDown);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.pnMain.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnStockList.ResumeLayout(false);
            this.tcStockList.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private PaikRichStock.Common.ucMainStockVer2 ucMainStockVer2;
        private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel pnStockList;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItem1000;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItem2000;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItem3000;
        private System.Windows.Forms.TabControl tcStockList;
        private System.Windows.Forms.TabPage tabPage1;
        private PaikRichStock.UcForm.ucFavList ucFavList;
        private System.Windows.Forms.TabPage tabPage2;
        private PaikRichStock.UcForm.ucCondition ucCondition;
        private System.Windows.Forms.ToolStripButton tsBtnView;
        private System.Windows.Forms.ToolStripButton tsBtnLeft;
        private System.Windows.Forms.ToolStripButton tsBtnRight;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItem3100;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItem1100;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItem1110;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItem1200;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItem2100;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItem4000;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItem4100;
        private System.Windows.Forms.ToolStripComboBox tsCbFormOpenType;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItem2200;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItem2210;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItem2220;
        private System.Windows.Forms.ToolStripMenuItem mnuItemTest;
        private System.Windows.Forms.ToolStripMenuItem mnuItemTestTask;
    }
}



