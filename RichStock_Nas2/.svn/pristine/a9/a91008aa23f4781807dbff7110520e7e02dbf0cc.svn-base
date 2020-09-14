Public Class ucAnalysisHogaWindow

    Private _MainStock As New PaikRichStock.Common.ucMainStockVer2
    Private _stockCode As String = ""

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
        End Set
    End Property

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
            '  SetToDayStockTradeAt()
        End Set
    End Property
#End Region

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

    End Sub

#End Region

#Region " SetDisplayHogaView "
    Private Sub SetDisplayHogaView()
        If _dsGetStockHogaJanQty Is Nothing = True Then Exit Sub
        Dim dr As DataRow

        Dim intMaxBuyHoga As Integer = 0
        Dim intMaxSellHoga As Integer = 0
        Dim xSellValue As Integer = 0
        Dim yBuyValue As Integer = 0
        Dim xSellRowIndex As Integer = 0
        Dim xBuyRowIndex As Integer = 0

        dr = _dsGetStockHogaJanQty.Tables(0).Rows(0)

        For i As Integer = 1 To 10
            If i = 1 Then
                intMaxBuyHoga = Math.Abs(CInt(dr("매수호가수량" & i.ToString())))
            Else
                If intMaxBuyHoga < Math.Abs(CInt(dr("매수호가수량" & i.ToString()))) Then
                    intMaxBuyHoga = Math.Abs(CInt(dr("매수호가수량" & i.ToString())))
                End If
            End If

            If i = 1 Then
                intMaxSellHoga = Math.Abs(CInt(dr("매도호가수량" & i.ToString())))
            Else
                If intMaxSellHoga < Math.Abs(CInt(dr("매도호가수량" & i.ToString()))) Then
                    intMaxSellHoga = Math.Abs(CInt(dr("매도호가수량" & i.ToString())))
                End If
            End If
        Next


        With dgvSell
            For row As Integer = dgvSell.Rows.Count - 1 To 0 Step -1

                If Math.Abs(CInt(.Rows(row).Cells(DgvSellIndex.Price).Value)) <= Math.Abs(CInt(dr("매도호가10"))) Then
                    If CInt(.Rows(row).Cells(DgvSellIndex.Price).Value) = 0 Then Continue For
                    .Rows.RemoveAt(row)
                End If

            Next
        End With

        With dgvBuy
            For row As Integer = .Rows.Count - 1 To 0 Step -1

                If Math.Abs(CInt(.Rows(row).Cells(DgvBuyIndex.Price).Value)) >= Math.Abs(CInt(dr("매수호가10"))) Then
                    If CInt(.Rows(row).Cells(DgvBuyIndex.Price).Value) = 0 Then Continue For
                    .Rows.RemoveAt(row)
                End If



            Next
        End With
        

        With dgvBuy
            For i As Integer = 10 To 1 Step -1

                .Rows.Insert(0, 1)

                .Rows(0).Cells(DgvBuyIndex.Price).Value = CInt(dr("매수호가" & i.ToString()))
                .Rows(0).Cells(DgvBuyIndex.Percent).Value = Math.Abs(CInt(dr("매수호가수량" & i.ToString())) / intMaxBuyHoga) * 100
                .Rows(0).Cells(DgvBuyIndex.BuyTradingQty).Value = CInt(dr("매수호가수량" & i.ToString()))

            Next
        End With

        Dim row2th As Integer = 0

        With dgvSell
            For i As Integer = 10 To 1 Step -1

                If .Rows.Count < 1 Then
                    row2th = 0
                Else
                    row2th = .Rows.Count - 1
                End If

                .Rows.Insert(row2th, 1)

                .Rows(row2th).Cells(DgvSellIndex.Price).Value = CInt(dr("매도호가" & i.ToString()))
                .Rows(row2th).Cells(DgvSellIndex.Percent).Value = Math.Abs(CInt(dr("매도호가수량" & i.ToString())) / intMaxBuyHoga) * 100
                .Rows(row2th).Cells(DgvSellIndex.SellTradingQty).Value = CInt(dr("매도호가수량" & i.ToString()))

            Next

            .FirstDisplayedCell = dgvSell(DgvSellIndex.SellTradingQty, .Rows.Count - 1)
        End With

    End Sub
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

End Class
