Public Class frmSearchConditionList

    Sub New(ByVal mainStock As PaikRichStock.Common.ucMainStockVer2)

        ' 이 호출은 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        _MainStock2 = mainStock
        UcCondition.MainStock = mainStock
        UcFavManageVer2.MainStock2 = mainStock
        UcVolumeBasicAnalysis.MainStock2 = mainStock

    End Sub

    Private _MainStock2 As New PaikRichStock.Common.ucMainStockVer2

#Region " GetXmlAnalysis "
    Private Sub GetXmlAnalysis()
        If chkAll.Checked = True Then
            UcVolumeBasicAnalysis.TagetDataSet = _MainStock2._allStockDataset
        Else
            Select Case tcMain.SelectedIndex
                Case 0
                    UcVolumeBasicAnalysis.TagetDataSet = UcCondition.DsStockList
                Case 1
                    UcVolumeBasicAnalysis.TagetDataSet = UcFavManageVer2.returnDs
            End Select
        End If
        
    End Sub
#End Region

#Region " Control Event "
    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        GetXmlAnalysis()
    End Sub
#End Region
    
    Private Sub btnGetDt_Click(sender As Object, e As EventArgs) Handles btnGetDt.Click
        UcFavManageVer2.addDt = UcVolumeBasicAnalysis.SourceDt
    End Sub

    Private Sub chkReload_CheckedChanged(sender As Object, e As EventArgs) Handles chkReload.CheckedChanged
        If chkReload.Checked = True Then
            UcVolumeBasicAnalysis.Reload = True
        Else
            UcVolumeBasicAnalysis.Reload = False
        End If
    End Sub
End Class