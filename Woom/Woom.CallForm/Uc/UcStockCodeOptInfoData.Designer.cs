namespace Woom.CallForm.Uc
{
    partial class UcStockCodeOptInfoData
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
            this.pnStockCodeOptInfoData = new System.Windows.Forms.Panel();
            this.dgvStockInfo = new System.Windows.Forms.DataGridView();
            this.pnStockCodeOptInfoData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnStockCodeOptInfoData
            // 
            this.pnStockCodeOptInfoData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnStockCodeOptInfoData.Controls.Add(this.dgvStockInfo);
            this.pnStockCodeOptInfoData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnStockCodeOptInfoData.Location = new System.Drawing.Point(0, 0);
            this.pnStockCodeOptInfoData.Name = "pnStockCodeOptInfoData";
            this.pnStockCodeOptInfoData.Size = new System.Drawing.Size(1404, 777);
            this.pnStockCodeOptInfoData.TabIndex = 0;
            // 
            // dgvStockInfo
            // 
            this.dgvStockInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStockInfo.Location = new System.Drawing.Point(0, 0);
            this.dgvStockInfo.Name = "dgvStockInfo";
            this.dgvStockInfo.RowTemplate.Height = 23;
            this.dgvStockInfo.Size = new System.Drawing.Size(1402, 775);
            this.dgvStockInfo.TabIndex = 0;
            this.dgvStockInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStockInfo_CellClick);
            // 
            // UcStockCodeOptInfoData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnStockCodeOptInfoData);
            this.Name = "UcStockCodeOptInfoData";
            this.Size = new System.Drawing.Size(1404, 777);
            this.pnStockCodeOptInfoData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnStockCodeOptInfoData;
        private System.Windows.Forms.DataGridView dgvStockInfo;
    }
}
