Public Structure ReciveOrder
    Public CPrice As String
    Public Qty As String
    Public Price As String
    Public Gubun As String
    Public Auto As Boolean
End Structure


Public Class ucHogaWindowNew

    Private _MainStock As New PaikRichStock.Common.ucMainStockVer2
    Private _clsScreenNoManage As PaikRichStock.Common.clsScreenNoManage
    Private _stockCode As String = ""
    Private _rOrder As ReciveOrder
    Private _opt10075Dt As DataTable

    Public WriteOnly Property Opt10075Dt As DataTable
        Set(value As DataTable)
            _opt10075Dt = value
            dg미체결.DataSource = _opt10075Dt
        End Set
    End Property

    Public Property ROrder As ReciveOrder
        Get
            Return _rOrder
        End Get
        Set(value As ReciveOrder)
            _rOrder = value
            InitWindows()
        End Set
    End Property

    Public Property StockCode As String
        Get
            Return _stockCode
        End Get
        Set(value As String)
            _stockCode = value
            InitWindows()
        End Set
    End Property

    Public WriteOnly Property MainStock As PaikRichStock.Common.ucMainStockVer2
        Set(value As PaikRichStock.Common.ucMainStockVer2)
            _MainStock = value
            InitWindows()
        End Set
    End Property

#Region " Enum "
    Private Enum DgvBuyIndex
        Percent
        Price               ' 매수가
        BuyTradingQty       ' 매수잔량
    End Enum

    Private Enum DgvSellIndex
        Percent
        SellTradingQty       ' 매도잔량
        Price               ' 매도가
    End Enum

    Private Enum DgvTradingIndex
        TradingPrice        ' 체결가
        TradingQty          ' 체결량
    End Enum

    Private Enum tbTradeIndex
        Buy
        Sell
    End Enum

    Private Enum DgvTodayStockTradeAtIndex
        SellGrowingQty
        SellVolumeQty
        SellTradeName
        BuyTradeName
        BuyVolumeQty
        BuyGrowingQty
    End Enum
#End Region

#Region " GetDataSet "
    Private _dsGetStockHogaJanQty As New DataSet
    Private _dsStockTrade As New DataSet
    Private _dsToDayStockTradeAt As New DataSet

    Public WriteOnly Property Property_GetStockHogaJanQty() As DataSet
        Set(value As DataSet)
            _dsGetStockHogaJanQty = value
            SetDisplayHogaView()
        End Set
    End Property

    Public WriteOnly Property Property_GetStockTrade() As DataSet
        Set(value As DataSet)
            _dsStockTrade = value
            SetDisplayTradingValue()
        End Set
    End Property

    Public WriteOnly Property Property_ToDayStockTradeAt() As DataSet
        Set(value As DataSet)
            _dsToDayStockTradeAt = value
            SetToDayStockTradeAt()
        End Set
    End Property
#End Region

#Region " Grid Paint Event "
    Private Sub dgvSell_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvSell.CellPainting
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Exit Sub
        If e.ColumnIndex <> DgvSellIndex.SellTradingQty Then Exit Sub

        e.Handled = True
        Dim oColor As Drawing.Color = Color.Gray

        Bar.PintaDegradado(oColor, e, CInt(dgvSell.Rows(e.RowIndex).Cells(DgvSellIndex.Percent).Value), 50, Color.Orange)

    End Sub

    Private Sub dgvBuy_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvBuy.CellPainting
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Exit Sub
        If e.ColumnIndex <> DgvBuyIndex.BuyTradingQty Then Exit Sub

        e.Handled = True

        Dim oColor As Drawing.Color = Color.Gray

        Bar.PintaDegradado(oColor, e, CInt(dgvBuy.Rows(e.RowIndex).Cells(DgvBuyIndex.Percent).Value), 50, Color.Orange)

    End Sub
#End Region

