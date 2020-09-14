''' <summary>
''' Draws a bar in a DatGridView cell.
''' </summary>
''' <remarks></remarks>
Public Class Bar

    ''' <summary>
    ''' Draws a glass bar into a cell
    ''' </summary>
    ''' <param name="oColor">Color of the bar.</param>
    ''' <param name="e">CellPainting event's parameters</param>
    ''' <remarks></remarks>
    Public Shared Sub PintaDegradado(ByVal oColor As Drawing.Color, ByVal e As DataGridViewCellPaintingEventArgs)
        Bar.PintaDegradado(oColor, e, -1)
    End Sub

    ''' <summary>
    ''' Draws a partial glass bar into a cell
    ''' </summary>
    ''' <param name="oColor">Color of the bar.</param>
    ''' <param name="e">CellPainting event's parameters</param>
    ''' <param name="iPorcentaje">Percent value to represent.</param>
    ''' <remarks></remarks>
    Public Shared Sub PintaDegradado(ByVal oColor As Drawing.Color, ByVal e As DataGridViewCellPaintingEventArgs, ByVal iPorcentaje As Integer)
        Dim aCol As Drawing.Color() = {oColor}
        Dim aPor As Integer() = {iPorcentaje}
        If iPorcentaje = -1 Then
            Dim aPorN As Integer() = {}
            Bar.PintaDegradado(aCol, e, aPorN)
        Else
            Bar.PintaDegradado(aCol, e, aPor)
        End If
    End Sub

    ''' <summary>
    ''' Draws a partial glass bar into a cell with target indicator.
    ''' </summary>
    ''' <param name="oColor">Color of the bar.</param>
    ''' <param name="e">CellPainting event's parameters</param>
    ''' <param name="iPorcentaje">Percent value to represent.</param>
    ''' <param name="iObjetivo">Target to show.</param>
    ''' <param name="oColorObjetivo">Target indicator color.</param>
    ''' <remarks></remarks>
    Public Shared Sub PintaDegradado(ByVal oColor As Drawing.Color, ByVal e As DataGridViewCellPaintingEventArgs, ByVal iPorcentaje As Integer, ByVal iObjetivo As Integer, ByVal oColorObjetivo As Drawing.Color)
        Dim aCol As Drawing.Color() = {oColor, oColorObjetivo}
        Dim aPor As Integer() = {iPorcentaje, iObjetivo}
        Bar.PintaDegradado(aCol, e, aPor)
    End Sub

    ''' <summary>
    ''' Does the hard job
    ''' </summary>
    ''' <param name="aColores">Color array to use.</param>
    ''' <param name="e">CellPainting event's parameters</param>
    ''' <param name="aPorcentajes">Percent array to show. It can be one or two. 
    ''' First shows bar percent and second shows target percent. 
    ''' If only one percent is passed and it's cero, it will fill all cell width with the glass bar.</param>
    ''' <remarks></remarks>
    Private Shared Sub PintaDegradado(ByVal aColores As Drawing.Color(), ByVal e As DataGridViewCellPaintingEventArgs, ByVal aPorcentajes As Integer())

        '   Declares brushes and color array
        Dim oPin1 As Drawing2D.LinearGradientBrush = Nothing
        Dim oPin2 As Drawing2D.LinearGradientBrush = Nothing
        Dim oPinO As Drawing2D.LinearGradientBrush = Nothing
        Dim oColor As Drawing.Color = aColores(0)

        '   Handles any exception
        Try
            '   Gets cell rectangle
            Dim oCelda As New Rectangle(e.CellBounds.X - 1, e.CellBounds.Y - 1, e.CellBounds.Width, e.CellBounds.Height)

            '   Debugs percent array
            'For iC As Integer = 0 To aPorcentajes.Length - 1
            '    If aPorcentajes(iC) > 100 Then aPorcentajes(iC) = 100
            'Next

            '   Declares rectangles.
            Dim oRect1 As Rectangle             ' Glass upper rect.
            Dim oRect2 As Rectangle             ' Glass lower rect.
            Dim oObj As Rectangle               ' Target rect.
            Dim oFond As Rectangle              ' Background rect.
            Dim oCuad As Rectangle = Nothing    ' Border rect.

            '   Declares percent variable
            Dim iPorcentaje As Integer = 0
            Dim bPor As Boolean = False

            '   Detects if percent(s) is/are passed
            '   One percent indicates only value percent.
            '   Two percents indicate value and target percent.
            If aPorcentajes.Length > 0 Then
                bPor = True
                iPorcentaje = aPorcentajes(0)

                If iPorcentaje > 0 Then

                    '   Gets rectangles and brushes for percent indicator.
                    oRect1 = New Rectangle(oCelda.X + 4, oCelda.Y + 4, Math.Round(((oCelda.Width - 7) * iPorcentaje * 0.01) + 0.49), Math.Round((oCelda.Height - 8) / 2))
                    If oRect1.Width > oCelda.Width - 7 Then oRect1.Width = oCelda.Width - 7
                    oRect2 = New Rectangle(oCelda.X + 4, oRect1.Bottom - 1, oRect1.Width, (oCelda.Height - 6) - oRect1.Height)
                    oFond = New Rectangle(oCelda.X + 4, oCelda.Y + 4, oCelda.Width - 7, oCelda.Height - 7)
                    oPin1 = New Drawing2D.LinearGradientBrush(oRect1, Color.White, Color.FromArgb(180, oColor), Drawing2D.LinearGradientMode.Vertical)
                    oPin2 = New Drawing2D.LinearGradientBrush(oRect2, oColor, Color.FromArgb(70, oColor), Drawing2D.LinearGradientMode.Vertical)
                End If

                If aPorcentajes.Length > 1 Then

                    '   Get rectangles and brushes for target indicator
                    Dim iObj As Integer = aPorcentajes(1)
                    Dim iPos As Integer = oCelda.X + 4 + Math.Round(((oCelda.Width - 7) * iObj * 0.01) + 0.49)
                    Dim iIni As Integer = iPos - 20
                    If iIni < oCelda.X + 4 Then iIni = oCelda.X + 4
                    oObj = New Rectangle(iIni, oCelda.Y + 2, iPos - iIni, oCelda.Height - 4)
                    oPinO = New Drawing2D.LinearGradientBrush(oObj, Drawing.Color.FromArgb(0, aColores(1)), aColores(1), Drawing2D.LinearGradientMode.Horizontal)
                End If

                '   Get border rectangle
                oCuad = New Rectangle(oCelda.X + 3, oCelda.Y + 3, oCelda.Width - 6, oCelda.Height - 6)
            Else

                '   Gets rectangles and brushes to fill cell (no percents)
                oRect1 = New Rectangle(oCelda.X + 1, oCelda.Y + 1, oCelda.Width - 1, Math.Round(oCelda.Height / 2))
                oRect2 = New Rectangle(oCelda.X + 1, oRect1.Bottom - 1, oCelda.Width - 1, oCelda.Height - oRect1.Height)
                oFond = New Rectangle(oCelda.X + 1, oCelda.Y + 1, oCelda.Width - 1, oCelda.Height)
                oPin1 = New Drawing2D.LinearGradientBrush(oRect1, Color.White, Color.FromArgb(180, oColor), Drawing2D.LinearGradientMode.Vertical)
                oPin2 = New Drawing2D.LinearGradientBrush(oRect2, oColor, Color.FromArgb(70, oColor), Drawing2D.LinearGradientMode.Vertical)

            End If

            '   Paints cell background
            e.PaintBackground(e.CellBounds, True)
            If bPor Then
                e.Graphics.DrawRectangle(Pens.DimGray, oCuad)
            End If

            '   Paints glass bar
            If oPin1 IsNot Nothing Then
                e.Graphics.FillRectangle(Brushes.White, oFond)
                e.Graphics.FillRectangle(oPin1, oRect1)
                e.Graphics.FillRectangle(oPin2, oRect2)
            End If

            '   Paints target indicator
            If oPinO IsNot Nothing Then
                e.Graphics.FillRectangle(oPinO, oObj)
            End If

            '   Paints cell contents
            e.PaintContent(oCelda)
            e.Paint(oCelda, DataGridViewPaintParts.Border)
            e.Handled = True
        Catch ex As Exception
            Debug.Print(ex.Message)
        Finally

            '   Dispose all resources
            If oPin1 IsNot Nothing Then
                oPin1.Dispose()
                oPin1 = Nothing
            End If
            If oPin2 IsNot Nothing Then
                oPin2.Dispose()
                oPin2 = Nothing
            End If
            If oPinO IsNot Nothing Then
                oPinO.Dispose()
                oPinO = Nothing
            End If
        End Try
    End Sub

End Class

