Public Class frmPaikStockMainVer3
    Private _blnDartStart As Boolean = False
    Private _clsScreenNoManage As PaikRichStock.Common.clsScreenNoManage

#Region " DgvRealDataIndex "
    Private Enum DgvRealDataIndex
        STOCK_NAME          ' 종목명
        CurrentPrice        ' 현재가
        lossGainRate        ' 등락율
        VolumePower         ' 체결강도
        TradingVolume       ' 거래량
        StartPrice          ' 시가
        HighestPrice        ' 고가
        LowestPrice         ' 저가
        TradingTime         ' 체결시간
        PreDayBySymbol      ' 전일대비기호
        STOCK_CODE
        ScreenNo_GetIn      ' 화면번호
    End Enum

    Private Enum DgvRealDataDartIndex
        STOCK_NAME          ' 종목명
        CurrentPrice        ' 현재가
        lossGainRate        ' 등락율
        VolumePower         ' 체결강도
        TradingVolume       ' 거래량
        StartPrice          ' 시가
        HighestPrice        ' 고가
        LowestPrice         ' 저가
        TradingTime         ' 체결시간
        PreDayBySymbol      ' 전일대비기호
        STOCK_CODE
        ScreenNo_GetIn      ' 화면번호
        creator
        link
        title
        FirstTradeQty
        NextTradeQty
        FirstAllowQty
        NextAllowQty
        FirstPrice
        FirstTradeTime
        NextTradeTime
    End Enum
#End Region

#Region " Load & Connect "
    Private Sub frmPaikStockMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _blnDartStart = False
        UcDart1.timer1.Stop()
    End Sub

    Private Sub ucMainStockVer2_OnConnection(status As String) Handles UcMainStockVer2.OnEventConnect
        'SetBaseFavControl(0)
        'SetBaseStockListDisplay()

        UcMainStockVer2.GetAccount()

        For Each dr As DataRow In UcMainStockVer2._AccNo.Tables("ACCNO").Rows
            cboAccount.Items.Add(Trim(dr("ACCNO").ToString()))
        Next

        cboAccount.SelectedIndex = 0

        UcHogaWindowNew.MainStock = UcMainStockVer2
        UcAnalysisHogaWindow1.MainStock = UcMainStockVer2
      
        UcMainStockVer2.Opt10085_OnReceiveChejanData(Trim(cboAccount.Text), "0998")

        UcFavManage1.MainStock2 = UcMainStockVer2

    End Sub
#End Region

#Region " 1. 실시간 종목 데이터 수신 Tx "

