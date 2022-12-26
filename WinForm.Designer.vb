<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WinForm
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
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

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請不要使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblLocalIP = New System.Windows.Forms.Label()
        Me.tbxLocalIP = New System.Windows.Forms.TextBox()
        Me.lblLocalPort = New System.Windows.Forms.Label()
        Me.tbxLocalPort = New System.Windows.Forms.TextBox()
        Me.lblLocalURL = New System.Windows.Forms.Label()
        Me.tbxLocalURL = New System.Windows.Forms.TextBox()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.lblSystemMessage = New System.Windows.Forms.Label()
        Me.lblRemoteIP = New System.Windows.Forms.Label()
        Me.tbxRemoteIP = New System.Windows.Forms.TextBox()
        Me.lblRemotePort = New System.Windows.Forms.Label()
        Me.tbxRemotePort = New System.Windows.Forms.TextBox()
        Me.lblRemoteURL = New System.Windows.Forms.Label()
        Me.tbxRemoteURL = New System.Windows.Forms.TextBox()
        Me.btnSend = New System.Windows.Forms.Button()
        Me.btnLinkTest = New System.Windows.Forms.Button()
        Me.tbxSystemMessage = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'lblLocalIP
        '
        Me.lblLocalIP.AutoSize = True
        Me.lblLocalIP.Location = New System.Drawing.Point(10, 20)
        Me.lblLocalIP.Name = "lblLocalIP"
        Me.lblLocalIP.Size = New System.Drawing.Size(44, 12)
        Me.lblLocalIP.TabIndex = 0
        Me.lblLocalIP.Text = "Local IP"
        '
        'tbxLocalIP
        '
        Me.tbxLocalIP.Location = New System.Drawing.Point(80, 15)
        Me.tbxLocalIP.Name = "tbxLocalIP"
        Me.tbxLocalIP.Size = New System.Drawing.Size(100, 22)
        Me.tbxLocalIP.TabIndex = 1
        Me.tbxLocalIP.Text = "127.0.0.1"
        '
        'lblLocalPort
        '
        Me.lblLocalPort.AutoSize = True
        Me.lblLocalPort.Location = New System.Drawing.Point(10, 60)
        Me.lblLocalPort.Name = "lblLocalPort"
        Me.lblLocalPort.Size = New System.Drawing.Size(53, 12)
        Me.lblLocalPort.TabIndex = 2
        Me.lblLocalPort.Text = "Local Port"
        '
        'tbxLocalPort
        '
        Me.tbxLocalPort.Location = New System.Drawing.Point(80, 55)
        Me.tbxLocalPort.Name = "tbxLocalPort"
        Me.tbxLocalPort.Size = New System.Drawing.Size(100, 22)
        Me.tbxLocalPort.TabIndex = 3
        Me.tbxLocalPort.Text = "6000"
        '
        'lblLocalURL
        '
        Me.lblLocalURL.AutoSize = True
        Me.lblLocalURL.Location = New System.Drawing.Point(10, 100)
        Me.lblLocalURL.Name = "lblLocalURL"
        Me.lblLocalURL.Size = New System.Drawing.Size(57, 12)
        Me.lblLocalURL.TabIndex = 4
        Me.lblLocalURL.Text = "Local URL"
        '
        'tbxLocalURL
        '
        Me.tbxLocalURL.Location = New System.Drawing.Point(80, 95)
        Me.tbxLocalURL.Name = "tbxLocalURL"
        Me.tbxLocalURL.Size = New System.Drawing.Size(100, 22)
        Me.tbxLocalURL.TabIndex = 5
        Me.tbxLocalURL.Text = "/test/"
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(10, 140)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(75, 23)
        Me.btnStart.TabIndex = 6
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.Enabled = False
        Me.btnStop.Location = New System.Drawing.Point(105, 140)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(75, 23)
        Me.btnStop.TabIndex = 7
        Me.btnStop.Text = "Stop"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'lblSystemMessage
        '
        Me.lblSystemMessage.AutoSize = True
        Me.lblSystemMessage.Location = New System.Drawing.Point(12, 201)
        Me.lblSystemMessage.Name = "lblSystemMessage"
        Me.lblSystemMessage.Size = New System.Drawing.Size(80, 12)
        Me.lblSystemMessage.TabIndex = 8
        Me.lblSystemMessage.Text = "System Message"
        '
        'lblRemoteIP
        '
        Me.lblRemoteIP.AutoSize = True
        Me.lblRemoteIP.Location = New System.Drawing.Point(230, 20)
        Me.lblRemoteIP.Name = "lblRemoteIP"
        Me.lblRemoteIP.Size = New System.Drawing.Size(54, 12)
        Me.lblRemoteIP.TabIndex = 9
        Me.lblRemoteIP.Text = "Remote IP"
        '
        'tbxRemoteIP
        '
        Me.tbxRemoteIP.Location = New System.Drawing.Point(306, 15)
        Me.tbxRemoteIP.Name = "tbxRemoteIP"
        Me.tbxRemoteIP.Size = New System.Drawing.Size(100, 22)
        Me.tbxRemoteIP.TabIndex = 10
        Me.tbxRemoteIP.Text = "127.0.0.1"
        '
        'lblRemotePort
        '
        Me.lblRemotePort.AutoSize = True
        Me.lblRemotePort.Location = New System.Drawing.Point(230, 60)
        Me.lblRemotePort.Name = "lblRemotePort"
        Me.lblRemotePort.Size = New System.Drawing.Size(63, 12)
        Me.lblRemotePort.TabIndex = 11
        Me.lblRemotePort.Text = "Remote Port"
        '
        'tbxRemotePort
        '
        Me.tbxRemotePort.Location = New System.Drawing.Point(306, 55)
        Me.tbxRemotePort.Name = "tbxRemotePort"
        Me.tbxRemotePort.Size = New System.Drawing.Size(100, 22)
        Me.tbxRemotePort.TabIndex = 12
        Me.tbxRemotePort.Text = "7000"
        '
        'lblRemoteURL
        '
        Me.lblRemoteURL.AutoSize = True
        Me.lblRemoteURL.Location = New System.Drawing.Point(230, 100)
        Me.lblRemoteURL.Name = "lblRemoteURL"
        Me.lblRemoteURL.Size = New System.Drawing.Size(67, 12)
        Me.lblRemoteURL.TabIndex = 13
        Me.lblRemoteURL.Text = "Remote URL"
        '
        'tbxRemoteURL
        '
        Me.tbxRemoteURL.Location = New System.Drawing.Point(306, 95)
        Me.tbxRemoteURL.Name = "tbxRemoteURL"
        Me.tbxRemoteURL.Size = New System.Drawing.Size(100, 22)
        Me.tbxRemoteURL.TabIndex = 14
        Me.tbxRemoteURL.Text = "/test/"
        '
        'btnSend
        '
        Me.btnSend.Location = New System.Drawing.Point(232, 140)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(75, 23)
        Me.btnSend.TabIndex = 15
        Me.btnSend.Text = "Send"
        Me.btnSend.UseVisualStyleBackColor = True
        '
        'btnLinkTest
        '
        Me.btnLinkTest.Location = New System.Drawing.Point(331, 140)
        Me.btnLinkTest.Name = "btnLinkTest"
        Me.btnLinkTest.Size = New System.Drawing.Size(75, 23)
        Me.btnLinkTest.TabIndex = 16
        Me.btnLinkTest.Text = "Link Test"
        Me.btnLinkTest.UseVisualStyleBackColor = True
        '
        'tbxSystemMessage
        '
        Me.tbxSystemMessage.Location = New System.Drawing.Point(14, 230)
        Me.tbxSystemMessage.Name = "tbxSystemMessage"
        Me.tbxSystemMessage.Size = New System.Drawing.Size(392, 294)
        Me.tbxSystemMessage.TabIndex = 17
        Me.tbxSystemMessage.Text = ""
        '
        'WinForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(431, 536)
        Me.Controls.Add(Me.tbxSystemMessage)
        Me.Controls.Add(Me.btnLinkTest)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.tbxRemoteURL)
        Me.Controls.Add(Me.lblRemoteURL)
        Me.Controls.Add(Me.tbxRemotePort)
        Me.Controls.Add(Me.lblRemotePort)
        Me.Controls.Add(Me.tbxRemoteIP)
        Me.Controls.Add(Me.lblRemoteIP)
        Me.Controls.Add(Me.lblSystemMessage)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.tbxLocalURL)
        Me.Controls.Add(Me.lblLocalURL)
        Me.Controls.Add(Me.tbxLocalPort)
        Me.Controls.Add(Me.lblLocalPort)
        Me.Controls.Add(Me.tbxLocalIP)
        Me.Controls.Add(Me.lblLocalIP)
        Me.Name = "WinForm"
        Me.Text = "WinForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblLocalIP As System.Windows.Forms.Label
    Friend WithEvents tbxLocalIP As System.Windows.Forms.TextBox
    Friend WithEvents lblLocalPort As System.Windows.Forms.Label
    Friend WithEvents tbxLocalPort As System.Windows.Forms.TextBox
    Friend WithEvents lblLocalURL As System.Windows.Forms.Label
    Friend WithEvents tbxLocalURL As System.Windows.Forms.TextBox
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents lblSystemMessage As System.Windows.Forms.Label
    Friend WithEvents lblRemoteIP As System.Windows.Forms.Label
    Friend WithEvents tbxRemoteIP As System.Windows.Forms.TextBox
    Friend WithEvents lblRemotePort As System.Windows.Forms.Label
    Friend WithEvents tbxRemotePort As System.Windows.Forms.TextBox
    Friend WithEvents lblRemoteURL As System.Windows.Forms.Label
    Friend WithEvents tbxRemoteURL As System.Windows.Forms.TextBox
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents btnLinkTest As System.Windows.Forms.Button
    Friend WithEvents tbxSystemMessage As System.Windows.Forms.RichTextBox

End Class
