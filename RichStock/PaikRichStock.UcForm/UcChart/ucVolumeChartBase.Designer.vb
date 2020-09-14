<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucVolumeChartBase
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
        Me.components = New System.ComponentModel.Container()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series4 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series5 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Title1 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Me.chartBase = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmsArea1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsArea2 = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.chartBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'chartBase
        '
        ChartArea1.CursorX.IsUserEnabled = True
        ChartArea1.CursorX.IsUserSelectionEnabled = True
        ChartArea1.Name = "Volume"
        ChartArea2.Name = "DistVolume"
        Me.chartBase.ChartAreas.Add(ChartArea1)
        Me.chartBase.ChartAreas.Add(ChartArea2)
        Me.chartBase.ContextMenuStrip = Me.ContextMenuStrip1
        Me.chartBase.Dock = System.Windows.Forms.DockStyle.Fill
        Legend1.Name = "Legend1"
        Me.chartBase.Legends.Add(Legend1)
        Me.chartBase.Location = New System.Drawing.Point(0, 0)
        Me.chartBase.Name = "chartBase"
        Series1.ChartArea = "Volume"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick
        Series1.Legend = "Legend1"
        Series1.Name = "Price"
        Series1.YValuesPerPoint = 4
        Series2.ChartArea = "Volume"
        Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine
        Series2.Legend = "Legend1"
        Series2.Name = "VolumeSum"
        Series2.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary
        Series3.ChartArea = "Volume"
        Series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine
        Series3.Legend = "Legend1"
        Series3.Name = "AvgPrice"
        Series4.ChartArea = "DistVolume"
        Series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick
        Series4.Legend = "Legend1"
        Series4.Name = "Price2"
        Series4.YValuesPerPoint = 4
        Series5.ChartArea = "DistVolume"
        Series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine
        Series5.Legend = "Legend1"
        Series5.Name = "Distri"
        Series5.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary
        Me.chartBase.Series.Add(Series1)
        Me.chartBase.Series.Add(Series2)
        Me.chartBase.Series.Add(Series3)
        Me.chartBase.Series.Add(Series4)
        Me.chartBase.Series.Add(Series5)
        Me.chartBase.Size = New System.Drawing.Size(902, 519)
        Me.chartBase.TabIndex = 1
        Me.chartBase.Text = "Chart1"
        Title1.Name = "Title1"
        Title1.Text = "test"
        Me.chartBase.Titles.Add(Title1)
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmsArea1, Me.cmsArea2})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(106, 48)
        '
        'cmsArea1
        '
        Me.cmsArea1.Name = "cmsArea1"
        Me.cmsArea1.Size = New System.Drawing.Size(105, 22)
        Me.cmsArea1.Text = "Area1"
        '
        'cmsArea2
        '
        Me.cmsArea2.Name = "cmsArea2"
        Me.cmsArea2.Size = New System.Drawing.Size(105, 22)
        Me.cmsArea2.Text = "Area2"
        '
        'ucVolumeChartBase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 11.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.chartBase)
        Me.Font = New System.Drawing.Font("굴림체", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Name = "ucVolumeChartBase"
        Me.Size = New System.Drawing.Size(902, 519)
        CType(Me.chartBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents chartBase As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmsArea1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsArea2 As System.Windows.Forms.ToolStripMenuItem

End Class
