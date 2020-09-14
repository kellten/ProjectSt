<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Order
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox_종목코드 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBox_거래구분 = New System.Windows.Forms.ComboBox()
        Me.Button_주문 = New System.Windows.Forms.Button()
        Me.ComboBox_매매구분 = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBox_주문수량 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBox_주문가격 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBox_원주문번호 = New System.Windows.Forms.TextBox()
        Me.TextBox_계좌번호 = New System.Windows.Forms.TextBox()
        Me.Button_계좌저장 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 24)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "계좌번호"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 56)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 14)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "종목코드"
        '
        'TextBox_종목코드
        '
        Me.TextBox_종목코드.Location = New System.Drawing.Point(79, 57)
        Me.TextBox_종목코드.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.TextBox_종목코드.Name = "TextBox_종목코드"
        Me.TextBox_종목코드.Size = New System.Drawing.Size(59, 20)
        Me.TextBox_종목코드.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 87)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 14)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "거래구분"
        '
        'ComboBox_거래구분
        '
        Me.ComboBox_거래구분.FormattingEnabled = True
        Me.ComboBox_거래구분.Location = New System.Drawing.Point(79, 93)
        Me.ComboBox_거래구분.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ComboBox_거래구분.Name = "ComboBox_거래구분"
        Me.ComboBox_거래구분.Size = New System.Drawing.Size(71, 22)
        Me.ComboBox_거래구분.TabIndex = 6
        '
        'Button_주문
        '
        Me.Button_주문.Location = New System.Drawing.Point(9, 247)
        Me.Button_주문.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Button_주문.Name = "Button_주문"
        Me.Button_주문.Size = New System.Drawing.Size(178, 52)
        Me.Button_주문.TabIndex = 7
        Me.Button_주문.Text = "주문"
        Me.Button_주문.UseVisualStyleBackColor = True
        '
        'ComboBox_매매구분
        '
        Me.ComboBox_매매구분.FormattingEnabled = True
        Me.ComboBox_매매구분.Location = New System.Drawing.Point(79, 123)
        Me.ComboBox_매매구분.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ComboBox_매매구분.Name = "ComboBox_매매구분"
        Me.ComboBox_매매구분.Size = New System.Drawing.Size(71, 22)
        Me.ComboBox_매매구분.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 118)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 14)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "매매구분"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 155)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(55, 14)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "주문수량"
        '
        'TextBox_주문수량
        '
        Me.TextBox_주문수량.Location = New System.Drawing.Point(79, 154)
        Me.TextBox_주문수량.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.TextBox_주문수량.Name = "TextBox_주문수량"
        Me.TextBox_주문수량.Size = New System.Drawing.Size(59, 20)
        Me.TextBox_주문수량.TabIndex = 10
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 187)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 14)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "주문가격"
        '
        'TextBox_주문가격
        '
        Me.TextBox_주문가격.Location = New System.Drawing.Point(79, 185)
        Me.TextBox_주문가격.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.TextBox_주문가격.Name = "TextBox_주문가격"
        Me.TextBox_주문가격.Size = New System.Drawing.Size(59, 20)
        Me.TextBox_주문가격.TabIndex = 12
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 218)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(67, 14)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "원주문번호"
        '
        'TextBox_원주문번호
        '
        Me.TextBox_원주문번호.Location = New System.Drawing.Point(79, 217)
        Me.TextBox_원주문번호.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.TextBox_원주문번호.Name = "TextBox_원주문번호"
        Me.TextBox_원주문번호.Size = New System.Drawing.Size(59, 20)
        Me.TextBox_원주문번호.TabIndex = 14
        '
        'TextBox_계좌번호
        '
        Me.TextBox_계좌번호.Location = New System.Drawing.Point(79, 23)
        Me.TextBox_계좌번호.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.TextBox_계좌번호.Name = "TextBox_계좌번호"
        Me.TextBox_계좌번호.Size = New System.Drawing.Size(59, 20)
        Me.TextBox_계좌번호.TabIndex = 16
        '
        'Button_계좌저장
        '
        Me.Button_계좌저장.Location = New System.Drawing.Point(151, 23)
        Me.Button_계좌저장.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Button_계좌저장.Name = "Button_계좌저장"
        Me.Button_계좌저장.Size = New System.Drawing.Size(43, 27)
        Me.Button_계좌저장.TabIndex = 17
        Me.Button_계좌저장.Text = "계좌저장"
        Me.Button_계좌저장.UseVisualStyleBackColor = True
        '
        'Order
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(4.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(216, 314)
        Me.Controls.Add(Me.Button_계좌저장)
        Me.Controls.Add(Me.TextBox_계좌번호)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBox_원주문번호)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBox_주문가격)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBox_주문수량)
        Me.Controls.Add(Me.ComboBox_매매구분)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Button_주문)
        Me.Controls.Add(Me.ComboBox_거래구분)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox_종목코드)
        Me.Controls.Add(Me.Label1)
        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Name = "Order"
        Me.Text = "Order ( Kiwoom Open API by ZooATS)"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBox_종목코드 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ComboBox_거래구분 As System.Windows.Forms.ComboBox
    Friend WithEvents Button_주문 As System.Windows.Forms.Button
    Friend WithEvents ComboBox_매매구분 As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBox_주문수량 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBox_주문가격 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBox_원주문번호 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_계좌번호 As System.Windows.Forms.TextBox
    Friend WithEvents Button_계좌저장 As System.Windows.Forms.Button
End Class
