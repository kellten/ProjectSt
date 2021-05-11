﻿Imports System.IO
Imports System.Xml

Public Class clsCombineCallOpt

    Private _MainStock As New PaikRichStock.Common.ucMainStockVer2
    Private Const SLEEP_TIME As Integer = 600

    Public WriteOnly Property MainStock As PaikRichStock.Common.ucMainStockVer2
        Set(value As PaikRichStock.Common.ucMainStockVer2)
            _MainStock = value
            AddHandler _MainStock.OnReceiveTrData_Opt10059, AddressOf OnReceiveTrData_opt10059
            AddHandler _MainStock.OnReceiveTrData_Opt10059Price, AddressOf OnReceiveTrData_opt10059Price
            AddHandler _MainStock.OnReceiveTrData_opt10081New, AddressOf OnReceiveTrData_opt10081
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

    Public Event OnEventCombineMsg(ByVal msg As String)

    Private _dt As New DataTable

    Public Sub CallOpt5981(ByVal stockCode As String, ByVal stockName As String)

        If _dt Is Nothing = False Then
            _dt.Clear()
            _dt.Reset()
            _dt = Nothing
            _dt = New DataTable
        End If

        If File.Exists("C:\Xml2\" & stockCode & "_" & stockName & ".xml") Then

            Dim xmlFile As XmlReader
            Dim xmlFileSc As XmlReader
            xmlFile = XmlReader.Create("C:\Xml2\" & stockCode & "_" & stockName & ".xml", New XmlReaderSettings())
            xmlFileSc = XmlReader.Create("C:\Xml2\" & stockCode & "_" & stockName & ".xsd", New XmlReaderSettings())

            _dt.ReadXmlSchema(xmlFileSc)
            _dt.ReadXml(xmlFile)


            _lastDate = _dt.Rows(0).Item("일자")
            GetOpt10081(stockCode)
        Else

            _lastDate = ""

            GetOpt10081(stockCode)
        End If

    End Sub

#Region " CallOpt10059(종목별투자자기관별요청) "
    Public Event OnEventCompleteCallOpt10059(ByVal dt As DataTable)

    Private _dtOpt10059 As New DataTable
    Private _stockCode_Opt10059 As String = ""

    Private Sub CallOpt10059(ByVal startDate As String)
        _MainStock.Opt10059_OnReceiveTrData(startDate, Trim(_stockCode_Opt10059), _MainStock.GetStockInfo(_stockCode_Opt10059), "2", "0", "1")
    End Sub

    Public Sub GetOpt10059(ByVal stockCode As String)
        If stockCode = "" Then Exit Sub
        _stockCode_Opt10059 = stockCode

        If _dtOpt10059 Is Nothing = False Then
            _dtOpt10059.Clear()
            _dtOpt10059.Reset()
        End If

        Dim sysDate As String = CDateTime.FormatDate(Now.Date)

        CallOpt10059(sysDate)

    End Sub

    Private Sub OnReceiveTrData_opt10059(ByVal ds As DataSet)
        Try

            If ds Is Nothing = True Then
                ' RaiseEvent OnEventCompleteCallOpt10059(_dtOpt10059)
                System.Threading.Thread.Sleep(3000)
                combineOpt5981()
                Exit Sub
            End If

            If ds.Tables(0).Rows.Count < 1 Then
                'RaiseEvent OnEventCompleteCallOpt10059(_dtOpt10059)
                System.Threading.Thread.Sleep(3000)
                combineOpt5981()
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

                    _dtOpt10059.PrimaryKey = New DataColumn() {_dtOpt10059.Columns("일자")}

                End If

                For Each dr In ds.Tables(0).Rows
                    ' 버그 때문에 추가(EX. 2016년 08월 11일자로 조회 했는데 2017년 자료가 다시 날라오는 경우가 있음)
                    If .Rows.Count > 0 Then
                        If .Rows(.Rows.Count - 1).Item("일자") <= dr("일자") Then Continue For
                    End If

                    If _lastDate <> "" Then
                        If dr("일자") <= _lastDate Then
                            Continue For
                        End If
                    End If

                    dr2th = .NewRow
                    'dr2th = .Rows.Add

                    For i As Integer = 0 To ds.Tables(0).Columns.Count - 1
                        dr2th(ds.Tables(0).Columns(i).ColumnName) = dr(ds.Tables(0).Columns(i).ColumnName)
                    Next

                    .Rows.Add(dr2th)

                Next

                nextDate = CDateTime.FormatDate(DateAdd(DateInterval.Day, -1, CDate(CDateTime.FormatDate(ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item("일자"), "."))))

            End With

            If _lastDate <> "" Then
                If nextDate <= _lastDate Then
                    If _stockCode_Opt10059 <> _stockCode_Opt10059Price Then
                        '  RaiseEvent OnEventCompleteCallOpt10059(_dtOpt10059)
                        System.Threading.Thread.Sleep(3000)
                        combineOpt5981()
                        Exit Sub
                    Else
                        System.Threading.Thread.Sleep(3000)
                        combineOpt5981()
                        Exit Sub
                    End If

                End If
            End If

            System.Threading.Thread.Sleep(SLEEP_TIME)
            CallOpt10059(nextDate)

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub
#End Region

#Region " CallOpt10059(종목별투자자기관별요청) 금액 "
    Public Event OnEventCompleteCallOpt10059Price(ByVal dt As DataTable)

    Private _dtOpt10059Price As New DataTable
    Private _stockCode_Opt10059Price As String = ""

    Private Sub CallOpt10059Price(ByVal startDate As String)
        _MainStock.Opt10059_OnReceiveTrDataPrice(startDate, Trim(_stockCode_Opt10059Price), _MainStock.GetStockInfo(_stockCode_Opt10059Price), "1", "0", "1")
    End Sub

    Public Sub GetOpt10059Price(ByVal stockCode As String)
        If stockCode = "" Then Exit Sub
        _stockCode_Opt10059Price = stockCode

        If _dtOpt10059Price Is Nothing = False Then
            _dtOpt10059Price.Clear()
            _dtOpt10059Price.Reset()
        End If

        Dim sysDate As String = CDateTime.FormatDate(Now.Date)

        CallOpt10059Price(sysDate)

    End Sub

    Private Sub OnReceiveTrData_opt10059Price(ByVal ds As DataSet)
        Try

            If ds Is Nothing = True Then
                'RaiseEvent OnEventCompleteCallOpt10059Price(_dtOpt10059Price)
                GetOpt10059(_stockCode_Opt10081)
                Exit Sub
            End If

            If ds.Tables(0).Rows.Count < 1 Then
                '   RaiseEvent OnEventCompleteCallOpt10059Price(_dtOpt10059Price)
                GetOpt10059(_stockCode_Opt10081)
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

                    _dtOpt10059Price.PrimaryKey = New DataColumn() {_dtOpt10059Price.Columns("일자")}

                    '.Columns.Add("총금액합", System.Type.GetType("System.Int62"))
                    '.Columns.Add("유통금액량", System.Type.GetType("System.Int62"))

                    'For i As Integer = 0 To UBound(modDataTable.opt10059_Cal)
                    '    .Columns.Add(modDataTable.opt10059_Cal(i) & "금액합", System.Type.GetType("System.Int62"))
                    'Next

                End If

                For Each dr In ds.Tables(0).Rows
                    ' 버그 때문에 추가(EX. 2016년 08월 11일자로 조회 했는데 2017년 자료가 다시 날라오는 경우가 있음)
                    If .Rows.Count > 0 Then
                        If .Rows(.Rows.Count - 1).Item("일자") <= dr("일자") Then Continue For
                    End If

                    If _lastDate <> "" Then
                        If dr("일자") <= _lastDate Then
                            Continue For
                        End If
                    End If

                    dr2th = .NewRow
                    'dr2th = .Rows.Add

                    For i As Integer = 0 To ds.Tables(0).Columns.Count - 1
                        dr2th(ds.Tables(0).Columns(i).ColumnName) = dr(ds.Tables(0).Columns(i).ColumnName)
                    Next

                    .Rows.Add(dr2th)

                Next

                nextDate = CDateTime.FormatDate(DateAdd(DateInterval.Day, -1, CDate(CDateTime.FormatDate(ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item("일자"), "."))))

            End With

            If _lastDate <> "" Then
                If nextDate <= _lastDate Then
                    GetOpt10059(_stockCode_Opt10081)
                    Exit Sub
                End If
            End If

            System.Threading.Thread.Sleep(SLEEP_TIME)
            '  System.Threading.Thread.Sleep(200)

            CallOpt10059Price(nextDate)

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
#End Region

#Region " CallOpt10081(주식일봉차트조회요청) "
    Public Event OnEventCompleteCallOpt10081(ByVal dt As DataTable)

    Private _dtOpt10081 As New DataTable
    Private _stockCode_Opt10081 As String = ""

    Private Sub CallOpt10081(ByVal stockCode As String, ByVal stdDate As String)
        _MainStock.Opt10081New_OnReceiveTrData(stockCode, _MainStock.GetStockInfo(stockCode), stdDate)
    End Sub

    Public Sub GetOpt10081(ByVal stockCode As String)
        If stockCode = "" Then Exit Sub

        Dim stdDate As String = ""

        If _dtOpt10081 Is Nothing = False Then
            _dtOpt10081.Clear()
            _dtOpt10081.Reset()
        End If

        stdDate = CDateTime.FormatDate(DateAdd(DateInterval.Day, -1, Now.Date))

        If CDateTime.WeekDayNames(stdDate) = "일요일" Then
            stdDate = CDateTime.FormatDate(DateAdd("d", -2, CDateTime.FormatDate(Now.Date, "-")), "")
        ElseIf CDateTime.WeekDayNames(stdDate) = "토요일" Then
            stdDate = CDateTime.FormatDate(DateAdd("d", -1, CDateTime.FormatDate(Now.Date, "-")), "")
        End If

        _stockCode_Opt10081 = stockCode

        CallOpt10081(_stockCode_Opt10081, stdDate)

    End Sub

    Private Sub OnReceiveTrData_opt10081(ByVal ds As DataSet)
        Try

            If ds Is Nothing = True Then
                'RaiseEvent OnEventCompleteCallOpt10081(_dtOpt10081)
                GetOpt10059Price(_stockCode_Opt10081)
                Exit Sub
            End If

            If ds.Tables(0).Rows.Count < 1 Then
                'RaiseEvent OnEventCompleteCallOpt10081(_dtOpt10081)
                GetOpt10059Price(_stockCode_Opt10081)
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

                    _dtOpt10081.PrimaryKey = New DataColumn() {_dtOpt10081.Columns("일자")}

                End If

                For Each dr In ds.Tables(0).Rows
                    If .Rows.Count > 0 Then
                        If .Rows(.Rows.Count - 1).Item("일자") <= dr("일자") Then Continue For
                    End If

                    If _lastDate <> "" Then
                        If dr("일자") <= _lastDate Then
                            Continue For
                        End If
                    End If

                    dr2th = .NewRow

                    'dr2th = .Rows.Add

                    For i As Integer = 0 To ds.Tables(0).Columns.Count - 1
                        dr2th(ds.Tables(0).Columns(i).ColumnName) = dr(ds.Tables(0).Columns(i).ColumnName)
                    Next

                    .Rows.Add(dr2th)

                Next

                Dim stdDate As String = ""

                stdDate = CDateTime.FormatDate(DateAdd("d", -1, CDateTime.FormatDate(Now.Date, "-")), "")

                nextDate = CDateTime.FormatDate(DateAdd(DateInterval.Day, -1, CDate(CDateTime.FormatDate(ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item("일자"), "."))))

                If _lastDate <> "" Then
                    If nextDate <= _lastDate Then
                        'RaiseEvent OnEventCompleteCallOpt10081(_dtOpt10081)
                        GetOpt10059Price(_stockCode_Opt10081)
                        Exit Sub
                    End If
                End If

                System.Threading.Thread.Sleep(SLEEP_TIME)

                CallOpt10081(_stockCode_Opt10081, nextDate)

            End With
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub
#End Region

#Region " CombineOpt5981 "
    Public Event OnEventCombineOpt5981(ByVal dt As DataTable)

    Private Sub combineOpt5981()
        Try
            RaiseEvent OnEventCombineMsg(_stockCode_Opt10081 & "의 Combine 작업중...")

            With _dtOpt10081

                '.Columns.Add("총합", System.Type.GetType("System.Int32"))
                '.Columns.Add("총금액합", System.Type.GetType("System.Int64"))
                .Columns.Add("유통량", System.Type.GetType("System.Int32"))
                .Columns.Add("유통금액량", System.Type.GetType("System.Int64"))

                For i As Integer = 0 To UBound(modDataTable.opt10059_Cal)
                    .Columns.Add(modDataTable.opt10059_Cal(i), System.Type.GetType("System.Int32"))
                    .Columns.Add(modDataTable.opt10059_Cal(i) & "합", System.Type.GetType("System.Int32"))
                    .Columns.Add(modDataTable.opt10059_Cal(i) & "금액", System.Type.GetType("System.Int32"))
                    .Columns.Add(modDataTable.opt10059_Cal(i) & "금액합", System.Type.GetType("System.Int64"))
                Next

            End With

            If _lastDate > "" Then
                Dim dr2th As DataRow
                Dim rowIndex2 As Integer = 9999999

                For Each dr2th In _dtOpt10081.Select("일자 = " & _lastDate)
                    rowIndex2 = dr2th.Table.Rows.IndexOf(dr2th)
                    Exit For
                Next

                If rowIndex2 <> 9999999 Then
                    With _dtOpt10081
                        For i As Integer = rowIndex2 To .Rows.Count - 1 Step -1
                            .Rows.RemoveAt(rowIndex2)
                        Next
                    End With
                End If

                rowIndex2 = 0

                With _dtOpt10081
                    For i As Integer = 0 To _dt.Rows.Count - 1

                        dr2th = .NewRow

                        For j As Integer = 0 To _dt.Columns.Count - 1
                            dr2th(_dt.Columns(j).ColumnName) = _dt.Rows(i).Item(_dt.Columns(j).ColumnName)
                        Next

                        .Rows.Add(dr2th)

                    Next
                End With

            End If

            Dim strDate As String = ""
            Dim dr As DataRow
            Dim rowIndex As Integer = 0
            Dim ModifyPriceRate As Integer = 0
            Dim giganTotalQty As Integer = 0
            Dim giganTotalPriceQty As Integer = 0
            Dim youtongQty As Integer = 0
            Dim youtongPriceQty As Integer = 0

            For i As Integer = _dtOpt10081.Rows.Count - 1 To 0 Step -1
                strDate = Trim(_dtOpt10081.Rows(i).Item("일자").ToString())

                rowIndex = -1

                ModifyPriceRate = 0
                giganTotalQty = 0
                giganTotalPriceQty = 0
                youtongQty = 0
                youtongPriceQty = 0

                For Each dr In _dtOpt10059.Select("일자 = " & strDate)
                    rowIndex = dr.Table.Rows.IndexOf(dr)
                    Exit For
                Next

                If rowIndex <> -1 Then
                    ' 수정주가 때문
                    If Math.Abs(_dtOpt10081.Rows(i).Item("현재가")) > 0 And Math.Abs(_dtOpt10059.Rows(rowIndex).Item("현재가")) Then
                        If Math.Abs(_dtOpt10081.Rows(i).Item("현재가")) <> Math.Abs(_dtOpt10059.Rows(rowIndex).Item("현재가")) Then
                            ModifyPriceRate = Math.Abs(_dtOpt10081.Rows(i).Item("현재가")) / Math.Abs(_dtOpt10059.Rows(rowIndex).Item("현재가"))
                        End If
                    End If

                    For j As Integer = 0 To UBound(modDataTable.opt10059_GiganCal)
                        If ModifyPriceRate <> 0 Then
                            giganTotalQty = giganTotalQty + _dtOpt10059.Rows(rowIndex).Item(modDataTable.opt10059_GiganCal(j)) * ModifyPriceRate
                        Else
                            giganTotalQty = giganTotalQty + _dtOpt10059.Rows(rowIndex).Item(modDataTable.opt10059_GiganCal(j))
                        End If
                    Next

                    For j As Integer = 0 To UBound(modDataTable.opt10059_Cal)
                        If modDataTable.opt10059_Cal(j) = "기관계" Then
                            _dtOpt10081.Rows(i).Item(modDataTable.opt10059_Cal(j)) = giganTotalQty
                        Else
                            If ModifyPriceRate <> 0 Then
                                _dtOpt10081.Rows(i).Item(modDataTable.opt10059_Cal(j)) = _dtOpt10059.Rows(rowIndex).Item(modDataTable.opt10059_Cal(j)) * ModifyPriceRate
                            Else
                                _dtOpt10081.Rows(i).Item(modDataTable.opt10059_Cal(j)) = _dtOpt10059.Rows(rowIndex).Item(modDataTable.opt10059_Cal(j))
                            End If
                        End If
                    Next

                    If rowIndex + 1 = _dtOpt10059.Rows.Count And _lastDate = "" Then
                        For j As Integer = 0 To UBound(modDataTable.opt10059_Cal)
                            _dtOpt10081.Rows(i).Item(modDataTable.opt10059_Cal(j) & "합") = _dtOpt10059.Rows(rowIndex).Item(modDataTable.opt10059_Cal(j))
                        Next
                    Else
                        For j As Integer = 0 To UBound(modDataTable.opt10059_Cal)
                            _dtOpt10081.Rows(i).Item(modDataTable.opt10059_Cal(j) & "합") = _dtOpt10059.Rows(rowIndex).Item(modDataTable.opt10059_Cal(j)) + _
                                                                                            _dtOpt10081.Rows(i + 1).Item(modDataTable.opt10059_Cal(j) & "합")
                        Next
                    End If


                    For j As Integer = 0 To UBound(modDataTable.opt10059_YouTongCal)
                        If _dtOpt10081.Rows(i).Item(modDataTable.opt10059_YouTongCal(j) & "합") > 0 Then
                            youtongQty = youtongQty + _dtOpt10081.Rows(i).Item(modDataTable.opt10059_YouTongCal(j) & "합")
                        End If
                    Next

                    _dtOpt10081.Rows(i).Item("유통량") = youtongQty

                End If

                rowIndex = -1

                For Each dr In _dtOpt10059Price.Select("일자 = " & strDate)
                    rowIndex = dr.Table.Rows.IndexOf(dr)
                    Exit For
                Next

                If rowIndex <> -1 Then
                    For j As Integer = 0 To UBound(modDataTable.opt10059_GiganCal)
                        giganTotalPriceQty = giganTotalPriceQty + _dtOpt10059Price.Rows(rowIndex).Item(modDataTable.opt10059_GiganCal(j))
                    Next

                    For j As Integer = 0 To UBound(modDataTable.opt10059_Cal)
                        _dtOpt10081.Rows(i).Item(modDataTable.opt10059_Cal(j) & "금액") = _dtOpt10059Price.Rows(rowIndex).Item(modDataTable.opt10059_Cal(j))
                    Next

                    ' 첫번째 날자인경우.
                    If rowIndex + 1 = _dtOpt10059Price.Rows.Count And _lastDate = "" Then
                        For j As Integer = 0 To UBound(modDataTable.opt10059_Cal)
                            _dtOpt10081.Rows(i).Item(modDataTable.opt10059_Cal(j) & "금액합") = _dtOpt10059Price.Rows(rowIndex).Item(modDataTable.opt10059_Cal(j))
                        Next
                    Else
                        For j As Integer = 0 To UBound(modDataTable.opt10059_Cal)
                            _dtOpt10081.Rows(i).Item(modDataTable.opt10059_Cal(j) & "금액합") = _dtOpt10059Price.Rows(rowIndex).Item(modDataTable.opt10059_Cal(j)) + _
                                                                                                _dtOpt10081.Rows(i + 1).Item(modDataTable.opt10059_Cal(j) & "금액합")
                        Next
                    End If

                    For j As Integer = 0 To UBound(modDataTable.opt10059_YouTongCal)
                        If _dtOpt10081.Rows(i).Item(modDataTable.opt10059_YouTongCal(j) & "금액합") > 0 Then
                            youtongPriceQty = youtongPriceQty + _dtOpt10081.Rows(i).Item(modDataTable.opt10059_YouTongCal(j) & "금액합")
                        End If
                    Next
                    _dtOpt10081.Rows(i).Item("유통금액량") = youtongPriceQty
                End If

            Next

            RaiseEvent OnEventCombineMsg(_stockCode_Opt10081 & "의 Combine 작업완료...")
            RaiseEvent OnEventCombineOpt5981(_dtOpt10081)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
#End Region

End Class