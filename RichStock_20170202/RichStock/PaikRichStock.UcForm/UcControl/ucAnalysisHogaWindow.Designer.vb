<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucAnalysisHogaWindow
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
        Me.dgvBuy = New System.Windows.Forms.DataGridView()
        Me.Percent_buy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvSell = New System.Windows.Forms.DataGridView()
        Me.Percent_Sell = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SellQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SellPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblStockName = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.lblPreDayPrice = New System.Windows.Forms.Label()
        Me.lblStockCode = New System.Windows.Forms.Label()
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
        Me.dgvTrading = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        CType(Me.dgvBuy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSell, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvTrading, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvBuy
        '
        Me.dgvBuy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBuy.ColumnHeadersVisible = False
        Me.dgvBuy.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Percent_buy, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn1})
        Me.dgvBuy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvBuy.Location = New System.Drawing.Point(0, 0)
        Me.dgvBuy.Name = "dgvBuy"
        Me.dgvBuy.RowHeadersVisible = False
        Me.dgvBuy.RowTemplate.Height = 23
        Me.dgvBuy.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvBuy.Size = New System.Drawing.Size(249, 540)
        Me.dgvBuy.TabIndex = 3
        '
        'Percent_buy
        '
        Me.Percent_buy.HeaderText = "Percent_buy"
        Me.Percent_buy.Name = "Percent_buy"
        Me.Percent_buy.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "매도가"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 70
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "매도호가별거래량"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 150
        '
        'dgvSell
        '
        Me.dgvSell.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSell.ColumnHeadersVisible = False
        Me.dgvSell.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Percent_Sell, Me.SellQty, Me.SellPrice})
        Me.dgvSell.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSell.Location = New System.Drawing.Point(0, 0)
        Me.dgvSell.Name = "dgvSell"
        Me.dgvSell.RowHeadersVisible = False
        Me.dgvSell.RowTemplate.Height = 20
        Me.dgvSell.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvSell.Size = New System.Drawing.Size(260, 540)
        Me.dgvSell.TabIndex = 2
        '
        'Percent_Sell
        '
        Me.Percent_Sell.HeaderText = "Percent_Sell"
        Me.Percent_Sell.Name = "Percent_Sell"
        Me.Percent_Sell.Visible = False
        '
        'SellQty
        '
        Me.SellQty.HeaderText = "매도호가별거래량"
        Me.SellQty.Name = "SellQty"
        Me.SellQty.Width = 150
        '
        'SellPrice
        '
        Me.SellPrice.HeaderText = "매도가"
        Me.SellPrice.Name = "SellPrice"
        Me.SellPrice.Width = 70
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
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(219, 183)
        Me.Panel1.TabIndex = 387
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
        'dgvTrading
        '
        Me.dgvTrading.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTrading.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4})
        Me.dgvTrading.Location = New System.Drawing.Point(2, 192)
        Me.dgvTrading.Name = "dgvTrading"
        Me.dgvTrading.RowHeadersVisible = False
        Me.dgvTrading.RowTemplate.Height = 23
        Me.dgvTrading.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.dgvTrading.Size = New System.Drawing.Size(217, 204)
        Me.dgvTrading.TabIndex = 388
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
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgvTrading)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Size = New System.Drawing.Size(745, 540)
        Me.SplitContainer1.SplitterDistance = 513
        Me.SplitContainer1.TabIndex = 389
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.dgvSell)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.dgvBuy)
        Me.SplitContainer2.Size = New System.Drawing.Size(513, 540)
        Me.SplitContainer2.SplitterDistance = 260
        Me.SplitContainer2.TabIndex = 0
        '
        'ucAnalysisHogaWindow
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.Controls.Add(Me.SplitContainer1)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Name = "ucAnalysisHogaWindow"
        Me.Size = New System.Drawing.Size(745, 540)
        CType(Me.dgvBuy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSell, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.dgvTrading, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvBuy As System.Windows.Forms.DataGridView
    Friend WithEvents Percent_buy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvSell As System.Windows.Forms.DataGridView
    Friend WithEvents Percent_Sell As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SellQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SellPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblStockName As System.Windows.Forms.Label
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents lblPreDayPrice As System.Windows.Forms.Label
    Friend WithEvents lblStockCode As System.Windows.Forms.Label
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
    Friend WithEvents dgvTrading As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer

End Class
