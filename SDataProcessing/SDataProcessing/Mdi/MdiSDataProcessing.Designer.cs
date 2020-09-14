namespace SDataProcessing.Mdi
{
    partial class MdiSDataProcessing
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
            this.작업ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemVolumeCollection = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemVolume10060Collection = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripDbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripCybosStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.newVolume10060CollectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.작업ToolStripMenuItem,
            this.MenuItem2});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(737, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // 작업ToolStripMenuItem
            // 
            this.작업ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemConnection});
            this.작업ToolStripMenuItem.Name = "작업ToolStripMenuItem";
            this.작업ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.작업ToolStripMenuItem.Text = "기본";
            // 
            // MenuItemConnection
            // 
            this.MenuItemConnection.Name = "MenuItemConnection";
            this.MenuItemConnection.Size = new System.Drawing.Size(180, 22);
            this.MenuItemConnection.Text = "접속";
            this.MenuItemConnection.Click += new System.EventHandler(this.MenuItemConnection_Click);
            // 
            // MenuItem2
            // 
            this.MenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemVolumeCollection,
            this.menuItemVolume10060Collection,
            this.newVolume10060CollectionToolStripMenuItem});
            this.MenuItem2.Name = "MenuItem2";
            this.MenuItem2.Size = new System.Drawing.Size(67, 20);
            this.MenuItem2.Text = "배치작업";
            // 
            // menuItemVolumeCollection
            // 
            this.menuItemVolumeCollection.Name = "menuItemVolumeCollection";
            this.menuItemVolumeCollection.Size = new System.Drawing.Size(237, 22);
            this.menuItemVolumeCollection.Text = "VolumeCollection";
            this.menuItemVolumeCollection.Click += new System.EventHandler(this.menuItemVolumeCollection_Click);
            // 
            // menuItemVolume10060Collection
            // 
            this.menuItemVolume10060Collection.Name = "menuItemVolume10060Collection";
            this.menuItemVolume10060Collection.Size = new System.Drawing.Size(237, 22);
            this.menuItemVolume10060Collection.Text = "Volume10060Collection";
            this.menuItemVolume10060Collection.Click += new System.EventHandler(this.menuItemVolume10060Collection_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripDbStatus,
            this.toolStripCybosStatus});
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
            // toolStripDbStatus
            // 
            this.toolStripDbStatus.Name = "toolStripDbStatus";
            this.toolStripDbStatus.Size = new System.Drawing.Size(47, 17);
            this.toolStripDbStatus.Text = "DB상태";
            // 
            // toolStripCybosStatus
            // 
            this.toolStripCybosStatus.Name = "toolStripCybosStatus";
            this.toolStripCybosStatus.Size = new System.Drawing.Size(73, 17);
            this.toolStripCybosStatus.Text = "CybosStatus";
            // 
            // newVolume10060CollectionToolStripMenuItem
            // 
            this.newVolume10060CollectionToolStripMenuItem.Name = "newVolume10060CollectionToolStripMenuItem";
            this.newVolume10060CollectionToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.newVolume10060CollectionToolStripMenuItem.Text = "(New)Volume10060Collection";
            this.newVolume10060CollectionToolStripMenuItem.Click += new System.EventHandler(this.newVolume10060CollectionToolStripMenuItem_Click);
            // 
            // MdiSDataProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 418);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MdiSDataProcessing";
            this.Text = "MdiSDataProcessing";
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
        private System.Windows.Forms.ToolStripMenuItem 작업ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItem2;
        private System.Windows.Forms.ToolStripMenuItem MenuItemConnection;
        private System.Windows.Forms.ToolStripStatusLabel toolStripDbStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripCybosStatus;
        private System.Windows.Forms.ToolStripMenuItem menuItemVolumeCollection;
        private System.Windows.Forms.ToolStripMenuItem menuItemVolume10060Collection;
        private System.Windows.Forms.ToolStripMenuItem newVolume10060CollectionToolStripMenuItem;
    }
}



