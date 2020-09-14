namespace charts175
{
    partial class uc_caCtl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.cbx_X = new System.Windows.Forms.CheckBox();
            this.lbl_X = new System.Windows.Forms.Label();
            this.tr_X = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.cbx_W = new System.Windows.Forms.CheckBox();
            this.lbl_W = new System.Windows.Forms.Label();
            this.tr_W = new System.Windows.Forms.TrackBar();
            this.lbl_CA = new System.Windows.Forms.Label();
            this.cb_refresh = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.clb_cas = new System.Windows.Forms.CheckedListBox();
            this.cbx_Paint = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.tr_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tr_W)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "X %";
            // 
            // cbx_X
            // 
            this.cbx_X.AutoSize = true;
            this.cbx_X.Location = new System.Drawing.Point(47, 46);
            this.cbx_X.Name = "cbx_X";
            this.cbx_X.Size = new System.Drawing.Size(47, 17);
            this.cbx_X.TabIndex = 6;
            this.cbx_X.Text = "auto";
            this.cbx_X.UseVisualStyleBackColor = true;
            this.cbx_X.CheckedChanged += new System.EventHandler(this.cbx_CheckedChanged);
            // 
            // lbl_X
            // 
            this.lbl_X.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_X.AutoSize = true;
            this.lbl_X.Location = new System.Drawing.Point(337, 49);
            this.lbl_X.Name = "lbl_X";
            this.lbl_X.Size = new System.Drawing.Size(25, 13);
            this.lbl_X.TabIndex = 5;
            this.lbl_X.Text = "000";
            // 
            // tr_X
            // 
            this.tr_X.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tr_X.AutoSize = false;
            this.tr_X.Location = new System.Drawing.Point(101, 43);
            this.tr_X.Maximum = 100;
            this.tr_X.Name = "tr_X";
            this.tr_X.Size = new System.Drawing.Size(217, 23);
            this.tr_X.TabIndex = 4;
            this.tr_X.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tr_X.ValueChanged += new System.EventHandler(this.tr_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "W %";
            // 
            // cbx_W
            // 
            this.cbx_W.AutoSize = true;
            this.cbx_W.Location = new System.Drawing.Point(45, 77);
            this.cbx_W.Name = "cbx_W";
            this.cbx_W.Size = new System.Drawing.Size(47, 17);
            this.cbx_W.TabIndex = 10;
            this.cbx_W.Text = "auto";
            this.cbx_W.UseVisualStyleBackColor = true;
            this.cbx_W.CheckedChanged += new System.EventHandler(this.cbx_CheckedChanged);
            // 
            // lbl_W
            // 
            this.lbl_W.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_W.AutoSize = true;
            this.lbl_W.Location = new System.Drawing.Point(335, 80);
            this.lbl_W.Name = "lbl_W";
            this.lbl_W.Size = new System.Drawing.Size(25, 13);
            this.lbl_W.TabIndex = 9;
            this.lbl_W.Text = "000";
            // 
            // tr_W
            // 
            this.tr_W.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tr_W.AutoSize = false;
            this.tr_W.Location = new System.Drawing.Point(100, 74);
            this.tr_W.Maximum = 100;
            this.tr_W.Name = "tr_W";
            this.tr_W.Size = new System.Drawing.Size(217, 23);
            this.tr_W.TabIndex = 8;
            this.tr_W.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tr_W.ValueChanged += new System.EventHandler(this.tr_ValueChanged);
            // 
            // lbl_CA
            // 
            this.lbl_CA.AutoSize = true;
            this.lbl_CA.Location = new System.Drawing.Point(3, 15);
            this.lbl_CA.Name = "lbl_CA";
            this.lbl_CA.Size = new System.Drawing.Size(54, 13);
            this.lbl_CA.TabIndex = 12;
            this.lbl_CA.Text = "ChartArea";
            // 
            // cb_refresh
            // 
            this.cb_refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_refresh.Location = new System.Drawing.Point(335, 9);
            this.cb_refresh.Name = "cb_refresh";
            this.cb_refresh.Size = new System.Drawing.Size(32, 30);
            this.cb_refresh.TabIndex = 14;
            this.cb_refresh.Text = "c";
            this.cb_refresh.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cb_refresh.UseVisualStyleBackColor = true;
            this.cb_refresh.Click += new System.EventHandler(this.cb_refresh_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.clb_cas);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cbx_Paint);
            this.splitContainer1.Panel2.Controls.Add(this.lbl_X);
            this.splitContainer1.Panel2.Controls.Add(this.lbl_CA);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.cbx_W);
            this.splitContainer1.Panel2.Controls.Add(this.cb_refresh);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.lbl_W);
            this.splitContainer1.Panel2.Controls.Add(this.cbx_X);
            this.splitContainer1.Panel2.Controls.Add(this.tr_X);
            this.splitContainer1.Panel2.Controls.Add(this.tr_W);
            this.splitContainer1.Size = new System.Drawing.Size(521, 131);
            this.splitContainer1.SplitterDistance = 134;
            this.splitContainer1.SplitterWidth = 7;
            this.splitContainer1.TabIndex = 15;
            // 
            // clb_cas
            // 
            this.clb_cas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clb_cas.FormattingEnabled = true;
            this.clb_cas.Location = new System.Drawing.Point(0, 0);
            this.clb_cas.Name = "clb_cas";
            this.clb_cas.Size = new System.Drawing.Size(134, 131);
            this.clb_cas.TabIndex = 0;
            this.clb_cas.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clb_cas_ItemCheck);
            this.clb_cas.SelectedIndexChanged += new System.EventHandler(this.clb_cas_SelectedIndexChanged);
            // 
            // cbx_Paint
            // 
            this.cbx_Paint.AutoSize = true;
            this.cbx_Paint.Location = new System.Drawing.Point(14, 109);
            this.cbx_Paint.Name = "cbx_Paint";
            this.cbx_Paint.Size = new System.Drawing.Size(49, 17);
            this.cbx_Paint.TabIndex = 15;
            this.cbx_Paint.Text = "paint";
            this.cbx_Paint.UseVisualStyleBackColor = true;
            this.cbx_Paint.CheckedChanged += new System.EventHandler(this.cbx_Paint_CheckedChanged);
            // 
            // uc_caCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "uc_caCtl";
            this.Size = new System.Drawing.Size(521, 131);
            this.Load += new System.EventHandler(this.uc_caCtl_Load);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.uc_caCtl_Layout);
            ((System.ComponentModel.ISupportInitialize)(this.tr_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tr_W)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbx_X;
        private System.Windows.Forms.Label lbl_X;
        private System.Windows.Forms.TrackBar tr_X;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbx_W;
        private System.Windows.Forms.Label lbl_W;
        private System.Windows.Forms.TrackBar tr_W;
        private System.Windows.Forms.Label lbl_CA;
        private System.Windows.Forms.Button cb_refresh;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckedListBox clb_cas;
        private System.Windows.Forms.CheckBox cbx_Paint;
    }
}
