Imports PaikRichStock.Common

Public Class frmSMainVer2016

    Private Const GroupNameFav As String = "관심종목"
    Private Const GroupNameCondition As String = "조건검색"
    Private _allStockDataset As DataSet

#Region " Enum(Spread) "
    Private Enum spGroupIndex
        DetailGroupName
        Index
        ScreenNo
    End Enum

    Private Enum spSearchCondiIndex
        SearchName
        SearchIndex
    End Enum

    Private Enum spStockListIndex
        StockName
        NowPrice
        StartPrice
        LowPrice
        HighPrice
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
        LowestMa3
    End Enum

    Private Enum spSearchResultIndex
        StockName
        NowPrice
        StartPrice
        EndPrice
        LowPrice
        HighestPrice
        LowestMa
        LowestMa2
        LowestMa3
        StockCode
        ScreenNo
    End Enum
#End Region

#Region " Enum(Type) "
    Private Enum GroupType
        All
        Fav
        Condition
        Kosdak
    End Enum
#End Region

    Private Sub frmSMainVer2016_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UcMainStock1.StatusOnEventConnect = EventOn
        UcMainStock1.StatusOnReceiveConditionVer = EventOn
        UcMainStock1.StatusOnReceiveTrCondition = EventOn
        UcMainStock1.StatusOnReceiveTrData = EventOn
    End Sub

    Private Sub UcMainStock1_OnConnection(ByVal status As String) Handles UcMainStock1.OnConnection
        _allStockDataset = UcMainStock1._allStockDataset
    End Sub


#Region " GetGroupList "
    Private Sub GetGroupList(ByVal groupType As GroupType)

        SpreadInit(spGroupList, True, 6)

        Select Case groupType
            Case groupType.All

            Case groupType.Fav
                GetFavList()
                lblGroupName.Text = GroupNameFav
            Case groupType.Condition
                lblGroupName.Text = GroupNameCondition
                GetDataUserCondition()
            Case groupType.Kosdak

        End Select
    End Sub
#End Region

#Region " 조건 검색 가져오기 "
    Private Sub GetDataUserCondition()
        UcMainStock1.GetUserConditionLoad()
    End Sub

    Private Sub UcMainStock1_OnDsGetConditionList(ByVal ds As System.Data.DataSet) Handles UcMainStock1.OnDsGetConditionList
        If ds Is Nothing = False Then
            Try
                If UcMainStock1.EVENT_STATUS.STATUS_ONRECEIVECONDITIONVER = EventOn Then
                    UcMainStock1.StatusOnReceiveConditionVer = EventOff
                End If

                Dim row As Integer = 0

                SpreadInit(spGroupList, True, 6)

                With spGroupList.ActiveSheet
                    For Each dr As DataRow In ds.Tables("CondiList").Rows
                        If .RowCount - 1 <= row Then .RowCount = .RowCount + 1
                        .Cells(row, spGroupIndex.DetailGroupName).Text = Trim(dr("CONDI_NAME"))
                        .Cells(row, spGroupIndex.Index).Text = Trim(dr("CONDI_SEQ"))
                        .Cells(row, spGroupIndex.ScreenNo).Text = "9" & Trim(dr("CONDI_SEQ"))

                        row = row + 1
                    Next
                End With

                If UcMainStock1.EVENT_STATUS.STATUS_ONRECEIVECONDITIONVER = EventOff Then
                    UcMainStock1.StatusOnReceiveConditionVer = EventOn
                End If

            Catch ex As Exception
                MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
                If UcMainStock1.EVENT_STATUS.STATUS_ONRECEIVECONDITIONVER = EventOff Then
                    UcMainStock1.StatusOnReceiveConditionVer = EventOn
                End If
            End Try
        End If
    End Sub
#End Region

