<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBatchTradeInfo
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.proBarOpt10059Qty = New System.Windows.Forms.ProgressBar()
        Me.proBarOpt10059Price = New System.Windows.Forms.ProgressBar()
        Me.proBarOpt10081 = New System.Windows.Forms.ProgressBar()
        Me.btnStartJob = New System.Windows.Forms.Button()
        Me.lblMsg = New System.Windows.Forms.Label()
        Me.chkDay = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.proBarOpt10060Qty = New System.Windows.Forms.ProgressBar()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.proBarOpt10060Price = New System.Windows.Forms.ProgressBar()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.proBarOpt10078 = New System.Windows.Forms.ProgressBar()
        Me.gbFcode = New System.Windows.Forms.GroupBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.dgvFCode = New System.Windows.Forms.DataGridView()
        Me.lblGroupName = New System.Windows.Forms.Label()
        Me.gbFcode.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dgvFCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "OPT10059_QTY"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 12)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "OPT10059_PRICE"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 12)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "OPT10081"
        '
        'proBarOpt10059Qty
        '
        Me.proBarOpt10059Qty.Location = New System.Drawing.Point(121, 12)
        Me.proBarOpt10059Qty.Name = "proBarOpt10059Qty"
        Me.proBarOpt10059Qty.Size = New System.Drawing.Size(688, 23)
        Me.proBarOpt10059Qty.TabIndex = 3
        '
        'proBarOpt10059Price
        '
        Me.proBarOpt10059Price.Location = New System.Drawing.Point(121, 41)
        Me.proBarOpt10059Price.Name = "proBarOpt10059Price"
        Me.proBarOpt10059Price.Size = New System.Drawing.Size(688, 23)
        Me.proBarOpt10059Price.TabIndex = 4
        '
        'proBarOpt10081
        '
        Me.proBarOpt10081.Location = New System.Drawing.Point(121, 70)
        Me.proBarOpt10081.Name = "proBarOpt10081"
        Me.proBarOpt10081.Size = New System.Drawing.Size(688, 23)
        Me.proBarOpt10081.TabIndex = 5
        '
        'btnStartJob
        '
        Me.btnStartJob.Location = New System.Drawing.Point(733, 192)
        Me.btnStartJob.Name = "btnStartJob"
        Me.btnStartJob.Size = New System.Drawing.Size(75, 23)
        Me.btnStartJob.TabIndex = 6
        Me.btnStartJob.Text = "배치작업"
        Me.btnStartJob.UseVisualStyleBackColor = True
        '
        'lblMsg
        '
        Me.lblMsg.AutoSize = True
        Me.lblMsg.Location = New System.Drawing.Point(12, 196)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(42, 12)
        Me.lblMsg.TabIndex = 7
        Me.lblMsg.Text = "Label4"
        '
        'chkDay
        '
        Me.chkDay.AutoSize = True
        Me.chkDay.Location = New System.Drawing.Point(609, 197)
        Me.chkDay.Name = "chkDay"
        Me.chkDay.Size = New System.Drawing.Size(100, 16)
        Me.chkDay.TabIndex = 8
        Me.chkDay.Text = "최근일자 포함"
        Me.chkDay.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 107)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 12)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "OPT10060_QTY"
        '
        'proBarOpt10060Qty
        '
        Me.proBarOpt10060Qty.Location = New System.Drawing.Point(121, 101)
        Me.proBarOpt10060Qty.Name = "proBarOpt10060Qty"
        Me.proBarOpt10060Qty.Size = New System.Drawing.Size(688, 23)
        Me.proBarOpt10060Qty.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 138)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(102, 12)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "OPT10060_PRICE"
        '
        'proBarOpt10060Price
        '
        Me.proBarOpt10060Price.Location = New System.Drawing.Point(121, 133)
        Me.proBarOpt10060Price.Name = "proBarOpt10060Price"
        Me.proBarOpt10060Price.Size = New System.Drawing.Size(688, 23)
        Me.proBarOpt10060Price.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 167)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 12)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "OPT10078"
        '
        'proBarOpt10078
        '
        Me.proBarOpt10078.Location = New System.Drawing.Point(121, 162)
        Me.proBarOpt10078.Name = "proBarOpt10078"
        Me.proBarOpt10078.Size = New System.Drawing.Size(688, 23)
        Me.proBarOpt10078.TabIndex = 14
        '
        'gbFcode
        '
        Me.gbFcode.Controls.Add(Me.SplitContainer1)
        Me.gbFcode.Location = New System.Drawing.Point(11, 226)
        Me.gbFcode.Name = "gbFcode"
        Me.gbFcode.Size = New System.Drawing.Size(798, 288)
        Me.gbFcode.TabIndex = 15
        Me.gbFcode.TabStop = False
        Me.gbFcode.Text = "GroupBox1"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 17)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvFCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblGroupName)
        Me.SplitContainer1.Size = New System.Drawing.Size(792, 268)
        Me.SplitContainer1.SplitterDistance = 264
        Me.SplitContainer1.TabIndex = 0
        '
        'dgvFCode
        '
        Me.dgvFCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvFCode.Location = New System.Drawing.Point(0, 0)
        Me.dgvFCode.Name = "dgvFCode"
        Me.dgvFCode.RowTemplate.Height = 23
        Me.dgvFCode.Size = New System.Drawing.Size(264, 268)
        Me.dgvFCode.TabIndex = 1
        '
        'lblGroupName
        '
        Me.lblGroupName.AutoSize = True
        Me.lblGroupName.Location = New System.Drawing.Point(14, 10)
        Me.lblGroupName.Name = "lblGroupName"
        Me.lblGroupName.Size = New System.Drawing.Size(42, 12)
        Me.lblGroupName.TabIndex = 8
        Me.lblGroupName.Text = "Label4"
        '
        'frmBatchTradeInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(818, 513)
        Me.Controls.Add(Me.gbFcode)
        Me.Controls.Add(Me.proBarOpt10078)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.proBarOpt10060Price)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.proBarOpt10060Qty)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.chkDay)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.btnStartJob)
        Me.Controls.Add(Me.proBarOpt10081)
        Me.Controls.Add(Me.proBarOpt10059Price)
        Me.Controls.Add(Me.proBarOpt10059Qty)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmBatchTradeInfo"
        Me.Text = "거래원 분석 배치작업(frmBatchTradeInfo)"
        Me.gbFcode.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dgvFCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents proBarOpt10059Qty As System.Windows.Forms.ProgressBar
    Friend WithEvents proBarOpt10059Price As System.Windows.Forms.ProgressBar
    Friend WithEvents proBarOpt10081 As System.Windows.Forms.ProgressBar
    Friend WithEvents btnStartJob As System.Windows.Forms.Button
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents chkDay As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents proBarOpt10060Qty As System.Windows.Forms.ProgressBar
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents proBarOpt10060Price As System.Windows.Forms.ProgressBar
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents proBarOpt10078 As System.Windows.Forms.ProgressBar
    Friend WithEvents gbFcode As System.Windows.Forms.GroupBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Private WithEvents dgvFCode As System.Windows.Forms.DataGridView
    Friend WithEvents lblGroupName As System.Windows.Forms.Label
End Class
