Imports PaikRichStock.Common
Imports System.IO

Public Class frmStockVolumeScriptToXml

    Private _MainStock As New PaikRichStock.Common.ucMainStockVer2
    Private _clsCallOpt As New clsCallOpt

    ' Private _dtOpt10014 As New DataTable ' 공매도추이요청
    Private _dtOpt10059 As New DataTable ' 종목별투자자기관별요청
    Private _dtOpt10059Price As New DataTable ' 종목별투자자기관별요청(금액)
    Private _dtOpt10081 As New DataTable ' 주식일봉차트조회
    Private _dtOpt10061 As New DataTable ' 종목별투자자기관별합계 요청
    ' Private _dtOpt20068 As New DataTable ' 대차거래추이요청

    Private _dtCombineOptS As New DataTable

    Private _stockCode As String = ""
    Private _firstCall As Boolean = False
    Private _lastDate As String = ""

    Sub New(ByVal mainStock As ucMainStockVer2)

        ' 이 호출은 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        _MainStock = mainStock
        UcFavList1.MainStock2 = mainStock
        _clsCallOpt.MainStock = mainStock
    End Sub

    Private Enum dgvIndex
        StockName
        StockCode
        JobGb
    End Enum

#Region " SetInitControl "
    Private Sub SetInitControl(ByVal ds As DataSet)
        With dgvList
            .Rows.Clear()
            .RowCount = ds.Tables(0).Rows.Count

            Dim row As Integer = 0

            For Each dr In ds.Tables(0).Rows
                If Trim(dr("STOCK_CODE").ToString) = "" Then Continue For

                .Rows.Item(row).Cells(dgvIndex.StockName).Value = Trim(dr("STOCK_NAME"))
                .Rows.Item(row).Cells(dgvIndex.StockCode).Value = Trim(dr("STOCK_CODE"))
                .Rows.Item(row).Cells(dgvIndex.JobGb).Value = ""

                row = row + 1

            Next
        End With

        proBar.Minimum = 0
        proBar.Maximum = ds.Tables(0).Rows.Count

    End Sub
#End Region

#Region " InitDataTable "
    Private Sub InitDataTable()
        '_dtOpt10014.Clear()
        _dtOpt10059.Reset()
        _dtOpt10059Price.Reset()
        _dtOpt10081.Reset()
        _dtOpt10061.Reset()
        '_dtOpt20068.Clear()
    End Sub
#End Region

