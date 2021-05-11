﻿Imports AnalysisSt.KiwoomVB.KiCodeGroup
Imports System.IO

Public Class ucMainStockVer2
    Public _allStockDataset As DataSet      ' 전체 종목
    Public _KospiStockDataset As DataSet    ' 코스피 종목
    Public _KosDakStockDataset As DataSet   ' 코스닥 종목

    Private _clsKiwoomBaseInfo As New clsKiwoomBaseInfo
    Private _tech_TypicalPrice As New Tech_TypicalPrice

#Region " Logger "

    Private _LoggerStartOption As Boolean = False

    Public Property LoggerStartOption() As Boolean
        Get
            Return _LoggerStartOption
        End Get
        Set(ByVal value As Boolean)
            _LoggerStartOption = value
        End Set
    End Property

    Private Enum LoggerType
        Connect
        RealData
        ScreeNo
        Order
        NotRealData
        OnMsg
    End Enum

    Public _sLogger As New DataTable

    Public Sub InitsLogger()
        With _sLogger.Columns
            .Add("LOGGER_TYPE")
            .Add("MESSAGE")
            .Add("MESSAGE_NAME")
            .Add("STOCK_CODE")
        End With
    End Sub

    Private Sub Logger(ByVal loggerType As LoggerType, ByVal message As String, ByVal messageName As String, ByVal stockCode As String)
        If _LoggerStartOption = False Then
            If _sLogger.Rows.Count > 0 Then
                _sLogger.Rows.Clear()
            End If
            Exit Sub
        End If

        Dim dr As DataRow
        Dim ClickTime As Date
        Dim sysTime As String
        ClickTime = Date.Now
        sysTime = ClickTime.Hour & ":" & ClickTime.Minute & ":" & ClickTime.Second

        ' 나중에 삭제할 구문
        With _sLogger
            dr = .Rows.Add

            dr("LOGGER_TYPE") = loggerType
            dr("MESSAGE") = message
            dr("MESSAGE_NAME") = messageName
            dr("STOCK_CODE") = stockCode

        End With

        Dim sLog As String = ""

        Select Case loggerType
            Case loggerType.Connect
                sLog = "접속정보" & Space(5) & sysTime & Space(5) & message & Space(5) & messageName & Space(5) & "(" & stockCode & ")"

                File_Write(sLog)
            Case loggerType.RealData
                sLog = "실시간" & Space(5) & sysTime & Space(5) & message & Space(5) & messageName & Space(5) & "(" & stockCode & ")"

                File_Write(sLog)
            Case loggerType.ScreeNo
                sLog = "화면번호" & Space(5) & sysTime & Space(5) & message & Space(5) & messageName & Space(5) & "(" & stockCode & ")"

                File_Write(sLog)
            Case loggerType.Order
                sLog = "주문" & Space(5) & sysTime & Space(5) & message & Space(5) & messageName & Space(5) & "(" & stockCode & ")"

                File_Write(sLog)
            Case loggerType.NotRealData
                sLog = "일반" & Space(5) & sysTime & Space(5) & message & Space(5) & messageName & Space(5) & "(" & stockCode & ")"

                File_Write(sLog)

            Case loggerType.OnMsg
                sLog = "OnMsg" & Space(5) & sysTime & Space(5) & message & Space(5) & messageName & Space(5) & "(" & stockCode & ")"

                File_Write(sLog)

        End Select

        lblMsg.Text = sLog

    End Sub

    Public Sub File_Write(ByVal sSet_Log As String)

        Dim FPath As String

        Dim FName As String

        Dim FDate As String

        Dim FsLog As StreamWriter

        FDate = DateTime.Now.ToString

        FDate = FDate.Substring(0, 11)

        FName = "sLog_" + FDate + ".ini"

        FPath = Path.GetFullPath(FName)

        If File.Exists(FPath) = False Then

            FsLog = File.CreateText(FPath)

        Else

            FsLog = File.AppendText(FPath)

        End If

        Log(sSet_Log, FsLog)

        FsLog.Close()

    End Sub

    Public Sub Log(ByVal logMessage As String, ByVal FsLog As TextWriter)

        'FsLog.WriteLine(ControlChars.CrLf & "Log Entry: ") 줄바꿈 문자

        FsLog.WriteLine(logMessage)

        FsLog.Flush()

    End Sub
#End Region

#Region "화면번호 관리 "
    Public _DtScreenNoManage As New DataTable

    Public Enum Enum_ScreenNo
        TR_OPT1
        TR_OPT1_MARKET
        TR_OPT2
        TR_OPTK
        TR_ORDER
        TR_REAL
        TR_CONDITION
        TR_CONDITIONLIST
    End Enum

    Public Sub InitDtScreenNoManage()
        With _DtScreenNoManage.Columns
            .Add("SCREEN_NO")
            .Add("TR_TYPE")
            .Add("TR_NAME")
            .Add("STOCK_CODE")
        End With
    End Sub

    Public Sub DisconnectRealData(ByVal ScreenNo As String)
        Dim dr As DataRow
        Dim blnTrue As Boolean

        For row As Integer = _DtScreenNoManage.Rows.Count - 1 To 0 Step -1
            dr = _DtScreenNoManage.Rows(row)

            If dr("SCREEN_NO").ToString.Trim = ScreenNo Then
                _DtScreenNoManage.Rows.RemoveAt(row)
                blnTrue = True
                Exit For
            End If
        Next

        If blnTrue = True Then
            AxKH.DisconnectRealData(ScreenNo)
            Logger(LoggerType.ScreeNo, "실시간데이터 화면번호 삭제" & "(" & ScreenNo & ")", "DisconnectRealData", "")
        Else
            AxKH.DisconnectRealData(ScreenNo)
            Logger(LoggerType.ScreeNo, "[DataSet에 없음]실시간데이터 화면번호 삭제" & "(" & ScreenNo & ")", "DisconnectRealData", "")
        End If
    End Sub

    Public Sub DisconnectRealDataStockCode(ByVal stockCode As String)
        Dim dr As DataRow
        Dim blnTrue As Boolean
        Dim screenNo As String = ""
        Dim blnValidation As Boolean = False

        For row As Integer = _DtScreenNoManage.Rows.Count - 1 To 0 Step -1

            blnTrue = False
            screenNo = ""
            dr = _DtScreenNoManage.Rows(row)

            If dr("STOCK_CODE").ToString.Trim = stockCode Then
                screenNo = Trim(dr("SCREEN_NO"))
                _DtScreenNoManage.Rows.RemoveAt(row)
                blnTrue = True
                blnValidation = True
            End If

            If blnTrue = True Then
                AxKH.DisconnectRealData(screenNo)
                Logger(LoggerType.ScreeNo, "실시간데이터 화면번호 삭제" & "(" & screenNo & ")", "DisconnectRealDataStockCode", "")
            End If

        Next

        If blnValidation = False Then
            Logger(LoggerType.ScreeNo, "(" & stockCode & ")" & "실시간데이터 해당 종목 삭제 실패" & "(" & screenNo & ")", "DisconnectRealDataStockCode", "")
        End If
    End Sub


    Public Function ScreenNoManage(ByVal enumScreenNo As Enum_ScreenNo, ByVal trName As String, ByVal stockCode As String) As String
        Dim screenNo As String = ""
        Dim dr As DataRow
        Dim drReader As DataRow
        Dim blnExists As Boolean = False

        Try

            If _DtScreenNoManage.Columns.Count = 0 Then
                InitDtScreenNoManage()
            End If

            With _DtScreenNoManage

                If .Rows.Count < 1 Then
                    dr = .Rows.Add

                    dr("SCREEN_NO") = "1001"
                    dr("TR_TYPE") = enumScreenNo
                    dr("TR_NAME") = trName
                    dr("STOCK_CODE") = stockCode
                    screenNo = "1001"
                    screenNo = "1001"
                Else
                    For i As Integer = 1001 To 1200

                        blnExists = False

                        For Each drReader In .Rows
                            If Trim(drReader("SCREEN_NO").ToString()) = i.ToString() Then
                                blnExists = True
                            End If
                        Next

                        If blnExists = False Then

                            dr = .Rows.Add

                            dr("SCREEN_NO") = Trim(i.ToString())
                            dr("TR_TYPE") = enumScreenNo
                            dr("TR_NAME") = trName
                            dr("STOCK_CODE") = stockCode

                            screenNo = Trim(i.ToString())

                            Exit For
                        End If
                    Next

                End If

            End With

            If screenNo = "" Then
                MsgBox("화면번호가 200개가 넘었습니다. 확인하여 주십시요.", MsgBoxStyle.Exclamation)
                Return ""
            End If

            Return screenNo

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
        End Try

        If screenNo = "" Then
            MsgBox("화면번호 획득에 실패했습니다. 확인하여 주십시요.", MsgBoxStyle.Exclamation)
            Return ""
        End If

        Return ""

    End Function

    Private Function AllDeleteScreenNum() As Boolean
        Try
            With _DtScreenNoManage
                If .Rows.Count = 0 Then Return True

                For Each drReader As DataRow In .Rows
                    AxKH.DisconnectRealData(Trim(drReader("SCREEN_NO").ToString()))
                Next

                _DtScreenNoManage.Reset()

                InitDtScreenNoManage()

            End With

            Return True
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
            Return False
        End Try
    End Function

#End Region

#Region "1. 접속"

    Private _loginStatus As LoginStatus = LoginStatus.DisConnect

#Region " 접속Main "
    ''' <summary>
    ''' C:\Kiwoom\KiwoomFlash2\khministarter.exe 를 통해 접속한다.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Connection()
        If _loginStatus = LoginStatus.DisConnect Then
            If _sLogger.Columns.Count = 0 Then
                InitsLogger()
            End If

            AxKH.CommConnect()
            Logger(LoggerType.Connect, "접속요청", "Connection", "")

        Else
            MsgBox("이미 접속 상태입니다.")
            Exit Sub
        End If
    End Sub

    Public Sub DisConnection()
        If _loginStatus = LoginStatus.DisConnect Then
            MsgBox("이미 접속 해제 상태입니다.")
        Else
            AllDeleteScreenNum()
            AxKH.CommTerminate()
            Logger(LoggerType.Connect, "접속해제요청", "DisConnection", "")
            _loginStatus = LoginStatus.DisConnect
            lblLoginStatus.Text = "로그아웃 성공"
        End If
    End Sub

#End Region

#Region " Control Event "
    Private Sub btnDisconnect_Click(sender As Object, e As EventArgs) Handles btnDisconnect.Click
        DisConnection()
    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        Connection()
    End Sub
#End Region

#End Region

#Region "2. Opt함수 "

