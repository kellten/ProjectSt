Imports AnalysisSt.DataBaseFunc
Imports AnalysisSt.KiwoomVB
Imports System.Windows.Forms
Imports AnalysisSt.Common.Class

Public Class frmBatchTradeInfo

    Sub New()

        ' 이 호출은 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        AddHandler ModStatus._ModMainStock.OnReceiveTrData_Opt10059, AddressOf OnReceiveTrData_opt10059
        AddHandler ModStatus._ModMainStock.OnReceiveTrData_Opt10059Price, AddressOf OnReceiveTrData_opt10059Price
        AddHandler ModStatus._ModMainStock.OnReceiveTrData_opt10081New, AddressOf OnReceiveTrData_opt10081

        GetStockDataSet()
        GetFCodeData()

    End Sub

    Private Sub frmBatchTradeInfo_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

#Region " StockDataSet "
    Private _ds As DataSet

    Private Sub GetStockDataSet()
        Dim oQuery As New AnalysisSt.DataBaseFunc.RichQuery

        _ds = oQuery.p_ScodeQuery("1", "", "", False)

    End Sub

    Private Sub GetFCodeData()
        Dim oQuery As New AnalysisSt.DataBaseFunc.RichQuery
        Dim ds As DataSet

        ds = oQuery.p_FCodeQuery("2", "", "", "", False)

        dgvFCode.ColumnCount = 3
        dgvFCode.Columns(0).Name = "SGROUP_CODE"
        dgvFCode.Columns(1).Name = "SGROUP_NAME"
        dgvFCode.Columns(2).Name = "SGROUP_INFO"

        Dim i As Integer = 0

        dgvFCode.Rows.Clear()

        For Each dr In ds.Tables(0).Rows

            dgvFCode.Rows.Add()
            dgvFCode.Rows(i).Cells("SGROUP_CODE").Value = dr("SGROUP_CODE").ToString()
            dgvFCode.Rows(i).Cells("SGROUP_NAME").Value = dr("SGROUP_NAME").ToString()
            dgvFCode.Rows(i).Cells("SGROUP_INFO").Value = dr("SGROUP_INFO").ToString()

            i = i + 1

        Next

        ds.Reset()

    End Sub

    Dim _i As Integer = 0

    Private Function RetStockCode10059Qty() As String
        If _i > _ds.Tables(0).Rows.Count - 1 Then
            GetOpt10059_Price(RetStockCode10059Price)
            Return ""
        End If

        Dim stockCode As String = ""
        Dim blnChk As Boolean = False

        If chkDay.Checked = False Then
            For i As Integer = _i To _ds.Tables(0).Rows.Count - 1
                If Trim(_ds.Tables(0).Rows(i).Item("OPT10059_QTY").ToString()) <> "Y" Then
                    _i = i
                    blnChk = True
                    Exit For
                End If

                proBarOpt10059Qty.Value = proBarOpt10059Qty.Value + 1

            Next

            If blnChk = False Then
                GetOpt10059_Price(RetStockCode10059Price)
                Return ""
            End If
        End If

        stockCode = Trim(_ds.Tables(0).Rows(_i).Item("STOCK_CODE").ToString())

        lblMsg.Text = stockCode & "작업중(10059QTY)" & "(" & _i.ToString() & ")"

        _i = _i + 1

        Return stockCode

    End Function

    Dim _j As Integer = 0

    Private Function RetStockCode10059Price() As String
        If _j > _ds.Tables(0).Rows.Count - 1 Then
            GetOpt10081(RetStockCode10081)
            Return ""
        End If
        Dim stockCode As String = ""
        Dim blnChk As Boolean = False

        If chkDay.Checked = False Then
            For i As Integer = _j To _ds.Tables(0).Rows.Count - 1
                If Trim(_ds.Tables(0).Rows(i).Item("OPT10059_PRICE").ToString()) <> "Y" Then
                    _j = i
                    blnChk = True
                    Exit For
                End If

                proBarOpt10059Price.Value = proBarOpt10059Price.Value + 1

            Next

            If blnChk = False Then
                GetOpt10081(RetStockCode10081)
                Return ""
            End If
        End If

        stockCode = Trim(_ds.Tables(0).Rows(_j).Item("STOCK_CODE").ToString())

        lblMsg.Text = stockCode & "작업중(10059Price)" & "(" & _j.ToString() & ")"

        _j = _j + 1

        Return stockCode

    End Function

    Dim _k As Integer = 0

    Private Function RetStockCode10081() As String
        If _k > _ds.Tables(0).Rows.Count - 1 Then
            MsgBox("작업이 완료되었습니다.", MsgBoxStyle.Information)
            Return "END"
        End If
        Dim stockCode As String = ""
        Dim blnChk As Boolean = False

        If chkDay.Checked = False Then
            For i As Integer = _k To _ds.Tables(0).Rows.Count - 1
                If Trim(_ds.Tables(0).Rows(i).Item("OPT10081").ToString()) <> "Y" Then
                    _k = i
                    blnChk = True
                    Exit For
                End If

                proBarOpt10081.Value = proBarOpt10081.Value + 1

            Next

            If blnChk = False Then
                MsgBox("작업이 완료되었습니다.", MsgBoxStyle.Information)
                Return "END"
            End If
        End If

        stockCode = Trim(_ds.Tables(0).Rows(_k).Item("STOCK_CODE").ToString())
        lblMsg.Text = stockCode & "작업중(10081)" & "(" & _k.ToString() & ")"
        _k = _k + 1

        Return stockCode

    End Function

    Dim _T_60_QTY As Integer = 0

    Private Function RetStockCode10060QTY() As String
        If _T_60_QTY > _ds.Tables(0).Rows.Count - 1 Then
            MsgBox("작업이 완료되었습니다.", MsgBoxStyle.Information)
            Return "END"
        End If
        Dim stockCode As String = ""
        Dim blnChk As Boolean = False

        If chkDay.Checked = False Then
            For i As Integer = _T_60_QTY To _ds.Tables(0).Rows.Count - 1
                If Trim(_ds.Tables(0).Rows(i).Item("OPT10060_QTY").ToString()) <> "Y" Then
                    _T_60_QTY = i
                    blnChk = True
                    Exit For
                End If

                proBarOpt10060Qty.Value = proBarOpt10060Qty.Value + 1

            Next

            If blnChk = False Then
                MsgBox("작업이 완료되었습니다.", MsgBoxStyle.Information)
                Return "END"
            End If
        End If

        stockCode = Trim(_ds.Tables(0).Rows(_T_60_QTY).Item("STOCK_CODE").ToString())
        lblMsg.Text = stockCode & "작업중(10060_QTY)" & "(" & _T_60_QTY.ToString() & ")"
        _T_60_QTY = _T_60_QTY + 1

        Return stockCode

    End Function

    Dim _T_60_PRICE As Integer = 0

    Private Function RetStockCode10060PRICE() As String
        If _T_60_PRICE > _ds.Tables(0).Rows.Count - 1 Then
            MsgBox("작업이 완료되었습니다.", MsgBoxStyle.Information)
            Return "END"
        End If
        Dim stockCode As String = ""
        Dim blnChk As Boolean = False

        If chkDay.Checked = False Then
            For i As Integer = _T_60_PRICE To _ds.Tables(0).Rows.Count - 1
                If Trim(_ds.Tables(0).Rows(i).Item("OPT10060_PRICE").ToString()) <> "Y" Then
                    _T_60_PRICE = i
                    blnChk = True
                    Exit For
                End If

                proBarOpt10060Price.Value = proBarOpt10060Price.Value + 1

            Next

            If blnChk = False Then
                MsgBox("작업이 완료되었습니다.", MsgBoxStyle.Information)
                Return "END"
            End If
        End If

        stockCode = Trim(_ds.Tables(0).Rows(_T_60_PRICE).Item("STOCK_CODE").ToString())
        lblMsg.Text = stockCode & "작업중(10060_PRICE)" & "(" & _T_60_PRICE.ToString() & ")"
        _T_60_PRICE = _T_60_PRICE + 1

        Return stockCode

    End Function
