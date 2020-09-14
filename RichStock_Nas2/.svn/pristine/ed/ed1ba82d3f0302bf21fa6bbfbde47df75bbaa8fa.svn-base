Imports PaikRichStock.Common

Public Class frmPaikStockAnalysis

    Private _MainStock As New PaikRichStock.Common.ucMainStockVer2

    Sub New(ByVal mainStock As ucMainStockVer2)

        ' 이 호출은 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        _MainStock = mainStock

    End Sub

    Private Sub frmPaikStockAnalysis_Load(sender As Object, e As EventArgs) Handles Me.Load
        AddHandler UcFavList.SelectedStockCode, AddressOf SelectedStockCode
        UcFavList.MainStock2 = _MainStock
        UcStockVolumeAnalysis.MainStock = _MainStock
    End Sub

    Private Sub SelectedStockCode(ByVal stockCode As String, ByVal stockName As String)
        UcStockVolumeAnalysis.StockCode = stockCode
    End Sub
End Class