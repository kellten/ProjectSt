﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucHogaWindowNew
    Inherits System.Windows.Forms.UserControl

    'UserControl은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.tpBuy = New System.Windows.Forms.TabPage()
        Me.cboBuyTradeGb = New System.Windows.Forms.ComboBox()
        Me.numBuyPrice = New System.Windows.Forms.NumericUpDown()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.btnBuy = New System.Windows.Forms.Button()
        Me.chkBuyCurrentPrice = New System.Windows.Forms.CheckBox()
        Me.numBuyQty = New System.Windows.Forms.NumericUpDown()
        Me.tpSell = New System.Windows.Forms.TabPage()
        Me.chkSellAvaQty = New System.Windows.Forms.CheckBox()
        Me.cboSellTradeGb = New System.Windows.Forms.ComboBox()
        Me.numSellPrice = New System.Windows.Forms.NumericUpDown()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.btnSell = New System.Windows.Forms.Button()
        Me.chkSellCurrentPrice = New System.Windows.Forms.CheckBox()
        Me.numSellQty = New System.Windows.Forms.NumericUpDown()
        Me.lblStockCode = New System.Windows.Forms.Label()
        Me.dgvTradeAt = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblStockName = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.lblPreDayPrice = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblStartPrice = New System.Windows.Forms.Label()
        Me.lblUpDownRate = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblHighestPrice = New System.Windows.Forms.Label()
        Me.lblTradingQty = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblLowestPrice = New System.Windows.Forms.Label()
        Me.lblCurrentPrice = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboAccount = New System.Windows.Forms.ComboBox()
        Me.pnTrade = New System.Windows.Forms.Panel()
        Me.tbTrade = New System.Windows.Forms.TabControl()
        Me.tpModify = New System.Windows.Forms.TabPage()
        Me.btn취소 = New System.Windows.Forms.Button()
        Me.txtGb = New System.Windows.Forms.TextBox()
        Me.txtisSellBuy = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtOrderNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnMi = New System.Windows.Forms.Button()
        Me.numModPrice = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btn정정 = New System.Windows.Forms.Button()
        Me.chkModCurrentPrice = New System.Windows.Forms.CheckBox()
        Me.numModQty = New System.Windows.Forms.NumericUpDown()
        Me.dgvSell = New System.Windows.Forms.DataGridView()
        Me.Percent_Sell = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SellQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SellPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvBuy = New System.Windows.Forms.DataGridView()
        Me.Percent_buy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvTrading = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dg미체결 = New System.Windows.Forms.DataGridView()
        Me.txtModStockCode = New System.Windows.Forms.TextBox()
        Me.txtModStockName = New System.Windows.Forms.TextBox()
        Me.tpBuy.SuspendLayout()
        CType(Me.numBuyPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numBuyQty, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpSell.SuspendLayout()
        CType(Me.numSellPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numSellQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvTradeAt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.pnTrade.SuspendLayout()
        Me.tbTrade.SuspendLayout()
        Me.tpModify.SuspendLayout()
        CType(Me.numModPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numModQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSell, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvBuy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvTrading, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dg미체결, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tpBuy
        '
        Me.tpBuy.BackColor = System.Drawing.Color.OldLace
        Me.tpBuy.Controls.Add(Me.cboBuyTradeGb)
        Me.tpBuy.Controls.Add(Me.numBuyPrice)
        Me.tpBuy.Controls.Add(Me.Label15)
        Me.tpBuy.Controls.Add(Me.Label17)
        Me.tpBuy.Controls.Add(Me.btnBuy)
        Me.tpBuy.Controls.Add(Me.chkBuyCurrentPrice)
        Me.tpBuy.Controls.Add(Me.numBuyQty)
        Me.tpBuy.Location = New System.Drawing.Point(4, 24)
        Me.tpBuy.Name = "tpBuy"
        Me.tpBuy.Padding = New System.Windows.Forms.Padding(3)
        Me.tpBuy.Size = New System.Drawing.Size(208, 111)
        Me.tpBuy.TabIndex = 0
        Me.tpBuy.Text = "매수"
        '
        'cboBuyTradeGb
        '
        Me.cboBuyTradeGb.FormattingEnabled = True
        Me.cboBuyTradeGb.Items.AddRange(New Object() {"00 - 지정가", "03 - 시장가", "05 - 조건부지정가", "06 - 최유리지정가", "07 - 최우선지정가", "10 - 지정가IOC", "13 - 시장가IOC", "16 - 최유리IOC", "20 - 지정가FOK", "23 - 시장가FOK", "26 - 최유리FOK", "61 - 장전시간외종가", "62 - 시간외단일가매매", "81 - 장후시간외종가"})
        Me.cboBuyTradeGb.Location = New System.Drawing.Point(6, 7)
        Me.cboBuyTradeGb.Name = "cboBuyTradeGb"
        Me.cboBuyTradeGb.Size = New System.Drawing.Size(121, 19)
        Me.cboBuyTradeGb.TabIndex = 354
        Me.cboBuyTradeGb.Text = "00 - 지정가"
        '
        'numBuyPrice
        '
        Me.numBuyPrice.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.numBuyPrice.Location = New System.Drawing.Point(39, 54)
        Me.numBuyPrice.Maximum = New Decimal(New Integer() {10000000, 0, 0, 0})
        Me.numBuyPrice.Name = "numBuyPrice"
        Me.numBuyPrice.Size = New System.Drawing.Size(95, 21)
        Me.numBuyPrice.TabIndex = 5
        Me.numBuyPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numBuyPrice.ThousandsSeparator = True
        Me.numBuyPrice.Value = New Decimal(New Integer() {10000000, 0, 0, 0})
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label15.Location = New System.Drawing.Point(3, 35)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(31, 12)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "수량"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label17.Location = New System.Drawing.Point(3, 58)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(31, 12)
        Me.Label17.TabIndex = 1
        Me.Label17.Text = "가격"
        '
        'btnBuy
        '
        Me.btnBuy.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.btnBuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBuy.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnBuy.ForeColor = System.Drawing.Color.White
        Me.btnBuy.Location = New System.Drawing.Point(137, 27)
        Me.btnBuy.Name = "btnBuy"
        Me.btnBuy.Size = New System.Drawing.Size(65, 67)
        Me.btnBuy.TabIndex = 4
        Me.btnBuy.Text = "현금매수"
        Me.btnBuy.UseVisualStyleBackColor = False
        '
        'chkBuyCurrentPrice
        '
        Me.chkBuyCurrentPrice.AutoSize = True
        Me.chkBuyCurrentPrice.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkBuyCurrentPrice.Location = New System.Drawing.Point(3, 78)
        Me.chkBuyCurrentPrice.Name = "chkBuyCurrentPrice"
        Me.chkBuyCurrentPrice.Size = New System.Drawing.Size(103, 16)
        Me.chkBuyCurrentPrice.TabIndex = 2
        Me.chkBuyCurrentPrice.Text = "자동(현재가)"
        Me.chkBuyCurrentPrice.UseVisualStyleBackColor = True
        '
        'numBuyQty
        '
        Me.numBuyQty.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.numBuyQty.Location = New System.Drawing.Point(39, 32)
        Me.numBuyQty.Maximum = New Decimal(New Integer() {10000000, 0, 0, 0})
        Me.numBuyQty.Name = "numBuyQty"
        Me.numBuyQty.Size = New System.Drawing.Size(95, 21)
        Me.numBuyQty.TabIndex = 3
        Me.numBuyQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numBuyQty.ThousandsSeparator = True
        '
        'tpSell
        '
        Me.tpSell.BackColor = System.Drawing.Color.LightSteelBlue
        Me.tpSell.Controls.Add(Me.chkSellAvaQty)
        Me.tpSell.Controls.Add(Me.cboSellTradeGb)
        Me.tpSell.Controls.Add(Me.numSellPrice)
        Me.tpSell.Controls.Add(Me.Label16)
        Me.tpSell.Controls.Add(Me.Label18)
        Me.tpSell.Controls.Add(Me.btnSell)
        Me.tpSell.Controls.Add(Me.chkSellCurrentPrice)
        Me.tpSell.Controls.Add(Me.numSellQty)
        Me.tpSell.Location = New System.Drawing.Point(4, 24)
        Me.tpSell.Name = "tpSell"
        Me.tpSell.Padding = New System.Windows.Forms.Padding(3)
        Me.tpSell.Size = New System.Drawing.Size(208, 111)
        Me.tpSell.TabIndex = 1
        Me.tpSell.Text = "매도"
        '
        'chkSellAvaQty
        '
        Me.chkSellAvaQty.AutoSize = True
        Me.chkSellAvaQty.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkSellAvaQty.Location = New System.Drawing.Point(6, 53)
        Me.chkSellAvaQty.Name = "chkSellAvaQty"
        Me.chkSellAvaQty.Size = New System.Drawing.Size(50, 16)
        Me.chkSellAvaQty.TabIndex = 355
        Me.chkSellAvaQty.Text = "가능"
        Me.chkSellAvaQty.UseVisualStyleBackColor = True
        '
        'cboSellTradeGb
        '
        Me.cboSellTradeGb.FormattingEnabled = True
        Me.cboSellTradeGb.Items.AddRange(New Object() {"00 - 지정가", "03 - 시장가", "05 - 조건부지정가", "06 - 최유리지정가", "07 - 최우선지정가", "10 - 지정가IOC", "13 - 시장가IOC", "16 - 최유리IOC", "20 - 지정가FOK", "23 - 시장가FOK", "26 - 최유리FOK", "61 - 장전시간외종가", "62 - 시간외단일가매매", "81 - 장후시간외종가"})
        Me.cboSellTradeGb.Location = New System.Drawing.Point(5, 7)
        Me.cboSellTradeGb.Name = "cboSellTradeGb"
        Me.cboSellTradeGb.Size = New System.Drawing.Size(121, 19)
        Me.cboSellTradeGb.TabIndex = 354
        Me.cboSellTradeGb.Text = "00 - 지정가"
        '
        'numSellPrice
        '
        Me.numSellPrice.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.numSellPrice.Location = New System.Drawing.Point(39, 69)
        Me.numSellPrice.Maximum = New Decimal(New Integer() {10000000, 0, 0, 0})
        Me.numSellPrice.Name = "numSellPrice"
        Me.numSellPrice.Size = New System.Drawing.Size(95, 21)
        Me.numSellPrice.TabIndex = 11
        Me.numSellPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numSellPrice.ThousandsSeparator = True
        Me.numSellPrice.Value = New Decimal(New Integer() {10000000, 0, 0, 0})
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label16.Location = New System.Drawing.Point(3, 35)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(31, 12)
        Me.Label16.TabIndex = 6
        Me.Label16.Text = "수량"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label18.Location = New System.Drawing.Point(3, 73)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(31, 12)
        Me.Label18.TabIndex = 7
        Me.Label18.Text = "가격"
        '
        'btnSell
        '
        Me.btnSell.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btnSell.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSell.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnSell.ForeColor = System.Drawing.Color.White
        Me.btnSell.Location = New System.Drawing.Point(137, 27)
        Me.btnSell.Name = "btnSell"
        Me.btnSell.Size = New System.Drawing.Size(65, 67)
        Me.btnSell.TabIndex = 10
        Me.btnSell.Text = "현금매도"
        Me.btnSell.UseVisualStyleBackColor = False
        '
        'chkSellCurrentPrice
        '
        Me.chkSellCurrentPrice.AutoSize = True
        Me.chkSellCurrentPrice.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkSellCurrentPrice.Location = New System.Drawing.Point(3, 93)
        Me.chkSellCurrentPrice.Name = "chkSellCurrentPrice"
        Me.chkSellCurrentPrice.Size = New System.Drawing.Size(103, 16)
        Me.chkSellCurrentPrice.TabIndex = 8
        Me.chkSellCurrentPrice.Text = "자동(현재가)"
        Me.chkSellCurrentPrice.UseVisualStyleBackColor = True
        '
        'numSellQty
        '
        Me.numSellQty.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.numSellQty.Location = New System.Drawing.Point(39, 32)
        Me.numSellQty.Maximum = New Decimal(New Integer() {10000000, 0, 0, 0})
        Me.numSellQty.Name = "numSellQty"
        Me.numSellQty.Size = New System.Drawing.Size(95, 21)
        Me.numSellQty.TabIndex = 9
        Me.numSellQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numSellQty.ThousandsSeparator = True
        '
        'lblStockCode
        '
        Me.lblStockCode.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblStockCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStockCode.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblStockCode.Font = New System.Drawing.Font("굴림체", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblStockCode.ForeColor = System.Drawing.Color.White
        Me.lblStockCode.Location = New System.Drawing.Point(148, 0)
        Me.lblStockCode.Name = "lblStockCode"
        Me.lblStockCode.Size = New System.Drawing.Size(67, 24)
        Me.lblStockCode.TabIndex = 375
        Me.lblStockCode.Tag = "/D"
        Me.lblStockCode.Text = "111111"
        Me.lblStockCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dgvTradeAt
        '
        Me.dgvTradeAt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTradeAt.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6})
        Me.dgvTradeAt.Location = New System.Drawing.Point(2, 607)
        Me.dgvTradeAt.Name = "dgvTradeAt"
        Me.dgvTradeAt.RowHeadersVisible = False
        Me.dgvTradeAt.RowTemplate.Height = 23
        Me.dgvTradeAt.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.dgvTradeAt.Size = New System.Drawing.Size(444, 169)
        Me.dgvTradeAt.TabIndex = 387
        '
        'Column1
        '
        Me.Column1.HeaderText = "증감"
        Me.Column1.Name = "Column1"
        Me.Column1.Width = 30
        '
        'Column2
        '
        Me.Column2.HeaderText = "수량"
        Me.Column2.Name = "Column2"
        Me.Column2.Width = 110
        '
        'Column3
        '
        Me.Column3.HeaderText = "매도거래원"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 80
        '
        'Column4
        '
        Me.Column4.HeaderText = "매수거래원"
        Me.Column4.Name = "Column4"
        Me.Column4.Width = 80
        '
        'Column5
        '
        Me.Column5.HeaderText = "수량"
        Me.Column5.Name = "Column5"
        Me.Column5.Width = 110
        '
        'Column6
        '
        Me.Column6.HeaderText = "증감"
        Me.Column6.Name = "Column6"
        Me.Column6.Width = 30
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.lblStockName)
        Me.Panel1.Controls.Add(Me.label1)
        Me.Panel1.Controls.Add(Me.lblPreDayPrice)
        Me.Panel1.Controls.Add(Me.lblStockCode)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.lblStartPrice)
        Me.Panel1.Controls.Add(Me.lblUpDownRate)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.lblHighestPrice)
        Me.Panel1.Controls.Add(Me.lblTradingQty)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.lblLowestPrice)
        Me.Panel1.Controls.Add(Me.lblCurrentPrice)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Location = New System.Drawing.Point(223, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(219, 183)
        Me.Panel1.TabIndex = 386
        '
        'lblStockName
        '
        Me.lblStockName.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblStockName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStockName.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblStockName.Font = New System.Drawing.Font("굴림체", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblStockName.ForeColor = System.Drawing.Color.White
        Me.lblStockName.Location = New System.Drawing.Point(-2, 0)
        Me.lblStockName.Name = "lblStockName"
        Me.lblStockName.Size = New System.Drawing.Size(150, 24)
        Me.lblStockName.TabIndex = 359
        Me.lblStockName.Tag = "/D"
        Me.lblStockName.Text = "111111"
        Me.lblStockName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'label1
        '
        Me.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.label1.ForeColor = System.Drawing.Color.Black
        Me.label1.Location = New System.Drawing.Point(-1, 24)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(103, 22)
        Me.label1.TabIndex = 360
        Me.label1.Tag = "/D"
        Me.label1.Text = "전일가"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPreDayPrice
        '
        Me.lblPreDayPrice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPreDayPrice.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblPreDayPrice.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPreDayPrice.ForeColor = System.Drawing.Color.Black
        Me.lblPreDayPrice.Location = New System.Drawing.Point(100, 24)
        Me.lblPreDayPrice.Name = "lblPreDayPrice"
        Me.lblPreDayPrice.Size = New System.Drawing.Size(115, 22)
        Me.lblPreDayPrice.TabIndex = 361
        Me.lblPreDayPrice.Tag = "/D"
        Me.lblPreDayPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(-1, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(103, 22)
        Me.Label4.TabIndex = 362
        Me.Label4.Tag = "/D"
        Me.Label4.Text = "시가"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStartPrice
        '
        Me.lblStartPrice.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.lblStartPrice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStartPrice.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblStartPrice.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblStartPrice.ForeColor = System.Drawing.Color.Red
        Me.lblStartPrice.Location = New System.Drawing.Point(100, 46)
        Me.lblStartPrice.Name = "lblStartPrice"
        Me.lblStartPrice.Size = New System.Drawing.Size(115, 22)
        Me.lblStartPrice.TabIndex = 363
        Me.lblStartPrice.Tag = "/D"
        Me.lblStartPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblUpDownRate
        '
        Me.lblUpDownRate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUpDownRate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblUpDownRate.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUpDownRate.ForeColor = System.Drawing.Color.Red
        Me.lblUpDownRate.Location = New System.Drawing.Point(100, 156)
        Me.lblUpDownRate.Name = "lblUpDownRate"
        Me.lblUpDownRate.Size = New System.Drawing.Size(115, 22)
        Me.lblUpDownRate.TabIndex = 373
        Me.lblUpDownRate.Tag = "/D"
        Me.lblUpDownRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label6.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label6.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(-1, 68)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(103, 22)
        Me.Label6.TabIndex = 364
        Me.Label6.Tag = "/D"
        Me.Label6.Text = "고가"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label14.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label14.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(-1, 156)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(103, 22)
        Me.Label14.TabIndex = 372
        Me.Label14.Tag = "/D"
        Me.Label14.Text = "등락률"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblHighestPrice
        '
        Me.lblHighestPrice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblHighestPrice.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblHighestPrice.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblHighestPrice.ForeColor = System.Drawing.Color.Red
        Me.lblHighestPrice.Location = New System.Drawing.Point(100, 68)
        Me.lblHighestPrice.Name = "lblHighestPrice"
        Me.lblHighestPrice.Size = New System.Drawing.Size(115, 22)
        Me.lblHighestPrice.TabIndex = 365
        Me.lblHighestPrice.Tag = "/D"
        Me.lblHighestPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTradingQty
        '
        Me.lblTradingQty.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.lblTradingQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTradingQty.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblTradingQty.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTradingQty.Location = New System.Drawing.Point(100, 134)
        Me.lblTradingQty.Name = "lblTradingQty"
        Me.lblTradingQty.Size = New System.Drawing.Size(115, 22)
        Me.lblTradingQty.TabIndex = 371
        Me.lblTradingQty.Tag = "/D"
        Me.lblTradingQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label8.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label8.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(-1, 90)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(103, 22)
        Me.Label8.TabIndex = 366
        Me.Label8.Tag = "/D"
        Me.Label8.Text = "저가"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label12.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label12.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(-1, 134)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(103, 22)
        Me.Label12.TabIndex = 370
        Me.Label12.Tag = "/D"
        Me.Label12.Text = "거래량"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLowestPrice
        '
        Me.lblLowestPrice.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.lblLowestPrice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLowestPrice.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblLowestPrice.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblLowestPrice.ForeColor = System.Drawing.Color.Blue
        Me.lblLowestPrice.Location = New System.Drawing.Point(100, 90)
        Me.lblLowestPrice.Name = "lblLowestPrice"
        Me.lblLowestPrice.Size = New System.Drawing.Size(115, 22)
        Me.lblLowestPrice.TabIndex = 367
        Me.lblLowestPrice.Tag = "/D"
        Me.lblLowestPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCurrentPrice
        '
        Me.lblCurrentPrice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCurrentPrice.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblCurrentPrice.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCurrentPrice.ForeColor = System.Drawing.Color.Red
        Me.lblCurrentPrice.Location = New System.Drawing.Point(100, 112)
        Me.lblCurrentPrice.Name = "lblCurrentPrice"
        Me.lblCurrentPrice.Size = New System.Drawing.Size(115, 22)
        Me.lblCurrentPrice.TabIndex = 369
        Me.lblCurrentPrice.Tag = "/D"
        Me.lblCurrentPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label10.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label10.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(-1, 112)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(103, 22)
        Me.Label10.TabIndex = 368
        Me.Label10.Tag = "/D"
        Me.Label10.Text = "현재가"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.GhostWhite
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Location = New System.Drawing.Point(4, 439)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 21)
        Me.Label2.TabIndex = 385
        Me.Label2.Text = "계좌번호"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboAccount
        '
        Me.cboAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAccount.FormattingEnabled = True
        Me.cboAccount.Location = New System.Drawing.Point(79, 439)
        Me.cboAccount.Name = "cboAccount"
        Me.cboAccount.Size = New System.Drawing.Size(90, 19)
        Me.cboAccount.TabIndex = 384
        '
        'pnTrade
        '
        Me.pnTrade.BackColor = System.Drawing.Color.MistyRose
        Me.pnTrade.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnTrade.Controls.Add(Me.tbTrade)
        Me.pnTrade.Location = New System.Drawing.Point(0, 465)
        Me.pnTrade.Name = "pnTrade"
        Me.pnTrade.Size = New System.Drawing.Size(220, 143)
        Me.pnTrade.TabIndex = 383
        '
        'tbTrade
        '
        Me.tbTrade.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.tbTrade.Controls.Add(Me.tpBuy)
        Me.tbTrade.Controls.Add(Me.tpSell)
        Me.tbTrade.Controls.Add(Me.tpModify)
        Me.tbTrade.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbTrade.ItemSize = New System.Drawing.Size(95, 20)
        Me.tbTrade.Location = New System.Drawing.Point(0, 0)
        Me.tbTrade.Name = "tbTrade"
        Me.tbTrade.SelectedIndex = 0
        Me.tbTrade.Size = New System.Drawing.Size(216, 139)
        Me.tbTrade.TabIndex = 0
        '
        'tpModify
        '
        Me.tpModify.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.tpModify.Controls.Add(Me.txtModStockName)
        Me.tpModify.Controls.Add(Me.txtModStockCode)
        Me.tpModify.Controls.Add(Me.btn취소)
        Me.tpModify.Controls.Add(Me.txtGb)
        Me.tpModify.Controls.Add(Me.txtisSellBuy)
        Me.tpModify.Controls.Add(Me.Label9)
        Me.tpModify.Controls.Add(Me.txtOrderNo)
        Me.tpModify.Controls.Add(Me.Label7)
        Me.tpModify.Controls.Add(Me.btnMi)
        Me.tpModify.Controls.Add(Me.numModPrice)
        Me.tpModify.Controls.Add(Me.Label3)
        Me.tpModify.Controls.Add(Me.Label5)
        Me.tpModify.Controls.Add(Me.btn정정)
        Me.tpModify.Controls.Add(Me.chkModCurrentPrice)
        Me.tpModify.Controls.Add(Me.numModQty)
        Me.tpModify.Location = New System.Drawing.Point(4, 24)
        Me.tpModify.Name = "tpModify"
        Me.tpModify.Size = New System.Drawing.Size(208, 111)
        Me.tpModify.TabIndex = 2
        Me.tpModify.Text = "정정"
        '
        'btn취소
        '
        Me.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn취소.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn취소.ForeColor = System.Drawing.Color.Red
        Me.btn취소.Location = New System.Drawing.Point(139, 82)
        Me.btn취소.Name = "btn취소"
        Me.btn취소.Size = New System.Drawing.Size(65, 26)
        Me.btn취소.TabIndex = 368
        Me.btn취소.Text = "취소"
        Me.btn취소.UseVisualStyleBackColor = False
        '
        'txtGb
        '
        Me.txtGb.Location = New System.Drawing.Point(41, 26)
        Me.txtGb.Name = "txtGb"
        Me.txtGb.ReadOnly = True
        Me.txtGb.Size = New System.Drawing.Size(53, 20)
        Me.txtGb.TabIndex = 367
        '
        'txtisSellBuy
        '
        Me.txtisSellBuy.Location = New System.Drawing.Point(139, 4)
        Me.txtisSellBuy.Name = "txtisSellBuy"
        Me.txtisSellBuy.ReadOnly = True
        Me.txtisSellBuy.Size = New System.Drawing.Size(41, 20)
        Me.txtisSellBuy.TabIndex = 366
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label9.Location = New System.Drawing.Point(4, 31)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(31, 12)
        Me.Label9.TabIndex = 365
        Me.Label9.Text = "구분"
        '
        'txtOrderNo
        '
        Me.txtOrderNo.Location = New System.Drawing.Point(73, 4)
        Me.txtOrderNo.Name = "txtOrderNo"
        Me.txtOrderNo.ReadOnly = True
        Me.txtOrderNo.Size = New System.Drawing.Size(63, 20)
        Me.txtOrderNo.TabIndex = 364
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.Location = New System.Drawing.Point(4, 8)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 12)
        Me.Label7.TabIndex = 363
        Me.Label7.Text = "원주문번호"
        '
        'btnMi
        '
        Me.btnMi.Location = New System.Drawing.Point(182, 3)
        Me.btnMi.Name = "btnMi"
        Me.btnMi.Size = New System.Drawing.Size(23, 23)
        Me.btnMi.TabIndex = 362
        Me.btnMi.Text = "미체결"
        Me.btnMi.UseVisualStyleBackColor = True
        '
        'numModPrice
        '
        Me.numModPrice.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.numModPrice.Location = New System.Drawing.Point(41, 68)
        Me.numModPrice.Maximum = New Decimal(New Integer() {10000000, 0, 0, 0})
        Me.numModPrice.Name = "numModPrice"
        Me.numModPrice.Size = New System.Drawing.Size(95, 21)
        Me.numModPrice.TabIndex = 360
        Me.numModPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numModPrice.ThousandsSeparator = True
        Me.numModPrice.Value = New Decimal(New Integer() {10000000, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.Location = New System.Drawing.Point(5, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 12)
        Me.Label3.TabIndex = 355
        Me.Label3.Text = "수량"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.Location = New System.Drawing.Point(5, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(31, 12)
        Me.Label5.TabIndex = 356
        Me.Label5.Text = "가격"
        '
        'btn정정
        '
        Me.btn정정.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn정정.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn정정.ForeColor = System.Drawing.Color.Blue
        Me.btn정정.Location = New System.Drawing.Point(139, 53)
        Me.btn정정.Name = "btn정정"
        Me.btn정정.Size = New System.Drawing.Size(65, 27)
        Me.btn정정.TabIndex = 359
        Me.btn정정.Text = "정정"
        Me.btn정정.UseVisualStyleBackColor = False
        '
        'chkModCurrentPrice
        '
        Me.chkModCurrentPrice.AutoSize = True
        Me.chkModCurrentPrice.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkModCurrentPrice.Location = New System.Drawing.Point(5, 92)
        Me.chkModCurrentPrice.Name = "chkModCurrentPrice"
        Me.chkModCurrentPrice.Size = New System.Drawing.Size(103, 16)
        Me.chkModCurrentPrice.TabIndex = 357
        Me.chkModCurrentPrice.Text = "자동(현재가)"
        Me.chkModCurrentPrice.UseVisualStyleBackColor = True
        '
        'numModQty
        '
        Me.numModQty.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.numModQty.Location = New System.Drawing.Point(41, 46)
        Me.numModQty.Maximum = New Decimal(New Integer() {10000000, 0, 0, 0})
        Me.numModQty.Name = "numModQty"
        Me.numModQty.Size = New System.Drawing.Size(95, 21)
        Me.numModQty.TabIndex = 358
        Me.numModQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numModQty.ThousandsSeparator = True
        '
        'dgvSell
        '
        Me.dgvSell.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSell.ColumnHeadersVisible = False
        Me.dgvSell.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Percent_Sell, Me.SellQty, Me.SellPrice})
        Me.dgvSell.Location = New System.Drawing.Point(3, 0)
        Me.dgvSell.Name = "dgvSell"
        Me.dgvSell.ReadOnly = True
        Me.dgvSell.RowHeadersVisible = False
        Me.dgvSell.RowTemplate.Height = 20
        Me.dgvSell.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.dgvSell.Size = New System.Drawing.Size(220, 230)
        Me.dgvSell.TabIndex = 380
        '
        'Percent_Sell
        '
        Me.Percent_Sell.HeaderText = "Percent_Sell"
        Me.Percent_Sell.Name = "Percent_Sell"
        Me.Percent_Sell.ReadOnly = True
        Me.Percent_Sell.Visible = False
        '
        'SellQty
        '
        Me.SellQty.HeaderText = "매도호가별거래량"
        Me.SellQty.Name = "SellQty"
        Me.SellQty.ReadOnly = True
        Me.SellQty.Width = 150
        '
        'SellPrice
        '
        Me.SellPrice.HeaderText = "매도가"
        Me.SellPrice.Name = "SellPrice"
        Me.SellPrice.ReadOnly = True
        Me.SellPrice.Width = 70
        '
        'dgvBuy
        '
        Me.dgvBuy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBuy.ColumnHeadersVisible = False
        Me.dgvBuy.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Percent_buy, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn1})
        Me.dgvBuy.Location = New System.Drawing.Point(222, 230)
        Me.dgvBuy.Name = "dgvBuy"
        Me.dgvBuy.ReadOnly = True
        Me.dgvBuy.RowHeadersVisible = False
        Me.dgvBuy.RowTemplate.Height = 23
        Me.dgvBuy.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.dgvBuy.Size = New System.Drawing.Size(220, 230)
        Me.dgvBuy.TabIndex = 381
        '
        'Percent_buy
        '
        Me.Percent_buy.HeaderText = "Percent_buy"
        Me.Percent_buy.Name = "Percent_buy"
        Me.Percent_buy.ReadOnly = True
        Me.Percent_buy.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "매도가"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 70
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "매도호가별거래량"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 150
        '
        'dgvTrading
        '
        Me.dgvTrading.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTrading.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4})
        Me.dgvTrading.Location = New System.Drawing.Point(3, 232)
        Me.dgvTrading.Name = "dgvTrading"
        Me.dgvTrading.RowHeadersVisible = False
        Me.dgvTrading.RowTemplate.Height = 23
        Me.dgvTrading.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.dgvTrading.Size = New System.Drawing.Size(217, 204)
        Me.dgvTrading.TabIndex = 382
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "체결가"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 110
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "체결량"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Width = 110
        '
        'dg미체결
        '
        Me.dg미체결.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg미체결.Location = New System.Drawing.Point(222, 465)
        Me.dg미체결.Name = "dg미체결"
        Me.dg미체결.RowTemplate.Height = 23
        Me.dg미체결.Size = New System.Drawing.Size(218, 136)
        Me.dg미체결.TabIndex = 388
        Me.dg미체결.Visible = False
        '
        'txtModStockCode
        '
        Me.txtModStockCode.Location = New System.Drawing.Point(97, 26)
        Me.txtModStockCode.Name = "txtModStockCode"
        Me.txtModStockCode.ReadOnly = True
        Me.txtModStockCode.Size = New System.Drawing.Size(53, 20)
        Me.txtModStockCode.TabIndex = 369
        '
        'txtModStockName
        '
        Me.txtModStockName.Location = New System.Drawing.Point(151, 26)
        Me.txtModStockName.Name = "txtModStockName"
        Me.txtModStockName.ReadOnly = True
        Me.txtModStockName.Size = New System.Drawing.Size(53, 20)
        Me.txtModStockName.TabIndex = 370
        '
        'ucHogaWindowNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 11.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.dg미체결)
        Me.Controls.Add(Me.dgvTradeAt)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboAccount)
        Me.Controls.Add(Me.pnTrade)
        Me.Controls.Add(Me.dgvSell)
        Me.Controls.Add(Me.dgvBuy)
        Me.Controls.Add(Me.dgvTrading)
        Me.Font = New System.Drawing.Font("굴림체", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Name = "ucHogaWindowNew"
        Me.Size = New System.Drawing.Size(447, 782)
        Me.tpBuy.ResumeLayout(False)
        Me.tpBuy.PerformLayout()
        CType(Me.numBuyPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numBuyQty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpSell.ResumeLayout(False)
        Me.tpSell.PerformLayout()
        CType(Me.numSellPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numSellQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvTradeAt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.pnTrade.ResumeLayout(False)
        Me.tbTrade.ResumeLayout(False)
        Me.tpModify.ResumeLayout(False)
        Me.tpModify.PerformLayout()
        CType(Me.numModPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numModQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSell, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvBuy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvTrading, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dg미체결, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tpBuy As System.Windows.Forms.TabPage
    Friend WithEvents cboBuyTradeGb As System.Windows.Forms.ComboBox
    Friend WithEvents numBuyPrice As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btnBuy As System.Windows.Forms.Button
    Friend WithEvents chkBuyCurrentPrice As System.Windows.Forms.CheckBox
    Friend WithEvents numBuyQty As System.Windows.Forms.NumericUpDown
    Friend WithEvents tpSell As System.Windows.Forms.TabPage
    Friend WithEvents chkSellAvaQty As System.Windows.Forms.CheckBox
    Friend WithEvents cboSellTradeGb As System.Windows.Forms.ComboBox
    Friend WithEvents numSellPrice As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents btnSell As System.Windows.Forms.Button
    Friend WithEvents chkSellCurrentPrice As System.Windows.Forms.CheckBox
    Friend WithEvents numSellQty As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblStockCode As System.Windows.Forms.Label
    Friend WithEvents dgvTradeAt As System.Windows.Forms.DataGridView
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblStockName As System.Windows.Forms.Label
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents lblPreDayPrice As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblStartPrice As System.Windows.Forms.Label
    Friend WithEvents lblUpDownRate As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblHighestPrice As System.Windows.Forms.Label
    Friend WithEvents lblTradingQty As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblLowestPrice As System.Windows.Forms.Label
    Friend WithEvents lblCurrentPrice As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboAccount As System.Windows.Forms.ComboBox
    Friend WithEvents pnTrade As System.Windows.Forms.Panel
    Friend WithEvents tbTrade As System.Windows.Forms.TabControl
    Friend WithEvents dgvSell As System.Windows.Forms.DataGridView
    Friend WithEvents Percent_Sell As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SellQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SellPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvBuy As System.Windows.Forms.DataGridView
    Friend WithEvents Percent_buy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvTrading As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tpModify As System.Windows.Forms.TabPage
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnMi As System.Windows.Forms.Button
    Friend WithEvents numModPrice As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btn정정 As System.Windows.Forms.Button
    Friend WithEvents chkModCurrentPrice As System.Windows.Forms.CheckBox
    Friend WithEvents numModQty As System.Windows.Forms.NumericUpDown
    Friend WithEvents dg미체결 As System.Windows.Forms.DataGridView
    Friend WithEvents txtOrderNo As System.Windows.Forms.TextBox
    Friend WithEvents txtisSellBuy As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtGb As System.Windows.Forms.TextBox
    Friend WithEvents btn취소 As System.Windows.Forms.Button
    Friend WithEvents txtModStockCode As System.Windows.Forms.TextBox
    Friend WithEvents txtModStockName As System.Windows.Forms.TextBox

End Class