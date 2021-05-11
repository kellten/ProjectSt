﻿Imports System.Windows.Forms.DataVisualization.Charting
Imports PaikRichStock.Common

Public Class ucChartVolumeAnalysisA

#Region " Property "
    Private _firstCall As Boolean = False

    Private _dtOpt10014 As DataTable
    Private _dtOpt10059 As DataTable
    Private _dtOpt10081 As DataTable
    Private _dtOpt20068 As DataTable
    Private _dtCycleCalData As DataTable

    Public WriteOnly Property DtCycleCalData As DataTable
        Set(value As DataTable)
            _dtCycleCalData = value
            InitChart()
        End Set
    End Property

    Public WriteOnly Property DtOpt10014 As DataTable
        Set(value As DataTable)
            _dtOpt10014 = value
        End Set
    End Property

    Public WriteOnly Property DtOpt10059 As DataTable
        Set(value As DataTable)
            _dtOpt10059 = value
        End Set
    End Property

    Public WriteOnly Property DtOpt10081 As DataTable
        Set(value As DataTable)
            _dtOpt10081 = value
        End Set
    End Property

    Public WriteOnly Property DtOpt20068 As DataTable
        Set(value As DataTable)
            _dtOpt20068 = value
        End Set
    End Property
#End Region

#Region " InitChart "
    Private Sub InitChart()
        With stockBaseChart
            For j As Integer = 0 To .Series.Count - 1
                .Series(j).Points.Clear()
            Next
        End With

        dtpFromDate.Text = CDateTime.FormatDate(_dtOpt10059.Rows(_dtOpt10059.Rows.Count - 1).Item("일자").ToString(), ".")
        dtpToDate.Text = CDateTime.FormatDate(_dtOpt10059.Rows(0).Item("일자").ToString(), ".")

        _firstCall = True

        DisplayChartAreaPriceVolume()
        DisplayChartAreaGomeado()
    End Sub
#End Region

