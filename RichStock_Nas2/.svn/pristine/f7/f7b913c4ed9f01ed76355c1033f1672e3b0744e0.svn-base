Imports PaikRichStock.Common
Imports System.IO
Imports System.Xml

Public Class frmVolumeStock
    Private _MainStock As New PaikRichStock.Common.ucMainStockVer2
    Private _clsCalVolume As New clsCalVolumeAnalysis
    Private _clsFunc As New clsFunc

    Sub New(MainStock As PaikRichStock.Common.ucMainStockVer2)

        ' 이 호출은 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        _MainStock = MainStock

        UcCondition.MainStock = MainStock
        UcFavList.MainStock2 = MainStock
    End Sub

#Region " "
    Private Sub GetUserConditionMatch()
        Select Case tcMain.SelectedIndex
            Case 0
                For Each dr In UcFavList.ReturnFavDs.Tables(0).Rows
                    If File.Exists("C:\Xml\" & Trim(dr("STOCK_CODE").ToString()) & "_" & Trim(dr("STOCK_NAME").ToString()) & ".xml") = True Then
                        _clsCalVolume.SetDs = _clsFunc.GetXmlToDataSet(Trim(dr("STOCK_CODE").ToString()), Trim(dr("STOCK_NAME").ToString()))
                        _clsCalVolume.AutoCalSupplyAndDemand()
                    End If
                Next
            Case 1
                For Each dr In UcCondition.DsStockList.Tables(0).Rows
                    If File.Exists("C:\Xml\" & Trim(dr("STOCK_CODE").ToString()) & "_" & Trim(dr("STOCK_NAME").ToString()) & ".xml") = True Then
                        _clsCalVolume.SetDs = _clsFunc.GetXmlToDataSet(Trim(dr("STOCK_CODE").ToString()), Trim(dr("STOCK_NAME").ToString()))
                    End If
                Next
        End Select
    End Sub
#End Region

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        GetUserConditionMatch()
    End Sub
End Class