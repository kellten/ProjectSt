Public Class frmFinance
    Private _mainStock As New PaikRichStock.Common.ucMainStockVer2

    Sub New(ByVal mainStock As PaikRichStock.Common.ucMainStockVer2)

        ' 이 호출은 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        _mainStock = mainStock
        AddHandler _mainStock.OnReceiveTrData_Opt10001, AddressOf Opt10001_OnReceiveTrData
        UcFinance.UcStockMain = _mainStock

    End Sub

    Private _stockCode As String

    Public WriteOnly Property StockCode() As String
        Set(value As String)
            _stockCode = value
            CallUcFinance(_stockCode)
        End Set
    End Property

    Private Sub CallUcFinance(ByVal stockCode As String)
        If stockCode = "" Then Exit Sub
        _mainStock.Opt10001_OnReceiveTrData(stockCode, _mainStock.GetStockInfo(stockCode))
    End Sub

    Private Sub Opt10001_OnReceiveTrData(ByVal ds As DataSet)
        UcFinance.StockCode = _stockCode
        UcFinance.Prop_StockBaseInfo = ds
    End Sub

End Class