#Region " 조건 검색 세부 종목 리스트를 가져온다 "
    ''' <summary>
    ''' 조건 검색 세부 종목 리스트를 가져온다
    ''' </summary>
    ''' <param name="screenNo">화면번호</param>
    ''' <param name="condiName">조건검색명</param>
    ''' <param name="CondiIndex">조건검색Index</param>
    ''' <param name="seqSearch">0 - ?</param>
    ''' <remarks></remarks>
    Private Sub GetDataCondiStockList(ByVal screenNo As String, ByVal condiName As String, ByVal CondiIndex As String, ByVal seqSearch As Integer)

        UcMainStock1.GetUserConditionStockLoad(screenNo, condiName, CondiIndex, seqSearch)

    End Sub

    Private Sub UcMainStock1_OnDsSetConditionList(ByVal ds As System.Data.DataSet) Handles UcMainStock1.OnDsSetConditionList
        If ds Is Nothing = False Then
            Try

                If UcMainStock1.EVENT_STATUS.STATUS_ONRECEIVETRCONDITION = EventOn Then
                    UcMainStock1.StatusOnReceiveTrCondition = EventOff
                End If

                SpreadInit(spStockList, True, 50)
                Dim row As Integer = 0

                With spStockList.ActiveSheet
                    For Each dr As DataRow In ds.Tables(0).Rows
                        If .RowCount - 1 <= row Then .RowCount = .RowCount + 1

                        'UcMainStock1.GetStockBaseInfo(Trim(dr("FAVST_CODE").ToString()))

                        '.Cells(row, spIndexStockList.NowPrice).Text = _dsFavStockListInfo.Tables("StockBaseInfo").Rows(0).Item("현재가")
                        '.Cells(row, spIndexStockList.StartPrice).Text = _dsFavStockListInfo.Tables("StockBaseInfo").Rows(0).Item("시가")
                        '.Cells(row, spIndexStockList.LowPrice).Text = _dsFavStockListInfo.Tables("StockBaseInfo").Rows(0).Item("저가")
                        '.Cells(row, spIndexStockList.HighestPrice).Text = _dsFavStockListInfo.Tables("StockBaseInfo").Rows(0).Item("고가")

                        .Cells(row, spStockListIndex.StockCode).Text = Trim(dr("STOCK_CODE").ToString())

                        Select Case Len((row + 1).ToString)
                            Case 1
                                .Cells(row, spStockListIndex.ScreenNo).Text = "8" & "00" & (row + 1).ToString
                            Case 2
                                .Cells(row, spStockListIndex.ScreenNo).Text = "8" & "0" & (row + 1).ToString
                            Case 3
                                .Cells(row, spStockListIndex.ScreenNo).Text = "8" & "" & (row + 1).ToString
                        End Select

                        .Cells(row, spStockListIndex.StockName).Text = Trim(dr("STOCK_NAME").ToString())

                        row = row + 1
                    Next
                End With

                If UcMainStock1.EVENT_STATUS.STATUS_ONRECEIVETRCONDITION = EventOff Then
                    UcMainStock1.StatusOnReceiveTrCondition = EventOn
                End If

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
                If UcMainStock1.EVENT_STATUS.STATUS_ONRECEIVETRCONDITION = EventOff Then
                    UcMainStock1.StatusOnReceiveTrCondition = EventOn
                End If
            End Try
        End If

    End Sub
#End Region

#Region " GetFavList                                | 관심종목 그룹을 가져온다 "
    Private Sub GetFavList()
        Dim ds As DataSet, dr As DataRow
        Dim row As Integer = 0

        Try
            Common.dbConn.Open()

            ds = dbConn.GetDataTableSp("p_Fav01Query")

            If ds.Tables(0).Rows.Count < 1 Then
                ds.Reset()
                Common.dbConn.Close()
            End If

            SpreadInit(spGroupList, True, 6)

            With spGroupList.ActiveSheet
                For Each dr In ds.Tables(0).Rows
                    If .RowCount - 1 <= row Then .RowCount = .RowCount + 1

                    .Cells(row, spGroupIndex.DetailGroupName).Text = Trim(dr("FAVST_GNAME").ToString())
                    .Cells(row, spGroupIndex.Index).Text = Trim(dr("FAVST_GID").ToString())
                    .Cells(row, spGroupIndex.ScreenNo).Text = ""

                    row = row + 1

                Next
            End With

            ds.Reset()

            Common.dbConn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
            Common.dbConn.Close()
        End Try

    End Sub
#End Region