#Region " InitWindows "
    Private Sub InitWindows()
        lblPreDayPrice.Text = ""
        lblStartPrice.Text = ""
        lblHighestPrice.Text = ""
        lblLowestPrice.Text = ""
        lblCurrentPrice.Text = ""
        lblTradingQty.Text = ""
        lblUpDownRate.Text = ""
        lblStockName.Text = ""
        dgvBuy.Rows.Clear()
        dgvSell.Rows.Clear()
        dgvTrading.Rows.Clear()

        lblStockCode.Text = _stockCode

        If cboAccount.Items.Count = 0 Then
            _MainStock.GetAccount()

            For Each dr As DataRow In _MainStock._AccNo.Tables("ACCNO").Rows
                cboAccount.Items.Add(Trim(dr("ACCNO").ToString()))
            Next

            If cboAccount.Items.Count > 0 Then
                cboAccount.SelectedIndex = 0
            End If
        End If

        If _stockCode <> "" Then
            lblStockName.Text = GetStockInfo(_stockCode)
        End If

    End Sub

#Region " 종목명을 반환한다. "
    Private Function GetStockInfo(ByVal stockCode As String) As String
        Dim dv As DataView
        If _MainStock._allStockDataset Is Nothing Then Return ""
        dv = New DataView(_MainStock._allStockDataset.Tables("StockList"))

        dv.RowFilter = String.Format("STOCK_CODE = '{0}'", _stockCode)

        For Each drRowView As DataRowView In dv
            Return Trim(drRowView("STOCK_NAME").ToString())
        Next

        Return ""
    End Function
#End Region

#End Region

#Region " SetDisplayTradingValue "
    Private Sub SetDisplayTradingValue()
        If _dsStockTrade Is Nothing Then Exit Sub

        Dim dr As DataRow = _dsStockTrade.Tables("StockTrade").Rows(0)

        If dgvTrading.Rows.Count = 0 Then
            dgvTrading.Rows.Add(CInt(dr("현재가")), CInt(("거래량")))
        Else
            dgvTrading.Rows.Insert(0, 1)
            dgvTrading.Rows(0).Cells(DgvTradingIndex.TradingPrice).Value = CInt(dr("현재가"))
            dgvTrading.Rows(0).Cells(DgvTradingIndex.TradingQty).Value = CInt(dr("거래량"))
        End If

        lblPreDayPrice.Text = dr("전일대비").ToString()
        lblStartPrice.Text = dr("시가").ToString()
        lblHighestPrice.Text = dr("고가").ToString()
        lblLowestPrice.Text = dr("저가").ToString()
        lblCurrentPrice.Text = dr("현재가").ToString()
        lblTradingQty.Text = dr("누적거래량").ToString()
        lblUpDownRate.Text = dr("등락율").ToString()

    End Sub
#End Region

#Region " SetDisplayHogaView "
    Private Sub SetDisplayHogaView()
        If _dsGetStockHogaJanQty Is Nothing = True Then Exit Sub

        Dim dr As DataRow

        Dim intMaxBuyHoga As Integer = 0
        Dim intMaxSellHoga As Integer = 0

        dgvBuy.Rows.Clear()
        dgvSell.Rows.Clear()

        ' dr = _dsGetStockHogaJanQty.Tables("StockHogaJanQty").Rows(0)
        dr = _dsGetStockHogaJanQty.Tables(0).Rows(0)

        For i As Integer = 1 To 10
            If i = 1 Then
                intMaxBuyHoga = CInt(dr("매수호가수량" & i.ToString()))
            Else
                If intMaxBuyHoga < CInt(dr("매수호가수량" & i.ToString())) Then
                    intMaxBuyHoga = CInt(dr("매수호가수량" & i.ToString()))
                End If
            End If

            If i = 1 Then
                intMaxSellHoga = CInt(dr("매도호가수량" & i.ToString()))
            Else
                If intMaxSellHoga < CInt(dr("매도호가수량" & i.ToString())) Then
                    intMaxSellHoga = CInt(dr("매도호가수량" & i.ToString()))
                End If
            End If
        Next

        With dgvBuy.Rows
            For i As Integer = 1 To 10
                .Add(Math.Abs(CInt(dr("매수호가수량" & i.ToString())) / intMaxBuyHoga) * 100, CInt(dr("매수호가" & i.ToString())), CInt(dr("매수호가수량" & i.ToString())))
            Next
        End With

        With dgvSell.Rows
            For i As Integer = 10 To 1 Step -1
                .Add(Math.Abs(CInt(dr("매도호가수량" & i.ToString())) / intMaxSellHoga) * 100, CInt(dr("매도호가수량" & i.ToString())), CInt(dr("매도호가" & i.ToString())))
            Next
        End With

        _dsGetStockHogaJanQty.Reset()

    End Sub
