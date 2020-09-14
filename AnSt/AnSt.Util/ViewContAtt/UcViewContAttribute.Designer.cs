namespace AnSt.Util.ViewContAtt
{
    partial class UcViewContAttribute
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitConMain = new System.Windows.Forms.SplitContainer();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblControlName = new System.Windows.Forms.Label();
            this.lblFormName = new System.Windows.Forms.Label();
            this.proPertyGrid = new System.Windows.Forms.PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.splitConMain)).BeginInit();
            this.splitConMain.Panel1.SuspendLayout();
            this.splitConMain.Panel2.SuspendLayout();
            this.splitConMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitConMain
            // 
            this.splitConMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitConMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitConMain.Location = new System.Drawing.Point(0, 0);
            this.splitConMain.Name = "splitConMain";
            this.splitConMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitConMain.Panel1
            // 
            this.splitConMain.Panel1.Controls.Add(this.btnRefresh);
            this.splitConMain.Panel1.Controls.Add(this.lblControlName);
            this.splitConMain.Panel1.Controls.Add(this.lblFormName);
            // 
            // splitConMain.Panel2
            // 
            this.splitConMain.Panel2.Controls.Add(this.proPertyGrid);
            this.splitConMain.Size = new System.Drawing.Size(279, 675);
            this.splitConMain.SplitterDistance = 25;
            this.splitConMain.SplitterWidth = 9;
            this.splitConMain.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(241, -1);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(31, 23);
            this.btnRefresh.TabIndex = 12;
            this.btnRefresh.Text = "!";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblControlName
            // 
            this.lblControlName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblControlName.Location = new System.Drawing.Point(98, 0);
            this.lblControlName.Name = "lblControlName";
            this.lblControlName.Size = new System.Drawing.Size(137, 21);
            this.lblControlName.TabIndex = 11;
            this.lblControlName.Text = "검색어";
            this.lblControlName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFormName
            // 
            this.lblFormName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFormName.Location = new System.Drawing.Point(-2, 0);
            this.lblFormName.Name = "lblFormName";
            this.lblFormName.Size = new System.Drawing.Size(100, 21);
            this.lblFormName.TabIndex = 10;
            this.lblFormName.Text = "검색어";
            this.lblFormName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // proPertyGrid
            // 
            this.proPertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.proPertyGrid.LineColor = System.Drawing.SystemColors.ControlDark;
            this.proPertyGrid.Location = new System.Drawing.Point(0, 0);
            this.proPertyGrid.Name = "proPertyGrid";
            this.proPertyGrid.Size = new System.Drawing.Size(275, 637);
            this.proPertyGrid.TabIndex = 0;
            // 
            // UcViewContAttribute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitConMain);
            this.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "UcViewContAttribute";
            this.Size = new System.Drawing.Size(279, 675);
            this.splitConMain.Panel1.ResumeLayout(false);
            this.splitConMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitConMain)).EndInit();
            this.splitConMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitConMain;
        internal System.Windows.Forms.Label lblControlName;
        internal System.Windows.Forms.Label lblFormName;
        private System.Windows.Forms.PropertyGrid proPertyGrid;
        private System.Windows.Forms.Button btnRefresh;
    }
}
