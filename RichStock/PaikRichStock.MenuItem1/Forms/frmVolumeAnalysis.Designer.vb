﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVolumeAnalysis
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
        Me.lblStockCode = New System.Windows.Forms.Label()
        Me.lblStockName = New System.Windows.Forms.Label()
        Me.UcFavList = New PaikRichStock.UcForm.ucFavList()
        Me.UcVolumeAnalysis1 = New PaikRichStock.UcForm.ucVolumeAnalysis()
        CType(Me.spcMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.spcMain.Panel1.SuspendLayout()
        Me.spcMain.Panel2.SuspendLayout()
        Me.spcMain.SuspendLayout()
        CType(Me.spcSubA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.spcSubA.Panel1.SuspendLayout()
        Me.spcSubA.Panel2.SuspendLayout()
        Me.spcSubA.SuspendLayout()
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
        Me.spcMain.Panel2.Controls.Add(Me.UcVolumeAnalysis1)
        Me.spcMain.Size = New System.Drawing.Size(1243, 639)
        Me.spcMain.SplitterDistance = 414
        Me.spcMain.TabIndex = 2
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
        Me.spcSubA.Panel1.Controls.Add(Me.lblStockCode)
        Me.spcSubA.Panel1.Controls.Add(Me.lblStockName)
        '
        'spcSubA.Panel2
        '
        Me.spcSubA.Panel2.Controls.Add(Me.UcFavList)
        Me.spcSubA.Size = New System.Drawing.Size(414, 639)
        Me.spcSubA.SplitterDistance = 32
        Me.spcSubA.TabIndex = 0
        '
        'lblStockCode
        '
        Me.lblStockCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStockCode.Location = New System.Drawing.Point(7, 5)
        Me.lblStockCode.Name = "lblStockCode"
        Me.lblStockCode.Size = New System.Drawing.Size(68, 20)
        Me.lblStockCode.TabIndex = 382
        Me.lblStockCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStockName
        '
        Me.lblStockName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStockName.Location = New System.Drawing.Point(78, 5)
        Me.lblStockName.Name = "lblStockName"
        Me.lblStockName.Size = New System.Drawing.Size(118, 20)
        Me.lblStockName.TabIndex = 381
        Me.lblStockName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'UcFavList
        '
        Me.UcFavList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcFavList.Font = New System.Drawing.Font("굴림체", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.UcFavList.Location = New System.Drawing.Point(0, 0)
        Me.UcFavList.Name = "UcFavList"
        Me.UcFavList.Size = New System.Drawing.Size(410, 599)
        Me.UcFavList.TabIndex = 0
        '
        'UcVolumeAnalysis1
        '
        Me.UcVolumeAnalysis1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcVolumeAnalysis1.Font = New System.Drawing.Font("굴림체", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.UcVolumeAnalysis1.Location = New System.Drawing.Point(0, 0)
        Me.UcVolumeAnalysis1.Name = "UcVolumeAnalysis1"
        Me.UcVolumeAnalysis1.Size = New System.Drawing.Size(821, 635)
        Me.UcVolumeAnalysis1.TabIndex = 0
        '
        'frmVolumeAnalysis
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 11.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1243, 639)
        Me.Controls.Add(Me.spcMain)
        Me.Font = New System.Drawing.Font("굴림체", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "frmVolumeAnalysis"
        Me.Text = "frmVolumeAnalysis"
        Me.spcMain.Panel1.ResumeLayout(False)
        Me.spcMain.Panel2.ResumeLayout(False)
        CType(Me.spcMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.spcMain.ResumeLayout(False)
        Me.spcSubA.Panel1.ResumeLayout(False)
        Me.spcSubA.Panel2.ResumeLayout(False)
        CType(Me.spcSubA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.spcSubA.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents spcMain As System.Windows.Forms.SplitContainer
    Friend WithEvents spcSubA As System.Windows.Forms.SplitContainer
    Friend WithEvents UcFavList As PaikRichStock.UcForm.ucFavList
    Friend WithEvents lblStockName As System.Windows.Forms.Label
    Friend WithEvents lblStockCode As System.Windows.Forms.Label
    Friend WithEvents UcVolumeAnalysis1 As PaikRichStock.UcForm.ucVolumeAnalysis
End Class