#Region " DisplayChartAreaPriceVolume "
    Private Sub DisplayChartAreaPriceVolume()

        Dim high As Integer = 0
        Dim close As Integer = 0
        Dim low As Integer = 0
        Dim open As Integer = 0
        Dim intPoint As Integer = 0
        Dim maxMagipVolume As Integer = 0
        Dim minMagipVolume As Integer = 0

        With stockBaseChart
            For i As Integer = _dtOpt10081.Rows.Count - 1 To 0 Step -1
                If Trim(_dtOpt10081.Rows(i).Item("일자").ToString()) > CDateTime.FormatDate(dtpToDate.Text) Then
                    Continue For
                End If

                If Trim(_dtOpt10081.Rows(i).Item("일자").ToString()) < CDateTime.FormatDate(dtpFromDate.Text) Then
                    Continue For
                End If


                If high = 0 Then

                    high = RemovePlusMinus(_dtOpt10081.Rows(i).Item("고가").ToString())

                Else

                    If high < RemovePlusMinus(_dtOpt10081.Rows(i).Item("고가").ToString()) Then
                        high = RemovePlusMinus(_dtOpt10081.Rows(i).Item("고가").ToString())
                    End If

                End If

                If low = 0 Then
                    low = RemovePlusMinus(_dtOpt10081.Rows(i).Item("저가").ToString())
                Else
                    If RemovePlusMinus(_dtOpt10081.Rows(i).Item("저가").ToString()) <> 0 Then
                        If low > RemovePlusMinus(_dtOpt10081.Rows(i).Item("저가").ToString()) Then
                            low = RemovePlusMinus(_dtOpt10081.Rows(i).Item("저가").ToString())
                        End If
                    End If
                End If

                .Series("Price").Points.AddXY(_dtOpt10081.Rows(i).Item("일자"), RemovePlusMinus(_dtOpt10081.Rows(i).Item("고가").ToString()))
                .Series("Volume").Points.AddXY(_dtOpt10081.Rows(i).Item("일자"), RemovePlusMinus(_dtOpt10081.Rows(i).Item("거래량").ToString()))
                .Series("Price").Points(intPoint).YValues(1) = RemovePlusMinus(_dtOpt10081.Rows(i).Item("저가").ToString())
                .Series("Price").Points(intPoint).YValues(2) = RemovePlusMinus(_dtOpt10081.Rows(i).Item("시가").ToString())
                .Series("Price").Points(intPoint).YValues(3) = RemovePlusMinus(_dtOpt10081.Rows(i).Item("현재가").ToString())

                intPoint = intPoint + 1

            Next

            .ChartAreas("Price").AxisY.Minimum = CInt(low - (low * 0.1))
            .ChartAreas("Price").AxisY.Maximum = CInt(high + (high * 0.1))
            .ChartAreas("Price").AxisX.IsLabelAutoFit = True
            .Series("Price").ChartType = SeriesChartType.Candlestick


            For i As Integer = _dtOpt10059.Rows.Count - 1 To 0 Step -1
                If Trim(_dtOpt10059.Rows(i).Item("일자").ToString()) > CDateTime.FormatDate(dtpToDate.Text) Then
                    Continue For
                End If

                If Trim(_dtOpt10059.Rows(i).Item("일자").ToString()) < CDateTime.FormatDate(dtpFromDate.Text) Then
                    Continue For
                End If

                .Series("VolumeCalQtyFore").Points.AddXY(_dtOpt10059.Rows(i).Item("일자"), _dtOpt10059.Rows(i).Item("외국인투자자합").ToString())
                .Series("VolumeCalQtyGaeIn").Points.AddXY(_dtOpt10059.Rows(i).Item("일자"), _dtOpt10059.Rows(i).Item("개인투자자합").ToString())
                .Series("VolumeCalQtyGigan").Points.AddXY(_dtOpt10059.Rows(i).Item("일자"), _dtOpt10059.Rows(i).Item("기관계합").ToString())
                .Series("VolumeCalQtyGumWoong").Points.AddXY(_dtOpt10059.Rows(i).Item("일자"), _dtOpt10059.Rows(i).Item("금융투자합").ToString())
                .Series("VolumeCalQtyBohum").Points.AddXY(_dtOpt10059.Rows(i).Item("일자"), _dtOpt10059.Rows(i).Item("보험합").ToString())
                .Series("VolumeCalQtyTusin").Points.AddXY(_dtOpt10059.Rows(i).Item("일자"), _dtOpt10059.Rows(i).Item("투신합").ToString())
                .Series("VolumeCalQtyGitaGumWoong").Points.AddXY(_dtOpt10059.Rows(i).Item("일자"), _dtOpt10059.Rows(i).Item("기타금융합").ToString())
                .Series("VolumeCalQtyBank").Points.AddXY(_dtOpt10059.Rows(i).Item("일자"), _dtOpt10059.Rows(i).Item("은행합").ToString())
                .Series("VolumeCalQtyYeungGiGum").Points.AddXY(_dtOpt10059.Rows(i).Item("일자"), _dtOpt10059.Rows(i).Item("연기금등합").ToString())
                .Series("VolumeCalQtySamoFund").Points.AddXY(_dtOpt10059.Rows(i).Item("일자"), _dtOpt10059.Rows(i).Item("사모펀드합").ToString())
                .Series("VolumeCalQtyNation").Points.AddXY(_dtOpt10059.Rows(i).Item("일자"), _dtOpt10059.Rows(i).Item("국가합").ToString())
                .Series("VolumeCalQtyGitaBubin").Points.AddXY(_dtOpt10059.Rows(i).Item("일자"), _dtOpt10059.Rows(i).Item("기타법인합").ToString())
                .Series("VolumeCalQtyIOFor").Points.AddXY(_dtOpt10059.Rows(i).Item("일자"), _dtOpt10059.Rows(i).Item("내외국인합").ToString())

            Next

            '.ChartAreas("Price").AxisY2.Minimum =
            '.ChartAreas("Price").AxisY2.Maximum = 

        End With

    End Sub