#Region " RealDataDisplay "
    Private Sub RealDataDisplay(ByVal ds As DataSet)
        Dim dr As DataRow = ds.Tables(0).Rows(0)

        With dgvMyStock
            For i As Integer = 0 To .Rows.Count - 1
                If dr("STOCK_CODE") = Trim(.Rows(i).Cells(DgvRealDataIndex.STOCK_CODE).Value) Then

                    '            Dim dgvRow As DataGridViewRow = .Rows


                    '            DataGridViewRow row = dgv.Rows
                    '    .Cast<DataGridViewRow>()
                    '    .Where(r => r.Cells["SystemId"].Value.ToString().Equals(searchValue))
                    '    .First();

                    'rowIndex = row.Index;

                    .Rows(i).Cells(DgvRealDataIndex.CurrentPrice).Value = Trim(dr("현재가").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.lossGainRate).Value = Trim(dr("등락율").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.VolumePower).Value = Trim(dr("체결강도").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.TradingVolume).Value = Trim(dr("거래량").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.StartPrice).Value = Trim(dr("시가").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.HighestPrice).Value = Trim(dr("고가").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.LowestPrice).Value = Trim(dr("저가").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.TradingTime).Value = Trim(dr("체결시간").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.PreDayBySymbol).Value = Trim(dr("전일대비기호").ToString())

                    Exit For

                End If
            Next
        End With

        With dgvDart
            For i As Integer = 0 To .Rows.Count - 1
                If dr("STOCK_CODE") = Trim(.Rows(i).Cells(DgvRealDataDartIndex.STOCK_CODE).Value) Then

                    .Rows(i).Cells(DgvRealDataDartIndex.CurrentPrice).Value = Trim(dr("현재가").ToString())
                    .Rows(i).Cells(DgvRealDataDartIndex.lossGainRate).Value = Trim(dr("등락율").ToString())
                    .Rows(i).Cells(DgvRealDataDartIndex.VolumePower).Value = Trim(dr("체결강도").ToString())
                    .Rows(i).Cells(DgvRealDataDartIndex.TradingVolume).Value = Trim(dr("거래량").ToString())
                    .Rows(i).Cells(DgvRealDataDartIndex.StartPrice).Value = Trim(dr("시가").ToString())
                    .Rows(i).Cells(DgvRealDataDartIndex.HighestPrice).Value = Trim(dr("고가").ToString())
                    .Rows(i).Cells(DgvRealDataDartIndex.LowestPrice).Value = Trim(dr("저가").ToString())
                    .Rows(i).Cells(DgvRealDataDartIndex.TradingTime).Value = Trim(dr("체결시간").ToString())
                    .Rows(i).Cells(DgvRealDataDartIndex.PreDayBySymbol).Value = Trim(dr("전일대비기호").ToString())

                    If Trim(.Rows(i).Cells(DgvRealDataDartIndex.FirstTradeQty).Value) <> "" Then
                        .Rows(i).Cells(DgvRealDataDartIndex.NextTradeQty).Value = Trim(dr("거래량").ToString())
                    Else
                        .Rows(i).Cells(DgvRealDataDartIndex.FirstTradeQty).Value = Trim(dr("거래량").ToString())
                    End If

                    If Trim(.Rows(i).Cells(DgvRealDataDartIndex.FirstAllowQty).Value) <> "" Then
                        .Rows(i).Cells(DgvRealDataDartIndex.NextAllowQty).Value = Trim(dr("체결강도").ToString())
                    Else
                        .Rows(i).Cells(DgvRealDataDartIndex.FirstAllowQty).Value = Trim(dr("체결강도").ToString())
                    End If

                    If Trim(.Rows(i).Cells(DgvRealDataDartIndex.FirstAllowQty).Value) = "" Then
                        .Rows(i).Cells(DgvRealDataDartIndex.FirstPrice).Value = Trim(dr("현재가").ToString())
                    End If

                    If Trim(.Rows(i).Cells(DgvRealDataDartIndex.FirstTradeTime).Value) <> "" Then
                        .Rows(i).Cells(DgvRealDataDartIndex.NextTradeTime).Value = Trim(dr("체결시간").ToString())
                    Else
                        .Rows(i).Cells(DgvRealDataDartIndex.FirstTradeTime).Value = Trim(dr("체결시간").ToString())
                    End If

                    Exit For

                End If
            Next
        End With

        With dgvCondition
            For i As Integer = 0 To .Rows.Count - 1
                If dr("STOCK_CODE") = Trim(.Rows(i).Cells(DgvRealDataIndex.STOCK_CODE).Value) Then

                    .Rows(i).Cells(DgvRealDataIndex.CurrentPrice).Value = Trim(dr("현재가").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.lossGainRate).Value = Trim(dr("등락율").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.VolumePower).Value = Trim(dr("체결강도").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.TradingVolume).Value = Trim(dr("거래량").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.StartPrice).Value = Trim(dr("시가").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.HighestPrice).Value = Trim(dr("고가").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.LowestPrice).Value = Trim(dr("저가").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.TradingTime).Value = Trim(dr("체결시간").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.PreDayBySymbol).Value = Trim(dr("전일대비기호").ToString())

                    Exit For

                End If
            Next
        End With

        With dgvFav
            For i As Integer = 0 To .Rows.Count - 1
                If dr("STOCK_CODE") = Trim(.Rows(i).Cells(DgvRealDataIndex.STOCK_CODE).Value) Then

                    .Rows(i).Cells(DgvRealDataIndex.CurrentPrice).Value = Trim(dr("현재가").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.lossGainRate).Value = Trim(dr("등락율").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.VolumePower).Value = Trim(dr("체결강도").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.TradingVolume).Value = Trim(dr("거래량").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.StartPrice).Value = Trim(dr("시가").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.HighestPrice).Value = Trim(dr("고가").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.LowestPrice).Value = Trim(dr("저가").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.TradingTime).Value = Trim(dr("체결시간").ToString())
                    .Rows(i).Cells(DgvRealDataIndex.PreDayBySymbol).Value = Trim(dr("전일대비기호").ToString())

                    Exit For

                End If
            Next
        End With

    End Sub

