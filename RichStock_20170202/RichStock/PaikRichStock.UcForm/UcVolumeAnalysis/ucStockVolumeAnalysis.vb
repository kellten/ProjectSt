﻿Imports PaikRichStock.Common
Imports Microsoft.Office.Interop

Public Class ucStockVolumeAnalysis
    Private _MainStock As New PaikRichStock.Common.ucMainStockVer2
    Private _stockCode As String = ""
    Private _clsCallOpt As New clsCallOpt
    Private _RegisterEvent As Boolean = False

    Private _dtOpt10014 As New DataTable
    Private _dtOpt10059 As New DataTable
    Private _dtOpt10081 As New DataTable
    Private _dtOpt20068 As New DataTable

    Private _dtSaD As New DataTable
    Private _dtAutoCalSaD As New DataTable

#Region " Property "
    Public Property StockCode As String
        Get
            Return _stockCode
        End Get
        Set(value As String)

            _stockCode = value

            lblStockCode.Text = value
            lblStockName.Text = _MainStock.GetStockInfo(value)

            _dtOpt10014.Clear()
            _dtOpt10059.Clear()
            _dtOpt10081.Clear()
            _dtOpt20068.Clear()

            _dtOpt10059.TableName = "OPT10059"

            lblOpt10014.Text = ""
            lblOpt10059.Text = ""
            lblOpt10081.Text = ""
            lblOpt20068.Text = ""

            If _RegisterEvent = False Then
                _RegisterEvent = True
                RegisterCallOptEvent()
                InitdtSaD()
                InitdtAutoCalSaD()
            End If

            CallOpt()

        End Set
    End Property

    Public WriteOnly Property MainStock As PaikRichStock.Common.ucMainStockVer2
        Set(value As PaikRichStock.Common.ucMainStockVer2)
            _MainStock = value
        End Set
    End Property

    Public ReadOnly Property DtOpt10014 As DataTable
        Get
            Return _dtOpt10014
        End Get
    End Property
    Public ReadOnly Property DtOpt10059 As DataTable
        Get
            Return _dtOpt10059
        End Get
    End Property
    Public ReadOnly Property DtOpt10081 As DataTable
        Get
            Return _dtOpt10081
        End Get
    End Property
    Public ReadOnly Property DtOpt20068 As DataTable
        Get
            Return _dtOpt20068
        End Get
    End Property
#End Region
    

    Private Sub RegisterCallOptEvent()
        AddHandler _clsCallOpt.OnEventReturn10014ResultDt, AddressOf OnEventReturn10014ResultDt
        AddHandler _clsCallOpt.OnEventReturn10059ResultDt, AddressOf OnEventReturn10059ResultDt
        AddHandler _clsCallOpt.OnEventReturn10081ResultDt, AddressOf OnEventReturn10081ResultDt
        AddHandler _clsCallOpt.OnEventReturn20068ResultDt, AddressOf OnEventReturn20068ResultDt
    End Sub

    Public Structure DefineSt_Sad
        Public SUM_TITLE As String
        Public SUM_Price As Integer
        Public SUM_VolumeQty As Integer
        Public SUM_GAEIN As Integer
        Public SUM_SERUK As Integer
        Public SUM_FORE As Integer
        Public SUM_GUMWOONG As Integer
        Public SUM_BOHUM As Integer
        Public SUM_TUSIN As Integer
        Public SUM_GITAGUMWOONG As Integer
        Public SUM_BANK As Integer
        Public SUM_YEONGIGUM As Integer
        Public SUM_SAMOFUND As Integer
        Public SUM_NATION As Integer
        Public SUM_IOFOR As Integer

        Public Sub Init()
            SUM_TITLE = ""
            SUM_Price = 0
            SUM_VolumeQty = 0
            SUM_GAEIN = 0
            SUM_SERUK = 0
            SUM_FORE = 0
            SUM_GUMWOONG = 0
            SUM_BOHUM = 0
            SUM_TUSIN = 0
            SUM_GITAGUMWOONG = 0
            SUM_BANK = 0
            SUM_YEONGIGUM = 0
            SUM_SAMOFUND = 0
            SUM_NATION = 0
            SUM_IOFOR = 0
        End Sub
    End Structure

    Private Sub InitdtSaD()
        With _dtSaD.Columns
            .Add("일자")
            .Add("평균단가")
            .Add("거래량")
            .Add("개인")
            .Add("세력합")
            .Add("외국인")
            .Add("금융투자")
            .Add("보험")
            .Add("투신")
            .Add("기타금융")
            .Add("은행")
            .Add("연기금")
            .Add("사모펀드")
            .Add("국가")
            .Add("내외국인")
        End With
    End Sub

    Private Sub InitdtAutoCalSaD()
        With _dtAutoCalSaD.Columns
            .Add("일자")

            .Add("총합")
            .Add("유통량")

            .Add("개인투자자합")
            .Add("개투최고저점")
            .Add("개투매집수량")
            .Add("개투매집고점")
            .Add("개투분산비율")

            .Add("외국인투자자합")
            .Add("외투최고저점")
            .Add("외투매집수량")
            .Add("외투매집고점")
            .Add("외투분산비율")

            .Add("기관계합")
            .Add("기관최고저점")
            .Add("기관매집수량")
            .Add("기관매집고점")
            .Add("기관분산비율")

            .Add("금융투자합")
            .Add("금투최고저점")
            .Add("금투매집수량")
            .Add("금투매집고점")
            .Add("금투분산비율")

            .Add("보험합")
            .Add("보험최고저점")
            .Add("보험매집수량")
            .Add("보험매집고점")
            .Add("보험분산비율")

            .Add("투신합")
            .Add("투신최고저점")
            .Add("투신매집수량")
            .Add("투신매집고점")
            .Add("투신분산비율")

            .Add("기타금융합")
            .Add("기금최고저점")
            .Add("기금매집수량")
            .Add("기금매집고점")
            .Add("기금분산비율")

            .Add("은행합")
            .Add("은행최고저점")
            .Add("은행매집수량")
            .Add("은행매집고점")
            .Add("은행분산비율")

            .Add("연기금등합")
            .Add("연금최고저점")
            .Add("연금매집수량")
            .Add("연금매집고점")
            .Add("연금분산비율")

            .Add("사모펀드합")
            .Add("사모최고저점")
            .Add("사모매집수량")
            .Add("사모매집고점")
            .Add("사모분산비율")

            .Add("국가합")
            .Add("국가최고저점")
            .Add("국가매집수량")
            .Add("국가매집고점")
            .Add("국가분산비율")

            .Add("기타법인합")
            .Add("기법최고저점")
            .Add("기법매집수량")
            .Add("기법매집고점")
            .Add("기법분산비율")

            .Add("내외국인합")
            .Add("내외최고저점")
            .Add("내외매집수량")
            .Add("내외매집고점")
            .Add("내외분산비율")
        End With
    End Sub

    Private Sub CallOpt()
        _clsCallOpt.GetOpt10014(_stockCode)
        System.Threading.Thread.Sleep(500)
        _clsCallOpt.GetOpt10059(_stockCode)
        System.Threading.Thread.Sleep(500)
        _clsCallOpt.GetOpt10081(_stockCode)
        System.Threading.Thread.Sleep(500)
        _clsCallOpt.GetOpt20068(_stockCode)
    End Sub

