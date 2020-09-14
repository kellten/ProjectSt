namespace StudyProject.Exec.Mdi
{
    partial class MdiStudy
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
            this.designPatternToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gOFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.singleTonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbMenuSingleTon1 = new System.Windows.Forms.ToolStripMenuItem();
            this.factoryMethodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbFactoryMethod1 = new System.Windows.Forms.ToolStripMenuItem();
            this.abstractFactoryMethodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbAstractFM1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.interfaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TsbbindingList = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.designPatternToolStripMenuItem,
            this.cControlToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(737, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // designPatternToolStripMenuItem
            // 
            this.designPatternToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gOFToolStripMenuItem});
            this.designPatternToolStripMenuItem.Name = "designPatternToolStripMenuItem";
            this.designPatternToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.designPatternToolStripMenuItem.Text = "DesignPattern";
            // 
            // gOFToolStripMenuItem
            // 
            this.gOFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.singleTonToolStripMenuItem,
            this.factoryMethodToolStripMenuItem,
            this.abstractFactoryMethodToolStripMenuItem});
            this.gOFToolStripMenuItem.Name = "gOFToolStripMenuItem";
            this.gOFToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.gOFToolStripMenuItem.Text = "GOF";
            // 
            // singleTonToolStripMenuItem
            // 
            this.singleTonToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbMenuSingleTon1});
            this.singleTonToolStripMenuItem.Name = "singleTonToolStripMenuItem";
            this.singleTonToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.singleTonToolStripMenuItem.Text = "SingleTon";
            // 
            // tsbMenuSingleTon1
            // 
            this.tsbMenuSingleTon1.Name = "tsbMenuSingleTon1";
            this.tsbMenuSingleTon1.Size = new System.Drawing.Size(152, 22);
            this.tsbMenuSingleTon1.Text = "SingleTon1";
            this.tsbMenuSingleTon1.Click += new System.EventHandler(this.tsbMenuSingleTon1_Click);
            // 
            // factoryMethodToolStripMenuItem
            // 
            this.factoryMethodToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFactoryMethod1});
            this.factoryMethodToolStripMenuItem.Name = "factoryMethodToolStripMenuItem";
            this.factoryMethodToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.factoryMethodToolStripMenuItem.Text = "FactoryMethod";
            // 
            // tsbFactoryMethod1
            // 
            this.tsbFactoryMethod1.Name = "tsbFactoryMethod1";
            this.tsbFactoryMethod1.Size = new System.Drawing.Size(162, 22);
            this.tsbFactoryMethod1.Text = "FactoryMethod1";
            this.tsbFactoryMethod1.Click += new System.EventHandler(this.tsbFactoryMethod1_Click);
            // 
            // abstractFactoryMethodToolStripMenuItem
            // 
            this.abstractFactoryMethodToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAstractFM1});
            this.abstractFactoryMethodToolStripMenuItem.Name = "abstractFactoryMethodToolStripMenuItem";
            this.abstractFactoryMethodToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.abstractFactoryMethodToolStripMenuItem.Text = "AbstractFactoryMethod";
            // 
            // tsbAstractFM1
            // 
            this.tsbAstractFM1.Name = "tsbAstractFM1";
            this.tsbAstractFM1.Size = new System.Drawing.Size(142, 22);
            this.tsbAstractFM1.Text = "AbstractFM1";
            this.tsbAstractFM1.Click += new System.EventHandler(this.tsbAstractFM1_Click);
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
            // cControlToolStripMenuItem
            // 
            this.cControlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.interfaceToolStripMenuItem});
            this.cControlToolStripMenuItem.Name = "cControlToolStripMenuItem";
            this.cControlToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.cControlToolStripMenuItem.Text = "C# Control";
            // 
            // interfaceToolStripMenuItem
            // 
            this.interfaceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbbindingList});
            this.interfaceToolStripMenuItem.Name = "interfaceToolStripMenuItem";
            this.interfaceToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.interfaceToolStripMenuItem.Text = "Interface Framework";
            // 
            // TsbbindingList
            // 
            this.TsbbindingList.Name = "TsbbindingList";
            this.TsbbindingList.Size = new System.Drawing.Size(152, 22);
            this.TsbbindingList.Text = "BindingList";
            this.TsbbindingList.Click += new System.EventHandler(this.TsbbindingList_Click);
            // 
            // MdiStudy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 418);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MdiStudy";
            this.Text = "MdiStudy";
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
        private System.Windows.Forms.ToolStripMenuItem designPatternToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gOFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem singleTonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsbMenuSingleTon1;
        private System.Windows.Forms.ToolStripMenuItem factoryMethodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsbFactoryMethod1;
        private System.Windows.Forms.ToolStripMenuItem abstractFactoryMethodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsbAstractFM1;
        private System.Windows.Forms.ToolStripMenuItem cControlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem interfaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TsbbindingList;
    }
}