#End Region

#Region " 종목 실시간 데이터 수신 Tx "
    Private Sub UcMainStockVer2_OnReceiveRealData_HogaJanQty(ds As DataSet) Handles UcMainStockVer2.OnReceiveRealData_HogaJanQty
        If ds.Tables.Count = 0 Then Exit Sub

        If UcAnalysisHogaWindow1.StockCode = Trim(ds.Tables(0).Rows(0).Item("STOCK_CODE")) Then
            UcAnalysisHogaWindow1.Property_GetStockHogaJanQty = ds
        End If

        If UcHogaWindowNew.StockCode = Trim(ds.Tables(0).Rows(0).Item("STOCK_CODE")) Then
            UcHogaWindowNew.Property_GetStockHogaJanQty = ds
        End If

    End Sub

    Private Sub UcMainStockVer2_OnReceiveRealData_TodayTradePort(ds As DataSet) Handles UcMainStockVer2.OnReceiveRealData_TodayTradePort
        If ds.Tables.Count = 0 Then Exit Sub

        If UcAnalysisHogaWindow1.StockCode = Trim(ds.Tables(0).Rows(0).Item("STOCK_CODE")) Then
            UcAnalysisHogaWindow1.Property_ToDayStockTradeAt = ds
        End If

        If UcHogaWindowNew.StockCode = Trim(ds.Tables(0).Rows(0).Item("STOCK_CODE")) Then
            UcHogaWindowNew.Property_ToDayStockTradeAt = ds
        End If

    End Sub

    Private Sub ucMainStockVer2_OnReceiveRealData_Volume(ds As DataSet) Handles UcMainStockVer2.OnReceiveRealData_Volume
        If ds.Tables.Count = 0 Then Exit Sub

        If UcAnalysisHogaWindow1.StockCode = Trim(ds.Tables(0).Rows(0).Item("STOCK_CODE")) Then
            UcAnalysisHogaWindow1.Property_GetStockTrade = ds
        End If

        If UcHogaWindowNew.StockCode = Trim(ds.Tables(0).Rows(0).Item("STOCK_CODE")) Then
            UcHogaWindowNew.Property_GetStockTrade = ds
        End If

        RealDataDisplay(ds)

    End Sub
#End Region

#Region " 종목 선택 이벤트 "
    Private Sub dgvDart_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDart.CellContentDoubleClick, dgvMyStock.CellDoubleClick, dgvFav.CellDoubleClick, dgvCondition.CellDoubleClick
        Dim oDataGridView As DataGridView = CType(sender, DataGridView)

        With oDataGridView
            Select Case .Name
                Case dgvDart.Name
                    If Trim(.Rows(e.RowIndex).Cells(DgvRealDataDartIndex.STOCK_CODE).Value) = "" Then
                        Exit Sub
                    End If

                    UcHogaWindowNew.StockCode = Trim(.Rows(e.RowIndex).Cells(DgvRealDataDartIndex.STOCK_CODE).Value)
                    UcAnalysisHogaWindow1.StockCode = Trim(.Rows(e.RowIndex).Cells(DgvRealDataDartIndex.STOCK_CODE).Value)
                    'UcTotalVolumeAnaylsis1.StockCode = Trim(.Rows(e.RowIndex).Cells(DgvRealDataDartIndex.STOCK_CODE).Value)

                Case dgvMyStock.Name
                    If Trim(.Rows(e.RowIndex).Cells(DgvRealDataIndex.STOCK_CODE).Value) = "" Then
                        Exit Sub
                    End If

                    UcHogaWindowNew.StockCode = Trim(.Rows(e.RowIndex).Cells(DgvRealDataIndex.STOCK_CODE).Value)
                    UcAnalysisHogaWindow1.StockCode = Trim(.Rows(e.RowIndex).Cells(DgvRealDataIndex.STOCK_CODE).Value)
                    'UcTotalVolumeAnaylsis1.StockCode = Trim(.Rows(e.RowIndex).Cells(DgvRealDataDartIndex.STOCK_CODE).Value)

                Case dgvFav.Name
                    If Trim(.Rows(e.RowIndex).Cells(DgvRealDataIndex.STOCK_CODE).Value) = "" Then
                        Exit Sub
                    End If

                    UcHogaWindowNew.StockCode = Trim(.Rows(e.RowIndex).Cells(DgvRealDataIndex.STOCK_CODE).Value)
                    UcAnalysisHogaWindow1.StockCode = Trim(.Rows(e.RowIndex).Cells(DgvRealDataIndex.STOCK_CODE).Value)
                    'UcTotalVolumeAnaylsis1.StockCode = Trim(.Rows(e.RowIndex).Cells(DgvRealDataDartIndex.STOCK_CODE).Value)
                Case dgvCondition.Name
                    If Trim(.Rows(e.RowIndex).Cells(DgvRealDataIndex.STOCK_CODE).Value) = "" Then
                        Exit Sub
                    End If

                    UcHogaWindowNew.StockCode = Trim(.Rows(e.RowIndex).Cells(DgvRealDataIndex.STOCK_CODE).Value)
                    UcAnalysisHogaWindow1.StockCode = Trim(.Rows(e.RowIndex).Cells(DgvRealDataIndex.STOCK_CODE).Value)
                    'UcTotalVolumeAnaylsis1.StockCode = Trim(.Rows(e.RowIndex).Cells(DgvRealDataDartIndex.STOCK_CODE).Value)
            End Select

        End With

    End Sub
