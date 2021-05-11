﻿Imports PaikRichStock.Common
Imports System.Xml
Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices

Public Class ucVolumeBasicAnalysis

    Private _MainStock2 As New PaikRichStock.Common.ucMainStockVer2
    Private _sourceDt As New DataTable
    Private _copSourceDt As New DataTable
    Private _clsFunc As New PaikRichStock.Common.clsFunc
    Private _xmlDt As New DataTable
    Private _tagetDtRowCount As Integer = 0
    Private _tagetDtIngRowIndex As Integer = 0
    Private _clsCombineCallOpt As New PaikRichStock.Common.clsCombineCallOpt()

#Region " Property "
    Public WriteOnly Property MainStock2 As PaikRichStock.Common.ucMainStockVer2
        Set(value As PaikRichStock.Common.ucMainStockVer2)
            dtpStdDate.Value = DateAdd(DateInterval.Day, -1, Now.Date)

            _MainStock2 = value
            For i As Integer = 0 To UBound(modDataTable.opt10059_Cal)
                cboCondition.Items.Add(modDataTable.opt10059_Cal(i))
            Next

            cboCondition.Items.Add("주도세력")
            _clsCombineCallOpt.MainStock = _MainStock2

            With dgvSugubXmlList
                Dim dir As New IO.DirectoryInfo("C:\Xml3")

                Dim fname As IO.FileInfo

                For Each fname In dir.GetFiles()

                    If fname.Extension.Equals(".xml") Then

                        .Rows.Insert(0, 1)
                        .Rows(0).Cells(0).Value = Trim(fname.ToString())

                    End If

                Next
           
            End With
            

        End Set
    End Property

    Private _tagetDt As New DataTable

    Public WriteOnly Property TagetDt As DataTable
        Set(value As DataTable)
            GetTagetDt(value)
            InitSourceDataTable()
            GetCurrentVolume()
        End Set
    End Property

    Public WriteOnly Property TagetDataSet As DataSet
        Set(value As DataSet)
            GetTagetDataSet(value)
            InitSourceDataTable()
            GetCurrentVolume()
        End Set
    End Property

    Public ReadOnly Property SourceDt As DataTable
        Get
            Return _sourceDt
        End Get
    End Property

    Private _reload As Boolean = False

    Public WriteOnly Property Reload As Boolean
        Set(value As Boolean)
            _reload = value
        End Set
    End Property
#End Region

#Region " InitTagetDataTable "
    Private Sub InitTagetDataTable()
        _tagetDt.Reset()
        _tagetDt = Nothing
        _tagetDt = New DataTable

        With _tagetDt.Columns
            .Add("STOCK_CODE", System.Type.GetType("System.String"))
            .Add("STOCK_NAME", System.Type.GetType("System.String"))
            .Add("RELOAD_GB", System.Type.GetType("System.String"))
        End With

    End Sub

    Private Sub InitSourceDataTable()
        _sourceDt.Reset()
        _sourceDt = Nothing
        _sourceDt = New DataTable

        With _sourceDt.Columns
            .Add("STOCK_CODE", System.Type.GetType("System.String"))
            .Add("STOCK_NAME", System.Type.GetType("System.String"))
            .Add("STD_DATE", System.Type.GetType("System.String"))
            .Add("JUDO_SERUK", System.Type.GetType("System.String"))
            For i As Integer = 0 To UBound(modDataTable.opt10059_Cal)
                .Add(modDataTable.opt10059_Cal(i) & "합최저점", System.Type.GetType("System.Int32"))
                .Add(modDataTable.opt10059_Cal(i) & "합최고점", System.Type.GetType("System.Int32"))
                .Add(modDataTable.opt10059_Cal(i) & "분산합비율", System.Type.GetType("System.Int16"))
                '.Add(modDataTable.opt10059_Cal(i) & "금액최저점", System.Type.GetType("System.Int64"))
                '.Add(modDataTable.opt10059_Cal(i) & "금액최고점", System.Type.GetType("System.Int64"))
                .Add(modDataTable.opt10059_Cal(i) & "금액최저점", System.Type.GetType("System.Int64"))
                .Add(modDataTable.opt10059_Cal(i) & "금액최고점", System.Type.GetType("System.Int64"))
                .Add(modDataTable.opt10059_Cal(i) & "분산금액비율", System.Type.GetType("System.Int16"))
            Next
        End With
    End Sub