#End Region

#Region " DisplayChartAreaGomeado "
    Private Sub DisplayChartAreaGomeado()
        With stockBaseChart
            For i As Integer = _dtOpt10014.Rows.Count - 1 To 0 Step -1
                .Series("GomeaDoQty").Points.AddXY(_dtOpt10014.Rows(i).Item("일자"), _dtOpt10014.Rows(i).Item("공매도량").ToString())
                .Series("GomeaDoPrice").Points.AddXY(_dtOpt10014.Rows(i).Item("일자"), _dtOpt10014.Rows(i).Item("공매도평균가").ToString())
            Next
        End With
    End Sub
#End Region

#Region " RemovePlusMinus "
    Private Function RemovePlusMinus(ByVal value As String) As Integer
        If value.ToString().Trim = "" Then Return 0

        Dim str As String = Replace(value, "+", "")
        str = Replace(value, "-", "")

        Return CInt(str)

    End Function
#End Region

#Region " Control Event "
    Private Sub chkVolume_CheckedChanged(sender As Object, e As EventArgs) Handles chkVolume.CheckedChanged
        If _firstCall = False Then Exit Sub
        If chkVolume.Checked = True Then
            stockBaseChart.ChartAreas("Volume").Visible = True
        Else
            stockBaseChart.ChartAreas("Volume").Visible = False
        End If
    End Sub

    Private Sub chkGomeado_CheckedChanged(sender As Object, e As EventArgs) Handles chkGomeado.CheckedChanged
        If _firstCall = False Then Exit Sub
        If chkGomeado.Checked = True Then
            stockBaseChart.ChartAreas("Gomeado").Visible = True
        Else
            stockBaseChart.ChartAreas("Gomeado").Visible = False
        End If
    End Sub


    Private Sub chkVolumeCalQtyFore_CheckedChanged(sender As Object, e As EventArgs) Handles chkVolumeCalQtyFore.CheckedChanged, chkVolumeCalQtyBank.CheckedChanged, chkVolumeCalQtyBohum.CheckedChanged, chkVolumeCalQtyGaeIn.CheckedChanged, _
                                                                                             chkVolumeCalQtyGigan.CheckedChanged, chkVolumeCalQtyGitaBubin.CheckedChanged, chkVolumeCalQtyGitaGumWoong.CheckedChanged, chkVolumeCalQtyIOFor.CheckedChanged, _
                                                                                             chkVolumeCalQtyIOFor.CheckedChanged, chkVolumeCalQtyNation.CheckedChanged, chkVolumeCalQtySamoFund.CheckedChanged, chkVolumeCalQtyTusin.CheckedChanged, _
                                                                                             chkVolumeCalQtyYeungGiGum.CheckedChanged, chkVolumeCalQtyGumWoong.CheckedChanged

        If _firstCall = False Then Exit Sub

        If CType(sender, CheckBox).Name = "" Then Exit Sub

        Dim chartAreasName As String = Mid(CType(sender, CheckBox).Name, 4, CType(sender, CheckBox).Name.Length - 3)
        If CType(sender, CheckBox).Checked = True Then
            stockBaseChart.Series(chartAreasName).Enabled = True
        Else
            stockBaseChart.Series(chartAreasName).Enabled = False
        End If

    End Sub

#End Region


    Private Sub stockBaseChart_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles stockBaseChart.MouseDoubleClick
        Dim htr As HitTestResult
        Dim selectDataPoint As DataPoint

        htr = stockBaseChart.HitTest(e.X, e.Y)
        selectDataPoint = stockBaseChart.Series(0).Points(htr.PointIndex)

    End Sub
End Class