#End Region

#End Region

#Region " 2. 공시 "
    ' 공시 발생 시 넘어오는 이벤트
    Private Sub UcDart1_OnChangeDartV(dartV As DartPrj.UcDart.dartValue) Handles UcDart1.OnChangeDartV
        Dim dv As DataView

        dv = New DataView(UcMainStockVer2._allStockDataset.Tables("StockList"))

        'dv.RowFilter = String.Format("STOCK_NAME LIKE '{0}", "%" & dartV.title & "%")
        dv.RowFilter = String.Format("STOCK_NAME LIKE '{0}'", "%" & dartV.creator & "%")

        Dim screenNo As String = ""
        Dim blnRealGb As Boolean = False

        With dgvDart
            For Each drRowView As DataRowView In dv

                blnRealGb = False

                For i As Integer = 0 To .RowCount - 1
                    If Trim(.Rows(i).Cells(DgvRealDataDartIndex.STOCK_CODE).Value) = Trim(drRowView("STOCK_CODE").ToString()) Then
                        blnRealGb = True
                        Exit For
                    End If
                Next

                If blnRealGb = False Then
                    .Rows.Insert(0, 1)

                    .Rows(0).Cells(DgvRealDataDartIndex.STOCK_CODE).Value = Trim(drRowView("STOCK_CODE").ToString())
                    .Rows(0).Cells(DgvRealDataDartIndex.STOCK_NAME).Value = Trim(drRowView("STOCK_NAME").ToString())
                    .Rows(0).Cells(DgvRealDataDartIndex.creator).Value = dartV.creator
                    .Rows(0).Cells(DgvRealDataDartIndex.link).Value = dartV.link
                    .Rows(0).Cells(DgvRealDataDartIndex.title).Value = dartV.title

                End If

                For i As Integer = 0 To dgvFav.RowCount - 1
                    If Trim(dgvFav.Rows(i).Cells(DgvRealDataIndex.STOCK_CODE).Value) = Trim(drRowView("STOCK_CODE").ToString()) Then
                        blnRealGb = True
                        Exit For
                    End If
                Next

                For i As Integer = 0 To dgvMyStock.RowCount - 1
                    If Trim(dgvMyStock.Rows(i).Cells(DgvRealDataIndex.STOCK_CODE).Value) = Trim(drRowView("STOCK_CODE").ToString()) Then
                        blnRealGb = True
                        Exit For
                    End If
                Next

                For i As Integer = 0 To dgvCondition.RowCount - 1
                    If Trim(dgvCondition.Rows(i).Cells(DgvRealDataIndex.STOCK_CODE).Value) = Trim(drRowView("STOCK_CODE").ToString()) Then
                        blnRealGb = True
                        Exit For
                    End If
                Next

                If blnRealGb = True Then
                    Continue For
                Else
                    UcMainStockVer2.OptKWFid_OnReceiveRealData(Trim(drRowView("STOCK_CODE").ToString()), 1)
                    System.Threading.Thread.Sleep(600)
                End If

            Next
        End With
    End Sub

    Private Sub btnDartStart_Click(sender As Object, e As EventArgs) Handles btnDartStart.Click
        _blnDartStart = Not _blnDartStart
        If _blnDartStart = False Then
            btnDartStart.Text = "▶"
            UcDart1.timer1.Stop()
        Else

            btnDartStart.Text = "||"
            UcDart1.timer1.Interval = Val(msktDartTimer.Text)
            UcDart1.timer1.Start()

        End If
    End Sub

