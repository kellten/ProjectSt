Imports PaikRichStock.Common

Public Class ucFavList
   
    Private _DataAcc As New DataAccess
    Private _AllStockDataset As DataSet
    Private _KospiDataset As DataSet
    Private _KosDakDataset As DataSet
    Private _favDs As DataSet

    'Sub New(ByVal allStockDataset As DataSet, ByVal KospiDataset As DataSet, ByVal KosDakDataset As DataSet, ByVal favDs As DataSet)

    '    ' 이 호출은 Windows Form 디자이너에 필요합니다.
    '    InitializeComponent()

    '    ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    '    'If Me.DesignMode = False Then
    '    '    _AllStockDataset = allStockDataset
    '    '    _KospiDataset = KospiDataset
    '    '    _KosDakDataset = KosDakDataset
    '    '    _favDs = favDs
    '    'End If
    'End Sub

    Private _MainStock2 As New PaikRichStock.Common.ucMainStockVer2

    Public WriteOnly Property MainStock2 As PaikRichStock.Common.ucMainStockVer2
        Set(value As PaikRichStock.Common.ucMainStockVer2)
            _MainStock2 = value
            InitDsStockList()
        End Set
    End Property

    Private _returnFavDs As DataSet

    Public ReadOnly Property ReturnFavDs As DataSet
        Get
            Return _returnFavDs
        End Get
    End Property

    Private Enum dgvFavStockListIndex
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

    Private Enum dgvIndex
        StockName
        StockCode
    End Enum

    Private _clsFavStock As New PaikRichStock.Common.clsFavStock

    Private Sub InitDsStockList()

        If _AllStockDataset Is Nothing = True Then
            _AllStockDataset = _MainStock2._allStockDataset
        End If

        If _KospiDataset Is Nothing = True Then
            _KospiDataset = _MainStock2._KospiStockDataset
        End If

        If _KosDakDataset Is Nothing = True Then
            _KosDakDataset = _MainStock2._KosDakStockDataset
        End If

    End Sub


    Private Sub SettingFavStockListData(ByVal stockId As String, ByVal interId As Integer)
        Dim ds As DataSet, dr As DataRow
        Dim strCode As String = ""
        Dim row As Integer = 0
        Dim screenNo As String = ""

        dgvFavStockList.Rows.Clear()

        txtGroupName.Text = ""

        ds = _clsFavStock.GetDataFavStockList(stockId, interId)

        If ds.Tables(0).Rows.Count < 1 Then
            ds.Reset()
            Exit Sub
        End If

        txtGroupName.Text = ds.Tables(0).Rows(0).Item("INTER_NAME")
        lblInterId.Text = ds.Tables(0).Rows(0).Item("INTER_ID")

        With dgvFavStockList

            For Each dr In ds.Tables(0).Rows

                .Rows.Insert(row, 1)

                strCode = strCode & Trim(dr("STOCK_CODE").ToString()) & ";"

                .Rows(row).Cells(dgvFavStockListIndex.STOCK_CODE).Value = Trim(dr("STOCK_CODE").ToString())
                .Rows(row).Cells(dgvFavStockListIndex.STOCK_NAME).Value = GetStockInfo(Trim(dr("STOCK_CODE").ToString()))

                row = row + 1

            Next

        End With

        If _returnFavDs Is Nothing = True Then
            _returnFavDs = PaikRichStock.Common.clsFunc.FavAddDataGridViewToDataSet(dgvFavStockList).Copy
        Else
            _returnFavDs.Reset()
            _returnFavDs = PaikRichStock.Common.clsFunc.FavAddDataGridViewToDataSet(dgvFavStockList).Copy
        End If

        ds.Reset()

    End Sub

    Private Sub SettingFavStockListDetailData(ByVal ds As DataSet)
        If ds Is Nothing = True Then Exit Sub
        Dim row As Integer = 0
        Dim dr As DataRow

        With dgvFavStockList
            For Each dr In ds.Tables(0).Rows
                For row = 0 To .Rows.Count - 1
                    If Trim(dr("STOCK_CODE").ToString()) = Trim(.Rows(row).Cells(dgvFavStockListIndex.STOCK_CODE).Value) Then

                        .Rows(row).Cells(dgvFavStockListIndex.CurrentPrice).Value = Trim(dr("현재가").ToString())
                        .Rows(row).Cells(dgvFavStockListIndex.lossGainRate).Value = Trim(dr("등락율").ToString())
                        .Rows(row).Cells(dgvFavStockListIndex.PreDayBySymbol).Value = Trim(dr("전일대비기호").ToString())
                        .Rows(row).Cells(dgvFavStockListIndex.StartPrice).Value = Trim(dr("시가").ToString())
                        .Rows(row).Cells(dgvFavStockListIndex.HighestPrice).Value = Trim(dr("고가").ToString())
                        .Rows(row).Cells(dgvFavStockListIndex.LowestPrice).Value = Trim(dr("저가").ToString())

                        Exit Sub
                    End If
                Next

            Next
        End With

        Application.DoEvents()

    End Sub

#Region " 종목명을 반환한다. "
    Private Function GetStockInfo(ByVal stockCode As String) As String
        Dim dv As DataView

        dv = New DataView(_MainStock2._allStockDataset.Tables("StockList"))

        dv.RowFilter = String.Format("STOCK_CODE = '{0}'", stockCode)

        For Each drRowView As DataRowView In dv
            Return Trim(drRowView("STOCK_NAME").ToString())
        Next

        Return ""
    End Function
