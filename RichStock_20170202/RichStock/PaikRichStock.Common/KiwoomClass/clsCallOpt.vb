﻿Imports System.IO
Imports System.Xml

Public Class clsCallOpt
    Private _MainStock As New PaikRichStock.Common.ucMainStockVer2

    Public WriteOnly Property MainStock As PaikRichStock.Common.ucMainStockVer2
        Set(value As PaikRichStock.Common.ucMainStockVer2)
            _MainStock = value
        End Set
    End Property

    Private _lastDate As String = ""

    Public WriteOnly Property LastDate As String
        Set(value As String)
            _lastDate = value
        End Set
    End Property

    Private _NewestData As Boolean = False

    Public Property NewestData As Boolean
        Set(value As Boolean)
            _NewestData = value
        End Set
        Get
            Return _NewestData
        End Get
    End Property

    'Private Sub GetXmlData()
    '    If File.Exists("C:\Xml\" & _stockCode & "_" & Trim(_MainStock.GetStockInfo(_stockCode)) & ".xml") Then
    '        Dim ds As New DataSet
    '        Dim xmlFile As XmlReader
    '        xmlFile = XmlReader.Create("C:\Xml\" & stockCode & "_" & Trim(_MainStock.GetStockInfo(stockCode)) & ".xml", New XmlReaderSettings())

    '        ds.ReadXml(xmlFile)
    '    End If
    'End Sub

#Region " 1. CallOpt "