#End Region

#Region " SetDisplayHogaView "
    Private Sub SetToDayStockTradeAt()
        If _dsToDayStockTradeAt Is Nothing = True Then Exit Sub

        Dim dr As DataRow

        dgvTradeAt.Rows.Clear()

        With dgvTradeAt
            For Each dr In _dsToDayStockTradeAt.Tables(0).Rows
                For i As Integer = 1 To 5
                    .Rows.Insert(i - 1, 1)

                    .Rows(i - 1).Cells(DgvTodayStockTradeAtIndex.SellGrowingQty).Value = dr("매도거래원별증감" & i.ToString())
                    .Rows(i - 1).Cells(DgvTodayStockTradeAtIndex.SellVolumeQty).Value = dr("매도거래원수량" & i.ToString())
                    .Rows(i - 1).Cells(DgvTodayStockTradeAtIndex.SellTradeName).Value = dr("매도거래원" & i.ToString())
                    .Rows(i - 1).Cells(DgvTodayStockTradeAtIndex.BuyTradeName).Value = dr("매수거래원" & i.ToString())
                    .Rows(i - 1).Cells(DgvTodayStockTradeAtIndex.BuyVolumeQty).Value = dr("매수거래원수량" & i.ToString())
                    .Rows(i - 1).Cells(DgvTodayStockTradeAtIndex.BuyGrowingQty).Value = dr("매수거래원별증감" & i.ToString())

                Next
            Next
        End With


    End Sub
#End Region

#Region " Order "
    Private Sub btnBuy_Click(sender As Object, e As EventArgs) Handles btnBuy.Click
        SendBuyOrder()
    End Sub

    Private Sub btnSell_Click(sender As Object, e As EventArgs) Handles btnSell.Click
        SendSellOrder()
    End Sub

    Private Sub SendBuyOrder()
        If lblStockCode.Text = "" Then Exit Sub
        If numBuyQty.Value = 0 Then
            MsgBox("수량이 1보다 작을수 없습니다.")
            Exit Sub
        End If

        _MainStock.SendOrder_OnReceiveChejanData("매수", Trim(cboAccount.Text), Common.ucMainStockVer2.OrderType.신규매수, Trim(lblStockName.Text), Trim(lblStockCode.Text), _
                             numBuyQty.Value, numBuyPrice.Value, Mid(cboBuyTradeGb.Text, 1, 2), "")

    End Sub

    Private Sub SendSellOrder()
        If lblStockCode.Text = "" Then Exit Sub
        If numSellQty.Value = 0 Then
            MsgBox("수량이 1보다 작을수 없습니다.")
            Exit Sub
        End If

        _MainStock.SendOrder_OnReceiveChejanData("매도", Trim(cboAccount.Text), Common.ucMainStockVer2.OrderType.신규매도, Trim(lblStockName.Text), Trim(lblStockCode.Text), _
                             numSellQty.Value, numSellPrice.Value, Mid(cboSellTradeGb.Text, 1, 2), "")

    End Sub

    Private Sub chkSellCurrentPrice_CheckedChanged(sender As Object, e As EventArgs) Handles chkSellCurrentPrice.CheckedChanged
        If IsNumeric(lblCurrentPrice.Text) AndAlso chkSellCurrentPrice.Checked = True Then
            numSellPrice.Value = Math.Abs(CInt(lblCurrentPrice.Text))
        End If
    End Sub

    Private Sub chkBuyCurrentPrice_CheckedChanged(sender As Object, e As EventArgs) Handles chkBuyCurrentPrice.CheckedChanged
        If chkBuyCurrentPrice.Checked = True Then
            If lblCurrentPrice.Text <> "" Then
                numBuyPrice.Value = CInt(Replace(Replace(lblCurrentPrice.Text, "-", ""), "+", ""))
            End If
        End If
    End Sub