#End Region

#Region " OPT10059_QTY "
    Private _stockCode_Opt10059 As String = ""
    Private _lastDate As String = ""
    Private _Max_10059qty As String = ""
    Private _Min_10059qty As String = ""

    Private Sub CallOpt10059(ByVal startDate As String)
        ModStatus._ModMainStock.Opt10059_OnReceiveTrData(startDate, Trim(_stockCode_Opt10059), ModStatus._ModMainStock.GetStockInfo(_stockCode_Opt10059), "2", "0", "1")
    End Sub

    Public Sub GetOpt10059(ByVal stockCode As String)
        If stockCode = "" Then Exit Sub
        proBarOpt10059Qty.Value = proBarOpt10059Qty.Value + 1
        _stockCode_Opt10059 = stockCode
        _lastDate = ""
        _Max_10059qty = ""
        _Min_10059qty = ""

        Dim ds As DataSet
        Dim oQuery As New AnalysisSt.DataBaseFunc.KiwoomQuery

        ds = oQuery.p_Opt10059QtyMinMaxQuery("1", stockCode, "", False)

        If ds.Tables(0).Rows.Count < 1 Or ds Is Nothing Then
            ds.Reset()
            _Max_10059qty = ""
        Else
            _Max_10059qty = ds.Tables(0).Rows(0).Item("MAX_STOCK_DATE")
            ds.Reset()
        End If

        ds = oQuery.p_Opt10059QtyMinMaxQuery("2", stockCode, "", False)

        If ds.Tables(0).Rows.Count < 1 Or ds Is Nothing Then
            ds.Reset()
            _Min_10059qty = ""
        Else
            _Min_10059qty = ds.Tables(0).Rows(0).Item("MIN_STOCK_DATE")
            ds.Reset()
        End If

        If chkDay.Checked = True Then
            If DateTime.Now.ToString("HH") + DateTime.Now.ToString("ss") > "1600" Then
                If CDateTime.FormatDate(Now.Date) = _Max_10059qty Then
                    GetOpt10059(RetStockCode10059Qty())
                    Exit Sub
                End If
            Else
                If CDateTime.FormatDate(DateAdd(DateInterval.Day, -1, Now.Date)) = _Max_10059qty Then
                    GetOpt10059(RetStockCode10059Qty())
                    Exit Sub
                End If
            End If
            
        End If

        Dim sysDate As String = ""
        If DateTime.Now.ToString("HH") + DateTime.Now.ToString("ss") > "1600" Then
            sysDate = CDateTime.FormatDate(Now.Date)
        Else
            sysDate = CDateTime.FormatDate(DateAdd(DateInterval.Day, -1, Now.Date))
        End If

        CallOpt10059(sysDate)

    End Sub

    Public Sub OnReceiveTrData_opt10059(ByVal ds As DataSet)
        Try
            Application.DoEvents()
            If ds Is Nothing Then
                StoredSCodeStatus(_stockCode_Opt10059, "CQ")
                System.Threading.Thread.Sleep(4000)
                GetOpt10059(RetStockCode10059Qty())
                Exit Sub
            End If

            If ds.Tables(0).Rows.Count < 1 Then
                StoredSCodeStatus(_stockCode_Opt10059, "CQ")
                System.Threading.Thread.Sleep(4000)
                GetOpt10059(RetStockCode10059Qty())
                Exit Sub
            End If

            Dim arrParam As New ArrayParam
            Dim oSql As New Sql("EDPB2F011\VADIS", "KIWOOMDB")

            For Each dr In ds.Tables(0).Rows
                With arrParam
                    .Clear()
                    .Add("@ACTION_GB", "A")
                    .Add("@STOCK_CODE", _stockCode_Opt10059)
                    .Add("@STOCK_DATE", dr("일자"))
                    .Add("@DATE_SEQNO", 0)
                    .Add("@NUJUK_TRDAEGUM", dr("누적거래대금"))
                    .Add("@GAIN_QTY", dr("개인투자자"))
                    .Add("@FORE_QTY", dr("외국인투자자"))
                    .Add("@GIGAN_QTY", dr("기관계"))
                    .Add("@GUMY_QTY", dr("금융투자"))
                    .Add("@BOHUM_QTY", dr("보험"))
                    .Add("@TOSIN_QTY", dr("투신"))
                    .Add("@GITA_QTY", dr("기타금융"))
                    .Add("@BANK_QTY", dr("은행"))
                    .Add("@YEONGI_QTY", dr("연기금등"))
                    .Add("@SAMO_QTY", dr("사모펀드"))
                    .Add("@NATION_QTY", dr("국가"))
                    .Add("@BUBIN_QTY", dr("기타법인"))
                    .Add("@IOFORE_QTY", dr("내외국인"))
                    .Add("@GIGAN_SUM_QTY", dr("금융투자") + dr("보험") + dr("투신") + _
                                            dr("기타금융") + dr("은행") + dr("연기금등") + dr("사모펀드") + dr("국가"))
                    .Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput)
                End With

                oSql.ExecuteNonQuery("p_Opt10059_QtyAdd", CommandType.StoredProcedure, arrParam)

            Next

            Dim nextDate As String = ""

            If _lastDate <> ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item("일자") Then
                _lastDate = ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item("일자")
                nextDate = CDateTime.FormatDate(DateAdd(DateInterval.Day, -1, CDate(CDateTime.FormatDate(ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item("일자"), "."))))
            Else
                nextDate = CDateTime.FormatDate(DateAdd(DateInterval.Day, -2, CDate(CDateTime.FormatDate(ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item("일자"), "."))))
            End If

            If Trim(_Min_10059qty) <> "" Then
                ' 10059_qty 최소 일자가 크다면 최소값을 던진다.
                If _lastDate > _Min_10059qty Then
                    nextDate = _Min_10059qty
                End If
            End If

            System.Threading.Thread.Sleep(4000)

            If chkDay.Checked = True Then
                If nextDate < "20170101" Then
                    GetOpt10059(RetStockCode10059Qty())
                    Exit Sub
                End If
            End If

            CallOpt10059(nextDate)

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub
#End Region

