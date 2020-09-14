Imports PaikRichStock.Common
Imports System.Xml
Imports System.IO

Public Class ucSupplyDemandAnalysis
    Private _MainStock As New PaikRichStock.Common.ucMainStockVer2
    Private _clsFunc As New PaikRichStock.Common.clsFunc
    Private _clsCallOpt As New PaikRichStock.Common.clsCallOpt

    Public Structure StructureGetStockInfo
        Public stockCode As String
        Public stockName As String

        Public Sub Init()
            stockCode = ""
            stockName = ""
        End Sub
    End Structure

    Private _StockInfo As StructureGetStockInfo

    Public WriteOnly Property StockInfo As StructureGetStockInfo
        Set(value As StructureGetStockInfo)
            _StockInfo = value
            InitdtSaD()
            InitdtAutoCalSaD()
            GetXmlData(_StockInfo.stockCode, _StockInfo.stockName)
        End Set
    End Property

    Public WriteOnly Property MainStock As PaikRichStock.Common.ucMainStockVer2
        Set(value As PaikRichStock.Common.ucMainStockVer2)
            _MainStock = value
            _clsCallOpt.MainStock = value
        End Set
    End Property

    Private _ds2th As DataSet

    Private Sub ucSupplyDemandAnalysis_Load(sender As Object, e As EventArgs) Handles Me.Load
        AddHandler _clsCallOpt.OnEventReturn10059ResultDt, AddressOf OnEventReturn10059ResultDt
        AddHandler _clsCallOpt.OnEventReturn10081ResultDt, AddressOf OnEventReturn10081ResultDt
        AddHandler _clsCallOpt.OnEventReturn10059PriceResultDt, AddressOf OnEventReturn10059PriceResultDt
    End Sub