#Region " 수급분석표 "
    Public Structure CycleCalData
        Public WEEK_STARTDATE_1 As String
        Public WEEK_STARTDATE_2 As String
        Public WEEK_STARTDATE_3 As String
        Public WEEK_STARTDATE_4 As String

        Public MONTH_STARTDATE_1 As String
        Public MONTH_STARTDATE_2 As String
        Public MONTH_STARTDATE_3 As String

        Public BUGI_STARTDATE_1 As String
        Public BUGI_STARTDATE_2 As String
        Public BUGI_STARTDATE_3 As String
        Public BUGI_STARTDATE_4 As String

        Public Sub Init()
            WEEK_STARTDATE_1 = ""
            WEEK_STARTDATE_2 = ""
            WEEK_STARTDATE_3 = ""
            WEEK_STARTDATE_4 = ""

            MONTH_STARTDATE_1 = ""
            MONTH_STARTDATE_2 = ""
            MONTH_STARTDATE_3 = ""

            BUGI_STARTDATE_1 = ""
            BUGI_STARTDATE_2 = ""
            BUGI_STARTDATE_3 = ""
            BUGI_STARTDATE_4 = ""
        End Sub
    End Structure

    Private _CycleCalData As CycleCalData

    Private Sub SupplyAndDemandAnalysis()
        Dim row As Integer = 1
        Dim firstDate As String = _dtOpt10059.Rows(0).Item("일자")
        Dim firstWeekName As String = CDateTime.WeekDayNames(_dtOpt10059.Rows(0).Item("일자"), True)
        Dim firstBugi As String = ""

        _dtSaD.Rows.Clear()
        dgvVolumeB.DataSource = Nothing

        _sd5Avg.Init()
        _sd10Avg.Init()
        _sd20Avg.Init()
        _sd60Avg.Init()
        _sd120Avg.Init()
        _sum_CurrentWeek.Init()
        _sum_CurrentWeek.SUM_TITLE = "금주"
        _sum_Week1.Init()
        _sum_Week1.SUM_TITLE = "1주"
        _sum_Week2.Init()
        _sum_Week2.SUM_TITLE = "2주"
        _sum_Week3.Init()
        _sum_Week3.SUM_TITLE = "3주"
        _sum_Week4.Init()
        _sum_Week4.SUM_TITLE = "4주"

        _sum_CurrentMonth.Init()
        _sum_CurrentMonth.SUM_TITLE = "이번달"
        _sum_Month1.Init()
        _sum_Month1.SUM_TITLE = "1달"
        _sum_Month2.Init()
        _sum_Month2.SUM_TITLE = "2달"
        _sum_Month3.Init()
        _sum_Month3.SUM_TITLE = "3달"

        _sum_CurrentBungi.Init()
        _sum_CurrentBungi.SUM_TITLE = "이번분기"
        _sum_Bungi1.Init()
        _sum_Bungi1.SUM_TITLE = "1분기"
        _sum_Bungi2.Init()
        _sum_Bungi2.SUM_TITLE = "2분기"
        _sum_Bungi3.Init()
        _sum_Bungi3.SUM_TITLE = "3분기"
        _sum_Bungi4.Init()
        _sum_Bungi4.SUM_TITLE = "4분기"

        _CycleCalData.Init()

        _sdYearAvg = Nothing

        Dim intWeekAdd As Integer = 0

        With _CycleCalData
            Select Case firstWeekName
                Case "월"
                    intWeekAdd = -7
                Case "화"
                    intWeekAdd = -8
                Case "수"
                    intWeekAdd = -9
                Case "목"
                    intWeekAdd = -10
                Case "금"
                    intWeekAdd = -11
            End Select

            .WEEK_STARTDATE_1 = CDateTime.FormatDate(DateAdd(DateInterval.Day, intWeekAdd, CDate(CDateTime.FormatDate(firstDate, "-"))))
            .WEEK_STARTDATE_2 = CDateTime.FormatDate(DateAdd(DateInterval.Day, -7, CDate(CDateTime.FormatDate(.WEEK_STARTDATE_1, "-"))))
            .WEEK_STARTDATE_3 = CDateTime.FormatDate(DateAdd(DateInterval.Day, -7, CDate(CDateTime.FormatDate(.WEEK_STARTDATE_2, "-"))))
            .WEEK_STARTDATE_4 = CDateTime.FormatDate(DateAdd(DateInterval.Day, -7, CDate(CDateTime.FormatDate(.WEEK_STARTDATE_3, "-"))))

            .MONTH_STARTDATE_1 = Mid(CDateTime.FormatDate(DateAdd(DateInterval.Month, -1, CDate(CDateTime.FormatDate(firstDate, "-")))), 1, 6) & "01"
            .MONTH_STARTDATE_2 = Mid(CDateTime.FormatDate(DateAdd(DateInterval.Month, -2, CDate(CDateTime.FormatDate(firstDate, "-")))), 1, 6) & "01"
            .MONTH_STARTDATE_3 = Mid(CDateTime.FormatDate(DateAdd(DateInterval.Month, -3, CDate(CDateTime.FormatDate(firstDate, "-")))), 1, 6) & "01"

            Select Case Mid(firstDate, 5, 2)
                Case "01", "02", "03"
                    firstBugi = Mid(firstDate, 1, 4) & "0101"
                Case "04", "05", "06"
                    firstBugi = Mid(firstDate, 1, 4) & "0401"
                Case "07", "08", "09"
                    firstBugi = Mid(firstDate, 1, 4) & "0701"
                Case "10", "11", "12"
                    firstBugi = Mid(firstDate, 1, 4) & "1001"
            End Select

            .BUGI_STARTDATE_1 = Mid(CDateTime.FormatDate(DateAdd(DateInterval.Month, -3, CDate(CDateTime.FormatDate(firstBugi, "-")))), 1, 6) & "01"
            .BUGI_STARTDATE_2 = Mid(CDateTime.FormatDate(DateAdd(DateInterval.Month, -6, CDate(CDateTime.FormatDate(firstBugi, "-")))), 1, 6) & "01"
            .BUGI_STARTDATE_3 = Mid(CDateTime.FormatDate(DateAdd(DateInterval.Month, -9, CDate(CDateTime.FormatDate(firstBugi, "-")))), 1, 6) & "01"
            .BUGI_STARTDATE_4 = Mid(CDateTime.FormatDate(DateAdd(DateInterval.Month, -12, CDate(CDateTime.FormatDate(firstBugi, "-")))), 1, 6) & "01"

        End With
        

        For Each dr As DataRow In _dtOpt10059.Rows
            Select Case row
                Case Is < 6
                    SDAvgAnalysis(_sd5Avg, dr)
                Case Is < 11
                    SDAvgAnalysis(_sd10Avg, dr)
                Case Is < 21
                    SDAvgAnalysis(_sd20Avg, dr)
                Case Is < 61
                    SDAvgAnalysis(_sd60Avg, dr)
                Case Is < 121
                    SDAvgAnalysis(_sd120Avg, dr)
            End Select

            With _CycleCalData
                ' 2. 이번주, 1주, 2주, 3주, 4주
                If dr("일자") >= CDateTime.FormatDate(DateAdd(DateInterval.Day, 6, CDate(CDateTime.FormatDate(.WEEK_STARTDATE_1, "-")))) Then
                    SDAvgAnalysis(_sum_CurrentWeek, dr)
                End If
                If dr("일자") >= .WEEK_STARTDATE_1 And dr("일자") <= CDateTime.FormatDate(DateAdd(DateInterval.Day, 5, CDate(CDateTime.FormatDate(.WEEK_STARTDATE_1, "-")))) Then
                    SDAvgAnalysis(_sum_Week1, dr)
                End If
                If dr("일자") >= .WEEK_STARTDATE_2 And dr("일자") <= CDateTime.FormatDate(DateAdd(DateInterval.Day, 5, CDate(CDateTime.FormatDate(.WEEK_STARTDATE_2, "-")))) Then
                    SDAvgAnalysis(_sum_Week2, dr)
                End If
                If dr("일자") >= .WEEK_STARTDATE_3 And dr("일자") <= CDateTime.FormatDate(DateAdd(DateInterval.Day, 5, CDate(CDateTime.FormatDate(.WEEK_STARTDATE_3, "-")))) Then
                    SDAvgAnalysis(_sum_Week3, dr)
                End If
                If dr("일자") >= .WEEK_STARTDATE_4 And dr("일자") <= CDateTime.FormatDate(DateAdd(DateInterval.Day, 5, CDate(CDateTime.FormatDate(.WEEK_STARTDATE_4, "-")))) Then
                    SDAvgAnalysis(_sum_Week4, dr)
                End If
                ' 3. 이달 ,1달, 2달, 3달
                Select Case Mid(dr("일자"), 1, 6)
                    Case Mid(firstDate, 1, 6)
                        SDAvgAnalysis(_sum_CurrentMonth, dr)
                    Case Mid(.MONTH_STARTDATE_1, 1, 6)
                        SDAvgAnalysis(_sum_Month1, dr)
                    Case Mid(.MONTH_STARTDATE_2, 1, 6)
                        SDAvgAnalysis(_sum_Month2, dr)
                    Case Mid(.MONTH_STARTDATE_3, 1, 6)
                        SDAvgAnalysis(_sum_Month3, dr)
                End Select
                ' 4. 이번분기, 1분기, 2분기, 3분기, 4분기
                If dr("일자") > Mid(CDateTime.FormatDate(DateAdd(DateInterval.Month, 2, CDate(CDateTime.FormatDate(.BUGI_STARTDATE_1, "-")))), 1, 6) & "31" Then
                    SDAvgAnalysis(_sum_CurrentBungi, dr)
                End If

                If dr("일자") >= .BUGI_STARTDATE_1 And _
                   dr("일자") <= Mid(CDateTime.FormatDate(DateAdd(DateInterval.Month, 2, CDate(CDateTime.FormatDate(.BUGI_STARTDATE_1, "-")))), 1, 6) & "31" Then
                    SDAvgAnalysis(_sum_Bungi1, dr)
                End If

                If dr("일자") >= .BUGI_STARTDATE_2 And _
                   dr("일자") <= Mid(CDateTime.FormatDate(DateAdd(DateInterval.Month, 2, CDate(CDateTime.FormatDate(.BUGI_STARTDATE_2, "-")))), 1, 6) & "31" Then
                    SDAvgAnalysis(_sum_Bungi2, dr)
                End If

                If dr("일자") >= .BUGI_STARTDATE_3 And _
                   dr("일자") <= Mid(CDateTime.FormatDate(DateAdd(DateInterval.Month, 2, CDate(CDateTime.FormatDate(.BUGI_STARTDATE_3, "-")))), 1, 6) & "31" Then
                    SDAvgAnalysis(_sum_Bungi3, dr)
                End If

                If dr("일자") >= .BUGI_STARTDATE_4 And _
                   dr("일자") <= Mid(CDateTime.FormatDate(DateAdd(DateInterval.Month, 2, CDate(CDateTime.FormatDate(.BUGI_STARTDATE_4, "-")))), 1, 6) & "31" Then
                    SDAvgAnalysis(_sum_Bungi4, dr)
                End If

            End With

            ' 5. 1년, 2년, 3년, 4년.....
            SDAvgAnalysisYear(dr)

            row = row + 1
        Next

        DtSadInsertRow(_sum_CurrentWeek)
        DtSadInsertRow(_sum_Week1)
        DtSadInsertRow(_sum_Week2)
        DtSadInsertRow(_sum_Week3)
        DtSadInsertRow(_sum_Week4)

        DtSadInsertRow(_sum_CurrentMonth)
        DtSadInsertRow(_sum_Month1)
        DtSadInsertRow(_sum_Month2)
        DtSadInsertRow(_sum_Month3)

        DtSadInsertRow(_sum_CurrentBungi)
        DtSadInsertRow(_sum_Bungi1)
        DtSadInsertRow(_sum_Bungi2)
        DtSadInsertRow(_sum_Bungi3)
        DtSadInsertRow(_sum_Bungi4)

        If _sdYearAvg Is Nothing = False Then
            For i As Integer = 0 To UBound(_sdYearAvg)
                DtSadInsertRow(_sdYearAvg(i))
            Next
        End If

        CalMainSeRuk()

        dgvVolumeB.DataSource = _dtSaD

    End Sub

    Private Sub DtSadInsertRow(ByVal value As DefineSt_Sad)
        Dim dr2th As DataRow
        With _dtSaD
            dr2th = .Rows.Add
            dr2th("일자") = value.SUM_TITLE
            dr2th("평균단가") = value.SUM_Price
            dr2th("거래량") = value.SUM_VolumeQty
            dr2th("개인") = value.SUM_GAEIN
            dr2th("세력합") = value.SUM_FORE + value.SUM_GUMWOONG + value.SUM_BOHUM + value.SUM_TUSIN + value.SUM_GITAGUMWOONG + value.SUM_BANK + value.SUM_YEONGIGUM + _
                               value.SUM_SAMOFUND + value.SUM_NATION
            dr2th("외국인") = value.SUM_FORE
            dr2th("금융투자") = value.SUM_GUMWOONG
            dr2th("보험") = value.SUM_BOHUM
            dr2th("투신") = value.SUM_TUSIN
            dr2th("기타금융") = value.SUM_GITAGUMWOONG
            dr2th("은행") = value.SUM_BANK
            dr2th("연기금") = value.SUM_YEONGIGUM
            dr2th("사모펀드") = value.SUM_SAMOFUND
            dr2th("국가") = value.SUM_NATION
            dr2th("내외국인") = value.SUM_IOFOR
        End With
    End Sub

