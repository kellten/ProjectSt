Public Class frmVolumeChart
    Private _ColumnName As UcForm.ucVolumeChartBase.Struture_ColumnName

    Public WriteOnly Property ColumnName As UcForm.ucVolumeChartBase.Struture_ColumnName
        Set(value As UcForm.ucVolumeChartBase.Struture_ColumnName)
            _ColumnName = value
            ShowVolumeChartBase()
        End Set
    End Property

    Private _ds As DataSet

    Public WriteOnly Property ds As DataSet
        Set(value As DataSet)
            _ds = value
        End Set
    End Property

    Private _ds2th As DataSet
    Public WriteOnly Property ds2th As DataSet
        Set(value As DataSet)
            _ds2th = value
        End Set
    End Property

    Private Sub ShowVolumeChartBase()
        UcVolumeChartBase1.ColumeName = _ColumnName
        UcVolumeChartBase1.Ds2th = _ds2th
        UcVolumeChartBase1.Ds = _ds

    End Sub
End Class