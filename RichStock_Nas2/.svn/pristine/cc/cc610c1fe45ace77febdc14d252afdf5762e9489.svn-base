Imports PaikRichStock.Common

Public Class ucFavManage
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
            InitDsStockList2()
        End Set
    End Property

    Private _MainStock As New PaikRichStock.Common.ucMainStock

    Public WriteOnly Property MainStock As PaikRichStock.Common.ucMainStock
        Set(value As PaikRichStock.Common.ucMainStock)
            _MainStock = value
            InitDsStockList()
        End Set
    End Property

    Private _returnDs As DataSet

    Public ReadOnly Property returnDs As DataSet
        Get
            Return _returnDs
        End Get
    End Property


    Private _addDs As DataSet

    Public WriteOnly Property addDs As DataSet
        Set(value As DataSet)
            _addDs = value
            SettingAddStockList()
        End Set
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
        Dim row As Integer = 0

        If _AllStockDataset Is Nothing = True Then
            _AllStockDataset = _MainStock._allStockDataset

            dgvAll.Rows.Clear()

            For Each dr As DataRow In _AllStockDataset.Tables(0).Rows

                dgvAll.Rows.Insert(row, 1)

                dgvAll.Rows(row).Cells(dgvIndex.StockName).Value = dr("STOCK_NAME")
                dgvAll.Rows(row).Cells(dgvIndex.StockCode).Value = dr("STOCK_CODE")

                row = row + 1
            Next
        End If

        row = 0

        If _KospiDataset Is Nothing = True Then
            dgvKospi.Rows.Clear()

            _KospiDataset = _MainStock._KospiStockDataset
            For Each dr As DataRow In _KospiDataset.Tables(0).Rows

                dgvKospi.Rows.Insert(row, 1)

                dgvKospi.Rows(row).Cells(dgvIndex.StockName).Value = dr("STOCK_NAME")
                dgvKospi.Rows(row).Cells(dgvIndex.StockCode).Value = dr("STOCK_CODE")

                row = row + 1
            Next
        End If

        row = 0

        If _KosDakDataset Is Nothing = True Then
            _KosDakDataset = _MainStock._KosDakStockDataset

            dgvKosdak.Rows.Clear()

            For Each dr As DataRow In _KosDakDataset.Tables(0).Rows
                dgvKosdak.Rows.Insert(row, 1)
                dgvKosdak.Rows(row).Cells(dgvIndex.StockName).Value = dr("STOCK_NAME")
                dgvKosdak.Rows(row).Cells(dgvIndex.StockCode).Value = dr("STOCK_CODE")

                row = row + 1
            Next
        End If

    End Sub

    Private Sub InitDsStockList2()
        Dim row As Integer = 0

        If _AllStockDataset Is Nothing = True Then
            _AllStockDataset = _MainStock2._allStockDataset

            dgvAll.Rows.Clear()

            For Each dr As DataRow In _AllStockDataset.Tables(0).Rows

                dgvAll.Rows.Insert(row, 1)

                dgvAll.Rows(row).Cells(dgvIndex.StockName).Value = dr("STOCK_NAME")
                dgvAll.Rows(row).Cells(dgvIndex.StockCode).Value = dr("STOCK_CODE")

                row = row + 1
            Next
        End If

        row = 0

        If _KospiDataset Is Nothing = True Then
            dgvKospi.Rows.Clear()

            _KospiDataset = _MainStock2._KospiStockDataset
            For Each dr As DataRow In _KospiDataset.Tables(0).Rows

                dgvKospi.Rows.Insert(row, 1)

                dgvKospi.Rows(row).Cells(dgvIndex.StockName).Value = dr("STOCK_NAME")
                dgvKospi.Rows(row).Cells(dgvIndex.StockCode).Value = dr("STOCK_CODE")

                row = row + 1
            Next
        End If

        row = 0

        If _KosDakDataset Is Nothing = True Then
            _KosDakDataset = _MainStock2._KosDakStockDataset

            dgvKosdak.Rows.Clear()

            For Each dr As DataRow In _KosDakDataset.Tables(0).Rows
                dgvKosdak.Rows.Insert(row, 1)
                dgvKosdak.Rows(row).Cells(dgvIndex.StockName).Value = dr("STOCK_NAME")
                dgvKosdak.Rows(row).Cells(dgvIndex.StockCode).Value = dr("STOCK_CODE")

                row = row + 1
            Next
        End If

    End Sub

    Private Sub SettingAddStockList()
        Dim row As Integer = 0

        dgvAddStock.Rows.Clear()

        For Each dr As DataRow In _addDs.Tables(0).Rows

            dgvAddStock.Rows.Insert(row, 1)

            dgvAddStock.Rows(row).Cells(dgvIndex.StockName).Value = dr("STOCK_NAME")
            dgvAddStock.Rows(row).Cells(dgvIndex.StockCode).Value = dr("STOCK_CODE")

            row = row + 1
        Next
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

        If _returnDs Is Nothing = True Then
            _returnDs = PaikRichStock.Common.clsFunc.FavAddDataGridViewToDataSet(dgvFavStockList).Copy
        Else
            _returnDs.Reset()
            _returnDs = PaikRichStock.Common.clsFunc.FavAddDataGridViewToDataSet(dgvFavStockList).Copy
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

