﻿Public Class Tech_TypicalPrice

#Region " Fn_PeriodLowestMA -  기간별 최저가 및 저가 + 종가 /2 의 이동평균값을 가져온다."
    Private arrary_peridLowestMa As String() = {"일자", "저가MA", "기간최저가", "기간종가최저가", "최저가MA", "최저가종가MA", "최고저가종가MA"}
    ''' <summary>
    ''' 기간별 최저가 및 저가 + 종가 /2 의 이동평균값을 가져온다.
    ''' </summary>
    ''' <param name="period"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Fn_PeriodLowestMA(ByVal period As Integer, ByVal ds As DataSet, ByVal tagetTableName As String, _
                                      ByVal stockCode As String, ByVal stockName As String) As DataSet
        Dim returnDs As New DataSet
        Dim dt As New DataTable
        Dim totalValue As Integer = 0
        Dim cnt As Integer = 0
        Dim lowestValue As Integer = 0
        Dim lowestEndValue As Integer = 0
        Dim highestEndValue As Integer = 0
        'With dt.Columns
        '    .Add("일자", System.Type.GetType("System.String"))
        '    .Add("저가MA", System.Type.GetType("System.Int32"))
        '    .Add("기간최저가", System.Type.GetType("System.Int32"))
        '    .Add("기간종가최저가", System.Type.GetType("System.Int32"))
        '    .Add("기간종가최고가", System.Type.GetType("System.Int32"))
        '    .Add("최저가MA", System.Type.GetType("System.Decimal"))
        '    .Add("최저가종가MA", System.Type.GetType("System.Decimal"))
        '    .Add("최고저가종가MA", System.Type.GetType("System.Decimal"))
        'End With

        With ds.Tables(tagetTableName)
            .Columns.Add("저종", System.Type.GetType("System.Int32"))
            .Columns.Add("저가MA", System.Type.GetType("System.Int32"))
            .Columns.Add("기간최저가", System.Type.GetType("System.Int32"))
            .Columns.Add("기간종가최저가", System.Type.GetType("System.Int32"))
            .Columns.Add("기간종가최고가", System.Type.GetType("System.Int32"))
            .Columns.Add("최저가MA", System.Type.GetType("System.Decimal"))
            .Columns.Add("최저가종가MA", System.Type.GetType("System.Decimal"))
            .Columns.Add("최고저가종가MA", System.Type.GetType("System.Decimal"))

            If ds.Tables(tagetTableName).Rows.Count = 0 Then
                Return ds
            End If


            For Each dr As DataRow In .Rows
                dr("저종") = (Convert.ToInt32(dr("저가").ToString) + Convert.ToInt32(dr("현재가").ToString)) / 2
                cnt += 1
                If cnt = period Then
                    Exit For
                End If
            Next

            If .Rows.Count <= 2 * period Then
                For i As Integer = .Rows.Count - 1 To 0 Step -1
                    totalValue = 0
                    lowestValue = 0
                    highestEndValue = 0
                    lowestValue = .Compute("MIN(저가)", String.Empty)
                    lowestEndValue = .Compute("MIN(저종)", String.Empty)
                    highestEndValue = .Compute("MAX(저종)", String.Empty)
                    totalValue = .Compute("SUM(저가)", String.Empty)

                    .Rows(i)("기간최저가") = lowestValue
                    .Rows(i)("기간종가최저가") = lowestEndValue
                    .Rows(i)("기간종가최고가") = highestEndValue
                    .Rows(i)("저가MA") = totalValue / period
                Next

                totalValue = .Compute("SUM(기간최저가)", String.Empty)
                .Rows(0)("최저가MA") = totalValue / period
                totalValue = .Compute("SUM(기간종가최저가)", String.Empty)
                .Rows(0)("최저가종가MA") = totalValue / period
                totalValue = .Compute("SUM(기간종가최고가)", String.Empty)
                .Rows(0)("최고저가종가MA") = totalValue / period
            Else

                For i As Integer = period - 1 To 0 Step -1
                    totalValue = 0
                    lowestValue = 0
                    highestEndValue = 0
                    lowestValue = .Compute("MIN(저가)", String.Format("일자 > '{0}' AND 일자 <= '{1}'", .Rows(i + period)("일자").ToString, .Rows(i)("일자").ToString))
                    lowestEndValue = .Compute("MIN(저종)", String.Format("일자 > '{0}' AND 일자 <= '{1}'", .Rows(i + period)("일자").ToString, .Rows(i)("일자").ToString))
                    highestEndValue = .Compute("MAX(저종)", String.Format("일자 > '{0}' AND 일자 <= '{1}'", .Rows(i + period)("일자").ToString, .Rows(i)("일자").ToString))
                    totalValue = .Compute("SUM(저가)", String.Format("일자 > '{0}' AND 일자 <= '{1}'", .Rows(i + period)("일자").ToString, .Rows(i)("일자").ToString))


                    .Rows(i)("기간최저가") = lowestValue
                    .Rows(i)("기간종가최저가") = lowestEndValue
                    .Rows(i)("기간종가최고가") = highestEndValue
                    .Rows(i)("저가MA") = totalValue / period
                Next

                totalValue = .Compute("SUM(기간최저가)", String.Format("일자 > '{0}' AND 일자 <= '{1}'", .Rows(period)("일자").ToString, .Rows(0)("일자").ToString))
                .Rows(0)("최저가MA") = totalValue / period
                totalValue = .Compute("SUM(기간종가최저가)", String.Format("일자 > '{0}' AND 일자 <= '{1}'", .Rows(period)("일자").ToString, .Rows(0)("일자").ToString))
                .Rows(0)("최저가종가MA") = totalValue / period
                totalValue = .Compute("SUM(기간종가최고가)", String.Format("일자 > '{0}' AND 일자 <= '{1}'", .Rows(period)("일자").ToString, .Rows(0)("일자").ToString))
                .Rows(0)("최고저가종가MA") = totalValue / period
            End If

        End With

        'For i As Integer = 0 To dt.Rows.Count - period - 1
        '    totalValue = 0

        '    For row As Integer = i To period - 1 + i
        '        totalValue = totalValue + dt.Rows(row).Item("기간최저가")
        '    Next

        '    dt.Rows(i).Item("최저가MA") = totalValue / period

        'Next

        'For i As Integer = 0 To dt.Rows.Count - period - 1
        '    totalValue = 0

        '    For row As Integer = i To period - 1 + i
        '        totalValue = totalValue + dt.Rows(row).Item("기간종가최저가")
        '    Next

        '    dt.Rows(i).Item("최저가종가MA") = totalValue / period

        'Next

        'For i As Integer = 0 To dt.Rows.Count - period - 1
        '    totalValue = 0

        '    For row As Integer = i To period - 1 + i
        '        totalValue = totalValue + dt.Rows(row).Item("기간종가최고가")
        '    Next

        '    dt.Rows(i).Item("최고저가종가MA") = totalValue / period
        'Next

        'Dim dv As DataView = New DataView(dt)

        'For Each dr2th As DataRow In ds.Tables(tagetTableName).Rows

        '    'drNew = dtNew.Rows.Add
        '    drNew = dtNew.NewRow

        '    For i As Integer = 0 To ds.Tables(tagetTableName).Columns.Count - 1
        '        drNew(ds.Tables(tagetTableName).Columns(i).ColumnName) = dr2th(ds.Tables(tagetTableName).Columns(i).ColumnName)
        '    Next

        '    dv.RowFilter = String.Format("일자 = '{0}'", Trim(dr2th("일자").ToString()))

        '    For Each drRowView As DataRowView In dv

        '        drNew("저가MA") = drRowView("저가MA")
        '        drNew("기간최저가") = drRowView("기간최저가")
        '        drNew("기간종가최저가") = drRowView("기간종가최저가")
        '        drNew("기간종가최고가") = drRowView("기간종가최고가")
        '        drNew("최저가MA") = drRowView("최저가MA")
        '        drNew("최저가종가MA") = drRowView("최저가종가MA")
        '        drNew("최고저가종가MA") = drRowView("최고저가종가MA")
        '    Next

        '    dtNew.Rows.Add(drNew)

        'Next

        'returnDs.Tables.Add(dtNew)

        'Return returnDs
        Return ds
    End Function
#End Region


#Region "수급 분석 "
    ' 사전매집 -> 수급 장악 주가상승 -> 대중유인 - > 물량정리



#End Region

End Class