#End Region

#Region " 3. 조건검색 "
    Private Enum DgvCondiListIndex
        DetailGroupName
        Index
        ScreenNo
    End Enum

    Private Enum DgvConditionStockListIndex
        STOCK_NAME
        CurrentPrice
        lossGainRate
        VolumePower
        TradingVolume
        StartPrice
        HighestPrice
        LowestPrice
        TradingTime
        PreDayBySymbol
        STOCK_CODE
        ScreenNo_GetIn
    End Enum

    Private Sub GetConditionList()
        UcMainStockVer2.GetConditionLoad_OnReceiveTrCondition()
    End Sub

    Private Sub drvConditionList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles drvConditionList.CellDoubleClick
        If Trim(drvConditionList.Rows(e.RowIndex).Cells(DgvCondiListIndex.ScreenNo).Value) = "" Then Exit Sub

        With drvConditionList
            UcMainStockVer2.SendCondition_OnReceiveConditionVer(Trim(drvConditionList.Rows(e.RowIndex).Cells(DgvCondiListIndex.ScreenNo).Value), Trim(drvConditionList.Rows(e.RowIndex).Cells(DgvCondiListIndex.DetailGroupName).Value), _
                                                    Trim(drvConditionList.Rows(e.RowIndex).Cells(DgvCondiListIndex.Index).Value), 0)

        End With
    End Sub

    Private Sub dgvConditionStockList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvConditionStockList.CellDoubleClick

    End Sub

    Private Sub btnConditionFavAdd_Click(sender As Object, e As EventArgs) Handles btnConditionFavAdd.Click
        If Trim(dgvConditionStockList.Rows(0).Cells(DgvConditionStockListIndex.STOCK_CODE).Value) <> "" Then
            UcFavManage1.addDs = PaikRichStock.Common.clsFunc.FavAddDataGridViewToDataSet(dgvConditionStockList)
            tcMain.SelectedIndex = 2
        End If
    End Sub

    Private Sub SettingConditionStockListDetailData(ByVal ds As DataSet)
        'If ds Is Nothing = True Then Exit Sub
        'Dim row As Integer = 0
        'Dim dr As DataRow

        'With spConDetailList.ActiveSheet
        '    For Each dr In ds.Tables(0).Rows
        '        For row = 0 To .GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data)
        '            If Trim(dr("STOCK_CODE").ToString()) = Trim(.Cells(row, FavStockList.StockCode).Text) Then
        '                .Cells(row, spConDetailListIndex.NowPrice).Text = Trim(dr("현재가").ToString())
        '                .Cells(row, spConDetailListIndex.UpDownRate).Text = Trim(dr("등락율").ToString())
        '                .Cells(row, spConDetailListIndex.PrevRateSymbol).Text = Trim(dr("전일대비기호").ToString())
        '                .Cells(row, spConDetailListIndex.StartPrice).Text = Trim(dr("시가").ToString())
        '                .Cells(row, spConDetailListIndex.HighPrice).Text = Trim(dr("고가").ToString())
        '                .Cells(row, spConDetailListIndex.LowPrice).Text = Trim(dr("저가").ToString())

        '                Select Case Len(row.ToString())
        '                    Case 1
        '                        .Cells(row, spConDetailListIndex.ScreenNo).Text = "2" & "00" & row.ToString()
        '                    Case 2
        '                        .Cells(row, spConDetailListIndex.ScreenNo).Text = "2" & "0" & row.ToString()
        '                    Case 3
        '                        .Cells(row, spConDetailListIndex.ScreenNo).Text = "2" & row.ToString()
        '                End Select

        '                Exit Sub
        '            End If
        '        Next

        '    Next
        'End With

        'Application.DoEvents()

    End Sub


    Private Sub GetDayBaseStockInfo()

        'With spConDetailList.ActiveSheet
        '    If Trim(.Cells(0, spConDetailListIndex.ScreenNo).Text) = "" Then Exit Sub

        '    For row As Integer = 0 To .GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data)

        '        ucMainStockVer2.GetDayStockBaseInfo(Trim(.Cells(row, spConDetailListIndex.StockCode).Text), Trim(.Cells(row, spConDetailListIndex.ScreenNo).Text))

        '        System.Threading.Thread.Sleep(500)

        '    Next

        'End With

    End Sub

