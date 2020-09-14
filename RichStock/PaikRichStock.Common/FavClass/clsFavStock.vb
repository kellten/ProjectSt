Public Class clsFavStock

    Private _dataAcc As New DataAccess

#Region " GetDataFavGroup - 관심그룹을 질의 "
    Public Function GetDataFavGroup(ByVal stockId As String) As DataSet
        Dim ds As DataSet

        ds = _dataAcc.p_Psi01Query("1", Mid(stockId, 1, 6), "", "")

        Return ds

    End Function
#End Region

#Region " GetDataFavStockList "
    Public Function GetDataFavStockList(ByVal stockId As String, ByVal interId As String) As DataSet
        Dim ds As DataSet

        ds = _dataAcc.p_Psi02Query("1", Mid(stockId, 1, 6), interId, "", "", "")

        Return ds

    End Function
#End Region

End Class
