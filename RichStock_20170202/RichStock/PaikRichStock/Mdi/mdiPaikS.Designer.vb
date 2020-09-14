<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class mdiPaikS
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
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.mnuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuItem1100 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuItem1200 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuItem1300 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuItem1400 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuItem1500 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuItem1600 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuItem1700 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuItem1800 = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnMain = New System.Windows.Forms.Panel()
        Me.spcMain = New System.Windows.Forms.SplitContainer()
        Me.pnAccount = New System.Windows.Forms.Panel()
        Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboTradeGb = New System.Windows.Forms.ComboBox()
        Me.cboAccount = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.UcMainStockVer2 = New PaikRichStock.Common.ucMainStockVer2()
        Me.cmControl = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmView = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmHidden = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.pnMain.SuspendLayout()
        CType(Me.spcMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.spcMain.Panel1.SuspendLayout()
        Me.spcMain.Panel2.SuspendLayout()
        Me.spcMain.SuspendLayout()
        Me.pnAccount.SuspendLayout()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmControl.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuItem1})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Padding = New System.Windows.Forms.Padding(7, 2, 0, 2)
        Me.MenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.MenuStrip.Size = New System.Drawing.Size(1244, 24)
        Me.MenuStrip.TabIndex = 5
        Me.MenuStrip.Text = "MenuStrip"
        '
        'mnuItem1
        '
        Me.mnuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuItem1100, Me.mnuItem1200, Me.mnuItem1300, Me.mnuItem1400, Me.mnuItem1500, Me.mnuItem1600, Me.mnuItem1700, Me.mnuItem1800})
        Me.mnuItem1.Name = "mnuItem1"
        Me.mnuItem1.Size = New System.Drawing.Size(43, 20)
        Me.mnuItem1.Text = "메인"
        '
        'mnuItem1100
        '
        Me.mnuItem1100.Name = "mnuItem1100"
        Me.mnuItem1100.Size = New System.Drawing.Size(152, 22)
        Me.mnuItem1100.Text = "Trade"
        '
        'mnuItem1200
        '
        Me.mnuItem1200.Name = "mnuItem1200"
        Me.mnuItem1200.Size = New System.Drawing.Size(152, 22)
        Me.mnuItem1200.Text = "Analysis"
        '
        'mnuItem1300
        '
        Me.mnuItem1300.Name = "mnuItem1300"
        Me.mnuItem1300.Size = New System.Drawing.Size(152, 22)
        Me.mnuItem1300.Text = "FavStock"
        '
        'mnuItem1400
        '
        Me.mnuItem1400.Name = "mnuItem1400"
        Me.mnuItem1400.Size = New System.Drawing.Size(152, 22)
        Me.mnuItem1400.Text = "Mini"
        '
        'mnuItem1500
        '
        Me.mnuItem1500.Name = "mnuItem1500"
        Me.mnuItem1500.Size = New System.Drawing.Size(152, 22)
        Me.mnuItem1500.Text = "test"
        '
        'mnuItem1600
        '
        Me.mnuItem1600.Name = "mnuItem1600"
        Me.mnuItem1600.Size = New System.Drawing.Size(152, 22)
        Me.mnuItem1600.Text = "test2"
        '
        'mnuItem1700
        '
        Me.mnuItem1700.Name = "mnuItem1700"
        Me.mnuItem1700.Size = New System.Drawing.Size(152, 22)
        Me.mnuItem1700.Text = "test3"
        '
        'mnuItem1800
        '
        Me.mnuItem1800.Name = "mnuItem1800"
        Me.mnuItem1800.Size = New System.Drawing.Size(152, 22)
        Me.mnuItem1800.Text = "test4"
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 529)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Padding = New System.Windows.Forms.Padding(1, 0, 16, 0)
        Me.StatusStrip.Size = New System.Drawing.Size(1244, 22)
        Me.StatusStrip.TabIndex = 7
        Me.StatusStrip.Text = "StatusStrip"
        '
        'ToolStripStatusLabel
        '
        Me.ToolStripStatusLabel.Name = "ToolStripStatusLabel"
        Me.ToolStripStatusLabel.Size = New System.Drawing.Size(31, 17)
        Me.ToolStripStatusLabel.Text = "상태"
        '
        'pnMain
        '
        Me.pnMain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnMain.Controls.Add(Me.spcMain)
        Me.pnMain.Location = New System.Drawing.Point(0, 491)
        Me.pnMain.Name = "pnMain"
        Me.pnMain.Size = New System.Drawing.Size(1244, 35)
        Me.pnMain.TabIndex = 11
        '
        'spcMain
        '
        Me.spcMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.spcMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spcMain.Location = New System.Drawing.Point(0, 0)
        Me.spcMain.Name = "spcMain"
        '
        'spcMain.Panel1
        '
        Me.spcMain.Panel1.Controls.Add(Me.pnAccount)
        '
        'spcMain.Panel2
        '
        Me.spcMain.Panel2.Controls.Add(Me.UcMainStockVer2)
        Me.spcMain.Size = New System.Drawing.Size(1244, 35)
        Me.spcMain.SplitterDistance = 548
        Me.spcMain.TabIndex = 0
        '
        'pnAccount
        '
        Me.pnAccount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnAccount.Controls.Add(Me.NumericUpDown2)
        Me.pnAccount.Controls.Add(Me.Label4)
        Me.pnAccount.Controls.Add(Me.Label6)
        Me.pnAccount.Controls.Add(Me.cboTradeGb)
        Me.pnAccount.Controls.Add(Me.cboAccount)
        Me.pnAccount.Controls.Add(Me.Label3)
        Me.pnAccount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnAccount.Location = New System.Drawing.Point(0, 0)
        Me.pnAccount.Name = "pnAccount"
        Me.pnAccount.Size = New System.Drawing.Size(544, 31)
        Me.pnAccount.TabIndex = 0
        '
        'NumericUpDown2
        '
        Me.NumericUpDown2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.NumericUpDown2.Increment = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.NumericUpDown2.Location = New System.Drawing.Point(444, 2)
        Me.NumericUpDown2.Maximum = New Decimal(New Integer() {10000000, 0, 0, 0})
        Me.NumericUpDown2.Name = "NumericUpDown2"
        Me.NumericUpDown2.Size = New System.Drawing.Size(95, 21)
        Me.NumericUpDown2.TabIndex = 362
        Me.NumericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NumericUpDown2.ThousandsSeparator = True
        Me.NumericUpDown2.Value = New Decimal(New Integer() {1000000, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.GhostWhite
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Location = New System.Drawing.Point(-1, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 21)
        Me.Label4.TabIndex = 358
        Me.Label4.Text = "계좌번호"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.GhostWhite
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Location = New System.Drawing.Point(372, 2)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 21)
        Me.Label6.TabIndex = 361
        Me.Label6.Text = "금액설정"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboTradeGb
        '
        Me.cboTradeGb.FormattingEnabled = True
        Me.cboTradeGb.Items.AddRange(New Object() {"00 - 지정가", "03 - 시장가", "05 - 조건부지정가", "06 - 최유리지정가", "07 - 최우선지정가", "10 - 지정가IOC", "13 - 시장가IOC", "16 - 최유리IOC", "20 - 지정가FOK", "23 - 시장가FOK", "26 - 최유리FOK", "61 - 장전시간외종가", "62 - 시간외단일가매매", "81 - 장후시간외종가"})
        Me.cboTradeGb.Location = New System.Drawing.Point(245, 2)
        Me.cboTradeGb.Name = "cboTradeGb"
        Me.cboTradeGb.Size = New System.Drawing.Size(121, 20)
        Me.cboTradeGb.TabIndex = 359
        Me.cboTradeGb.Text = "00 - 지정가"
        '
        'cboAccount
        '
        Me.cboAccount.FormattingEnabled = True
        Me.cboAccount.Location = New System.Drawing.Point(74, 3)
        Me.cboAccount.Name = "cboAccount"
        Me.cboAccount.Size = New System.Drawing.Size(90, 20)
        Me.cboAccount.TabIndex = 357
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.GhostWhite
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Location = New System.Drawing.Point(170, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 21)
        Me.Label3.TabIndex = 360
        Me.Label3.Text = "주문구분"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'UcMainStockVer2
        '
        Me.UcMainStockVer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcMainStockVer2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.UcMainStockVer2.Location = New System.Drawing.Point(0, 0)
        Me.UcMainStockVer2.LoggerStartOption = False
        Me.UcMainStockVer2.Name = "UcMainStockVer2"
        Me.UcMainStockVer2.Size = New System.Drawing.Size(688, 31)
        Me.UcMainStockVer2.TabIndex = 9
        '
        'cmControl
        '
        Me.cmControl.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmView, Me.tsmHidden})
        Me.cmControl.Name = "cmControl"
        Me.cmControl.Size = New System.Drawing.Size(111, 48)
        '
        'tsmView
        '
        Me.tsmView.Name = "tsmView"
        Me.tsmView.Size = New System.Drawing.Size(152, 22)
        Me.tsmView.Text = "보이기"
        '
        'tsmHidden
        '
        Me.tsmHidden.Name = "tsmHidden"
        Me.tsmHidden.Size = New System.Drawing.Size(152, 22)
        Me.tsmHidden.Text = "감추기"
        '
        'mdiPaikS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1244, 551)
        Me.ContextMenuStrip = Me.cmControl
        Me.Controls.Add(Me.pnMain)
        Me.Controls.Add(Me.MenuStrip)
        Me.Controls.Add(Me.StatusStrip)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip
        Me.Name = "mdiPaikS"
        Me.Text = "mdiPaikS"
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.pnMain.ResumeLayout(False)
        Me.spcMain.Panel1.ResumeLayout(False)
        Me.spcMain.Panel2.ResumeLayout(False)
        CType(Me.spcMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.spcMain.ResumeLayout(False)
        Me.pnAccount.ResumeLayout(False)
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmControl.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuItem1100 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuItem1200 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuItem1300 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuItem1400 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UcMainStockVer2 As PaikRichStock.Common.ucMainStockVer2
    Friend WithEvents pnMain As System.Windows.Forms.Panel
    Friend WithEvents spcMain As System.Windows.Forms.SplitContainer
    Friend WithEvents pnAccount As System.Windows.Forms.Panel
    Friend WithEvents NumericUpDown2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cboTradeGb As System.Windows.Forms.ComboBox
    Friend WithEvents cboAccount As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents mnuItem1500 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuItem1600 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuItem1700 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuItem1800 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmControl As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmView As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmHidden As System.Windows.Forms.ToolStripMenuItem

End Class