#Region " 입력, 수정 "

#Region " StoredRecordPsi02 "
    Private Function StoredRecordPsi02(ByVal stockId As String, ByVal interId As Integer, ByVal StockCode As String, ByVal StockPint As String, ByVal remark As String) As Boolean
        Dim arrParam As New ArrayParam
        Dim intNewMaterId As Integer = 0
        Dim ix As Integer = 0

        Return _DataAcc.p_Psi02Add("A", stockId, interId, StockCode, StockPint, remark, "")

    End Function
#End Region

#Region " DelRecordPsi02 "
    Private Function DelRecordPsi02(ByVal stockId As String, ByVal interId As Integer, ByVal StockCode As String) As Boolean
        Dim arrParam As New ArrayParam
        Dim intNewMaterId As Integer = 0
        Dim ix As Integer = 0

        Return _DataAcc.p_psi02Del("", stockId, interId, StockCode)

    End Function
#End Region

#Region " StoredRecordPsi01 "
    Private Function StoredRecordPsi01(ByVal stockId As String, ByVal interId As Integer, ByVal interName As String) As Boolean
        Dim arrParam As New ArrayParam
        Dim intNewMaterId As Integer = 0
        Dim ix As Integer = 0

        Return _DataAcc.p_Psi01Add("", stockId, interId, interName)

    End Function
#End Region

#Region " DeleteRecordPsi01 "
    Private Function DeleteRecordPsi01(ByVal stockId As String, ByVal interId As Integer) As Boolean
        Dim arrParam As New ArrayParam
        Dim intNewMaterId As Integer = 0
        Dim ix As Integer = 0

        With arrParam
            .Clear()
            .Add("_STOCK_ID", stockId, Odbc.OdbcType.Char, ParameterDirection.Input)
            .Add("_INTER_ID", interId, Odbc.OdbcType.Int, ParameterDirection.Input)
        End With

        Try
            Common.mySqlDbConn.ExecuteNonQuery("p_psi01Del", arrParam)

            'GetDataFavGroup()

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
#End Region

#End Region

#Region " 종목명을 반환한다. "
    Private Function GetStockInfo(ByVal stockCode As String) As String
        Dim dv As DataView

        dv = New DataView(_AllStockDataset.Tables("StockList"))

        dv.RowFilter = String.Format("STOCK_CODE = '{0}'", stockCode)

        For Each drRowView As DataRowView In dv
            Return Trim(drRowView("STOCK_NAME").ToString())
        Next

        Return ""
    End Function
#End Region

