﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSearchConditionList
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
        Me.spcMain = New System.Windows.Forms.SplitContainer()
        Me.spcSubA = New System.Windows.Forms.SplitContainer()
        Me.pnJogun = New System.Windows.Forms.Panel()
        Me.chkReload = New System.Windows.Forms.CheckBox()
        Me.btnGetDt = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.tcMain = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.UcCondition = New PaikRichStock.UcForm.ucCondition()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.UcFavManageVer2 = New PaikRichStock.UcForm.ucFavManageVer2()
        Me.UcVolumeBasicAnalysis = New PaikRichStock.UcForm.ucVolumeBasicAnalysis()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        CType(Me.spcMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.spcMain.Panel1.SuspendLayout()
        Me.spcMain.Panel2.SuspendLayout()
        Me.spcMain.SuspendLayout()
        CType(Me.spcSubA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.spcSubA.Panel1.SuspendLayout()
        Me.spcSubA.Panel2.SuspendLayout()
        Me.spcSubA.SuspendLayout()
        Me.pnJogun.SuspendLayout()
        Me.tcMain.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
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
        Me.spcMain.Panel1.Controls.Add(Me.spcSubA)
        '
        'spcMain.Panel2
        '
        Me.spcMain.Panel2.Controls.Add(Me.UcVolumeBasicAnalysis)
        Me.spcMain.Size = New System.Drawing.Size(1104, 662)
        Me.spcMain.SplitterDistance = 444
        Me.spcMain.TabIndex = 0
        '
        'spcSubA
        '
        Me.spcSubA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.spcSubA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spcSubA.Location = New System.Drawing.Point(0, 0)
        Me.spcSubA.Name = "spcSubA"
        Me.spcSubA.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'spcSubA.Panel1
        '
        Me.spcSubA.Panel1.Controls.Add(Me.pnJogun)
        '
        'spcSubA.Panel2
        '
        Me.spcSubA.Panel2.Controls.Add(Me.tcMain)
        Me.spcSubA.Size = New System.Drawing.Size(444, 662)
        Me.spcSubA.SplitterDistance = 52
        Me.spcSubA.TabIndex = 0
        '
        'pnJogun
        '
        Me.pnJogun.Controls.Add(Me.chkAll)
        Me.pnJogun.Controls.Add(Me.chkReload)
        Me.pnJogun.Controls.Add(Me.btnGetDt)
        Me.pnJogun.Controls.Add(Me.btnStart)
        Me.pnJogun.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnJogun.Location = New System.Drawing.Point(0, 0)
        Me.pnJogun.Name = "pnJogun"
        Me.pnJogun.Size = New System.Drawing.Size(440, 48)
        Me.pnJogun.TabIndex = 0
        '
        'chkReload
        '
        Me.chkReload.AutoSize = True
        Me.chkReload.Location = New System.Drawing.Point(169, 15)
        Me.chkReload.Name = "chkReload"
        Me.chkReload.Size = New System.Drawing.Size(60, 15)
        Me.chkReload.TabIndex = 2
        Me.chkReload.Text = "Reload"
        Me.chkReload.UseVisualStyleBackColor = True
        '
        'btnGetDt
        '
        Me.btnGetDt.Location = New System.Drawing.Point(88, 10)
        Me.btnGetDt.Name = "btnGetDt"
        Me.btnGetDt.Size = New System.Drawing.Size(75, 23)
        Me.btnGetDt.TabIndex = 1
        Me.btnGetDt.Text = "가져오기"
        Me.btnGetDt.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(7, 10)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(75, 23)
        Me.btnStart.TabIndex = 0
        Me.btnStart.Text = "분석"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'tcMain
        '
        Me.tcMain.Controls.Add(Me.TabPage1)
        Me.tcMain.Controls.Add(Me.TabPage2)
        Me.tcMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcMain.Location = New System.Drawing.Point(0, 0)
        Me.tcMain.Name = "tcMain"
        Me.tcMain.SelectedIndex = 0
        Me.tcMain.Size = New System.Drawing.Size(440, 602)
        Me.tcMain.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.UcCondition)
        Me.TabPage1.Location = New System.Drawing.Point(4, 21)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(432, 577)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'UcCondition
        '
        Me.UcCondition.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCondition.Font = New System.Drawing.Font("굴림체", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.UcCondition.Location = New System.Drawing.Point(3, 3)
        Me.UcCondition.Name = "UcCondition"
        Me.UcCondition.Size = New System.Drawing.Size(426, 571)
        Me.UcCondition.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.UcFavManageVer2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 21)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(432, 503)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'UcFavManageVer2
        '
        Me.UcFavManageVer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcFavManageVer2.Font = New System.Drawing.Font("굴림체", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.UcFavManageVer2.Location = New System.Drawing.Point(3, 3)
        Me.UcFavManageVer2.Name = "UcFavManageVer2"
        Me.UcFavManageVer2.Size = New System.Drawing.Size(426, 497)
        Me.UcFavManageVer2.TabIndex = 0
        '
        'UcVolumeBasicAnalysis
        '
        Me.UcVolumeBasicAnalysis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcVolumeBasicAnalysis.Font = New System.Drawing.Font("굴림체", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.UcVolumeBasicAnalysis.Location = New System.Drawing.Point(0, 0)
        Me.UcVolumeBasicAnalysis.Name = "UcVolumeBasicAnalysis"
        Me.UcVolumeBasicAnalysis.Size = New System.Drawing.Size(652, 658)
        Me.UcVolumeBasicAnalysis.TabIndex = 0
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.Location = New System.Drawing.Point(235, 15)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(60, 15)
        Me.chkAll.TabIndex = 3
        Me.chkAll.Text = "전종목"
        Me.chkAll.UseVisualStyleBackColor = True
        '
        'frmSearchConditionList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 11.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1104, 662)
        Me.Controls.Add(Me.spcMain)
        Me.Font = New System.Drawing.Font("굴림체", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Name = "frmSearchConditionList"
        Me.Text = "frmSearchConditionList"
        Me.spcMain.Panel1.ResumeLayout(False)
        Me.spcMain.Panel2.ResumeLayout(False)
        CType(Me.spcMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.spcMain.ResumeLayout(False)
        Me.spcSubA.Panel1.ResumeLayout(False)
        Me.spcSubA.Panel2.ResumeLayout(False)
        CType(Me.spcSubA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.spcSubA.ResumeLayout(False)
        Me.pnJogun.ResumeLayout(False)
        Me.pnJogun.PerformLayout()
        Me.tcMain.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents spcMain As System.Windows.Forms.SplitContainer
    Friend WithEvents spcSubA As System.Windows.Forms.SplitContainer
    Friend WithEvents pnJogun As System.Windows.Forms.Panel
    Friend WithEvents UcCondition As PaikRichStock.UcForm.ucCondition
    Friend WithEvents tcMain As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents UcFavManageVer2 As PaikRichStock.UcForm.ucFavManageVer2
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents UcVolumeBasicAnalysis As PaikRichStock.UcForm.ucVolumeBasicAnalysis
    Friend WithEvents btnGetDt As System.Windows.Forms.Button
    Friend WithEvents chkReload As System.Windows.Forms.CheckBox
    Friend WithEvents chkAll As System.Windows.Forms.CheckBox
End Class