#Region " 수급 주체 "
    Private Sub CalMainSeRuk()
        With _dtOpt10059
            For i As Integer = _dtOpt10059.Rows.Count - 1 To 0 Step -1
                If _MaxDate = Trim(.Rows(i).Item("일자")) Then
                    _MinPrice = CInt(Replace(Replace(Trim((.Rows(i).Item("현재가"))), "+", ""), "-", ""))
                    _MinDate = Trim(.Rows(i).Item("일자"))
                    _MinRow = i
                ElseIf _MaxDate < Trim(.Rows(i).Item("일자")) Then
                    If _MinPrice > CInt(Replace(Replace(Trim((.Rows(i).Item("현재가"))), "+", ""), "-", "")) Then
                        _MinPrice = CInt(Replace(Replace(Trim((.Rows(i).Item("현재가"))), "+", ""), "-", ""))
                        _MinDate = Trim(.Rows(i).Item("일자"))
                        _MinRow = i
                    End If
                End If
            Next
        End With

        Dim dr2th As DataRow

        With _dtOpt10059.Rows(_MaxRow)
            dr2th = _dtSaD.Rows.Add

            dr2th("일자") = "최고점"
            dr2th("평균단가") = .Item("현재가")
            dr2th("거래량") = ""
            dr2th("개인") = CInt(.Item("개인투자자합"))
            dr2th("외국인") = CInt(.Item("외국인투자자합"))
            dr2th("세력합") = CInt(.Item("기관계합")) + CInt(.Item("외국인투자자합"))
            dr2th("금융투자") = CInt(.Item("금융투자합"))
            dr2th("보험") = CInt(.Item("보험합"))
            dr2th("투신") = CInt(.Item("투신합"))
            dr2th("기타금융") = CInt(.Item("기타금융합"))
            dr2th("은행") = CInt(.Item("은행합"))
            dr2th("연기금") = CInt(.Item("연기금등합"))
            dr2th("사모펀드") = CInt(.Item("사모펀드합"))
            dr2th("국가") = CInt(.Item("국가합"))
            dr2th("내외국인") = CInt(.Item("내외국인합"))
        End With

        With _dtOpt10059.Rows(_MinRow)
            dr2th = _dtSaD.Rows.Add

            dr2th("일자") = "최저점"
            dr2th("평균단가") = .Item("현재가")
            dr2th("거래량") = ""
            dr2th("개인") = CInt(.Item("개인투자자합"))
            dr2th("외국인") = CInt(.Item("외국인투자자합"))
            dr2th("세력합") = CInt(.Item("기관계합")) + CInt(.Item("외국인투자자합"))
            dr2th("금융투자") = CInt(.Item("금융투자합"))
            dr2th("보험") = CInt(.Item("보험합"))
            dr2th("투신") = CInt(.Item("투신합"))
            dr2th("기타금융") = CInt(.Item("기타금융합"))
            dr2th("은행") = CInt(.Item("은행합"))
            dr2th("연기금") = CInt(.Item("연기금등합"))
            dr2th("사모펀드") = CInt(.Item("사모펀드합"))
            dr2th("국가") = CInt(.Item("국가합"))
            dr2th("내외국인") = CInt(.Item("내외국인합"))
        End With

        With _dtOpt10059
            dr2th = _dtSaD.Rows.Add

            dr2th("일자") = "합계"
            dr2th("평균단가") = 0
            dr2th("거래량") = ""
            dr2th("개인") = CInt(.Rows(_MaxRow).Item("개인투자자합")) - CInt(.Rows(_MinRow).Item("개인투자자합"))
            dr2th("외국인") = CInt(.Rows(_MaxRow).Item("외국인투자자합")) - CInt(.Rows(_MinRow).Item("외국인투자자합"))
            dr2th("세력합") = CInt(.Rows(_MaxRow).Item("기관계합")) + CInt(.Rows(_MaxRow).Item("외국인투자자합")) - _
                              (CInt(.Rows(_MinRow).Item("기관계합")) + CInt(.Rows(_MinRow).Item("외국인투자자합")))
            dr2th("금융투자") = CInt(.Rows(_MaxRow).Item("금융투자합")) - CInt(.Rows(_MinRow).Item("금융투자합"))
            dr2th("보험") = CInt(.Rows(_MaxRow).Item("보험합")) - CInt(.Rows(_MinRow).Item("보험합"))
            dr2th("투신") = CInt(.Rows(_MaxRow).Item("투신합")) - CInt(.Rows(_MinRow).Item("투신합"))
            dr2th("기타금융") = CInt(.Rows(_MaxRow).Item("기타금융합")) - CInt(.Rows(_MinRow).Item("기타금융합"))
            dr2th("은행") = CInt(.Rows(_MaxRow).Item("은행합")) - CInt(.Rows(_MinRow).Item("은행합"))
            dr2th("연기금") = CInt(.Rows(_MaxRow).Item("연기금등합")) - CInt(.Rows(_MinRow).Item("연기금등합"))
            dr2th("사모펀드") = CInt(.Rows(_MaxRow).Item("사모펀드합")) - CInt(.Rows(_MinRow).Item("사모펀드합"))
            dr2th("국가") = CInt(.Rows(_MaxRow).Item("국가합")) - CInt(.Rows(_MinRow).Item("국가합"))
            dr2th("내외국인") = CInt(.Rows(_MaxRow).Item("내외국인합")) - CInt(.Rows(_MinRow).Item("내외국인합"))
        End With
        

    End Sub