#Region " OPT10059_PRICE "
    Private _stockCode_Opt10059_Price As String = ""
    Private _last_Price As String = ""
    Private _Max_10059Price As String = ""
    Private _Min_10059Price As String = ""

    Private Sub CallOpt10059_Price(ByVal startDate As String)
        ModStatus._ModMainStock.Opt10059_OnReceiveTrDataPrice(startDate, Trim(_stockCode_Opt10059_Price), ModStatus._ModMainStock.GetStockInfo(_stockCode_Opt10059_Price), "1", "0", "1")
    End Sub

    Public Sub GetOpt10059_Price(ByVal stockCode As String)
        If stockCode = "" Then Exit Sub
        proBarOpt10059Price.Value = proBarOpt10059Price.Value + 1
        _stockCode_Opt10059_Price = stockCode
        _last_Price = ""

        _Max_10059Price = ""
        _Min_10059Price = ""

        Dim ds As DataSet
        Dim oQuery As New AnalysisSt.DataBaseFunc.KiwoomQuery

        ds = oQuery.p_Opt10059PriceMinMaxQuery("1", stockCode, "", False)

        If ds.Tables(0).Rows.Count < 1 Or ds Is Nothing Then
            ds.Reset()
            _Max_10059Price = ""
        Else
            _Max_10059Price = ds.Tables(0).Rows(0).Item("MAX_STOCK_DATE")
            ds.Reset()
        End If

        ds = oQuery.p_Opt10059PriceMinMaxQuery("2", stockCode, "", False)

        If ds.Tables(0).Rows.Count < 1 Or ds Is Nothing Then
            ds.Reset()
            _Min_10059Price = ""
        Else
            _Min_10059Price = ds.Tables(0).Rows(0).Item("MIN_STOCK_DATE")
            ds.Reset()
        End If

        If chkDay.Checked = True Then
            If DateTime.Now.ToString("HH") + DateTime.Now.ToString("ss") > "1600" Then
                If CDateTime.FormatDate(Now.Date) = _Max_10059Price Then
                    GetOpt10059_Price(RetStockCode10059Price())
                    Exit Sub
                End If
            Else
                If CDateTime.FormatDate(DateAdd(DateInterval.Day, -1, Now.Date)) = _Max_10059Price Then
                    GetOpt10059_Price(RetStockCode10059Price())
                    Exit Sub
                End If
            End If
        End If

        Dim sysDate As String = ""
        If DateTime.Now.ToString("HH") + DateTime.Now.ToString("ss") > "1600" Then
            sysDate = CDateTime.FormatDate(Now.Date)
        Else
            sysDate = CDateTime.FormatDate(DateAdd(DateInterval.Day, -1, Now.Date))
        End If

        CallOpt10059_Price(sysDate)

    End Sub

    Public Sub OnReceiveTrData_opt10059Price(ByVal ds As DataSet)
        Try
            Application.DoEvents()
            If ds Is Nothing Then
                StoredSCodeStatus(_stockCode_Opt10059_Price, "CP")
                System.Threading.Thread.Sleep(4000)
                GetOpt10059_Price(RetStockCode10059Price())
                Exit Sub
            End If

            If ds.Tables(0).Rows.Count < 1 Then
                StoredSCodeStatus(_stockCode_Opt10059_Price, "CP")
                System.Threading.Thread.Sleep(4000)
                GetOpt10059_Price(RetStockCode10059Price())
                Exit Sub
            End If

            Dim arrParam As New ArrayParam
            Dim oSql As New Sql("EDPB2F011\VADIS", "KIWOOMDB")

            For Each dr In ds.Tables(0).Rows
                If dr("기관계") = 2147483648 Or dr("기관계") = -2147483648 Or _
                   dr("개인투자자") = 2147483648 Or dr("기관계") = -2147483648 Or _
                   dr("외국인투자자") = 2147483648 Or dr("외국인투자자") = -2147483648 Or _
                   dr("기타법인") = 2147483648 Or dr("기타법인") = -2147483648 Then

                    With arrParam
                        .Clear()
                        .Add("@ACTION_GB", "A")
                        .Add("@STOCK_CODE", _stockCode_Opt10059_Price)
                        .Add("@STOCK_DATE", dr("일자"))
                        .Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput)
                    End With

                    oSql.ExecuteNonQuery("p_OPT10059_PRICE_ERRORAdd", CommandType.StoredProcedure, arrParam)

                    Continue For

                End If
                With arrParam
                    .Clear()
                    .Add("@ACTION_GB", "A")
                    .Add("@STOCK_CODE", _stockCode_Opt10059_Price)
                    .Add("@STOCK_DATE", dr("일자"))
                    .Add("@DATE_SEQNO", 0)
                    .Add("@NUJUK_TRDAEGUM", dr("누적거래대금"))
                    .Add("@GAIN_PRICE", dr("개인투자자"))
                    .Add("@FORE_PRICE", dr("외국인투자자"))
                    .Add("@GIGAN_PRICE", dr("기관계"))
                    .Add("@GUMY_PRICE", dr("금융투자"))
                    .Add("@BOHUM_PRICE", dr("보험"))
                    .Add("@TOSIN_PRICE", dr("투신"))
                    .Add("@GITA_PRICE", dr("기타금융"))
                    .Add("@BANK_PRICE", dr("은행"))
                    .Add("@YEONGI_PRICE", dr("연기금등"))
                    .Add("@SAMO_PRICE", dr("사모펀드"))
                    .Add("@NATION_PRICE", dr("국가"))
                    .Add("@BUBIN_PRICE", dr("기타법인"))
                    .Add("@IOFORE_PRICE", dr("내외국인"))
                    .Add("@GIGAN_SUM_PRICE", dr("금융투자") + dr("보험") + dr("투신") + _
                                            dr("기타금융") + dr("은행") + dr("연기금등") + dr("사모펀드") + dr("국가"))
                    .Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput)
                End With

                oSql.ExecuteNonQuery("p_Opt10059_PriceAdd", CommandType.StoredProcedure, arrParam)

            Next

            Dim nextDate As String = ""

            If _last_Price <> ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item("일자") Then
                _last_Price = ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item("일자")
                nextDate = CDateTime.FormatDate(DateAdd(DateInterval.Day, -1, CDate(CDateTime.FormatDate(ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item("일자"), "."))))
            Else
                nextDate = CDateTime.FormatDate(DateAdd(DateInterval.Day, -2, CDate(CDateTime.FormatDate(ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item("일자"), "."))))
            End If

            If Trim(_Min_10059Price) <> "" Then
                ' 10059_Price 최소 일자가 크다면 최소값을 던진다.
                If _last_Price > _Min_10059Price Then
                    nextDate = _Min_10059Price
                End If
            End If

            System.Threading.Thread.Sleep(4000)

            If chkDay.Checked = True Then
                If nextDate < "20170101" Then
                    GetOpt10059_Price(RetStockCode10059Price())
                    Exit Sub
                End If
            End If

            CallOpt10059_Price(nextDate)

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub
#End Region

