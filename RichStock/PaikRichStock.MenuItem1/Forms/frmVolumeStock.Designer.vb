<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVolumeStock
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
        Me.tcMain = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.spcSubA = New System.Windows.Forms.SplitContainer()
        Me.dgvVolumeA = New System.Windows.Forms.DataGridView()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.UcFavList = New PaikRichStock.UcForm.ucFavList()
        Me.UcCondition = New PaikRichStock.UcForm.ucCondition()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.spcMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.spcMain.Panel1.SuspendLayout()
        Me.spcMain.Panel2.SuspendLayout()
        Me.spcMain.SuspendLayout()
        Me.tcMain.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.spcSubA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.spcSubA.Panel1.SuspendLayout()
        Me.spcSubA.Panel2.SuspendLayout()
        Me.spcSubA.SuspendLayout()
        CType(Me.dgvVolumeA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.spcMain.Panel1.Controls.Add(Me.tcMain)
        '
        'spcMain.Panel2
        '
        Me.spcMain.Panel2.Controls.Add(Me.spcSubA)
        Me.spcMain.Size = New System.Drawing.Size(1082, 580)
        Me.spcMain.SplitterDistance = 360
        Me.spcMain.TabIndex = 0
        '
        'tcMain
        '
        Me.tcMain.Controls.Add(Me.TabPage1)
        Me.tcMain.Controls.Add(Me.TabPage2)
        Me.tcMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcMain.Location = New System.Drawing.Point(0, 0)
        Me.tcMain.Name = "tcMain"
        Me.tcMain.SelectedIndex = 0
        Me.tcMain.Size = New System.Drawing.Size(356, 576)
        Me.tcMain.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.UcFavList)
        Me.TabPage1.Location = New System.Drawing.Point(4, 21)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(348, 551)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.UcCondition)
        Me.TabPage2.Location = New System.Drawing.Point(4, 21)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(348, 551)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
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
        Me.spcSubA.Panel1.Controls.Add(Me.Button1)
        Me.spcSubA.Panel1.Controls.Add(Me.dgvVolumeA)
        '
        'spcSubA.Panel2
        '
        Me.spcSubA.Panel2.Controls.Add(Me.DataGridView1)
        Me.spcSubA.Size = New System.Drawing.Size(718, 580)
        Me.spcSubA.SplitterDistance = 283
        Me.spcSubA.TabIndex = 0
        '
        'dgvVolumeA
        '
        Me.dgvVolumeA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvVolumeA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvVolumeA.Location = New System.Drawing.Point(0, 0)
        Me.dgvVolumeA.Name = "dgvVolumeA"
        Me.dgvVolumeA.RowTemplate.Height = 23
        Me.dgvVolumeA.Size = New System.Drawing.Size(714, 279)
        Me.dgvVolumeA.TabIndex = 0
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.Size = New System.Drawing.Size(714, 289)
        Me.DataGridView1.TabIndex = 1
        '
        'UcFavList
        '
        Me.UcFavList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcFavList.Font = New System.Drawing.Font("굴림체", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.UcFavList.Location = New System.Drawing.Point(3, 3)
        Me.UcFavList.Name = "UcFavList"
        Me.UcFavList.Size = New System.Drawing.Size(342, 545)
        Me.UcFavList.TabIndex = 0
        '
        'UcCondition
        '
        Me.UcCondition.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCondition.Font = New System.Drawing.Font("굴림체", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.UcCondition.Location = New System.Drawing.Point(3, 3)
        Me.UcCondition.Name = "UcCondition"
        Me.UcCondition.Size = New System.Drawing.Size(342, 544)
        Me.UcCondition.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(3, 10)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmVolumeStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 11.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1082, 580)
        Me.Controls.Add(Me.spcMain)
        Me.Font = New System.Drawing.Font("굴림체", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "frmVolumeStock"
        Me.Text = "frmVolumeStock"
        Me.spcMain.Panel1.ResumeLayout(False)
        Me.spcMain.Panel2.ResumeLayout(False)
        CType(Me.spcMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.spcMain.ResumeLayout(False)
        Me.tcMain.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.spcSubA.Panel1.ResumeLayout(False)
        Me.spcSubA.Panel2.ResumeLayout(False)
        CType(Me.spcSubA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.spcSubA.ResumeLayout(False)
        CType(Me.dgvVolumeA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents spcMain As System.Windows.Forms.SplitContainer
    Friend WithEvents tcMain As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents UcFavList As PaikRichStock.UcForm.ucFavList
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents spcSubA As System.Windows.Forms.SplitContainer
    Friend WithEvents dgvVolumeA As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents UcCondition As PaikRichStock.UcForm.ucCondition
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
