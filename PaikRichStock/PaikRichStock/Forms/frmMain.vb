Imports PaikRichStock.Common

Public Class frmMain

    Private _dsAllStockList As DataSet
    Private _dsFavStockListInfo As DataSet
    Private _blnSearchCondition As Boolean = False

    Private Enum spIndexStockList
        StockName
        NowPrice
        StartPrice
        LowPrice
        HighestPrice
        StockCode
        ScreenNo
    End Enum

    Private Enum spIndexInfoList
        Day
        NowPrice
        StartPrice
        EndPrice
        LowPrice
        HighestPrice
        LowestMa
        LowestMa2
    End Enum

    Private Enum spIndexConList
        CondiName
        CondiIndex
        ScreenNo
    End Enum


#Region " Control Event "
    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        UcMainStock1.Connection()
    End Sub


    Private Sub btnFavStockModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFavStockModify.Click
        Dim oform As New frmFavStockModify(_dsAllStockList)

        oform.ShowDialog()

    End Sub
#End Region

#Region " GetStockList "
    Private Sub GetStockList()
        Dim ds As DataSet, dr As DataRow
        Dim dv As DataView
        Dim row As Integer = 0
        Dim strArrStockcode As String = ""

        If _dsAllStockList Is Nothing Then
            _dsAllStockList = UcMainStock1.UserGetCodeListByMarket()
        End If

        dv = New DataView(_dsAllStockList.Tables("StockList"))

        Common.dbConn.Open()

        ds = dbConn.GetDataTableSp("p_Fav01Query")

        If ds.Tables(0).Rows.Count < 1 Then
            MsgBox("내역이 존재하지 않습니다.", MsgBoxStyle.Information)
            ds.Reset()
            Common.dbConn.Close()
            Exit Sub
        End If

        spStockList.ActiveSheet.ClearRange(0, 0, spStockList.ActiveSheet.RowCount, spStockList.ActiveSheet.ColumnCount, True)

        With spStockList.ActiveSheet
            For Each dr In ds.Tables(0).Rows
                If .RowCount - 1 <= row Then .RowCount = .RowCount + 1

                'UcMainStock1.GetStockBaseInfo(Trim(dr("FAVST_CODE").ToString()))

                '.Cells(row, spIndexStockList.NowPrice).Text = _dsFavStockListInfo.Tables("StockBaseInfo").Rows(0).Item("현재가")
                '.Cells(row, spIndexStockList.StartPrice).Text = _dsFavStockListInfo.Tables("StockBaseInfo").Rows(0).Item("시가")
                '.Cells(row, spIndexStockList.LowPrice).Text = _dsFavStockListInfo.Tables("StockBaseInfo").Rows(0).Item("저가")
                '.Cells(row, spIndexStockList.HighestPrice).Text = _dsFavStockListInfo.Tables("StockBaseInfo").Rows(0).Item("고가")

                .Cells(row, spIndexStockList.StockCode).Text = Trim(dr("FAVST_CODE").ToString())

                Select Case Len((row + 1).ToString)
                    Case 1
                        .Cells(row, spIndexStockList.ScreenNo).Text = "1" & "00" & (row + 1).ToString
                    Case 2
                        .Cells(row, spIndexStockList.ScreenNo).Text = "1" & "0" & (row + 1).ToString
                    Case 3
                        .Cells(row, spIndexStockList.ScreenNo).Text = "1" & "" & (row + 1).ToString
                End Select

                'strArrStockcode = strArrStockcode & Trim(dr("FAVST_CODE").ToString()) & ";"

                UcMainStock1.GetStockBaseInfo(Trim(dr("FAVST_CODE").ToString()), Trim(.Cells(row, spIndexStockList.ScreenNo).Text))

                dv.RowFilter = String.Format("STOCK_CODE = '{0}'", Trim(dr("FAVST_CODE").ToString()))

                For Each drRowView As DataRowView In dv
                    .Cells(row, spIndexStockList.StockName).Text = Trim(drRowView("STOCK_NAME").ToString())
                Next

                row = row + 1

            Next
        End With

        Common.dbConn.Close()

    End Sub
#End Region