#End Region

    Private _sd5Avg As DefineSt_Sad
    Private _sd10Avg As DefineSt_Sad
    Private _sd20Avg As DefineSt_Sad
    Private _sd60Avg As DefineSt_Sad
    Private _sd120Avg As DefineSt_Sad

    Private _sum_CurrentWeek As DefineSt_Sad
    Private _sum_Week1 As DefineSt_Sad
    Private _sum_Week2 As DefineSt_Sad
    Private _sum_Week3 As DefineSt_Sad
    Private _sum_Week4 As DefineSt_Sad

    Private _sum_CurrentMonth As DefineSt_Sad
    Private _sum_Month1 As DefineSt_Sad
    Private _sum_Month2 As DefineSt_Sad
    Private _sum_Month3 As DefineSt_Sad

    Private _sum_CurrentBungi As DefineSt_Sad
    Private _sum_Bungi1 As DefineSt_Sad
    Private _sum_Bungi2 As DefineSt_Sad
    Private _sum_Bungi3 As DefineSt_Sad
    Private _sum_Bungi4 As DefineSt_Sad

    Private _sdYearAvg As DefineSt_Sad()

#Region " SupplyAndDemand Avg "
    Private Sub SDAvgAnalysis(ByRef structureVale As DefineSt_Sad, ByVal dr As DataRow)
        With structureVale
            If .SUM_Price = 0 Then
                .SUM_Price = .SUM_Price + Replace((Replace(dr("현재가"), "-", "")), "+", "")
            Else
                .SUM_Price = CInt((.SUM_Price + Replace((Replace(dr("현재가"), "-", "")), "+", "")) / 2)
            End If
            '  .SUM_VolumeQty = .SUM_VolumeQty + dr("거래량")
            .SUM_GAEIN = .SUM_GAEIN + dr("개인투자자")
            .SUM_FORE = .SUM_FORE + dr("외국인투자자")
            .SUM_GUMWOONG = .SUM_GUMWOONG + dr("금융투자")
            .SUM_BOHUM = .SUM_BOHUM + dr("보험")
            .SUM_TUSIN = .SUM_TUSIN + dr("투신")
            .SUM_GITAGUMWOONG = .SUM_GITAGUMWOONG + dr("기타금융")
            .SUM_BANK = .SUM_BANK + dr("은행")
            .SUM_YEONGIGUM = .SUM_YEONGIGUM + dr("연기금등")
            .SUM_SAMOFUND = .SUM_SAMOFUND + dr("사모펀드")
            .SUM_NATION = .SUM_NATION + dr("국가")
            .SUM_IOFOR = .SUM_IOFOR + dr("내외국인")
        End With
    End Sub

    Private Sub SDAvgAnalysisYear(ByVal dr As DataRow)
        Dim blnExist As Boolean = False

        If _sdYearAvg Is Nothing = True Then

            ReDim _sdYearAvg(0)

            _sdYearAvg(0).Init()

            With _sdYearAvg(0)
                .SUM_TITLE = Mid(dr("일자"), 1, 4)
                .SUM_Price = Replace((Replace(dr("현재가"), "-", "")), "+", "")
                '  .SUM_VolumeQty = dr("거래량")
                .SUM_GAEIN = dr("개인투자자")
                .SUM_FORE = dr("외국인투자자")
                .SUM_GUMWOONG = dr("금융투자")
                .SUM_BOHUM = dr("보험")
                .SUM_TUSIN = dr("투신")
                .SUM_GITAGUMWOONG = dr("기타금융")
                .SUM_BANK = dr("은행")
                .SUM_YEONGIGUM = dr("연기금등")
                .SUM_SAMOFUND = dr("사모펀드")
                .SUM_NATION = dr("국가")
                .SUM_IOFOR = dr("내외국인")
            End With

        Else
            For i As Integer = 0 To UBound(_sdYearAvg)
                If _sdYearAvg(i).SUM_TITLE = Mid(dr("일자"), 1, 4) Then
                    blnExist = True
                    With _sdYearAvg(i)
                        .SUM_TITLE = Mid(dr("일자"), 1, 4)
                        .SUM_Price = CInt((.SUM_Price + Replace((Replace(dr("현재가"), "-", "")), "+", "")) / 2)
                        '  .SUM_VolumeQty = .SUM_VolumeQty + dr("거래량")
                        .SUM_GAEIN = .SUM_GAEIN + dr("개인투자자")
                        .SUM_FORE = .SUM_FORE + dr("외국인투자자")
                        .SUM_GUMWOONG = .SUM_GUMWOONG + dr("금융투자")
                        .SUM_BOHUM = .SUM_BOHUM + dr("보험")
                        .SUM_TUSIN = .SUM_TUSIN + dr("투신")
                        .SUM_GITAGUMWOONG = .SUM_GITAGUMWOONG + dr("기타금융")
                        .SUM_BANK = .SUM_BANK + dr("은행")
                        .SUM_YEONGIGUM = .SUM_YEONGIGUM + dr("연기금등")
                        .SUM_SAMOFUND = .SUM_SAMOFUND + dr("사모펀드")
                        .SUM_NATION = .SUM_NATION + dr("국가")
                        .SUM_IOFOR = .SUM_IOFOR + dr("내외국인")
                    End With

                    Exit Sub

                End If
            Next

            If blnExist = False Then
                ReDim Preserve _sdYearAvg(UBound(_sdYearAvg) + 1)
                _sdYearAvg(UBound(_sdYearAvg)).Init()

                With _sdYearAvg(UBound(_sdYearAvg))
                    .SUM_TITLE = Mid(dr("일자"), 1, 4)
                    .SUM_Price = Replace((Replace(dr("현재가"), "-", "")), "+", "")
                    '.SUM_VolumeQty = dr("거래량")
                    .SUM_GAEIN = dr("개인투자자")
                    .SUM_FORE = dr("외국인투자자")
                    .SUM_GUMWOONG = dr("금융투자")
                    .SUM_BOHUM = dr("보험")
                    .SUM_TUSIN = dr("투신")
                    .SUM_GITAGUMWOONG = dr("기타금융")
                    .SUM_BANK = dr("은행")
                    .SUM_YEONGIGUM = dr("연기금등")
                    .SUM_SAMOFUND = dr("사모펀드")
                    .SUM_NATION = dr("국가")
                    .SUM_IOFOR = dr("내외국인")
                End With
            End If
        End If

    End Sub

