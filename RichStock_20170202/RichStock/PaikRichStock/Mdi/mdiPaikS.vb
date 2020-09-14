﻿Imports System.Windows.Forms
Imports System.IO

Public Class mdiPaikS

    Private Sub ucMainStockVer2_OnConnection(status As String) Handles UcMainStockVer2.OnEventConnect

        UcMainStockVer2.GetAccount()

        For Each dr As DataRow In UcMainStockVer2._AccNo.Tables("ACCNO").Rows
            cboAccount.Items.Add(Trim(dr("ACCNO").ToString()))
        Next

        cboAccount.SelectedIndex = 0

        UcMainStockVer2.Opt10085_OnReceiveChejanData(Trim(cboAccount.Text), "0998")

    End Sub

#Region " ShowChildForm "
    Public Sub ShowChildForm(ByVal childForm As Form, Optional ByVal dialogForm As Boolean = False)
        Dim isAlreadyContained As Boolean = False
        Dim frm As Form

        Try
            For Each frm In Me.MdiChildren
                If frm.GetType Is childForm.GetType Then
                    isAlreadyContained = True
                    frm.Activate()
                    Exit For
                End If
            Next

            If isAlreadyContained = False Then
                childForm.MdiParent = Me
                If dialogForm = True Then
                    childForm.ShowDialog()
                Else
                    childForm.Show()
                End If
                childForm.Update()
            Else
                childForm.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
        End Try
    End Sub
#End Region

#Region " "
    Private Sub mnuItem1_Click(sender As Object, e As EventArgs) Handles mnuItem1100.Click, mnuItem1200.Click, mnuItem1300.Click, mnuItem1400.Click, mnuItem1500.Click, _
                                                                         mnuItem1600.Click, mnuItem1700.Click, mnuItem1800.Click

        Select Case CType(sender, ToolStripMenuItem).Name
            Case mnuItem1100.Name

            Case mnuItem1200.Name
                ShowChildForm(New frmPaikStockAnalysis(UcMainStockVer2))
            Case mnuItem1300.Name

            Case mnuItem1400.Name

            Case mnuItem1500.Name
                'ShowChildForm(New PaikRichStock.MenuItem1.frmStockVolumeScriptToXml(UcMainStockVer2))
                ShowChildForm(New PaikRichStock.MenuItem1.frmStockVolumeScriptToXml_Ver2(UcMainStockVer2))
            Case mnuItem1600.Name
                ShowChildForm(New PaikRichStock.MenuItem1.frmVolumeAnalysis(UcMainStockVer2))
            Case mnuItem1700.Name
                ShowChildForm(New PaikRichStock.frmPaikStockMainVer3())
            Case mnuItem1800.Name
                ShowChildForm(New PaikRichStock.MenuItem1.frmVolumeStock(UcMainStockVer2))
        End Select

    End Sub
#End Region
    
    Private Sub tsmHidden_Click(sender As Object, e As EventArgs) Handles tsmHidden.Click
        pnMain.Visible = False
    End Sub

    Private Sub tsmView_Click(sender As Object, e As EventArgs) Handles tsmView.Click
        pnMain.Visible = True
    End Sub
End Class
