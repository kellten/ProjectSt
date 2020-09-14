namespace AnSt.Mdi
{
    partial class MdiAnSt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MdiAnSt));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItemBasicSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbMenuItem1100 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbMenuItem1110 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbMenuItem1120 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbMenuItem1130 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbMenuItem1200 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbMenuItem1210 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbWaveMenuItem1220 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemChart = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem2100 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemFavTester = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemFrmFavTester = new System.Windows.Forms.ToolStripMenuItem();
            this.종목Wave입력ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.속성값보기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.종목볼륨기본ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsBtnFav = new System.Windows.Forms.ToolStripButton();
            this.tsBtnStock = new System.Windows.Forms.ToolStripButton();
            this.tsBtnDanFav = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ucDasinFav1 = new AnSt.BasicSetting.Favorite.UcDasinFav();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemBasicSetting,
            this.ToolStripMenuItemChart,
            this.ToolStripMenuItemFavTester});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1295, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // ToolStripMenuItemBasicSetting
            // 
            this.ToolStripMenuItemBasicSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbMenuItem1100,
            this.toolStripSeparator2,
            this.tsbMenuItem1200});
            this.ToolStripMenuItemBasicSetting.Name = "ToolStripMenuItemBasicSetting";
            this.ToolStripMenuItemBasicSetting.Size = new System.Drawing.Size(67, 20);
            this.ToolStripMenuItemBasicSetting.Text = "기본설정";
            // 
            // tsbMenuItem1100
            // 
            this.tsbMenuItem1100.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbMenuItem1110,
            this.tsbMenuItem1120,
            this.toolStripSeparator1,
            this.tsbMenuItem1130});
            this.tsbMenuItem1100.Name = "tsbMenuItem1100";
            this.tsbMenuItem1100.Size = new System.Drawing.Size(127, 22);
            this.tsbMenuItem1100.Text = "관심종목";
            // 
            // tsbMenuItem1110
            // 
            this.tsbMenuItem1110.Name = "tsbMenuItem1110";
            this.tsbMenuItem1110.Size = new System.Drawing.Size(150, 22);
            this.tsbMenuItem1110.Text = "관심종목";
            this.tsbMenuItem1110.Click += new System.EventHandler(this.tsbMenuItem1110_Click);
            // 
            // tsbMenuItem1120
            // 
            this.tsbMenuItem1120.Name = "tsbMenuItem1120";
            this.tsbMenuItem1120.Size = new System.Drawing.Size(150, 22);
            this.tsbMenuItem1120.Text = "관심종목 관리";
            this.tsbMenuItem1120.Click += new System.EventHandler(this.tsbMenuItem1120_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(147, 6);
            // 
            // tsbMenuItem1130
            // 
            this.tsbMenuItem1130.Name = "tsbMenuItem1130";
            this.tsbMenuItem1130.Size = new System.Drawing.Size(150, 22);
            this.tsbMenuItem1130.Text = "전체종목";
            this.tsbMenuItem1130.Click += new System.EventHandler(this.tsbMenuItem1130_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(124, 6);
            // 
            // tsbMenuItem1200
            // 
            this.tsbMenuItem1200.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbMenuItem1210,
            this.tsbWaveMenuItem1220});
            this.tsbMenuItem1200.Name = "tsbMenuItem1200";
            this.tsbMenuItem1200.Size = new System.Drawing.Size(127, 22);
            this.tsbMenuItem1200.Text = "Wave정보";
            // 
            // tsbMenuItem1210
            // 
            this.tsbMenuItem1210.Name = "tsbMenuItem1210";
            this.tsbMenuItem1210.Size = new System.Drawing.Size(155, 22);
            this.tsbMenuItem1210.Text = "Wave정보";
            // 
            // tsbWaveMenuItem1220
            // 
            this.tsbWaveMenuItem1220.Name = "tsbWaveMenuItem1220";
            this.tsbWaveMenuItem1220.Size = new System.Drawing.Size(155, 22);
            this.tsbWaveMenuItem1220.Text = "Wave정보 관리";
            this.tsbWaveMenuItem1220.Click += new System.EventHandler(this.tsbWaveMenuItem1220_Click);
            // 
            // ToolStripMenuItemChart
            // 
            this.ToolStripMenuItemChart.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem2100});
            this.ToolStripMenuItemChart.Name = "ToolStripMenuItemChart";
            this.ToolStripMenuItemChart.Size = new System.Drawing.Size(43, 20);
            this.ToolStripMenuItemChart.Text = "차트";
            // 
            // MenuItem2100
            // 
            this.MenuItem2100.Name = "MenuItem2100";
            this.MenuItem2100.Size = new System.Drawing.Size(122, 22);
            this.MenuItem2100.Text = "기본차트";
            this.MenuItem2100.Click += new System.EventHandler(this.MenuItem2100_Click);
            // 
            // ToolStripMenuItemFavTester
            // 
            this.ToolStripMenuItemFavTester.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemFrmFavTester,
            this.종목Wave입력ToolStripMenuItem,
            this.속성값보기ToolStripMenuItem,
            this.종목볼륨기본ToolStripMenuItem});
            this.ToolStripMenuItemFavTester.Name = "ToolStripMenuItemFavTester";
            this.ToolStripMenuItemFavTester.Size = new System.Drawing.Size(55, 20);
            this.ToolStripMenuItemFavTester.Text = "테스트";
            // 
            // ToolStripMenuItemFrmFavTester
            // 
            this.ToolStripMenuItemFrmFavTester.Name = "ToolStripMenuItemFrmFavTester";
            this.ToolStripMenuItemFrmFavTester.Size = new System.Drawing.Size(158, 22);
            this.ToolStripMenuItemFrmFavTester.Text = "즐겨찾기테스트";
            this.ToolStripMenuItemFrmFavTester.Click += new System.EventHandler(this.ToolStripMenuItemFrmFavTester_Click);
            // 
            // 종목Wave입력ToolStripMenuItem
            // 
            this.종목Wave입력ToolStripMenuItem.Name = "종목Wave입력ToolStripMenuItem";
            this.종목Wave입력ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.종목Wave입력ToolStripMenuItem.Text = "종목Wave입력";
            this.종목Wave입력ToolStripMenuItem.Click += new System.EventHandler(this.종목Wave입력ToolStripMenuItem_Click);
            // 
            // 속성값보기ToolStripMenuItem
            // 
            this.속성값보기ToolStripMenuItem.Name = "속성값보기ToolStripMenuItem";
            this.속성값보기ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.속성값보기ToolStripMenuItem.Text = "속성값보기";
            this.속성값보기ToolStripMenuItem.Click += new System.EventHandler(this.속성값보기ToolStripMenuItem_Click);
            // 
            // 종목볼륨기본ToolStripMenuItem
            // 
            this.종목볼륨기본ToolStripMenuItem.Name = "종목볼륨기본ToolStripMenuItem";
            this.종목볼륨기본ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.종목볼륨기본ToolStripMenuItem.Text = "종목볼륨기본";
            this.종목볼륨기본ToolStripMenuItem.Click += new System.EventHandler(this.종목볼륨기본ToolStripMenuItem_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnFav,
            this.tsBtnStock,
            this.tsBtnDanFav});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1295, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "ToolStrip";
            // 
            // tsBtnFav
            // 
            this.tsBtnFav.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnFav.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnFav.Name = "tsBtnFav";
            this.tsBtnFav.Size = new System.Drawing.Size(29, 22);
            this.tsBtnFav.Text = "Fav";
            this.tsBtnFav.Click += new System.EventHandler(this.tsBtnFav_Click);
            // 
            // tsBtnStock
            // 
            this.tsBtnStock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnStock.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnStock.Image")));
            this.tsBtnStock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnStock.Name = "tsBtnStock";
            this.tsBtnStock.Size = new System.Drawing.Size(23, 22);
            this.tsBtnStock.Text = "St";
            this.tsBtnStock.Click += new System.EventHandler(this.tsBtnStock_Click);
            // 
            // tsBtnDanFav
            // 
            this.tsBtnDanFav.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnDanFav.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnDanFav.Name = "tsBtnDanFav";
            this.tsBtnDanFav.Size = new System.Drawing.Size(44, 22);
            this.tsBtnDanFav.Text = "DaFav";
            this.tsBtnDanFav.Click += new System.EventHandler(this.tsBtnDanFav_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 784);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.Size = new System.Drawing.Size(1295, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(31, 17);
            this.toolStripStatusLabel.Text = "상태";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ucDasinFav1);
            this.splitContainer1.Size = new System.Drawing.Size(1295, 735);
            this.splitContainer1.SplitterDistance = 430;
            this.splitContainer1.TabIndex = 4;
            // 
            // ucDasinFav1
            // 
            this.ucDasinFav1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ucDasinFav1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDasinFav1.Location = new System.Drawing.Point(0, 0);
            this.ucDasinFav1.Name = "ucDasinFav1";
            this.ucDasinFav1.Size = new System.Drawing.Size(426, 731);
            this.ucDasinFav1.TabIndex = 0;
            this.ucDasinFav1.Load += new System.EventHandler(this.ucDasinFav1_Load);
            // 
            // MdiAnSt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1295, 806);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MdiAnSt";
            this.Text = "MdiAnSt";
            this.Load += new System.EventHandler(this.MdiAnSt_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemBasicSetting;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemFavTester;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemFrmFavTester;
        private System.Windows.Forms.ToolStripMenuItem 종목Wave입력ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsBtnFav;
        private System.Windows.Forms.ToolStripButton tsBtnStock;
        private System.Windows.Forms.ToolStripMenuItem tsbMenuItem1100;
        private System.Windows.Forms.ToolStripMenuItem tsbMenuItem1110;
        private System.Windows.Forms.ToolStripMenuItem tsbMenuItem1120;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsbMenuItem1130;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsbMenuItem1200;
        private System.Windows.Forms.ToolStripMenuItem tsbMenuItem1210;
        private System.Windows.Forms.ToolStripMenuItem tsbWaveMenuItem1220;
        private System.Windows.Forms.ToolStripMenuItem 속성값보기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemChart;
        private System.Windows.Forms.ToolStripMenuItem MenuItem2100;
        private System.Windows.Forms.ToolStripButton tsBtnDanFav;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem 종목볼륨기본ToolStripMenuItem;
        private BasicSetting.Favorite.UcDasinFav ucDasinFav1;
    }
}



