Public Class frmTester
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim oQuery As New SDataAccess.RichQuery
        Dim ds As DataSet

        ds = oQuery.p_ScodeQuery(query:="2", stockCode:="", ybYongCode:="", bln3tier:=False)

    End Sub
End Class