#Region " UcMainStock Event "
    Private Sub ucMainStockVer2_OnReceiveTrCondition(ByVal ds As System.Data.DataSet, ByVal index As String, ByVal scrNo As String, ByVal conName As String) Handles UcMainStockVer2.OnReceiveTrCondition
        If ds Is Nothing = False Then
            Try

                dgvConditionStockList.Rows.Clear()
                Dim row As Integer = 0

                With dgvConditionStockList
                    For Each dr As DataRow In ds.Tables(0).Rows
                        .Rows.Insert(row, 1)

                        .Rows(row).Cells(DgvConditionStockListIndex.STOCK_CODE).Value = Trim(dr("STOCK_CODE").ToString())

                        Select Case Len((row + 1).ToString)
                            Case 1
                                .Rows(row).Cells(DgvConditionStockListIndex.ScreenNo_GetIn).Value = "4" & "00" & (row + 1).ToString
                            Case 2
                                .Rows(row).Cells(DgvConditionStockListIndex.ScreenNo_GetIn).Value = "4" & "0" & (row + 1).ToString
                            Case 3
                                .Rows(row).Cells(DgvConditionStockListIndex.ScreenNo_GetIn).Value = "4" & "" & (row + 1).ToString
                        End Select

                        .Rows(row).Cells(DgvConditionStockListIndex.STOCK_NAME).Value = Trim(dr("STOCK_NAME").ToString())

                        row = row + 1
                    Next

                End With

                Application.DoEvents()

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Exclamation)

            End Try
        End If
    End Sub

    Private Sub ucMainStockVer2_OnReceiveConditionVer(ByVal ds As System.Data.DataSet) Handles UcMainStockVer2.OnReceiveConditionVer
        If ds Is Nothing = False Then
            Try
                If ds Is Nothing = False Then
                    Dim row As Integer = 0

                    drvConditionList.Rows.Clear()

                    With drvConditionList
                        For Each dr As DataRow In ds.Tables("CondiList").Rows
                            .Rows.Insert(row, 1)

                            .Rows(row).Cells(DgvCondiListIndex.DetailGroupName).Value = Trim(dr("CONDI_NAME"))
                            .Rows(row).Cells(DgvCondiListIndex.Index).Value = Trim(dr("CONDI_SEQ"))
                            .Rows(row).Cells(DgvCondiListIndex.ScreenNo).Value = "9" & Trim(dr("CONDI_SEQ"))

                            row = row + 1
                        Next
                    End With
                End If
            Catch ex As Exception
                MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)

            End Try
        End If
    End Sub

#End Region

#End Region

#Region " 4. 관심종목"


#End Region

#Region " 종목명을 반환한다. "
    Private Function GetStockInfo(ByVal stockCode As String) As String
        Dim dv As DataView

        dv = New DataView(UcMainStockVer2._allStockDataset.Tables("StockList"))

        dv.RowFilter = String.Format("STOCK_CODE = '{0}'", stockCode)

        For Each drRowView As DataRowView In dv
            Return Trim(drRowView("STOCK_NAME").ToString())
        Next

        Return ""
    End Function
#End Region