#End Region

#End Region

#Region " 자동 수급 계산 "
    Private Function MaxCal(ByVal value1 As Integer, ByVal value2 As Integer) As Integer
        If value1 < value2 Then
            Return value2
        Else
            Return value1
        End If
    End Function

    Private Function MinCal(ByVal value1 As Integer, ByVal value2 As Integer) As Integer
        If value1 < value2 Then
            Return value1
        Else
            Return value2
        End If
    End Function

    Private Function CalDistribution(ByVal value As Integer, ByVal minValue As Integer, ByVal MaxValue As Integer) As Integer
        If MaxValue = 0 Then Return 0

        Return CInt((value - minValue) / MaxValue * 100)
    End Function

    Private _MaxPrice As Integer = 0
    Private _MaxDate As Integer = 0
    Private _MaxRow As Integer = 0

    Private _MinPrice As Integer = 0
    Private _MinDate As Integer = 0
    Private _MinRow As Integer = 0

    Private Sub AutoCalSupplyAndDemand()
        Dim dr2th As DataRow

        Dim Max_GAEIN As Integer = 0
        Dim Max_FORE As Integer = 0
        Dim Max_GIGAN As Integer = 0
        Dim Max_GUMWOONG As Integer = 0
        Dim Max_BOHUM As Integer = 0
        Dim Max_TUSIN As Integer = 0
        Dim Max_GITAGUMWOONG As Integer = 0
        Dim Max_BANK As Integer = 0
        Dim Max_YEUNGGIGUM As Integer = 0
        Dim Max_SAMOFUND As Integer = 0
        Dim Max_NATION As Integer = 0
        Dim Max_GITABUBIN As Integer = 0
        Dim Max_IOFOR As Integer = 0

        Dim Min_GAEIN As Integer = 0
        Dim Min_FORE As Integer = 0
        Dim Min_GIGAN As Integer = 0
        Dim Min_GUMWOONG As Integer = 0
        Dim Min_BOHUM As Integer = 0
        Dim Min_TUSIN As Integer = 0
        Dim Min_GITAGUMWOONG As Integer = 0
        Dim Min_BANK As Integer = 0
        Dim Min_YEUNGGIGUM As Integer = 0
        Dim Min_SAMOFUND As Integer = 0
        Dim Min_NATION As Integer = 0
        Dim Min_GITABUBIN As Integer = 0
        Dim Min_IOFOR As Integer = 0

        With _dtAutoCalSaD

            .Rows.Clear()

            For i As Integer = _dtOpt10059.Rows.Count - 1 To 0 Step -1
                With _dtOpt10059.Rows(i)

                    dr2th = _dtAutoCalSaD.Rows.Add

                    If i = _dtOpt10059.Rows.Count - 1 Then
                        Max_GAEIN = CInt(.Item("개인투자자합"))
                        Max_FORE = CInt(.Item("외국인투자자합"))
                        Max_GIGAN = CInt(.Item("기관계합"))
                        Max_GUMWOONG = CInt(.Item("금융투자합"))
                        Max_BOHUM = CInt(.Item("보험합"))
                        Max_TUSIN = CInt(.Item("투신합"))
                        Max_GITAGUMWOONG = CInt(.Item("기타금융합"))
                        Max_BANK = CInt(.Item("은행합"))
                        Max_YEUNGGIGUM = CInt(.Item("연기금등합"))
                        Max_SAMOFUND = CInt(.Item("사모펀드합"))
                        Max_NATION = CInt(.Item("국가합"))
                        Max_GITABUBIN = CInt(.Item("기타법인합"))
                        Max_IOFOR = CInt(.Item("내외국인합"))

                        Min_GAEIN = CInt(.Item("개인투자자합"))
                        Min_FORE = CInt(.Item("외국인투자자합"))
                        Min_GIGAN = CInt(.Item("기관계합"))
                        Min_GUMWOONG = CInt(.Item("금융투자합"))
                        Min_BOHUM = CInt(.Item("보험합"))
                        Min_TUSIN = CInt(.Item("투신합"))
                        Min_GITAGUMWOONG = CInt(.Item("기타금융합"))
                        Min_BANK = CInt(.Item("은행합"))
                        Min_YEUNGGIGUM = CInt(.Item("연기금등합"))
                        Min_SAMOFUND = CInt(.Item("사모펀드합"))
                        Min_NATION = CInt(.Item("국가합"))
                        Min_GITABUBIN = CInt(.Item("기타법인합"))
                        Min_IOFOR = CInt(.Item("내외국인합"))

                        _MaxDate = Trim((.Item("일자")))
                        _MaxPrice = CInt(Replace(Replace(Trim((.Item("현재가"))), "+", ""), "-", ""))
                        _MaxRow = i
                    Else
                        Max_GAEIN = MaxCal(Max_GAEIN, (.Item("개인투자자합")))
                        Max_FORE = MaxCal(Max_FORE, (.Item("외국인투자자합")))
                        Max_GIGAN = MaxCal(Max_GIGAN, (.Item("기관계합")))
                        Max_GUMWOONG = MaxCal(Max_GUMWOONG, (.Item("금융투자합")))
                        Max_BOHUM = MaxCal(Max_BOHUM, (.Item("보험합")))
                        Max_TUSIN = MaxCal(Max_TUSIN, (.Item("투신합")))
                        Max_GITAGUMWOONG = MaxCal(Max_GITAGUMWOONG, (.Item("기타금융합")))
                        Max_BANK = MaxCal(Max_BANK, (.Item("은행합")))
                        Max_YEUNGGIGUM = MaxCal(Max_YEUNGGIGUM, (.Item("연기금등합")))
                        Max_SAMOFUND = MaxCal(Max_SAMOFUND, (.Item("사모펀드합")))
                        Max_NATION = MaxCal(Max_NATION, (.Item("국가합")))
                        Max_GITABUBIN = MaxCal(Max_GITABUBIN, (.Item("기타법인합")))
                        Max_IOFOR = MaxCal(Max_IOFOR, (.Item("내외국인합")))

                        Min_GAEIN = MinCal(Min_GAEIN, (.Item("개인투자자합")))
                        Min_FORE = MinCal(Min_FORE, (.Item("외국인투자자합")))
                        Min_GIGAN = MinCal(Min_GIGAN, (.Item("기관계합")))
                        Min_GUMWOONG = MinCal(Min_GUMWOONG, (.Item("금융투자합")))
                        Min_BOHUM = MinCal(Min_BOHUM, (.Item("보험합")))
                        Min_TUSIN = MinCal(Min_TUSIN, (.Item("투신합")))
                        Min_GITAGUMWOONG = MinCal(Min_GITAGUMWOONG, (.Item("기타금융합")))
                        Min_BANK = MinCal(Min_BANK, (.Item("은행합")))
                        Min_YEUNGGIGUM = MinCal(Min_YEUNGGIGUM, (.Item("연기금등합")))
                        Min_SAMOFUND = MinCal(Min_SAMOFUND, (.Item("사모펀드합")))
                        Min_NATION = MinCal(Min_NATION, (.Item("국가합")))
                        Min_GITABUBIN = MinCal(Min_GITABUBIN, (.Item("기타법인합")))
                        Min_IOFOR = MinCal(Min_IOFOR, (.Item("내외국인합")))

                        If _MaxPrice < CInt(Replace(Replace(Trim((.Item("현재가"))), "+", ""), "-", "")) Then
                            _MaxDate = Trim((.Item("일자")))
                            _MaxPrice = CInt(Replace(Replace(Trim((.Item("현재가"))), "+", ""), "-", ""))
                            _MaxRow = i
                        End If

                    End If
                    dr2th("일자") = .Item("일자")
                    dr2th("총합") = CInt(.Item("총합"))
                    dr2th("유통량") = CInt(.Item("유통량"))

                    dr2th("개인투자자합") = CInt(.Item("개인투자자합"))
                    dr2th("개투최고저점") = Min_GAEIN
                    dr2th("개투매집수량") = CInt(.Item("개인투자자합")) - Min_GAEIN
                    dr2th("개투매집고점") = Max_GAEIN
                    dr2th("개투분산비율") = CalDistribution(CInt(.Item("개인투자자합")), Min_GAEIN, Max_GAEIN)

                    dr2th("외국인투자자합") = CInt(.Item("외국인투자자합"))
                    dr2th("외투최고저점") = Min_FORE
                    dr2th("외투매집수량") = CInt(.Item("외국인투자자합")) - Min_FORE
                    dr2th("외투매집고점") = Max_FORE
                    dr2th("외투분산비율") = CalDistribution(CInt(.Item("외국인투자자합")), Min_FORE, Max_FORE)

                    dr2th("기관계합") = CInt(.Item("기관계합"))
                    dr2th("기관최고저점") = Min_GIGAN
                    dr2th("기관매집수량") = CInt(.Item("기관계합")) - Min_GIGAN
                    dr2th("기관매집고점") = Max_GIGAN
                    dr2th("기관분산비율") = CalDistribution(CInt(.Item("기관계합")), Min_GIGAN, Max_GIGAN)

                    dr2th("금융투자합") = CInt(.Item("금융투자합"))
                    dr2th("금투최고저점") = Min_GUMWOONG
                    dr2th("금투매집수량") = CInt(.Item("금융투자합")) - Min_GUMWOONG
                    dr2th("금투매집고점") = Max_GUMWOONG
                    dr2th("금투분산비율") = CalDistribution(CInt(.Item("금융투자합")), Min_GUMWOONG, Max_GUMWOONG)

                    dr2th("보험합") = CInt(.Item("보험합"))
                    dr2th("보험최고저점") = Min_BOHUM
                    dr2th("보험매집수량") = CInt(.Item("보험합")) - Min_BOHUM
                    dr2th("보험매집고점") = Max_BOHUM
                    dr2th("보험분산비율") = CalDistribution(CInt(.Item("보험합")), Min_BOHUM, Max_BOHUM)

                    dr2th("투신합") = CInt(.Item("투신합"))
                    dr2th("투신최고저점") = Min_TUSIN
                    dr2th("투신매집수량") = CInt(.Item("투신합")) - Min_TUSIN
                    dr2th("투신매집고점") = Max_TUSIN
                    dr2th("투신분산비율") = CalDistribution(CInt(.Item("투신합")), Min_TUSIN, Max_TUSIN)

                    dr2th("기타금융합") = CInt(.Item("기타금융합"))
                    dr2th("기금최고저점") = Min_GITAGUMWOONG
                    dr2th("기금매집수량") = CInt(.Item("기타금융합")) - Min_GITAGUMWOONG
                    dr2th("기금매집고점") = Max_GITAGUMWOONG
                    dr2th("기금분산비율") = CalDistribution(CInt(.Item("기타금융합")), Min_GITAGUMWOONG, Max_GITAGUMWOONG)

                    dr2th("은행합") = CInt(.Item("은행합"))
                    dr2th("은행최고저점") = Min_BANK
                    dr2th("은행매집수량") = CInt(.Item("은행합")) - Min_BANK
                    dr2th("은행매집고점") = Max_BANK
                    dr2th("은행분산비율") = CalDistribution(CInt(.Item("은행합")), Min_BANK, Max_BANK)

                    dr2th("연기금등합") = CInt(.Item("연기금등합"))
                    dr2th("연금최고저점") = Min_YEUNGGIGUM
                    dr2th("연금매집수량") = CInt(.Item("연기금등합")) - Min_YEUNGGIGUM
                    dr2th("연금매집고점") = Max_YEUNGGIGUM
                    dr2th("연금분산비율") = CalDistribution(CInt(.Item("연기금등합")), Min_YEUNGGIGUM, Max_YEUNGGIGUM)

                    dr2th("사모펀드합") = CInt(.Item("사모펀드합"))
                    dr2th("사모최고저점") = Min_SAMOFUND
                    dr2th("사모매집수량") = CInt(.Item("사모펀드합")) - Min_SAMOFUND
                    dr2th("사모매집고점") = Max_SAMOFUND
                    dr2th("사모분산비율") = CalDistribution(CInt(.Item("사모펀드합")), Min_SAMOFUND, Max_SAMOFUND)

                    dr2th("국가합") = CInt(.Item("국가합"))
                    dr2th("국가최고저점") = Min_NATION
                    dr2th("국가매집수량") = CInt(.Item("국가합")) - Min_NATION
                    dr2th("국가매집고점") = Max_NATION
                    dr2th("국가분산비율") = CalDistribution(CInt(.Item("국가합")), Min_NATION, Max_NATION)

                    dr2th("기타법인합") = CInt(.Item("기타법인합"))
                    dr2th("기법최고저점") = Min_GITABUBIN
                    dr2th("기법매집수량") = CInt(.Item("기타법인합")) - Min_GITABUBIN
                    dr2th("기법매집고점") = Max_GITABUBIN
                    dr2th("기법분산비율") = CalDistribution(CInt(.Item("기타법인합")), Min_GITABUBIN, Max_GITABUBIN)

                    dr2th("내외국인합") = CInt(.Item("내외국인합"))
                    dr2th("내외최고저점") = Min_IOFOR
                    dr2th("내외매집수량") = CInt(.Item("내외국인합")) - Min_IOFOR
                    dr2th("내외매집고점") = Max_IOFOR
                    dr2th("내외분산비율") = CalDistribution(CInt(.Item("내외국인합")), Min_IOFOR, Max_IOFOR)

                End With
            Next
        End With

        '_dtAutoCalSaD.DefaultView.Sort = "일자 Desc"

        dgvVolumeA.DataSource = Nothing
        dgvVolumeA.DataSource = _dtAutoCalSaD
        dgvVolumeA.Sort(dgvVolumeA.Columns(0), System.ComponentModel.ListSortDirection.Descending)
        SupplyAndDemandAnalysis()

    End Sub