#Region " CallOpt10081(주식일봉차트조회요청) "
    Private _stockCode_Opt10081 As String = ""
    Private _last_10081 As String = ""
    Private _Max_10081 As String = ""
    Private _Min_10081 As String = ""

    Private Sub CallOpt10081(ByVal stockCode As String, ByVal stdDate As String)
        ModStatus._ModMainStock.Opt10081New_OnReceiveTrData(stockCode, ModStatus._ModMainStock.GetStockInfo(stockCode), stdDate)
    End Sub

    Public Sub GetOpt10081(ByVal stockCode As String)
        If stockCode = "" Then Exit Sub
        If stockCode = "END" Then Exit Sub
        proBarOpt10081.Value = proBarOpt10081.Value + 1
        Dim stdDate As String = ""
        _last_10081 = ""

        If DateTime.Now.ToString("HH") + DateTime.Now.ToString("ss") > "1600" Then
            stdDate = CDateTime.FormatDate(Now.Date)
        Else
            stdDate = CDateTime.FormatDate(DateAdd(DateInterval.Day, -1, Now.Date))
        End If

        _Max_10081 = ""
        _Min_10081 = ""

        Dim ds As DataSet
        Dim oQuery As New AnalysisSt.DataBaseFunc.KiwoomQuery

        ds = oQuery.p_Opt10081MinMaxQuery("1", stockCode, "", False)

        If ds.Tables(0).Rows.Count < 1 Or ds Is Nothing Then
            ds.Reset()
            _Max_10081 = ""
        End If

        _Max_10081 = ds.Tables(0).Rows(0).Item("MAX_STOCK_DATE")

        ds.Reset()

        ds = oQuery.p_Opt10081MinMaxQuery("2", stockCode, "", False)

        If ds.Tables(0).Rows.Count < 1 Or ds Is Nothing Then
            ds.Reset()
            _Min_10081 = ""
        End If

        _Min_10081 = ds.Tables(0).Rows(0).Item("MIN_STOCK_DATE")

        ds.Reset()

        If chkDay.Checked = True Then
            If DateTime.Now.ToString("HH") + DateTime.Now.ToString("ss") > "1600" Then
                If CDateTime.FormatDate(Now.Date) = _Max_10081 Then
                    GetOpt10081(RetStockCode10081())
                    Exit Sub
                End If
            Else
                If CDateTime.FormatDate(DateAdd(DateInterval.Day, -1, Now.Date)) = _Max_10081 Then
                    GetOpt10081(RetStockCode10081())
                    Exit Sub
                End If
            End If
        End If

        _stockCode_Opt10081 = stockCode

        CallOpt10081(_stockCode_Opt10081, stdDate)

    End Sub

    Private Sub OnReceiveTrData_opt10081(ByVal ds As DataSet)
        Try
            Application.DoEvents()
            If ds Is Nothing Then
                StoredSCodeStatus(_stockCode_Opt10081, "C8")
                GetOpt10081(RetStockCode10081())
                Exit Sub
            End If

            If ds.Tables(0).Rows.Count < 1 Then
                StoredSCodeStatus(_stockCode_Opt10081, "C8")
                System.Threading.Thread.Sleep(4000)
                GetOpt10081(RetStockCode10081())
                Exit Sub
            End If

            Dim arrParam As New ArrayParam
            Dim oSql As New Sql("EDPB2F011\VADIS", "KIWOOMDB")

            For Each dr In ds.Tables(0).Rows
                With arrParam
                    .Clear()
                    .Add("@ACTION_GB", "A")
                    .Add("@STOCK_CODE", _stockCode_Opt10081)
                    .Add("@STOCK_DATE", dr("일자"))
                    .Add("@DATE_SEQNO", 0)
                    .Add("@NOW_PRICE", dr("현재가"))
                    .Add("@TRADE_QTY", dr("거래량"))
                    .Add("@TRADE_DAEGUM", dr("거래대금"))
                    .Add("@START_PRICE", dr("시가"))
                    .Add("@HIGH_PRICE", dr("고가"))
                    .Add("@LOW_PRICE", dr("저가"))
                    .Add("@CHG_JUGA_GB", dr("수정주가구분"))
                    .Add("@CHG_RATE", dr("수정비율"))
                    .Add("@CHG_JUGA_EVENT", dr("수정주가이벤트"))
                    .Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput)
                End With

                oSql.ExecuteNonQuery("p_Opt10081Add", CommandType.StoredProcedure, arrParam)

            Next

            Dim nextDate As String = ""

            If _last_10081 <> ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item("일자") Then
                _last_10081 = ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item("일자")
                nextDate = CDateTime.FormatDate(DateAdd(DateInterval.Day, -1, CDate(CDateTime.FormatDate(ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item("일자"), "."))))
            Else
                nextDate = CDateTime.FormatDate(DateAdd(DateInterval.Day, -2, CDate(CDateTime.FormatDate(ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item("일자"), "."))))
            End If

            If Trim(_Min_10081) <> "" Then
                ' 10081 최소 일자가 크다면 최소값을 던지다.
                If _last_10081 > _Min_10081 Then
                    nextDate = _Min_10081
                End If
            End If

            System.Threading.Thread.Sleep(4000)

            If chkDay.Checked = True Then
                If nextDate < "20170101" Then
                    GetOpt10081(RetStockCode10081())
                    Exit Sub
                End If
            End If

            CallOpt10081(_stockCode_Opt10081, nextDate)

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub
#End Region

