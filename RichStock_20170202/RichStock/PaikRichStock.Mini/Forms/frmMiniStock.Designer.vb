﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMiniStock
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMiniStock))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.tabControl = New System.Windows.Forms.TabControl()
        Me.tabPage1 = New System.Windows.Forms.TabPage()
        Me.txtMemo = New System.Windows.Forms.TextBox()
        Me.tabPage2 = New System.Windows.Forms.TabPage()
        Me.txtGiho = New System.Windows.Forms.TextBox()
        Me.tabPage3 = New System.Windows.Forms.TabPage()
        Me.txtScript = New System.Windows.Forms.TextBox()
        Me.tabPage4 = New System.Windows.Forms.TabPage()
        Me.pn = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtIndex = New System.Windows.Forms.TextBox()
        Me.txtSpread = New System.Windows.Forms.TextBox()
        Me.btnSpread = New System.Windows.Forms.Button()
        Me.btnSp = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnCode = New System.Windows.Forms.Button()
        Me.txtResult = New System.Windows.Forms.TextBox()
        Me.txtOriginal = New System.Windows.Forms.TextBox()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.UcFavList1 = New PaikRichStock.UcForm.ucFavList()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkTop = New System.Windows.Forms.CheckBox()
        Me.btnPre = New System.Windows.Forms.Button()
        Me.btnChange = New System.Windows.Forms.Button()
        Me.btnTray = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.UcMainStockVer2 = New PaikRichStock.Common.ucMainStockVer2()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.tabControl.SuspendLayout()
        Me.tabPage1.SuspendLayout()
        Me.tabPage2.SuspendLayout()
        Me.tabPage3.SuspendLayout()
        Me.tabPage4.SuspendLayout()
        Me.pn.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.SplitContainer4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.tabControl)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Size = New System.Drawing.Size(687, 510)
        Me.SplitContainer1.SplitterDistance = 460
        Me.SplitContainer1.TabIndex = 7
        '
        'tabControl
        '
        Me.tabControl.Controls.Add(Me.tabPage1)
        Me.tabControl.Controls.Add(Me.tabPage2)
        Me.tabControl.Controls.Add(Me.tabPage3)
        Me.tabControl.Controls.Add(Me.tabPage4)
        Me.tabControl.Controls.Add(Me.TabPage5)
        Me.tabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabControl.ItemSize = New System.Drawing.Size(68, 25)
        Me.tabControl.Location = New System.Drawing.Point(0, 0)
        Me.tabControl.Name = "tabControl"
        Me.tabControl.SelectedIndex = 0
        Me.tabControl.Size = New System.Drawing.Size(687, 460)
        Me.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.tabControl.TabIndex = 3
        '
        'tabPage1
        '
        Me.tabPage1.Controls.Add(Me.txtMemo)
        Me.tabPage1.Location = New System.Drawing.Point(4, 29)
        Me.tabPage1.Name = "tabPage1"
        Me.tabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPage1.Size = New System.Drawing.Size(679, 427)
        Me.tabPage1.TabIndex = 0
        Me.tabPage1.Text = "Memo"
        Me.tabPage1.UseVisualStyleBackColor = True
        '
        'txtMemo
        '
        Me.txtMemo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMemo.Font = New System.Drawing.Font("맑은 고딕", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtMemo.Location = New System.Drawing.Point(3, 3)
        Me.txtMemo.MaxLength = 0
        Me.txtMemo.Multiline = True
        Me.txtMemo.Name = "txtMemo"
        Me.txtMemo.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtMemo.Size = New System.Drawing.Size(673, 421)
        Me.txtMemo.TabIndex = 0
        '
        'tabPage2
        '
        Me.tabPage2.Controls.Add(Me.txtGiho)
        Me.tabPage2.Location = New System.Drawing.Point(4, 29)
        Me.tabPage2.Name = "tabPage2"
        Me.tabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPage2.Size = New System.Drawing.Size(679, 427)
        Me.tabPage2.TabIndex = 1
        Me.tabPage2.Text = "특수기호"
        Me.tabPage2.UseVisualStyleBackColor = True
        '
        'txtGiho
        '
        Me.txtGiho.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtGiho.Location = New System.Drawing.Point(3, 3)
        Me.txtGiho.MaxLength = 0
        Me.txtGiho.Multiline = True
        Me.txtGiho.Name = "txtGiho"
        Me.txtGiho.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtGiho.Size = New System.Drawing.Size(673, 421)
        Me.txtGiho.TabIndex = 1
        Me.txtGiho.Text = resources.GetString("txtGiho.Text")
        '
        'tabPage3
        '
        Me.tabPage3.Controls.Add(Me.txtScript)
        Me.tabPage3.Location = New System.Drawing.Point(4, 29)
        Me.tabPage3.Name = "tabPage3"
        Me.tabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPage3.Size = New System.Drawing.Size(679, 427)
        Me.tabPage3.TabIndex = 2
        Me.tabPage3.Text = "Script"
        Me.tabPage3.UseVisualStyleBackColor = True
        '
        'txtScript
        '
        Me.txtScript.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtScript.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtScript.Location = New System.Drawing.Point(3, 3)
        Me.txtScript.MaxLength = 0
        Me.txtScript.Multiline = True
        Me.txtScript.Name = "txtScript"
        Me.txtScript.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtScript.Size = New System.Drawing.Size(673, 421)
        Me.txtScript.TabIndex = 2
        Me.txtScript.Text = resources.GetString("txtScript.Text")
        '
        'tabPage4
        '
        Me.tabPage4.BackColor = System.Drawing.Color.Transparent
        Me.tabPage4.Controls.Add(Me.pn)
        Me.tabPage4.Controls.Add(Me.btnSp)
        Me.tabPage4.Controls.Add(Me.btnClear)
        Me.tabPage4.Controls.Add(Me.btnCode)
        Me.tabPage4.Controls.Add(Me.txtResult)
        Me.tabPage4.Controls.Add(Me.txtOriginal)
        Me.tabPage4.Location = New System.Drawing.Point(4, 29)
        Me.tabPage4.Name = "tabPage4"
        Me.tabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPage4.Size = New System.Drawing.Size(679, 427)
        Me.tabPage4.TabIndex = 3
        Me.tabPage4.Text = "SP용"
        Me.tabPage4.UseVisualStyleBackColor = True
        '
        'pn
        '
        Me.pn.BackColor = System.Drawing.Color.Beige
        Me.pn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pn.Controls.Add(Me.Label2)
        Me.pn.Controls.Add(Me.Label1)
        Me.pn.Controls.Add(Me.txtIndex)
        Me.pn.Controls.Add(Me.txtSpread)
        Me.pn.Controls.Add(Me.btnSpread)
        Me.pn.Location = New System.Drawing.Point(189, 418)
        Me.pn.Name = "pn"
        Me.pn.Size = New System.Drawing.Size(199, 41)
        Me.pn.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(1, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 12)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Index"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(20, 12)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Sp"
        '
        'txtIndex
        '
        Me.txtIndex.Location = New System.Drawing.Point(32, 20)
        Me.txtIndex.Name = "txtIndex"
        Me.txtIndex.Size = New System.Drawing.Size(73, 21)
        Me.txtIndex.TabIndex = 0
        '
        'txtSpread
        '
        Me.txtSpread.Location = New System.Drawing.Point(32, 0)
        Me.txtSpread.Name = "txtSpread"
        Me.txtSpread.Size = New System.Drawing.Size(73, 21)
        Me.txtSpread.TabIndex = 0
        '
        'btnSpread
        '
        Me.btnSpread.Location = New System.Drawing.Point(126, 3)
        Me.btnSpread.Name = "btnSpread"
        Me.btnSpread.Size = New System.Drawing.Size(68, 29)
        Me.btnSpread.TabIndex = 6
        Me.btnSpread.Text = "스프레드"
        Me.btnSpread.UseVisualStyleBackColor = True
        '
        'btnSp
        '
        Me.btnSp.Location = New System.Drawing.Point(120, 422)
        Me.btnSp.Name = "btnSp"
        Me.btnSp.Size = New System.Drawing.Size(64, 21)
        Me.btnSp.TabIndex = 6
        Me.btnSp.Text = "sp용"
        Me.btnSp.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(21, 422)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(30, 21)
        Me.btnClear.TabIndex = 5
        Me.btnClear.Text = "C"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnCode
        '
        Me.btnCode.Location = New System.Drawing.Point(54, 422)
        Me.btnCode.Name = "btnCode"
        Me.btnCode.Size = New System.Drawing.Size(64, 21)
        Me.btnCode.TabIndex = 5
        Me.btnCode.Text = "코드용"
        Me.btnCode.UseVisualStyleBackColor = True
        '
        'txtResult
        '
        Me.txtResult.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtResult.Location = New System.Drawing.Point(9, 209)
        Me.txtResult.Multiline = True
        Me.txtResult.Name = "txtResult"
        Me.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtResult.Size = New System.Drawing.Size(426, 190)
        Me.txtResult.TabIndex = 3
        '
        'txtOriginal
        '
        Me.txtOriginal.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtOriginal.Location = New System.Drawing.Point(9, 6)
        Me.txtOriginal.Multiline = True
        Me.txtOriginal.Name = "txtOriginal"
        Me.txtOriginal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOriginal.Size = New System.Drawing.Size(426, 198)
        Me.txtOriginal.TabIndex = 4
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.SplitContainer2)
        Me.TabPage5.Location = New System.Drawing.Point(4, 29)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(679, 427)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "TabPage5"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.UcMainStockVer2)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Size = New System.Drawing.Size(673, 421)
        Me.SplitContainer2.SplitterDistance = 37
        Me.SplitContainer2.TabIndex = 1
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.SplitContainer4)
        Me.SplitContainer3.Size = New System.Drawing.Size(673, 380)
        Me.SplitContainer3.SplitterDistance = 270
        Me.SplitContainer3.TabIndex = 0
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.UcFavList1)
        Me.SplitContainer4.Size = New System.Drawing.Size(399, 380)
        Me.SplitContainer4.SplitterDistance = 226
        Me.SplitContainer4.TabIndex = 0
        '
        'UcFavList1
        '
        Me.UcFavList1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcFavList1.Font = New System.Drawing.Font("굴림체", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.UcFavList1.Location = New System.Drawing.Point(0, 0)
        Me.UcFavList1.Name = "UcFavList1"
        Me.UcFavList1.Size = New System.Drawing.Size(399, 226)
        Me.UcFavList1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkTop)
        Me.Panel1.Controls.Add(Me.btnPre)
        Me.Panel1.Controls.Add(Me.btnChange)
        Me.Panel1.Controls.Add(Me.btnTray)
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Controls.Add(Me.btnNext)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(687, 46)
        Me.Panel1.TabIndex = 5
        '
        'chkTop
        '
        Me.chkTop.AutoSize = True
        Me.chkTop.Location = New System.Drawing.Point(213, 14)
        Me.chkTop.Name = "chkTop"
        Me.chkTop.Size = New System.Drawing.Size(64, 16)
        Me.chkTop.TabIndex = 3
        Me.chkTop.Text = "항상 위"
        Me.chkTop.UseVisualStyleBackColor = True
        '
        'btnPre
        '
        Me.btnPre.ImageIndex = 0
        Me.btnPre.Location = New System.Drawing.Point(3, 3)
        Me.btnPre.Name = "btnPre"
        Me.btnPre.Size = New System.Drawing.Size(36, 36)
        Me.btnPre.TabIndex = 1
        Me.btnPre.UseVisualStyleBackColor = True
        '
        'btnChange
        '
        Me.btnChange.ImageKey = "gnome-monitor.ico"
        Me.btnChange.Location = New System.Drawing.Point(171, 4)
        Me.btnChange.Name = "btnChange"
        Me.btnChange.Size = New System.Drawing.Size(36, 36)
        Me.btnChange.TabIndex = 1
        Me.btnChange.UseVisualStyleBackColor = True
        '
        'btnTray
        '
        Me.btnTray.ImageIndex = 2
        Me.btnTray.Location = New System.Drawing.Point(87, 4)
        Me.btnTray.Name = "btnTray"
        Me.btnTray.Size = New System.Drawing.Size(36, 36)
        Me.btnTray.TabIndex = 1
        Me.btnTray.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.ImageKey = "exit.ico"
        Me.btnExit.Location = New System.Drawing.Point(129, 4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(36, 36)
        Me.btnExit.TabIndex = 1
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.ImageIndex = 1
        Me.btnNext.Location = New System.Drawing.Point(45, 3)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(36, 36)
        Me.btnNext.TabIndex = 1
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'UcMainStockVer2
        '
        Me.UcMainStockVer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcMainStockVer2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.UcMainStockVer2.Location = New System.Drawing.Point(0, 0)
        Me.UcMainStockVer2.Name = "UcMainStockVer2"
        Me.UcMainStockVer2.Size = New System.Drawing.Size(673, 37)
        Me.UcMainStockVer2.TabIndex = 0
        '
        'frmMiniStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(687, 510)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmMiniStock"
        Me.Text = "미니"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.tabControl.ResumeLayout(False)
        Me.tabPage1.ResumeLayout(False)
        Me.tabPage1.PerformLayout()
        Me.tabPage2.ResumeLayout(False)
        Me.tabPage2.PerformLayout()
        Me.tabPage3.ResumeLayout(False)
        Me.tabPage3.PerformLayout()
        Me.tabPage4.ResumeLayout(False)
        Me.tabPage4.PerformLayout()
        Me.pn.ResumeLayout(False)
        Me.pn.PerformLayout()
        Me.TabPage5.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        CType(Me.SplitContainer4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents tabControl As System.Windows.Forms.TabControl
    Friend WithEvents tabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents txtMemo As System.Windows.Forms.TextBox
    Friend WithEvents tabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents txtGiho As System.Windows.Forms.TextBox
    Friend WithEvents tabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents txtScript As System.Windows.Forms.TextBox
    Friend WithEvents tabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents pn As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtIndex As System.Windows.Forms.TextBox
    Friend WithEvents txtSpread As System.Windows.Forms.TextBox
    Friend WithEvents btnSpread As System.Windows.Forms.Button
    Friend WithEvents btnSp As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnCode As System.Windows.Forms.Button
    Friend WithEvents txtResult As System.Windows.Forms.TextBox
    Friend WithEvents txtOriginal As System.Windows.Forms.TextBox
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkTop As System.Windows.Forms.CheckBox
    Friend WithEvents btnPre As System.Windows.Forms.Button
    Friend WithEvents btnChange As System.Windows.Forms.Button
    Friend WithEvents btnTray As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents UcFavList1 As PaikRichStock.UcForm.ucFavList
    Friend WithEvents UcMainStockVer2 As PaikRichStock.Common.ucMainStockVer2
End Class