#End Region

#Region " CallOpt_Event"
    Private Sub OnEventReturn10014ResultDt(ByVal dt As DataTable)
        lblOpt10014.Text = "10014"
        _dtOpt10014 = dt
    End Sub
    Private Sub OnEventReturn10059ResultDt(ByVal dt As DataTable, ByVal calOpt10059 As clsCallOpt.CalOpt10059)
        lblOpt10059.Text = "10059"
        _dtOpt10059 = dt
        AutoCalSupplyAndDemand()
    End Sub
    Private Sub OnEventReturn10081ResultDt(ByVal dt As DataTable)
        lblOpt10081.Text = "10081"
        _dtOpt10081 = dt
    End Sub
    Private Sub OnEventReturn20068ResultDt(ByVal dt As DataTable)
        lblOpt20068.Text = "20068"
        _dtOpt20068 = dt
    End Sub
#End Region

    Private Sub btnReCal_Click(sender As Object, e As EventArgs) Handles btnReCal.Click
        SupplyAndDemandAnalysis()
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Dim sysDate As String = CDateTime.FormatDate(Now.Date) & "_" & Replace(Trim(Now.Date.ToShortTimeString), ":", "")
        Dim strFileName As String = "C:\" & lblStockName.Text & "_" & sysDate
       
        SaveFromDataTableToExcel(_dtOpt10059, strFileName)

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sysDate As String = CDateTime.FormatDate(Now.Date) & "_" & Replace(Trim(Now.Date.ToShortTimeString), ":", "")
        Dim strFileName As String = "C:\" & lblStockName.Text & "_" & sysDate

        SaveFromDataTableToExcel(_dtSaD, strFileName)

    End Sub

    Private Sub btnViewGraph_Click(sender As Object, e As EventArgs) Handles btnViewGraph.Click
        UcChartVolumeAnalysisA1.DtOpt10014 = _dtOpt10014
        UcChartVolumeAnalysisA1.DtOpt10059 = _dtOpt10059
        UcChartVolumeAnalysisA1.DtOpt10081 = _dtOpt10081
        UcChartVolumeAnalysisA1.DtOpt20068 = _dtOpt20068
        UcChartVolumeAnalysisA1.DtCycleCalData = _dtOpt10059
    End Sub