#End Region

#Region " GetTagetDt "
    Private Sub GetTagetDt(ByVal value As DataTable)
        Dim dr2th As DataRow

        InitTagetDataTable()

        With _tagetDt
            For Each dr As DataRow In value.Rows
                If Mid(Trim(dr("STOCK_NAME").ToString()), 1, 2) = "QV" Then Continue For
                dr2th = .NewRow

                dr2th("STOCK_CODE") = dr("STOCK_CODE")
                dr2th("STOCK_NAME") = dr("STOCK_NAME")

                .Rows.Add(dr2th)
            Next
        End With

        _tagetDtRowCount = _tagetDt.Rows.Count - 1
        _tagetDtIngRowIndex = 0
        lblTagetDtRowCount.Text = _tagetDtRowCount

    End Sub
#End Region

    Private Sub DisplayTagetDtIng()
        lblTagetDtRowIndex.Text = _tagetDtIngRowIndex
    End Sub

#Region " GetTagetDataSet "
    Private Sub GetTagetDataSet(ByVal value As DataSet)
        Dim dr2th As DataRow

        InitTagetDataTable()

        With _tagetDt
            For Each dr As DataRow In value.Tables(0).Rows
                If Mid(Trim(dr("STOCK_NAME").ToString()), 1, 2) = "QV" Then Continue For
                dr2th = .NewRow

                dr2th("STOCK_CODE") = dr("STOCK_CODE")
                dr2th("STOCK_NAME") = dr("STOCK_NAME")

                .Rows.Add(dr2th)
            Next
        End With

        _tagetDtRowCount = _tagetDt.Rows.Count - 1
        _tagetDtIngRowIndex = 0
        lblTagetDtRowCount.Text = _tagetDtRowCount

    End Sub
#End Region

