<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucStockChartBase
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
        Dim ChartArea4 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim ChartArea5 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim ChartArea6 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series18 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series19 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series20 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series21 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series22 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series23 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series24 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series25 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series26 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series27 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series28 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series29 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series30 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series31 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series32 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series33 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series34 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.stockBaseChart = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.chkVolume = New System.Windows.Forms.CheckBox()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.pnOption = New System.Windows.Forms.Panel()
        Me.chkVolumeCalQtyIOFor = New System.Windows.Forms.CheckBox()
        Me.chkVolumeCalQtyGitaBubin = New System.Windows.Forms.CheckBox()
        Me.chkVolumeCalQtyNation = New System.Windows.Forms.CheckBox()
        Me.chkVolumeCalQtySamoFund = New System.Windows.Forms.CheckBox()
        Me.chkVolumeCalQtyYeungGiGum = New System.Windows.Forms.CheckBox()
        Me.chkVolumeCalQtyGitaGumWoong = New System.Windows.Forms.CheckBox()
        Me.chkVolumeCalQtyTusin = New System.Windows.Forms.CheckBox()
        Me.chkVolumeCalQtyBank = New System.Windows.Forms.CheckBox()
        Me.chkVolumeCalQtyBohum = New System.Windows.Forms.CheckBox()
        Me.chkVolumeCalQtyGumWoong = New System.Windows.Forms.CheckBox()
        Me.chkVolumeCalQtyGaeIn = New System.Windows.Forms.CheckBox()
        Me.chkVolumeCalQtyFore = New System.Windows.Forms.CheckBox()
        Me.chkVolumeCalQtyGigan = New System.Windows.Forms.CheckBox()
        Me.chkGomeado = New System.Windows.Forms.CheckBox()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnGiganJijung = New System.Windows.Forms.Button()
        CType(Me.stockBaseChart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.pnOption.SuspendLayout()
        Me.SuspendLayout()
        '
        'stockBaseChart
        '
        ChartArea4.CursorX.IsUserEnabled = True
        ChartArea4.CursorX.IsUserSelectionEnabled = True
        ChartArea4.Name = "Price"
        ChartArea5.AlignWithChartArea = "Price"
        ChartArea5.Name = "Volume"
        ChartArea6.Name = "Gomeado"
        Me.stockBaseChart.ChartAreas.Add(ChartArea4)
        Me.stockBaseChart.ChartAreas.Add(ChartArea5)
        Me.stockBaseChart.ChartAreas.Add(ChartArea6)
        Me.stockBaseChart.Dock = System.Windows.Forms.DockStyle.Fill
        Legend2.Name = "Legend1"
        Me.stockBaseChart.Legends.Add(Legend2)
        Me.stockBaseChart.Location = New System.Drawing.Point(0, 0)
        Me.stockBaseChart.Name = "stockBaseChart"
        Series18.ChartArea = "Price"
        Series18.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Stock
        Series18.Legend = "Legend1"
        Series18.Name = "Price"
        Series18.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
        Series18.YValuesPerPoint = 4
        Series19.ChartArea = "Volume"
        Series19.Legend = "Legend1"
        Series19.Name = "Volume"
        Series19.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
        Series20.ChartArea = "Price"
        Series20.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine
        Series20.Legend = "Legend1"
        Series20.LegendText = "외국인"
        Series20.Name = "VolumeCalQtyFore"
        Series20.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary
        Series21.ChartArea = "Price"
        Series21.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine
        Series21.Legend = "Legend1"
        Series21.LegendText = "개인"
        Series21.Name = "VolumeCalQtyGaeIn"
        Series21.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary
        Series22.ChartArea = "Price"
        Series22.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine
        Series22.Legend = "Legend1"
        Series22.LegendText = "기관합"
        Series22.Name = "VolumeCalQtyGigan"
        Series22.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary
        Series23.ChartArea = "Price"
        Series23.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine
        Series23.Legend = "Legend1"
        Series23.LegendText = "금융"
        Series23.Name = "VolumeCalQtyGumWoong"
        Series23.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary
        Series24.ChartArea = "Price"
        Series24.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine
        Series24.Legend = "Legend1"
        Series24.LegendText = "보험"
        Series24.Name = "VolumeCalQtyBohum"
        Series24.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary
        Series25.ChartArea = "Price"
        Series25.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine
        Series25.Legend = "Legend1"
        Series25.LegendText = "투신"
        Series25.Name = "VolumeCalQtyTusin"
        Series25.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary
        Series26.ChartArea = "Price"
        Series26.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine
        Series26.Legend = "Legend1"
        Series26.LegendText = "기금"
        Series26.Name = "VolumeCalQtyGitaGumWoong"
        Series26.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary
        Series27.ChartArea = "Price"
        Series27.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine
        Series27.Legend = "Legend1"
        Series27.LegendText = "은행"
        Series27.Name = "VolumeCalQtyBank"
        Series27.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary
        Series28.ChartArea = "Price"
        Series28.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine
        Series28.Legend = "Legend1"
        Series28.LegendText = "연기금"
        Series28.Name = "VolumeCalQtyYeungGiGum"
        Series28.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary
        Series29.ChartArea = "Price"
        Series29.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine
        Series29.Legend = "Legend1"
        Series29.LegendText = "사모펀드"
        Series29.Name = "VolumeCalQtySamoFund"
        Series29.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary
        Series30.ChartArea = "Price"
        Series30.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine
        Series30.Legend = "Legend1"
        Series30.LegendText = "국가"
        Series30.Name = "VolumeCalQtyNation"
        Series30.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary
        Series31.ChartArea = "Price"
        Series31.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine
        Series31.Legend = "Legend1"
        Series31.LegendText = "기법"
        Series31.Name = "VolumeCalQtyGitaBubin"
        Series31.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary
        Series32.ChartArea = "Price"
        Series32.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine
        Series32.Legend = "Legend1"
        Series32.LegendText = "내외국인"
        Series32.Name = "VolumeCalQtyIOFor"
        Series32.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary
        Series33.ChartArea = "Gomeado"
        Series33.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine
        Series33.Legend = "Legend1"
        Series33.Name = "GomeaDoQty"
        Series33.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary
        Series34.ChartArea = "Gomeado"
        Series34.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine
        Series34.Legend = "Legend1"
        Series34.Name = "GomeaDoPrice"
        Me.stockBaseChart.Series.Add(Series18)
        Me.stockBaseChart.Series.Add(Series19)
        Me.stockBaseChart.Series.Add(Series20)
        Me.stockBaseChart.Series.Add(Series21)
        Me.stockBaseChart.Series.Add(Series22)
        Me.stockBaseChart.Series.Add(Series23)
        Me.stockBaseChart.Series.Add(Series24)
        Me.stockBaseChart.Series.Add(Series25)
        Me.stockBaseChart.Series.Add(Series26)
        Me.stockBaseChart.Series.Add(Series27)
        Me.stockBaseChart.Series.Add(Series28)
        Me.stockBaseChart.Series.Add(Series29)
        Me.stockBaseChart.Series.Add(Series30)
        Me.stockBaseChart.Series.Add(Series31)
        Me.stockBaseChart.Series.Add(Series32)
        Me.stockBaseChart.Series.Add(Series33)
        Me.stockBaseChart.Series.Add(Series34)
        Me.stockBaseChart.Size = New System.Drawing.Size(585, 437)
        Me.stockBaseChart.TabIndex = 0
        Me.stockBaseChart.Text = "Chart1"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnGiganJijung)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpToDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpFromDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.stockBaseChart)
        Me.SplitContainer1.Size = New System.Drawing.Size(589, 486)
        Me.SplitContainer1.SplitterDistance = 41
        Me.SplitContainer1.TabIndex = 1
        '
        'chkVolume
        '
        Me.chkVolume.AutoSize = True
        Me.chkVolume.Checked = True
        Me.chkVolume.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVolume.Location = New System.Drawing.Point(3, 3)
        Me.chkVolume.Name = "chkVolume"
        Me.chkVolume.Size = New System.Drawing.Size(60, 15)
        Me.chkVolume.TabIndex = 0
        Me.chkVolume.Text = "Volume"
        Me.chkVolume.UseVisualStyleBackColor = True
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.pnOption)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer1)
        Me.SplitContainer2.Size = New System.Drawing.Size(678, 486)
        Me.SplitContainer2.SplitterDistance = 85
        Me.SplitContainer2.TabIndex = 2
        '
        'pnOption
        '
        Me.pnOption.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnOption.Controls.Add(Me.chkGomeado)
        Me.pnOption.Controls.Add(Me.chkVolumeCalQtyIOFor)
        Me.pnOption.Controls.Add(Me.chkVolumeCalQtyGitaBubin)
        Me.pnOption.Controls.Add(Me.chkVolumeCalQtyNation)
        Me.pnOption.Controls.Add(Me.chkVolumeCalQtySamoFund)
        Me.pnOption.Controls.Add(Me.chkVolumeCalQtyYeungGiGum)
        Me.pnOption.Controls.Add(Me.chkVolumeCalQtyGitaGumWoong)
        Me.pnOption.Controls.Add(Me.chkVolumeCalQtyTusin)
        Me.pnOption.Controls.Add(Me.chkVolumeCalQtyBank)
        Me.pnOption.Controls.Add(Me.chkVolumeCalQtyBohum)
        Me.pnOption.Controls.Add(Me.chkVolume)
        Me.pnOption.Controls.Add(Me.chkVolumeCalQtyGumWoong)
        Me.pnOption.Controls.Add(Me.chkVolumeCalQtyGaeIn)
        Me.pnOption.Controls.Add(Me.chkVolumeCalQtyFore)
        Me.pnOption.Controls.Add(Me.chkVolumeCalQtyGigan)
        Me.pnOption.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnOption.Location = New System.Drawing.Point(0, 0)
        Me.pnOption.Name = "pnOption"
        Me.pnOption.Size = New System.Drawing.Size(85, 486)
        Me.pnOption.TabIndex = 0
        '
        'chkVolumeCalQtyIOFor
        '
        Me.chkVolumeCalQtyIOFor.AutoSize = True
        Me.chkVolumeCalQtyIOFor.Checked = True
        Me.chkVolumeCalQtyIOFor.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVolumeCalQtyIOFor.Location = New System.Drawing.Point(3, 276)
        Me.chkVolumeCalQtyIOFor.Name = "chkVolumeCalQtyIOFor"
        Me.chkVolumeCalQtyIOFor.Size = New System.Drawing.Size(72, 15)
        Me.chkVolumeCalQtyIOFor.TabIndex = 13
        Me.chkVolumeCalQtyIOFor.Text = "내외국인"
        Me.chkVolumeCalQtyIOFor.UseVisualStyleBackColor = True
        '
        'chkVolumeCalQtyGitaBubin
        '
        Me.chkVolumeCalQtyGitaBubin.AutoSize = True
        Me.chkVolumeCalQtyGitaBubin.Checked = True
        Me.chkVolumeCalQtyGitaBubin.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVolumeCalQtyGitaBubin.Location = New System.Drawing.Point(3, 255)
        Me.chkVolumeCalQtyGitaBubin.Name = "chkVolumeCalQtyGitaBubin"
        Me.chkVolumeCalQtyGitaBubin.Size = New System.Drawing.Size(48, 15)
        Me.chkVolumeCalQtyGitaBubin.TabIndex = 12
        Me.chkVolumeCalQtyGitaBubin.Text = "기법"
        Me.chkVolumeCalQtyGitaBubin.UseVisualStyleBackColor = True
        '
        'chkVolumeCalQtyNation
        '
        Me.chkVolumeCalQtyNation.AutoSize = True
        Me.chkVolumeCalQtyNation.Checked = True
        Me.chkVolumeCalQtyNation.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVolumeCalQtyNation.Location = New System.Drawing.Point(3, 234)
        Me.chkVolumeCalQtyNation.Name = "chkVolumeCalQtyNation"
        Me.chkVolumeCalQtyNation.Size = New System.Drawing.Size(48, 15)
        Me.chkVolumeCalQtyNation.TabIndex = 11
        Me.chkVolumeCalQtyNation.Text = "국가"
        Me.chkVolumeCalQtyNation.UseVisualStyleBackColor = True
        '
        'chkVolumeCalQtySamoFund
        '
        Me.chkVolumeCalQtySamoFund.AutoSize = True
        Me.chkVolumeCalQtySamoFund.Checked = True
        Me.chkVolumeCalQtySamoFund.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVolumeCalQtySamoFund.Location = New System.Drawing.Point(3, 213)
        Me.chkVolumeCalQtySamoFund.Name = "chkVolumeCalQtySamoFund"
        Me.chkVolumeCalQtySamoFund.Size = New System.Drawing.Size(72, 15)
        Me.chkVolumeCalQtySamoFund.TabIndex = 10
        Me.chkVolumeCalQtySamoFund.Text = "사모펀드"
        Me.chkVolumeCalQtySamoFund.UseVisualStyleBackColor = True
        '
        'chkVolumeCalQtyYeungGiGum
        '
        Me.chkVolumeCalQtyYeungGiGum.AutoSize = True
        Me.chkVolumeCalQtyYeungGiGum.Checked = True
        Me.chkVolumeCalQtyYeungGiGum.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVolumeCalQtyYeungGiGum.Location = New System.Drawing.Point(3, 192)
        Me.chkVolumeCalQtyYeungGiGum.Name = "chkVolumeCalQtyYeungGiGum"
        Me.chkVolumeCalQtyYeungGiGum.Size = New System.Drawing.Size(60, 15)
        Me.chkVolumeCalQtyYeungGiGum.TabIndex = 9
        Me.chkVolumeCalQtyYeungGiGum.Text = "연기금"
        Me.chkVolumeCalQtyYeungGiGum.UseVisualStyleBackColor = True
        '
        'chkVolumeCalQtyGitaGumWoong
        '
        Me.chkVolumeCalQtyGitaGumWoong.AutoSize = True
        Me.chkVolumeCalQtyGitaGumWoong.Checked = True
        Me.chkVolumeCalQtyGitaGumWoong.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVolumeCalQtyGitaGumWoong.Location = New System.Drawing.Point(3, 150)
        Me.chkVolumeCalQtyGitaGumWoong.Name = "chkVolumeCalQtyGitaGumWoong"
        Me.chkVolumeCalQtyGitaGumWoong.Size = New System.Drawing.Size(48, 15)
        Me.chkVolumeCalQtyGitaGumWoong.TabIndex = 7
        Me.chkVolumeCalQtyGitaGumWoong.Text = "기금"
        Me.chkVolumeCalQtyGitaGumWoong.UseVisualStyleBackColor = True
        '
        'chkVolumeCalQtyTusin
        '
        Me.chkVolumeCalQtyTusin.AutoSize = True
        Me.chkVolumeCalQtyTusin.Checked = True
        Me.chkVolumeCalQtyTusin.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVolumeCalQtyTusin.Location = New System.Drawing.Point(3, 129)
        Me.chkVolumeCalQtyTusin.Name = "chkVolumeCalQtyTusin"
        Me.chkVolumeCalQtyTusin.Size = New System.Drawing.Size(48, 15)
        Me.chkVolumeCalQtyTusin.TabIndex = 6
        Me.chkVolumeCalQtyTusin.Text = "투신"
        Me.chkVolumeCalQtyTusin.UseVisualStyleBackColor = True
        '
        'chkVolumeCalQtyBank
        '
        Me.chkVolumeCalQtyBank.AutoSize = True
        Me.chkVolumeCalQtyBank.Checked = True
        Me.chkVolumeCalQtyBank.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVolumeCalQtyBank.Location = New System.Drawing.Point(3, 171)
        Me.chkVolumeCalQtyBank.Name = "chkVolumeCalQtyBank"
        Me.chkVolumeCalQtyBank.Size = New System.Drawing.Size(48, 15)
        Me.chkVolumeCalQtyBank.TabIndex = 8
        Me.chkVolumeCalQtyBank.Text = "은행"
        Me.chkVolumeCalQtyBank.UseVisualStyleBackColor = True
        '
        'chkVolumeCalQtyBohum
        '
        Me.chkVolumeCalQtyBohum.AutoSize = True
        Me.chkVolumeCalQtyBohum.Checked = True
        Me.chkVolumeCalQtyBohum.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVolumeCalQtyBohum.Location = New System.Drawing.Point(3, 108)
        Me.chkVolumeCalQtyBohum.Name = "chkVolumeCalQtyBohum"
        Me.chkVolumeCalQtyBohum.Size = New System.Drawing.Size(48, 15)
        Me.chkVolumeCalQtyBohum.TabIndex = 5
        Me.chkVolumeCalQtyBohum.Text = "보험"
        Me.chkVolumeCalQtyBohum.UseVisualStyleBackColor = True
        '
        'chkVolumeCalQtyGumWoong
        '
        Me.chkVolumeCalQtyGumWoong.AutoSize = True
        Me.chkVolumeCalQtyGumWoong.Checked = True
        Me.chkVolumeCalQtyGumWoong.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVolumeCalQtyGumWoong.Location = New System.Drawing.Point(3, 87)
        Me.chkVolumeCalQtyGumWoong.Name = "chkVolumeCalQtyGumWoong"
        Me.chkVolumeCalQtyGumWoong.Size = New System.Drawing.Size(48, 15)
        Me.chkVolumeCalQtyGumWoong.TabIndex = 4
        Me.chkVolumeCalQtyGumWoong.Text = "금융"
        Me.chkVolumeCalQtyGumWoong.UseVisualStyleBackColor = True
        '
        'chkVolumeCalQtyGaeIn
        '
        Me.chkVolumeCalQtyGaeIn.AutoSize = True
        Me.chkVolumeCalQtyGaeIn.Checked = True
        Me.chkVolumeCalQtyGaeIn.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVolumeCalQtyGaeIn.Location = New System.Drawing.Point(3, 45)
        Me.chkVolumeCalQtyGaeIn.Name = "chkVolumeCalQtyGaeIn"
        Me.chkVolumeCalQtyGaeIn.Size = New System.Drawing.Size(48, 15)
        Me.chkVolumeCalQtyGaeIn.TabIndex = 2
        Me.chkVolumeCalQtyGaeIn.Text = "개인"
        Me.chkVolumeCalQtyGaeIn.UseVisualStyleBackColor = True
        '
        'chkVolumeCalQtyFore
        '
        Me.chkVolumeCalQtyFore.AutoSize = True
        Me.chkVolumeCalQtyFore.Checked = True
        Me.chkVolumeCalQtyFore.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVolumeCalQtyFore.Location = New System.Drawing.Point(3, 24)
        Me.chkVolumeCalQtyFore.Name = "chkVolumeCalQtyFore"
        Me.chkVolumeCalQtyFore.Size = New System.Drawing.Size(60, 15)
        Me.chkVolumeCalQtyFore.TabIndex = 1
        Me.chkVolumeCalQtyFore.Text = "외국인"
        Me.chkVolumeCalQtyFore.UseVisualStyleBackColor = True
        '
        'chkVolumeCalQtyGigan
        '
        Me.chkVolumeCalQtyGigan.AutoSize = True
        Me.chkVolumeCalQtyGigan.Checked = True
        Me.chkVolumeCalQtyGigan.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVolumeCalQtyGigan.Location = New System.Drawing.Point(3, 66)
        Me.chkVolumeCalQtyGigan.Name = "chkVolumeCalQtyGigan"
        Me.chkVolumeCalQtyGigan.Size = New System.Drawing.Size(60, 15)
        Me.chkVolumeCalQtyGigan.TabIndex = 3
        Me.chkVolumeCalQtyGigan.Text = "기관합"
        Me.chkVolumeCalQtyGigan.UseVisualStyleBackColor = True
        '
        'chkGomeado
        '
        Me.chkGomeado.AutoSize = True
        Me.chkGomeado.Checked = True
        Me.chkGomeado.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkGomeado.Location = New System.Drawing.Point(3, 297)
        Me.chkGomeado.Name = "chkGomeado"
        Me.chkGomeado.Size = New System.Drawing.Size(60, 15)
        Me.chkGomeado.TabIndex = 14
        Me.chkGomeado.Text = "공매도"
        Me.chkGomeado.UseVisualStyleBackColor = True
        '
        'dtpFromDate
        '
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFromDate.Location = New System.Drawing.Point(62, 9)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(90, 20)
        Me.dtpFromDate.TabIndex = 351
        Me.dtpFromDate.Tag = "/FDate/PEnabled=True"
        '
        'dtpToDate
        '
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpToDate.Location = New System.Drawing.Point(178, 9)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(90, 20)
        Me.dtpToDate.TabIndex = 352
        Me.dtpToDate.Tag = "/FDate/PEnabled=True"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 11)
        Me.Label1.TabIndex = 353
        Me.Label1.Text = "기간설정"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("굴림체", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.Location = New System.Drawing.Point(156, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(17, 16)
        Me.Label2.TabIndex = 354
        Me.Label2.Text = "~"
        '
        'btnGiganJijung
        '
        Me.btnGiganJijung.Location = New System.Drawing.Point(274, 7)
        Me.btnGiganJijung.Name = "btnGiganJijung"
        Me.btnGiganJijung.Size = New System.Drawing.Size(74, 25)
        Me.btnGiganJijung.TabIndex = 355
        Me.btnGiganJijung.Text = "설정"
        Me.btnGiganJijung.UseVisualStyleBackColor = True
        '
        'ucStockChartBase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 11.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainer2)
        Me.Font = New System.Drawing.Font("굴림체", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Name = "ucStockChartBase"
        Me.Size = New System.Drawing.Size(678, 486)
        CType(Me.stockBaseChart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.pnOption.ResumeLayout(False)
        Me.pnOption.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents stockBaseChart As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents chkVolume As System.Windows.Forms.CheckBox
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents chkVolumeCalQtyBohum As System.Windows.Forms.CheckBox
    Friend WithEvents chkVolumeCalQtyGumWoong As System.Windows.Forms.CheckBox
    Friend WithEvents chkVolumeCalQtyGigan As System.Windows.Forms.CheckBox
    Friend WithEvents chkVolumeCalQtyFore As System.Windows.Forms.CheckBox
    Friend WithEvents chkVolumeCalQtyGaeIn As System.Windows.Forms.CheckBox
    Friend WithEvents pnOption As System.Windows.Forms.Panel
    Friend WithEvents chkVolumeCalQtySamoFund As System.Windows.Forms.CheckBox
    Friend WithEvents chkVolumeCalQtyYeungGiGum As System.Windows.Forms.CheckBox
    Friend WithEvents chkVolumeCalQtyGitaGumWoong As System.Windows.Forms.CheckBox
    Friend WithEvents chkVolumeCalQtyTusin As System.Windows.Forms.CheckBox
    Friend WithEvents chkVolumeCalQtyBank As System.Windows.Forms.CheckBox
    Friend WithEvents chkVolumeCalQtyIOFor As System.Windows.Forms.CheckBox
    Friend WithEvents chkVolumeCalQtyGitaBubin As System.Windows.Forms.CheckBox
    Friend WithEvents chkVolumeCalQtyNation As System.Windows.Forms.CheckBox
    Friend WithEvents chkGomeado As System.Windows.Forms.CheckBox
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnGiganJijung As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
