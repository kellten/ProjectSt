Imports AnalysisSt.DataBaseFunc

Public Class OptEventCaller


    Public Event OnEventCompleteCallOpt10059(ByVal dt As DataTable)

    Private _dtOpt10059 As New DataTable
    Private _stockCode_Opt10059 As String = ""

    Private Sub CallOpt10059(ByVal startDate As String)
        ModStatus._ModMainStock.Opt10059_OnReceiveTrData(startDate, Trim(_stockCode_Opt10059), ModStatus._ModMainStock.GetStockInfo(_stockCode_Opt10059), "2", "0", "1")
    End Sub

    Public Sub GetOpt10059(ByVal stockCode As String)
        If stockCode = "" Then Exit Sub
        _stockCode_Opt10059 = stockCode

        If _dtOpt10059 Is Nothing = False Then
            _dtOpt10059.Clear()
            _dtOpt10059.Reset()
        End If

        RemoveHandler ModStatus._ModMainStock.OnReceiveTrData_Opt10059, AddressOf OnReceiveTrData_opt10059
        RemoveHandler ModStatus._ModMainStock.OnReceiveTrData_Opt10059, AddressOf OnReceiveTrData_opt10059
        AddHandler ModStatus._ModMainStock.OnReceiveTrData_Opt10059, AddressOf OnReceiveTrData_opt10059

        _lastDate = ""

        Dim sysDate As String = CDateTime.FormatDate(Now.Date)

        CallOpt10059(sysDate)

    End Sub

    Private _lastDate As String = ""

    Public Sub OnReceiveTrData_opt10059(ByVal ds As DataSet)
        Try

            If ds Is Nothing Then
                MsgBox("자료가 없습니다.")
                Exit Sub
            End If

            If ds.Tables(0).Rows.Count < 1 Then
                MsgBox("자료가 없습니다.")
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

            System.Threading.Thread.Sleep(3600)

            CallOpt10059(nextDate)

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub
End Class