#Region " GetCurrentVolume "
    Private Sub GetCurrentVolume()
        If _tagetDt.Rows.Count < 1 Then Exit Sub
        Try
            If _reload = False Then

                For Each dr In _tagetDt.Rows
                    GetXmlDataAnalysis(Trim(dr("STOCK_CODE").ToString()), Trim(dr("STOCK_NAME").ToString()))
                Next

                DisplaySourceDt(True)

            Else

                '                DisplayTagetDtIng()
                'For Each dr In _tagetDt.Rows
                '    GetXmlDataAnalysis(Trim(dr("STOCK_CODE").ToString()), Trim(dr("STOCK_NAME").ToString()))
                'Next

                'If Trim(_tagetDt.Rows(_tagetDtIngRowIndex).Item("STOCK_NAME").ToString()) = "탑엔지니어링" Then ' 산술연산오버플로우
                '    _tagetDtIngRowIndex = _tagetDtIngRowIndex + 1
                '    '    DisplayTagetDtIng()
                'End If
                'GetXmlDataAnalysis(Trim(_tagetDt.Rows(_tagetDtIngRowIndex).Item("STOCK_CODE").ToString()), Trim(_tagetDt.Rows(_tagetDtIngRowIndex).Item("STOCK_NAME").ToString()))

                'Application.DoEvents()

                dgvTagetList.Rows.Clear()

                lblTagetDtRowIndex.Text = 0

                Dim strStockCode As String = ""
                With dgvTagetList
                    For i As Integer = 0 To _tagetDt.Rows.Count - 1
                        If Trim(_tagetDt.Rows(i).Item("STOCK_CODE").ToString()) = "" Then Continue For
                        If Trim(_tagetDt.Rows(i).Item("STOCK_NAME").ToString()) = "에이치케이" Then Continue For
                        If Trim(_tagetDt.Rows(i).Item("STOCK_NAME").ToString()) = "유니더스" Then Continue For
                        If Trim(_tagetDt.Rows(i).Item("STOCK_NAME").ToString()) = "호전실업" Then Continue For
                        If Trim(_tagetDt.Rows(i).Item("STOCK_NAME").ToString()) = "탑엔지니어링" Then Continue For

                        If ReloadDataGuBun(Trim(_tagetDt.Rows(i).Item("STOCK_CODE").ToString()), Trim(_tagetDt.Rows(i).Item("STOCK_NAME").ToString())) = True Then

                            lblStockCode.Text = Trim(_tagetDt.Rows(i).Item("STOCK_CODE").ToString())
                            lblStockName.Text = Trim(_tagetDt.Rows(i).Item("STOCK_NAME").ToString())

                            If strStockCode = "" Then
                                strStockCode = Trim(_tagetDt.Rows(i).Item("STOCK_CODE").ToString()) & ";"
                            Else
                                strStockCode = strStockCode & Trim(_tagetDt.Rows(i).Item("STOCK_CODE").ToString()) & ";"
                            End If
                        Else
                            GetXmlDataAnalysis(Trim(_tagetDt.Rows(i).Item("STOCK_CODE").ToString()), Trim(_tagetDt.Rows(i).Item("STOCK_NAME").ToString()))
                            lblTagetDtRowIndex.Text = lblTagetDtRowIndex.Text + 1
                        End If

                        Application.DoEvents()

                    Next
                End With
                

                CallOpt(clsFunc.FirstCallSequnceOpt(strStockCode, ";"))

            End If

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub CallOpt(ByVal stockCode As String)
        If stockCode = "STOP" Then
            MsgBox("Reload 작업이 완료 되었습니다.", MsgBoxStyle.Information)

            For Each dr In _tagetDt.Rows
                GetXmlDataAnalysis(Trim(dr("STOCK_CODE").ToString()), Trim(dr("STOCK_NAME").ToString()))
            Next

            DisplaySourceDt(True)

            Exit Sub

        End If

        lblStockCode.Text = stockCode
        lblStockName.Text = _MainStock2.GetStockInfo(stockCode)
        _clsCombineCallOpt.CallOpt5981(stockCode, _MainStock2.GetStockInfo(stockCode))

    End Sub

    Private Function ReloadDataGuBun(ByVal stockCode As String, ByVal stockName As String) As Boolean
        If File.Exists("C:\Xml2\" & stockCode & "_" & stockName & ".xml") Then
            If File.Exists("C:\Xml2\" & stockCode & "_" & stockName & ".xsd") = True Then
                Dim xmlFile As XmlReader
                Dim xmlFileSc As XmlReader
                Dim xmlDt As New DataTable
                xmlFile = XmlReader.Create("C:\Xml2\" & stockCode & "_" & stockName & ".xml", New XmlReaderSettings())
                xmlFileSc = XmlReader.Create("C:\Xml2\" & stockCode & "_" & stockName & ".xsd", New XmlReaderSettings())

                xmlDt.ReadXmlSchema(xmlFileSc)
                xmlDt.ReadXml(xmlFile)

                xmlFile.Close()
                xmlFile.Dispose()
                xmlFileSc.Close()
                xmlFileSc.Dispose()

                For i As Integer = 0 To xmlDt.Rows.Count - 1
                    If Trim(xmlDt.Rows(i).Item("거래량").ToString()) <> "" Then Exit For

                    If Trim(xmlDt.Rows(i).Item("거래량").ToString()) = "" Then
                        xmlDt.Rows.RemoveAt(0)
                    End If
                Next

                If xmlDt.Rows.Count < 1 Then

                    xmlDt = Nothing
                    xmlDt = New DataTable
                    My.Computer.FileSystem.DeleteFile("C:\Xml2\" & stockCode & "_" & stockName & ".xml")
                    My.Computer.FileSystem.DeleteFile("C:\Xml2\" & stockCode & "_" & stockName & ".xsd")

                    Return True

                End If

                If lblStockName.Text = "코라오홀딩스" Then
                    MsgBox("코라오홀딩스")
                End If

                With dgvTagetList
                    .Rows.Insert(0, 1)
                    .Rows(0).Cells(0).Value = stockCode
                    .Rows(0).Cells(1).Value = stockName
                    .Rows(0).Cells(2).Value = Trim(xmlDt.Rows(0).Item("일자"))

                    If Trim(xmlDt.Rows(0).Item("일자")) >= CDateTime.FormatDate(dtpStdDate.Text) Then
                        .Rows(0).Cells(3).Value = ""
                        Return False
                    Else
                        .Rows(0).Cells(3).Value = "N"
                        Return True
                    End If
                End With

            End If
        End If

        Return True
    End Function

    Private Sub GetXmlDataAnalysis(ByVal stockCode As String, ByVal stockName As String)
        'If stockName = "이화전기" Then
        '    MsgBox("이화전기")
        'End If
        Try

            'If _tagetDtIngRowIndex = 63 Then
            '    MsgBox("63")
            'End If

            lblStockCode.Text = stockCode
            lblStockName.Text = stockName

            If File.Exists("C:\Xml2\" & stockCode & "_" & stockName & ".xml") Then
                If File.Exists("C:\Xml2\" & stockCode & "_" & stockName & ".xsd") = False Then
                    'If _reload = True Then
                    '    _clsCombineCallOpt.CallOpt5981(stockCode, stockName)
                    '    Exit Sub
                    'Else
                    '    Exit Sub
                    'End If
                    Exit Sub
                End If

                _xmlDt = Nothing
                _xmlDt = New DataTable

                Dim xmlFile As XmlReader
                Dim xmlFileSc As XmlReader
                xmlFile = XmlReader.Create("C:\Xml2\" & stockCode & "_" & stockName & ".xml", New XmlReaderSettings())
                xmlFileSc = XmlReader.Create("C:\Xml2\" & stockCode & "_" & stockName & ".xsd", New XmlReaderSettings())

                _xmlDt.ReadXmlSchema(xmlFileSc)
                _xmlDt.ReadXml(xmlFile)

                xmlFile.Close()
                xmlFile.Dispose()
                xmlFileSc.Close()
                xmlFileSc.Dispose()

                For i As Integer = 0 To _xmlDt.Rows.Count - 1
                    If Trim(_xmlDt.Rows(i).Item("거래량").ToString()) <> "" Then Exit For

                    If Trim(_xmlDt.Rows(i).Item("거래량").ToString()) = "" Then
                        _xmlDt.Rows.RemoveAt(0)
                    End If
                Next

                If _xmlDt.Rows.Count < 1 Then
                    'If _reload = True Then
                    '    _xmlDt = Nothing
                    '    _xmlDt = New DataTable
                    '    My.Computer.FileSystem.DeleteFile("C:\Xml2\" & stockCode & "_" & stockName & ".xml")
                    '    My.Computer.FileSystem.DeleteFile("C:\Xml2\" & stockCode & "_" & stockName & ".xsd")
                    '    _clsCombineCallOpt.CallOpt5981(stockCode, stockName)
                    '    Exit Sub
                    'Else
                    '    Exit Sub
                    'End If
                    Exit Sub
                End If

                'If _reload = True Then
                '    If Trim(_xmlDt.Rows(0).Item("일자")) < CDateTime.FormatDate(dtpStdDate.Text) Then

                '        _clsCombineCallOpt.CallOpt5981(stockCode, stockName)

                '    Else
                '        CalVolume(stockCode, stockName)

                '        If _tagetDtIngRowIndex = _tagetDtRowCount Then
                '            DisplaySourceDt(True)
                '            Exit Sub
                '        Else
                '            _tagetDtIngRowIndex = _tagetDtIngRowIndex + 1
                '            GetCurrentVolume()
                '            Exit Sub
                '        End If

                '    End If

                'Else
                '    CalVolume(stockCode, stockName)
                'End If

                CalVolume(stockCode, stockName)

            Else
                'If _reload = True Then
                '    _clsCombineCallOpt.CallOpt5981(stockCode, stockName)
                'End If
                Exit Sub
            End If

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub CalVolume(ByVal stockCode As String, ByVal stockName As String)
        Dim dr As DataRow

        With _sourceDt

            dr = .NewRow

            dr("STOCK_CODE") = stockCode
            dr("STOCK_NAME") = stockName
            dr("STD_DATE") = Trim(_xmlDt.Rows(0).Item("일자").ToString())

            For i As Integer = 0 To UBound(modDataTable.opt10059_Cal)
                dr(modDataTable.opt10059_Cal(i) & "합최저점") = _xmlDt.Compute("MIN(" & modDataTable.opt10059_Cal(i) & "합)", String.Empty)
                dr(modDataTable.opt10059_Cal(i) & "합최고점") = _xmlDt.Compute("MAX(" & modDataTable.opt10059_Cal(i) & "합)", String.Empty)
                dr(modDataTable.opt10059_Cal(i) & "금액최저점") = _xmlDt.Compute("MIN(" & modDataTable.opt10059_Cal(i) & "금액합)", String.Empty)
                dr(modDataTable.opt10059_Cal(i) & "금액최고점") = _xmlDt.Compute("MAX(" & modDataTable.opt10059_Cal(i) & "금액합)", String.Empty)
                dr(modDataTable.opt10059_Cal(i) & "분산합비율") = CalDistribution(_xmlDt.Rows(0).Item(modDataTable.opt10059_Cal(i) & "합"), _
                                                                                       _xmlDt.Compute("MIN(" & modDataTable.opt10059_Cal(i) & "합)", String.Empty), _
                                                                                        _xmlDt.Compute("MAX(" & modDataTable.opt10059_Cal(i) & "합)", String.Empty))
                dr(modDataTable.opt10059_Cal(i) & "분산금액비율") = CalDistribution(_xmlDt.Rows(0).Item(modDataTable.opt10059_Cal(i) & "금액합"), _
                                                                                   _xmlDt.Compute("MIN(" & modDataTable.opt10059_Cal(i) & "금액합)", String.Empty), _
                                                                                  _xmlDt.Compute("MAX(" & modDataTable.opt10059_Cal(i) & "금액합)", String.Empty))
            Next

            Dim maxPriceQty As Integer

            For i As Integer = 0 To UBound(modDataTable.opt10059_Cal)
                If modDataTable.opt10059_Cal(i) = "개인투자자" Then Continue For
                If modDataTable.opt10059_Cal(i) = "기관계" Then Continue For

                If maxPriceQty = 0 Then
                    maxPriceQty = dr(modDataTable.opt10059_Cal(i) & "금액최고점")
                    dr("JUDO_SERUK") = modDataTable.opt10059_Cal(i)
                Else
                    If maxPriceQty < dr(modDataTable.opt10059_Cal(i) & "금액최고점") Then
                        maxPriceQty = dr(modDataTable.opt10059_Cal(i) & "금액최고점")
                        dr("JUDO_SERUK") = modDataTable.opt10059_Cal(i)
                    End If
                End If
            Next

            .Rows.Add(dr)

            _xmlDt.Dispose()
            _xmlDt = Nothing

        End With
    End Sub

    Private Sub DisplaySourceDt(ByVal complete As Boolean)
        If complete = True Then
            _copSourceDt = Nothing
            _copSourceDt = New DataTable

            _copSourceDt = _sourceDt.Copy

        End If

        _clsFunc.DataTableMappingToDataGridView(_sourceDt, dgvList)

    End Sub
#End Region

    Private Function CalDistribution(ByVal value As Long, ByVal minValue As Long, ByVal MaxValue As Long) As Integer
        If MaxValue = 0 Then Return 0
        If (MaxValue - minValue) = 0 Then Return 0

        Return CInt(Math.Abs(((value - minValue) / (MaxValue - minValue)) * 100))
    End Function

    Private Sub btnJukCondition_Click(sender As Object, e As EventArgs) Handles btnJukCondition.Click
        If cboCondition.Text = "주도세력" Then
            For i As Integer = _sourceDt.Rows.Count - 1 To 0 Step -1
                If _sourceDt.Rows(i).Item(Trim(_sourceDt.Rows(i).Item("JUDO_SERUK")) & "분산금액비율") < nudGaInValue.Value Then
                    _sourceDt.Rows.RemoveAt(i)
                End If
            Next

            If chkQty.Checked = True Then
                For i As Integer = _sourceDt.Rows.Count - 1 To 0 Step -1
                    If _sourceDt.Rows(i).Item(Trim(_sourceDt.Rows(i).Item("JUDO_SERUK")) & "분산합비율") < nudGaInValue.Value Then
                        _sourceDt.Rows.RemoveAt(i)
                    End If
                Next
            End If
        Else
            For i As Integer = _sourceDt.Rows.Count - 1 To 0 Step -1
                If _sourceDt.Rows(i).Item(cboCondition.Text & "분산금액비율") < nudGaInValue.Value Then
                    _sourceDt.Rows.RemoveAt(i)
                End If
            Next

            If chkQty.Checked = True Then
                For i As Integer = _sourceDt.Rows.Count - 1 To 0 Step -1
                    If _sourceDt.Rows(i).Item(cboCondition.Text & "분산합비율") < nudGaInValue.Value Then
                        _sourceDt.Rows.RemoveAt(i)
                    End If
                Next
            End If
        End If

        DisplaySourceDt(False)

    End Sub

    Private Sub OnEventCombineOpt5981(ByVal dt As DataTable)
        If _xmlDt Is Nothing = False Then
            _xmlDt.Reset()
        End If

        lblTagetDtRowIndex.Text = lblTagetDtRowIndex.Text + 1

        Dim sysTime As String = Now.Hour & Now.Minute
        Dim sysDate As String = CDateTime.FormatDate(Now.Date)

        If sysDate = Trim(dt.Rows(0).Item("일자")) Then
            If sysTime < "1800" Then
                dt.Rows.RemoveAt(0)
            End If
        End If
        _xmlDt = Nothing
        _xmlDt = New DataTable
        _xmlDt = dt.Copy

        If Trim(_xmlDt.Rows(0).Item("종목코드").ToString()) <> lblStockCode.Text Then
            MsgBox(Trim(_xmlDt.Rows(0).Item("종목코드").ToString()) & "와 종목 코드가 틀립니다.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If File.Exists("C:\Xml2\" & lblStockCode.Text & "_" & lblStockName.Text & ".xml") Then
            If File.Exists("C:\Xml2\" & lblStockCode.Text & "_" & lblStockName.Text & ".xsd") = False Then Exit Sub
            My.Computer.FileSystem.RenameFile("C:\Xml2\" & lblStockCode.Text & "_" & lblStockName.Text & ".xml", _
                                               lblStockCode.Text & "_" & lblStockName.Text & "_" & sysDate & ".xml")
            My.Computer.FileSystem.RenameFile("C:\Xml2\" & lblStockCode.Text & "_" & lblStockName.Text & ".xsd", _
                                              lblStockCode.Text & "_" & lblStockName.Text & "_" & sysDate & ".xsd")

        End If

        _xmlDt.TableName = lblStockCode.Text & "_" & lblStockName.Text
        _xmlDt.WriteXml("C:\Xml2\" & _xmlDt.TableName & ".xml")
        _xmlDt.WriteXmlSchema("C:\Xml2\" & _xmlDt.TableName & ".xsd")

        CalVolume(lblStockCode.Text, lblStockName.Text)

        'If lblStockName.Text = "코리아홀딩스" Then
        '    MsgBox("코리아홀딩스")
        'End If

        For i As Integer = 0 To dgvTagetList.Rows.Count - 1
            If Trim(dgvTagetList.Rows(i).Cells(0).Value) = Trim(lblStockCode.Text) Then
                dgvTagetList.Rows(i).Cells(3).Value = "Y"
                Exit For
            End If
        Next

        dt = Nothing

        Application.DoEvents()

        CallOpt(clsFunc.NextCallSequnceOpt())

        'If _tagetDtIngRowIndex = _tagetDtRowCount Then
        '    DisplaySourceDt(False)
        'Else
        '    _tagetDtIngRowIndex = _tagetDtIngRowIndex + 1
        '    DisplayTagetDtIng()
        '    GetXmlDataAnalysis(Trim(_tagetDt.Rows(_tagetDtIngRowIndex).Item("STOCK_CODE").ToString()), Trim(_tagetDt.Rows(_tagetDtIngRowIndex).Item("STOCK_NAME").ToString()))
        'End If


    End Sub

    Private Sub ucVolumeBasicAnalysis_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        RemoveHandler _clsCombineCallOpt.OnEventCombineOpt5981, AddressOf OnEventCombineOpt5981
        RemoveHandler _clsCombineCallOpt.OnEventCombineOpt5981, AddressOf OnEventCombineOpt5981
    End Sub

    Private Sub ucVolumeBasicAnalysis_Load(sender As Object, e As EventArgs) Handles Me.Load
        AddHandler _clsCombineCallOpt.OnEventCombineOpt5981, AddressOf OnEventCombineOpt5981
    End Sub

    Private Sub btnReload_Click(sender As Object, e As EventArgs) Handles btnReload.Click
        _sourceDt = Nothing
        _sourceDt = New DataTable

        _sourceDt = _copSourceDt.Copy

        DisplaySourceDt(False)

    End Sub

    Private Sub btnToXml_Click(sender As Object, e As EventArgs) Handles btnToXml.Click
        Dim sysDate As String = CDateTime.FormatDate(Now.Date)
        Dim sysTime As String = Now.TimeOfDay.Hours & Now.TimeOfDay.Minutes

        _sourceDt.TableName = sysDate & "_수급분석"
        _sourceDt.WriteXml("C:\Xml3\" & sysDate & "_수급분석" & sysTime & ".xml")
        _sourceDt.WriteXmlSchema("C:\Xml3\" & sysDate & "_수급분석" & sysTime & ".xsd")

    End Sub

    Private Sub btnToExcel_Click(sender As Object, e As EventArgs) Handles btnToExcel.Click
        Dim sysDate As String = CDateTime.FormatDate(Now.Date) & "_" & Replace(Trim(Now.Date.ToShortTimeString), ":", "")
        Dim strFileName As String = "C:\" & "수급분석" & "_" & sysDate

        SaveFromDataTableToExcel(_sourceDt, strFileName)

    End Sub

    Private Sub dgvSugubXmlList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSugubXmlList.CellDoubleClick
        With dgvSugubXmlList
            If Trim(.Rows(e.RowIndex).Cells(0).Value) = "" Then Exit Sub

            _sourceDt = Nothing
            _sourceDt = New DataTable

            Dim xmlFile As XmlReader
            Dim xmlFileSc As XmlReader
            xmlFile = XmlReader.Create("C:\Xml3\" & Replace(Trim(.Rows(e.RowIndex).Cells(0).Value), ".xml", "") & ".xml", New XmlReaderSettings())
            xmlFileSc = XmlReader.Create("C:\Xml3\" & Replace(Trim(.Rows(e.RowIndex).Cells(0).Value), ".xml", "") & ".xsd", New XmlReaderSettings())

            _sourceDt.ReadXmlSchema(xmlFileSc)
            _sourceDt.ReadXml(xmlFile)

            xmlFile.Close()
            xmlFile.Dispose()
            xmlFileSc.Close()
            xmlFileSc.Dispose()

            DisplaySourceDt(False)

        End With
    End Sub

    Private Sub dgvList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellDoubleClick
        With dgvList
            If Trim(.Rows(e.RowIndex).Cells(0).Value) = "" Then Exit Sub

            lblStockName.Text = Trim(.Rows(e.RowIndex).Cells(1).Value)
            _clsFunc.DataTableMappingToDataGridView(_clsFunc.GetVolumeByGigan(Trim(.Rows(e.RowIndex).Cells(0).Value), Trim(.Rows(e.RowIndex).Cells(1).Value)), dgvSuGubAnalA)

            Dim chartFrm As New Chart.frmChart

            chartFrm.MainStock = _MainStock2
            chartFrm.GetChartData(Trim(.Rows(e.RowIndex).Cells(0).Value))
            chartFrm.Show()



            'Dim lpszParentClass As String = "NotePad"

            'Dim lpszParentWindow As String = "제목 없음 - 메모장"
            'ParenthWnd = FindWindow(lpszParentClass, lpszParentWindow)

            'If ParenthWnd.Equals(IntPtr.Zero) Then

            '    MsgBox("Notepad Not Running!")

            'Else

            '    ' Found it, so echo that we found it to debug window  

            '    ' Then set it to foreground  

            '    MsgBox("Notepad Window: " & ParenthWnd.ToString())

            '    SetForegroundWindow(ParenthWnd)

            'End If

        End With
    End Sub

    <DllImport("user32.dll")> _
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Long, ByVal Param As Integer, ByVal s As String) As Integer

    End Function

    'Private Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" (ByVal hwnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As String) As Integer
    'Private Declare Function FindWindow Lib "user32.dll" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
    'Private Declare Function FindWindowEx Lib "user32.dll" Alias "FindWindowExA" (ByVal hWnd1 As IntPtr, ByVal hWnd2 As IntPtr, ByVal lpsz1 As String, ByVal lpsz2 As String) As IntPtr

    'Private Const WM_SETTEXT As Integer = &HC

    'Private Sub test(ByVal text As String)
    '    Dim iHwnd As IntPtr = FindWindow("notepad", vbNullString)
    '    Dim iHwndChild As IntPtr = FindWindowEx(iHwnd, IntPtr.Zero, "Edit", vbNullString)
    '    SendMessage(iHwndChild, WM_SETTEXT, 0, text) '- replaces all text in edit control
    'End Sub


    '<DllImport("user32.dll")> _
    'Private Shared Function GetDlgItem(ByVal hDlg As IntPtr, ByVal nIDDlgItem As Integer) As IntPtr

    'End Function
    '<DllImport("user32.dll")> _
    'Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Long, ByVal Param As Integer, ByVal s As String) As Integer

    'End Function

    ''Private Const WM_SETTEXT As Integer = 1

    'Private Sub SetText(ByVal hWnd As IntPtr, ByVal text As String)
    '    Dim boxHwnd As IntPtr = GetDlgItem(hWnd, 114)
    '    SendMessage(boxHwnd, WM_SETTEXT, 0, text)
    'End Sub

    '' This is 2 functions from user32.dll (1 for finding the application and 1 to set it to foreground with focus)  
    '<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    'Private Shared Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr

    'End Function

    '<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    'Private Shared Function SetForegroundWindow(ByVal hWnd As IntPtr) As Long

    'End Function
    ' Here we are looking for notepad by class name and caption  

    'private DataSet _dsOpt;
    '   async Task DoOpt100059Async(string stockCode , string stockName)
    '   {
    '       TaskCompletionSource<bool> tcsOpt100059 = null;
    '       tcsOpt100059 = new TaskCompletionSource<bool>();

    '       _ucMainStockVer2.OnReceiveTrData_Opt10059Price += (d) =>
    '       {
    '           _dsOpt = d;
    '           if (tcsOpt100059.Task.IsCompleted)
    '               return;
    '           tcsOpt100059.SetResult(true);
    '       };

    '       _ucMainStockVer2.Opt10059_OnReceiveTrDataPrice(DateTime.Now.ToString("yyyyMMdd"), stockCode, stockName, "1", "0", "1");
    '       await tcsOpt100059.Task;
    '   }
    'async Task DoOpt10014Async(string stockCode, string stockName)
    '   {
    '       TaskCompletionSource<bool> tcs = null;
    '       tcs = new TaskCompletionSource<bool>();

    '       ucMainStockVer2.OnReceiveTrData_Opt10014 += (d) =>
    '       {
    '           if (tcs == null || tcs.Task.IsCompleted)
    '               return;
    '           StoredOpt10014(stockCode , d);
    '           tcs.SetResult(true);
    '           System.Threading.Thread.Sleep(300);
    '       };

    '       ucMainStockVer2.Opt10014_OnReceiveTrData(stockCode, stockName, "1", DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd"));
    '       await tcs.Task;
    '       tcs.Task.Dispose();
    '       tcs = null;
    '   }

    Dim ParenthWnd As New IntPtr(0)


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

End Class