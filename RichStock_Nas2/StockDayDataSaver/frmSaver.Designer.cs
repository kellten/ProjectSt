﻿namespace StockDayDataSaver
{
    partial class frmSaver
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
            this.btn일봉데이터전송 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.UcMainStock1 = new PaikRichStock.Common.ucMainStock();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn일봉데이터전송
            // 
            this.btn일봉데이터전송.Location = new System.Drawing.Point(3, 30);
            this.btn일봉데이터전송.Name = "btn일봉데이터전송";
            this.btn일봉데이터전송.Size = new System.Drawing.Size(161, 31);
            this.btn일봉데이터전송.TabIndex = 1;
            this.btn일봉데이터전송.Text = "일봉데이터전송";
            this.btn일봉데이터전송.UseVisualStyleBackColor = true;
            this.btn일봉데이터전송.Click += new System.EventHandler(this.btn일봉데이터전송_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 265);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(830, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "작업이완료 되었습니다.";
            // 
            // tslStatus
            // 
            this.tslStatus.Name = "tslStatus";
            this.tslStatus.Size = new System.Drawing.Size(140, 17);
            this.tslStatus.Text = "작업이 완료되었습니다!!!";
            // 
            // UcMainStock1
            // 
            this.UcMainStock1.Location = new System.Drawing.Point(3, 3);
            this.UcMainStock1.Name = "UcMainStock1";
            this.UcMainStock1.OnEventConnect = false;
            this.UcMainStock1.OnReceiveConditionVer = false;
            this.UcMainStock1.OnReceiveTrCondition = false;
            this.UcMainStock1.OnReceiveTrData = false;
            this.UcMainStock1.Size = new System.Drawing.Size(209, 31);
            this.UcMainStock1.TabIndex = 0;
            this.UcMainStock1.OnConnection += new PaikRichStock.Common.ucMainStock.OnConnectionEventHandler(this.UcMainStock1_OnConnection);
            this.UcMainStock1.OnDsBaseInfo += new PaikRichStock.Common.ucMainStock.OnDsBaseInfoEventHandler(this.UcMainStock1_OnDsBaseInfo);
            this.UcMainStock1.OnDayDsBaseInfo += new PaikRichStock.Common.ucMainStock.OnDayDsBaseInfoEventHandler(this.ucMainStock1_OnDayDsBaseInfo);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(168, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(159, 31);
            this.button1.TabIndex = 5;
            this.button1.Text = "키움주식기본정보";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(333, 30);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(159, 31);
            this.button2.TabIndex = 6;
            this.button2.Text = "회시CEO";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(663, 30);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(159, 31);
            this.button3.TabIndex = 7;
            this.button3.Text = "주식마스터";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(498, 30);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(159, 31);
            this.button4.TabIndex = 8;
            this.button4.Text = "회사기본정보";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(3, 67);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(161, 31);
            this.button5.TabIndex = 9;
            this.button5.Text = "일봉미비데이터전송";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(747, 71);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 10;
            this.button6.Text = "button6";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // frmSaver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 287);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btn일봉데이터전송);
            this.Controls.Add(this.UcMainStock1);
            this.Name = "frmSaver";
            this.Text = "일봉데이터전송";
            this.Load += new System.EventHandler(this.frmSaver_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn일봉데이터전송;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslStatus;
        public PaikRichStock.Common.ucMainStock UcMainStock1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
    }
}

