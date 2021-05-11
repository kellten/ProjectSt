namespace Woom.DataAccess.Forms
{
    partial class FrmConnectionInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConnectionInfo));
            this.ucMain0 = new Woom.DataAccess.Uc.ucMain();
            this.notifyIcon0 = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnTray = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.lblLoginStatus = new System.Windows.Forms.Label();
            this.lblMsgTitle = new System.Windows.Forms.Label();
            this.pn0 = new System.Windows.Forms.Panel();
            this.btnPassword = new System.Windows.Forms.Button();
            this.btnLoggerStart = new System.Windows.Forms.Button();
            this.splitCon0 = new System.Windows.Forms.SplitContainer();
            this.lblMsg = new System.Windows.Forms.Label();
            this.pn0.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCon0)).BeginInit();
            this.splitCon0.Panel1.SuspendLayout();
            this.splitCon0.Panel2.SuspendLayout();
            this.splitCon0.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucMain0
            // 
            this.ucMain0.Location = new System.Drawing.Point(482, 130);
            this.ucMain0.Name = "ucMain0";
            this.ucMain0.Size = new System.Drawing.Size(86, 54);
            this.ucMain0.TabIndex = 0;
            this.ucMain0.Visible = false;
            // 
            // notifyIcon0
            // 
            this.notifyIcon0.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon0.Icon")));
            this.notifyIcon0.Text = "notifyIcon1";
            this.notifyIcon0.Visible = true;
            this.notifyIcon0.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon0_MouseDoubleClick);
            // 
            // btnTray
            // 
            this.btnTray.Location = new System.Drawing.Point(281, 2);
            this.btnTray.Name = "btnTray";
            this.btnTray.Size = new System.Drawing.Size(57, 23);
            this.btnTray.TabIndex = 1;
            this.btnTray.Text = "트레이";
            this.btnTray.UseVisualStyleBackColor = true;
            this.btnTray.Click += new System.EventHandler(this.btnTray_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(137, 2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(45, 23);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "접속";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(181, 2);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(62, 23);
            this.btnDisconnect.TabIndex = 8;
            this.btnDisconnect.Text = "로그아웃";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // lblLoginStatus
            // 
            this.lblLoginStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLoginStatus.Location = new System.Drawing.Point(3, 4);
            this.lblLoginStatus.Name = "lblLoginStatus";
            this.lblLoginStatus.Size = new System.Drawing.Size(134, 21);
            this.lblLoginStatus.TabIndex = 6;
            this.lblLoginStatus.Tag = "/D";
            this.lblLoginStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMsgTitle
            // 
            this.lblMsgTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsgTitle.Font = new System.Drawing.Font("굴림체", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMsgTitle.Location = new System.Drawing.Point(-2, 0);
            this.lblMsgTitle.Name = "lblMsgTitle";
            this.lblMsgTitle.Size = new System.Drawing.Size(550, 30);
            this.lblMsgTitle.TabIndex = 9;
            this.lblMsgTitle.Tag = "/D";
            this.lblMsgTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pn0
            // 
            this.pn0.Controls.Add(this.btnTray);
            this.pn0.Controls.Add(this.btnPassword);
            this.pn0.Controls.Add(this.btnLoggerStart);
            this.pn0.Controls.Add(this.btnConnect);
            this.pn0.Controls.Add(this.btnDisconnect);
            this.pn0.Controls.Add(this.lblLoginStatus);
            this.pn0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn0.Location = new System.Drawing.Point(0, 0);
            this.pn0.Name = "pn0";
            this.pn0.Size = new System.Drawing.Size(546, 26);
            this.pn0.TabIndex = 0;
            // 
            // btnPassword
            // 
            this.btnPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPassword.Location = new System.Drawing.Point(-812, 2);
            this.btnPassword.Name = "btnPassword";
            this.btnPassword.Size = new System.Drawing.Size(19, 23);
            this.btnPassword.TabIndex = 11;
            this.btnPassword.Text = "비";
            this.btnPassword.UseVisualStyleBackColor = true;
            // 
            // btnLoggerStart
            // 
            this.btnLoggerStart.Location = new System.Drawing.Point(242, 2);
            this.btnLoggerStart.Name = "btnLoggerStart";
            this.btnLoggerStart.Size = new System.Drawing.Size(40, 23);
            this.btnLoggerStart.TabIndex = 9;
            this.btnLoggerStart.Text = "off";
            this.btnLoggerStart.UseVisualStyleBackColor = true;
            // 
            // splitCon0
            // 
            this.splitCon0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitCon0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCon0.Location = new System.Drawing.Point(0, 0);
            this.splitCon0.Name = "splitCon0";
            this.splitCon0.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitCon0.Panel1
            // 
            this.splitCon0.Panel1.Controls.Add(this.pn0);
            this.splitCon0.Panel1MinSize = 30;
            // 
            // splitCon0.Panel2
            // 
            this.splitCon0.Panel2.Controls.Add(this.lblMsg);
            this.splitCon0.Panel2.Controls.Add(this.lblMsgTitle);
            this.splitCon0.Size = new System.Drawing.Size(550, 176);
            this.splitCon0.SplitterDistance = 30;
            this.splitCon0.TabIndex = 10;
            // 
            // lblMsg
            // 
            this.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsg.Font = new System.Drawing.Font("굴림체", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMsg.Location = new System.Drawing.Point(-2, 30);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(550, 104);
            this.lblMsg.TabIndex = 10;
            this.lblMsg.Tag = "/D";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmConnectionInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 176);
            this.Controls.Add(this.splitCon0);
            this.Controls.Add(this.ucMain0);
            this.Name = "FrmConnectionInfo";
            this.Text = "연결정보(FrmConnectionInfo)";
            this.pn0.ResumeLayout(false);
            this.splitCon0.Panel1.ResumeLayout(false);
            this.splitCon0.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCon0)).EndInit();
            this.splitCon0.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Uc.ucMain ucMain0;
        private System.Windows.Forms.NotifyIcon notifyIcon0;
        private System.Windows.Forms.Button btnTray;
        internal System.Windows.Forms.Button btnConnect;
        internal System.Windows.Forms.Button btnDisconnect;
        internal System.Windows.Forms.Label lblLoginStatus;
        internal System.Windows.Forms.Label lblMsgTitle;
        internal System.Windows.Forms.Panel pn0;
        internal System.Windows.Forms.Button btnPassword;
        public System.Windows.Forms.Button btnLoggerStart;
        private System.Windows.Forms.SplitContainer splitCon0;
        internal System.Windows.Forms.Label lblMsg;
    }
}