#Region " GetStockList "
    Private Sub GetStockList()
        Dim ds As DataSet, dr As DataRow
        Dim dv As DataView
        Dim row As Integer = 0
        Dim strArrStockcode As String = ""

        dv = New DataView(_allStockDataset.Tables("StockList"))

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

                .Cells(row, spStockListIndex.StockCode).Text = Trim(dr("FAVST_CODE").ToString())

                Select Case Len((row + 1).ToString)
                    Case 1
                        .Cells(row, spStockListIndex.ScreenNo).Text = "1" & "00" & (row + 1).ToString
                    Case 2
                        .Cells(row, spStockListIndex.ScreenNo).Text = "1" & "0" & (row + 1).ToString
                    Case 3
                        .Cells(row, spStockListIndex.ScreenNo).Text = "1" & "" & (row + 1).ToString
                End Select

                'strArrStockcode = strArrStockcode & Trim(dr("FAVST_CODE").ToString()) & ";"

                'UcMainStock1.GetStockBaseInfo(Trim(dr("FAVST_CODE").ToString()), Trim(.Cells(row, spStockListIndex.ScreenNo).Text))

                dv.RowFilter = String.Format("STOCK_CODE = '{0}'", Trim(dr("FAVST_CODE").ToString()))

                For Each drRowView As DataRowView In dv
                    .Cells(row, spStockListIndex.StockName).Text = Trim(drRowView("STOCK_NAME").ToString())
                Next

                row = row + 1

            Next
        End With

        Common.dbConn.Close()

    End Sub
#End Region

#Region " SpreadInit "
    Private Sub SpreadInit(ByVal spread As FarPoint.Win.Spread.FpSpread, ByVal cleartype As Boolean, ByVal rowCount As Integer)
        spread.ActiveSheet.ClearRange(0, 0, spread.ActiveSheet.RowCount, spread.ActiveSheet.ColumnCount, cleartype)
        spread.ActiveSheet.RowCount = rowCount
    End Sub
#End Region

#Region " Control Event "
    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        UcMainStock1.Connection()
    End Sub

    Private Sub rdoGroupNo0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoGroupNo0.CheckedChanged, rdoGroupNo1.CheckedChanged, rdoGroupNo2.CheckedChanged, rdoGroupNo3.CheckedChanged
        Select Case CType(sender, RadioButton).Name
            Case rdoGroupNo0.Name
                GetGroupList(GroupType.All)
            Case rdoGroupNo1.Name
                GetGroupList(GroupType.Fav)
            Case rdoGroupNo2.Name
                GetGroupList(GroupType.Condition)
            Case rdoGroupNo3.Name
                GetGroupList(GroupType.Kosdak)
        End Select

    End Sub
#End Region

#Region " Spread Event "
    Private Sub spGroupList_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spGroupList.CellDoubleClick
        With spGroupList.ActiveSheet
            If Trim(.Cells(e.Row, spGroupIndex.DetailGroupName).Text) = "" Then Exit Sub

            If rdoGroupNo1.Checked = True And lblGroupName.Text = GroupNameFav Then
                GetStockList()
            ElseIf rdoGroupNo2.Checked = True And lblGroupName.Text = GroupNameCondition Then
                GetDataCondiStockList(Trim(.Cells(e.Row, spGroupIndex.ScreenNo).Text), Trim(.Cells(e.Row, spGroupIndex.DetailGroupName).Text), _
                                      Trim(.Cells(e.Row, spGroupIndex.Index).Text), 0)
            End If
        End With
        
    End Sub

    Private Sub spStockList_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spStockList.CellDoubleClick
        If Trim(spStockList.ActiveSheet.Cells(e.Row, spStockListIndex.StockCode).Text) = "" Then Exit Sub

        ''With spStockList.ActiveSheet
        ''    UcMainStock1.GetDayStockBaseInfo(Trim(.Cells(e.Row, spStockListIndex.StockCode).Text), Trim(.Cells(e.Row, spStockListIndex.ScreenNo).Text))
        ''End With

    End Sub
#End Region

