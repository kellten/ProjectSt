﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaikStockMainVer3
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
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
        Me.components = New System.ComponentModel.Container()
        Me.DataGridViewTextBoxColumn33 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn35 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn36 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SplitConSubMain = New System.Windows.Forms.SplitContainer()
        Me.SplitConSubMainWorker = New System.Windows.Forms.SplitContainer()
        Me.UcHogaWindowNew = New PaikRichStock.UcForm.ucHogaWindowNew()
        Me.tcMain = New System.Windows.Forms.TabControl()
        Me.tpDart = New System.Windows.Forms.TabPage()
        Me.SplitConDart = New System.Windows.Forms.SplitContainer()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnDartStart = New System.Windows.Forms.Button()
        Me.lblFileNumDate = New System.Windows.Forms.Label()
        Me.msktDartTimer = New System.Windows.Forms.MaskedTextBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.btnDartBuyCondition = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtAllowQty = New System.Windows.Forms.TextBox()
        Me.UcDart1 = New DartPrj.UcDart()
        Me.tpCondition = New System.Windows.Forms.TabPage()
        Me.SplitConConditionA = New System.Windows.Forms.SplitContainer()
        Me.SplitConConditionB = New System.Windows.Forms.SplitContainer()
        Me.drvConditionList = New System.Windows.Forms.DataGridView()
        Me.CONDI_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CONDI_SEQ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ScreenNoCondition = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnConditionFavAdd = New System.Windows.Forms.Button()
        Me.dgvConditionStockList = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn37 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn38 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn39 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn40 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn41 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn42 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn43 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn44 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn45 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn46 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn47 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn48 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tpFav = New System.Windows.Forms.TabPage()
        Me.UcFavManage1 = New PaikRichStock.UcForm.ucFavManage()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.dgvScreenNo = New System.Windows.Forms.DataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.UcAnalysisHogaWindow1 = New PaikRichStock.UcForm.ucAnalysisHogaWindow()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.SplitConTradeInfo = New System.Windows.Forms.SplitContainer()
        Me.dgvTradeInfo = New System.Windows.Forms.DataGridView()
        Me.dgvAccountInfo = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn34 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn32 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VolumePower = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StartPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HighestPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LowestPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TradingTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PreDayBySymbol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.STOCK_CODE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ScreenNo_GetIn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gbDart = New System.Windows.Forms.GroupBox()
        Me.dgvDart = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.creator = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.title = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.conMenuStop = New System.Windows.Forms.ToolStripMenuItem()
        Me.conMenuStart = New System.Windows.Forms.ToolStripMenuItem()
        Me.TradingVolume = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lossGainRate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SplitConMain = New System.Windows.Forms.SplitContainer()
        Me.SplitConStatus = New System.Windows.Forms.SplitContainer()
        Me.UcMainStockVer2 = New PaikRichStock.Common.ucMainStockVer2()
        Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboTradeGb = New System.Windows.Forms.ComboBox()
        Me.cboAccount = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SplitConWorker = New System.Windows.Forms.SplitContainer()
        Me.SplitConRealDataMain = New System.Windows.Forms.SplitContainer()
        Me.SplitConRealDataA = New System.Windows.Forms.SplitContainer()
        Me.gbGetStock = New System.Windows.Forms.GroupBox()
        Me.dgvMyStock = New System.Windows.Forms.DataGridView()
        Me.STOCK_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CurrentPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SplitConRealDataB = New System.Windows.Forms.SplitContainer()
        Me.gbCondition = New System.Windows.Forms.GroupBox()
        Me.dgvCondition = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn18 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn19 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn20 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn21 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn22 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn23 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn24 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gbFav = New System.Windows.Forms.GroupBox()
        Me.dgvFav = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn25 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn26 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn27 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn28 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn29 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn30 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn31 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.conMenuFavAdd = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.SplitConSubMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitConSubMain.Panel1.SuspendLayout()
        Me.SplitConSubMain.Panel2.SuspendLayout()
        Me.SplitConSubMain.SuspendLayout()
        CType(Me.SplitConSubMainWorker, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitConSubMainWorker.Panel1.SuspendLayout()
        Me.SplitConSubMainWorker.Panel2.SuspendLayout()
        Me.SplitConSubMainWorker.SuspendLayout()
        Me.tcMain.SuspendLayout()
        Me.tpDart.SuspendLayout()
        CType(Me.SplitConDart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitConDart.Panel1.SuspendLayout()
        Me.SplitConDart.Panel2.SuspendLayout()
        Me.SplitConDart.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.tpCondition.SuspendLayout()
        CType(Me.SplitConConditionA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitConConditionA.Panel1.SuspendLayout()
        Me.SplitConConditionA.Panel2.SuspendLayout()
        Me.SplitConConditionA.SuspendLayout()
        CType(Me.SplitConConditionB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitConConditionB.Panel1.SuspendLayout()
        Me.SplitConConditionB.Panel2.SuspendLayout()
        Me.SplitConConditionB.SuspendLayout()
        CType(Me.drvConditionList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvConditionStockList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpFav.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.dgvScreenNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        CType(Me.SplitConTradeInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitConTradeInfo.Panel1.SuspendLayout()
        Me.SplitConTradeInfo.Panel2.SuspendLayout()
        Me.SplitConTradeInfo.SuspendLayout()
        CType(Me.dgvTradeInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvAccountInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbDart.SuspendLayout()
        CType(Me.dgvDart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.SplitConMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitConMain.Panel1.SuspendLayout()
        Me.SplitConMain.Panel2.SuspendLayout()
        Me.SplitConMain.SuspendLayout()
        CType(Me.SplitConStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitConStatus.Panel1.SuspendLayout()
        Me.SplitConStatus.Panel2.SuspendLayout()
        Me.SplitConStatus.SuspendLayout()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitConWorker, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitConWorker.Panel1.SuspendLayout()
        Me.SplitConWorker.Panel2.SuspendLayout()
        Me.SplitConWorker.SuspendLayout()
        CType(Me.SplitConRealDataMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitConRealDataMain.Panel1.SuspendLayout()
        Me.SplitConRealDataMain.Panel2.SuspendLayout()
        Me.SplitConRealDataMain.SuspendLayout()
        CType(Me.SplitConRealDataA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitConRealDataA.Panel1.SuspendLayout()
        Me.SplitConRealDataA.Panel2.SuspendLayout()
        Me.SplitConRealDataA.SuspendLayout()
        Me.gbGetStock.SuspendLayout()
        CType(Me.dgvMyStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitConRealDataB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitConRealDataB.Panel1.SuspendLayout()
        Me.SplitConRealDataB.Panel2.SuspendLayout()
        Me.SplitConRealDataB.SuspendLayout()
        Me.gbCondition.SuspendLayout()
        CType(Me.dgvCondition, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbFav.SuspendLayout()
        CType(Me.dgvFav, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridViewTextBoxColumn33
        '
        Me.DataGridViewTextBoxColumn33.HeaderText = "체결시간"
        Me.DataGridViewTextBoxColumn33.Name = "DataGridViewTextBoxColumn33"
        '
        'DataGridViewTextBoxColumn35
        '
        Me.DataGridViewTextBoxColumn35.HeaderText = "STOCK_CODE"
        Me.DataGridViewTextBoxColumn35.Name = "DataGridViewTextBoxColumn35"
        '
        'DataGridViewTextBoxColumn36
        '
        Me.DataGridViewTextBoxColumn36.HeaderText = "화면번호"
        Me.DataGridViewTextBoxColumn36.Name = "DataGridViewTextBoxColumn36"
        '
        'SplitConSubMain
        '
        Me.SplitConSubMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitConSubMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitConSubMain.Location = New System.Drawing.Point(0, 0)
        Me.SplitConSubMain.Name = "SplitConSubMain"
        Me.SplitConSubMain.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitConSubMain.Panel1
        '
        Me.SplitConSubMain.Panel1.Controls.Add(Me.SplitConSubMainWorker)
        '
        'SplitConSubMain.Panel2
        '
        Me.SplitConSubMain.Panel2.Controls.Add(Me.SplitConTradeInfo)
        Me.SplitConSubMain.Size = New System.Drawing.Size(1012, 586)
        Me.SplitConSubMain.SplitterDistance = 440
        Me.SplitConSubMain.TabIndex = 0
        '
        'SplitConSubMainWorker
        '
        Me.SplitConSubMainWorker.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitConSubMainWorker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitConSubMainWorker.Location = New System.Drawing.Point(0, 0)
        Me.SplitConSubMainWorker.Name = "SplitConSubMainWorker"
        '
        'SplitConSubMainWorker.Panel1
        '
        Me.SplitConSubMainWorker.Panel1.Controls.Add(Me.UcHogaWindowNew)
        '
        'SplitConSubMainWorker.Panel2
        '
        Me.SplitConSubMainWorker.Panel2.Controls.Add(Me.tcMain)
        Me.SplitConSubMainWorker.Size = New System.Drawing.Size(1012, 440)
        Me.SplitConSubMainWorker.SplitterDistance = 25
        Me.SplitConSubMainWorker.TabIndex = 0
        '
        'UcHogaWindowNew
        '
        Me.UcHogaWindowNew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcHogaWindowNew.Font = New System.Drawing.Font("굴림체", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.UcHogaWindowNew.Location = New System.Drawing.Point(0, 0)
        Me.UcHogaWindowNew.Name = "UcHogaWindowNew"
        Me.UcHogaWindowNew.Size = New System.Drawing.Size(21, 436)
        Me.UcHogaWindowNew.StockCode = ""
        Me.UcHogaWindowNew.TabIndex = 0
        '
        'tcMain
        '
        Me.tcMain.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.tcMain.Controls.Add(Me.tpDart)
        Me.tcMain.Controls.Add(Me.tpCondition)
        Me.tcMain.Controls.Add(Me.tpFav)
        Me.tcMain.Controls.Add(Me.TabPage1)
        Me.tcMain.Controls.Add(Me.TabPage2)
        Me.tcMain.Controls.Add(Me.TabPage3)
        Me.tcMain.Controls.Add(Me.TabPage4)
        Me.tcMain.Controls.Add(Me.TabPage5)
        Me.tcMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcMain.Location = New System.Drawing.Point(0, 0)
        Me.tcMain.Name = "tcMain"
        Me.tcMain.SelectedIndex = 0
        Me.tcMain.Size = New System.Drawing.Size(979, 436)
        Me.tcMain.TabIndex = 0
        '
        'tpDart
        '
        Me.tpDart.Controls.Add(Me.SplitConDart)
        Me.tpDart.Location = New System.Drawing.Point(4, 24)
        Me.tpDart.Name = "tpDart"
        Me.tpDart.Padding = New System.Windows.Forms.Padding(3)
        Me.tpDart.Size = New System.Drawing.Size(971, 408)
        Me.tpDart.TabIndex = 0
        Me.tpDart.Text = "공시"
        Me.tpDart.UseVisualStyleBackColor = True
        '
        'SplitConDart
        '
        Me.SplitConDart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitConDart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitConDart.Location = New System.Drawing.Point(3, 3)
        Me.SplitConDart.Name = "SplitConDart"
        Me.SplitConDart.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitConDart.Panel1
        '
        Me.SplitConDart.Panel1.Controls.Add(Me.Button1)
        Me.SplitConDart.Panel1.Controls.Add(Me.Panel1)
        Me.SplitConDart.Panel1.Controls.Add(Me.GroupBox5)
        '
        'SplitConDart.Panel2
        '
        Me.SplitConDart.Panel2.Controls.Add(Me.UcDart1)
        Me.SplitConDart.Size = New System.Drawing.Size(965, 402)
        Me.SplitConDart.SplitterDistance = 124
        Me.SplitConDart.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(9, 76)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnDartStart)
        Me.Panel1.Controls.Add(Me.lblFileNumDate)
        Me.Panel1.Controls.Add(Me.msktDartTimer)
        Me.Panel1.Location = New System.Drawing.Point(1, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(961, 25)
        Me.Panel1.TabIndex = 362
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.GhostWhite
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 21)
        Me.Label1.TabIndex = 349
        Me.Label1.Text = "타이머"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnDartStart
        '
        Me.btnDartStart.Location = New System.Drawing.Point(128, 0)
        Me.btnDartStart.Name = "btnDartStart"
        Me.btnDartStart.Size = New System.Drawing.Size(46, 23)
        Me.btnDartStart.TabIndex = 6
        Me.btnDartStart.Text = "▶"
        Me.btnDartStart.UseVisualStyleBackColor = True
        '
        'lblFileNumDate
        '
        Me.lblFileNumDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFileNumDate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblFileNumDate.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblFileNumDate.ForeColor = System.Drawing.Color.DarkRed
        Me.lblFileNumDate.Location = New System.Drawing.Point(171, 1)
        Me.lblFileNumDate.Name = "lblFileNumDate"
        Me.lblFileNumDate.Size = New System.Drawing.Size(532, 22)
        Me.lblFileNumDate.TabIndex = 358
        Me.lblFileNumDate.Tag = "/D"
        Me.lblFileNumDate.Text = "테스트임다"
        Me.lblFileNumDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblFileNumDate.Visible = False
        '
        'msktDartTimer
        '
        Me.msktDartTimer.Location = New System.Drawing.Point(75, 1)
        Me.msktDartTimer.Name = "msktDartTimer"
        Me.msktDartTimer.Size = New System.Drawing.Size(47, 20)
        Me.msktDartTimer.TabIndex = 357
        Me.msktDartTimer.Text = "1000"
        Me.msktDartTimer.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.Controls.Add(Me.btnDartBuyCondition)
        Me.GroupBox5.Controls.Add(Me.TextBox1)
        Me.GroupBox5.Controls.Add(Me.Label5)
        Me.GroupBox5.Controls.Add(Me.TextBox2)
        Me.GroupBox5.Controls.Add(Me.Label9)
        Me.GroupBox5.Controls.Add(Me.Label2)
        Me.GroupBox5.Controls.Add(Me.txtAllowQty)
        Me.GroupBox5.Location = New System.Drawing.Point(1, 25)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(956, 45)
        Me.GroupBox5.TabIndex = 361
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "매수조건"
        '
        'btnDartBuyCondition
        '
        Me.btnDartBuyCondition.Location = New System.Drawing.Point(425, 16)
        Me.btnDartBuyCondition.Name = "btnDartBuyCondition"
        Me.btnDartBuyCondition.Size = New System.Drawing.Size(58, 23)
        Me.btnDartBuyCondition.TabIndex = 365
        Me.btnDartBuyCondition.Text = "표시"
        Me.btnDartBuyCondition.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(196, 17)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(71, 20)
        Me.TextBox1.TabIndex = 364
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.GhostWhite
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Location = New System.Drawing.Point(121, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 21)
        Me.Label5.TabIndex = 363
        Me.Label5.Text = "체결량(%)"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(348, 18)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(71, 20)
        Me.TextBox2.TabIndex = 362
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.GhostWhite
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Location = New System.Drawing.Point(273, 17)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(69, 21)
        Me.Label9.TabIndex = 361
        Me.Label9.Text = "거래량비율"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.GhostWhite
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Location = New System.Drawing.Point(8, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 21)
        Me.Label2.TabIndex = 359
        Me.Label2.Text = "체결강도"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtAllowQty
        '
        Me.txtAllowQty.Location = New System.Drawing.Point(81, 16)
        Me.txtAllowQty.Name = "txtAllowQty"
        Me.txtAllowQty.Size = New System.Drawing.Size(34, 20)
        Me.txtAllowQty.TabIndex = 360
        Me.txtAllowQty.Text = "90"
        '
        'UcDart1
        '
        Me.UcDart1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcDart1.Location = New System.Drawing.Point(0, 0)
        Me.UcDart1.Name = "UcDart1"
        Me.UcDart1.Size = New System.Drawing.Size(961, 270)
        Me.UcDart1.TabIndex = 0
        '
        'tpCondition
        '
        Me.tpCondition.Controls.Add(Me.SplitConConditionA)
        Me.tpCondition.Location = New System.Drawing.Point(4, 24)
        Me.tpCondition.Name = "tpCondition"
        Me.tpCondition.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCondition.Size = New System.Drawing.Size(971, 408)
        Me.tpCondition.TabIndex = 1
        Me.tpCondition.Text = "조건검색"
        Me.tpCondition.UseVisualStyleBackColor = True
        '
        'SplitConConditionA
        '
        Me.SplitConConditionA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitConConditionA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitConConditionA.Location = New System.Drawing.Point(3, 3)
        Me.SplitConConditionA.Name = "SplitConConditionA"
        Me.SplitConConditionA.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitConConditionA.Panel1
        '
        Me.SplitConConditionA.Panel1.Controls.Add(Me.SplitConConditionB)
        '
        'SplitConConditionA.Panel2
        '
        Me.SplitConConditionA.Panel2.Controls.Add(Me.dgvConditionStockList)
        Me.SplitConConditionA.Size = New System.Drawing.Size(965, 402)
        Me.SplitConConditionA.SplitterDistance = 124
        Me.SplitConConditionA.TabIndex = 0
        '
        'SplitConConditionB
        '
        Me.SplitConConditionB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitConConditionB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitConConditionB.Location = New System.Drawing.Point(0, 0)
        Me.SplitConConditionB.Name = "SplitConConditionB"
        '
        'SplitConConditionB.Panel1
        '
        Me.SplitConConditionB.Panel1.Controls.Add(Me.drvConditionList)
        '
        'SplitConConditionB.Panel2
        '
        Me.SplitConConditionB.Panel2.Controls.Add(Me.btnConditionFavAdd)
        Me.SplitConConditionB.Size = New System.Drawing.Size(965, 124)
        Me.SplitConConditionB.SplitterDistance = 323
        Me.SplitConConditionB.TabIndex = 0
        '
        'drvConditionList
        '
        Me.drvConditionList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.drvConditionList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CONDI_NAME, Me.CONDI_SEQ, Me.ScreenNoCondition})
        Me.drvConditionList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.drvConditionList.Location = New System.Drawing.Point(0, 0)
        Me.drvConditionList.Name = "drvConditionList"
        Me.drvConditionList.RowTemplate.Height = 23
        Me.drvConditionList.Size = New System.Drawing.Size(319, 120)
        Me.drvConditionList.TabIndex = 0
        '
        'CONDI_NAME
        '
        Me.CONDI_NAME.HeaderText = "조건검색명"
        Me.CONDI_NAME.Name = "CONDI_NAME"
        Me.CONDI_NAME.Width = 200
        '
        'CONDI_SEQ
        '
        Me.CONDI_SEQ.HeaderText = "조건인덱스"
        Me.CONDI_SEQ.Name = "CONDI_SEQ"
        '
        'ScreenNoCondition
        '
        Me.ScreenNoCondition.HeaderText = "화면번호"
        Me.ScreenNoCondition.Name = "ScreenNoCondition"
        '
        'btnConditionFavAdd
        '
        Me.btnConditionFavAdd.Location = New System.Drawing.Point(3, 3)
        Me.btnConditionFavAdd.Name = "btnConditionFavAdd"
        Me.btnConditionFavAdd.Size = New System.Drawing.Size(133, 29)
        Me.btnConditionFavAdd.TabIndex = 0
        Me.btnConditionFavAdd.Text = "관심종목에 추가"
        Me.btnConditionFavAdd.UseVisualStyleBackColor = True
        '
        'dgvConditionStockList
        '
        Me.dgvConditionStockList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvConditionStockList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn37, Me.DataGridViewTextBoxColumn38, Me.DataGridViewTextBoxColumn39, Me.DataGridViewTextBoxColumn40, Me.DataGridViewTextBoxColumn41, Me.DataGridViewTextBoxColumn42, Me.DataGridViewTextBoxColumn43, Me.DataGridViewTextBoxColumn44, Me.DataGridViewTextBoxColumn45, Me.DataGridViewTextBoxColumn46, Me.DataGridViewTextBoxColumn47, Me.DataGridViewTextBoxColumn48})
        Me.dgvConditionStockList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvConditionStockList.Location = New System.Drawing.Point(0, 0)
        Me.dgvConditionStockList.Name = "dgvConditionStockList"
        Me.dgvConditionStockList.RowHeadersVisible = False
        Me.dgvConditionStockList.Size = New System.Drawing.Size(961, 270)
        Me.dgvConditionStockList.TabIndex = 1
        '
        'DataGridViewTextBoxColumn37
        '
        Me.DataGridViewTextBoxColumn37.HeaderText = "종목명"
        Me.DataGridViewTextBoxColumn37.Name = "DataGridViewTextBoxColumn37"
        Me.DataGridViewTextBoxColumn37.Width = 120
        '
        'DataGridViewTextBoxColumn38
        '
        Me.DataGridViewTextBoxColumn38.HeaderText = "현재가"
        Me.DataGridViewTextBoxColumn38.MaxInputLength = 10000000
        Me.DataGridViewTextBoxColumn38.Name = "DataGridViewTextBoxColumn38"
        Me.DataGridViewTextBoxColumn38.Width = 65
        '
        'DataGridViewTextBoxColumn39
        '
        Me.DataGridViewTextBoxColumn39.HeaderText = "등락율"
        Me.DataGridViewTextBoxColumn39.Name = "DataGridViewTextBoxColumn39"
        Me.DataGridViewTextBoxColumn39.Width = 65
        '
        'DataGridViewTextBoxColumn40
        '
        Me.DataGridViewTextBoxColumn40.HeaderText = "체결강도"
        Me.DataGridViewTextBoxColumn40.Name = "DataGridViewTextBoxColumn40"
        '
        'DataGridViewTextBoxColumn41
        '
        Me.DataGridViewTextBoxColumn41.HeaderText = "거래량"
        Me.DataGridViewTextBoxColumn41.MaxInputLength = 999999999
        Me.DataGridViewTextBoxColumn41.Name = "DataGridViewTextBoxColumn41"
        Me.DataGridViewTextBoxColumn41.Width = 70
        '
        'DataGridViewTextBoxColumn42
        '
        Me.DataGridViewTextBoxColumn42.HeaderText = "시가"
        Me.DataGridViewTextBoxColumn42.MaxInputLength = 10000000
        Me.DataGridViewTextBoxColumn42.Name = "DataGridViewTextBoxColumn42"
        '
        'DataGridViewTextBoxColumn43
        '
        Me.DataGridViewTextBoxColumn43.HeaderText = "고가"
        Me.DataGridViewTextBoxColumn43.MaxInputLength = 10000000
        Me.DataGridViewTextBoxColumn43.Name = "DataGridViewTextBoxColumn43"
        '
        'DataGridViewTextBoxColumn44
        '
        Me.DataGridViewTextBoxColumn44.HeaderText = "저가"
        Me.DataGridViewTextBoxColumn44.MaxInputLength = 10000000
        Me.DataGridViewTextBoxColumn44.Name = "DataGridViewTextBoxColumn44"
        '
        'DataGridViewTextBoxColumn45
        '
        Me.DataGridViewTextBoxColumn45.HeaderText = "체결시간"
        Me.DataGridViewTextBoxColumn45.Name = "DataGridViewTextBoxColumn45"
        '
        'DataGridViewTextBoxColumn46
        '
        Me.DataGridViewTextBoxColumn46.HeaderText = "전일대비기호"
        Me.DataGridViewTextBoxColumn46.Name = "DataGridViewTextBoxColumn46"
        '
        'DataGridViewTextBoxColumn47
        '
        Me.DataGridViewTextBoxColumn47.HeaderText = "STOCK_CODE"
        Me.DataGridViewTextBoxColumn47.Name = "DataGridViewTextBoxColumn47"
        '
        'DataGridViewTextBoxColumn48
        '
        Me.DataGridViewTextBoxColumn48.HeaderText = "화면번호"
        Me.DataGridViewTextBoxColumn48.Name = "DataGridViewTextBoxColumn48"
        '
        'tpFav
        '
        Me.tpFav.Controls.Add(Me.UcFavManage1)
        Me.tpFav.Location = New System.Drawing.Point(4, 24)
        Me.tpFav.Name = "tpFav"
        Me.tpFav.Size = New System.Drawing.Size(971, 408)
        Me.tpFav.TabIndex = 2
        Me.tpFav.Text = "관심종목"
        Me.tpFav.UseVisualStyleBackColor = True
        '
        'UcFavManage1
        '
        Me.UcFavManage1.AutoScroll = True
        Me.UcFavManage1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcFavManage1.Font = New System.Drawing.Font("굴림체", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.UcFavManage1.Location = New System.Drawing.Point(0, 0)
        Me.UcFavManage1.Name = "UcFavManage1"
        Me.UcFavManage1.Size = New System.Drawing.Size(971, 408)
        Me.UcFavManage1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dgvScreenNo)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(971, 408)
        Me.TabPage1.TabIndex = 3
        Me.TabPage1.Text = "tpStockInfo"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'dgvScreenNo
        '
        Me.dgvScreenNo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvScreenNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvScreenNo.Location = New System.Drawing.Point(0, 0)
        Me.dgvScreenNo.Name = "dgvScreenNo"
        Me.dgvScreenNo.RowTemplate.Height = 23
        Me.dgvScreenNo.Size = New System.Drawing.Size(971, 408)
        Me.dgvScreenNo.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.UcAnalysisHogaWindow1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(971, 408)
        Me.TabPage2.TabIndex = 4
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'UcAnalysisHogaWindow1
        '
        Me.UcAnalysisHogaWindow1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAnalysisHogaWindow1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.UcAnalysisHogaWindow1.Location = New System.Drawing.Point(0, 0)
        Me.UcAnalysisHogaWindow1.Name = "UcAnalysisHogaWindow1"
        Me.UcAnalysisHogaWindow1.Size = New System.Drawing.Size(971, 408)
        Me.UcAnalysisHogaWindow1.StockCode = ""
        Me.UcAnalysisHogaWindow1.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Button2)
        Me.TabPage3.Location = New System.Drawing.Point(4, 24)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(971, 408)
        Me.TabPage3.TabIndex = 5
        Me.TabPage3.Text = "TabPage3"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(555, 6)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Button3)
        Me.TabPage4.Location = New System.Drawing.Point(4, 24)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(971, 408)
        Me.TabPage4.TabIndex = 6
        Me.TabPage4.Text = "TabPage4"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(237, 3)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 1
        Me.Button3.Text = "Button3"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'TabPage5
        '
        Me.TabPage5.Location = New System.Drawing.Point(4, 24)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(971, 408)
        Me.TabPage5.TabIndex = 7
        Me.TabPage5.Text = "TabPage5"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'SplitConTradeInfo
        '
        Me.SplitConTradeInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitConTradeInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitConTradeInfo.Location = New System.Drawing.Point(0, 0)
        Me.SplitConTradeInfo.Name = "SplitConTradeInfo"
        '
        'SplitConTradeInfo.Panel1
        '
        Me.SplitConTradeInfo.Panel1.Controls.Add(Me.dgvTradeInfo)
        '
        'SplitConTradeInfo.Panel2
        '
        Me.SplitConTradeInfo.Panel2.Controls.Add(Me.dgvAccountInfo)
        Me.SplitConTradeInfo.Size = New System.Drawing.Size(1012, 142)
        Me.SplitConTradeInfo.SplitterDistance = 490
        Me.SplitConTradeInfo.TabIndex = 0
        '
        'dgvTradeInfo
        '
        Me.dgvTradeInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTradeInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTradeInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgvTradeInfo.Name = "dgvTradeInfo"
        Me.dgvTradeInfo.RowTemplate.Height = 23
        Me.dgvTradeInfo.Size = New System.Drawing.Size(486, 138)
        Me.dgvTradeInfo.TabIndex = 0
        '
        'dgvAccountInfo
        '
        Me.dgvAccountInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAccountInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAccountInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgvAccountInfo.Name = "dgvAccountInfo"
        Me.dgvAccountInfo.RowTemplate.Height = 23
        Me.dgvAccountInfo.Size = New System.Drawing.Size(514, 138)
        Me.dgvAccountInfo.TabIndex = 0
        '
        'DataGridViewTextBoxColumn34
        '
        Me.DataGridViewTextBoxColumn34.HeaderText = "전일대비기호"
        Me.DataGridViewTextBoxColumn34.Name = "DataGridViewTextBoxColumn34"
        '
        'DataGridViewTextBoxColumn32
        '
        Me.DataGridViewTextBoxColumn32.HeaderText = "저가"
        Me.DataGridViewTextBoxColumn32.MaxInputLength = 10000000
        Me.DataGridViewTextBoxColumn32.Name = "DataGridViewTextBoxColumn32"
        '
        'VolumePower
        '
        Me.VolumePower.HeaderText = "체결강도"
        Me.VolumePower.Name = "VolumePower"
        '
        'StartPrice
        '
        Me.StartPrice.HeaderText = "시가"
        Me.StartPrice.MaxInputLength = 10000000
        Me.StartPrice.Name = "StartPrice"
        '
        'HighestPrice
        '
        Me.HighestPrice.HeaderText = "고가"
        Me.HighestPrice.MaxInputLength = 10000000
        Me.HighestPrice.Name = "HighestPrice"
        '
        'LowestPrice
        '
        Me.LowestPrice.HeaderText = "저가"
        Me.LowestPrice.MaxInputLength = 10000000
        Me.LowestPrice.Name = "LowestPrice"
        '
        'TradingTime
        '
        Me.TradingTime.HeaderText = "체결시간"
        Me.TradingTime.Name = "TradingTime"
        '
        'PreDayBySymbol
        '
        Me.PreDayBySymbol.HeaderText = "전일대비기호"
        Me.PreDayBySymbol.Name = "PreDayBySymbol"
        '
        'STOCK_CODE
        '
        Me.STOCK_CODE.HeaderText = "STOCK_CODE"
        Me.STOCK_CODE.Name = "STOCK_CODE"
        '
        'ScreenNo_GetIn
        '
        Me.ScreenNo_GetIn.HeaderText = "화면번호"
        Me.ScreenNo_GetIn.Name = "ScreenNo_GetIn"
        '
        'gbDart
        '
        Me.gbDart.Controls.Add(Me.dgvDart)
        Me.gbDart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbDart.Location = New System.Drawing.Point(0, 0)
        Me.gbDart.Name = "gbDart"
        Me.gbDart.Size = New System.Drawing.Size(301, 137)
        Me.gbDart.TabIndex = 1
        Me.gbDart.TabStop = False
        Me.gbDart.Text = "공시종목"
        '
        'dgvDart
        '
        Me.dgvDart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDart.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9, Me.DataGridViewTextBoxColumn10, Me.DataGridViewTextBoxColumn11, Me.DataGridViewTextBoxColumn12, Me.creator, Me.Column8, Me.title, Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7})
        Me.dgvDart.ContextMenuStrip = Me.ContextMenuStrip1
        Me.dgvDart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDart.Location = New System.Drawing.Point(3, 16)
        Me.dgvDart.Name = "dgvDart"
        Me.dgvDart.RowHeadersVisible = False
        Me.dgvDart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDart.Size = New System.Drawing.Size(295, 118)
        Me.dgvDart.TabIndex = 1
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "종목명"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 120
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "현재가"
        Me.DataGridViewTextBoxColumn2.MaxInputLength = 10000000
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 65
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "등락율"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 65
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "체결강도"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "거래량"
        Me.DataGridViewTextBoxColumn5.MaxInputLength = 999999999
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Width = 70
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "시가"
        Me.DataGridViewTextBoxColumn6.MaxInputLength = 10000000
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.HeaderText = "고가"
        Me.DataGridViewTextBoxColumn7.MaxInputLength = 10000000
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "저가"
        Me.DataGridViewTextBoxColumn8.MaxInputLength = 10000000
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "체결시간"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.HeaderText = "전일대비기호"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.HeaderText = "STOCK_CODE"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.HeaderText = "화면번호"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        '
        'creator
        '
        Me.creator.HeaderText = "creator"
        Me.creator.Name = "creator"
        '
        'Column8
        '
        Me.Column8.HeaderText = "link"
        Me.Column8.Name = "Column8"
        '
        'title
        '
        Me.title.HeaderText = "title"
        Me.title.Name = "title"
        '
        'Column1
        '
        Me.Column1.HeaderText = "최초거래량"
        Me.Column1.MaxInputLength = 999999999
        Me.Column1.Name = "Column1"
        '
        'Column2
        '
        Me.Column2.HeaderText = "다음거래량"
        Me.Column2.MaxInputLength = 999999999
        Me.Column2.Name = "Column2"
        '
        'Column3
        '
        Me.Column3.HeaderText = "최초체결강도"
        Me.Column3.Name = "Column3"
        '
        'Column4
        '
        Me.Column4.HeaderText = "다음체결강도"
        Me.Column4.Name = "Column4"
        '
        'Column5
        '
        Me.Column5.HeaderText = "최초가"
        Me.Column5.MaxInputLength = 10000000
        Me.Column5.Name = "Column5"
        '
        'Column6
        '
        Me.Column6.HeaderText = "최초체결시간"
        Me.Column6.Name = "Column6"
        '
        'Column7
        '
        Me.Column7.HeaderText = "다음체결시간"
        Me.Column7.Name = "Column7"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.conMenuStop, Me.conMenuStart})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(139, 48)
        '
        'conMenuStop
        '
        Me.conMenuStop.Name = "conMenuStop"
        Me.conMenuStop.Size = New System.Drawing.Size(138, 22)
        Me.conMenuStop.Text = "실시간 중단"
        '
        'conMenuStart
        '
        Me.conMenuStart.Name = "conMenuStart"
        Me.conMenuStart.Size = New System.Drawing.Size(138, 22)
        Me.conMenuStart.Text = "실시간 시작"
        '
        'TradingVolume
        '
        Me.TradingVolume.HeaderText = "거래량"
        Me.TradingVolume.MaxInputLength = 999999999
        Me.TradingVolume.Name = "TradingVolume"
        Me.TradingVolume.Width = 70
        '
        'lossGainRate
        '
        Me.lossGainRate.HeaderText = "등락율"
        Me.lossGainRate.Name = "lossGainRate"
        Me.lossGainRate.Width = 65
        '
        'SplitConMain
        '
        Me.SplitConMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitConMain.Location = New System.Drawing.Point(0, 0)
        Me.SplitConMain.Name = "SplitConMain"
        Me.SplitConMain.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitConMain.Panel1
        '
        Me.SplitConMain.Panel1.Controls.Add(Me.SplitConStatus)
        '
        'SplitConMain.Panel2
        '
        Me.SplitConMain.Panel2.Controls.Add(Me.SplitConWorker)
        Me.SplitConMain.Size = New System.Drawing.Size(1321, 622)
        Me.SplitConMain.SplitterDistance = 32
        Me.SplitConMain.TabIndex = 1
        '
        'SplitConStatus
        '
        Me.SplitConStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitConStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitConStatus.Location = New System.Drawing.Point(0, 0)
        Me.SplitConStatus.Name = "SplitConStatus"
        '
        'SplitConStatus.Panel1
        '
        Me.SplitConStatus.Panel1.Controls.Add(Me.UcMainStockVer2)
        '
        'SplitConStatus.Panel2
        '
        Me.SplitConStatus.Panel2.Controls.Add(Me.NumericUpDown2)
        Me.SplitConStatus.Panel2.Controls.Add(Me.Label4)
        Me.SplitConStatus.Panel2.Controls.Add(Me.Label6)
        Me.SplitConStatus.Panel2.Controls.Add(Me.cboTradeGb)
        Me.SplitConStatus.Panel2.Controls.Add(Me.cboAccount)
        Me.SplitConStatus.Panel2.Controls.Add(Me.Label3)
        Me.SplitConStatus.Size = New System.Drawing.Size(1321, 32)
        Me.SplitConStatus.SplitterDistance = 755
        Me.SplitConStatus.TabIndex = 0
        '
        'UcMainStockVer2
        '
        Me.UcMainStockVer2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.UcMainStockVer2.Location = New System.Drawing.Point(0, -2)
        Me.UcMainStockVer2.LoggerStartOption = False
        Me.UcMainStockVer2.Name = "UcMainStockVer2"
        Me.UcMainStockVer2.Size = New System.Drawing.Size(748, 32)
        Me.UcMainStockVer2.TabIndex = 0
        '
        'NumericUpDown2
        '
        Me.NumericUpDown2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.NumericUpDown2.Increment = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.NumericUpDown2.Location = New System.Drawing.Point(454, 4)
        Me.NumericUpDown2.Maximum = New Decimal(New Integer() {10000000, 0, 0, 0})
        Me.NumericUpDown2.Name = "NumericUpDown2"
        Me.NumericUpDown2.Size = New System.Drawing.Size(95, 21)
        Me.NumericUpDown2.TabIndex = 356
        Me.NumericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NumericUpDown2.ThousandsSeparator = True
        Me.NumericUpDown2.Value = New Decimal(New Integer() {1000000, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.GhostWhite
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Location = New System.Drawing.Point(9, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 21)
        Me.Label4.TabIndex = 352
        Me.Label4.Text = "계좌번호"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.GhostWhite
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Location = New System.Drawing.Point(382, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 21)
        Me.Label6.TabIndex = 355
        Me.Label6.Text = "금액설정"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboTradeGb
        '
        Me.cboTradeGb.FormattingEnabled = True
        Me.cboTradeGb.Items.AddRange(New Object() {"00 - 지정가", "03 - 시장가", "05 - 조건부지정가", "06 - 최유리지정가", "07 - 최우선지정가", "10 - 지정가IOC", "13 - 시장가IOC", "16 - 최유리IOC", "20 - 지정가FOK", "23 - 시장가FOK", "26 - 최유리FOK", "61 - 장전시간외종가", "62 - 시간외단일가매매", "81 - 장후시간외종가"})
        Me.cboTradeGb.Location = New System.Drawing.Point(255, 4)
        Me.cboTradeGb.Name = "cboTradeGb"
        Me.cboTradeGb.Size = New System.Drawing.Size(121, 19)
        Me.cboTradeGb.TabIndex = 353
        Me.cboTradeGb.Text = "00 - 지정가"
        '
        'cboAccount
        '
        Me.cboAccount.FormattingEnabled = True
        Me.cboAccount.Location = New System.Drawing.Point(84, 5)
        Me.cboAccount.Name = "cboAccount"
        Me.cboAccount.Size = New System.Drawing.Size(90, 19)
        Me.cboAccount.TabIndex = 351
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.GhostWhite
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Location = New System.Drawing.Point(180, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 21)
        Me.Label3.TabIndex = 354
        Me.Label3.Text = "주문구분"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SplitConWorker
        '
        Me.SplitConWorker.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitConWorker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitConWorker.Location = New System.Drawing.Point(0, 0)
        Me.SplitConWorker.Name = "SplitConWorker"
        '
        'SplitConWorker.Panel1
        '
        Me.SplitConWorker.Panel1.Controls.Add(Me.SplitConRealDataMain)
        '
        'SplitConWorker.Panel2
        '
        Me.SplitConWorker.Panel2.Controls.Add(Me.SplitConSubMain)
        Me.SplitConWorker.Size = New System.Drawing.Size(1321, 586)
        Me.SplitConWorker.SplitterDistance = 305
        Me.SplitConWorker.TabIndex = 0
        '
        'SplitConRealDataMain
        '
        Me.SplitConRealDataMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitConRealDataMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitConRealDataMain.Location = New System.Drawing.Point(0, 0)
        Me.SplitConRealDataMain.Name = "SplitConRealDataMain"
        Me.SplitConRealDataMain.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitConRealDataMain.Panel1
        '
        Me.SplitConRealDataMain.Panel1.Controls.Add(Me.SplitConRealDataA)
        '
        'SplitConRealDataMain.Panel2
        '
        Me.SplitConRealDataMain.Panel2.Controls.Add(Me.SplitConRealDataB)
        Me.SplitConRealDataMain.Size = New System.Drawing.Size(305, 586)
        Me.SplitConRealDataMain.SplitterDistance = 271
        Me.SplitConRealDataMain.TabIndex = 0
        '
        'SplitConRealDataA
        '
        Me.SplitConRealDataA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitConRealDataA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitConRealDataA.Location = New System.Drawing.Point(0, 0)
        Me.SplitConRealDataA.Name = "SplitConRealDataA"
        Me.SplitConRealDataA.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitConRealDataA.Panel1
        '
        Me.SplitConRealDataA.Panel1.Controls.Add(Me.gbGetStock)
        '
        'SplitConRealDataA.Panel2
        '
        Me.SplitConRealDataA.Panel2.Controls.Add(Me.gbDart)
        Me.SplitConRealDataA.Size = New System.Drawing.Size(305, 271)
        Me.SplitConRealDataA.SplitterDistance = 126
        Me.SplitConRealDataA.TabIndex = 0
        '
        'gbGetStock
        '
        Me.gbGetStock.Controls.Add(Me.dgvMyStock)
        Me.gbGetStock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGetStock.Location = New System.Drawing.Point(0, 0)
        Me.gbGetStock.Name = "gbGetStock"
        Me.gbGetStock.Size = New System.Drawing.Size(301, 122)
        Me.gbGetStock.TabIndex = 0
        Me.gbGetStock.TabStop = False
        Me.gbGetStock.Text = "보유종목"
        '
        'dgvMyStock
        '
        Me.dgvMyStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMyStock.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.STOCK_NAME, Me.CurrentPrice, Me.lossGainRate, Me.VolumePower, Me.TradingVolume, Me.StartPrice, Me.HighestPrice, Me.LowestPrice, Me.TradingTime, Me.PreDayBySymbol, Me.STOCK_CODE, Me.ScreenNo_GetIn})
        Me.dgvMyStock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMyStock.Location = New System.Drawing.Point(3, 16)
        Me.dgvMyStock.Name = "dgvMyStock"
        Me.dgvMyStock.RowHeadersVisible = False
        Me.dgvMyStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMyStock.Size = New System.Drawing.Size(295, 103)
        Me.dgvMyStock.TabIndex = 0
        '
        'STOCK_NAME
        '
        Me.STOCK_NAME.HeaderText = "종목명"
        Me.STOCK_NAME.Name = "STOCK_NAME"
        Me.STOCK_NAME.Width = 120
        '
        'CurrentPrice
        '
        Me.CurrentPrice.HeaderText = "현재가"
        Me.CurrentPrice.MaxInputLength = 10000000
        Me.CurrentPrice.Name = "CurrentPrice"
        Me.CurrentPrice.Width = 65
        '
        'SplitConRealDataB
        '
        Me.SplitConRealDataB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitConRealDataB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitConRealDataB.Location = New System.Drawing.Point(0, 0)
        Me.SplitConRealDataB.Name = "SplitConRealDataB"
        Me.SplitConRealDataB.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitConRealDataB.Panel1
        '
        Me.SplitConRealDataB.Panel1.Controls.Add(Me.gbCondition)
        '
        'SplitConRealDataB.Panel2
        '
        Me.SplitConRealDataB.Panel2.Controls.Add(Me.gbFav)
        Me.SplitConRealDataB.Size = New System.Drawing.Size(305, 311)
        Me.SplitConRealDataB.SplitterDistance = 159
        Me.SplitConRealDataB.TabIndex = 0
        '
        'gbCondition
        '
        Me.gbCondition.Controls.Add(Me.dgvCondition)
        Me.gbCondition.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbCondition.Location = New System.Drawing.Point(0, 0)
        Me.gbCondition.Name = "gbCondition"
        Me.gbCondition.Size = New System.Drawing.Size(301, 155)
        Me.gbCondition.TabIndex = 1
        Me.gbCondition.TabStop = False
        Me.gbCondition.Text = "조건검색종목"
        '
        'dgvCondition
        '
        Me.dgvCondition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCondition.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn13, Me.DataGridViewTextBoxColumn14, Me.DataGridViewTextBoxColumn15, Me.DataGridViewTextBoxColumn16, Me.DataGridViewTextBoxColumn17, Me.DataGridViewTextBoxColumn18, Me.DataGridViewTextBoxColumn19, Me.DataGridViewTextBoxColumn20, Me.DataGridViewTextBoxColumn21, Me.DataGridViewTextBoxColumn22, Me.DataGridViewTextBoxColumn23, Me.DataGridViewTextBoxColumn24})
        Me.dgvCondition.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvCondition.Location = New System.Drawing.Point(3, 16)
        Me.dgvCondition.Name = "dgvCondition"
        Me.dgvCondition.RowHeadersVisible = False
        Me.dgvCondition.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCondition.Size = New System.Drawing.Size(295, 136)
        Me.dgvCondition.TabIndex = 1
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.HeaderText = "종목명"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.Width = 120
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.HeaderText = "현재가"
        Me.DataGridViewTextBoxColumn14.MaxInputLength = 10000000
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.Width = 65
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.HeaderText = "등락율"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.Width = 65
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.HeaderText = "체결강도"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.HeaderText = "거래량"
        Me.DataGridViewTextBoxColumn17.MaxInputLength = 999999999
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.Width = 70
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.HeaderText = "시가"
        Me.DataGridViewTextBoxColumn18.MaxInputLength = 10000000
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        '
        'DataGridViewTextBoxColumn19
        '
        Me.DataGridViewTextBoxColumn19.HeaderText = "고가"
        Me.DataGridViewTextBoxColumn19.MaxInputLength = 10000000
        Me.DataGridViewTextBoxColumn19.Name = "DataGridViewTextBoxColumn19"
        '
        'DataGridViewTextBoxColumn20
        '
        Me.DataGridViewTextBoxColumn20.HeaderText = "저가"
        Me.DataGridViewTextBoxColumn20.MaxInputLength = 10000000
        Me.DataGridViewTextBoxColumn20.Name = "DataGridViewTextBoxColumn20"
        '
        'DataGridViewTextBoxColumn21
        '
        Me.DataGridViewTextBoxColumn21.HeaderText = "체결시간"
        Me.DataGridViewTextBoxColumn21.Name = "DataGridViewTextBoxColumn21"
        '
        'DataGridViewTextBoxColumn22
        '
        Me.DataGridViewTextBoxColumn22.HeaderText = "전일대비기호"
        Me.DataGridViewTextBoxColumn22.Name = "DataGridViewTextBoxColumn22"
        '
        'DataGridViewTextBoxColumn23
        '
        Me.DataGridViewTextBoxColumn23.HeaderText = "STOCK_CODE"
        Me.DataGridViewTextBoxColumn23.Name = "DataGridViewTextBoxColumn23"
        '
        'DataGridViewTextBoxColumn24
        '
        Me.DataGridViewTextBoxColumn24.HeaderText = "화면번호"
        Me.DataGridViewTextBoxColumn24.Name = "DataGridViewTextBoxColumn24"
        '
        'gbFav
        '
        Me.gbFav.Controls.Add(Me.dgvFav)
        Me.gbFav.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFav.Location = New System.Drawing.Point(0, 0)
        Me.gbFav.Name = "gbFav"
        Me.gbFav.Size = New System.Drawing.Size(301, 144)
        Me.gbFav.TabIndex = 1
        Me.gbFav.TabStop = False
        Me.gbFav.Text = "관심종목"
        '
        'dgvFav
        '
        Me.dgvFav.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFav.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn25, Me.DataGridViewTextBoxColumn26, Me.DataGridViewTextBoxColumn27, Me.DataGridViewTextBoxColumn28, Me.DataGridViewTextBoxColumn29, Me.DataGridViewTextBoxColumn30, Me.DataGridViewTextBoxColumn31, Me.DataGridViewTextBoxColumn32, Me.DataGridViewTextBoxColumn33, Me.DataGridViewTextBoxColumn34, Me.DataGridViewTextBoxColumn35, Me.DataGridViewTextBoxColumn36})
        Me.dgvFav.ContextMenuStrip = Me.ContextMenuStrip2
        Me.dgvFav.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvFav.Location = New System.Drawing.Point(3, 16)
        Me.dgvFav.Name = "dgvFav"
        Me.dgvFav.RowHeadersVisible = False
        Me.dgvFav.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvFav.Size = New System.Drawing.Size(295, 125)
        Me.dgvFav.TabIndex = 1
        '
        'DataGridViewTextBoxColumn25
        '
        Me.DataGridViewTextBoxColumn25.HeaderText = "종목명"
        Me.DataGridViewTextBoxColumn25.Name = "DataGridViewTextBoxColumn25"
        Me.DataGridViewTextBoxColumn25.Width = 120
        '
        'DataGridViewTextBoxColumn26
        '
        Me.DataGridViewTextBoxColumn26.HeaderText = "현재가"
        Me.DataGridViewTextBoxColumn26.MaxInputLength = 10000000
        Me.DataGridViewTextBoxColumn26.Name = "DataGridViewTextBoxColumn26"
        Me.DataGridViewTextBoxColumn26.Width = 65
        '
        'DataGridViewTextBoxColumn27
        '
        Me.DataGridViewTextBoxColumn27.HeaderText = "등락율"
        Me.DataGridViewTextBoxColumn27.Name = "DataGridViewTextBoxColumn27"
        Me.DataGridViewTextBoxColumn27.Width = 65
        '
        'DataGridViewTextBoxColumn28
        '
        Me.DataGridViewTextBoxColumn28.HeaderText = "체결강도"
        Me.DataGridViewTextBoxColumn28.Name = "DataGridViewTextBoxColumn28"
        '
        'DataGridViewTextBoxColumn29
        '
        Me.DataGridViewTextBoxColumn29.HeaderText = "거래량"
        Me.DataGridViewTextBoxColumn29.MaxInputLength = 999999999
        Me.DataGridViewTextBoxColumn29.Name = "DataGridViewTextBoxColumn29"
        Me.DataGridViewTextBoxColumn29.Width = 70
        '
        'DataGridViewTextBoxColumn30
        '
        Me.DataGridViewTextBoxColumn30.HeaderText = "시가"
        Me.DataGridViewTextBoxColumn30.MaxInputLength = 10000000
        Me.DataGridViewTextBoxColumn30.Name = "DataGridViewTextBoxColumn30"
        '
        'DataGridViewTextBoxColumn31
        '
        Me.DataGridViewTextBoxColumn31.HeaderText = "고가"
        Me.DataGridViewTextBoxColumn31.MaxInputLength = 10000000
        Me.DataGridViewTextBoxColumn31.Name = "DataGridViewTextBoxColumn31"
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.conMenuFavAdd})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(171, 26)
        '
        'conMenuFavAdd
        '
        Me.conMenuFavAdd.Name = "conMenuFavAdd"
        Me.conMenuFavAdd.Size = New System.Drawing.Size(170, 22)
        Me.conMenuFavAdd.Text = "관심종목가져오기"
        '
        'frmPaikStockMainVer3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 11.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1321, 622)
        Me.Controls.Add(Me.SplitConMain)
        Me.Font = New System.Drawing.Font("굴림체", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Name = "frmPaikStockMainVer3"
        Me.Text = "frmPaikStockMainVer3"
        Me.SplitConSubMain.Panel1.ResumeLayout(False)
        Me.SplitConSubMain.Panel2.ResumeLayout(False)
        CType(Me.SplitConSubMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitConSubMain.ResumeLayout(False)
        Me.SplitConSubMainWorker.Panel1.ResumeLayout(False)
        Me.SplitConSubMainWorker.Panel2.ResumeLayout(False)
        CType(Me.SplitConSubMainWorker, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitConSubMainWorker.ResumeLayout(False)
        Me.tcMain.ResumeLayout(False)
        Me.tpDart.ResumeLayout(False)
        Me.SplitConDart.Panel1.ResumeLayout(False)
        Me.SplitConDart.Panel2.ResumeLayout(False)
        CType(Me.SplitConDart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitConDart.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.tpCondition.ResumeLayout(False)
        Me.SplitConConditionA.Panel1.ResumeLayout(False)
        Me.SplitConConditionA.Panel2.ResumeLayout(False)
        CType(Me.SplitConConditionA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitConConditionA.ResumeLayout(False)
        Me.SplitConConditionB.Panel1.ResumeLayout(False)
        Me.SplitConConditionB.Panel2.ResumeLayout(False)
        CType(Me.SplitConConditionB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitConConditionB.ResumeLayout(False)
        CType(Me.drvConditionList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvConditionStockList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpFav.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.dgvScreenNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.SplitConTradeInfo.Panel1.ResumeLayout(False)
        Me.SplitConTradeInfo.Panel2.ResumeLayout(False)
        CType(Me.SplitConTradeInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitConTradeInfo.ResumeLayout(False)
        CType(Me.dgvTradeInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvAccountInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbDart.ResumeLayout(False)
        CType(Me.dgvDart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.SplitConMain.Panel1.ResumeLayout(False)
        Me.SplitConMain.Panel2.ResumeLayout(False)
        CType(Me.SplitConMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitConMain.ResumeLayout(False)
        Me.SplitConStatus.Panel1.ResumeLayout(False)
        Me.SplitConStatus.Panel2.ResumeLayout(False)
        CType(Me.SplitConStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitConStatus.ResumeLayout(False)
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitConWorker.Panel1.ResumeLayout(False)
        Me.SplitConWorker.Panel2.ResumeLayout(False)
        CType(Me.SplitConWorker, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitConWorker.ResumeLayout(False)
        Me.SplitConRealDataMain.Panel1.ResumeLayout(False)
        Me.SplitConRealDataMain.Panel2.ResumeLayout(False)
        CType(Me.SplitConRealDataMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitConRealDataMain.ResumeLayout(False)
        Me.SplitConRealDataA.Panel1.ResumeLayout(False)
        Me.SplitConRealDataA.Panel2.ResumeLayout(False)
        CType(Me.SplitConRealDataA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitConRealDataA.ResumeLayout(False)
        Me.gbGetStock.ResumeLayout(False)
        CType(Me.dgvMyStock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitConRealDataB.Panel1.ResumeLayout(False)
        Me.SplitConRealDataB.Panel2.ResumeLayout(False)
        CType(Me.SplitConRealDataB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitConRealDataB.ResumeLayout(False)
        Me.gbCondition.ResumeLayout(False)
        CType(Me.dgvCondition, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbFav.ResumeLayout(False)
        CType(Me.dgvFav, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tpCondition As System.Windows.Forms.TabPage
    Friend WithEvents SplitConConditionA As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitConConditionB As System.Windows.Forms.SplitContainer
    Friend WithEvents drvConditionList As System.Windows.Forms.DataGridView
    Friend WithEvents CONDI_NAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CONDI_SEQ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ScreenNoCondition As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvConditionStockList As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn37 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn38 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn39 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn40 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn41 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn42 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn43 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn44 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn45 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn46 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn47 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn48 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnDartBuyCondition As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn33 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn35 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn36 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SplitConSubMain As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitConSubMainWorker As System.Windows.Forms.SplitContainer
    Friend WithEvents tcMain As System.Windows.Forms.TabControl
    Friend WithEvents tpDart As System.Windows.Forms.TabPage
    Friend WithEvents SplitConDart As System.Windows.Forms.SplitContainer
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnDartStart As System.Windows.Forms.Button
    Friend WithEvents lblFileNumDate As System.Windows.Forms.Label
    Friend WithEvents msktDartTimer As System.Windows.Forms.MaskedTextBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtAllowQty As System.Windows.Forms.TextBox
    Friend WithEvents UcDart1 As DartPrj.UcDart
    Friend WithEvents tpFav As System.Windows.Forms.TabPage
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents SplitConTradeInfo As System.Windows.Forms.SplitContainer
    Friend WithEvents dgvTradeInfo As System.Windows.Forms.DataGridView
    Friend WithEvents dgvAccountInfo As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn34 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn32 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VolumePower As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StartPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HighestPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LowestPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TradingTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PreDayBySymbol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents STOCK_CODE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ScreenNo_GetIn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents gbDart As System.Windows.Forms.GroupBox
    Friend WithEvents dgvDart As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents creator As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents title As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TradingVolume As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lossGainRate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SplitConMain As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitConStatus As System.Windows.Forms.SplitContainer
    Friend WithEvents UcMainStockVer2 As PaikRichStock.Common.ucMainStockVer2
    Friend WithEvents NumericUpDown2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cboTradeGb As System.Windows.Forms.ComboBox
    Friend WithEvents cboAccount As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents SplitConWorker As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitConRealDataMain As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitConRealDataA As System.Windows.Forms.SplitContainer
    Friend WithEvents gbGetStock As System.Windows.Forms.GroupBox
    Friend WithEvents dgvMyStock As System.Windows.Forms.DataGridView
    Friend WithEvents STOCK_NAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CurrentPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SplitConRealDataB As System.Windows.Forms.SplitContainer
    Friend WithEvents gbCondition As System.Windows.Forms.GroupBox
    Friend WithEvents dgvCondition As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn20 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn21 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn22 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn23 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn24 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents gbFav As System.Windows.Forms.GroupBox
    Friend WithEvents dgvFav As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn25 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn26 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn27 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn28 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn29 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn30 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn31 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UcHogaWindowNew As PaikRichStock.UcForm.ucHogaWindowNew
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents UcFavManage1 As PaikRichStock.UcForm.ucFavManage
    Friend WithEvents btnConditionFavAdd As System.Windows.Forms.Button
    Friend WithEvents dgvScreenNo As System.Windows.Forms.DataGridView
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents conMenuStop As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents conMenuStart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents UcAnalysisHogaWindow1 As PaikRichStock.UcForm.ucAnalysisHogaWindow
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents ContextMenuStrip2 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents conMenuFavAdd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
End Class