#Region " CallOpt10014(공매도추이요청) "
    Public Event OnEventReturn10014ResultDt(ByVal dt As DataTable)

    Private _dtOpt10014 As New DataTable
    Private _stockCode_Opt10014 As String = ""

    Private Sub CallOpt10014(ByVal stockCode As String, ByVal fromDate As String, ByVal toDate As String)
        _MainStock.Opt10014_OnReceiveTrData(stockCode, _MainStock.GetStockInfo(stockCode), "1", fromDate, toDate)
    End Sub

    Public Sub GetOpt10014(ByVal stockCode As String)
        If stockCode = "" Then Exit Sub

        Dim fromDate As String = ""
        Dim toDate As String = ""
        _stockCode_Opt10014 = stockCode

        RemoveHandler _MainStock.OnReceiveTrData_Opt10014, AddressOf OnReceiveTrData_opt10014
        RemoveHandler _MainStock.OnReceiveTrData_Opt10014, AddressOf OnReceiveTrData_opt10014

        AddHandler _MainStock.OnReceiveTrData_Opt10014, AddressOf OnReceiveTrData_opt10014

        If _dtOpt10014 Is Nothing = False Then
            _dtOpt10014.Clear()
        End If

        fromDate = Now.Year & "0101"
        toDate = Now.Year & "1231"

        CallOpt10014(_stockCode_Opt10014, fromDate, toDate)

    End Sub

    Private Sub OnReceiveTrData_opt10014(ByVal ds As DataSet)
        If ds Is Nothing = True Then
            RaiseEvent OnEventReturn10014ResultDt(_dtOpt10014)
            Exit Sub
        End If

        If ds.Tables(0).Rows.Count < 1 Then
            RaiseEvent OnEventReturn10014ResultDt(_dtOpt10014)
            Exit Sub
        End If

        Dim dr2th As DataRow
        Dim nextDate As String = ""

        With _dtOpt10014

            If _dtOpt10014.Columns.Count < 1 Then

                If clsFunc.DataTableColumnCloneToDataSet(_dtOpt10014, ds) = False Then
                    MsgBox("_dtOpt10014에 컬럼 추가가 실패했습니다.", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If

            End If

            For Each dr In ds.Tables(0).Rows
                If .Rows.Count > 0 Then
                    If .Rows(.Rows.Count - 1).Item("일자") < dr("일자") Then Continue For
                End If

                dr2th = .Rows.Add

                For i As Integer = 0 To ds.Tables(0).Columns.Count - 1
                    dr2th(ds.Tables(0).Columns(i).ColumnName) = dr(ds.Tables(0).Columns(i).ColumnName)
                Next

            Next

            Dim fromDate As String = Mid(CDateTime.FormatDate(DateAdd(DateInterval.Year, -1, CDate(CDateTime.FormatDate(ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item("일자"), ".")))), 1, 4) & "0101"
            Dim toDate As String = Mid(fromDate, 1, 4) & "1231"

            System.Threading.Thread.Sleep(200)

            CallOpt10014(_stockCode_Opt10014, fromDate, toDate)

        End With

    End Sub
#End Region

#Region " CallOpt10059(종목별투자자기관별요청) "
    Public Event OnEventReturn10059ResultDt(ByVal dt As DataTable, ByVal calOpt10059 As CalOpt10059)

    Private _dtOpt10059 As New DataTable
    Private _Structure_CalOpt10059 As CalOpt10059
    Private _stockCode_Opt10059 As String = ""

    Private Sub CallOpt10059(ByVal startDate As String)
        _MainStock.Opt10059_OnReceiveTrData(startDate, Trim(_stockCode_Opt10059), _MainStock.GetStockInfo(_stockCode_Opt10059), "2", "0", "1")
    End Sub

    Public Sub GetOpt10059(ByVal stockCode As String)
        If stockCode = "" Then Exit Sub
        _stockCode_Opt10059 = stockCode
        'RemoveHandler _MainStock.OnReceiveTrData_Opt10059, AddressOf OnReceiveTrData_opt10059
        RemoveHandler _MainStock.OnReceiveTrData_Opt10059, AddressOf OnReceiveTrData_opt10059
        AddHandler _MainStock.OnReceiveTrData_Opt10059, AddressOf OnReceiveTrData_opt10059

        _Structure_CalOpt10059.Init()

        If _dtOpt10059 Is Nothing = False Then
            _dtOpt10059.Clear()
            _dtOpt10059.Reset()
        End If

        Dim sysDate As String = CDateTime.FormatDate(Now.Date)

        CallOpt10059(sysDate)

    End Sub

    Private Sub OnReceiveTrData_opt10059(ByVal ds As DataSet)
        If ds Is Nothing = True Then
            MathingVolumeAToDt10059()
            Exit Sub
        End If

        If ds.Tables(0).Rows.Count < 1 Then
            MathingVolumeAToDt10059()
            Exit Sub
        End If

        Dim dr2th As DataRow
        Dim nextDate As String = ""

        With _dtOpt10059
            If _dtOpt10059.Columns.Count < 1 Then

                If clsFunc.DataTableColumnCloneToDataSet(_dtOpt10059, ds) = False Then
                    MsgBox("_dtOpt10059에 컬럼 추가가 실패했습니다.", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If

                '.Columns.Add("총합")
                '.Columns.Add("유통량")

                '.Columns.Add("개인투자자합")
                '.Columns.Add("외국인투자자합")
                '.Columns.Add("기관계합")
                '.Columns.Add("금융투자합")
                '.Columns.Add("보험합")
                '.Columns.Add("투신합")
                '.Columns.Add("기타금융합")
                '.Columns.Add("은행합")
                '.Columns.Add("연기금등합")
                '.Columns.Add("사모펀드합")
                '.Columns.Add("국가합")
                '.Columns.Add("기타법인합")
                '.Columns.Add("내외국인합")

            End If

            For Each dr In ds.Tables(0).Rows
                ' 버그 때문에 추가(EX. 2016년 08월 11일자로 조회 했는데 2017년 자료가 다시 날라오는 경우가 있음)
                If .Rows.Count > 0 Then
                    If .Rows(.Rows.Count - 1).Item("일자") < dr("일자") Then Continue For
                End If

                If _lastDate <> "" Then
                    If dr("일자") <= _lastDate Then
                        Continue For
                    End If
                End If

                dr2th = .Rows.Add

                For i As Integer = 0 To ds.Tables(0).Columns.Count - 1
                    dr2th(ds.Tables(0).Columns(i).ColumnName) = dr(ds.Tables(0).Columns(i).ColumnName)
                Next

            Next

            nextDate = CDateTime.FormatDate(DateAdd(DateInterval.Day, -1, CDate(CDateTime.FormatDate(ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item("일자"), "."))))

        End With

        If _lastDate <> "" Then
            If nextDate <= _lastDate Then
                MathingVolumeAToDt10059()
                Exit Sub
            End If
        End If

        System.Threading.Thread.Sleep(300)

        CallOpt10059(nextDate)

    End Sub

    Private Sub MathingVolumeAToDt10059()
        'CalHighestLowestDistribution()
        RaiseEvent OnEventReturn10059ResultDt(_dtOpt10059, _Structure_CalOpt10059)
    End Sub
#End Region

#Region " CallOpt10059(종목별투자자기관별요청) 금액 "
    Public Event OnEventReturn10059PriceResultDt(ByVal dt As DataTable, ByVal calOpt10059Price As CalOpt10059Price)

    Private _dtOpt10059Price As New DataTable
    Private _Structure_CalOpt10059Price As CalOpt10059Price
    Private _stockCode_Opt10059Price As String = ""

    Private Sub CallOpt10059Price(ByVal startDate As String)
        _MainStock.Opt10059_OnReceiveTrDataPrice(startDate, Trim(_stockCode_Opt10059Price), _MainStock.GetStockInfo(_stockCode_Opt10059Price), "1", "0", "1")

    End Sub

    Public Sub GetOpt10059Price(ByVal stockCode As String)
        If stockCode = "" Then Exit Sub
        _stockCode_Opt10059Price = stockCode
        'RemoveHandler _MainStock.OnReceiveTrData_Opt10059Price, AddressOf OnReceiveTrData_opt10059Price
        RemoveHandler _MainStock.OnReceiveTrData_Opt10059Price, AddressOf OnReceiveTrData_opt10059Price

        AddHandler _MainStock.OnReceiveTrData_Opt10059Price, AddressOf OnReceiveTrData_opt10059Price

        _Structure_CalOpt10059Price.Init()

        If _dtOpt10059Price Is Nothing = False Then
            _dtOpt10059Price.Clear()
            _dtOpt10059Price.Reset()
        End If

        Dim sysDate As String = CDateTime.FormatDate(Now.Date)

        CallOpt10059Price(sysDate)

    End Sub

    Private Sub OnReceiveTrData_opt10059Price(ByVal ds As DataSet)
        If ds Is Nothing = True Then
            MathingVolumeAToDt10059Price()
            Exit Sub
        End If

        If ds.Tables(0).Rows.Count < 1 Then
            MathingVolumeAToDt10059Price()
            Exit Sub
        End If

        Dim dr2th As DataRow
        Dim nextDate As String = ""

        With _dtOpt10059Price
            If _dtOpt10059Price.Columns.Count < 1 Then

                If clsFunc.DataTableColumnCloneToDataSet(_dtOpt10059Price, ds) = False Then
                    MsgBox("_dtOpt10059Price에 컬럼 추가가 실패했습니다.", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If

                .Columns.Add("총금액합", System.Type.GetType("System.Int62"))
                .Columns.Add("유통금액량", System.Type.GetType("System.Int62"))

                For i As Integer = 0 To UBound(modDataTable.opt10059_Cal)
                    .Columns.Add(modDataTable.opt10059_Cal(i) & "금액합", System.Type.GetType("System.Int62"))
                Next

            End If

            For Each dr In ds.Tables(0).Rows
                ' 버그 때문에 추가(EX. 2016년 08월 11일자로 조회 했는데 2017년 자료가 다시 날라오는 경우가 있음)
                If .Rows.Count > 0 Then
                    If .Rows(.Rows.Count - 1).Item("일자") < dr("일자") Then Continue For
                End If

                If _lastDate <> "" Then
                    If dr("일자") <= _lastDate Then
                        Continue For
                    End If
                End If

                dr2th = .Rows.Add

                For i As Integer = 0 To ds.Tables(0).Columns.Count - 1
                    dr2th(ds.Tables(0).Columns(i).ColumnName) = dr(ds.Tables(0).Columns(i).ColumnName)
                Next

            Next

            nextDate = CDateTime.FormatDate(DateAdd(DateInterval.Day, -1, CDate(CDateTime.FormatDate(ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item("일자"), "."))))

        End With

        If _lastDate <> "" Then
            If nextDate <= _lastDate Then
                MathingVolumeAToDt10059Price()
                Exit Sub
            End If
        End If

        System.Threading.Thread.Sleep(300)

        CallOpt10059Price(nextDate)

    End Sub

    Private Sub MathingVolumeAToDt10059Price()
        'CalHighestLowestDistributionPrice()
        RaiseEvent OnEventReturn10059PriceResultDt(_dtOpt10059Price, _Structure_CalOpt10059Price)
    End Sub

#End Region

#Region " CallOpt10081(주식일봉차트조회요청) "

    Public Event OnEventReturn10081ResultDt(ByVal dt As DataTable)

    Private _dtOpt10081 As New DataTable
    Private _stockCode_Opt10081 As String = ""

    Private Sub CallOpt10081(ByVal stockCode As String, ByVal stdDate As String)
        _MainStock.Opt10081_OnReceiveTrData(stockCode, _MainStock.GetStockInfo(stockCode), stdDate)
    End Sub

    Public Sub GetOpt10081(ByVal stockCode As String)
        If stockCode = "" Then Exit Sub

        Dim stdDate As String = ""

        RemoveHandler _MainStock.OnReceiveTrData_opt10081, AddressOf OnReceiveTrData_opt10081
        RemoveHandler _MainStock.OnReceiveTrData_opt10081, AddressOf OnReceiveTrData_opt10081

        AddHandler _MainStock.OnReceiveTrData_opt10081, AddressOf OnReceiveTrData_opt10081

        If _dtOpt10081 Is Nothing = False Then
            _dtOpt10081.Clear()
            _dtOpt10081.Reset()
        End If

        stdDate = CDateTime.FormatDate(DateAdd("d", -1, CDateTime.FormatDate(Now.Date, "-")), "")

        _stockCode_Opt10081 = stockCode

        CallOpt10081(_stockCode_Opt10081, stdDate)

    End Sub

    Private Sub OnReceiveTrData_opt10081(ByVal ds As DataSet)
        If ds Is Nothing = True Then
            RaiseEvent OnEventReturn10081ResultDt(_dtOpt10081)
            Exit Sub
        End If

        If ds.Tables(0).Rows.Count < 1 Then
            RaiseEvent OnEventReturn10081ResultDt(_dtOpt10081)
            Exit Sub
        End If

        Dim dr2th As DataRow
        Dim nextDate As String = ""

        With _dtOpt10081

            If _dtOpt10081.Columns.Count < 1 Then

                If clsFunc.DataTableColumnCloneToDataSet(_dtOpt10081, ds) = False Then
                    MsgBox("_dtOpt10081에 컬럼 추가가 실패했습니다.", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If

            End If

            For Each dr In ds.Tables(0).Rows
                If .Rows.Count > 0 Then
                    If .Rows(.Rows.Count - 1).Item("일자") < dr("일자") Then Continue For
                End If

                If _lastDate <> "" Then
                    If dr("일자") <= _lastDate Then
                        Continue For
                    End If
                End If

                dr2th = .Rows.Add

                For i As Integer = 0 To ds.Tables(0).Columns.Count - 1
                    dr2th(ds.Tables(0).Columns(i).ColumnName) = dr(ds.Tables(0).Columns(i).ColumnName)
                Next

            Next
            Dim stdDate As String = ""

            stdDate = CDateTime.FormatDate(DateAdd("d", -1, CDateTime.FormatDate(Now.Date, "-")), "")

            nextDate = CDateTime.FormatDate(DateAdd(DateInterval.Day, -1, CDate(CDateTime.FormatDate(ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item("일자"), "."))))

            If _lastDate <> "" Then
                If nextDate <= _lastDate Then
                    RaiseEvent OnEventReturn10081ResultDt(_dtOpt10081)
                    Exit Sub
                End If
            End If

            System.Threading.Thread.Sleep(200)

            CallOpt10081(_stockCode_Opt10081, nextDate)

        End With

    End Sub
#End Region

#Region " CallOpt10061(주식일봉차트조회요청) "

    Public Event OnEventReturn10061ResultDt(ByVal dt As DataTable)

    Private _dtOpt10061 As New DataTable
    Private _stockCode_Opt10061 As String = ""
    Private _startYear As String = ""

    Private Sub CallOpt10061(ByVal stockCode As String, ByVal fromDate As String, ByVal ToDate As String)
        _MainStock.Opt10061_OnReceiveTrData(stockCode, _MainStock.GetStockInfo(stockCode), fromDate, ToDate, "2", "0", "1")
    End Sub

    Public Sub GetOpt10061(ByVal stockCode As String, ByVal startYear As String)
        If stockCode = "" Then Exit Sub

        Dim stdDate As String = ""

        RemoveHandler _MainStock.OnReceiveTrData_Opt10061, AddressOf OnReceiveTrData_opt10061
        RemoveHandler _MainStock.OnReceiveTrData_Opt10061, AddressOf OnReceiveTrData_opt10061

        AddHandler _MainStock.OnReceiveTrData_Opt10061, AddressOf OnReceiveTrData_opt10061

        If _dtOpt10061 Is Nothing = False Then
            _dtOpt10061.Clear()
        End If

        _startYear = startYear

        _stockCode_Opt10061 = stockCode

        CallOpt10061(_stockCode_Opt10061, startYear & "0101", startYear & "1231")

    End Sub

    Private Sub OnReceiveTrData_opt10061(ByVal ds As DataSet)
        If ds Is Nothing = True Then
            RaiseEvent OnEventReturn10061ResultDt(_dtOpt10061)
            Exit Sub
        End If

        If ds.Tables(0).Rows.Count < 1 Then
            RaiseEvent OnEventReturn10061ResultDt(_dtOpt10061)
            Exit Sub
        End If

        _startYear = (CInt(_startYear) + 1).ToString()

        If _startYear > Now.Year Then
            RaiseEvent OnEventReturn10061ResultDt(_dtOpt10061)
            Exit Sub
        End If

        Dim dr2th As DataRow
        Dim nextDate As String = ""

        With _dtOpt10061

            If _dtOpt10061.Columns.Count < 1 Then

                If clsFunc.DataTableColumnCloneToDataSet(_dtOpt10061, ds) = False Then
                    MsgBox("_dtOpt10061에 컬럼 추가가 실패했습니다.", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If

            End If

            For Each dr In ds.Tables(0).Rows

                dr2th = .Rows.Add

                For i As Integer = 0 To ds.Tables(0).Columns.Count - 1
                    dr2th(ds.Tables(0).Columns(i).ColumnName) = dr(ds.Tables(0).Columns(i).ColumnName)
                Next

            Next

            System.Threading.Thread.Sleep(200)

            CallOpt10061(_stockCode_Opt10061, _startYear & "0101", _startYear & "1231")

        End With

    End Sub
#End Region

#Region " CallOpt20068(대차거래추이요청(종목별)) "
    Public Event OnEventReturn20068ResultDt(ByVal dt As DataTable)

    Private _dtOpt20068 As New DataTable
    Private _stockCode_Opt20068 As String = ""

    Private Sub CallOpt20068(ByVal stockCode As String, ByVal fromDate As String, ByVal toDate As String)
        _MainStock.Opt20068_OnReceiveTrData(stockCode, _MainStock.GetStockInfo(stockCode), fromDate, toDate)
    End Sub

    Public Sub GetOpt20068(ByVal stockCode As String)
        If stockCode = "" Then Exit Sub

        Dim fromDate As String = ""
        Dim toDate As String = ""
        _stockCode_Opt20068 = stockCode

        RemoveHandler _MainStock.OnReceiveTrData_Opt20068, AddressOf OnReceiveTrData_opt20068
        RemoveHandler _MainStock.OnReceiveTrData_Opt20068, AddressOf OnReceiveTrData_opt20068

        AddHandler _MainStock.OnReceiveTrData_Opt20068, AddressOf OnReceiveTrData_opt20068

        If _dtOpt20068 Is Nothing = False Then
            _dtOpt20068.Clear()
        End If

        fromDate = Now.Year & "0101"
        toDate = Now.Year & "1231"

        CallOpt20068(_stockCode_Opt20068, fromDate, toDate)

    End Sub

    Private Sub OnReceiveTrData_opt20068(ByVal ds As DataSet)
        If ds Is Nothing = True Then
            RaiseEvent OnEventReturn20068ResultDt(_dtOpt20068)
            Exit Sub
        End If

        If ds.Tables(0).Rows.Count < 1 Then
            RaiseEvent OnEventReturn20068ResultDt(_dtOpt20068)
            Exit Sub
        End If

        Dim dr2th As DataRow
        Dim nextDate As String = ""

        With _dtOpt20068

            If _dtOpt20068.Columns.Count < 1 Then

                If clsFunc.DataTableColumnCloneToDataSet(_dtOpt20068, ds) = False Then
                    MsgBox("_dtOpt20068에 컬럼 추가가 실패했습니다.", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If

            End If

            For Each dr In ds.Tables(0).Rows
                If .Rows.Count > 0 Then
                    If .Rows(.Rows.Count - 1).Item("일자") < dr("일자") Then Continue For
                End If

                dr2th = .Rows.Add

                For i As Integer = 0 To ds.Tables(0).Columns.Count - 1
                    dr2th(ds.Tables(0).Columns(i).ColumnName) = dr(ds.Tables(0).Columns(i).ColumnName)
                Next

            Next

            Dim fromDate As String = Mid(CDateTime.FormatDate(DateAdd(DateInterval.Year, -1, CDate(CDateTime.FormatDate(ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item("일자"), ".")))), 1, 4) & "0101"
            Dim toDate As String = Mid(fromDate, 1, 4) & "1231"

            If _lastDate <> "" Then
                If toDate < _lastDate Then
                    RaiseEvent OnEventReturn20068ResultDt(_dtOpt20068)
                    Exit Sub
                End If
            End If


            System.Threading.Thread.Sleep(200)

            CallOpt20068(_stockCode_Opt20068, fromDate, toDate)

        End With

    End Sub
#End Region

#End Region


#Region " 삭제할것 "
    Public Structure CalOpt10059
        Public INVESTGBSUM_GAEIN As Integer
        Public INVESTGBSUM_FORE As Integer
        Public INVESTGBSUM_GIGAN As Integer
        Public INVESTGBSUM_GUMWOONG As Integer
        Public INVESTGBSUM_BOHUM As Integer
        Public INVESTGBSUM_TUSIN As Integer
        Public INVESTGBSUM_GITAGUMWOONG As Integer
        Public INVESTGBSUM_BANK As Integer
        Public INVESTGBSUM_YEUNGGIGUM As Integer
        Public INVESTGBSUM_SAMOFUND As Integer
        Public INVESTGBSUM_NATION As Integer
        Public INVESTGBSUM_GITABUBIN As Integer
        Public INVESTGBSUM_IOFOR As Integer
        Public YOOTONG_QTY As Integer
        Public AVALIBLE_STOCKQTY As Integer
        Public MAX_HIGHEST_DATE As String
        Public MAX_HIGHEST_PRICE As Integer
        Public MIN_LOWEST_DATE As String
        Public MIN_LOWEST_PRICE As Integer

        Public Sub Init()
            INVESTGBSUM_GAEIN = 0
            INVESTGBSUM_FORE = 0
            INVESTGBSUM_GIGAN = 0
            INVESTGBSUM_GUMWOONG = 0
            INVESTGBSUM_BOHUM = 0
            INVESTGBSUM_TUSIN = 0
            INVESTGBSUM_GITAGUMWOONG = 0
            INVESTGBSUM_BANK = 0
            INVESTGBSUM_YEUNGGIGUM = 0
            INVESTGBSUM_SAMOFUND = 0
            INVESTGBSUM_NATION = 0
            INVESTGBSUM_GITABUBIN = 0
            INVESTGBSUM_IOFOR = 0
            YOOTONG_QTY = 0
            AVALIBLE_STOCKQTY = 0
            MAX_HIGHEST_DATE = ""
            MAX_HIGHEST_PRICE = 0
            MIN_LOWEST_DATE = ""
            MIN_LOWEST_PRICE = 0
        End Sub
    End Structure

    Private Sub CalHighestLowestDistribution()

        Dim maxHighestPrice As Integer = 0
        Dim maxHighestDate As String = ""

        Dim intInvestGbSum_GaeIn As Integer = 0
        Dim intInvestGbSum_Fore As Integer = 0
        Dim intInvestGbSum_Gigan As Integer = 0
        Dim intInvestGbSum_GumWoong As Integer = 0
        Dim intInvestGbSum_Bohum As Integer = 0
        Dim intInvestGbSum_Tusin As Integer = 0
        Dim intInvestGbSum_GitaGumWoong As Integer = 0
        Dim intInvestGbSum_bank As Integer = 0
        Dim intInvestGbSum_YeungGiGum As Integer = 0
        Dim intInvestGbSum_SamoFund As Integer = 0
        Dim intInvestGbSum_Nation As Integer = 0
        Dim intInvestGbSum_GitaBubin As Integer = 0
        Dim intInvestGbSum_IOFor As Integer = 0

        Dim intYooTongQty As Integer = 0
        Dim intSumQty As Integer = 0
        Dim intGiganSumQty As Integer = 0

        Dim giganSum As Integer = 0

        Dim currentPrice As Integer = 0

        With _dtOpt10059

            For row As Integer = .Rows.Count - 1 To 0 Step -1

                With _Structure_CalOpt10059

                    giganSum = CInt(_dtOpt10059.Rows(row).Item("금융투자")) + CInt(_dtOpt10059.Rows(row).Item("보험")) + CInt(_dtOpt10059.Rows(row).Item("투신")) + CInt(_dtOpt10059.Rows(row).Item("기타금융")) + _
                               CInt(_dtOpt10059.Rows(row).Item("은행")) + CInt(_dtOpt10059.Rows(row).Item("연기금등")) + CInt(_dtOpt10059.Rows(row).Item("사모펀드")) + CInt(_dtOpt10059.Rows(row).Item("국가"))

                    .INVESTGBSUM_GAEIN = .INVESTGBSUM_GAEIN + CInt(_dtOpt10059.Rows(row).Item("개인투자자"))
                    .INVESTGBSUM_FORE = .INVESTGBSUM_FORE + CInt(_dtOpt10059.Rows(row).Item("외국인투자자"))
                    .INVESTGBSUM_GIGAN = .INVESTGBSUM_GIGAN + giganSum
                    .INVESTGBSUM_GUMWOONG = .INVESTGBSUM_GUMWOONG + CInt(_dtOpt10059.Rows(row).Item("금융투자"))
                    .INVESTGBSUM_BOHUM = .INVESTGBSUM_BOHUM + CInt(_dtOpt10059.Rows(row).Item("보험"))
                    .INVESTGBSUM_TUSIN = .INVESTGBSUM_TUSIN + CInt(_dtOpt10059.Rows(row).Item("투신"))
                    .INVESTGBSUM_GITAGUMWOONG = .INVESTGBSUM_GITAGUMWOONG + CInt(_dtOpt10059.Rows(row).Item("기타금융"))
                    .INVESTGBSUM_BANK = .INVESTGBSUM_BANK + CInt(_dtOpt10059.Rows(row).Item("은행"))
                    .INVESTGBSUM_YEUNGGIGUM = .INVESTGBSUM_YEUNGGIGUM + CInt(_dtOpt10059.Rows(row).Item("연기금등"))
                    .INVESTGBSUM_SAMOFUND = .INVESTGBSUM_SAMOFUND + CInt(_dtOpt10059.Rows(row).Item("사모펀드"))
                    .INVESTGBSUM_NATION = .INVESTGBSUM_NATION + CInt(_dtOpt10059.Rows(row).Item("국가"))
                    .INVESTGBSUM_GITABUBIN = .INVESTGBSUM_GITABUBIN + CInt(_dtOpt10059.Rows(row).Item("기타법인"))
                    .INVESTGBSUM_IOFOR = .INVESTGBSUM_IOFOR + CInt(_dtOpt10059.Rows(row).Item("내외국인"))

                End With

                currentPrice = CInt(Replace(Replace(.Rows(0).Item("현재가"), "+", ""), "-", ""))

                If Mid(.Rows(.Rows.Count - 1).Item("일자"), 1, 4) = Mid(.Rows(0).Item("일자"), 1, 4) Then ' 신규상장
                    If _Structure_CalOpt10059.MAX_HIGHEST_DATE = "" Then
                        _Structure_CalOpt10059.MAX_HIGHEST_PRICE = currentPrice
                        _Structure_CalOpt10059.MAX_HIGHEST_DATE = Trim(.Rows(row).Item("일자"))
                    Else
                        If _Structure_CalOpt10059.MAX_HIGHEST_DATE < currentPrice Then
                            _Structure_CalOpt10059.MAX_HIGHEST_PRICE = currentPrice
                            _Structure_CalOpt10059.MAX_HIGHEST_DATE = Trim(.Rows(row).Item("일자"))
                        End If
                    End If

                    If _Structure_CalOpt10059.MIN_LOWEST_DATE = "" Then
                        _Structure_CalOpt10059.MIN_LOWEST_PRICE = currentPrice
                        _Structure_CalOpt10059.MIN_LOWEST_DATE = Trim(.Rows(row).Item("일자"))
                    Else
                        If _Structure_CalOpt10059.MIN_LOWEST_DATE > currentPrice Then
                            _Structure_CalOpt10059.MIN_LOWEST_PRICE = currentPrice
                            _Structure_CalOpt10059.MIN_LOWEST_DATE = Trim(.Rows(row).Item("일자"))
                        End If
                    End If
                Else
                    If Mid(.Rows(.Rows.Count - 1).Item("일자"), 1, 4) < Mid(.Rows(row).Item("일자"), 1, 4) Then
                        If _Structure_CalOpt10059.MAX_HIGHEST_DATE = "" Then
                            _Structure_CalOpt10059.MAX_HIGHEST_PRICE = currentPrice
                            _Structure_CalOpt10059.MAX_HIGHEST_DATE = Trim(.Rows(row).Item("일자"))
                        Else
                            If _Structure_CalOpt10059.MAX_HIGHEST_DATE < currentPrice Then
                                _Structure_CalOpt10059.MAX_HIGHEST_PRICE = currentPrice
                                _Structure_CalOpt10059.MAX_HIGHEST_DATE = Trim(.Rows(row).Item("일자"))
                            End If
                        End If

                        If _Structure_CalOpt10059.MIN_LOWEST_DATE = "" Then
                            _Structure_CalOpt10059.MIN_LOWEST_PRICE = currentPrice
                            _Structure_CalOpt10059.MIN_LOWEST_DATE = Trim(.Rows(row).Item("일자"))
                        Else
                            If _Structure_CalOpt10059.MIN_LOWEST_DATE > currentPrice Then
                                _Structure_CalOpt10059.MIN_LOWEST_PRICE = currentPrice
                                _Structure_CalOpt10059.MIN_LOWEST_DATE = Trim(.Rows(row).Item("일자"))
                            End If
                        End If
                    End If
                End If

                If row = .Rows.Count - 1 Then

                    intInvestGbSum_GaeIn = .Rows(row).Item("개인투자자")
                    intInvestGbSum_Fore = .Rows(row).Item("외국인투자자")
                    'intInvestGbSum_Gigan = .Rows(row).Item("기관계")
                    intInvestGbSum_GumWoong = .Rows(row).Item("금융투자")
                    intInvestGbSum_Bohum = .Rows(row).Item("보험")
                    intInvestGbSum_Tusin = .Rows(row).Item("투신")
                    intInvestGbSum_GitaGumWoong = .Rows(row).Item("기타금융")
                    intInvestGbSum_bank = .Rows(row).Item("은행")
                    intInvestGbSum_YeungGiGum = .Rows(row).Item("연기금등")
                    intInvestGbSum_SamoFund = .Rows(row).Item("사모펀드")
                    intInvestGbSum_Nation = .Rows(row).Item("국가")
                    intInvestGbSum_GitaBubin = .Rows(row).Item("기타법인")
                    intInvestGbSum_IOFor = .Rows(row).Item("내외국인")

                Else

                    intInvestGbSum_GaeIn = intInvestGbSum_GaeIn + .Rows(row).Item("개인투자자")
                    intInvestGbSum_Fore = intInvestGbSum_Fore + .Rows(row).Item("외국인투자자")
                    'intInvestGbSum_Gigan = intInvestGbSum_Gigan + .Rows(row).Item("기관계")
                    intInvestGbSum_GumWoong = intInvestGbSum_GumWoong + .Rows(row).Item("금융투자")
                    intInvestGbSum_Bohum = intInvestGbSum_Bohum + .Rows(row).Item("보험")
                    intInvestGbSum_Tusin = intInvestGbSum_Tusin + .Rows(row).Item("투신")
                    intInvestGbSum_GitaGumWoong = intInvestGbSum_GitaGumWoong + .Rows(row).Item("기타금융")
                    intInvestGbSum_bank = intInvestGbSum_bank + .Rows(row).Item("은행")
                    intInvestGbSum_YeungGiGum = intInvestGbSum_YeungGiGum + .Rows(row).Item("연기금등")
                    intInvestGbSum_SamoFund = intInvestGbSum_SamoFund + .Rows(row).Item("사모펀드")
                    intInvestGbSum_Nation = intInvestGbSum_Nation + .Rows(row).Item("국가")
                    intInvestGbSum_GitaBubin = intInvestGbSum_GitaBubin + .Rows(row).Item("기타법인")
                    intInvestGbSum_IOFor = intInvestGbSum_IOFor + .Rows(row).Item("내외국인")

                End If

                intGiganSumQty = intInvestGbSum_GumWoong + intInvestGbSum_Bohum + intInvestGbSum_Tusin + intInvestGbSum_GitaGumWoong + intInvestGbSum_bank + _
                                 intInvestGbSum_YeungGiGum + intInvestGbSum_SamoFund + intInvestGbSum_Nation

                intSumQty = intInvestGbSum_GaeIn + intInvestGbSum_Fore + intInvestGbSum_GitaBubin + intInvestGbSum_IOFor + intGiganSumQty

                If intInvestGbSum_GaeIn > 0 Then
                    intYooTongQty = intInvestGbSum_GaeIn
                End If

                If intInvestGbSum_Fore > 0 Then
                    intYooTongQty = intYooTongQty + intInvestGbSum_Fore
                End If

                If intInvestGbSum_Gigan > 0 Then
                    intYooTongQty = intYooTongQty + intInvestGbSum_Gigan
                End If

                If intInvestGbSum_GitaBubin > 0 Then
                    intYooTongQty = intYooTongQty + intInvestGbSum_GitaBubin
                End If

                If intInvestGbSum_Nation > 0 Then
                    intYooTongQty = intYooTongQty + intInvestGbSum_Nation
                End If

                If intInvestGbSum_IOFor > 0 Then
                    intYooTongQty = intYooTongQty + intInvestGbSum_IOFor
                End If

                .Rows(row).Item("총합") = intSumQty
                .Rows(row).Item("유통량") = intYooTongQty
                .Rows(row).Item("개인투자자합") = intInvestGbSum_GaeIn
                .Rows(row).Item("외국인투자자합") = intInvestGbSum_Fore
                .Rows(row).Item("기관계합") = intGiganSumQty
                .Rows(row).Item("금융투자합") = intInvestGbSum_GumWoong
                .Rows(row).Item("보험합") = intInvestGbSum_Bohum
                .Rows(row).Item("투신합") = intInvestGbSum_Tusin
                .Rows(row).Item("기타금융합") = intInvestGbSum_GitaGumWoong
                .Rows(row).Item("은행합") = intInvestGbSum_bank
                .Rows(row).Item("연기금등합") = intInvestGbSum_YeungGiGum
                .Rows(row).Item("사모펀드합") = intInvestGbSum_SamoFund
                .Rows(row).Item("국가합") = intInvestGbSum_Nation
                .Rows(row).Item("기타법인합") = intInvestGbSum_GitaBubin
                .Rows(row).Item("내외국인합") = intInvestGbSum_IOFor

                intYooTongQty = 0
                intSumQty = 0
                intGiganSumQty = 0

            Next

            _Structure_CalOpt10059.AVALIBLE_STOCKQTY = _Structure_CalOpt10059.INVESTGBSUM_GAEIN + _Structure_CalOpt10059.INVESTGBSUM_FORE + _
                                             _Structure_CalOpt10059.INVESTGBSUM_GIGAN + _Structure_CalOpt10059.INVESTGBSUM_GITABUBIN + _Structure_CalOpt10059.INVESTGBSUM_IOFOR

            If intInvestGbSum_GaeIn > 0 Then
                _Structure_CalOpt10059.YOOTONG_QTY = intInvestGbSum_GaeIn
            End If

            If intInvestGbSum_Fore > 0 Then
                _Structure_CalOpt10059.YOOTONG_QTY = _Structure_CalOpt10059.YOOTONG_QTY + intInvestGbSum_Fore
            End If

            If intInvestGbSum_Gigan > 0 Then
                _Structure_CalOpt10059.YOOTONG_QTY = _Structure_CalOpt10059.YOOTONG_QTY + intInvestGbSum_Gigan
            End If

            If intInvestGbSum_GitaBubin > 0 Then
                _Structure_CalOpt10059.YOOTONG_QTY = _Structure_CalOpt10059.YOOTONG_QTY + intInvestGbSum_GitaBubin
            End If

            If intInvestGbSum_IOFor > 0 Then
                _Structure_CalOpt10059.YOOTONG_QTY = _Structure_CalOpt10059.YOOTONG_QTY + intInvestGbSum_IOFor
            End If

        End With

    End Sub

    Public Structure CalOpt10059Price
        Public INVESTGBSUM_GAEIN As Integer
        Public INVESTGBSUM_FORE As Integer
        Public INVESTGBSUM_GIGAN As Integer
        Public INVESTGBSUM_GUMWOONG As Integer
        Public INVESTGBSUM_BOHUM As Integer
        Public INVESTGBSUM_TUSIN As Integer
        Public INVESTGBSUM_GITAGUMWOONG As Integer
        Public INVESTGBSUM_BANK As Integer
        Public INVESTGBSUM_YEUNGGIGUM As Integer
        Public INVESTGBSUM_SAMOFUND As Integer
        Public INVESTGBSUM_NATION As Integer
        Public INVESTGBSUM_GITABUBIN As Integer
        Public INVESTGBSUM_IOFOR As Integer
        Public YOOTONG_QTY As Integer
        Public AVALIBLE_STOCKQTY As Integer
        Public MAX_HIGHEST_DATE As String
        Public MAX_HIGHEST_PRICE As Integer
        Public MIN_LOWEST_DATE As String
        Public MIN_LOWEST_PRICE As Integer

        Public Sub Init()
            INVESTGBSUM_GAEIN = 0
            INVESTGBSUM_FORE = 0
            INVESTGBSUM_GIGAN = 0
            INVESTGBSUM_GUMWOONG = 0
            INVESTGBSUM_BOHUM = 0
            INVESTGBSUM_TUSIN = 0
            INVESTGBSUM_GITAGUMWOONG = 0
            INVESTGBSUM_BANK = 0
            INVESTGBSUM_YEUNGGIGUM = 0
            INVESTGBSUM_SAMOFUND = 0
            INVESTGBSUM_NATION = 0
            INVESTGBSUM_GITABUBIN = 0
            INVESTGBSUM_IOFOR = 0
            YOOTONG_QTY = 0
            AVALIBLE_STOCKQTY = 0
            MAX_HIGHEST_DATE = ""
            MAX_HIGHEST_PRICE = 0
            MIN_LOWEST_DATE = ""
            MIN_LOWEST_PRICE = 0
        End Sub
    End Structure

    Private Sub CalHighestLowestDistributionPrice()

        Dim maxHighestPrice As Integer = 0
        Dim maxHighestDate As String = ""

        Dim intInvestGbSum_GaeIn As Integer = 0
        Dim intInvestGbSum_Fore As Integer = 0
        Dim intInvestGbSum_Gigan As Integer = 0
        Dim intInvestGbSum_GumWoong As Integer = 0
        Dim intInvestGbSum_Bohum As Integer = 0
        Dim intInvestGbSum_Tusin As Integer = 0
        Dim intInvestGbSum_GitaGumWoong As Integer = 0
        Dim intInvestGbSum_bank As Integer = 0
        Dim intInvestGbSum_YeungGiGum As Integer = 0
        Dim intInvestGbSum_SamoFund As Integer = 0
        Dim intInvestGbSum_Nation As Integer = 0
        Dim intInvestGbSum_GitaBubin As Integer = 0
        Dim intInvestGbSum_IOFor As Integer = 0

        Dim intYooTongQty As Integer = 0
        Dim intSumQty As Integer = 0
        Dim intGiganSumQty As Integer = 0

        Dim giganSum As Integer = 0

        Dim currentPrice As Integer = 0

        With _dtOpt10059Price

            For row As Integer = .Rows.Count - 1 To 0 Step -1

                With _Structure_CalOpt10059Price

                    giganSum = CInt(_dtOpt10059Price.Rows(row).Item("금융투자")) + CInt(_dtOpt10059Price.Rows(row).Item("보험")) + CInt(_dtOpt10059Price.Rows(row).Item("투신")) + CInt(_dtOpt10059Price.Rows(row).Item("기타금융")) + _
                               CInt(_dtOpt10059Price.Rows(row).Item("은행")) + CInt(_dtOpt10059Price.Rows(row).Item("연기금등")) + CInt(_dtOpt10059Price.Rows(row).Item("사모펀드")) + CInt(_dtOpt10059Price.Rows(row).Item("국가"))

                    .INVESTGBSUM_GAEIN = .INVESTGBSUM_GAEIN + CInt(_dtOpt10059Price.Rows(row).Item("개인투자자"))
                    .INVESTGBSUM_FORE = .INVESTGBSUM_FORE + CInt(_dtOpt10059Price.Rows(row).Item("외국인투자자"))
                    .INVESTGBSUM_GIGAN = .INVESTGBSUM_GIGAN + giganSum
                    .INVESTGBSUM_GUMWOONG = .INVESTGBSUM_GUMWOONG + CInt(_dtOpt10059Price.Rows(row).Item("금융투자"))
                    .INVESTGBSUM_BOHUM = .INVESTGBSUM_BOHUM + CInt(_dtOpt10059Price.Rows(row).Item("보험"))
                    .INVESTGBSUM_TUSIN = .INVESTGBSUM_TUSIN + CInt(_dtOpt10059Price.Rows(row).Item("투신"))
                    .INVESTGBSUM_GITAGUMWOONG = .INVESTGBSUM_GITAGUMWOONG + CInt(_dtOpt10059Price.Rows(row).Item("기타금융"))
                    .INVESTGBSUM_BANK = .INVESTGBSUM_BANK + CInt(_dtOpt10059Price.Rows(row).Item("은행"))
                    .INVESTGBSUM_YEUNGGIGUM = .INVESTGBSUM_YEUNGGIGUM + CInt(_dtOpt10059Price.Rows(row).Item("연기금등"))
                    .INVESTGBSUM_SAMOFUND = .INVESTGBSUM_SAMOFUND + CInt(_dtOpt10059Price.Rows(row).Item("사모펀드"))
                    .INVESTGBSUM_NATION = .INVESTGBSUM_NATION + CInt(_dtOpt10059Price.Rows(row).Item("국가"))
                    .INVESTGBSUM_GITABUBIN = .INVESTGBSUM_GITABUBIN + CInt(_dtOpt10059Price.Rows(row).Item("기타법인"))
                    .INVESTGBSUM_IOFOR = .INVESTGBSUM_IOFOR + CInt(_dtOpt10059Price.Rows(row).Item("내외국인"))

                End With

                currentPrice = CInt(Replace(Replace(.Rows(0).Item("현재가"), "+", ""), "-", ""))

                If Mid(.Rows(.Rows.Count - 1).Item("일자"), 1, 4) = Mid(.Rows(0).Item("일자"), 1, 4) Then ' 신규상장
                    If _Structure_CalOpt10059Price.MAX_HIGHEST_DATE = "" Then
                        _Structure_CalOpt10059Price.MAX_HIGHEST_PRICE = currentPrice
                        _Structure_CalOpt10059Price.MAX_HIGHEST_DATE = Trim(.Rows(row).Item("일자"))
                    Else
                        If _Structure_CalOpt10059Price.MAX_HIGHEST_DATE < currentPrice Then
                            _Structure_CalOpt10059Price.MAX_HIGHEST_PRICE = currentPrice
                            _Structure_CalOpt10059Price.MAX_HIGHEST_DATE = Trim(.Rows(row).Item("일자"))
                        End If
                    End If

                    If _Structure_CalOpt10059Price.MIN_LOWEST_DATE = "" Then
                        _Structure_CalOpt10059Price.MIN_LOWEST_PRICE = currentPrice
                        _Structure_CalOpt10059Price.MIN_LOWEST_DATE = Trim(.Rows(row).Item("일자"))
                    Else
                        If _Structure_CalOpt10059Price.MIN_LOWEST_DATE > currentPrice Then
                            _Structure_CalOpt10059Price.MIN_LOWEST_PRICE = currentPrice
                            _Structure_CalOpt10059Price.MIN_LOWEST_DATE = Trim(.Rows(row).Item("일자"))
                        End If
                    End If
                Else
                    If Mid(.Rows(.Rows.Count - 1).Item("일자"), 1, 4) < Mid(.Rows(row).Item("일자"), 1, 4) Then
                        If _Structure_CalOpt10059Price.MAX_HIGHEST_DATE = "" Then
                            _Structure_CalOpt10059Price.MAX_HIGHEST_PRICE = currentPrice
                            _Structure_CalOpt10059Price.MAX_HIGHEST_DATE = Trim(.Rows(row).Item("일자"))
                        Else
                            If _Structure_CalOpt10059Price.MAX_HIGHEST_DATE < currentPrice Then
                                _Structure_CalOpt10059Price.MAX_HIGHEST_PRICE = currentPrice
                                _Structure_CalOpt10059Price.MAX_HIGHEST_DATE = Trim(.Rows(row).Item("일자"))
                            End If
                        End If

                        If _Structure_CalOpt10059Price.MIN_LOWEST_DATE = "" Then
                            _Structure_CalOpt10059Price.MIN_LOWEST_PRICE = currentPrice
                            _Structure_CalOpt10059Price.MIN_LOWEST_DATE = Trim(.Rows(row).Item("일자"))
                        Else
                            If _Structure_CalOpt10059Price.MIN_LOWEST_DATE > currentPrice Then
                                _Structure_CalOpt10059Price.MIN_LOWEST_PRICE = currentPrice
                                _Structure_CalOpt10059Price.MIN_LOWEST_DATE = Trim(.Rows(row).Item("일자"))
                            End If
                        End If
                    End If
                End If

                If row = .Rows.Count - 1 Then

                    intInvestGbSum_GaeIn = .Rows(row).Item("개인투자자")
                    intInvestGbSum_Fore = .Rows(row).Item("외국인투자자")
                    'intInvestGbSum_Gigan = .Rows(row).Item("기관계")
                    intInvestGbSum_GumWoong = .Rows(row).Item("금융투자")
                    intInvestGbSum_Bohum = .Rows(row).Item("보험")
                    intInvestGbSum_Tusin = .Rows(row).Item("투신")
                    intInvestGbSum_GitaGumWoong = .Rows(row).Item("기타금융")
                    intInvestGbSum_bank = .Rows(row).Item("은행")
                    intInvestGbSum_YeungGiGum = .Rows(row).Item("연기금등")
                    intInvestGbSum_SamoFund = .Rows(row).Item("사모펀드")
                    intInvestGbSum_Nation = .Rows(row).Item("국가")
                    intInvestGbSum_GitaBubin = .Rows(row).Item("기타법인")
                    intInvestGbSum_IOFor = .Rows(row).Item("내외국인")

                Else

                    intInvestGbSum_GaeIn = intInvestGbSum_GaeIn + .Rows(row).Item("개인투자자")
                    intInvestGbSum_Fore = intInvestGbSum_Fore + .Rows(row).Item("외국인투자자")
                    'intInvestGbSum_Gigan = intInvestGbSum_Gigan + .Rows(row).Item("기관계")
                    intInvestGbSum_GumWoong = intInvestGbSum_GumWoong + .Rows(row).Item("금융투자")
                    intInvestGbSum_Bohum = intInvestGbSum_Bohum + .Rows(row).Item("보험")
                    intInvestGbSum_Tusin = intInvestGbSum_Tusin + .Rows(row).Item("투신")
                    intInvestGbSum_GitaGumWoong = intInvestGbSum_GitaGumWoong + .Rows(row).Item("기타금융")
                    intInvestGbSum_bank = intInvestGbSum_bank + .Rows(row).Item("은행")
                    intInvestGbSum_YeungGiGum = intInvestGbSum_YeungGiGum + .Rows(row).Item("연기금등")
                    intInvestGbSum_SamoFund = intInvestGbSum_SamoFund + .Rows(row).Item("사모펀드")
                    intInvestGbSum_Nation = intInvestGbSum_Nation + .Rows(row).Item("국가")
                    intInvestGbSum_GitaBubin = intInvestGbSum_GitaBubin + .Rows(row).Item("기타법인")
                    intInvestGbSum_IOFor = intInvestGbSum_IOFor + .Rows(row).Item("내외국인")

                End If

                intGiganSumQty = intInvestGbSum_GumWoong + intInvestGbSum_Bohum + intInvestGbSum_Tusin + intInvestGbSum_GitaGumWoong + intInvestGbSum_bank + _
                                 intInvestGbSum_YeungGiGum + intInvestGbSum_SamoFund + intInvestGbSum_Nation

                intSumQty = intInvestGbSum_GaeIn + intInvestGbSum_Fore + intInvestGbSum_GitaBubin + intInvestGbSum_IOFor + intGiganSumQty

                If intInvestGbSum_GaeIn > 0 Then
                    intYooTongQty = intInvestGbSum_GaeIn
                End If

                If intInvestGbSum_Fore > 0 Then
                    intYooTongQty = intYooTongQty + intInvestGbSum_Fore
                End If

                If intInvestGbSum_Gigan > 0 Then
                    intYooTongQty = intYooTongQty + intInvestGbSum_Gigan
                End If

                If intInvestGbSum_GitaBubin > 0 Then
                    intYooTongQty = intYooTongQty + intInvestGbSum_GitaBubin
                End If

                If intInvestGbSum_Nation > 0 Then
                    intYooTongQty = intYooTongQty + intInvestGbSum_Nation
                End If

                If intInvestGbSum_IOFor > 0 Then
                    intYooTongQty = intYooTongQty + intInvestGbSum_IOFor
                End If

                .Rows(row).Item("총금액합") = intSumQty
                .Rows(row).Item("유통금액량") = intYooTongQty
                .Rows(row).Item("개인투자자금액합") = intInvestGbSum_GaeIn
                .Rows(row).Item("외국인투자자금액합") = intInvestGbSum_Fore
                .Rows(row).Item("기관계금액합") = intGiganSumQty
                .Rows(row).Item("금융투자금액합") = intInvestGbSum_GumWoong
                .Rows(row).Item("보험금액합") = intInvestGbSum_Bohum
                .Rows(row).Item("투신금액합") = intInvestGbSum_Tusin
                .Rows(row).Item("기타금융금액합") = intInvestGbSum_GitaGumWoong
                .Rows(row).Item("은행금액합") = intInvestGbSum_bank
                .Rows(row).Item("연기금등금액합") = intInvestGbSum_YeungGiGum
                .Rows(row).Item("사모펀드금액합") = intInvestGbSum_SamoFund
                .Rows(row).Item("국가금액합") = intInvestGbSum_Nation
                .Rows(row).Item("기타법인금액합") = intInvestGbSum_GitaBubin
                .Rows(row).Item("내외국인금액합") = intInvestGbSum_IOFor

                intYooTongQty = 0
                intSumQty = 0
                intGiganSumQty = 0

            Next

            _Structure_CalOpt10059Price.AVALIBLE_STOCKQTY = _Structure_CalOpt10059Price.INVESTGBSUM_GAEIN + _Structure_CalOpt10059Price.INVESTGBSUM_FORE + _
                                             _Structure_CalOpt10059Price.INVESTGBSUM_GIGAN + _Structure_CalOpt10059Price.INVESTGBSUM_GITABUBIN + _Structure_CalOpt10059Price.INVESTGBSUM_IOFOR

            If intInvestGbSum_GaeIn > 0 Then
                _Structure_CalOpt10059Price.YOOTONG_QTY = intInvestGbSum_GaeIn
            End If

            If intInvestGbSum_Fore > 0 Then
                _Structure_CalOpt10059Price.YOOTONG_QTY = _Structure_CalOpt10059Price.YOOTONG_QTY + intInvestGbSum_Fore
            End If

            If intInvestGbSum_Gigan > 0 Then
                _Structure_CalOpt10059Price.YOOTONG_QTY = _Structure_CalOpt10059Price.YOOTONG_QTY + intInvestGbSum_Gigan
            End If

            If intInvestGbSum_GitaBubin > 0 Then
                _Structure_CalOpt10059Price.YOOTONG_QTY = _Structure_CalOpt10059Price.YOOTONG_QTY + intInvestGbSum_GitaBubin
            End If

            If intInvestGbSum_IOFor > 0 Then
                _Structure_CalOpt10059Price.YOOTONG_QTY = _Structure_CalOpt10059Price.YOOTONG_QTY + intInvestGbSum_IOFor
            End If

        End With

    End Sub
#End Region

End Class