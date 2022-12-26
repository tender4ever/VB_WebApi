Imports VB_WebApi.WebApi

Public Class WinForm



#Region "Attribute"

    Private aWebApi As VB_WebApi.WebApi

    Public Delegate Sub UpdateUI(ByVal sMessage As String, ByVal sControl As Control)


#End Region



#Region "Method"


    ''' <summary>
    ''' 處理 System Message
    ''' </summary>
    ''' <param name="sMessage"></param>
    ''' <remarks></remarks>
    Private Sub processSysMessage(ByVal sMessage As String)

        ' 處理更新 UI
        ProcessUpdateUI(sMessage, tbxSystemMessage)

    End Sub


    ''' <summary>
    ''' 處理更新 UI
    ''' </summary>
    ''' <param name="sMessage"></param>
    ''' <param name="sControl"></param>
    ''' <remarks></remarks>
    Private Sub ProcessUpdateUI(ByVal sMessage As String, ByVal sControl As Control)

        If Me.InvokeRequired() Then
            Dim cb As New UpdateUI(AddressOf ProcessUpdateUI)
            Me.Invoke(cb, sMessage, sControl)
        Else
            sControl.Text = sControl.Text + sMessage + vbLf
        End If

    End Sub



#End Region


#Region "Event"


    ''' <summary>
    ''' 關閉視窗
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub WinForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        If aWebApi IsNot Nothing Then
            aWebApi.StopWebServer()
        End If

    End Sub


    ''' <summary>
    ''' Start
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click

        If aWebApi Is Nothing Then

            aWebApi = New VB_WebApi.WebApi
            AddHandler aWebApi.OnSysMessage, New VB_WebApi.WebApi.SysMessageHandler(AddressOf processSysMessage)

        End If
        
        aWebApi.StartWebApiServer(tbxLocalIP.Text, Convert.ToInt16(tbxLocalPort.Text), tbxLocalURL.Text)

        btnStart.Enabled = False
        btnStop.Enabled = True

    End Sub


    ''' <summary>
    ''' Stop
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click

        If aWebApi IsNot Nothing Then
            aWebApi.StopWebServer()
        End If

        btnStart.Enabled = True
        btnStop.Enabled = False

    End Sub


    ''' <summary>
    ''' Send Link Test
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLinkTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLinkTest.Click

        If aWebApi Is Nothing Then

            aWebApi = New VB_WebApi.WebApi
            AddHandler aWebApi.OnSysMessage, New VB_WebApi.WebApi.SysMessageHandler(AddressOf processSysMessage)

        End If

        aWebApi.SendLinkTest(tbxRemoteIP.Text, Convert.ToInt16(tbxRemotePort.Text), tbxRemoteURL.Text)

    End Sub



#End Region



    
End Class