#Region " StoredScodeStatus "
    Private Sub StoredSCodeStatus( ByVal stockCode As String, ByVal actionGb As String)
        Dim arrParam As New ArrayParam
        Dim oSql As New Sql("EDPB2F011\VADIS", "RICHDB")

        With arrParam
            .Clear()
            .Add("@ACTION_GB", actionGb)
            .Add("@STOCK_CODE", stockCode)
            .Add("@STOCK_NAME", "")
            .Add("@YBJONG_CODE", "")
            .Add("@OPT10059_QTY", "Y")
            .Add("@OPT10059_PRICE", "Y")
            .Add("@OPT10081", "Y")
            .Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput)
        End With

        oSql.ExecuteNonQuery("p_ScodeAdd", CommandType.StoredProcedure, arrParam)

    End Sub
#End Region

    Private Sub btnStartJob_Click(sender As Object, e As EventArgs) Handles btnStartJob.Click
        proBarOpt10059Qty.Maximum = _ds.Tables(0).Rows.Count
        proBarOpt10059Price.Maximum = _ds.Tables(0).Rows.Count
        proBarOpt10081.Maximum = _ds.Tables(0).Rows.Count
        GetOpt10059(RetStockCode10059Qty())
    End Sub

    Private Sub dgvFCode_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFCode.CellDoubleClick
        If Trim(dgvFCode.Rows(e.RowIndex).Cells("SGROUP_CODE").Value.ToString()) <> "" Then
            lblGroupName.Text = Trim(dgvFCode.Rows(e.RowIndex).Cells("SGROUP_NAME").Value.ToString())
            _ds.Reset()

            Dim oQuery As New RichQuery
            _ds = oQuery.p_FCodeQuery("4", Trim(dgvFCode.Rows(e.RowIndex).Cells("SGROUP_CODE").Value.ToString()), "", "", False)

            MsgBox("해당 관심종목을 가져왔습니다.")

        End If
    End Sub
End Class