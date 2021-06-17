namespace Woom.CallForm.Forms
{
    partial class FrmThemaManage
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.txtThemaGroupDesc = new System.Windows.Forms.TextBox();
            this.btnClearThemaGroup = new System.Windows.Forms.Button();
            this.lblThemaGroup = new System.Windows.Forms.Label();
            this.btnThemaGroupStore = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtThemaGroup = new System.Windows.Forms.TextBox();
            this.dgvThemaGroup = new System.Windows.Forms.DataGridView();
            this.THGROUP_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TGPSEQ_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.THGROUP_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.txtThemaDesc = new System.Windows.Forms.TextBox();
            this.btnThemaClear = new System.Windows.Forms.Button();
            this.btnThemaStore = new System.Windows.Forms.Button();
            this.lblThema = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtThema = new System.Windows.Forms.TextBox();
            this.dgvThema = new System.Windows.Forms.DataGridView();
            this.THEMA_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.THGROUP_NAME2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.THEMA_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TGPSEQ_NO2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.THEMA_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.dgvThemaPerStock = new System.Windows.Forms.DataGridView();
            this.ThemaByStock_THEMA_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThemaByStock_STOCK_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThemaByStock_STOCK_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ucStockList1 = new Woom.CallForm.Uc.UcStockList();
            this.tcStock = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.txtStockMemo = new System.Windows.Forms.TextBox();
            this.BtnStoreThemaFromMemo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThemaGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThema)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThemaPerStock)).BeginInit();
            this.tcStock.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer5);
            this.splitContainer1.Size = new System.Drawing.Size(1627, 756);
            this.splitContainer1.SplitterDistance = 450;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(450, 756);
            this.splitContainer2.SplitterDistance = 193;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.txtThemaGroupDesc);
            this.splitContainer4.Panel1.Controls.Add(this.btnClearThemaGroup);
            this.splitContainer4.Panel1.Controls.Add(this.lblThemaGroup);
            this.splitContainer4.Panel1.Controls.Add(this.btnThemaGroupStore);
            this.splitContainer4.Panel1.Controls.Add(this.label1);
            this.splitContainer4.Panel1.Controls.Add(this.txtThemaGroup);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.dgvThemaGroup);
            this.splitContainer4.Size = new System.Drawing.Size(450, 193);
            this.splitContainer4.SplitterDistance = 77;
            this.splitContainer4.TabIndex = 0;
            // 
            // txtThemaGroupDesc
            // 
            this.txtThemaGroupDesc.Location = new System.Drawing.Point(10, 32);
            this.txtThemaGroupDesc.Multiline = true;
            this.txtThemaGroupDesc.Name = "txtThemaGroupDesc";
            this.txtThemaGroupDesc.Size = new System.Drawing.Size(400, 39);
            this.txtThemaGroupDesc.TabIndex = 46;
            // 
            // btnClearThemaGroup
            // 
            this.btnClearThemaGroup.Location = new System.Drawing.Point(334, 7);
            this.btnClearThemaGroup.Name = "btnClearThemaGroup";
            this.btnClearThemaGroup.Size = new System.Drawing.Size(76, 24);
            this.btnClearThemaGroup.TabIndex = 45;
            this.btnClearThemaGroup.Text = "Clear";
            this.btnClearThemaGroup.UseVisualStyleBackColor = true;
            this.btnClearThemaGroup.Click += new System.EventHandler(this.btnClearThemaGroup_Click);
            // 
            // lblThemaGroup
            // 
            this.lblThemaGroup.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblThemaGroup.Location = new System.Drawing.Point(171, 8);
            this.lblThemaGroup.Name = "lblThemaGroup";
            this.lblThemaGroup.Size = new System.Drawing.Size(84, 21);
            this.lblThemaGroup.TabIndex = 44;
            this.lblThemaGroup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnThemaGroupStore
            // 
            this.btnThemaGroupStore.Location = new System.Drawing.Point(256, 7);
            this.btnThemaGroupStore.Name = "btnThemaGroupStore";
            this.btnThemaGroupStore.Size = new System.Drawing.Size(75, 23);
            this.btnThemaGroupStore.TabIndex = 2;
            this.btnThemaGroupStore.Text = "입력(&수정)";
            this.btnThemaGroupStore.UseVisualStyleBackColor = true;
            this.btnThemaGroupStore.Click += new System.EventHandler(this.btnThemaGroupStore_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "테마그룹";
            // 
            // txtThemaGroup
            // 
            this.txtThemaGroup.Location = new System.Drawing.Point(69, 8);
            this.txtThemaGroup.Name = "txtThemaGroup";
            this.txtThemaGroup.Size = new System.Drawing.Size(100, 21);
            this.txtThemaGroup.TabIndex = 0;
            // 
            // dgvThemaGroup
            // 
            this.dgvThemaGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThemaGroup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.THGROUP_NAME,
            this.TGPSEQ_NO,
            this.THGROUP_DESC});
            this.dgvThemaGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvThemaGroup.Location = new System.Drawing.Point(0, 0);
            this.dgvThemaGroup.Name = "dgvThemaGroup";
            this.dgvThemaGroup.RowTemplate.Height = 23;
            this.dgvThemaGroup.Size = new System.Drawing.Size(446, 108);
            this.dgvThemaGroup.TabIndex = 0;
            this.dgvThemaGroup.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThemaGroup_CellDoubleClick);
            // 
            // THGROUP_NAME
            // 
            this.THGROUP_NAME.HeaderText = "테마그룹명";
            this.THGROUP_NAME.Name = "THGROUP_NAME";
            this.THGROUP_NAME.Width = 300;
            // 
            // TGPSEQ_NO
            // 
            this.TGPSEQ_NO.HeaderText = "테마그룹코드";
            this.TGPSEQ_NO.Name = "TGPSEQ_NO";
            // 
            // THGROUP_DESC
            // 
            this.THGROUP_DESC.HeaderText = "테마그룹설명";
            this.THGROUP_DESC.Name = "THGROUP_DESC";
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.txtThemaDesc);
            this.splitContainer3.Panel1.Controls.Add(this.btnThemaClear);
            this.splitContainer3.Panel1.Controls.Add(this.btnThemaStore);
            this.splitContainer3.Panel1.Controls.Add(this.lblThema);
            this.splitContainer3.Panel1.Controls.Add(this.label4);
            this.splitContainer3.Panel1.Controls.Add(this.txtThema);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.dgvThema);
            this.splitContainer3.Size = new System.Drawing.Size(450, 559);
            this.splitContainer3.SplitterDistance = 81;
            this.splitContainer3.TabIndex = 0;
            // 
            // txtThemaDesc
            // 
            this.txtThemaDesc.Location = new System.Drawing.Point(9, 33);
            this.txtThemaDesc.Multiline = true;
            this.txtThemaDesc.Name = "txtThemaDesc";
            this.txtThemaDesc.Size = new System.Drawing.Size(400, 39);
            this.txtThemaDesc.TabIndex = 47;
            // 
            // btnThemaClear
            // 
            this.btnThemaClear.Location = new System.Drawing.Point(315, 5);
            this.btnThemaClear.Name = "btnThemaClear";
            this.btnThemaClear.Size = new System.Drawing.Size(94, 24);
            this.btnThemaClear.TabIndex = 46;
            this.btnThemaClear.Text = "Clear";
            this.btnThemaClear.UseVisualStyleBackColor = true;
            this.btnThemaClear.Click += new System.EventHandler(this.btnThemaClear_Click);
            // 
            // btnThemaStore
            // 
            this.btnThemaStore.Location = new System.Drawing.Point(234, 5);
            this.btnThemaStore.Name = "btnThemaStore";
            this.btnThemaStore.Size = new System.Drawing.Size(75, 23);
            this.btnThemaStore.TabIndex = 45;
            this.btnThemaStore.Text = "입력(&수정)";
            this.btnThemaStore.UseVisualStyleBackColor = true;
            this.btnThemaStore.Click += new System.EventHandler(this.btnThemaStore_Click);
            // 
            // lblThema
            // 
            this.lblThema.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblThema.Location = new System.Drawing.Point(146, 6);
            this.lblThema.Name = "lblThema";
            this.lblThema.Size = new System.Drawing.Size(88, 23);
            this.lblThema.TabIndex = 47;
            this.lblThema.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 46;
            this.label4.Text = "테마";
            // 
            // txtThema
            // 
            this.txtThema.Location = new System.Drawing.Point(44, 6);
            this.txtThema.Name = "txtThema";
            this.txtThema.Size = new System.Drawing.Size(100, 21);
            this.txtThema.TabIndex = 45;
            // 
            // dgvThema
            // 
            this.dgvThema.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThema.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.THEMA_NAME,
            this.THGROUP_NAME2,
            this.THEMA_CODE,
            this.TGPSEQ_NO2,
            this.THEMA_DESC});
            this.dgvThema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvThema.Location = new System.Drawing.Point(0, 0);
            this.dgvThema.Name = "dgvThema";
            this.dgvThema.RowTemplate.Height = 23;
            this.dgvThema.Size = new System.Drawing.Size(446, 470);
            this.dgvThema.TabIndex = 0;
            this.dgvThema.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThema_CellDoubleClick);
            // 
            // THEMA_NAME
            // 
            this.THEMA_NAME.HeaderText = "테마명";
            this.THEMA_NAME.Name = "THEMA_NAME";
            this.THEMA_NAME.Width = 200;
            // 
            // THGROUP_NAME2
            // 
            this.THGROUP_NAME2.HeaderText = "테마그룹명";
            this.THGROUP_NAME2.Name = "THGROUP_NAME2";
            this.THGROUP_NAME2.Width = 150;
            // 
            // THEMA_CODE
            // 
            this.THEMA_CODE.HeaderText = "테마코드";
            this.THEMA_CODE.Name = "THEMA_CODE";
            // 
            // TGPSEQ_NO2
            // 
            this.TGPSEQ_NO2.HeaderText = "테마그룹코드";
            this.TGPSEQ_NO2.Name = "TGPSEQ_NO2";
            // 
            // THEMA_DESC
            // 
            this.THEMA_DESC.HeaderText = "테마설명";
            this.THEMA_DESC.Name = "THEMA_DESC";
            // 
            // splitContainer5
            // 
            this.splitContainer5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.dgvThemaPerStock);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.tcStock);
            this.splitContainer5.Size = new System.Drawing.Size(1173, 756);
            this.splitContainer5.SplitterDistance = 873;
            this.splitContainer5.TabIndex = 0;
            // 
            // dgvThemaPerStock
            // 
            this.dgvThemaPerStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThemaPerStock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ThemaByStock_THEMA_NAME,
            this.ThemaByStock_STOCK_NAME,
            this.ThemaByStock_STOCK_CODE});
            this.dgvThemaPerStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvThemaPerStock.Location = new System.Drawing.Point(0, 0);
            this.dgvThemaPerStock.Name = "dgvThemaPerStock";
            this.dgvThemaPerStock.RowTemplate.Height = 23;
            this.dgvThemaPerStock.Size = new System.Drawing.Size(871, 754);
            this.dgvThemaPerStock.TabIndex = 0;
            this.dgvThemaPerStock.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThemaPerStock_CellClick);
            this.dgvThemaPerStock.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvThemaPerStock_KeyDown);
            // 
            // ThemaByStock_THEMA_NAME
            // 
            this.ThemaByStock_THEMA_NAME.HeaderText = "테마명";
            this.ThemaByStock_THEMA_NAME.Name = "ThemaByStock_THEMA_NAME";
            this.ThemaByStock_THEMA_NAME.Visible = false;
            this.ThemaByStock_THEMA_NAME.Width = 200;
            // 
            // ThemaByStock_STOCK_NAME
            // 
            this.ThemaByStock_STOCK_NAME.HeaderText = "종목명";
            this.ThemaByStock_STOCK_NAME.Name = "ThemaByStock_STOCK_NAME";
            this.ThemaByStock_STOCK_NAME.Width = 200;
            // 
            // ThemaByStock_STOCK_CODE
            // 
            this.ThemaByStock_STOCK_CODE.HeaderText = "종목코드";
            this.ThemaByStock_STOCK_CODE.Name = "ThemaByStock_STOCK_CODE";
            // 
            // ucStockList1
            // 
            this.ucStockList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucStockList1.Location = new System.Drawing.Point(3, 3);
            this.ucStockList1.Name = "ucStockList1";
            this.ucStockList1.Size = new System.Drawing.Size(282, 724);
            this.ucStockList1.TabIndex = 0;
            // 
            // tcStock
            // 
            this.tcStock.Controls.Add(this.tabPage1);
            this.tcStock.Controls.Add(this.tabPage2);
            this.tcStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcStock.Location = new System.Drawing.Point(0, 0);
            this.tcStock.Name = "tcStock";
            this.tcStock.SelectedIndex = 0;
            this.tcStock.Size = new System.Drawing.Size(294, 754);
            this.tcStock.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ucStockList1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(288, 730);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "종목전체";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(286, 728);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "메모종목";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer6
            // 
            this.splitContainer6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.Location = new System.Drawing.Point(3, 3);
            this.splitContainer6.Name = "splitContainer6";
            this.splitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.BtnStoreThemaFromMemo);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.txtStockMemo);
            this.splitContainer6.Size = new System.Drawing.Size(280, 722);
            this.splitContainer6.SplitterDistance = 40;
            this.splitContainer6.TabIndex = 0;
            // 
            // txtStockMemo
            // 
            this.txtStockMemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStockMemo.Location = new System.Drawing.Point(0, 0);
            this.txtStockMemo.Multiline = true;
            this.txtStockMemo.Name = "txtStockMemo";
            this.txtStockMemo.Size = new System.Drawing.Size(278, 676);
            this.txtStockMemo.TabIndex = 0;
            // 
            // BtnStoreThemaFromMemo
            // 
            this.BtnStoreThemaFromMemo.Location = new System.Drawing.Point(3, 7);
            this.BtnStoreThemaFromMemo.Name = "BtnStoreThemaFromMemo";
            this.BtnStoreThemaFromMemo.Size = new System.Drawing.Size(80, 23);
            this.BtnStoreThemaFromMemo.TabIndex = 0;
            this.BtnStoreThemaFromMemo.Text = "저장";
            this.BtnStoreThemaFromMemo.UseVisualStyleBackColor = true;
            this.BtnStoreThemaFromMemo.Click += new System.EventHandler(this.BtnStoreThemaFromMemo_Click);
            // 
            // FrmThemaManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1627, 756);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmThemaManage";
            this.Text = "테마관리(FrmThemaManage)";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThemaGroup)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThema)).EndInit();
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThemaPerStock)).EndInit();
            this.tcStock.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel2.ResumeLayout(false);
            this.splitContainer6.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
            this.splitContainer6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgvThemaGroup;
        private System.Windows.Forms.DataGridView dgvThemaPerStock;
        private System.Windows.Forms.DataGridView dgvThema;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Button btnThemaGroupStore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtThemaGroup;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label lblThemaGroup;
        private System.Windows.Forms.Label lblThema;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtThema;
        private System.Windows.Forms.Button btnClearThemaGroup;
        private System.Windows.Forms.Button btnThemaClear;
        private System.Windows.Forms.Button btnThemaStore;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private Uc.UcStockList ucStockList1;
        private System.Windows.Forms.TextBox txtThemaGroupDesc;
        private System.Windows.Forms.TextBox txtThemaDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn THGROUP_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn TGPSEQ_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn THEMA_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn THGROUP_NAME2;
        private System.Windows.Forms.DataGridViewTextBoxColumn THEMA_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TGPSEQ_NO2;
        private System.Windows.Forms.DataGridViewTextBoxColumn THEMA_DESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn THGROUP_DESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThemaByStock_THEMA_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThemaByStock_STOCK_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThemaByStock_STOCK_CODE;
        private System.Windows.Forms.TabControl tcStock;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer6;
        private System.Windows.Forms.Button BtnStoreThemaFromMemo;
        private System.Windows.Forms.TextBox txtStockMemo;
    }
}