#Region " 검색식 검색 "

    Private Sub UcMainStock1_OnDayDsBaseInfo(ByVal ds As System.Data.DataSet) Handles UcMainStock1.OnDayDsBaseInfo
        Dim row As Integer = 0
        Dim dv As DataView
        Dim row2 As Integer = 0

        Try

            dv = New DataView(ds.Tables("DayStockBaseInfoMa"))

            spInfo.ActiveSheet.ClearRange(0, 0, spInfo.ActiveSheet.RowCount, spInfo.ActiveSheet.ColumnCount, True)

            With spInfo.ActiveSheet
                For Each dr As DataRow In ds.Tables("DayStockBaseInfo").Rows

                    If .RowCount - 1 <= row Then .RowCount = .RowCount + 1

                    .Cells(row, spIndexInfoList.Day).Text = Trim(dr("일자").ToString())
                    .Cells(row, spIndexInfoList.NowPrice).Text = Trim(dr("현재가").ToString())
                    .Cells(row, spIndexInfoList.StartPrice).Text = Trim(dr("시가").ToString())
                    .Cells(row, spIndexInfoList.EndPrice).Text = Trim(dr("종가").ToString())
                    .Cells(row, spIndexInfoList.LowPrice).Text = Trim(dr("저가").ToString())
                    .Cells(row, spIndexInfoList.HighestPrice).Text = Trim(dr("고가").ToString())

                    dv.RowFilter = String.Format("일자 = '{0}'", Trim(dr("일자").ToString()))

                    For Each drRowView As DataRowView In dv
                        .Cells(row, spIndexInfoList.LowestMa).Text = Trim(drRowView("저가MA").ToString())
                        .Cells(row, spIndexInfoList.LowestMa2).Text = Trim(drRowView("최저가MA").ToString())
                        .Cells(row, spIndexInfoList.LowestMa3).Text = Trim(drRowView("최저가종가MA").ToString())
                    Next

                    With spSearchResult.ActiveSheet

                        If Trim(.Cells(0, spSearchResultIndex.StockName).Text) = "" Then
                            row2 = 0
                        Else
                            row2 = .GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1
                        End If

                        If CDec(spInfo.ActiveSheet.Cells(0, spIndexInfoList.NowPrice).Text) <= CDec(spInfo.ActiveSheet.Cells(0, spIndexInfoList.LowestMa2).Text) Then

                            .Cells(row2, spSearchResultIndex.StockName).Text = Trim(dr("종목코드"))
                            .Cells(row2, spSearchResultIndex.NowPrice).Text = Trim(dr("현재가").ToString())
                            .Cells(row2, spSearchResultIndex.StartPrice).Text = Trim(dr("시가").ToString())
                            .Cells(row2, spSearchResultIndex.EndPrice).Text = Trim(dr("종가").ToString())
                            .Cells(row2, spSearchResultIndex.LowPrice).Text = Trim(dr("저가").ToString())
                            .Cells(row2, spSearchResultIndex.HighestPrice).Text = Trim(dr("고가").ToString())
                            .Cells(row2, spSearchResultIndex.LowestMa).Text = spInfo.ActiveSheet.Cells(row, spIndexInfoList.LowestMa).Text
                            .Cells(row2, spSearchResultIndex.LowestMa2).Text = spInfo.ActiveSheet.Cells(row, spIndexInfoList.LowestMa2).Text
                            .Cells(row2, spSearchResultIndex.LowestMa3).Text = spInfo.ActiveSheet.Cells(row, spIndexInfoList.LowestMa3).Text
                            .Cells(row2, spSearchResultIndex.StockCode).Text = Trim(dr("종목코드"))
                            '.Cells(.GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1, spSearchResultIndex.ScreenNo).text    =  

                        End If
                    End With

                    row = row + 1

                    Application.DoEvents()

                Next
            End With

            If UcMainStock1.EVENT_STATUS.STATUS_ONRECEIVETRDATA = EventOff Then
                UcMainStock1.StatusOnReceiveTrData = EventOn
            End If

        Catch ex As Exception
            If UcMainStock1.EVENT_STATUS.STATUS_ONRECEIVETRDATA = EventOff Then
                UcMainStock1.StatusOnReceiveTrData = EventOn
            End If
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        With spStockList.ActiveSheet
            If Trim(.Cells(0, spStockListIndex.StockCode).Text) = "" Then Exit Sub

            For row As Integer = 0 To .GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data)

                UcMainStock1.GetDayStockBaseInfo(Trim(.Cells(row, spStockListIndex.StockCode).Text), Trim(.Cells(row, spStockListIndex.ScreenNo).Text))

                Application.DoEvents()

                System.Threading.Thread.Sleep(1000)

            Next

        End With

    End Sub
#End Region

End Class