#Region " Event "
    Private Sub UcMainStock1_OnConnection(ByVal status As String) Handles UcMainStock1.OnConnection
        GetStockList()
    End Sub

    Private Sub UcMainStock1_OnDayDsBaseInfo(ByVal ds As System.Data.DataSet) Handles UcMainStock1.OnDayDsBaseInfo

        Dim row As Integer = 0
        Dim dv As DataView

        dv = New DataView(ds.Tables("DayStockBaseInfoMa"))

        spInfo.ActiveSheet.ClearRange(0, 0, spInfo.ActiveSheet.RowCount, spInfo.ActiveSheet.ColumnCount, True)

        With spInfo.ActiveSheet
            For Each dr As DataRow In ds.Tables("DayStockBaseInfo").Rows

                If .RowCount - 1 <= row Then .RowCount = .RowCount + 1

                .Cells(row, spIndexInfoList.Day).Text = Trim(dr("일자").ToString())
                .Cells(row, spIndexInfoList.NowPrice).Text = Trim(dr("현재가").ToString())
                .Cells(row, spIndexInfoList.StartPrice).Text = Trim(dr("시가").ToString())
                .Cells(row, spIndexInfoList.EndPrice).Text = Trim(dr("현재가").ToString())
                .Cells(row, spIndexInfoList.LowPrice).Text = Trim(dr("저가").ToString())
                .Cells(row, spIndexInfoList.HighestPrice).Text = Trim(dr("고가").ToString())

                dv.RowFilter = String.Format("일자 = '{0}'", Trim(dr("일자").ToString()))

                For Each drRowView As DataRowView In dv
                    .Cells(row, spIndexInfoList.LowestMa).Text = Trim(drRowView("저가MA").ToString())
                    .Cells(row, spIndexInfoList.LowestMa2).Text = Trim(drRowView("최저가MA").ToString())
                Next

                If row = 0 Then
                    If _blnSearchCondition = True Then
                        With spCondiStockList.ActiveSheet

                            For row2 As Integer = 0 To .GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data)
                                If Trim(.Cells(row2, spIndexStockList.StockCode).Text) = Trim(dr("종목코드")) Then

                                    .Cells(row2, spIndexStockList.NowPrice).Text = spInfo.ActiveSheet.Cells(row, spIndexInfoList.NowPrice).Text
                                    .Cells(row2, spIndexStockList.StartPrice).Text = spInfo.ActiveSheet.Cells(row, spIndexInfoList.LowestMa2).Text
                                    .Cells(row2, spIndexStockList.LowPrice).Text = spInfo.ActiveSheet.Cells(row, spIndexInfoList.LowPrice).Text
                                    .Cells(row2, spIndexStockList.HighestPrice).Text = spInfo.ActiveSheet.Cells(row, spIndexInfoList.HighestPrice).Text

                                    If CDec(spInfo.ActiveSheet.Cells(0, spIndexInfoList.NowPrice).Text) <= CDec(spInfo.ActiveSheet.Cells(0, spIndexInfoList.LowestMa2).Text) Then
                                        .Cells(row2, spIndexStockList.StockName).ForeColor = Color.Red
                                        Exit For
                                    End If
                                End If
                            Next

                        End With
                    End If
                End If

                row = row + 1

                Application.DoEvents()

            Next
        End With

      

    End Sub

    Private Sub UcMainStock1_OnDsBaseInfo(ByVal ds As System.Data.DataSet) Handles UcMainStock1.OnDsBaseInfo
        If ds Is Nothing = False Then
            Dim dr As DataRow

            'spStockList.ActiveSheet.ClearRange(0, 0, spStockList.ActiveSheet.RowCount, spStockList.ActiveSheet.ColumnCount, True)

            With spStockList.ActiveSheet
                For row As Integer = 0 To .RowCount - 1
                    For Each dr In ds.Tables("StockBaseInfo").Rows
                        If Trim(dr("종목명").ToString()) = Trim(.Cells(row, spIndexStockList.StockName).Text) Then

                            .Cells(row, spIndexStockList.NowPrice).Text = Trim(dr("현재가").ToString())
                            .Cells(row, spIndexStockList.StartPrice).Text = Trim(dr("시가").ToString())
                            .Cells(row, spIndexStockList.LowPrice).Text = Trim(dr("저가").ToString())
                            .Cells(row, spIndexStockList.HighestPrice).Text = Trim(dr("고가").ToString())

                            Exit For
                        End If
                    Next
                Next
            End With

        End If
    End Sub

    Private Sub UcMainStock1_OnDsGetConditionList(ByVal ds As System.Data.DataSet) Handles UcMainStock1.OnDsGetConditionList
        If ds Is Nothing = False Then
            Dim row As Integer = 0

            spCondiList.ActiveSheet.ClearRange(0, 0, spCondiList.ActiveSheet.RowCount, spCondiList.ActiveSheet.ColumnCount, True)

            With spCondiList.ActiveSheet
                For Each dr As DataRow In ds.Tables("CondiList").Rows
                    If .RowCount - 1 <= row Then .RowCount = .RowCount + 1

                    .Cells(row, spIndexConList.CondiName).Text = Trim(dr("CONDI_NAME"))
                    .Cells(row, spIndexConList.CondiIndex).Text = Trim(dr("CONDI_SEQ"))
                    .Cells(row, spIndexConList.ScreenNo).Text = "9" & Trim(dr("CONDI_SEQ"))

                    row = row + 1
                Next
            End With
        End If
    End Sub

    Private Sub UcMainStock1_OnDsSetConditionList(ByVal ds As System.Data.DataSet) Handles UcMainStock1.OnDsSetConditionList
        If ds Is Nothing = False Then

            Dim row As Integer = 0

            spCondiStockList.ActiveSheet.ClearRange(0, 0, spCondiStockList.ActiveSheet.RowCount, spCondiStockList.ActiveSheet.ColumnCount, True)

            With spCondiStockList.ActiveSheet
                For Each dr As DataRow In ds.Tables(0).Rows
                    If .RowCount - 1 <= row Then .RowCount = .RowCount + 1

                    'UcMainStock1.GetStockBaseInfo(Trim(dr("FAVST_CODE").ToString()))

                    '.Cells(row, spIndexStockList.NowPrice).Text = _dsFavStockListInfo.Tables("StockBaseInfo").Rows(0).Item("현재가")
                    '.Cells(row, spIndexStockList.StartPrice).Text = _dsFavStockListInfo.Tables("StockBaseInfo").Rows(0).Item("시가")
                    '.Cells(row, spIndexStockList.LowPrice).Text = _dsFavStockListInfo.Tables("StockBaseInfo").Rows(0).Item("저가")
                    '.Cells(row, spIndexStockList.HighestPrice).Text = _dsFavStockListInfo.Tables("StockBaseInfo").Rows(0).Item("고가")

                    .Cells(row, spIndexStockList.StockCode).Text = Trim(dr("STOCK_CODE").ToString())

                    Select Case Len((row + 1).ToString)
                        Case 1
                            .Cells(row, spIndexStockList.ScreenNo).Text = "8" & "00" & (row + 1).ToString
                        Case 2
                            .Cells(row, spIndexStockList.ScreenNo).Text = "8" & "0" & (row + 1).ToString
                        Case 3
                            .Cells(row, spIndexStockList.ScreenNo).Text = "8" & "" & (row + 1).ToString
                    End Select

                    .Cells(row, spIndexStockList.StockName).Text = Trim(dr("STOCK_NAME").ToString())

                    row = row + 1
                Next

            End With

        End If
    End Sub