#End Region

#Region " Control Event "

#Region " SetBaseFavControl "
    Private Sub SetBaseFavControl(ByVal interval As Integer)
        If interval = -1 Then
            If btnFav1.Text = "1" Then Exit Sub
        End If

        If interval = 1 Then
            If btnFav5.Text = "50" Then Exit Sub
        End If

        If interval = 0 Then
            For i As Integer = 1 To 5
                CType(PaikRichStock.Common.clsFunc.FindControl(Me, "btnFav" & i.ToString), Button).Text = i.ToString()
            Next
        Else
            For i As Integer = 1 To 5
                CType(PaikRichStock.Common.clsFunc.FindControl(Me, "btnFav" & i.ToString), Button).Text = _
                Val(CType(PaikRichStock.Common.clsFunc.FindControl(Me, "btnFav" & i.ToString), Button).Text) + interval
            Next
        End If

        SetBaseFavColorSetting()

    End Sub

    Private _PaintedBaseFavTitle As String = ""

    Private Sub SetBaseFavColorSetting()
        For i As Integer = 1 To 5
            If CType(PaikRichStock.Common.clsFunc.FindControl(Me, "btnFav" & i.ToString), Button).Text = _PaintedBaseFavTitle Then
                CType(PaikRichStock.Common.clsFunc.FindControl(Me, "btnFav" & i.ToString), Button).BackColor = Color.Yellow
            Else
                CType(PaikRichStock.Common.clsFunc.FindControl(Me, "btnFav" & i.ToString), Button).BackColor = Color.Transparent
            End If
        Next
    End Sub
#End Region

#Region " Fav 버튼 관련 "
    Private Sub btnNextInter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextInter.Click
        SetBaseFavControl(1)
    End Sub

    Private Sub btnPrevInter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevInter.Click
        SetBaseFavControl(-1)
    End Sub

    Private Sub btnFavDeung_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnFav1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFav1.Click, btnFav2.Click, btnFav3.Click, btnFav4.Click, _
                                                                                                  btnFav5.Click

        SettingFavStockListData(Mid(cboStockId.Text, 1, 6), CInt(CType(sender, Button).Text))

        CType(sender, Button).BackColor = Color.Yellow

        _PaintedBaseFavTitle = Trim(CType(sender, Button).Text)

        lblInterId.Text = CType(sender, Button).Text

        For i As Integer = 1 To 5
            If CType(PaikRichStock.Common.clsFunc.FindControl(Me, "btnFav" & i.ToString), Button).Name = CType(sender, Button).Name Then
                Continue For
            Else
                CType(PaikRichStock.Common.clsFunc.FindControl(Me, "btnFav" & i.ToString), Button).BackColor = SystemColors.Control
            End If
        Next

    End Sub
#End Region
    
#End Region

#Region " "
    Private Function GetFavRealData() As Boolean
        With dgvFavStockList
            If .Rows.Count = 0 Then Return False
            If .Rows(0).Cells(dgvFavStockListIndex.STOCK_CODE).Value.ToString().Trim = "" Then Return False

            Try

                Dim strStockCode As String = ""
            Dim nCount As Integer = 0

            For row As Integer = 0 To .Rows.Count - 1

                If strStockCode = "" Then
                    strStockCode = .Rows(0).Cells(dgvFavStockListIndex.STOCK_CODE).Value.ToString().Trim
                Else
                    strStockCode = strStockCode & ";" & .Rows(0).Cells(dgvFavStockListIndex.STOCK_CODE).Value.ToString().Trim
                End If

                nCount = nCount + 1
                Next

                If strStockCode = "" Then Return False

                lblScreenNo.Text = _MainStock2.OptKWFid_OnReceiveRealData(strStockCode, nCount)

                If lblScreenNo.Text = "" Then
                    MsgBox(" 화면번호를 받아오지 못했습니다.", MsgBoxStyle.Information)
                    Return False
                End If

                Return True

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
                Return False
            End Try

        End With
    End Function
#End Region

    Private Sub btnCallRealData_Click(sender As Object, e As EventArgs) Handles btnCallRealData.Click
        If btnCallRealData.Text = "▶" Then
            If GetFavRealData() = True Then
                btnCallRealData.Text = "||"
            Else
                btnCallRealData.Text = "▶"
            End If
        Else
            _MainStock2.DisconnectRealData(Trim(lblScreenNo.Text))
        End If
    End Sub

    Private Sub dgvFavStockList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFavStockList.CellDoubleClick
        If dgvFavStockList.Rows(e.RowIndex).Cells(dgvFavStockListIndex.STOCK_CODE).Value.ToString().Trim = "" Then Exit Sub

        RaiseEvent SelectedStockCode(dgvFavStockList.Rows(e.RowIndex).Cells(dgvFavStockListIndex.STOCK_CODE).Value.ToString().Trim, _
                                      dgvFavStockList.Rows(e.RowIndex).Cells(dgvFavStockListIndex.STOCK_NAME).Value.ToString().Trim)
        RaiseEvent SelectedOnlyReturnStockCode(dgvFavStockList.Rows(e.RowIndex).Cells(dgvFavStockListIndex.STOCK_CODE).Value.ToString().Trim)
    End Sub

    Public Event SelectedStockCode(ByVal stockCode As String, ByVal stockName As String)
    Public Event SelectedOnlyReturnStockCode(ByVal stockCode As String)

End Class
