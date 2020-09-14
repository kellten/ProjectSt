Public Class Tech_TypicalPrice

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
        Dim dtNew As New DataTable(tagetTableName)
        Dim dr As DataRow, drNew As DataRow
        Dim totalValue As Integer = 0
        Dim cnt As Integer = 0
        Dim lowestValue As Integer = 0
        Dim lowestEndValue As Integer = 0
        Dim highestEndValue As Integer = 0

        With dt.Columns
            .Add("일자", System.Type.GetType("System.String"))
            .Add("저가MA", System.Type.GetType("System.Int32"))
            .Add("기간최저가", System.Type.GetType("System.Int32"))
            .Add("기간종가최저가", System.Type.GetType("System.Int32"))
            .Add("기간종가최고가", System.Type.GetType("System.Int32"))
            .Add("최저가MA", System.Type.GetType("System.Int32"))
            .Add("최저가종가MA", System.Type.GetType("System.Int32"))
            .Add("최고저가종가MA", System.Type.GetType("System.Int32"))
        End With

        With dtNew.Columns
            For column As Integer = 0 To ds.Tables(tagetTableName).Columns.Count - 1
                .Add(ds.Tables(tagetTableName).Columns(column).ColumnName, ds.Tables(tagetTableName).Columns(column).DataType)
            Next

            .Add("저가MA", System.Type.GetType("System.Int32"))
            .Add("기간최저가", System.Type.GetType("System.Int32"))
            .Add("기간종가최저가", System.Type.GetType("System.Int32"))
            .Add("기간종가최고가", System.Type.GetType("System.Int32"))
            .Add("최저가MA", System.Type.GetType("System.Int32"))
            .Add("최저가종가MA", System.Type.GetType("System.Int32"))
            .Add("최고저가종가MA", System.Type.GetType("System.Int32"))

        End With

        With ds.Tables(tagetTableName)

            For i As Integer = 0 To (.Rows.Count - period - 1)

                'dr = dt.Rows.Add()
                dr = dt.NewRow

                totalValue = 0
                lowestValue = 0
                highestEndValue = 0

                For row As Integer = i To period - 1 + i
                    totalValue = totalValue + .Rows(row).Item("저가")
                Next

                For row As Integer = i To period - 1 + i
                    If lowestValue = 0 Then
                        lowestValue = .Rows(row).Item("저가")
                    Else
                        If lowestValue >= .Rows(row).Item("저가") Then
                            lowestValue = .Rows(row).Item("저가")
                        End If
                    End If
                Next

                For row As Integer = i To period - 1 + i
                    If lowestEndValue = 0 Then
                        lowestEndValue = (CDec(.Rows(row).Item("현재가")) + CDec(.Rows(row).Item("저가"))) / 2
                    Else
                        If lowestEndValue >= (CDec(.Rows(row).Item("현재가")) + CDec(.Rows(row).Item("저가"))) / 2 Then
                            lowestEndValue = (CDec(.Rows(row).Item("현재가")) + CDec(.Rows(row).Item("저가"))) / 2
                        End If
                    End If
                Next

                For row As Integer = i To period - 1 + i
                    If highestEndValue = 0 Then
                        highestEndValue = (CDec(.Rows(row).Item("현재가")) + CDec(.Rows(row).Item("저가"))) / 2
                    Else
                        If highestEndValue <= (CDec(.Rows(row).Item("현재가")) + CDec(.Rows(row).Item("저가"))) / 2 Then
                            highestEndValue = (CDec(.Rows(row).Item("현재가")) + CDec(.Rows(row).Item("저가"))) / 2
                        End If
                    End If
                Next

                dr("기간최저가") = lowestValue
                dr("기간종가최저가") = lowestEndValue
                dr("기간종가최고가") = highestEndValue
                dr("저가MA") = totalValue / period
                dr("일자") = Trim(ds.Tables(tagetTableName).Rows(i).Item("일자"))

                dt.Rows.Add(dr)
            Next

        End With

        For i As Integer = 0 To dt.Rows.Count - period - 1
            totalValue = 0

            For row As Integer = i To period - 1 + i
                totalValue = totalValue + dt.Rows(row).Item("기간최저가")
            Next

            dt.Rows(i).Item("최저가MA") = totalValue / period

        Next

        For i As Integer = 0 To dt.Rows.Count - period - 1
            totalValue = 0

            For row As Integer = i To period - 1 + i
                totalValue = totalValue + dt.Rows(row).Item("기간종가최저가")
            Next

            dt.Rows(i).Item("최저가종가MA") = totalValue / period

        Next

        For i As Integer = 0 To dt.Rows.Count - period - 1
            totalValue = 0

            For row As Integer = i To period - 1 + i
                totalValue = totalValue + dt.Rows(row).Item("기간종가최고가")
            Next

            dt.Rows(i).Item("최고저가종가MA") = totalValue / period
        Next

        Dim dv As DataView = New DataView(dt)

        For Each dr2th As DataRow In ds.Tables(tagetTableName).Rows

            'drNew = dtNew.Rows.Add
            drNew = dtNew.NewRow

            For i As Integer = 0 To ds.Tables(tagetTableName).Columns.Count - 1
                drNew(ds.Tables(tagetTableName).Columns(i).ColumnName) = dr2th(ds.Tables(tagetTableName).Columns(i).ColumnName)
            Next

            dv.RowFilter = String.Format("일자 = '{0}'", Trim(dr2th("일자").ToString()))

            For Each drRowView As DataRowView In dv

                drNew("저가MA") = drRowView("저가MA")
                drNew("기간최저가") = drRowView("기간최저가")
                drNew("기간종가최저가") = drRowView("기간종가최저가")
                drNew("기간종가최고가") = drRowView("기간종가최고가")
                drNew("최저가MA") = drRowView("최저가MA")
                drNew("최저가종가MA") = drRowView("최저가종가MA")
                drNew("최고저가종가MA") = drRowView("최고저가종가MA")
            Next

            dtNew.Rows.Add(drNew)

        Next

        returnDs.Tables.Add(dtNew)

        Return returnDs
    End Function
#End Region


#Region "수급 분석 "
    ' 사전매집 -> 수급 장악 주가상승 -> 대중유인 - > 물량정리



#End Region

End Class