#End Region

#Region " PaintStockGraph "
    Private Sub PaintStockGraph()
        ' 1 - 저가 2 - 시가 3 - 종가 4 - 고가

        Dim rowCnt As Integer = spInfo.ActiveSheet.GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data)

        chartStock.Data.Clear()
        chartStock.Data.Series = 4
        chartStock.Data.Points = spInfo.ActiveSheet.RowCount


        With spInfo.ActiveSheet
            For row As Integer = 1 To rowCnt + 1

                chartStock.Data(0, row) = .Cells(rowCnt - row, spIndexInfoList.LowPrice).Text
                chartStock.Data(1, row) = .Cells(rowCnt - row, spIndexInfoList.StartPrice).Text
                chartStock.Data(2, row) = .Cells(rowCnt - row, spIndexInfoList.EndPrice).Text
                chartStock.Data(3, row) = .Cells(rowCnt - row, spIndexInfoList.HighestPrice).Text

                chartStock.AxisX.Labels(row) = Trim(.Cells(rowCnt - row, spIndexInfoList.Day).Text)
            Next

        End With

     
    End Sub
#End Region

    Private Sub btnCondiAddSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCondiAddSearch.Click
        Search20MaDown()
    End Sub

#Region " Search20MaDown "
    Private Sub Search20MaDown()

        _blnSearchCondition = True

        With spCondiStockList.ActiveSheet

            For row As Integer = 0 To .GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data)
                UcMainStock1.GetDayStockBaseInfo(Trim(.Cells(row, spIndexStockList.StockCode).Text), Trim(.Cells(row, spIndexStockList.ScreenNo).Text))

                System.Threading.Thread.Sleep(1000)

            Next

        End With

    End Sub
#End Region
    
    Private Sub btnGetFav_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetFav.Click
        GetStockList()
    End Sub

    Private Sub spStockList_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spStockList.CellDoubleClick
        If Trim(spStockList.ActiveSheet.Cells(e.Row, spIndexStockList.StockCode).Text) = "" Then Exit Sub

        _blnSearchCondition = False

        With spStockList.ActiveSheet
            UcMainStock1.GetDayStockBaseInfo(Trim(.Cells(e.Row, spIndexStockList.StockCode).Text), Trim(.Cells(e.Row, spIndexStockList.ScreenNo).Text))
        End With

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        TabControl2.SelectedIndex = 1
        PaintStockGraph()
    End Sub

    Private Sub btnGetCondiList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetCondiList.Click
        UcMainStock1.GetUserConditionLoad()
    End Sub

    Private Sub spCondiList_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spCondiList.CellDoubleClick
        With spCondiList.ActiveSheet
            If Trim(.Cells(e.Row, spIndexConList.CondiIndex).Text) = "" Then Exit Sub

            UcMainStock1.GetUserConditionStockLoad(Trim(.Cells(e.Row, spIndexConList.ScreenNo).Text), Trim(.Cells(e.Row, spIndexConList.CondiName).Text), Trim(.Cells(e.Row, spIndexConList.CondiIndex).Text), 0)
        End With

    End Sub

End Class