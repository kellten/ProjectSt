Imports System.Windows.Forms.DataVisualization.Charting
Imports PaikRichStock.Common

Public Class ucStockChartBase

    Private _setDt As DataTable
    Private _yAxis1ColumnName As String
    Private _yAxis2ColumnName As String
    Private _xAxisColumnName As String

    Public WriteOnly Property SetDt As DataTable
        Set(value As DataTable)
            _setDt = value
            dtpFromDate.Text = CDateTime.FormatDate(_setDt.Rows(_setDt.Rows.Count - 1).Item("일자").ToString(), ".")
            dtpToDate.Text = CDateTime.FormatDate(_setDt.Rows(0).Item("일자").ToString(), ".")
            DisplayChart()
        End Set
    End Property

    Public WriteOnly Property yAxis1ColumnName As String
        Set(value As String)
            _yAxis1ColumnName = value
        End Set
    End Property

    Public WriteOnly Property yAxis2ColumnName As String
        Set(value As String)
            _yAxis2ColumnName = value
        End Set
    End Property


    Public WriteOnly Property xAxisColumnName As String
        Set(value As String)
            _xAxisColumnName = value
        End Set
    End Property

    Private Sub DisplayChart()

        Dim high As Integer = 0
        Dim close As Integer = 0
        Dim low As Integer = 0
        Dim open As Integer = 0
        Dim intPoint As Integer = 0

        With stockBaseChart
            For j As Integer = 0 To .Series.Count - 1
                .Series(j).Points.Clear()
            Next

            For i As Integer = _setDt.Rows.Count - 1 To 0 Step -1
                If Trim(_setDt.Rows(i).Item("일자").ToString()) > CDateTime.FormatDate(dtpToDate.Text) Then
                    Continue For
                End If

                If Trim(_setDt.Rows(i).Item("일자").ToString()) < CDateTime.FormatDate(dtpFromDate.Text) Then
                    Continue For
                End If

                If high = 0 Then

                    high = RemovePlusMinus(_setDt.Rows(i).Item("고가").ToString())

                Else

                    If high < RemovePlusMinus(_setDt.Rows(i).Item("고가").ToString()) Then
                        high = RemovePlusMinus(_setDt.Rows(i).Item("고가").ToString())
                    End If

                End If

                If low = 0 Then
                    low = RemovePlusMinus(_setDt.Rows(i).Item("저가").ToString())
                Else
                    If RemovePlusMinus(_setDt.Rows(i).Item("저가").ToString()) <> 0 Then
                        If low > RemovePlusMinus(_setDt.Rows(i).Item("저가").ToString()) Then
                            low = RemovePlusMinus(_setDt.Rows(i).Item("저가").ToString())
                        End If
                    End If
                End If

                .Series("Price").Points.AddXY(_setDt.Rows(i).Item("일자"), RemovePlusMinus(_setDt.Rows(i).Item("고가").ToString()))
                .Series("Volume").Points.AddXY(_setDt.Rows(i).Item("일자"), RemovePlusMinus(_setDt.Rows(i).Item("거래량").ToString()))
                .Series("Price").Points(intPoint).YValues(1) = RemovePlusMinus(_setDt.Rows(i).Item("저가").ToString())
                .Series("Price").Points(intPoint).YValues(2) = RemovePlusMinus(_setDt.Rows(i).Item("시가").ToString())
                .Series("Price").Points(intPoint).YValues(3) = RemovePlusMinus(_setDt.Rows(i).Item("종가").ToString())

                .Series("VolumeCalQtyFore").Points.AddXY(_setDt.Rows(i).Item("일자"), _setDt.Rows(i).Item("외국인투자자합").ToString())
                .Series("VolumeCalQtyGaeIn").Points.AddXY(_setDt.Rows(i).Item("일자"), _setDt.Rows(i).Item("개인투자자합").ToString())
                .Series("VolumeCalQtyGigan").Points.AddXY(_setDt.Rows(i).Item("일자"), _setDt.Rows(i).Item("기관계합").ToString())
                .Series("VolumeCalQtyGumWoong").Points.AddXY(_setDt.Rows(i).Item("일자"), _setDt.Rows(i).Item("금융투자합").ToString())
                .Series("VolumeCalQtyBohum").Points.AddXY(_setDt.Rows(i).Item("일자"), _setDt.Rows(i).Item("보험합").ToString())
                .Series("VolumeCalQtyTusin").Points.AddXY(_setDt.Rows(i).Item("일자"), _setDt.Rows(i).Item("투신합").ToString())
                .Series("VolumeCalQtyGitaGumWoong").Points.AddXY(_setDt.Rows(i).Item("일자"), _setDt.Rows(i).Item("기타금융합").ToString())
                .Series("VolumeCalQtyBank").Points.AddXY(_setDt.Rows(i).Item("일자"), _setDt.Rows(i).Item("은행합").ToString())
                .Series("VolumeCalQtyYeungGiGum").Points.AddXY(_setDt.Rows(i).Item("일자"), _setDt.Rows(i).Item("연기금등합").ToString())
                .Series("VolumeCalQtySamoFund").Points.AddXY(_setDt.Rows(i).Item("일자"), _setDt.Rows(i).Item("사모펀드합").ToString())
                .Series("VolumeCalQtyNation").Points.AddXY(_setDt.Rows(i).Item("일자"), _setDt.Rows(i).Item("국가합").ToString())
                .Series("VolumeCalQtyGitaBubin").Points.AddXY(_setDt.Rows(i).Item("일자"), _setDt.Rows(i).Item("기타법인합").ToString())
                .Series("VolumeCalQtyIOFor").Points.AddXY(_setDt.Rows(i).Item("일자"), _setDt.Rows(i).Item("내외국인합").ToString())

                .Series("GomeaDoQty").Points.AddXY(_setDt.Rows(i).Item("일자"), _setDt.Rows(i).Item("공매도량").ToString())
                .Series("GomeaDoPrice").Points.AddXY(_setDt.Rows(i).Item("일자"), _setDt.Rows(i).Item("공매도평균가").ToString())

                intPoint = intPoint + 1

            Next

            .ChartAreas("Price").AxisY.Minimum = CInt(low - (low * 0.1))
            .ChartAreas("Price").AxisY.Maximum = CInt(high + (high * 0.1))
            .ChartAreas("Price").AxisX.IsLabelAutoFit = True

        End With

        stockBaseChart.Series("Price").ChartType = SeriesChartType.Candlestick

    End Sub

    Private Function RemovePlusMinus(ByVal value As String) As Integer
        If value.ToString().Trim = "" Then Return 0

        Dim str As String = Replace(value, "+", "")
        str = Replace(value, "-", "")

        Return CInt(str)

    End Function

    Private Sub chkVolume_CheckedChanged(sender As Object, e As EventArgs) Handles chkVolume.CheckedChanged
        If chkVolume.Checked = True Then
            stockBaseChart.ChartAreas("Volume").Visible = True
        Else
            stockBaseChart.ChartAreas("Volume").Visible = False
        End If
    End Sub

    Private Sub chkVolumeCalQtyFore_CheckedChanged(sender As Object, e As EventArgs) Handles chkVolumeCalQtyFore.CheckedChanged, chkVolumeCalQtyBank.CheckedChanged, chkVolumeCalQtyBohum.CheckedChanged, chkVolumeCalQtyGaeIn.CheckedChanged, _
                                                                                             chkVolumeCalQtyGigan.CheckedChanged, chkVolumeCalQtyGitaBubin.CheckedChanged, chkVolumeCalQtyGitaGumWoong.CheckedChanged, chkVolumeCalQtyIOFor.CheckedChanged, _
                                                                                             chkVolumeCalQtyIOFor.CheckedChanged, chkVolumeCalQtyNation.CheckedChanged, chkVolumeCalQtySamoFund.CheckedChanged, chkVolumeCalQtyTusin.CheckedChanged, _
                                                                                             chkVolumeCalQtyYeungGiGum.CheckedChanged, chkVolumeCalQtyGumWoong.CheckedChanged

        If CType(sender, CheckBox).Name = "" Then Exit Sub

        Dim chartAreasName As String = Mid(CType(sender, CheckBox).Name, 4, CType(sender, CheckBox).Name.Length - 3)
        If CType(sender, CheckBox).Checked = True Then
            stockBaseChart.Series(chartAreasName).Enabled = True
        Else
            stockBaseChart.Series(chartAreasName).Enabled = False
        End If

    End Sub

    Private Sub btnGiganJijung_Click(sender As Object, e As EventArgs) Handles btnGiganJijung.Click
        DisplayChart()
    End Sub

    Private Sub stockBaseChart_MouseWheel(sender As Object, e As MouseEventArgs) Handles stockBaseChart.MouseWheel
        If e.Delta < 0 Then
            stockBaseChart.ChartAreas(0).AxisX.ScaleView.ZoomReset()
            stockBaseChart.ChartAreas(0).AxisY.ScaleView.ZoomReset()
        End If

        If e.Delta > 0 Then
            Dim xMin As Double = stockBaseChart.ChartAreas(0).AxisX.ScaleView.ViewMinimum
            Dim xMax As Double = stockBaseChart.ChartAreas(0).AxisX.ScaleView.ViewMaximum
            Dim yMin As Double = stockBaseChart.ChartAreas(0).AxisY.ScaleView.ViewMinimum
            Dim yMax As Double = stockBaseChart.ChartAreas(0).AxisY.ScaleView.ViewMaximum

            Dim posXStart As Double = stockBaseChart.ChartAreas(0).AxisX.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4
            Dim posXFinish As Double = stockBaseChart.ChartAreas(0).AxisX.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4
            Dim posYStart As Double = stockBaseChart.ChartAreas(0).AxisY.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4
            Dim posYFinish As Double = stockBaseChart.ChartAreas(0).AxisY.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4

            stockBaseChart.ChartAreas(0).AxisX.ScaleView.Zoom(posXStart, posXFinish)
            stockBaseChart.ChartAreas(0).AxisY.ScaleView.Zoom(posYStart, posYFinish)
        End If

    End Sub
End Class