#Region " CallOpt_Event"
    'Private Sub OnEventReturn10014ResultDt(ByVal dt As DataTable)
    '    _dtOpt10014 = dt
    'End Sub
    Private Sub OnEventReturn10059ResultDt(ByVal dt As DataTable, ByVal calOpt10059 As clsCallOpt.CalOpt10059)
        _dtOpt10059 = dt
        lblOpt10059.Text = "Y"
    End Sub
    Private Sub OnEventReturn10081ResultDt(ByVal dt As DataTable)
        _dtOpt10081 = dt
        lblOpt10081.Text = "Y"
    End Sub
    Private Sub OnEventReturn10059PriceResultDt(ByVal dt As DataTable, ByVal calOpt10059 As clsCallOpt.CalOpt10059Price)
        _dtOpt10059Price = dt
        lblOpt10059Price.Text = "Y"
    End Sub
    Private Sub OnEventReturn10061ResultDt(ByVal dt As DataTable)
        _dtOpt10061 = dt
        lblOpt10061.Text = "Y"
        _dtOpt10061.TableName = "테스트" & "_" & Trim(_MainStock.GetStockInfo(_stockCode))
        _dtOpt10061.WriteXml("C:\Xml\" & "테스트" & ".xml")
    End Sub
    'Private Sub OnEventReturn20068ResultDt(ByVal dt As DataTable)
    '    _dtOpt20068 = dt
    'End Sub
#End Region

    Private Sub RegisterCallOptEvent()

        If _firstCall = False Then
            'AddHandler _clsCallOpt.OnEventReturn10014ResultDt, AddressOf OnEventReturn10014ResultDt
            AddHandler _clsCallOpt.OnEventReturn10059ResultDt, AddressOf OnEventReturn10059ResultDt
            AddHandler _clsCallOpt.OnEventReturn10059PriceResultDt, AddressOf OnEventReturn10059PriceResultDt
            AddHandler _clsCallOpt.OnEventReturn10081ResultDt, AddressOf OnEventReturn10081ResultDt
            AddHandler _clsCallOpt.OnEventReturn10061ResultDt, AddressOf OnEventReturn10061ResultDt

            _firstCall = True

            'AddHandler _clsCallOpt.OnEventReturn20068ResultDt, AddressOf OnEventReturn20068ResultDt
        End If
        
    End Sub

#Region " GetVolumeData "
    Private Sub GetVolumeData()
        Dim strStockCode As String = ""

        With dgvList
            For i As Integer = 0 To dgvList.Rows.Count - 1
                If Trim(.Rows(i).Cells(dgvIndex.StockCode).Value) = "" Then Exit For
                If File.Exists("C:\Xml\" & Trim(.Rows(i).Cells(dgvIndex.StockCode).Value) & "_" & Trim(.Rows(i).Cells(dgvIndex.StockName).Value) & ".xml") = True Then
                    .Rows(i).Cells(dgvIndex.JobGb).Value = "Y"
                    Continue For
                End If

                If strStockCode = "" Then
                    strStockCode = Trim(.Rows(i).Cells(dgvIndex.StockCode).Value) & ";"
                Else
                    strStockCode = strStockCode & Trim(.Rows(i).Cells(dgvIndex.StockCode).Value) & ";"
                End If

            Next
        End With
        If strStockCode = "" Then
            MsgBox("작업 할 내역이 없습니다.")
            Exit Sub
        End If
        CallOpt(clsFunc.FirstCallSequnceOpt(strStockCode, ";"))

    End Sub

    Private Sub CallOpt(ByVal stockCode As String)
        If stockCode = "STOP" Then
            MsgBox("작업이 완료 되었습니다.", MsgBoxStyle.Information)
            Exit Sub
        End If
        _stockCode = stockCode
        '_clsCallOpt.GetOpt10014(stockCode)
        'System.Threading.Thread.Sleep(500)
        _clsCallOpt.GetOpt10059(stockCode)
        System.Threading.Thread.Sleep(500)
        _clsCallOpt.GetOpt10081(stockCode)
        System.Threading.Thread.Sleep(500)
        '_clsCallOpt.GetOpt20068(stockCode)
        _clsCallOpt.GetOpt10059Price(stockCode)
        System.Threading.Thread.Sleep(500)
    End Sub
#End Region

#Region " ComBineOpt10059WithOpt10081 "
    Private Sub ComBineOpt10059WithOpt10081()

        For column As Integer = 0 To _dtOpt10081.Columns.Count - 1
            If _dtOpt10081.Columns(column).ColumnName = "현재가" Then
                _dtOpt10081.Columns(column).ColumnName = "종가"
            End If

            If _dtOpt10081.Columns(column).ColumnName = "일자" Then
                _dtOpt10081.Columns(column).ColumnName = "일자2"
            End If
        Next

        With _dtOpt10059Price
            .Columns(0).ColumnName = "일자3"
            .Columns.Remove("현재가")
            .Columns.Remove("대비기호")
            .Columns.Remove("전일대비")
            .Columns.Remove("등락율")
            .Columns.Remove("누적거래대금")
            .Columns.Remove("개인투자자")
            .Columns.Remove("외국인투자자")
            .Columns.Remove("기관계")
            .Columns.Remove("금융투자")
            .Columns.Remove("기타금융")
            .Columns.Remove("보험")
            .Columns.Remove("투신")
            .Columns.Remove("은행")
            .Columns.Remove("연기금등")
            .Columns.Remove("사모펀드")
            .Columns.Remove("국가")
            .Columns.Remove("기타법인")
            .Columns.Remove("내외국인")
        End With

        If clsFunc.DataTableColumnCloneToDataSet(_dtOpt10059, _dtOpt10059Price) = False Then
            MsgBox("_dtOpt10059에 컬럼 추가가 실패했습니다.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If clsFunc.DataTableColumnCloneToDataSet(_dtOpt10059, _dtOpt10081) = False Then
            MsgBox("_dtOpt10059에 컬럼 추가가 실패했습니다.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim dr2th As DataRow
        Dim dr As DataRow

        With _dtOpt10059
            For Each dr In _dtOpt10059.Rows

                For Each dr3th In _dtOpt10059Price.Rows
                    If Trim(dr("일자").ToString()) = Trim(dr3th("일자3").ToString()) Then
                        For i As Integer = 0 To _dtOpt10059Price.Columns.Count - 1
                            dr(_dtOpt10059Price.Columns(i).ColumnName) = dr3th(_dtOpt10059Price.Columns(i).ColumnName)
                        Next

                        Exit For

                    End If
                Next

                For Each dr2th In _dtOpt10081.Rows
                    If Trim(dr("일자").ToString()) = Trim(dr2th("일자2").ToString()) Then
                        For i As Integer = 0 To _dtOpt10081.Columns.Count - 1
                            dr(_dtOpt10081.Columns(i).ColumnName) = dr2th(_dtOpt10081.Columns(i).ColumnName)
                        Next

                        Exit For

                    End If
                Next


            Next
        End With

    End Sub
#End Region

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        RegisterCallOptEvent()
        GetVolumeData()
    End Sub

    Private Sub lblOpt10059_TextChanged(sender As Object, e As EventArgs) Handles lblOpt10059.TextChanged, lblOpt10081.TextChanged, lblOpt10059Price.TextChanged
        If lblOpt10059.Text = "Y" And lblOpt10081.Text = "Y" And lblOpt10059Price.Text = "Y" Then
            ComBineOpt10059WithOpt10081()
            _dtOpt10059.TableName = _stockCode & "_" & Trim(_MainStock.GetStockInfo(_stockCode))
            _dtOpt10059.WriteXml("C:\Xml\" & _dtOpt10059.TableName & ".xml")
            InitDataTable()
            With dgvList
                For i As Integer = 0 To .Rows.Count - 1
                    If Trim(.Rows(i).Cells(dgvIndex.StockCode).Value) = _stockCode Then
                        .Rows(i).Cells(dgvIndex.JobGb).Value = "Y"
                        Exit For
                    End If
                Next

                proBar.Value = proBar.Value + 1

                lblOpt10059.Text = ""
                lblOpt10081.Text = ""
                lblOpt10059Price.Text = ""

                CallOpt(clsFunc.NextCallSequnceOpt())

            End With
        End If
    End Sub

    Private Sub btnAll_Click(sender As Object, e As EventArgs) Handles btnAll.Click
        SetInitControl(_MainStock._allStockDataset)
    End Sub

    Private Sub btnFav_Click(sender As Object, e As EventArgs) Handles btnFav.Click
        SetInitControl(UcFavList1.ReturnFavDs)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        AddHandler _clsCallOpt.OnEventReturn10061ResultDt, AddressOf OnEventReturn10061ResultDt
        _clsCallOpt.GetOpt10061("088910", "2006")
    End Sub
End Class