#Region " Excel "
    Public Overloads Function SaveFromDataTableToExcel(ByVal dt As DataTable, ByVal frmName As String) As Boolean
        Dim sFileName As String

        Dim dlgSave As New SaveFileDialog

        Dim xlsApp As Excel.Application = New Excel.Application


        dlgSave.Filter = "Excel file (*.xls)|*.xls|All files (*.*)|*.*"

        dlgSave.FileName = frmName

        Dim dr As DialogResult = dlgSave.ShowDialog()

        If dr = DialogResult.OK Then
            sFileName = dlgSave.FileName
        Else
            Return False
        End If


        Try

            'Dim DT As DataTable

            Dim wkbTemp As Excel.Workbook = xlsApp.Workbooks.Add()

            For j As Integer = 1 To dt.Columns.Count

                xlsApp.Cells(1, j) = dt.Columns(j - 1).ColumnName

            Next



            For i As Integer = 1 To dt.DefaultView.Count

                For j As Integer = 1 To dt.Columns.Count

                    xlsApp.Cells(i + 1, j) = dt.DefaultView.Item(i - 1)(j - 1)

                Next

            Next

            ' Cell에 어사인 한 값들을 입력받은 파일명으로 저장합니다.

            wkbTemp.SaveAs(sFileName)

            wkbTemp.Close()

            Return True

        Catch ex As Exception

            MessageBox.Show(ex.ToString(), "엑셀변환실패")

            Return False

        Finally


        End Try
    End Function
    Public Overloads Function SaveFromGridToExcel(ByVal objGrd As DataGrid, ByVal frmName As String) As Boolean

        Dim sFileName As String

        Dim dlgSave As New SaveFileDialog

        Dim xlsApp As Excel.Application = New Excel.Application

        Dim dt As DataTable


        dlgSave.Filter = "Excel file (*.xls)|*.xls|All files (*.*)|*.*"

        dlgSave.FileName = frmName

        Dim dr As DialogResult = dlgSave.ShowDialog()

        If dr = DialogResult.OK Then
            sFileName = dlgSave.FileName
        Else
            Return False
        End If

        If objGrd.TableStyles.Count = 0 Then

            MessageBox.Show("엑셀로 저장할 데이터가 없습니다.", "엑셀변환실패")

            Return False

        End If


        Try

            ' 실제 저장하는 부분입니다.

            dt = CType(objGrd.DataSource, DataTable)


            'Dim DT As DataTable

            Dim wkbTemp As Excel.Workbook = xlsApp.Workbooks.Add()

            For j As Integer = 1 To dt.Columns.Count

                xlsApp.Cells(1, j) = dt.Columns(j - 1).ColumnName

            Next



            For i As Integer = 1 To dt.DefaultView.Count

                For j As Integer = 1 To dt.Columns.Count

                    xlsApp.Cells(i + 1, j) = dt.DefaultView.Item(i - 1)(j - 1)

                Next

            Next



            ' Cell에 어사인 한 값들을 입력받은 파일명으로 저장합니다.

            wkbTemp.SaveAs(sFileName)

            wkbTemp.Close()

            Return True

        Catch ex As Exception

            MessageBox.Show(ex.ToString(), "엑셀변환실패")

            Return False

        Finally



        End Try

    End Function


    Public Overloads Function SaveFromGridToExcelCVS(ByVal objGrd As DataGrid, ByVal frmName As String) As Boolean

        Dim sFileName As String

        Dim dlgSave As New SaveFileDialog

        Dim sw As System.IO.StreamWriter

        Dim dt As DataTable

        Dim strTmp As String



        dlgSave.Filter = "Excel file (*.xls)|*.xls|All files (*.*)|*.*"

        dlgSave.FileName = frmName

        Dim dr As DialogResult = dlgSave.ShowDialog()



        If dr = DialogResult.OK Then

            sFileName = dlgSave.FileName

        Else

            Return False

        End If



        If objGrd.DataSource Is Nothing Then

            MessageBox.Show("엑셀로 저장할 데이터가 없습니다.", "엑셀변환실패")

            Return False

        End If



        If CType(objGrd.DataSource, DataTable).Rows.Count = 0 Then

            MessageBox.Show("엑셀로 저장할 데이터가 없습니다.", "엑셀변환실패")

            Return False

        End If



        sw = New System.IO.StreamWriter(sFileName, False, System.Text.Encoding.GetEncoding(949))



        Try

            ' 실제 저장하는 부분입니다.

            dt = CType(objGrd.DataSource, DataTable)



            strTmp = ""

            For j As Integer = 1 To dt.Columns.Count

                strTmp &= dt.Columns(j - 1).ColumnName & vbTab

            Next

            sw.WriteLine(strTmp)



            For i As Integer = 1 To dt.DefaultView.Count

                strTmp = ""

                For j As Integer = 1 To dt.Columns.Count

                    strTmp &= dt.DefaultView.Item(i - 1)(j - 1).ToString & vbTab

                Next

                sw.WriteLine(strTmp)

            Next

            sw.Close()

            Return True

        Catch ex As Exception

            MessageBox.Show(ex.ToString(), "엑셀변환실패")

            Return False

        Finally



        End Try

    End Function

#End Region

    Private Sub btnToXml_Click(sender As Object, e As EventArgs) Handles btnToXml.Click
        _dtOpt10059.TableName = "OPT10059"
        _dtOpt10059.WriteXml("F:\" & lblStockName.Text & ".xml")


    End Sub
End Class