#Region " 매수, 매도 이벤트 "
    Private Sub ucMainStockVer2_OnReceiveChejanData(ByVal ds As System.Data.DataSet) Handles UcMainStockVer2.OnReceiveChejanData
        If ds Is Nothing = False Then
            Dim row As Integer = 0
            Dim dr As DataRow

            dr = ds.Tables("ChejanFidList").Rows(0)

            UcMainStockVer2.Opt10085_OnReceiveChejanData(Trim(cboAccount.Text), "0998")

            If UcMainStockVer2._OrderResult Is Nothing = False Then
                If UcMainStockVer2._OrderResult.Tables.Count > 0 Then
                    dgvTradeInfo.DataSource = UcMainStockVer2._OrderResult.Tables(0)
                    dgvTradeInfo.AutoResizeRows()
                End If
            End If

        End If
    End Sub

    Private Sub ucMainStockVer2_OnReceiveTrData_opt10085(ds As DataSet) Handles UcMainStockVer2.OnReceiveTrData_opt10085

        Dim blnExist As Boolean = False
        Dim screenNo As String = ""
        Dim strStockCode As String = ""
        Dim nCount As Integer = 0

        For Each dr As DataRow In ds.Tables(0).Rows

            screenNo = ""

            blnExist = False

            For i As Integer = 0 To dgvMyStock.Rows.Count - 1
                If Trim(dr("종목코드").ToString()) = Trim(dgvMyStock.Rows(i).Cells(DgvRealDataIndex.STOCK_CODE).Value) Then
                    blnExist = True
                End If
            Next

            If blnExist = False Then
                dgvMyStock.Rows.Insert(0, 1)
                dgvMyStock.Rows(0).Cells(DgvRealDataIndex.STOCK_CODE).Value = Trim(dr("종목코드").ToString())
                dgvMyStock.Rows(0).Cells(DgvRealDataIndex.STOCK_NAME).Value = Trim(dr("종목명").ToString())

                If strStockCode = "" Then
                    strStockCode = Trim(dr("종목코드").ToString())
                Else
                    strStockCode = strStockCode & ";" & Trim(dr("종목코드").ToString())
                End If


                'If ucMainStockVer2.InOptKWFidScreenNo("", Trim(dr("종목코드").ToString())) = Common.ucMainStock.ReturnScreenNo.Success Then
                '    screenNo = ucMainStockVer2.GetOptKWFidScreenNo(Trim(dr("종목코드").ToString()))
                '    ucMainStockVer2.GetOptKWFid(Trim(dr("종목코드").ToString()), 1, screenNo)
                '    System.Threading.Thread.Sleep(600)
                'Else
                '    Continue For
                'End If
                nCount = nCount + 1
            End If
        Next

        If strStockCode <> "" Then

            UcMainStockVer2.OptKWFid_OnReceiveRealData(strStockCode, nCount)
            System.Threading.Thread.Sleep(600)

        End If

        dgvAccountInfo.DataSource = ds

    End Sub
#End Region

    Private Sub tcMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tcMain.SelectedIndexChanged
        Select Case tcMain.SelectedIndex
            Case 1
                GetConditionList()
        End Select
    End Sub


    Private Sub conMenuStart_Click(sender As Object, e As EventArgs) Handles conMenuStart.Click
        UcMainStockVer2.OptKWFid_OnReceiveRealData(Trim(dgvDart.Rows(dgvDart.CurrentRow.Index).Cells(DgvRealDataDartIndex.STOCK_CODE).Value), 1)
        dgvScreenNo.DataSource = UcMainStockVer2._DtScreenNoManage
    End Sub

    Private Sub conMenuStop_Click(sender As Object, e As EventArgs) Handles conMenuStop.Click
        UcMainStockVer2.DisconnectRealDataStockCode(Trim(dgvDart.Rows(dgvDart.CurrentRow.Index).Cells(DgvRealDataDartIndex.STOCK_CODE).Value))
        dgvScreenNo.DataSource = UcMainStockVer2._DtScreenNoManage
    End Sub

    Private Sub conMenuFavAdd_Click(sender As Object, e As EventArgs) Handles conMenuFavAdd.Click
        If UcFavManage1.returnDs Is Nothing = False Then
            With dgvFav
                dgvFav.Rows.Clear()
                dgvFav.RowCount = UcFavManage1.returnDs.Tables(0).Rows.Count

                Dim row As Integer = 0

                For Each dr As DataRow In UcFavManage1.returnDs.Tables(0).Rows

                    .Rows.Item(row).Cells(DgvRealDataIndex.STOCK_CODE).Value = Trim(dr("STOCK_CODE").ToString())
                    .Rows.Item(row).Cells(DgvRealDataIndex.STOCK_NAME).Value = Trim(dr("STOCK_NAME").ToString())

                    row = row + 1

                    Application.DoEvents()

                Next
            End With
            
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click


    End Sub

    Private Sub UcTotalVolumeAnaylsis1_Load(sender As Object, e As EventArgs)

    End Sub
End Class