#Region " GetXmlData "
    Private _ds As New DataSet

    Private Sub GetXmlData(ByVal stockCode As String, ByVal stockName As String)
        If _ds Is Nothing = False Then
            _ds.Reset()
        End If

        If File.Exists("C:\Xml\" & stockCode & "_" & stockName & ".xml") = False Then
            MsgBox("생성된 내역이 없습니다. 생성후 사용하세요.", MsgBoxStyle.Information)
            Exit Sub
        End If

        Dim xmlFile As XmlReader

        xmlFile = XmlReader.Create("C:\Xml\" & stockCode & "_" & stockName & ".xml", New XmlReaderSettings())

        _ds.ReadXml(xmlFile)

        For i As Integer = 0 To _ds.Tables(0).Rows.Count - 1
            If Trim(_ds.Tables(0).Rows(i).Item("거래량").ToString()) <> "" Then Exit For

            If Trim(_ds.Tables(0).Rows(i).Item("거래량").ToString()) = "" Then
                _ds.Tables(0).Rows.RemoveAt(0)
            End If
        Next

        If Trim(_ds.Tables(0).Rows(0).Item("일자")) < CDateTime.FormatDate(DateAdd(DateInterval.Day, -1, Now.Date)) Then
            _clsCallOpt.LastDate = Trim(_ds.Tables(0).Rows(0).Item("일자"))
            CallOpt(_StockInfo.stockCode)
        Else
            AutoCalSupplyAndDemand()
            SetBaseInfo()
            DisplayChartBase()
        End If

    End Sub
#End Region

#Region " "

    Private _dtOpt10059 As New DataTable
    Private _dtOpt10059Price As New DataTable
    Private _dtOpt10081 As New DataTable

    Private Sub CallOpt(ByVal stockCode As String)
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
    Public Structure ReXmlDataRecal
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

    Private _structure_ReXmlDataRecal As ReXmlDataRecal

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

        Dim newDatarow() As DataRow
        Dim row As Integer = 0
        Dim blnExists As Boolean = False

        Dim lastDr As DataRow = _ds.Tables(0).Rows(0)
        ReDim newDatarow(0)

        For Each dr In _dtOpt10059.Rows
            If Trim(dr("거래량").ToString) = "" Then Continue For

            If dr("일자") > _ds.Tables(0).Rows(0).Item("일자") Then

                blnExists = True

                If row = 0 Then
                    ReDim newDatarow(row)
                Else
                    ReDim Preserve newDatarow(row)
                End If

                newDatarow(row) = dr

                row = row + 1

            Else
                Exit For
            End If

        Next

        Dim dsAddDatarow As DataRow
        Dim blnExistsArray As Boolean = False

        _structure_ReXmlDataRecal.Init()

        If blnExists = True Then
            For i As Integer = UBound(newDatarow) To 0 Step -1
                dsAddDatarow = _ds.Tables(0).NewRow

                For j As Integer = 0 To _ds.Tables(0).Columns.Count - 1
                    blnExistsArray = False
                    For Each Str As String In _ChangeColums
                        If Str = _ds.Tables(0).Columns(j).ColumnName Then
                            'dsAddDatarow(_ds.Tables(0).Columns(j).ColumnName) = ReloadedXmlDataRecal(lastDr, _ds.Tables(0).Columns(j).ColumnName, newDatarow(i)(_ds.Tables(0).Columns(j).ColumnName))
                            dsAddDatarow(_ds.Tables(0).Columns(j).ColumnName) = CInt(lastDr(_ds.Tables(0).Columns(j).ColumnName)) + CInt(newDatarow(i)(_ds.Tables(0).Columns(j).ColumnName))
                            blnExistsArray = True
                            Exit For
                        End If
                    Next

                    If blnExistsArray = False Then
                        dsAddDatarow(_ds.Tables(0).Columns(j).ColumnName) = newDatarow(i)(_ds.Tables(0).Columns(j).ColumnName)
                    End If
                Next

                _ds.Tables(0).Rows.InsertAt(dsAddDatarow, 0)

            Next
        End If

    End Sub

    Private _ChangeColums As String() = {"총합", "유통량", "개인투자자합", "외국인투자자합", "기관계합", "금융투자합", "보험합", "투신합", "기타금융합", _
                                         "은행합", "연기금등합", "사모펀드합", "국가합", "기타법인합", "내외국인합", "총금액합", "유통금액량", "개인투자자금액합", _
                                         "외국인투자자금액합", "기관계금액합", "금융투자금액합", "보험금액합", "투신금액합", "기타금융금액합", "은행금액합", _
                                         "연기금등금액합", "사모펀드금액합", "국가금액합", "기타법인금액합", "내외국인금액합"}



    Private Sub lblOpt10059_TextChanged(sender As Object, e As EventArgs) Handles lblOpt10059.TextChanged, lblOpt10081.TextChanged, lblOpt10059Price.TextChanged, lblOpt10059Price.TextChanged
        If lblOpt10059.Text = "Y" And lblOpt10081.Text = "Y" And lblOpt10059Price.Text = "Y" Then
            lblOpt10059.Text = ""
            lblOpt10081.Text = ""
            lblOpt10059Price.Text = ""
            ComBineOpt10059WithOpt10081()
            '_dtOpt10059.TableName = _stockCode & "_" & Trim(_MainStock.GetStockInfo(_stockCode))
            '_dtOpt10059.WriteXml("C:\Xml\" & _dtOpt10059.TableName & ".xml")
            'InitDataTable()
            'With dgvList
            '    For i As Integer = 0 To .Rows.Count - 1
            '        If Trim(.Rows(i).Cells(dgvIndex.StockCode).Value) = _stockCode Then
            '            .Rows(i).Cells(dgvIndex.JobGb).Value = "Y"
            '            Exit For
            '        End If
            '    Next

            '    proBar.Value = proBar.Value + 1

            '    lblOpt10059.Text = ""
            '    lblOpt10081.Text = ""
            '    lblOpt10059Price.Text = ""

            '    CallOpt(clsFunc.NextCallSequnceOpt())

            'End With
            _clsCallOpt.LastDate = ""
            AutoCalSupplyAndDemand()
            SetBaseInfo()
            DisplayChartBase()
        End If
    End Sub
#End Region

#End Region

#Region " SetBaseInfo "
    Private _dtBaseInfo As New DataTable

    Private Sub SetBaseInfo()

        _dtBaseInfo.Reset()

        With _dtBaseInfo
            .Columns.Clear()
            .Columns.Add("종목투자자")
            .Columns.Add("현재보유량")
            .Columns.Add("최대보유량")
            .Columns.Add("최소보유량")
            .Columns.Add("분산비율")
            .Columns.Add("보유비율")
            .Columns.Add("평균가")
            SetBaseInfoAddData()
        End With

    End Sub

    Private Sub SetBaseInfoAddData()
        With _dtBaseInfo
            Dim dr2th As DataRow
            Dim dr As DataRow = _ds.Tables(0).Rows(0)

            Dim totalYoTongQty As Integer = 0

            dr2th = .Rows.Add

            With _dtAutoCalSaD.Rows(_dtAutoCalSaD.Rows.Count - 1)

                totalYoTongQty = .Item("유통량")

                dr2th("종목투자자") = "개인투자자"

                dr2th("현재보유량") = .Item("개인투자자합")
                dr2th("최대보유량") = .Item("개투매집고점")
                dr2th("최소보유량") = .Item("개투최고저점")
                dr2th("분산비율") = .Item("개투분산비율2")
                dr2th("보유비율") = CInt(.Item("개인투자자합")) / totalYoTongQty * 100
                If .Item("개인투자자합") = 0 Then
                    dr2th("평균가") = 0
                Else
                    dr2th("평균가") = CInt(dr("개인투자자금액합") * 1000000 / .Item("개인투자자합"))
                End If
            End With

            dr2th = .Rows.Add

            With _dtAutoCalSaD.Rows(_dtAutoCalSaD.Rows.Count - 1)

                dr2th("종목투자자") = "외국인투자자"

                dr2th("현재보유량") = .Item("외국인투자자합")
                dr2th("최대보유량") = .Item("외투매집고점")
                dr2th("최소보유량") = .Item("외투최고저점")
                dr2th("분산비율") = .Item("외투분산비율2")
                dr2th("보유비율") = CInt(.Item("외국인투자자합")) / totalYoTongQty * 100
                If .Item("외국인투자자합") = 0 Then
                    dr2th("평균가") = 0
                Else
                    dr2th("평균가") = CInt(dr("외국인투자자금액합") * 1000000 / .Item("외국인투자자합"))
                End If
            End With


            dr2th = .Rows.Add

            With _dtAutoCalSaD.Rows(_dtAutoCalSaD.Rows.Count - 1)

                dr2th("종목투자자") = "기관계"

                dr2th("현재보유량") = .Item("기관계합")
                dr2th("최대보유량") = .Item("기관매집고점")
                dr2th("최소보유량") = .Item("기관최고저점")
                dr2th("분산비율") = .Item("기관분산비율2")
                dr2th("보유비율") = CInt(.Item("기관계합")) / totalYoTongQty * 100
                If .Item("기관계합") = 0 Then
                    dr2th("평균가") = 0
                Else
                    dr2th("평균가") = CInt(dr("기관계금액합") * 1000000 / .Item("기관계합"))
                End If
            End With

            dr2th = .Rows.Add

            With _dtAutoCalSaD.Rows(_dtAutoCalSaD.Rows.Count - 1)

                dr2th("종목투자자") = "금융투자"

                dr2th("현재보유량") = .Item("금융투자합")
                dr2th("최대보유량") = .Item("금투매집고점")
                dr2th("최소보유량") = .Item("금투최고저점")
                dr2th("분산비율") = .Item("금투분산비율2")
                dr2th("보유비율") = CInt(.Item("금융투자합")) / totalYoTongQty * 100
                If .Item("금융투자합") = 0 Then
                    dr2th("평균가") = 0
                Else
                    dr2th("평균가") = CInt(dr("금융투자금액합") * 1000000 / .Item("금융투자합"))
                End If
            End With

            dr2th = .Rows.Add

            With _dtAutoCalSaD.Rows(_dtAutoCalSaD.Rows.Count - 1)

                dr2th("종목투자자") = "보험"

                dr2th("현재보유량") = .Item("보험합")
                dr2th("최대보유량") = .Item("보험매집고점")
                dr2th("최소보유량") = .Item("보험최고저점")
                dr2th("분산비율") = .Item("보험분산비율2")
                dr2th("보유비율") = CInt(.Item("보험합")) / totalYoTongQty * 100
                If .Item("보험합") = 0 Then
                    dr2th("평균가") = 0
                Else
                    dr2th("평균가") = CInt(dr("보험금액합") * 1000000 / .Item("보험합"))
                End If
            End With

            dr2th = .Rows.Add

            With _dtAutoCalSaD.Rows(_dtAutoCalSaD.Rows.Count - 1)

                dr2th("종목투자자") = "투신"

                dr2th("현재보유량") = .Item("투신합")
                dr2th("최대보유량") = .Item("투신매집고점")
                dr2th("최소보유량") = .Item("투신최고저점")
                dr2th("분산비율") = .Item("투신분산비율2")
                dr2th("보유비율") = CInt(.Item("투신합")) / totalYoTongQty * 100
                If .Item("투신합") = 0 Then
                    dr2th("평균가") = 0
                Else
                    dr2th("평균가") = CInt(dr("투신금액합") * 1000000 / .Item("투신합"))
                End If
            End With

            dr2th = .Rows.Add

            With _dtAutoCalSaD.Rows(_dtAutoCalSaD.Rows.Count - 1)

                dr2th("종목투자자") = "기타금융"

                dr2th("현재보유량") = .Item("기타금융합")
                dr2th("최대보유량") = .Item("기금매집고점")
                dr2th("최소보유량") = .Item("기금최고저점")
                dr2th("분산비율") = .Item("기금분산비율2")
                dr2th("보유비율") = CInt(.Item("기타금융합")) / totalYoTongQty * 100
                If .Item("기타금융합") = 0 Then
                    dr2th("평균가") = 0
                Else
                    dr2th("평균가") = CInt(dr("기타금융금액합") * 1000000 / .Item("기타금융합"))
                End If
            End With

            dr2th = .Rows.Add

            With _dtAutoCalSaD.Rows(_dtAutoCalSaD.Rows.Count - 1)

                dr2th("종목투자자") = "은행"

                dr2th("현재보유량") = .Item("은행합")
                dr2th("최대보유량") = .Item("은행매집고점")
                dr2th("최소보유량") = .Item("은행최고저점")
                dr2th("분산비율") = .Item("은행분산비율2")
                dr2th("보유비율") = CInt(.Item("은행합")) / totalYoTongQty * 100
                If .Item("은행합") = 0 Then
                    dr2th("평균가") = 0
                Else
                    dr2th("평균가") = CInt(dr("은행금액합") * 1000000 / .Item("은행합"))
                End If
            End With

            dr2th = .Rows.Add

            With _dtAutoCalSaD.Rows(_dtAutoCalSaD.Rows.Count - 1)

                dr2th("종목투자자") = "연기금등"

                dr2th("현재보유량") = .Item("연기금등합")
                dr2th("최대보유량") = .Item("연금매집고점")
                dr2th("최소보유량") = .Item("연금최고저점")
                dr2th("분산비율") = .Item("연금분산비율2")
                dr2th("보유비율") = CInt(.Item("연기금등합")) / totalYoTongQty * 100
                If .Item("연기금등합") = 0 Then
                    dr2th("평균가") = 0
                Else
                    dr2th("평균가") = CInt(dr("연기금등금액합") * 1000000 / .Item("연기금등합"))
                End If
            End With

            dr2th = .Rows.Add

            With _dtAutoCalSaD.Rows(_dtAutoCalSaD.Rows.Count - 1)

                dr2th("종목투자자") = "사모펀드"

                dr2th("현재보유량") = .Item("사모펀드합")
                dr2th("최대보유량") = .Item("사모매집고점")
                dr2th("최소보유량") = .Item("사모최고저점")
                dr2th("분산비율") = .Item("사모분산비율2")
                dr2th("보유비율") = CInt(.Item("사모펀드합")) / totalYoTongQty * 100
                If .Item("사모펀드합") = 0 Then
                    dr2th("평균가") = 0
                Else
                    dr2th("평균가") = CInt(dr("사모펀드금액합") * 1000000 / .Item("사모펀드합"))
                End If
            End With

            dr2th = .Rows.Add

            With _dtAutoCalSaD.Rows(_dtAutoCalSaD.Rows.Count - 1)

                dr2th("종목투자자") = "국가"

                dr2th("현재보유량") = .Item("국가합")
                dr2th("최대보유량") = .Item("국가매집고점")
                dr2th("최소보유량") = .Item("국가최고저점")
                dr2th("분산비율") = .Item("국가분산비율2")
                dr2th("보유비율") = CInt(.Item("국가합")) / totalYoTongQty * 100
                If .Item("국가합") = 0 Then
                    dr2th("평균가") = 0
                Else
                    dr2th("평균가") = CInt(dr("국가금액합") * 1000000 / .Item("국가합"))
                End If
            End With

            dr2th = .Rows.Add

            With _dtAutoCalSaD.Rows(_dtAutoCalSaD.Rows.Count - 1)

                dr2th("종목투자자") = "기타법인"

                dr2th("현재보유량") = .Item("기타법인합")
                dr2th("최대보유량") = .Item("기법매집고점")
                dr2th("최소보유량") = .Item("기법최고저점")
                dr2th("분산비율") = .Item("기법분산비율2")
                dr2th("보유비율") = CInt(.Item("기타법인합")) / totalYoTongQty * 100
                If .Item("기타법인합") = 0 Then
                    dr2th("평균가") = 0
                Else
                    dr2th("평균가") = CInt(dr("기타법인금액합") * 1000000 / .Item("기타법인합"))
                End If
            End With

            dr2th = .Rows.Add

            With _dtAutoCalSaD.Rows(_dtAutoCalSaD.Rows.Count - 1)

                dr2th("종목투자자") = "내외국인"

                dr2th("현재보유량") = .Item("내외국인합")
                dr2th("최대보유량") = .Item("내외매집고점")
                dr2th("최소보유량") = .Item("내외최고저점")
                dr2th("분산비율") = .Item("내외분산비율2")
                dr2th("보유비율") = CInt(.Item("내외국인합")) / totalYoTongQty * 100
                If .Item("내외국인합") = 0 Then
                    dr2th("평균가") = 0
                Else
                    dr2th("평균가") = CInt(dr("내외국인금액합") * 1000000 / .Item("내외국인합"))
                End If
            End With

            'dgvVolumeC.DataSource = _dtBaseInfo
            _clsFunc.DataTableMappingToDataGridView(_dtBaseInfo, dgvVolumeC)
        End With
    End Sub
#End Region

#Region " 수급 Init "
    Private _dtSaD As New DataTable
    Private _dtAutoCalSaD As New DataTable

    Public Structure DefineSt_Sad
        Public SUM_TITLE As String
        Public SUM_Price As Integer
        Public SUM_VolumeQty As Double
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
        _dtSaD.Reset()
        With _dtSaD.Columns
            .Clear()
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
        _dtAutoCalSaD.Reset()
        With _dtAutoCalSaD.Columns
            .Clear()
            .Add("일자")

            .Add("총합")
            .Add("유통량")

            .Add("개인투자자합")
            .Add("개투최고저점")
            .Add("개투매집수량")
            .Add("개투매집고점")
            .Add("개투분산비율")
            .Add("개투분산비율2")

            .Add("외국인투자자합")
            .Add("외투최고저점")
            .Add("외투매집수량")
            .Add("외투매집고점")
            .Add("외투분산비율")
            .Add("외투분산비율2")

            .Add("기관계합")
            .Add("기관최고저점")
            .Add("기관매집수량")
            .Add("기관매집고점")
            .Add("기관분산비율")
            .Add("기관분산비율2")

            .Add("금융투자합")
            .Add("금투최고저점")
            .Add("금투매집수량")
            .Add("금투매집고점")
            .Add("금투분산비율")
            .Add("금투분산비율2")

            .Add("보험합")
            .Add("보험최고저점")
            .Add("보험매집수량")
            .Add("보험매집고점")
            .Add("보험분산비율")
            .Add("보험분산비율2")

            .Add("투신합")
            .Add("투신최고저점")
            .Add("투신매집수량")
            .Add("투신매집고점")
            .Add("투신분산비율")
            .Add("투신분산비율2")

            .Add("기타금융합")
            .Add("기금최고저점")
            .Add("기금매집수량")
            .Add("기금매집고점")
            .Add("기금분산비율")
            .Add("기금분산비율2")

            .Add("은행합")
            .Add("은행최고저점")
            .Add("은행매집수량")
            .Add("은행매집고점")
            .Add("은행분산비율")
            .Add("은행분산비율2")

            .Add("연기금등합")
            .Add("연금최고저점")
            .Add("연금매집수량")
            .Add("연금매집고점")
            .Add("연금분산비율")
            .Add("연금분산비율2")

            .Add("사모펀드합")
            .Add("사모최고저점")
            .Add("사모매집수량")
            .Add("사모매집고점")
            .Add("사모분산비율")
            .Add("사모분산비율2")

            .Add("국가합")
            .Add("국가최고저점")
            .Add("국가매집수량")
            .Add("국가매집고점")
            .Add("국가분산비율")
            .Add("국가분산비율2")

            .Add("기타법인합")
            .Add("기법최고저점")
            .Add("기법매집수량")
            .Add("기법매집고점")
            .Add("기법분산비율")
            .Add("기법분산비율2")

            .Add("내외국인합")
            .Add("내외최고저점")
            .Add("내외매집수량")
            .Add("내외매집고점")
            .Add("내외분산비율")
            .Add("내외분산비율2")
        End With
    End Sub
#End Region

#Region " 수급분석표 "
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
        Dim firstDate As String = _ds.Tables(0).Rows(0).Item("일자")
        Dim firstWeekName As String = CDateTime.WeekDayNames(_ds.Tables(0).Rows(0).Item("일자"), True)
        Dim firstBugi As String = ""

        _dtSaD.Rows.Clear()
        'dgvVolumeB.DataSource = Nothing

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


        For Each dr As DataRow In _ds.Tables(0).Rows
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

        'dgvVolumeB.DataSource = _dtSaD
        _clsFunc.DataTableMappingToDataGridView(_dtSaD, dgvVolumeB)
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
        With _ds.Tables(0)
            For i As Integer = _ds.Tables(0).Rows.Count - 1 To 0 Step -1
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

        With _ds.Tables(0).Rows(_MaxRow)
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

        With _ds.Tables(0).Rows(_MinRow)
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

        With _ds.Tables(0)
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

#Region " SupplyAndDemand Avg "
    Private Sub SDAvgAnalysis(ByRef structureVale As DefineSt_Sad, ByVal dr As DataRow)
        With structureVale
            If .SUM_Price = 0 Then
                .SUM_Price = .SUM_Price + Replace((Replace(dr("현재가"), "-", "")), "+", "")
            Else
                .SUM_Price = CInt((.SUM_Price + Replace((Replace(dr("현재가"), "-", "")), "+", "")) / 2)
            End If
            .SUM_VolumeQty = .SUM_VolumeQty + CInt(IIf(Trim(dr("거래량").ToString()) = "", 0, dr("거래량")))
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
                .SUM_VolumeQty = CInt(IIf(Trim(dr("거래량").ToString()) = "", 0, dr("거래량")))
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
                        .SUM_VolumeQty = .SUM_VolumeQty + CInt(IIf(Trim(dr("거래량").ToString()) = "", 0, CInt(dr("거래량"))))
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
                    .SUM_VolumeQty = CInt(IIf(Trim(dr("거래량").ToString()) = "", 0, dr("거래량")))
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
        If value1 > value2 Then
            Return value2
        Else
            Return value1
        End If
    End Function

    Private Function CalDistribution(ByVal value As Integer, ByVal minValue As Integer, ByVal MaxValue As Integer) As Integer
        If MaxValue = 0 Then Return 0

        Return CInt((value - minValue) / MaxValue * 100)
    End Function


    Private Function CalDistribution2(ByVal value As Integer, ByVal minValue As Integer, ByVal MaxValue As Integer) As Integer
        If MaxValue = 0 Then Return 0
        If (MaxValue - minValue) = 0 Then Return 0

        Return CInt(Math.Abs(((value - minValue) / (MaxValue - minValue)) * 100))
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

            For i As Integer = _ds.Tables(0).Rows.Count - 1 To 0 Step -1
                With _ds.Tables(0).Rows(i)

                    dr2th = _dtAutoCalSaD.Rows.Add

                    If i = _ds.Tables(0).Rows.Count - 1 Then
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
                    dr2th("개투분산비율2") = CalDistribution2(CInt(.Item("개인투자자합")), Min_GAEIN, Max_GAEIN)

                    dr2th("외국인투자자합") = CInt(.Item("외국인투자자합"))
                    dr2th("외투최고저점") = Min_FORE
                    dr2th("외투매집수량") = CInt(.Item("외국인투자자합")) - Min_FORE
                    dr2th("외투매집고점") = Max_FORE
                    dr2th("외투분산비율") = CalDistribution(CInt(.Item("외국인투자자합")), Min_FORE, Max_FORE)
                    dr2th("외투분산비율2") = CalDistribution2(CInt(.Item("외국인투자자합")), Min_FORE, Max_FORE)

                    dr2th("기관계합") = CInt(.Item("기관계합"))
                    dr2th("기관최고저점") = Min_GIGAN
                    dr2th("기관매집수량") = CInt(.Item("기관계합")) - Min_GIGAN
                    dr2th("기관매집고점") = Max_GIGAN
                    dr2th("기관분산비율") = CalDistribution(CInt(.Item("기관계합")), Min_GIGAN, Max_GIGAN)
                    dr2th("기관분산비율2") = CalDistribution2(CInt(.Item("기관계합")), Min_GIGAN, Max_GIGAN)

                    dr2th("금융투자합") = CInt(.Item("금융투자합"))
                    dr2th("금투최고저점") = Min_GUMWOONG
                    dr2th("금투매집수량") = CInt(.Item("금융투자합")) - Min_GUMWOONG
                    dr2th("금투매집고점") = Max_GUMWOONG
                    dr2th("금투분산비율") = CalDistribution(CInt(.Item("금융투자합")), Min_GUMWOONG, Max_GUMWOONG)
                    dr2th("금투분산비율2") = CalDistribution2(CInt(.Item("금융투자합")), Min_GUMWOONG, Max_GUMWOONG)

                    dr2th("보험합") = CInt(.Item("보험합"))
                    dr2th("보험최고저점") = Min_BOHUM
                    dr2th("보험매집수량") = CInt(.Item("보험합")) - Min_BOHUM
                    dr2th("보험매집고점") = Max_BOHUM
                    dr2th("보험분산비율") = CalDistribution(CInt(.Item("보험합")), Min_BOHUM, Max_BOHUM)
                    dr2th("보험분산비율2") = CalDistribution2(CInt(.Item("보험합")), Min_BOHUM, Max_BOHUM)

                    dr2th("투신합") = CInt(.Item("투신합"))
                    dr2th("투신최고저점") = Min_TUSIN
                    dr2th("투신매집수량") = CInt(.Item("투신합")) - Min_TUSIN
                    dr2th("투신매집고점") = Max_TUSIN
                    dr2th("투신분산비율") = CalDistribution(CInt(.Item("투신합")), Min_TUSIN, Max_TUSIN)
                    dr2th("투신분산비율2") = CalDistribution2(CInt(.Item("투신합")), Min_TUSIN, Max_TUSIN)

                    dr2th("기타금융합") = CInt(.Item("기타금융합"))
                    dr2th("기금최고저점") = Min_GITAGUMWOONG
                    dr2th("기금매집수량") = CInt(.Item("기타금융합")) - Min_GITAGUMWOONG
                    dr2th("기금매집고점") = Max_GITAGUMWOONG
                    dr2th("기금분산비율") = CalDistribution(CInt(.Item("기타금융합")), Min_GITAGUMWOONG, Max_GITAGUMWOONG)
                    dr2th("기금분산비율2") = CalDistribution2(CInt(.Item("기타금융합")), Min_GITAGUMWOONG, Max_GITAGUMWOONG)

                    dr2th("은행합") = CInt(.Item("은행합"))
                    dr2th("은행최고저점") = Min_BANK
                    dr2th("은행매집수량") = CInt(.Item("은행합")) - Min_BANK
                    dr2th("은행매집고점") = Max_BANK
                    dr2th("은행분산비율") = CalDistribution(CInt(.Item("은행합")), Min_BANK, Max_BANK)
                    dr2th("은행분산비율2") = CalDistribution2(CInt(.Item("은행합")), Min_BANK, Max_BANK)

                    dr2th("연기금등합") = CInt(.Item("연기금등합"))
                    dr2th("연금최고저점") = Min_YEUNGGIGUM
                    dr2th("연금매집수량") = CInt(.Item("연기금등합")) - Min_YEUNGGIGUM
                    dr2th("연금매집고점") = Max_YEUNGGIGUM
                    dr2th("연금분산비율") = CalDistribution(CInt(.Item("연기금등합")), Min_YEUNGGIGUM, Max_YEUNGGIGUM)
                    dr2th("연금분산비율2") = CalDistribution2(CInt(.Item("연기금등합")), Min_YEUNGGIGUM, Max_YEUNGGIGUM)

                    dr2th("사모펀드합") = CInt(.Item("사모펀드합"))
                    dr2th("사모최고저점") = Min_SAMOFUND
                    dr2th("사모매집수량") = CInt(.Item("사모펀드합")) - Min_SAMOFUND
                    dr2th("사모매집고점") = Max_SAMOFUND
                    dr2th("사모분산비율") = CalDistribution(CInt(.Item("사모펀드합")), Min_SAMOFUND, Max_SAMOFUND)
                    dr2th("사모분산비율2") = CalDistribution2(CInt(.Item("사모펀드합")), Min_SAMOFUND, Max_SAMOFUND)

                    dr2th("국가합") = CInt(.Item("국가합"))
                    dr2th("국가최고저점") = Min_NATION
                    dr2th("국가매집수량") = CInt(.Item("국가합")) - Min_NATION
                    dr2th("국가매집고점") = Max_NATION
                    dr2th("국가분산비율") = CalDistribution(CInt(.Item("국가합")), Min_NATION, Max_NATION)
                    dr2th("국가분산비율2") = CalDistribution2(CInt(.Item("국가합")), Min_NATION, Max_NATION)

                    dr2th("기타법인합") = CInt(.Item("기타법인합"))
                    dr2th("기법최고저점") = Min_GITABUBIN
                    dr2th("기법매집수량") = CInt(.Item("기타법인합")) - Min_GITABUBIN
                    dr2th("기법매집고점") = Max_GITABUBIN
                    dr2th("기법분산비율") = CalDistribution(CInt(.Item("기타법인합")), Min_GITABUBIN, Max_GITABUBIN)
                    dr2th("기법분산비율2") = CalDistribution2(CInt(.Item("기타법인합")), Min_GITABUBIN, Max_GITABUBIN)

                    dr2th("내외국인합") = CInt(.Item("내외국인합"))
                    dr2th("내외최고저점") = Min_IOFOR
                    dr2th("내외매집수량") = CInt(.Item("내외국인합")) - Min_IOFOR
                    dr2th("내외매집고점") = Max_IOFOR
                    dr2th("내외분산비율") = CalDistribution(CInt(.Item("내외국인합")), Min_IOFOR, Max_IOFOR)
                    dr2th("내외분산비율2") = CalDistribution2(CInt(.Item("내외국인합")), Min_IOFOR, Max_IOFOR)

                End With
            Next
        End With

        '_dtAutoCalSaD.DefaultView.Sort = "일자 Desc"

        'dgvVolumeA.DataSource = Nothing
        'dgvVolumeA.AutoGenerateColumns = True
        'dgvVolumeA.DataSource = _dtAutoCalSaD.Copy
        _clsFunc.DataTableMappingToDataGridView(_dtAutoCalSaD, dgvVolumeA)

        If _ds2th Is Nothing = False Then
            _ds2th.Reset()
        End If

        _ds2th = PaikRichStock.Common.clsFunc.DataGridViewBindedDtToDataSet(dgvVolumeA)
        dgvVolumeA.Sort(dgvVolumeA.Columns(0), System.ComponentModel.ListSortDirection.Descending)
        SupplyAndDemandAnalysis()

    End Sub
#End Region

#Region " DisplayChartBase "
    Private Sub DisplayChartBase()
        Dim intPoint As Integer = 0
        Dim high As Integer = 0
        Dim close As Integer = 0
        Dim low As Integer = 0

        With chartBase
            For j As Integer = 0 To .Series.Count - 1
                .Series(j).Points.Clear()
            Next

            For i As Integer = _ds.Tables(0).Rows.Count - 1 To 0 Step -1
                If high = 0 Then

                    high = _clsFunc.RemovePlusMinus(_ds.Tables(0).Rows(i).Item("고가").ToString())

                Else

                    If high < _clsFunc.RemovePlusMinus(_ds.Tables(0).Rows(i).Item("고가").ToString()) Then
                        high = _clsFunc.RemovePlusMinus(_ds.Tables(0).Rows(i).Item("고가").ToString())
                    End If

                End If

                If low = 0 Then
                    low = _clsFunc.RemovePlusMinus(_ds.Tables(0).Rows(i).Item("저가").ToString())
                Else
                    If _clsFunc.RemovePlusMinus(_ds.Tables(0).Rows(i).Item("저가").ToString()) <> 0 Then
                        If low > _clsFunc.RemovePlusMinus(_ds.Tables(0).Rows(i).Item("저가").ToString()) Then
                            low = _clsFunc.RemovePlusMinus(_ds.Tables(0).Rows(i).Item("저가").ToString())
                        End If
                    End If
                End If

                .Series("Price").Points.AddXY(_ds.Tables(0).Rows(i).Item("일자"), _clsFunc.RemovePlusMinus(_ds.Tables(0).Rows(i).Item("고가").ToString()))
            

                .Series("Price").Points(intPoint).YValues(1) = _clsFunc.RemovePlusMinus(_ds.Tables(0).Rows(i).Item("저가").ToString())
                .Series("Price").Points(intPoint).YValues(2) = _clsFunc.RemovePlusMinus(_ds.Tables(0).Rows(i).Item("시가").ToString())
                .Series("Price").Points(intPoint).YValues(3) = _clsFunc.RemovePlusMinus(_ds.Tables(0).Rows(i).Item("현재가").ToString())


                intPoint = intPoint + 1
            Next

      
            .ChartAreas("Price").AxisY.Minimum = CInt(low - (low * 0.1))
            .ChartAreas("Price").AxisY.Maximum = CInt(high + (high * 0.1))
            .ChartAreas("Price").AxisX.IsLabelAutoFit = True

        End With
    End Sub
#End Region

#Region "  Event "
    Private Sub dgvVolumeC_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvVolumeC.CellDoubleClick
        With dgvVolumeC
            If Trim(.Rows(e.RowIndex).Cells(0).Value) = "" Then
                Exit Sub
            End If

            Dim ColumeName As New PaikRichStock.UcForm.ucVolumeChartBase.Struture_ColumnName

            Select Case Trim(.Rows(e.RowIndex).Cells(0).Value)
                Case "개인투자자"
                    ColumeName.VolumeSumName = "개인투자자합"
                    ColumeName.AvgPriceSum = "개인투자자금액합"
                    ColumeName.Title = "개인투자자"
                    ColumeName.DistributeVolume = "개투분산비율2"
                Case "외국인투자자"
                    ColumeName.VolumeSumName = "외국인투자자합"
                    ColumeName.AvgPriceSum = "외국인투자자금액합"
                    ColumeName.Title = "외국인투자자"
                    ColumeName.DistributeVolume = "외투분산비율2"
                Case "기관계"
                    ColumeName.VolumeSumName = "기관계합"
                    ColumeName.AvgPriceSum = "기관계금액합"
                    ColumeName.Title = "기관계"
                    ColumeName.DistributeVolume = "기관분산비율2"
                Case "금융투자"
                    ColumeName.VolumeSumName = "금융투자합"
                    ColumeName.AvgPriceSum = "금융투자금액합"
                    ColumeName.Title = "금융투자"
                    ColumeName.DistributeVolume = "금투분산비율2"
                Case "보험"
                    ColumeName.VolumeSumName = "보험합"
                    ColumeName.AvgPriceSum = "보험금액합"
                    ColumeName.Title = "보험"
                    ColumeName.DistributeVolume = "보험분산비율2"
                Case "투신"
                    ColumeName.VolumeSumName = "투신합"
                    ColumeName.AvgPriceSum = "투신금액합"
                    ColumeName.Title = "투신"
                    ColumeName.DistributeVolume = "투신분산비율2"
                Case "기타금융"
                    ColumeName.VolumeSumName = "기타금융합"
                    ColumeName.AvgPriceSum = "기타금융금액합"
                    ColumeName.Title = "기타금융"
                    ColumeName.DistributeVolume = "기금분산비율2"
                Case "은행"
                    ColumeName.VolumeSumName = "은행합"
                    ColumeName.AvgPriceSum = "은행금액합"
                    ColumeName.Title = "은행"
                    ColumeName.DistributeVolume = "은행분산비율2"
                Case "연기금등"
                    ColumeName.VolumeSumName = "연기금등합"
                    ColumeName.AvgPriceSum = "연기금등금액합"
                    ColumeName.Title = "연기금등"
                    ColumeName.DistributeVolume = "연금분산비율2"
                Case "사모펀드"
                    ColumeName.VolumeSumName = "사모펀드합"
                    ColumeName.AvgPriceSum = "사모펀드금액합"
                    ColumeName.Title = "사모펀드"
                    ColumeName.DistributeVolume = "사모분산비율2"
                Case "국가"
                    ColumeName.VolumeSumName = "국가합"
                    ColumeName.AvgPriceSum = "국가금액합"
                    ColumeName.Title = "국가"
                    ColumeName.DistributeVolume = "국가분산비율2"
                Case "기타법인"
                    ColumeName.VolumeSumName = "기타법인합"
                    ColumeName.AvgPriceSum = "기타법인금액합"
                    ColumeName.Title = "기타법인"
                    ColumeName.DistributeVolume = "기법분산비율2"
                Case "내외국인"
                    ColumeName.VolumeSumName = "내외국인합"
                    ColumeName.AvgPriceSum = "내외국인금액합"
                    ColumeName.Title = "내외국인"
                    ColumeName.DistributeVolume = "내외분산비율2"
            End Select

            RaiseEvent OnSelectVolumeC(ColumeName, _ds, _ds2th)

        End With
    End Sub

    Public Event OnSelectVolumeC(ByVal columnName As PaikRichStock.UcForm.ucVolumeChartBase.Struture_ColumnName, ByVal ds As DataSet, ByVal ds2th As DataSet)
#End Region
    
End Class