#End Region

    Private Sub _MainStock_OnDsopt10085(ByVal ds As System.Data.DataSet)
        If ds Is Nothing = True Then Exit Sub
        If ds.Tables.Count = 0 Then Exit Sub
        If ds.Tables(0).Rows.Count = 0 Then Exit Sub
        If ds Is Nothing = False Then
            numSellQty.Value = CInt(ds.Tables("opt10085").Rows(0).Item("보유수량"))
        End If

    End Sub

    Private Sub chkSellAvaQty_CheckedChanged(sender As Object, e As EventArgs) Handles chkSellAvaQty.CheckedChanged
        If chkSellAvaQty.Checked = True Then
            _MainStock.Opt10085_OnReceiveChejanData(Trim(cboAccount.Text), "0998")
        End If
    End Sub

    Public Sub SetManualOrder()
        lblCurrentPrice.Text = _rOrder.CPrice
        If _rOrder.Gubun = "1" Then '매수
            cboBuyTradeGb.SelectedIndex = 0
            tbTrade.SelectedIndex = 0
            numBuyQty.Value = _rOrder.Qty
            numBuyPrice.Value = _rOrder.Price
            chkBuyCurrentPrice.Checked = _rOrder.Auto
        ElseIf _rOrder.Gubun = "2" Then '매도
            cboBuyTradeGb.SelectedIndex = 1
            tbTrade.SelectedIndex = 1
            numSellQty.Value = _rOrder.Qty
            numSellPrice.Value = _rOrder.Price
            chkSellCurrentPrice.Checked = _rOrder.Auto
        End If
    End Sub

    Private Sub dgvSell_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSell.CellDoubleClick
        numBuyPrice.Value = Math.Abs(dgvSell.Rows(e.RowIndex).Cells(DgvSellIndex.Price).Value)
        numSellPrice.Value = Math.Abs(dgvSell.Rows(e.RowIndex).Cells(DgvSellIndex.Price).Value)
    End Sub

    Private Sub dgvBuy_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBuy.CellDoubleClick
        numBuyPrice.Value = Math.Abs(dgvBuy.Rows(e.RowIndex).Cells(DgvBuyIndex.Price).Value)
        numSellPrice.Value = Math.Abs(dgvBuy.Rows(e.RowIndex).Cells(DgvBuyIndex.Price).Value)
    End Sub

    Private Sub numBuyPrice_ValueChanged(sender As Object, e As EventArgs) Handles numBuyPrice.ValueChanged
        If numBuyPrice.Value < 1000 Then
            numBuyPrice.Increment = 1
        ElseIf numBuyPrice.Value < 5000 Then
            numBuyPrice.Increment = 5
        ElseIf numBuyPrice.Value < 10000 Then
            numBuyPrice.Increment = 10
        ElseIf numBuyPrice.Value < 50000 Then
            numBuyPrice.Increment = 50
        ElseIf numBuyPrice.Value < 100000 Then      
            numBuyPrice.Increment = 100
        Else
            '10만원이상일때 코스닥은 무조건
            numBuyPrice.Increment = 100
            '코스피는 10만원 이상 일때 500 50만원 이상일때 1000 
        End If
    End Sub

    Private Sub numSellPrice_ValueChanged(sender As Object, e As EventArgs) Handles numSellPrice.ValueChanged
        If numSellPrice.Value < 1000 Then
            numSellPrice.Increment = 1
        ElseIf numSellPrice.Value < 5000 Then
            numSellPrice.Increment = 5
        ElseIf numSellPrice.Value < 10000 Then
            numSellPrice.Increment = 10
        ElseIf numSellPrice.Value < 50000 Then
            numSellPrice.Increment = 50
        ElseIf numSellPrice.Value < 100000 Then
            numSellPrice.Increment = 100
        Else
            '10만원이상일때 코스닥은 무조건
            numSellPrice.Increment = 100
            '코스피는 10만원 이상 일때 500 50만원 이상일때 1000 
        End If
    End Sub

    Private Sub lblCurrentPrice_TextChanged(sender As Object, e As EventArgs) Handles lblCurrentPrice.TextChanged
        If IsNumeric(lblCurrentPrice.Text) = True And chkBuyCurrentPrice.Checked = True Then
            numBuyPrice.Value = Math.Abs(CInt(lblCurrentPrice.Text))
        End If
        If IsNumeric(lblCurrentPrice.Text) = True And chkSellCurrentPrice.Checked = True Then
            numSellPrice.Value = Math.Abs(CInt(lblCurrentPrice.Text))
        End If
    End Sub

    Private Sub btnMi_Click(sender As Object, e As EventArgs) Handles btnMi.Click
        If dg미체결.Visible = False Then
            _MainStock.Opt10075_OnReceiveChejanData(cboAccount.Text)
        End If
        dg미체결.Visible = Not dg미체결.Visible
    End Sub

    Private Sub dg미체결_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dg미체결.CellDoubleClick
        txtisSellBuy.Text = dg미체결.Rows(e.RowIndex).Cells("주문구분").Value
        txtOrderNo.Text = dg미체결.Rows(e.RowIndex).Cells("주문번호").Value
        txtModStockCode.Text = Strings.Right(dg미체결.Rows(e.RowIndex).Cells("종목코드").Value, 6)
        txtModStockName.Text = dg미체결.Rows(e.RowIndex).Cells("종목명").Value
        numModQty.Value = dg미체결.Rows(e.RowIndex).Cells("미체결수량").Value
        numModPrice.Value = dg미체결.Rows(e.RowIndex).Cells("주문가격").Value
    End Sub

    Private Sub btn취소_Click(sender As Object, e As EventArgs) Handles btn취소.Click
        If txtOrderNo.Text = "" Then
            MsgBox("미체결내역을 선택해 주세요!")
            Exit Sub
        End If

        SendCancelOrder()
    End Sub

    Private Sub SendCancelOrder()
        Dim gb As Common.ucMainStockVer2.OrderType
        If txtModStockCode.Text = "" Then Exit Sub
        If numModQty.Value = 0 Then
            MsgBox("수량이 1보다 작을수 없습니다.")
            Exit Sub
        End If

        If txtisSellBuy.Text = "+매수" Then
            gb = Common.ucMainStockVer2.OrderType.매수취소
        Else
            gb = Common.ucMainStockVer2.OrderType.매도취소
        End If
        _MainStock.SendOrder_OnReceiveChejanData("취소", Trim(cboAccount.Text), gb, Trim(lblStockName.Text), Trim(txtModStockCode.Text), _
                             numModQty.Value, 0, "00", txtOrderNo.Text.Trim)

    End Sub

    Private Sub btn정정_Click(sender As Object, e As EventArgs) Handles btn정정.Click
        If txtOrderNo.Text = "" Then
            MsgBox("미체결내역을 선택해 주세요!")
            Exit Sub
        End If

        SendModOrder()
    End Sub

    Private Sub SendModOrder()
        Dim gb As Common.ucMainStockVer2.OrderType
        If txtModStockCode.Text = "" Then Exit Sub
        If numModQty.Value = 0 Then
            MsgBox("수량이 1보다 작을수 없습니다.")
            Exit Sub
        End If

        If txtisSellBuy.Text = "+매수" Then
            gb = Common.ucMainStockVer2.OrderType.매수정정
        Else
            gb = Common.ucMainStockVer2.OrderType.매도정정
        End If
        _MainStock.SendOrder_OnReceiveChejanData("정정", Trim(cboAccount.Text), gb, Trim(lblStockName.Text), Trim(txtModStockCode.Text), _
                             numModQty.Value, 0, "00", txtOrderNo.Text.Trim)

    End Sub
End Class
