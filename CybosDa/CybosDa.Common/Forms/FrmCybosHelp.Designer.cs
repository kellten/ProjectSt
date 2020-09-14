namespace CybosDa.Common.Forms
{
    partial class FrmCybosHelp
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
            this.splitCon0 = new System.Windows.Forms.SplitContainer();
            this.dgvHelpList = new System.Windows.Forms.DataGridView();
            this.webBrowser0 = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.splitCon0)).BeginInit();
            this.splitCon0.Panel1.SuspendLayout();
            this.splitCon0.Panel2.SuspendLayout();
            this.splitCon0.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHelpList)).BeginInit();
            this.SuspendLayout();
            // 
            // splitCon0
            // 
            this.splitCon0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitCon0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCon0.Location = new System.Drawing.Point(0, 0);
            this.splitCon0.Name = "splitCon0";
            // 
            // splitCon0.Panel1
            // 
            this.splitCon0.Panel1.Controls.Add(this.dgvHelpList);
            // 
            // splitCon0.Panel2
            // 
            this.splitCon0.Panel2.Controls.Add(this.webBrowser0);
            this.splitCon0.Size = new System.Drawing.Size(850, 582);
            this.splitCon0.SplitterDistance = 243;
            this.splitCon0.TabIndex = 0;
            // 
            // dgvHelpList
            // 
            this.dgvHelpList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHelpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHelpList.Location = new System.Drawing.Point(0, 0);
            this.dgvHelpList.Name = "dgvHelpList";
            this.dgvHelpList.RowTemplate.Height = 23;
            this.dgvHelpList.Size = new System.Drawing.Size(239, 578);
            this.dgvHelpList.TabIndex = 0;
            this.dgvHelpList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHelpList_CellDoubleClick);
            // 
            // webBrowser0
            // 
            this.webBrowser0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser0.Location = new System.Drawing.Point(0, 0);
            this.webBrowser0.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser0.Name = "webBrowser0";
            this.webBrowser0.Size = new System.Drawing.Size(599, 578);
            this.webBrowser0.TabIndex = 0;
            // 
            // FrmCybosHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 582);
            this.Controls.Add(this.splitCon0);
            this.Name = "FrmCybosHelp";
            this.Text = "FrmCybosHelp";
            this.splitCon0.Panel1.ResumeLayout(false);
            this.splitCon0.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCon0)).EndInit();
            this.splitCon0.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHelpList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitCon0;
        private System.Windows.Forms.WebBrowser webBrowser0;
        private System.Windows.Forms.DataGridView dgvHelpList;
    }
}