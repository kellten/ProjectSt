﻿namespace Woom.DataAccess.Uc
{
    partial class ucMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMain));
            this.AxKH = new AxKHOpenAPILib.AxKHOpenAPI();
            ((System.ComponentModel.ISupportInitialize)(this.AxKH)).BeginInit();
            this.SuspendLayout();
            // 
            // AxKH
            // 
            this.AxKH.Enabled = true;
            this.AxKH.Location = new System.Drawing.Point(3, 16);
            this.AxKH.Name = "AxKH";
            this.AxKH.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("AxKH.OcxState")));
            this.AxKH.Size = new System.Drawing.Size(72, 28);
            this.AxKH.TabIndex = 0;
            // 
            // ucMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AxKH);
            this.Name = "ucMain";
            this.Size = new System.Drawing.Size(86, 54);
            ((System.ComponentModel.ISupportInitialize)(this.AxKH)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxKHOpenAPILib.AxKHOpenAPI AxKH;
    }
}
