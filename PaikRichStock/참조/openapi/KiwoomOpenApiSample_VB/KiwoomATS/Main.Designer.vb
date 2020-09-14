<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.Btn_Login = New System.Windows.Forms.Button()
        Me.AxKHOpenAPI1 = New AxKHOpenAPILib.AxKHOpenAPI()
        Me.ListBoxResult = New System.Windows.Forms.ListBox()
        Me.Label_종목코드 = New System.Windows.Forms.Label()
        Me.TextBox_종목코드 = New System.Windows.Forms.TextBox()
        Me.Button_주식기본정보 = New System.Windows.Forms.Button()
        Me.Label_로그인상태 = New System.Windows.Forms.Label()
        Me.Button_주식일봉차트 = New System.Windows.Forms.Button()
        Me.TextBox_기준일 = New System.Windows.Forms.TextBox()
        Me.Label_일봉수 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ListBoxStatus = New System.Windows.Forms.ListBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ButtonOrder = New System.Windows.Forms.Button()
        CType(Me.AxKHOpenAPI1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Btn_Login
        '
        Me.Btn_Login.Location = New System.Drawing.Point(7, 14)
        Me.Btn_Login.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Btn_Login.Name = "Btn_Login"
        Me.Btn_Login.Size = New System.Drawing.Size(50, 27)
        Me.Btn_Login.TabIndex = 0
        Me.Btn_Login.Text = "로그인"
        Me.Btn_Login.UseVisualStyleBackColor = True
        '
        'AxKHOpenAPI1
        '
        Me.AxKHOpenAPI1.Enabled = True
        Me.AxKHOpenAPI1.Location = New System.Drawing.Point(598, 423)
        Me.AxKHOpenAPI1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.AxKHOpenAPI1.Name = "AxKHOpenAPI1"
        Me.AxKHOpenAPI1.OcxState = CType(resources.GetObject("AxKHOpenAPI1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxKHOpenAPI1.Size = New System.Drawing.Size(59, 16)
        Me.AxKHOpenAPI1.TabIndex = 18
        '
        'ListBoxResult
        '
        Me.ListBoxResult.FormattingEnabled = True
        Me.ListBoxResult.HorizontalScrollbar = True
        Me.ListBoxResult.ItemHeight = 14
        Me.ListBoxResult.Location = New System.Drawing.Point(251, 216)
        Me.ListBoxResult.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ListBoxResult.Name = "ListBoxResult"
        Me.ListBoxResult.Size = New System.Drawing.Size(192, 270)
        Me.ListBoxResult.TabIndex = 2
        '
        'Label_종목코드
        '
        Me.Label_종목코드.AutoSize = True
        Me.Label_종목코드.Location = New System.Drawing.Point(5, 37)
        Me.Label_종목코드.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label_종목코드.Name = "Label_종목코드"
        Me.Label_종목코드.Size = New System.Drawing.Size(55, 14)
        Me.Label_종목코드.TabIndex = 3
        Me.Label_종목코드.Text = "종목코드"
        '
        'TextBox_종목코드
        '
        Me.TextBox_종목코드.Location = New System.Drawing.Point(72, 34)
        Me.TextBox_종목코드.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.TextBox_종목코드.Name = "TextBox_종목코드"
        Me.TextBox_종목코드.Size = New System.Drawing.Size(59, 20)
        Me.TextBox_종목코드.TabIndex = 4
        '
        'Button_주식기본정보
        '
        Me.Button_주식기본정보.Location = New System.Drawing.Point(142, 31)
        Me.Button_주식기본정보.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Button_주식기본정보.Name = "Button_주식기본정보"
        Me.Button_주식기본정보.Size = New System.Drawing.Size(55, 27)
        Me.Button_주식기본정보.TabIndex = 5
        Me.Button_주식기본정보.Text = "주식기본정보"
        Me.Button_주식기본정보.UseVisualStyleBackColor = True
        '
        'Label_로그인상태
        '
        Me.Label_로그인상태.AutoSize = True
        Me.Label_로그인상태.Location = New System.Drawing.Point(110, 20)
        Me.Label_로그인상태.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label_로그인상태.Name = "Label_로그인상태"
        Me.Label_로그인상태.Size = New System.Drawing.Size(15, 14)
        Me.Label_로그인상태.TabIndex = 6
        Me.Label_로그인상태.Text = "OFF"
        '
        'Button_주식일봉차트
        '
        Me.Button_주식일봉차트.Location = New System.Drawing.Point(6, 66)
        Me.Button_주식일봉차트.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Button_주식일봉차트.Name = "Button_주식일봉차트"
        Me.Button_주식일봉차트.Size = New System.Drawing.Size(73, 27)
        Me.Button_주식일봉차트.TabIndex = 7
        Me.Button_주식일봉차트.Text = "주식일봉차트"
        Me.Button_주식일봉차트.UseVisualStyleBackColor = True
        '
        'TextBox_기준일
        '
        Me.TextBox_기준일.Location = New System.Drawing.Point(11, 33)
        Me.TextBox_기준일.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.TextBox_기준일.Name = "TextBox_기준일"
        Me.TextBox_기준일.Size = New System.Drawing.Size(59, 20)
        Me.TextBox_기준일.TabIndex = 8
        '
        'Label_일봉수
        '
        Me.Label_일봉수.AutoSize = True
        Me.Label_일봉수.Location = New System.Drawing.Point(154, 72)
        Me.Label_일봉수.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label_일봉수.Name = "Label_일봉수"
        Me.Label_일봉수.Size = New System.Drawing.Size(12, 14)
        Me.Label_일봉수.TabIndex = 9
        Me.Label_일봉수.Text = "0"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(121, 72)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 14)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "일봉수 : "
        '
        'ListBoxStatus
        '
        Me.ListBoxStatus.FormattingEnabled = True
        Me.ListBoxStatus.ItemHeight = 14
        Me.ListBoxStatus.Location = New System.Drawing.Point(251, 42)
        Me.ListBoxStatus.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ListBoxStatus.Name = "ListBoxStatus"
        Me.ListBoxStatus.Size = New System.Drawing.Size(192, 130)
        Me.ListBoxStatus.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(251, 20)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 14)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "키움 Open API 상태"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(251, 198)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 14)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "키움 Open API 결과"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(99, 33)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(87, 14)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "날짜 (예. 20141105)"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(68, 20)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 14)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "로그인상태 : "
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBox_종목코드)
        Me.GroupBox1.Controls.Add(Me.Label_종목코드)
        Me.GroupBox1.Controls.Add(Me.Button_주식기본정보)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 65)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox1.Size = New System.Drawing.Size(240, 82)
        Me.GroupBox1.TabIndex = 16
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "주식기본정보"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TextBox_기준일)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Button_주식일봉차트)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Label_일봉수)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 166)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox2.Size = New System.Drawing.Size(240, 107)
        Me.GroupBox2.TabIndex = 17
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "주식일봉데이터"
        '
        'ButtonOrder
        '
        Me.ButtonOrder.Location = New System.Drawing.Point(7, 290)
        Me.ButtonOrder.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ButtonOrder.Name = "ButtonOrder"
        Me.ButtonOrder.Size = New System.Drawing.Size(167, 45)
        Me.ButtonOrder.TabIndex = 19
        Me.ButtonOrder.Text = "주문"
        Me.ButtonOrder.UseVisualStyleBackColor = True
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(4.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(458, 514)
        Me.Controls.Add(Me.ButtonOrder)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ListBoxStatus)
        Me.Controls.Add(Me.Label_로그인상태)
        Me.Controls.Add(Me.ListBoxResult)
        Me.Controls.Add(Me.AxKHOpenAPI1)
        Me.Controls.Add(Me.Btn_Login)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Name = "Main"
        Me.Text = "키움 Open API ( www. ZooATS.com )"
        CType(Me.AxKHOpenAPI1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Btn_Login As System.Windows.Forms.Button
    Friend WithEvents ListBoxResult As System.Windows.Forms.ListBox
    Friend WithEvents Label_종목코드 As System.Windows.Forms.Label
    Friend WithEvents TextBox_종목코드 As System.Windows.Forms.TextBox
    Friend WithEvents Button_주식기본정보 As System.Windows.Forms.Button
    Friend WithEvents Label_로그인상태 As System.Windows.Forms.Label
    Friend WithEvents Button_주식일봉차트 As System.Windows.Forms.Button
    Friend WithEvents TextBox_기준일 As System.Windows.Forms.TextBox
    Friend WithEvents Label_일봉수 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ListBoxStatus As System.Windows.Forms.ListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonOrder As System.Windows.Forms.Button
    Public WithEvents AxKHOpenAPI1 As AxKHOpenAPILib.AxKHOpenAPI

End Class