#Region " 종목코드별 "
    Public Sub Opt10001_OnReceiveTrData(ByVal stockCode As String, ByVal stockName As String)

        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.CommRqData("주식기본정보", "opt10001", "0", "4001")
        Logger(LoggerType.NotRealData, "주식기본정보 송신" & "(" & stockName & ")", "Opt10001_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt10002_OnReceiveTrData(ByVal stockCode As String, ByVal stockName As String)
        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.CommRqData("주식거래원요청", "OPT10002", "0", "4002")
        Logger(LoggerType.NotRealData, "주식거래원요청 송신" & "(" & stockName & ")", "Opt10002_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt10003_OnReceiveTrData(ByVal stockCode As String, ByVal stockName As String)

        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.CommRqData("체결정보요청", "opt10003", "0", "4003")
        Logger(LoggerType.NotRealData, "체결정보요청 송신" & "(" & stockName & ")", "Opt10003_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt10004_OnReceiveTrData(ByVal stockCode As String, ByVal stockName As String)

        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.CommRqData("주식호가요청", "opt10004", "0", "4004")
        Logger(LoggerType.NotRealData, "주식호가요청 송신" & "(" & stockName & ")", "Opt10004_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt10005_OnReceiveTrData(ByVal stockCode As String, ByVal stockName As String)

        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.CommRqData("주식일주월시분요청  ", "opt10005", "0", "4005")
        Logger(LoggerType.NotRealData, "주식일주월시분요청 송신" & "(" & stockName & ")", "Opt10005_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt10006_OnReceiveTrData(ByVal stockCode As String, ByVal stockName As String)

        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.CommRqData("주식시분요청", "Opt10006", "0", "4006")
        Logger(LoggerType.NotRealData, "주식시분요청 송신" & "(" & stockName & ")", "Opt10006_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt10006_OnReceiveTrData_NewsFinder(ByVal stockCode As String, ByVal stockName As String)

        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.CommRqData("주식시분요청|" + stockCode, "Opt10006", "0", "4006")
        Logger(LoggerType.NotRealData, "주식시분요청 송신" & "(" & stockName & ")", "Opt10006_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt10007_OnReceiveTrData(ByVal stockCode As String, ByVal stockName As String)

        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.CommRqData("시세표성정보요청", "Opt10007", 0, "4007")
        Logger(LoggerType.NotRealData, "시세표성정보요청 송신" & "(" & stockName & ")", "Opt10007_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt10008_OnReceiveTrData(ByVal stockCode As String, ByVal stockName As String)

        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.CommRqData("주식외국인요청 ", "Opt10008", "0", "4008")
        Logger(LoggerType.NotRealData, "주식외국인요청 송신" & "(" & stockName & ")", "Opt10008_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt10009_OnReceiveTrData(ByVal stockCode As String, ByVal stockName As String)

        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.CommRqData("주식기관요청", "Opt10009", "0", "4009")
        Logger(LoggerType.NotRealData, "주식기관요청 송신" & "(" & stockName & ")", "Opt10009_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt10010_OnReceiveTrData(ByVal stockCode As String, ByVal stockName As String)

        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.CommRqData("업종프로그램요청", "Opt10010", "0", "4010")
        Logger(LoggerType.NotRealData, "업종프로그램요청 송신" & "(" & stockName & ")", "Opt10010_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt10011_OnReceiveTrData(ByVal stockCode As String, ByVal stockName As String)

        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.CommRqData("투자자정보요청", "Opt10011", "0", "4011")
        Logger(LoggerType.NotRealData, "투자자정보요청 송신" & "(" & stockName & ")", "Opt10011_OnReceiveTrData", stockCode)

    End Sub

    ''' <summary>
    ''' 신용매매동향요청
    ''' </summary>
    ''' <param name="searchGb">1 - 융자 2 - 대주</param>
    ''' <remarks></remarks>
    Public Sub Opt10013_OnReceiveTrData(ByVal stockCode As String, ByVal stockName As String, ByVal stdDate As String, ByVal searchGb As String)

        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.SetInputValue("일자", stdDate)
        AxKH.SetInputValue("조회구분", searchGb)
        AxKH.CommRqData("신용매매동향요청", "Opt10013", "0", "4013")
        Logger(LoggerType.NotRealData, "신용매매동향요청 송신" & "(" & stockName & ")", "Opt10013_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt10013_OnReceiveTrDataNew(ByVal stockCode As String, ByVal stockName As String, ByVal stdDate As String, ByVal searchGb As String)

        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.SetInputValue("일자", stdDate)
        AxKH.SetInputValue("조회구분", searchGb)
        AxKH.CommRqData("신용매매동향요청|" & stockCode, "Opt10013", "0", "4013")
        Logger(LoggerType.NotRealData, "신용매매동향요청 송신" & "(" & stockName & ")", "Opt10013_OnReceiveTrData", stockCode)

    End Sub

    ''' <summary>
    ''' 공매도추이요청
    ''' </summary>
    ''' <param name="timeGb"> 0:시작일, 1:기간</param>
    ''' <remarks></remarks>
    Public Sub Opt10014_OnReceiveTrData(ByVal stockCode As String, ByVal stockName As String, ByVal timeGb As String, ByVal fromDate As String, ByVal toDate As String)

        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.SetInputValue("시간구분", timeGb)
        AxKH.SetInputValue("시작일자", fromDate)
        AxKH.SetInputValue("종료일자", toDate)
        AxKH.CommRqData("공매도추이요청", "Opt10014", "0", "4014")
        Logger(LoggerType.NotRealData, "공매도추이요청 송신" & "(" & stockName & ")", "Opt10014_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt10014_OnReceiveTrDataNew(ByVal stockCode As String, ByVal stockName As String, ByVal timeGb As String, ByVal fromDate As String, ByVal toDate As String)

        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.SetInputValue("시간구분", timeGb)
        AxKH.SetInputValue("시작일자", fromDate)
        AxKH.SetInputValue("종료일자", toDate)
        AxKH.CommRqData("공매도추이요청|" & stockCode, "Opt10014", "0", "4014")
        Logger(LoggerType.NotRealData, "공매도추이요청 송신" & "(" & stockName & ")", "Opt10014_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt10015_OnReceiveTrData(ByVal stockCode As String, ByVal stockName As String, ByVal startDate As String)

        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.SetInputValue("시작일자", startDate)
        AxKH.CommRqData("일별거래상세요청", "Opt10015", "0", "4015")
        Logger(LoggerType.NotRealData, "일별거래상세요청 송신" & "(" & stockName & ")", "Opt10015_OnReceiveTrData", stockCode)

    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="startDate">일자</param>
    ''' <param name="stockCode"></param>
    ''' <param name="stockName"></param>
    ''' <param name="AmountQtyGb">금액수량구분 = 1:금액, 2:수량</param>
    ''' <param name="MaeMaeGb">매매구분 = 0:순매수, 1:매수, 2:매도</param>
    ''' <param name="UnitGb">단위구분 = 1000:천주, 1:단주</param>
    ''' <remarks></remarks>
    Public Sub Opt10059_OnReceiveTrData(ByVal startDate As String, ByVal stockCode As String, ByVal stockName As String, _
                                        ByVal AmountQtyGb As String, ByVal MaeMaeGb As String, ByVal UnitGb As String)


        AxKH.SetInputValue("일자", startDate)
        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.SetInputValue("금액수량구분", AmountQtyGb)
        AxKH.SetInputValue("매매구분", MaeMaeGb)
        AxKH.SetInputValue("단위구분", UnitGb)
        AxKH.CommRqData("종목별투자자기관별요청", "Opt10059", "0", "4059")
        Logger(LoggerType.NotRealData, "종목별투자자기관별요청 송신" & "(" & stockName & ")", "Opt10059_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt10059_OnReceiveTrDataPrice(ByVal startDate As String, ByVal stockCode As String, ByVal stockName As String, _
                                    ByVal AmountQtyGb As String, ByVal MaeMaeGb As String, ByVal UnitGb As String)
        AxKH.SetInputValue("일자", startDate)
        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.SetInputValue("금액수량구분", "1")
        AxKH.SetInputValue("매매구분", MaeMaeGb)
        AxKH.SetInputValue("단위구분", UnitGb)
        AxKH.CommRqData("종목별투자자기관별금액", "Opt10059", "0", "7059")
        Logger(LoggerType.NotRealData, "종목별투자자기관별금액 송신" & "(" & stockName & ")", "Opt10059_OnReceiveTrDataPrice", stockCode)

    End Sub


    Public Sub Opt10059_OnReceiveTrDataPriceNew(ByVal startDate As String, ByVal stockCode As String, ByVal stockName As String, _
                                ByVal AmountQtyGb As String, ByVal MaeMaeGb As String, ByVal UnitGb As String)
        AxKH.SetInputValue("일자", startDate)
        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.SetInputValue("금액수량구분", "1")
        AxKH.SetInputValue("매매구분", MaeMaeGb)
        AxKH.SetInputValue("단위구분", UnitGb)
        AxKH.CommRqData("종목별투자자기관별금액" & stockCode, "Opt10059", "0", "7059")
        Logger(LoggerType.NotRealData, "종목별투자자기관별금액 송신" & "(" & stockName & ")", "Opt10059_OnReceiveTrDataPrice", stockCode)

    End Sub

    Public Sub Opt10060MaeSu_OnReceiveTrData(ByVal startDate As String, ByVal stockCode As String, ByVal stockName As String, _
                                    ByVal AmountQtyGb As String, ByVal MaeMaeGb As String, ByVal UnitGb As String)

        AxKH.SetInputValue("일자", startDate)
        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.SetInputValue("금액수량구분", "2")
        AxKH.SetInputValue("매매구분", "1")
        AxKH.SetInputValue("단위구분", "1")
        AxKH.CommRqData("종목별투자자기관별차트매수요청", "Opt10060", "0", "5060")
        Logger(LoggerType.NotRealData, "종목별투자자기관별차트매수요청 송신" & "(" & stockName & ")", "Opt10060MaeSu_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt10060MaeDo_OnReceiveTrData(ByVal startDate As String, ByVal stockCode As String, ByVal stockName As String, _
                                    ByVal AmountQtyGb As String, ByVal MaeMaeGb As String, ByVal UnitGb As String)

        AxKH.SetInputValue("일자", startDate)
        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.SetInputValue("금액수량구분", "2")
        AxKH.SetInputValue("매매구분", "2")
        AxKH.SetInputValue("단위구분", "1")
        AxKH.CommRqData("종목별투자자기관별차트매도요청", "Opt10060", "0", "6060")
        Logger(LoggerType.NotRealData, "종목별투자자기관별차트요청 송신" & "(" & stockName & ")", "Opt10060Maedo_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt10060PriceMaeSu_OnReceiveTrData(ByVal startDate As String, ByVal stockCode As String, ByVal stockName As String, _
                                    ByVal AmountQtyGb As String, ByVal MaeMaeGb As String, ByVal UnitGb As String)

        AxKH.SetInputValue("일자", startDate)
        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.SetInputValue("금액수량구분", "1")
        AxKH.SetInputValue("매매구분", "1")
        AxKH.SetInputValue("단위구분", "1")
        AxKH.CommRqData("종목별투자자기관별차트금액매수요청", "Opt10060", "0", "7060")
        Logger(LoggerType.NotRealData, "종목별투자자기관별차트요청 송신" & "(" & stockName & ")", "Opt10060PriceMaeSu_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt10060PriceMaedo_OnReceiveTrData(ByVal startDate As String, ByVal stockCode As String, ByVal stockName As String, _
                                    ByVal AmountQtyGb As String, ByVal MaeMaeGb As String, ByVal UnitGb As String)

        AxKH.SetInputValue("일자", startDate)
        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.SetInputValue("금액수량구분", "1")
        AxKH.SetInputValue("매매구분", "2")
        AxKH.SetInputValue("단위구분", "1")
        AxKH.CommRqData("종목별투자자기관별차트금액매도요청", "Opt10060", "0", "8060")
        Logger(LoggerType.NotRealData, "종목별투자자기관별차트요청 송신" & "(" & stockName & ")", "Opt10060PriceMaedo_OnReceiveTrData", stockCode)

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="stockCode"></param>
    ''' <param name="stockName"></param>
    ''' <param name="fromDate"></param>
    ''' <param name="toDate"></param>
    ''' <param name="AmountQtyGb">금액수량구분 = 1:금액, 2:수량</param>
    ''' <param name="MaeMaeGb">매매구분 = 0:순매수, 1:매수, 2:매도</param>
    ''' <param name="UnitGb">단위구분 = 1000:천주, 1:단주</param>
    ''' <remarks></remarks>
    Public Sub Opt10061_OnReceiveTrData(ByVal stockCode As String, ByVal stockName As String, ByVal fromDate As String, ByVal toDate As String, _
                                        ByVal AmountQtyGb As String, ByVal MaeMaeGb As String, ByVal UnitGb As String)

        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.SetInputValue("시작일자", fromDate)
        AxKH.SetInputValue("종료일자", toDate)
        AxKH.SetInputValue("금액수량구분", AmountQtyGb)
        AxKH.SetInputValue("매매구분", MaeMaeGb)
        AxKH.SetInputValue("단위구분", UnitGb)
        AxKH.CommRqData("종목별투자자기관별합계요청", "Opt10061", "0", "4061")
        Logger(LoggerType.NotRealData, "종목별투자자기관별합계요청 송신" & "(" & stockName & ")", "Opt10061_OnReceiveTrData", stockCode)

    End Sub

    ''' <param name="stockCode">종목코드</param>
    ''' <param name="stockName">종목명</param>
    ''' <param name="gb">시장구분</param>
    ''' <param name="qtygb">금액수량구분</param>
    ''' <param name="buysellgb">매매구분</param>
    ''' <remarks></remarks>
    Public Sub Opt10064_OnReceiveTrData(ByVal stockCode As String, ByVal stockName As String, ByVal gb As String, ByVal qtygb As String, _
                                        ByVal buysellgb As String)

        AxKH.SetInputValue("시장구분", gb)
        AxKH.SetInputValue("금액수량구분", qtygb)
        AxKH.SetInputValue("매매구분", buysellgb)
        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.CommRqData("장중투자자별매매차트요청", "Opt10064", "0", "4064")
        Logger(LoggerType.NotRealData, "종목별투자자기관별합계요청 송신" & "(" & stockName & ")", "Opt10061_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt10078_OnReceiveTrData(ByVal ktCode As String, ByVal stockCode As String, ByVal stockName As String, ByVal fromDate As String, ByVal toDate As String)

        ' 회원사코드 = ※ 회원사코드 참고
        AxKH.SetInputValue("회원사코드", ktCode)
        ' 종목코드 = 전문 조회할 종목코드
        AxKH.SetInputValue("종목코드", stockCode)
        ' 시작일자 = YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)
        AxKH.SetInputValue("시작일자", fromDate)
        ' 종료일자 = YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)
        AxKH.SetInputValue("종료일자", toDate)
        AxKH.CommRqData("증권사별종목매매동향요청", "OPT10078", "0", "4078")

        Logger(LoggerType.NotRealData, "증권사별종목매매동향요청 송신" & "(" & stockName & ")", "Opt10078_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt10080_OnReceiveTrData(ByVal stockCode As String, ByVal stockName As String, ByVal stdDate As String)

        AxKH.SetInputValue("종목코드", stockCode)
        '	틱범위 = 1:1분, 3:3분, 5:5분, 10:10분, 15:15분, 30:30분, 45:45분, 60:60분
        AxKH.SetInputValue("틱범위", stdDate)
        ' 0 or 1, 수신데이터 1:유상증자, 2:무상증자, 4:배당락, 8:액면분할, 16:액면병합, 32:기업합병, 64:감자, 256:권리락
        AxKH.SetInputValue("수정주가구분", 1)
        AxKH.CommRqData("주식분봉차트조회요청", "OPT10080", "0", "4080")

        Logger(LoggerType.NotRealData, "주식분봉차트조회요청 송신" & "(" & stockName & ")", "Opt10080_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt10081_OnReceiveTrData(ByVal stockCode As String, ByVal stockName As String, ByVal stdDate As String)

        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.SetInputValue("기준일자", stdDate)
        ' 0 or 1, 수신데이터 1:유상증자, 2:무상증자, 4:배당락, 8:액면분할, 16:액면병합, 32:기업합병, 64:감자, 256:권리락
        AxKH.SetInputValue("수정주가구분", 1)
        AxKH.CommRqData("주식일봉차트조회", "OPT10081", "0", "4081")

        Logger(LoggerType.NotRealData, "주식일봉차트조회 송신" & "(" & stockName & ")", "Opt10081_OnReceiveTrData", stockCode)

    End Sub


    Public Sub Opt10081New_OnReceiveTrData(ByVal stockCode As String, ByVal stockName As String, ByVal stdDate As String)

        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.SetInputValue("기준일자", stdDate)
        ' 0 or 1, 수신데이터 1:유상증자, 2:무상증자, 4:배당락, 8:액면분할, 16:액면병합, 32:기업합병, 64:감자, 256:권리락
        AxKH.SetInputValue("수정주가구분", 1)
        AxKH.CommRqData("주식일봉차트조회(New)", "OPT10081", "0", "7081")

        Logger(LoggerType.NotRealData, "주식일봉차트조회(New) 송신" & "(" & stockName & ")", "Opt10081New_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt10082_OnReceiveTrData(ByVal stockCode As String, ByVal stockName As String, ByVal stdDate As String)

        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.SetInputValue("기준일자", stdDate)
        AxKH.SetInputValue("끝일자", "")
        ' 0 or 1, 수신데이터 1:유상증자, 2:무상증자, 4:배당락, 8:액면분할, 16:액면병합, 32:기업합병, 64:감자, 256:권리락
        AxKH.SetInputValue("수정주가구분", 1)
        AxKH.CommRqData("주식주봉차트조회", "OPT10082", "0", "4082")

        Logger(LoggerType.NotRealData, "주식주봉차트조회 송신" & "(" & stockName & ")", "Opt10082_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt10087_OnReceiveTrData(ByVal stockCode As String, ByVal stockName As String)

        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.CommRqData("시간외단일가요청", "OPT10087", "0", "4087")

        Logger(LoggerType.NotRealData, "시간외단일가요청 송신" & "(" & stockName & ")", "OPT10087_OnReceiveTrData", stockCode)

    End Sub

    Public Sub Opt20068_OnReceiveTrData(ByVal stockCode As String, ByVal stockName As String, ByVal fromDate As String, ByVal toDate As String)

        AxKH.SetInputValue("시작일자", fromDate)
        AxKH.SetInputValue("종료일자", toDate)
        '전체구분 = 0:종목코드 입력종목만 표시, 1: 전체표시(지원안함. OPT10068사용).
        AxKH.SetInputValue("전체구분", "0")
        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.CommRqData("대차거래추이요청(종목별)", "opt20068", "0", "5068")

        Logger(LoggerType.NotRealData, "대차거래추이요청(종목별) 송신" & "(" & stockName & ")", "Opt20068_OnReceiveTrData", stockCode)

    End Sub

    'Public Sub Opt10081_OnReceiveTrDataSeqSearch(ByVal stockCode As String, ByVal stockName As String, ByVal stdDate As String, ByVal seqGb As String)

    '    AxKH.SetInputValue("종목코드", stockCode)
    '    AxKH.SetInputValue("기준일자", stdDate)
    '    ' 0 or 1, 수신데이터 1:유상증자, 2:무상증자, 4:배당락, 8:액면분할, 16:액면병합, 32:기업합병, 64:감자, 256:권리락
    '    AxKH.SetInputValue("수정주가구분", 1)
    '    AxKH.CommRqData("주식일봉차트조회(연속)", "OPT10081", seqGb, "5081")

    '    Logger(LoggerType.NotRealData, "주식일봉차트조회 송신" & "(" & stockName & ")", "Opt10081_OnReceiveTrData", stockCode)

    'End Sub
#End Region

#Region " 계좌 "
    Public Sub Opt10012_OnReceiveChejanData(ByVal accNo As String)
        AxKH.SetInputValue("계좌번호", accNo)
        AxKH.CommRqData("주문체결요청", "Opt10012", "0", "4012")
        Logger(LoggerType.NotRealData, "주문체결요청 송신" & "(" & accNo & ")", "Opt10012_OnReceiveChejanData", accNo)
    End Sub

    Public Sub Opt10075_OnReceiveChejanData(ByVal accNo As String)
        AxKH.SetInputValue("계좌번호", accNo)
        AxKH.SetInputValue("체결구분", "0")
        AxKH.SetInputValue("매매구분", "0")
        AxKH.CommRqData("실시간미체결요청", "opt10075", "0", "4075")
        Logger(LoggerType.NotRealData, "실시간미체결요청 송신" & "(" & accNo & ")", "Opt10075_OnReceiveChejanData", accNo)
    End Sub

    Public Sub Opt10085_OnReceiveChejanData(ByVal accNo As String, ByVal screenNo As String)
        AxKH.SetInputValue("계좌번호", accNo)
        AxKH.CommRqData("계좌수익률요청", "opt10085", "0", "4085")
        Logger(LoggerType.NotRealData, "계좌수익률 송신" & "(" & accNo & ")", "Opt10085_OnReceiveChejanData", accNo)
    End Sub
#End Region

#Region " 시장 정보 "
    ''' <summary>
    ''' 신고저가요청
    ''' </summary>
    ''' <param name="marketGb">시장구분 = 000:전체, 001:코스피, 101:코스닥</param>
    ''' <param name="newHighLowGb">신고저구분 = 1 : 신고가, 2 : 신저가()</param>
    ''' <param name="highLowGb">고저종구분 = 1 : 고저기준, 2 : 종가기준()</param>
    ''' <param name="stockJoguen">종목조건 = 0 : 전체조회, 1 : 관리종목제외, 3 : 우선주제외, 5 : 증100제외, 6 : 증100만보기, 7 : 증40만보기, 8 : 증30만보기()</param>
    ''' <param name="volumeGb">거래량구분 = 00000:전체조회, 00010:만주이상, 00050:5만주이상, 00100:10만주이상, 00150:15만주이상, 00200:20만주이상, 00300:30만주이상, 00500:50만주이상, 01000:백만주이상</param>
    ''' <param name="loanJoguen">신용조건 = 0 : 전체조회, 1 : 신용융자A군, 2 : 신용융자B군, 3 : 신용융자C군, 4 : 신용융자D군, 9 : 신용융자전체()</param>
    ''' <param name="sangHahanIn">상하한포함 = 0 : 미포함, 1 : 포함()</param>
    ''' <param name="stdDate">기간 = 5:5일, 10:10일, 20:20일, 60:60일, 250:250일, 250일까지 입력가능</param>
    ''' <remarks></remarks>
    Public Sub Opt10016_OnReceiveTrData(ByVal marketGb As String, ByVal newHighLowGb As String, ByVal highLowGb As String, ByVal stockJoguen As String, _
                                        ByVal volumeGb As String, ByVal loanJoguen As String, ByVal sangHahanIn As String, ByVal stdDate As String)

        AxKH.SetInputValue("시장구분", marketGb)
        AxKH.SetInputValue("신고저구분", newHighLowGb)
        AxKH.SetInputValue("고저종구분", highLowGb)
        AxKH.SetInputValue("종목조건", stockJoguen)
        AxKH.SetInputValue("거래량구분", volumeGb)
        AxKH.SetInputValue("신용조건", loanJoguen)
        AxKH.SetInputValue("상하한포함", sangHahanIn)
        AxKH.SetInputValue("기간", stdDate)
        AxKH.CommRqData("신고저가요청", "Opt10016", "0", "4016")
        Logger(LoggerType.NotRealData, "신고저가요청 송신" & "(Opt10016)", "Opt10016_OnReceiveTrData", "Opt10016")

    End Sub
    ''' <summary>
    ''' 상하한가요청
    ''' </summary>
    ''' <param name="marketGb">시장구분 = 000:전체, 001:코스피, 101:코스닥</param>
    ''' <param name="sangHahanInGb">상하한구분 = 1:상한, 2:상승, 3:보합, 4: 하한, 5:하락, 6:전일상한, 7:전일하한</param>
    ''' <param name="sortGb">정렬구분 = 1:종목코드순, 2:연속횟수순(상위100개), 3:등락률순</param>
    ''' <param name="stockGb">종목조건 = 0:전체조회,1:관리종목제외, 3:우선주제외, 4:우선주+관리종목제외, 5:증100제외, 6:증100만 보기, 7:증40만 보기, 8:증30만 보기, 9:증20만 보기, 10:우선주+관리종목+환기종목제외</param>
    ''' <param name="volumeGb">거래량구분 = 00000:전체조회, 00010:만주이상, 00050:5만주이상, 00100:10만주이상, 00150:15만주이상, 00200:20만주이상, 00300:30만주이상, 00500:50만주이상, 01000:백만주이상</param>
    ''' <param name="loadGb">신용조건 = 0:전체조회, 1:신용융자A군, 2:신용융자B군, 3:신용융자C군, 4:신용융자D군, 9:신용융자전체</param>
    ''' <param name="maemaegumeGb">매매금구분 = 0:전체조회, 1:1천원미만, 2:1천원~2천원, 3:2천원~3천원, 4:5천원~1만원, 5:1만원이상, 8:1천원이상</param>
    ''' <remarks></remarks>
    Public Sub Opt10017_OnReceiveTrData(ByVal marketGb As String, ByVal sangHahanInGb As String, ByVal sortGb As String, ByVal stockGb As String, _
                                        ByVal volumeGb As String, ByVal loadGb As String, ByVal maemaegumeGb As String)



        AxKH.SetInputValue("시장구분", marketGb)
        AxKH.SetInputValue("상하한구분", sangHahanInGb)
        AxKH.SetInputValue("정렬구분", sortGb)
        AxKH.SetInputValue("종목조건", stockGb)
        AxKH.SetInputValue("거래량구분", volumeGb)
        AxKH.SetInputValue("신용조건", loadGb)
        AxKH.SetInputValue("매매금구분", maemaegumeGb)
        AxKH.CommRqData("상하한가요청", "Opt10017", "0", "4017")
        Logger(LoggerType.NotRealData, "상하한가요청 송신" & "(Opt10017)", "Opt10017_OnReceiveTrData", "Opt10017")

    End Sub
    ''' <summary>
    ''' 고저가근접요청
    ''' </summary>
    ''' <param name="highlowGb">고저구분 = 1:고가, 2:저가</param>
    ''' <param name="approachGb">근접율 = 05:0.5 10:1.0, 15:1.5, 20:2.0. 25:2.5, 30:3.0</param>
    ''' <param name="marketGb">시장구분 = 000:전체, 001:코스피, 101:코스닥</param>
    ''' <param name="volumeGb">거래량구분 = 00000:전체조회, 00010:만주이상, 00050:5만주이상, 00100:10만주이상, 00150:15만주이상, 00200:20만주이상, 00300:30만주이상, 00500:50만주이상, 01000:백만주이상</param>
    ''' <param name="stockJoguen">종목조건 = 0:전체조회,1:관리종목제외, 3:우선주제외, 5:증100제외, 6:증100만보기, 7:증40만보기, 8:증30만보기</param>
    ''' <param name="loanJoguen">신용조건 = 0:전체조회, 1:신용융자A군, 2:신용융자B군, 3:신용융자C군, 4:신용융자D군, 9:신용융자전체</param>
    ''' <remarks></remarks>
    Public Sub Opt10018_OnReceiveTrData(ByVal highlowGb As String, ByVal approachGb As String, ByVal marketGb As String, ByVal volumeGb As String, _
                                        ByVal stockJoguen As String, ByVal loanJoguen As String)


        AxKH.SetInputValue("고저구분", highlowGb)
        AxKH.SetInputValue("근접율", approachGb)
        AxKH.SetInputValue("시장구분", marketGb)
        AxKH.SetInputValue("거래량구분", volumeGb)
        AxKH.SetInputValue("종목조건", "입력값 5")
        AxKH.SetInputValue("신용조건", "입력값 6")
        AxKH.CommRqData("고저가근접요청", "Opt10018", "0", "4018")
        Logger(LoggerType.NotRealData, "고저가근접요청 송신" & "(Opt10018)", "Opt10018_OnReceiveTrData", "Opt10018")

    End Sub
    ''' <summary>
    ''' 가격급등락요청
    ''' </summary>
    ''' <param name="marketGb">시장구분 = 000:전체, 001:코스피, 101:코스닥, 201:코스피200</param>
    ''' <param name="upDownGb">등락구분 = 1:급등, 2:급락</param>
    ''' <param name="timeGb">시간구분 = 1:분전, 2:일전</param>
    ''' <param name="time">시간 = 분 혹은 일입력</param>
    ''' <param name="volumeGb">거래량구분 = 00000:전체조회, 00010:만주이상, 00050:5만주이상, 00100:10만주이상, 00150:15만주이상, 00200:20만주이상, 00300:30만주이상, 00500:50만주이상, 01000:백만주이상</param>
    ''' <param name="stockJoguen">종목조건 = 0:전체조회,1:관리종목제외, 3:우선주제외, 5:증100제외, 6:증100만보기, 7:증40만보기, 8:증30만보기</param>
    ''' <param name="loanJoguen">신용조건 = 0:전체조회, 1:신용융자A군, 2:신용융자B군, 3:신용융자C군, 4:신용융자D군, 9:신용융자전체</param>
    ''' <param name="priceJoguen">가격조건 = 0:전체조회, 1:1천원미만, 2:1천원~2천원, 3:2천원~3천원, 4:5천원~1만원, 5:1만원이상, 8:1천원이상</param>
    ''' <param name="sangHahanIn">상하한포함 = 0:미포함, 1:포함</param>
    ''' <remarks></remarks>
    Public Sub Opt10019_OnReceiveTrData(ByVal marketGb As String, ByVal upDownGb As String, ByVal timeGb As String, ByVal time As String, _
                                        ByVal volumeGb As String, ByVal stockJoguen As String, ByVal loanJoguen As String, ByVal priceJoguen As String, _
                                        ByVal sangHahanIn As String)

        AxKH.SetInputValue("시장구분", marketGb)
        AxKH.SetInputValue("등락구분", upDownGb)
        AxKH.SetInputValue("시간구분", timeGb)
        AxKH.SetInputValue("시간", time)
        AxKH.SetInputValue("거래량구분", volumeGb)
        AxKH.SetInputValue("종목조건", stockJoguen)
        AxKH.SetInputValue("신용조건", loanJoguen)
        AxKH.SetInputValue("가격조건", priceJoguen)
        AxKH.SetInputValue("상하한포함", sangHahanIn)
        AxKH.CommRqData("가격급등락요청", "Opt10019", "0", "4019")
        Logger(LoggerType.NotRealData, "가격급등락요청 송신" & "(Opt10019)", "Opt10019_OnReceiveTrData", "Opt10019")

    End Sub
    ''' <summary>
    ''' 호가잔량상위요청
    ''' </summary>
    ''' <param name="marketGb">시장구분 = 001:코스피, 101:코스닥</param>
    ''' <param name="sortGb">정렬구분 = 1:순매수잔량순, 2:순매도잔량순, 3:매수비율순, 4:매도비율순</param>
    ''' <param name="volumeGb">거래량구분 = 0000:장시작전(0주이상), 0010:만주이상, 0050:5만주이상, 00100:10만주이상</param>
    ''' <param name="stockJoguen">종목조건 = 0:전체조회, 1:관리종목제외, 5:증100제외, 6:증100만보기, 7:증40만보기, 8:증30만보기, 9:증20만보기</param>
    ''' <param name="loanJoguen">신용조건 = 0:전체조회, 1:신용융자A군, 2:신용융자B군, 3:신용융자C군, 4:신용융자D군, 9:신용융자전체</param>
    ''' <remarks></remarks>
    Public Sub Opt10020_OnReceiveTrData(ByVal marketGb As String, ByVal sortGb As String, ByVal volumeGb As String, ByVal stockJoguen As String, ByVal loanJoguen As String)

        AxKH.SetInputValue("시장구분", marketGb)
        AxKH.SetInputValue("정렬구분", sortGb)
        AxKH.SetInputValue("거래량구분", volumeGb)
        AxKH.SetInputValue("종목조건", stockJoguen)
        AxKH.SetInputValue("신용조건", loanJoguen)
        AxKH.CommRqData("호가잔량상위요청", "Opt10020", "0", "4020")
        Logger(LoggerType.NotRealData, "호가잔량상위요청 송신" & "(Opt10020)", "Opt10020_OnReceiveTrData", "Opt10020")

    End Sub
    ''' <summary>
    ''' 호가잔량급증요청
    ''' </summary>
    ''' <param name="marketGb">시장구분 = 001:코스피, 101:코스닥</param>
    ''' <param name="meameaGb">매매구분 = 1:매수잔량, 2:매도잔량</param>
    ''' <param name="sortGb">정렬구분 = 1:급증량, 2:급증률</param>
    ''' <param name="timeGb">시간구분 = 분 입력</param>
    ''' <param name="volumeGb">거래량구분 = 1:천주이상, 5:5천주이상, 10:만주이상, 50:5만주이상, 100:10만주이상</param>
    ''' <param name="stockJoguenGb">종목조건 = 0:전체조회, 1:관리종목제외, 5:증100제외, 6:증100만보기, 7:증40만보기, 8:증30만보기, 9:증20만보기</param>
    ''' <remarks></remarks>
    Public Sub Opt10021_OnReceiveTrData(ByVal marketGb As String, ByVal meameaGb As String, ByVal sortGb As String, ByVal timeGb As String, ByVal volumeGb As String, ByVal stockJoguenGb As String)

        AxKH.SetInputValue("시장구분", marketGb)
        AxKH.SetInputValue("매매구분", meameaGb)
        AxKH.SetInputValue("정렬구분", sortGb)
        AxKH.SetInputValue("시간구분", timeGb)
        AxKH.SetInputValue("거래량구분", volumeGb)
        AxKH.SetInputValue("종목조건", stockJoguenGb)
        AxKH.CommRqData("호가잔량급증요청", "Opt10021", "0", "4021")
        Logger(LoggerType.NotRealData, "호가잔량급증요청 송신" & "(Opt10021)", "Opt10021_OnReceiveTrData", "Opt10021")

    End Sub
    ''' <summary>
    ''' 잔량율급증요청
    ''' </summary>
    ''' <param name="marketGb">시장구분 = 001:코스피, 101:코스닥</param>
    ''' <param name="rateGb">비율구분 = 1:매수/매도비율, 2:매도/매수비율</param>
    ''' <param name="timeGb">시간구분 = 분 입력</param>
    ''' <param name="volumeGb">거래량구분 = 5:5천주이상, 10:만주이상, 50:5만주이상, 100:10만주이상</param>
    ''' <param name="stockJoguenGb">종목조건 = 0:전체조회, 1:관리종목제외, 5:증100제외, 6:증100만보기, 7:증40만보기, 8:증30만보기, 9:증20만보기</param>
    ''' <remarks></remarks>
    Public Sub Opt10022_OnReceiveTrData(ByVal marketGb As String, ByVal rateGb As String, ByVal timeGb As String, ByVal volumeGb As String, ByVal stockJoguenGb As String)

        AxKH.SetInputValue("시장구분", "입력값 1")
        AxKH.SetInputValue("비율구분", "입력값 2")
        AxKH.SetInputValue("시간구분", "입력값 3")
        AxKH.SetInputValue("거래량구분", "입력값 4")
        AxKH.SetInputValue("종목조건", "입력값 5")
        AxKH.CommRqData("잔량율급증요청", "Opt10022", "0", "4022")
        Logger(LoggerType.NotRealData, "잔량율급증요청 송신" & "(Opt10022)", "Opt10022_OnReceiveTrData", "Opt10022")

    End Sub
    ''' <summary>
    ''' 거래량급증요청
    ''' </summary>
    ''' <param name="marketGb">시장구분 = 000:전체, 001:코스피, 101:코스닥</param>
    ''' <param name="sortGb">정렬구분 = 1:급증량, 2:급증률</param>
    ''' <param name="timeGb">시간구분 = 1:분, 2:전일</param>
    ''' <param name="volumeGb">거래량구분 = 5:5천주이상, 10:만주이상, 50:5만주이상, 100:10만주이상, 200:20만주이상, 300:30만주이상, 500:50만주이상, 1000:백만주이상</param>
    ''' <param name="time">시간 = 분 입력</param>
    ''' <param name="stockJoguenGb">종목조건 = 0:전체조회, 1:관리종목제외, 5:증100제외, 6:증100만보기, 7:증40만보기, 8:증30만보기, 9:증20만보기</param>
    ''' <param name="priceGb">가격구분 = 0:전체조회, 2:5만원이상, 5:1만원이상, 6:5천원이상, 8:1천원이상, 9:10만원이상</param>
    ''' <remarks></remarks>
    Public Sub Opt10023_OnReceiveTrData(ByVal marketGb As String, ByVal sortGb As String, ByVal timeGb As String, ByVal volumeGb As String, _
                                        ByVal time As String, ByVal stockJoguenGb As String, ByVal priceGb As String)

        AxKH.SetInputValue("시장구분", marketGb)
        AxKH.SetInputValue("정렬구분", sortGb)
        AxKH.SetInputValue("시간구분", timeGb)
        AxKH.SetInputValue("거래량구분", volumeGb)
        AxKH.SetInputValue("시간", time)
        AxKH.SetInputValue("종목조건", stockJoguenGb)
        AxKH.SetInputValue("가격구분", priceGb)
        AxKH.CommRqData("거래량급증요청", "Opt10023", "0", "4023")
        Logger(LoggerType.NotRealData, "거래량급증요청 송신" & "(Opt10023)", "Opt10023_OnReceiveTrData", "Opt10023")

    End Sub
    ''' <summary>
    ''' 거래량갱신요청
    ''' </summary>
    ''' <param name="marketGb">시장구분 = 000:전체, 001:코스피, 101:코스닥</param>
    ''' <param name="jugiGb">주기구분 = 5:5일, 10:10일, 20:20일, 60:60일, 250:250일</param>
    ''' <param name="volumeGb">거래량구분 = 5:5천주이상, 10:만주이상, 50:5만주이상, 100:10만주이상, 200:20만주이상, 300:30만주이상, 500:50만주이상, 1000:백만주이상</param>
    ''' <remarks></remarks>
    Public Sub Opt10024_OnReceiveTrData(ByVal marketGb As String, ByVal jugiGb As String, ByVal volumeGb As String)


        AxKH.SetInputValue("시장구분", marketGb)
        AxKH.SetInputValue("주기구분", jugiGb)
        AxKH.SetInputValue("거래량구분", volumeGb)
        AxKH.CommRqData("거래량갱신요청", "Opt10024", "0", "4024")
        Logger(LoggerType.NotRealData, "거래량갱신요청 송신" & "(Opt10024)", "Opt10024_OnReceiveTrData", "Opt10024")

    End Sub
    ''' <summary>
    ''' 매물대집중요청
    ''' </summary>
    ''' <param name="marketGb">시장구분 = 000:전체, 001:코스피, 101:코스닥</param>
    ''' <param name="mamulPointRate">매물집중비율 = 0~100 입력</param>
    ''' <param name="currentPriceJinIp">현재가진입 = 0:현재가 매물대 집입 포함안함, 1:현재가 매물대 집입포함</param>
    ''' <param name="mamulDaeSu">매물대수 = 숫자입력</param>
    ''' <param name="jugiGb">주기구분 = 50:50일, 100:100일, 150:150일, 200:200일, 250:250일, 300:300일</param>
    ''' <remarks></remarks>
    Public Sub Opt10025_OnReceiveTrData(ByVal marketGb As String, ByVal mamulPointRate As String, ByVal currentPriceJinIp As String, _
                                        ByVal mamulDaeSu As String, ByVal jugiGb As String)


        AxKH.SetInputValue("시장구분", marketGb)
        AxKH.SetInputValue("매물집중비율", mamulPointRate)
        AxKH.SetInputValue("현재가진입", currentPriceJinIp)
        AxKH.SetInputValue("매물대수", mamulDaeSu)
        AxKH.SetInputValue("주기구분", jugiGb)
        AxKH.CommRqData("매물대집중요청", "Opt10025", "0", "4025")
        Logger(LoggerType.NotRealData, "매물대집중요청 송신" & "(Opt10025)", "Opt10025_OnReceiveTrData", "Opt10025")

    End Sub
    ''' <summary>
    ''' 고저PER요청
    ''' </summary>
    ''' <param name="perGb">고저PER요청</param>
    ''' <remarks></remarks>
    Public Sub Opt10026_OnReceiveTrData(ByVal perGb As String)


        AxKH.SetInputValue("PER구분", perGb)
        AxKH.CommRqData("고저PER요청", "Opt10026", "0", "4026")
        Logger(LoggerType.NotRealData, "고저PER요청 송신" & "(Opt10026)", "Opt10026_OnReceiveTrData", "Opt10026")

    End Sub
    ''' <summary>
    ''' 전일대비등락률상위요청
    ''' </summary>
    ''' <param name="marketGb">시장구분 = 000:전체, 001:코스피, 101:코스닥</param>
    ''' <param name="sortGb">정렬구분 = 1:상승률, 2:상승폭, 3:하락률, 4:하락폭</param>
    ''' <param name="volumeJoguen">거래량조건 = 0000:전체조회, 0010:만주이상, 0050:5만주이상, 0100:10만주이상, 0150:15만주이상, 0200:20만주이상, 0300:30만주이상, 0500:50만주이상, 1000:백만주이상</param>
    ''' <param name="stockJoguen">종목조건 = 0:전체조회, 1:관리종목제외, 4:우선주+관리주제외, 3:우선주제외, 5:증100제외, 6:증100만보기, 7:증40만보기, 8:증30만보기, 9:증20만보기, 11:정리매매종목제외</param>
    ''' <param name="loadJoguen">신용조건 = 0:전체조회, 1:신용융자A군, 2:신용융자B군, 3:신용융자C군, 4:신용융자D군, 9:신용융자전체</param>
    ''' <param name="sangHahanIn">상하한포함 = 0:불 포함, 1:포함</param>
    ''' <param name="priceJoguen">가격조건 = 0:전체조회, 1:1천원미만, 2:1천원~2천원, 3:2천원~5천원, 4:5천원~1만원, 5:1만원이상, 8:1천원이상</param>
    ''' <param name="tradeDaeGumJoguen">거래대금조건 = 0:전체조회, 3:3천만원이상, 5:5천만원이상, 10:1억원이상, 30:3억원이상, 50:5억원이상, 100:10억원이상, 300:30억원이상, 500:50억원이상, 1000:100억원이상, 3000:300억원이상, 5000:500억원이상</param>
    ''' <remarks></remarks>
    Public Sub Opt10027_OnReceiveTrData(ByVal marketGb As String, ByVal sortGb As String, ByVal volumeJoguen As String, _
                                        ByVal stockJoguen As String, ByVal loadJoguen As String, ByVal sangHahanIn As String, _
                                        ByVal priceJoguen As String, ByVal tradeDaeGumJoguen As String)

        AxKH.SetInputValue("시장구분", marketGb)
        AxKH.SetInputValue("정렬구분", sortGb)
        AxKH.SetInputValue("거래량조건", volumeJoguen)
        AxKH.SetInputValue("종목조건", stockJoguen)
        AxKH.SetInputValue("신용조건", loadJoguen)
        AxKH.SetInputValue("상하한포함", sangHahanIn)
        AxKH.SetInputValue("가격조건", priceJoguen)
        AxKH.SetInputValue("거래대금조건", tradeDaeGumJoguen)
        AxKH.CommRqData("전일대비등락률상위요청", "Opt10027", "0", "4027")
        Logger(LoggerType.NotRealData, "전일대비등락률상위요청 송신" & "(Opt10027)", "Opt10027_OnReceiveTrData", "Opt10027")

    End Sub
    ''' <summary>
    ''' 전일대비등락률상위요청
    ''' </summary>
    ''' <param name="sortGb">정렬구분 = 1:시가, 2:고가, 3:저가, 4:기준가</param>
    ''' <param name="volumeJoguen">거래량조건 = 0000:전체조회, 0010:만주이상, 0050:5만주이상, 0100:10만주이상, 0500:50만주이상, 1000:백만주이상</param>
    ''' <param name="marketGb">시장구분 = 000:전체, 001:코스피, 101:코스닥</param>
    ''' <param name="sangHahanIn">상하한포함 = 0:불 포함, 1:포함</param>
    ''' <param name="stockJoguen">종목조건 = 0:전체조회, 1:관리종목제외, 4:우선주+관리주제외, 3:우선주제외, 5:증100제외, 6:증100만보기, 7:증40만보기, 8:증30만보기, 9:증20만보기</param>
    ''' <param name="loadJoguen">신용조건 = 0:전체조회, 1:신용융자A군, 2:신용융자B군, 3:신용융자C군, 4:신용융자D군, 9:신용융자전체</param>
    ''' <param name="tradeDaeGumJoguen">거래대금조건 = 0:전체조회, 3:3천만원이상, 5:5천만원이상, 10:1억원이상, 30:3억원이상, 50:5억원이상, 100:10억원이상, 300:30억원이상, 500:50억원이상, 1000:100억원이상, 3000:300억원이상, 5000:500억원이상</param>
    ''' <param name="updownJoguen">등락조건 = 1:상위, 2:하위</param>
    ''' <remarks></remarks>
    Public Sub Opt10028_OnReceiveTrData(ByVal sortGb As String, ByVal volumeJoguen As String, ByVal marketGb As String, _
                                        ByVal sangHahanIn As String, ByVal stockJoguen As String, ByVal loadJoguen As String, _
                                        ByVal tradeDaeGumJoguen As String, ByVal updownJoguen As String)

        AxKH.SetInputValue("정렬구분", sortGb)
        AxKH.SetInputValue("거래량조건", volumeJoguen)
        AxKH.SetInputValue("시장구분", marketGb)
        AxKH.SetInputValue("상하한포함", sangHahanIn)
        AxKH.SetInputValue("종목조건", stockJoguen)
        AxKH.SetInputValue("신용조건", loadJoguen)
        AxKH.SetInputValue("거래대금조건", tradeDaeGumJoguen)
        AxKH.SetInputValue("등락조건", updownJoguen)
        AxKH.CommRqData("전일대비등락률상위요청", "Opt10028", "0", "4028")
        Logger(LoggerType.NotRealData, "전일대비등락률상위요청 송신" & "(Opt10028)", "Opt10028_OnReceiveTrData", "Opt10028")

    End Sub
    ''' <summary>
    ''' 예상체결등락률상위요청
    ''' </summary>
    ''' <param name="marketGb">시장구분 = 000:전체, 001:코스피, 101:코스닥</param>
    ''' <param name="sortGb">정렬구분 = 1:상승률, 2:상승폭, 3:보합, 4:하락률,5:하락폭, 6, 체결량, 7:상한, 8:하한</param>
    ''' <param name="volumeJoguen">거래량조건 = 0:전체조회, 1;천주이상, 3:3천주, 5:5천주, 10:만주이상, 50:5만주이상, 100:10만주이상</param>
    ''' <param name="stockJoguen">종목조건 = 0:전체조회, 1:관리종목제외, 3:우선주제외, 5:증100제외, 6:증100만보기, 7:증40만보기, 8:증30만보기, 9:증20만보기, 11:정리매매종목제외</param>
    ''' <param name="loadJoguen">신용조건 = 0:전체조회, 1:신용융자A군, 2:신용융자B군, 3:신용융자C군, 4:신용융자D군, 9:신용융자전체</param>
    ''' <param name="priceJoguen">가격조건 = 0:전체조회, 1:1천원미만, 2:1천원~2천원, 3:2천원~5천원, 4:5천원~1만원, 5:1만원이상, 8:1천원이상</param>
    ''' <remarks></remarks>
    Public Sub Opt10029_OnReceiveTrData(ByVal marketGb As String, ByVal sortGb As String, ByVal volumeJoguen As String, _
                                    ByVal stockJoguen As String, ByVal loadJoguen As String, ByVal priceJoguen As String)


        AxKH.SetInputValue("시장구분", marketGb)
        AxKH.SetInputValue("정렬구분", sortGb)
        AxKH.SetInputValue("거래량조건", volumeJoguen)
        AxKH.SetInputValue("종목조건", stockJoguen)
        AxKH.SetInputValue("신용조건", loadJoguen)
        AxKH.SetInputValue("가격조건", priceJoguen)

        AxKH.CommRqData(" 예상체결등락률상위요청", "Opt10029", "0", "4029")
        Logger(LoggerType.NotRealData, " 예상체결등락률상위요청 송신" & "(Opt10029)", "Opt10029_OnReceiveTrData", "Opt10029")

    End Sub
    ''' <summary>
    ''' 당일거래량상위요청
    ''' </summary>
    ''' <param name="marketGb">시장구분 = 000:전체, 001:코스피, 101:코스닥</param>
    ''' <param name="sortGb">정렬구분 = 1:거래량, 2:거래회전율, 3:거래대금</param>
    ''' <param name="manageStockIn">관리종목포함 = 0:관리종목 포함, 1:관리종목 미포함</param>
    ''' <remarks></remarks>
    Public Sub Opt10030_OnReceiveTrData(ByVal marketGb As String, ByVal sortGb As String, ByVal manageStockIn As String)


        AxKH.SetInputValue("시장구분", marketGb)
        AxKH.SetInputValue("정렬구분", sortGb)
        AxKH.SetInputValue("관리종목포함", manageStockIn)
        AxKH.CommRqData(" 당일거래량상위요청", "Opt10030", "0", "4030")
        Logger(LoggerType.NotRealData, " 당일거래량상위요청 송신" & "(Opt10030)", "Opt10030_OnReceiveTrData", "Opt10030")

    End Sub
    ''' <summary>
    ''' 전일거래량상위요청
    ''' </summary>
    ''' <param name="marketGb">시장구분 = 000:전체, 001:코스피, 101:코스닥</param>
    ''' <param name="searchGb">조회구분 = 1:전일거래량 상위100종목, 2:전일거래대금 상위100종목</param>
    ''' <param name="fromRank">순위시작 = 0 ~ 100 값 중에  조회를 원하는 순위 시작값</param>
    ''' <param name="toRank">순위끝 = 0 ~ 100 값 중에  조회를 원하는 순위 끝값</param>
    ''' <remarks></remarks>
    Public Sub Opt10031_OnReceiveTrData(ByVal marketGb As String, ByVal searchGb As String, ByVal fromRank As String, ByVal toRank As String)


        AxKH.SetInputValue("시장구분", marketGb)
        AxKH.SetInputValue("조회구분", searchGb)
        AxKH.SetInputValue("순위시작", fromRank)
        AxKH.SetInputValue("순위끝", toRank)
        AxKH.CommRqData(" 전일거래량상위요청", "Opt10031", "0", "4031")
        Logger(LoggerType.NotRealData, " 전일거래량상위요청 송신" & "(Opt10031)", "Opt10031_OnReceiveTrData", "Opt10031")

    End Sub
    ''' <summary>
    ''' 거래대금상위요청
    ''' </summary>
    ''' <param name="marketGb">시장구분 = 000:전체, 001:코스피, 101:코스닥</param>
    ''' <param name="manageStockIn">관리종목포함 = 0:관리종목 미포함, 1:관리종목 포함</param>
    ''' <remarks></remarks>
    Public Sub Opt10032_OnReceiveTrData(ByVal marketGb As String, ByVal manageStockIn As String)

        AxKH.SetInputValue("시장구분", marketGb)
        AxKH.SetInputValue("관리종목포함", manageStockIn)
        AxKH.CommRqData(" 거래대금상위요청", "Opt10032", "0", "4032")
        Logger(LoggerType.NotRealData, " 거래대금상위요청 송신" & "(Opt10032)", "Opt10032_OnReceiveTrData", "Opt10032")

    End Sub
    ''' <summary>
    ''' 신용비율상위요청
    ''' </summary>
    ''' <param name="marketGb">시장구분 = 000:전체, 001:코스피, 101:코스닥</param>
    ''' <param name="volumeGb">거래량구분 = 0:전체조회, 10:만주이상, 50:5만주이상, 100:10만주이상, 200:20만주이상, 300:30만주이상, 500:50만주이상, 1000:백만주이상</param>
    ''' <param name="stockJoguen">종목조건 = 0:전체조회, 1:관리종목제외, 5:증100제외, 6:증100만보기, 7:증40만보기, 8:증30만보기, 9:증20만보기</param>
    ''' <param name="sangHanGb">상하한포함 = 0:상하한 미포함, 1:상하한포함</param>
    ''' <param name="loanJoguen">신용조건 = 0:전체조회, 1:신용융자A군, 2:신용융자B군, 3:신용융자C군, 4:신용융자D군, 9:신용융자전체</param>
    ''' <remarks></remarks>
    Public Sub Opt10033_OnReceiveTrData(ByVal marketGb As String, ByVal volumeGb As String, ByVal stockJoguen As String, ByVal sangHanGb As String, ByVal loanJoguen As String)

        AxKH.SetInputValue("시장구분", marketGb)
        AxKH.SetInputValue("거래량구분", volumeGb)
        AxKH.SetInputValue("종목조건", stockJoguen)
        AxKH.SetInputValue("상하한포함", sangHanGb)
        AxKH.SetInputValue("신용조건", loanJoguen)
        AxKH.CommRqData(" 신용비율상위요청", "Opt10033", "0", "4033")
        Logger(LoggerType.NotRealData, " 신용비율상위요청 송신" & "(Opt10033)", "Opt10033_OnReceiveTrData", "Opt10033")

    End Sub
#End Region

#End Region

#Region "3. OPTK 함수 "
    Public Function OptKWFid_OnReceiveRealData(ByVal stockCode As String, ByVal nCount As Integer) As String
        Dim screenNum As String = ScreenNoManage(Enum_ScreenNo.TR_OPTK, "OptKWFid_OnReceiveRealData", stockCode)

        If screenNum = "" Then Return ""

        Try

            AxKH.CommKwRqData(stockCode, 0, nCount, 0, "관심종목정보요청", screenNum)

            Logger(LoggerType.RealData, "관심종목정보요청 송신" & "(" & "복수종목" & ")", "OptKWFid_OnReceiveRealData", stockCode)

            Return screenNum

        Catch ex As Exception
            Logger(LoggerType.RealData, " 에러 발생 관심종목정보요청 송신" & "(" & "복수종목" & ")", "OptKWFid_OnReceiveRealData", stockCode)
            Return ""
        End Try

    End Function
#End Region

#Region "4. 조건검색 "

#Region " GetUserConditionLoad                  | 조건검색을 가져온다. "
    ' 조건검색 조회응답 이벤트(OnReceiveTrCondition)
    Public Sub GetConditionLoad_OnReceiveTrCondition()
        AxKH.GetConditionLoad()
    End Sub
#End Region

#Region " GetUserConditionStockLoad             | 조건검색에 따른 세부종목을 가져온다. "
    ' 로컬에 사용자조건식 저장 성공여부 응답 이벤트(OnReceiveConditionVer)
    ''' <summary>
    ''' 조건 검색 수신
    ''' </summary>
    ''' <param name="screenNo"></param>
    ''' <param name="conditionName"></param>
    ''' <param name="conditionNo"></param>
    ''' <param name="searchType"> 0 - 조건검색 1 - 실시간 </param>
    ''' <remarks></remarks>
    Public Sub SendCondition_OnReceiveConditionVer(ByVal screenNo As String, ByVal conditionName As String, ByVal conditionNo As String, ByVal searchType As Integer)
        AxKH.SendCondition(screenNo, conditionName, conditionNo, searchType)
    End Sub

    ''' <summary>
    ''' 조건 검색 수신 중지
    ''' </summary>
    ''' <param name="screenNo"></param>
    ''' <param name="conditionName"></param>
    ''' <param name="conditionNo"></param>
    ''' <remarks></remarks>
    Public Sub SendConditionStop(ByVal screenNo As String, ByVal conditionName As String, ByVal conditionNo As String)
        AxKH.SendConditionStop(screenNo, conditionName, conditionNo)
    End Sub
#End Region

#End Region

#Region "5. 주문"
    ' 주문과 관련한 이벤트 함수는 OnReceiveMsg(), OnReceiveTRData(), OnReceiveChejan()
    '  주문 ---> 접수 ---> 체결1 ---> 잔고1  ---> 체결2  ---> 잔고2... ---> 체결n  ---> 잔고n
    ' 모의투자에서는 지정가 주문과 시장가 주문만 가능합니다.
    Public Enum OrderType
        신규매수 = 1
        신규매도 = 2
        매수취소 = 3
        매도취소 = 4
        매수정정 = 5
        매도정정 = 6
    End Enum

    ' OnReceiveChejanData
    Public Sub SendOrder_OnReceiveChejanData(ByVal sRQName As String, ByVal accNo As String, ByVal eOrderType As OrderType, ByVal stockName As String, _
                                             ByVal stockCode As String, ByVal qty As Integer, ByVal price As Integer, ByVal tradeGb As String, ByVal orgOrderNo As String)

        'Dim screenNum As String = ScreenNoManage(Enum_ScreenNo.TR_ORDER, "SendOrder_OnReceiveChejanData", stockCode)

        'If screenNum = "" Then Exit Sub

        '테스트 필요 - 동시다발 주문시 테스트 해봐야함
        AxKH.SendOrder(sRQName, "2000", accNo, eOrderType, stockCode, Math.Abs(qty), Math.Abs(price), tradeGb, orgOrderNo)

        Logger(LoggerType.Order, Trim(sRQName) & " 송신" & "(" & stockName & ")", "SendOrder_OnReceiveChejanData", stockCode)

    End Sub
#End Region

#Region "6. OPW함수  "
#Region "계좌평가잔고내역요청"
    Public Sub Opw00018_OnReceiveTrData(ByVal accNo As String)
        AxKH.SetInputValue("계좌번호", accNo)
        AxKH.SetInputValue("비밀번호", "")
        AxKH.SetInputValue("비밀번호입력매체구분", "00")
        AxKH.SetInputValue("조회구분", "2")
        AxKH.CommRqData("계좌평가잔고내역요청", "opw00018", "0", "4218")
        Logger(LoggerType.NotRealData, "계좌평가잔고내역요청 송신" & "(" & accNo & ")", "Opw00018_OnReceiveTrData", "Opw00018")
    End Sub
#End Region
#End Region

#Region "Etc"

#Region " GetAccount                            | 계좌정보를 가져온다 "

    Public _AccNo As DataSet

    Public Sub GetAccount()
        Dim ds As New DataSet, dr As DataRow
        Dim dt As New DataTable("ACCNO")

        With dt.Columns
            .Add("ACCNO")
        End With


        Dim arrAccNo As String() = Split(AxKH.GetLoginInfo("ACCNO"), ";")

        For i As Integer = 0 To UBound(arrAccNo) - 1
            dr = dt.Rows.Add()

            dr("ACCNO") = arrAccNo(i)

        Next

        ds.Tables.Add(dt)

        _AccNo = ds

    End Sub
#End Region

#Region " GetStockInfo                          | 종목명을 반환한다. "
    Public Function GetStockInfo(ByVal stockCode As String) As String
        If _allStockDataset Is Nothing = True Then Return ""
        If _allStockDataset.Tables(0).Rows.Count < 1 Then Return ""
        Dim dv As DataView

        dv = New DataView(_allStockDataset.Tables("StockList"))

        dv.RowFilter = String.Format("STOCK_CODE = '{0}'", stockCode)

        For Each drRowView As DataRowView In dv
            Return Trim(drRowView("STOCK_NAME").ToString())
        Next

        Return ""
    End Function
#End Region

#End Region

#Region " ※ AxKH_Event "

#Region " 접속(OnEventConnect) "
    Private Sub AxKH_OnEventConnect(ByVal sender As Object, ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent) Handles AxKH.OnEventConnect

        Dim str As String = AxKH.GetLoginInfo("ACCNO")

        If KOAError.IsError(e.nErrCode) = False Then
            Logger(LoggerType.Connect, KOAError.GetErrorMassage, "AxKH_OnEventConnect", "")
            lblLoginStatus.Text = "로그인 실패"
            _loginStatus = LoginStatus.DisConnect

            RaiseEvent OnEventConnect("로그인 실패")

            Exit Sub
            _allStockDataset.Reset()
            _KospiStockDataset.Reset()
            _KosDakStockDataset.Reset()



        Else
            Logger(LoggerType.Connect, KOAError.GetErrorMassage, "AxKH_OnEventConnect", "")
            lblLoginStatus.Text = "로그인 성공"
            _allStockDataset = _clsKiwoomBaseInfo.GetStockListByMarKetAll(AxKH)
            _KospiStockDataset = _clsKiwoomBaseInfo.GetStockListByMarKetKospi(AxKH)
            _KosDakStockDataset = _clsKiwoomBaseInfo.GetStockListByMarKetKosDak(AxKH)

            _loginStatus = LoginStatus.Connect

            RaiseEvent OnEventConnect("로그인 성공")

        End If
    End Sub

    Public Event OnEventConnect(ByVal status As String)

#End Region

#Region " 수신 메시지 이벤트(OnReceiveMsg)"
    ''' <summary>
    ''' 수신 메시지 이벤트
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AxKH_OnReceiveMsg(sender As Object, e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveMsgEvent) Handles AxKH.OnReceiveMsg
        Logger(LoggerType.OnMsg, "=================<< 서버 메시지 >>=====================", "OnReceiveMsg", "")
        Logger(LoggerType.OnMsg, "1. 화면번호:" & Trim(e.sScrNo) & " 2. RQName:" & Trim(e.sRQName), Trim(e.sTrCode), Trim(e.sMsg))

        RaiseEvent OnReceiveMsg("1. 화면번호:" & Trim(e.sScrNo) & " 2. RQName:" & Trim(e.sRQName) & " 3. TrName : " & Trim(e.sTrCode) & " 4. 메세지:" & Trim(e.sMsg))

    End Sub

    Public Event OnReceiveMsg(ByVal msg As String)
#End Region

#Region " 주문 접수/확인 수신시 이벤트(OnReceiveChejanData)  "

    Private _clsOrderDefine As New clsOrderDefine

    Public _OrderResult As New DataSet

    ''' <summary>
    ''' 주문 접수/확인 수신시 이벤트
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AxKH_OnReceiveChejanData(sender As Object, e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveChejanDataEvent) Handles AxKH.OnReceiveChejanData
        Dim ds As DataSet = _clsOrderDefine.GetChejanFidList(AxKH, e)
        Dim dr As DataRow

        dr = ds.Tables("ChejanFidList").Rows(0)

        If dr("미체결수량") <> "" Then
            If CInt(dr("미체결수량")) = 0 Then
                'DisconnectRealData(Trim(dr("화면번호").ToString()))
            End If
        End If

        SetOrderResultDataSet(ds)
        RaiseEvent OnReceiveChejanData(ds)
    End Sub

    Public Event OnReceiveChejanData(ByVal ds As DataSet)

    Private Sub SetOrderResultDataSet(ByVal ds As DataSet)
        If _OrderResult Is Nothing = True Then
            Dim dt As DataTable = ds.Tables(0)

            _OrderResult.Tables.Add(dt.Copy)
        ElseIf _OrderResult.Tables.Count = 0 Then
            Dim dt As DataTable = ds.Tables(0)

            _OrderResult.Tables.Add(dt.Copy)
        Else

            Dim dt As DataTable = ds.Tables(0)
            Dim drValue As DataRow

            For Each dr As DataRow In ds.Tables(0).Rows
                drValue = _OrderResult.Tables(0).NewRow()
                For i As Integer = 0 To dr.ItemArray.Length - 1
                    drValue.Item(i) = dr.Item(i)
                Next

                _OrderResult.Tables(0).Rows.Add(drValue)
            Next

        End If
    End Sub
#End Region

#Region " 로컬에 사용자조건식 저장 성공여부 응답 이벤트(AxKH_OnReceiveConditionVer) "
    Private _clsConditionSearch As New clsConditionSearch

    ''' <summary>
    ''' 로컬에 사용자조건식 저장 성공여부 응답 이벤트
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AxKH_OnReceiveConditionVer(sender As Object, e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveConditionVerEvent) Handles AxKH.OnReceiveConditionVer
        If e.lRet <> 1 Then Exit Sub

        Dim strList As String = AxKH.GetConditionNameList()
        RaiseEvent OnReceiveConditionVer(_clsConditionSearch.SetUserConditionList(strList))
    End Sub

    Public Event OnReceiveConditionVer(ByVal ds As DataSet)
#End Region

#Region " 조건검색 조회응답 이벤트(OnReceiveTrCondition) "
    ''' <summary>
    ''' 조건검색 조회응답 이벤트
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AxKH_OnReceiveTrCondition(sender As Object, e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrConditionEvent) Handles AxKH.OnReceiveTrCondition
        If Trim(e.strCodeList) <> "" Then
            RaiseEvent OnReceiveTrCondition(_clsConditionSearch.SetUserConditionStockList(e.strCodeList, AxKH), e.nIndex, e.sScrNo, e.strConditionName)
        End If
    End Sub

    Public Event OnReceiveTrCondition(ByVal ds As DataSet, ByVal index As String, ByVal scrNo As String, ByVal conName As String)

#End Region

#Region " 조건검색 실시간 조회응답 이벤트(OnReceiveRealCondition) "
    ''' <summary>
    '''  조건검색 실시간 조회응답 이벤트
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AxKH_OnReceiveRealCondition(sender As Object, e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealConditionEvent) Handles AxKH.OnReceiveRealCondition
        RaiseEvent OnReceiveRealCondition(_clsConditionSearch.SetUserConditionStockReal(e, AxKH), e.strType, e.strConditionIndex, e.strConditionName)
    End Sub

    Public Event OnReceiveRealCondition(ByVal ds As DataSet, ByVal type As String, ByVal index As String, ByVal conName As String)
#End Region

#Region " 실시간 시세 이벤트(OnReceiveRealData) "

    Private _clsRealKiwoomDefine As New clsKiwoomRealDefine

    ''' <summary>
    ''' 실시간 시세 이벤트
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AxKH_OnReceiveRealData(sender As Object, e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent) Handles AxKH.OnReceiveRealData
        Select Case Trim(e.sRealType)
            Case "주식시세"
                RaiseEvent OnReceiveRealData_Sise(_clsRealKiwoomDefine.RealKiwoomDefineDic(AxKH, e))
            Case "주식체결"
                RaiseEvent OnReceiveRealData_Volume(_clsRealKiwoomDefine.RealKiwoomDefineDic(AxKH, e))
            Case "주식우선호가"
                RaiseEvent OnReceiveRealData_PriorityHoga(_clsRealKiwoomDefine.RealKiwoomDefineDic(AxKH, e))
            Case "주식호가잔량"
                RaiseEvent OnReceiveRealData_HogaJanQty(_clsRealKiwoomDefine.RealKiwoomDefineDic(AxKH, e))
            Case "주식시간외호가"
                RaiseEvent OnReceiveRealData_TimeOutHoga(_clsRealKiwoomDefine.RealKiwoomDefineDic(AxKH, e))
            Case "주식당일거래원"
                RaiseEvent OnReceiveRealData_TodayTradePort(_clsRealKiwoomDefine.RealKiwoomDefineDic(AxKH, e))
            Case "주식예상체결"
                RaiseEvent OnReceiveRealData_ExpectVolume(_clsRealKiwoomDefine.RealKiwoomDefineDic(AxKH, e))
            Case "주식종목정보"
                RaiseEvent OnReceiveRealData_StockInfo(_clsRealKiwoomDefine.RealKiwoomDefineDic(AxKH, e))
            Case "주식거래원"
                RaiseEvent OnReceiveRealData_TradePort(_clsRealKiwoomDefine.RealKiwoomDefineDic(AxKH, e))
        End Select
    End Sub

    ''' <summary>
    ''' 주식시세 real Data Event
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Public Event OnReceiveRealData_Sise(ByVal ds As DataSet)
    ''' <summary>
    ''' 주식체결 real Data Event
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Public Event OnReceiveRealData_Volume(ByVal ds As DataSet)
    ''' <summary>
    ''' 주식우선호가
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Public Event OnReceiveRealData_PriorityHoga(ByVal ds As DataSet)
    ''' <summary>
    ''' 주식호가잔량
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Public Event OnReceiveRealData_HogaJanQty(ByVal ds As DataSet)
    ''' <summary>
    ''' 주식시간외호가
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Public Event OnReceiveRealData_TimeOutHoga(ByVal ds As DataSet)
    ''' <summary>
    ''' 주식당일거래원
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Public Event OnReceiveRealData_TodayTradePort(ByVal ds As DataSet)
    ''' <summary>
    ''' 주식예상체결
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Public Event OnReceiveRealData_ExpectVolume(ByVal ds As DataSet)
    ''' <summary>
    ''' 주식종목정보
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Public Event OnReceiveRealData_StockInfo(ByVal ds As DataSet)
    ''' <summary>
    ''' 주식거래원
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Public Event OnReceiveRealData_TradePort(ByVal ds As DataSet)
#End Region

#Region " Tran 수신시 이벤트(OnReceiveTrData) "

    Private _clsKiwoomDefine As New clsKiwoomDefine
    Private _clsKiwoomMarketInfoDefine As New clsKiwoomMarketInfoDefine

    ''' <summary>
    ''' Tran 수신시 이벤트
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AxKH_OnReceiveTrData(sender As Object, e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent) Handles AxKH.OnReceiveTrData
        Dim ds As DataSet
        Select Case e.sRQName
            Case "주식기본정보"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt10001(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.Opt10001, "Opt10001", AxKH, e))
            Case "주식거래원요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt10002(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.Opt10002, "Opt10002", AxKH, e))
            Case "체결정보요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt10003(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.Opt10003, "Opt10003", AxKH, e))
            Case "주식호가요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt10004(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.Opt10004, "Opt10004", AxKH, e))
            Case "주식일주월시분요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt10005(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.Opt10005, "Opt10005", AxKH, e))
            Case "주식시분요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt10006(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10006, "opt10006", AxKH, e))
            Case "시세표성정보요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt10007(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10007, "opt10007", AxKH, e))
                'AxKH.SetRealRemove("4007", "002290")
            Case "주식외국인요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt10008(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10008, "opt10008", AxKH, e))
            Case "주식기관요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt10009(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10009, "opt10009", AxKH, e))
            Case "업종프로그램요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt10010(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10010, "opt10010", AxKH, e))
            Case "투자자정보요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt10011(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10011, "opt10011", AxKH, e))
            Case "주문체결요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt10012(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10012, "opt10012", AxKH, e))
            Case "신용매매동향요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt10013(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10013, "opt10013", AxKH, e))
            Case "공매도추이요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt10014(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10014, "opt10014", AxKH, e))
            Case "일별거래상세요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt10015(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10015, "opt10015", AxKH, e))

            Case "신고저가요청"
                RaiseEvent OnReceiveTrData_Opt10016(_clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineDic(clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineIndex.Opt10016, "opt10016", AxKH, e))
            Case "상하한가요청"
                RaiseEvent OnReceiveTrData_Opt10017(_clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineDic(clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineIndex.Opt10017, "opt10017", AxKH, e))
            Case "고저가근접요청"
                RaiseEvent OnReceiveTrData_Opt10018(_clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineDic(clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineIndex.Opt10018, "opt10018", AxKH, e))
            Case "가격급등락요청"
                RaiseEvent OnReceiveTrData_Opt10019(_clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineDic(clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineIndex.Opt10019, "opt10019", AxKH, e))
            Case "호가잔량상위요청"
                RaiseEvent OnReceiveTrData_Opt10020(_clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineDic(clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineIndex.Opt10020, "opt10020", AxKH, e))
            Case "호가잔량급증요청"
                RaiseEvent OnReceiveTrData_Opt10021(_clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineDic(clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineIndex.Opt10021, "opt10021", AxKH, e))
            Case "잔량율급증요청"
                RaiseEvent OnReceiveTrData_Opt10022(_clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineDic(clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineIndex.Opt10022, "opt10022", AxKH, e))
            Case "거래량급증요청"
                RaiseEvent OnReceiveTrData_Opt10023(_clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineDic(clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineIndex.Opt10023, "opt10023", AxKH, e))
            Case "거래량갱신요청"
                RaiseEvent OnReceiveTrData_Opt10024(_clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineDic(clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineIndex.Opt10024, "opt10024", AxKH, e))
            Case "매물대집중요청"
                RaiseEvent OnReceiveTrData_Opt10025(_clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineDic(clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineIndex.Opt10025, "opt10025", AxKH, e))
            Case "고저PER요청"
                RaiseEvent OnReceiveTrData_Opt10026(_clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineDic(clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineIndex.Opt10026, "opt10026", AxKH, e))
            Case "전일대비등락률상위요청"
                RaiseEvent OnReceiveTrData_Opt10027(_clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineDic(clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineIndex.Opt10027, "opt10027", AxKH, e))
            Case "시가대비등락률요청"
                RaiseEvent OnReceiveTrData_Opt10028(_clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineDic(clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineIndex.Opt10028, "opt10028", AxKH, e))
            Case "예상체결등락률상위요청"
                RaiseEvent OnReceiveTrData_Opt10029(_clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineDic(clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineIndex.Opt10029, "opt10029", AxKH, e))
            Case "당일거래량상위요청"
                RaiseEvent OnReceiveTrData_Opt10030(_clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineDic(clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineIndex.Opt10030, "opt10030", AxKH, e))
            Case "일거래량상위요청"
                RaiseEvent OnReceiveTrData_Opt10031(_clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineDic(clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineIndex.Opt10031, "opt10031", AxKH, e))
            Case "거래대금상위요청"
                RaiseEvent OnReceiveTrData_Opt10032(_clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineDic(clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineIndex.Opt10032, "opt10032", AxKH, e))
            Case "신용비율상위요청"
                RaiseEvent OnReceiveTrData_Opt10033(_clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineDic(clsKiwoomMarketInfoDefine.KiwoomMaketInfoDefineIndex.Opt10033, "opt10033", AxKH, e))

            Case "종목별투자자기관별요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt10059(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10059, "opt10059", AxKH, e))
            Case "종목별투자자기관별금액"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt10059Price(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10059, "opt10059Price", AxKH, e))
            Case "종목별투자자기관별차트매수요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt10060MaeSu(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10060, "opt10060", AxKH, e))
            Case "종목별투자자기관별차트매도요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt10060Maedo(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10060, "opt10060", AxKH, e))
            Case "종목별투자자기관별차트금액매수요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt10060PriceMaeSu(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10060, "opt10060", AxKH, e))
            Case "종목별투자자기관별차트금액매도요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt10060PriceMaedo(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10060, "opt10060", AxKH, e))

            Case "종목별투자자기관별합계요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt10061(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10061, "opt10061", AxKH, e))

            Case "장중투자자별매매차트요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt10064(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10064, "opt10064", AxKH, e))

            Case "계좌평가잔고내역요청"
                RaiseEvent OnReceiveTrData_Opw00018(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.Opw00018, "Opw00018", AxKH, e))
                'DisconnectRealData(e.sScrNo)

            Case "증권사별종목매매동향요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_opt10078(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.Opt10078, "opt10078", AxKH, e))

            Case "주식분봉차트조회요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_opt10080(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.Opt10080, "opt10080", AxKH, e))

            Case "주식일봉차트조회"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_opt10081(_tech_TypicalPrice.Fn_PeriodLowestMA(20, _clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.Opt10081, "opt10081", AxKH, e), "opt10081", e.sTrCode, ""))

            Case "주식일봉차트조회(New)"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_opt10081New(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.Opt10081, "opt10081New", AxKH, e))

            Case "주식주봉차트조회"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_opt10082(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.Opt10082, "opt10082", AxKH, e))

            Case "계좌수익률요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_opt10085(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10085, "opt10085", AxKH, e))

            Case "시간외단일가요청"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_opt10087(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10087, "opt10087", AxKH, e))

            Case "대차거래추이요청(종목별)"
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_Opt20068(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt20068, "opt20068", AxKH, e))
            Case "실시간미체결요청"
                ds = _clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10075, "opt10075", AxKH, e)
                'Dim dsTemp As New DataSet

                'Dim dt접수 As DataTable = ds.Tables(0).Clone()
                'dt접수.TableName = "접수"
                'Dim dt확인 As DataTable = ds.Tables(0).Clone()
                'dt확인.TableName = "확인"
                'Dim dt체결 As DataTable = ds.Tables(0).Clone()
                'dt체결.TableName = "체결"

                'Dim dt미체결 As DataTable

                'Dim dr접수() As DataRow = ds.Tables(0).Select("주문상태 = '접수'")
                'Dim dr확인() As DataRow = ds.Tables(0).Select("주문상태 = '확인'")
                'Dim dr체결() As DataRow = ds.Tables(0).Select("주문상태 = '체결'")
                'Dim tempRow() As DataRow

                'For Each dr As DataRow In dr접수
                '    dt접수.Rows.Add(dr.ItemArray)
                'Next
                'dsTemp.Tables.Add(dt접수)

                'dt미체결 = dt접수.Copy
                'dt미체결.TableName = "미체결"

                'For Each dr As DataRow In dr확인
                '    tempRow = dt미체결.Select(String.Format("주문번호 = '{0}'", dr("원주문번호").ToString().Trim()))
                '    If tempRow.Length < 1 Then Continue For
                '    tempRow(0)("주문수량") = Int32.Parse(tempRow(0)("주문수량").ToString()) - Int32.Parse(dr("주문수량").ToString())

                '    If Int32.Parse(tempRow(0)("주문수량").ToString()) = 0 Then
                '        dt미체결.Rows.Remove(tempRow(0))
                '    End If
                'Next
                'dsTemp.Tables.Add(dt미체결.Copy)

                'For Each dr As DataRow In dr체결
                '    dt체결.Rows.Add(dr.ItemArray)
                'Next
                'dsTemp.Tables.Add(dt체결.Copy)

                'RaiseEvent OnReceiveTrData_opt10075(dsTemp)
                'DisconnectRealData(e.sScrNo)
                DisconnectRealData(e.sScrNo)
                RaiseEvent OnReceiveTrData_opt10075(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10075, "opt10075", AxKH, e))

            Case Else
                '종목코드가 안넘어와서 따로처리한것들 - S
                If e.sRQName.IndexOf("주식시분요청") > -1 Then
                    DisconnectRealData(e.sScrNo)
                    RaiseEvent OnReceiveTrData_Opt10006(_clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10006, "opt10006", AxKH, e))
                End If

                If e.sRQName.IndexOf("신용매매동향요청") > -1 Then
                    ds = _clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10013, "opt10013", AxKH, e)
                    ds.Tables(0).Columns.Add("종목코드")
                    If ds.Tables(0).Rows.Count > 0 Then
                        ds.Tables(0).Rows(0)("종목코드") = Microsoft.VisualBasic.Right(e.sRQName, 6)
                    End If
                    DisconnectRealData(e.sScrNo)
                    RaiseEvent OnReceiveTrData_Opt10013(ds)
                End If

                If e.sRQName.IndexOf("공매도추이요청") > -1 Then
                    ds = _clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10014, "opt10014", AxKH, e)
                    ds.Tables(0).Columns.Add("종목코드")
                    If ds.Tables(0).Rows.Count > 0 Then
                        ds.Tables(0).Rows(0)("종목코드") = Microsoft.VisualBasic.Right(e.sRQName, 6)
                    End If
                    DisconnectRealData(e.sScrNo)
                    RaiseEvent OnReceiveTrData_Opt10014(ds)
                End If

                If e.sRQName.IndexOf("종목별투자자기관별금액") > -1 Then
                    ds = _clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10059, "opt10059Price", AxKH, e)
                    ds.Tables(0).Columns.Add("종목코드")
                    If ds.Tables(0).Rows.Count > 0 Then
                        ds.Tables(0).Rows(0)("종목코드") = Microsoft.VisualBasic.Right(e.sRQName, 6)
                    End If
                    DisconnectRealData(e.sScrNo)
                    RaiseEvent OnReceiveTrData_Opt10059Price(ds)
                End If
                '종목코드가 안넘어와서 따로처리한것들 - E
        End Select
    End Sub

    Public Event OnReceiveTrData_Opt10001(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10002(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10003(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10004(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10005(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10006(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10007(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10008(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10009(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10010(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10011(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10012(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10013(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10014(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10015(ByVal ds As DataSet)

    Public Event OnReceiveTrData_Opt10016(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10017(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10018(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10019(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10020(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10021(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10022(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10023(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10024(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10025(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10026(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10027(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10028(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10029(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10030(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10031(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10032(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10033(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10059(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10059Price(ByVal ds As DataSet)

    Public Event OnReceiveTrData_Opt10060MaeSu(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10060Maedo(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10060PriceMaeSu(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10060PriceMaedo(ByVal ds As DataSet)

    Public Event OnReceiveTrData_Opt10061(ByVal ds As DataSet)
    Public Event OnReceiveTrData_Opt10064(ByVal ds As DataSet)


    Public Event OnReceiveTrData_Opw00018(ByVal ds As DataSet)
    Public Event OnReceiveTrData_opt10078(ByVal ds As DataSet)
    Public Event OnReceiveTrData_opt10080(ByVal ds As DataSet)
    Public Event OnReceiveTrData_opt10081(ByVal ds As DataSet)
    Public Event OnReceiveTrData_opt10081New(ByVal ds As DataSet)
    Public Event OnReceiveTrData_opt10082(ByVal ds As DataSet)
    Public Event OnReceiveTrData_opt10085(ByVal ds As DataSet)
    Public Event OnReceiveTrData_opt10087(ByVal ds As DataSet)
    Public Event OnReceiveTrData_opt10075(ByVal ds As DataSet)

    Public Event OnReceiveTrData_Opt20068(ByVal ds As DataSet)
#End Region

#Region " ? (OnReceiveInvestRealData) "
    ''' <summary>
    ''' ?
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AxKH_OnReceiveInvestRealData(sender As Object, e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveInvestRealDataEvent) Handles AxKH.OnReceiveInvestRealData

    End Sub

    Public Event OnReceiveInvestRealData(ByVal ds As DataSet)
#End Region

#End Region

    Private Const LogStartOn As String = "on"
    Private Const LogStartOff As String = "off"

    Private Sub btnLoggerStart_Click(sender As Object, e As EventArgs) Handles btnLoggerStart.Click
        If btnLoggerStart.Text = LogStartOff Then
            _LoggerStartOption = True
            btnLoggerStart.Text = LogStartOn
        Else
            _LoggerStartOption = False
            btnLoggerStart.Text = LogStartOff
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnPassword.Click
        AxKH.KOA_Functions("ShowAccountWindow", Nothing)
    End Sub

    Private Sub ucMainStockVer2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Me.DesignMode = True Then Exit Sub
        CheckForIllegalCrossThreadCalls = False
    End Sub

End Class