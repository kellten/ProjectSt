namespace AnalysisSt.Chart.Uc
{
    partial class ucChartAreaSetting
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnAttriBute = new System.Windows.Forms.Panel();
            this.ChartAreaProp = new System.Windows.Forms.PropertyGrid();
            this.groupBox1.SuspendLayout();
            this.pnAttriBute.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pnAttriBute);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(234, 480);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "차트영역설정";
            // 
            // pnAttriBute
            // 
            this.pnAttriBute.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnAttriBute.Controls.Add(this.ChartAreaProp);
            this.pnAttriBute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnAttriBute.Location = new System.Drawing.Point(3, 17);
            this.pnAttriBute.Name = "pnAttriBute";
            this.pnAttriBute.Size = new System.Drawing.Size(228, 460);
            this.pnAttriBute.TabIndex = 0;
            // 
            // ChartAreaProp
            // 
            this.ChartAreaProp.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ChartAreaProp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartAreaProp.LineColor = System.Drawing.SystemColors.ControlDark;
            this.ChartAreaProp.Location = new System.Drawing.Point(0, 0);
            this.ChartAreaProp.Name = "ChartAreaProp";
            this.ChartAreaProp.Size = new System.Drawing.Size(224, 456);
            this.ChartAreaProp.TabIndex = 0;
            this.ChartAreaProp.ViewBackColor = System.Drawing.Color.Black;
            this.ChartAreaProp.ViewForeColor = System.Drawing.Color.White;
            // 
            // ucChartAreaSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ucChartAreaSetting";
            this.Size = new System.Drawing.Size(234, 480);
            this.Load += new System.EventHandler(this.ucChartAreaSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.pnAttriBute.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pnAttriBute;
        private System.Windows.Forms.PropertyGrid ChartAreaProp;
    }
}