#Region " Control Event "

    Private Sub btnAdditionAllAdd_Click(sender As Object, e As EventArgs) Handles btnAdditionAllAdd.Click
        If tbList.SelectedIndex <> 0 Then Exit Sub

        If lblInterId.Text = "" Or cboStockId.Text = "" Then
            MsgBox("관심그룹을 먼저 지정해야합니다.", MsgBoxStyle.Information)
            Exit Sub
        End If

        If MsgBox(" 해당 종목을 모두 추가하시겠습니까?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            With dgvAddStock
                For row As Integer = 0 To .Rows.Count - 1
                    If .Rows(row).Cells(dgvIndex.StockCode).Value = "" Then Exit For

                    If StoredRecordPsi02(Mid(cboStockId.Text, 1, 6), CInt(lblInterId.Text), Trim(dgvAddStock.Rows(row).Cells(dgvIndex.StockCode).Value), "00", "") = True Then
                        lblAddStatus.Text = Trim(dgvAddStock.Rows(row).Cells(dgvIndex.StockName).Value) & "이 입력되었습니다."
                    End If

                    System.Threading.Thread.Sleep(600)

                Next
            End With
        End If

        SettingFavStockListData(Mid(cboStockId.Text, 1, 6), CInt(lblInterId.Text))
        
    End Sub

    Private Sub btnInterNameModify_Click(sender As Object, e As EventArgs) Handles btnInterNameModify.Click
        If lblInterId.Text = "" Or cboStockId.Text = "" Or txtGroupName.Text = "" Then
            MsgBox("관심그룹을 먼저 지정해야합니다.", MsgBoxStyle.Information)
            Exit Sub
        End If

        If StoredRecordPsi01(Mid(cboStockId.Text, 1, 6), CInt(lblInterId.Text), Trim(txtGroupName.Text)) = True Then
            MsgBox("변경 되었습니다.", MsgBoxStyle.Information)
        Else
            MsgBox("그룹명 변경이 실패 되었습니다.", MsgBoxStyle.Information)
        End If

    End Sub

#Region " SetBaseFavControl "
    Private Sub SetBaseFavControl(ByVal interval As Integer)
        If interval = -1 Then
            If btnFav1.Text = "1" Then Exit Sub
        End If

        If interval = 1 Then
            If btnFav10.Text = "50" Then Exit Sub
        End If

        If interval = 0 Then
            For i As Integer = 1 To 10
                CType(PaikRichStock.Common.clsFunc.FindControl(Me, "btnFav" & i.ToString), Button).Text = i.ToString()
            Next
        Else
            For i As Integer = 1 To 10
                CType(PaikRichStock.Common.clsFunc.FindControl(Me, "btnFav" & i.ToString), Button).Text = _
                Val(CType(PaikRichStock.Common.clsFunc.FindControl(Me, "btnFav" & i.ToString), Button).Text) + interval
            Next
        End If

        SetBaseFavColorSetting()

    End Sub

    Private _PaintedBaseFavTitle As String = ""

    Private Sub SetBaseFavColorSetting()
        For i As Integer = 1 To 10
            If CType(PaikRichStock.Common.clsFunc.FindControl(Me, "btnFav" & i.ToString), Button).Text = _PaintedBaseFavTitle Then
                CType(PaikRichStock.Common.clsFunc.FindControl(Me, "btnFav" & i.ToString), Button).BackColor = Color.Yellow
            Else
                CType(PaikRichStock.Common.clsFunc.FindControl(Me, "btnFav" & i.ToString), Button).BackColor = Color.Transparent
            End If
        Next
    End Sub
#End Region

    Private Sub btnNextInter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextInter.Click
        SetBaseFavControl(1)
    End Sub

    Private Sub btnPrevInter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevInter.Click
        SetBaseFavControl(-1)
    End Sub

    Private Sub btnFavDeung_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnFav1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFav1.Click, btnFav2.Click, btnFav3.Click, btnFav4.Click, _
                                                                                                  btnFav5.Click, btnFav6.Click, btnFav7.Click, btnFav8.Click, _
                                                                                                  btnFav9.Click, btnFav10.Click

        SettingFavStockListData(Mid(cboStockId.Text, 1, 6), CInt(CType(sender, Button).Text))

        CType(sender, Button).BackColor = Color.Yellow

        _PaintedBaseFavTitle = Trim(CType(sender, Button).Text)

        lblInterId.Text = CType(sender, Button).Text

        For i As Integer = 1 To 10
            If CType(PaikRichStock.Common.clsFunc.FindControl(Me, "btnFav" & i.ToString), Button).Name = CType(sender, Button).Name Then
                Continue For
            Else
                CType(PaikRichStock.Common.clsFunc.FindControl(Me, "btnFav" & i.ToString), Button).BackColor = SystemColors.Control
            End If
        Next

    End Sub
#End Region

    Private Sub txtSearchStock_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearchStock.KeyUp
        If Trim(txtSearchStock.Text) = "" Then Exit Sub

        Dim str As String = txtSearchStock.Text
        Try

            Select Case tbList.SelectedIndex
                Case 0
                    For i As Integer = 0 To dgvAddStock.Rows.Count - 1

                        If dgvAddStock.Item(dgvIndex.StockName, i).Value.ToString().ToLower.StartsWith(str.ToLower) Then
                            dgvAddStock.Rows(i).Selected = True
                            dgvAddStock.CurrentCell = dgvAddStock.Rows(i).Cells(dgvIndex.StockName)
                            Exit Try
                        End If

                    Next i
                Case 1
                    For i As Integer = 0 To dgvKosdak.Rows.Count - 1
                        If Trim(dgvKosdak.Item(dgvIndex.StockName, i).Value.ToString()) = "" Then Exit For
                        If dgvKosdak.Item(dgvIndex.StockName, i).Value.ToString().ToLower.StartsWith(str.ToLower) Then
                            dgvKosdak.Rows(i).Selected = True
                            dgvKosdak.CurrentCell = dgvKosdak.Rows(i).Cells(dgvIndex.StockName)
                            Exit Try
                        End If

                    Next i
                Case 2
                    For i As Integer = 0 To dgvKospi.Rows.Count - 1
                        If Trim(dgvKospi.Item(dgvIndex.StockName, i).Value.ToString()) = "" Then Exit For
                        If dgvKospi.Item(dgvIndex.StockName, i).Value.ToString().ToLower.StartsWith(str.ToLower) Then
                            dgvKospi.Rows(i).Selected = True
                            dgvKospi.CurrentCell = dgvKospi.Rows(i).Cells(dgvIndex.StockName)
                            Exit Try
                        End If
                    Next i
                Case 3
                    For i As Integer = 0 To dgvAll.Rows.Count - 1
                        If Trim(dgvAll.Item(dgvIndex.StockName, i).Value.ToString()) = "" Then Exit For
                        If dgvAll.Item(dgvIndex.StockName, i).Value.ToString().ToLower.StartsWith(str.ToLower) Then
                            dgvAll.Rows(i).Selected = True
                            dgvAll.CurrentCell = dgvAll.Rows(i).Cells(dgvIndex.StockName)
                            Exit Try
                        End If
                    Next i
            End Select

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub dgvAddStock_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAddStock.CellDoubleClick, dgvKosdak.CellDoubleClick, dgvKospi.CellDoubleClick, dgvAll.CellDoubleClick
        If lblInterId.Text = "" Or cboStockId.Text = "" Then
            MsgBox("관심그룹을 먼저 지정해야합니다.", MsgBoxStyle.Information)
            Exit Sub
        End If


        With dgvFavStockList
            For row As Integer = 0 To dgvFavStockList.Rows.Count - 1
                If (dgvFavStockList.Rows(row).Cells(dgvFavStockListIndex.STOCK_CODE).Value) = Trim(CType(sender, DataGridView).Rows(e.RowIndex).Cells(dgvIndex.StockCode).Value) Then
                    MsgBox(" 해당 종목이 이미 관심종목에 있습니다. ")
                    Exit Sub
                End If
            Next

            If MsgBox(Trim(CType(sender, DataGridView).Rows(e.RowIndex).Cells(dgvIndex.StockName).Value) & "을 관심종목에 추가하시겠습니까?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                If StoredRecordPsi02(Mid(cboStockId.Text, 1, 6), CInt(lblInterId.Text), Trim(CType(sender, DataGridView).Rows(e.RowIndex).Cells(dgvIndex.StockCode).Value), "00", "") = True Then
                    lblAddStatus.Text = Trim(CType(sender, DataGridView).Rows(e.RowIndex).Cells(dgvIndex.StockName).Value) & "이 입력되었습니다."
                    SettingFavStockListData(Mid(cboStockId.Text, 1, 6), CInt(lblInterId.Text))
                End If
            End If
        End With
    End Sub

    Private Sub dgvFavStockList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFavStockList.CellDoubleClick
        If (dgvFavStockList.Rows(e.RowIndex).Cells(dgvFavStockListIndex.STOCK_CODE).Value) = "" Then Exit Sub

        If lblInterId.Text = "" Or cboStockId.Text = "" Then
            MsgBox("관심그룹을 먼저 지정해야합니다.", MsgBoxStyle.Information)
            Exit Sub
        End If

        If MsgBox(Trim(dgvFavStockList.Rows(e.RowIndex).Cells(dgvFavStockListIndex.STOCK_NAME).Value) & "을 관심종목에서 제외하시겠습니까?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If DelRecordPsi02(Mid(cboStockId.Text, 1, 6), CInt(lblInterId.Text), Trim(dgvFavStockList.Rows(e.RowIndex).Cells(dgvFavStockListIndex.STOCK_CODE).Value)) = True Then
                lblAddStatus.Text = Trim(dgvFavStockList.Rows(e.RowIndex).Cells(dgvFavStockListIndex.STOCK_NAME).Value) & "이 관심종목에서 제외되었습니다."
                SettingFavStockListData(Mid(cboStockId.Text, 1, 6), CInt(lblInterId.Text))
            End If
        End If
    End Sub
End Class
