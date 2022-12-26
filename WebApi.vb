Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.Xml
Imports System.Collections.Concurrent
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq


Public Class WebApi


#Region "Enum"

    ''' <summary>
    ''' 列舉 : Http Method
    ''' </summary>
    Public Enum enumHttpMethod

        HttpPost
        HttpGet

    End Enum


    ''' <summary>
    ''' 列舉 : Write Log
    ''' </summary>
    Public Enum enumWriteLog

        SendHttpRequest
        ReceiveHttpResponse
        ReceiveHttpRequest
        SendHttpResponse

    End Enum


#End Region


#Region "Event"

    Public Delegate Sub SysMessageHandler(ByVal sMessage As String)

    Public Event OnSysMessage As SysMessageHandler

#End Region


#Region "Attribute"

    Private aTCPListener As TcpListener

    Private aLocalIPAddress As IPAddress            ' Local IP
    Private intLocalPort As Integer                 ' Local Port
    Private strLocalBaseURL As String               ' Local Base URL

    Private aRemoteIPAddress As IPAddress           ' Remote IP
    Private intRemotePort As Integer                ' Remote Port
    Private strRemoteBaseURL As String              ' Remote Base URL

    Private aProcessThread As Thread
    Private aListenThread As Thread

    Private aMessageQueue As ConcurrentQueue(Of Socket)


#End Region


#Region "Constructor"


    ''' <summary>
    ''' 建構子
    ''' </summary>
    Public Sub New()

        aMessageQueue = New ConcurrentQueue(Of Socket)

    End Sub


#End Region


#Region "Method"


    ''' <summary>
    ''' 開始 Web Api Server
    ''' </summary>
    Public Function StartWebApiServer(ByVal sLocalIPAddress As String, ByVal sLocalPort As Integer, ByVal sLocalBaseURL As String) As Boolean

        Dim sFunName As String = New StackTrace(True).GetFrame(0).GetMethod().Name

        Try
            ' 檢查是否有 TCP Listener
            If aTCPListener Is Nothing Then

                aLocalIPAddress = IPAddress.Parse(sLocalIPAddress)
                intLocalPort = sLocalPort
                strLocalBaseURL = sLocalBaseURL

                aTCPListener = New TcpListener(aLocalIPAddress, intLocalPort)
                aTCPListener.Start()

                aListenThread = New Thread(AddressOf StartListen)
                aListenThread.Start()

                aProcessThread = New Thread(AddressOf ProcessSocket)
                aProcessThread.Start()

                Return True

            Else
                SendSysMessage("Already start the Web Api Server!")

            End If

            Return False

        Catch ex As Exception
            SendSysMessage(sFunName + " Exception : " + ex.ToString())
            Return False
        End Try

    End Function


    ''' <summary>
    ''' 關閉監聽 Http Request
    ''' </summary>
    Public Sub StopWebServer()

        Dim sFunName As String = New StackTrace(True).GetFrame(0).GetMethod().Name

        Try
            aTCPListener.Stop()
            aTCPListener = Nothing

            aListenThread.Abort()
            aProcessThread.Abort()

        Catch ex As Exception
            SendSysMessage(sFunName + " Exception : " + ex.ToString())
        End Try

    End Sub


    ''' <summary>
    ''' Send
    ''' </summary>
    Public Function Send(ByVal sRemoteIPAddress As String, ByVal sRemotePort As Integer, ByVal sRemoteBaseURL As String, ByVal sTitle As String, ByVal sJObject As JObject) As JObject

        Dim sFunName As String = New StackTrace(True).GetFrame(0).GetMethod().Name

        Try

            ' Http Request
            Dim request As WebRequest = WebRequest.Create("http://" + sRemoteIPAddress.ToString + ":" + sRemotePort.ToString + sRemoteBaseURL + sTitle)
            ' Http Method
            request.Method = "POST"
            request.ContentType = "application/json"

            ' Send Http Request
            SendHttpRequest(request, sJObject)


            ' Receive Http Response
            Dim response As WebResponse = request.GetResponse()
            Dim stream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(stream)

            Dim receiveObject As JObject = JsonConvert.DeserializeObject(reader.ReadToEnd)

            Return receiveObject

        Catch ex As Exception
            SendSysMessage(sFunName + " Exception : " + ex.ToString())
            Return Nothing
        End Try

    End Function


    ''' <summary>
    ''' 傳送 Sys Message 給外部
    ''' </summary>
    ''' <param name="sMessage"></param>
    ''' <remarks></remarks>
    Private Shadows Sub SendSysMessage(ByVal sMessage As String)

        Try
            RaiseEvent OnSysMessage(sMessage)

        Catch ex As Exception

        End Try

    End Sub



#End Region


#Region "Thread Method"


    ''' <summary>
    ''' 開始監聽 Http Request
    ''' </summary>
    Private Sub StartListen()

        Do While True

            'accept new socket connection
            Dim mySocket As Socket = aTCPListener.AcceptSocket

            If mySocket.Connected Then

                Thread.Sleep(50)

                aMessageQueue.Enqueue(mySocket)

            End If

        Loop

    End Sub


    ''' <summary>
    ''' 處理 Socket
    ''' </summary>
    Private Sub ProcessSocket()

        Do While True

            If aMessageQueue.Count > 0 Then

                Dim mySocket As Socket = Nothing
                aMessageQueue.TryDequeue(mySocket)

                Thread.Sleep(500)

                ' 檢查 Socket 是否已經收完
                'If mySocket.Available <= 186 Then
                '    aMessageQueue.Enqueue(mySocket)
                '    Continue Do
                'End If

                ' Receive Http Request
                Dim bReceive() As Byte = New [Byte](1024) {}
                Dim i As Integer = mySocket.Receive(bReceive, bReceive.Length, 0)
                Dim sBuffer As String = Encoding.ASCII.GetString(bReceive)

                Dim HttpVersion As String = ""              ' HTTP 版本
                Dim HttpMethod As enumHttpMethod            ' HTTP Method
                Dim HttpBaseURL As String = ""              ' HTTP Base URL
                Dim HttpFunctionName As String = ""         ' HTTP Function Name
                Dim Content As String = ""                  ' Content
                Dim sErrorMessage As String = ""            ' Error Message

                ' 檢查 Http Request
                If CheckHttpRequest(sBuffer, HttpVersion, HttpMethod, HttpBaseURL, HttpFunctionName, Content) = False Then

                    sErrorMessage = "404 Error! File Does Not Exists..."
                    SendHeader(HttpVersion, "", sErrorMessage.Length, " 404 Not Found", mySocket)
                    SendToBrowser(sErrorMessage, mySocket)

                    mySocket.Close()
                    mySocket.Dispose()
                    Continue Do

                End If

                ' Receive Request
                ' 記錄 Log
                Dim receiveObject As JObject = JsonConvert.DeserializeObject(Content)
                WriteLog(enumWriteLog.ReceiveHttpRequest, receiveObject)

                ' 處理 Http Request
                Select Case HttpMethod

                    Case enumHttpMethod.HttpPost

                        ' 處理 Http Request Post
                        ProcessHttpRequestPost(mySocket, HttpVersion, HttpFunctionName, Content)

                    Case enumHttpMethod.HttpGet


                    Case Else


                End Select

                mySocket.Close()
                mySocket.Dispose()

            End If

        Loop

    End Sub


    ''' <summary>
    ''' 處理 Http Request Post
    ''' </summary>
    Protected Overridable Sub ProcessHttpRequestPost(ByRef sSocket As Socket, ByVal sHttpVersion As String, ByVal sFunctionName As String, ByVal sContent As String)

        Dim sFunName As String = New StackTrace(True).GetFrame(0).GetMethod().Name

        Try

            Select Case sFunctionName

                Case "LinkTest/", "LinkTest"

                    ' 處理 Link Test
                    ProcessLinkTest(sSocket, sHttpVersion, sFunctionName, sContent)

            End Select

        Catch ex As Exception
            SendSysMessage(sFunName + " Exception : " + ex.ToString())
        End Try

    End Sub


    ''' <summary>
    ''' Send Http Request
    ''' </summary>
    ''' <param name="sRequest"></param>
    ''' <param name="sJObject"></param>
    Protected Overridable Sub SendHttpRequest(ByVal sRequest As WebRequest, ByVal sJObject As JObject)

        Dim sFunName As String = New StackTrace(True).GetFrame(0).GetMethod().Name

        Try
            Dim postData As String = JsonConvert.SerializeObject(sJObject)
            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)

            sRequest.ContentLength = byteArray.Length

            Dim stream As Stream = sRequest.GetRequestStream()
            stream.Write(byteArray, 0, byteArray.Length)
            stream.Close()

            ' Send Request
            ' 記錄 Log
            WriteLog(enumWriteLog.SendHttpRequest, sJObject)

        Catch ex As Exception
            SendSysMessage(sFunName + " Exception : " + ex.ToString())
        End Try

    End Sub


    ''' <summary>
    ''' 記錄 Http Log
    ''' </summary>
    ''' <param name="sJObject"></param>
    Protected Sub WriteLog(ByVal sWriteLog As enumWriteLog, ByVal sJObject As JObject)

        Dim sFunName As String = New StackTrace(True).GetFrame(0).GetMethod().Name

        Try
            Dim tempWriteLog As String = ""

            Select Case sWriteLog
                Case enumWriteLog.SendHttpRequest
                    tempWriteLog = "Send HttpRequest"
                Case enumWriteLog.SendHttpResponse
                    tempWriteLog = "Send HttpResponse"
                Case enumWriteLog.ReceiveHttpRequest
                    tempWriteLog = "Receive HttpRequest"
                Case enumWriteLog.ReceiveHttpResponse
                    tempWriteLog = "Receive HttpResponse"
            End Select

            Dim Message As String = JsonConvert.SerializeObject(sJObject, Newtonsoft.Json.Formatting.None)
            Dim Datetime_Message As String = Now.ToString("yyyy/MM/dd HH:mm:ss") + " : " + tempWriteLog + " " + Message

            SendSysMessage(Datetime_Message)

        Catch ex As Exception
            SendSysMessage(sFunName + " Exception : " + ex.ToString())
        End Try

    End Sub



#Region "   Process Socket"

    ''' <summary>
    ''' 檢查 Http Request
    ''' </summary>
    Private Function CheckHttpRequest(ByVal sBuffer As String, ByRef sHttpVersion As String, ByRef sHttpMethod As enumHttpMethod, ByRef sHttpBaseURL As String, ByRef sHttpFunctionName As String, ByRef sContent As String) As Boolean

        Dim sFunName As String = New StackTrace(True).GetFrame(0).GetMethod().Name

        Try
            ' Http URL Position
            Dim HttpURL_Position As Integer = sBuffer.IndexOf("/", 1)

            ' Http Method
            Dim tempHttpMethod As String = sBuffer.Substring(0, HttpURL_Position)
            If tempHttpMethod = "POST " Then
                sHttpMethod = enumHttpMethod.HttpPost
            ElseIf tempHttpMethod = "GET " Then
                sHttpMethod = enumHttpMethod.HttpGet
            Else
                Return False
            End If

            sBuffer = sBuffer.Substring(HttpURL_Position)

            ' Http Version Position
            Dim HttpVersion_Position As Integer = sBuffer.IndexOf("HTTP", 1)

            ' Http URL
            sHttpBaseURL = sBuffer.Substring(0, HttpVersion_Position)
            sHttpBaseURL = sBuffer.Substring(0, HttpVersion_Position - 1)
            sHttpBaseURL.Replace("\\", "/")
            If (sHttpBaseURL.IndexOf(".") < 1) And (Not (sHttpBaseURL.EndsWith("/"))) Then
                sHttpBaseURL = sHttpBaseURL & "/"
            End If

            ' Http Version
            sHttpVersion = sBuffer.Substring(HttpVersion_Position, 8)

            ' Function Name
            If sHttpBaseURL.StartsWith(strLocalBaseURL) Then
                sHttpFunctionName = sHttpBaseURL.Replace(strLocalBaseURL, "")
            Else
                Return False
            End If

            ' Content 起始位置
            Dim ContentStartPosition As Integer = sBuffer.IndexOf("{", 1)
            ' Content
            sContent = sBuffer.Substring(ContentStartPosition)

            Return True

        Catch ex As Exception
            SendSysMessage(sFunName + " Exception : " + ex.ToString())
            Return False
        End Try

    End Function


    ''' <summary>
    ''' 檢查 Http Response
    ''' </summary>
    ''' <param name="sFunctionName"></param>
    ''' <returns></returns>
    Protected Function CheckHttpResponse(ByVal sFunctionName As String, ByVal sJObject As JObject) As Boolean

        Dim sFunName As String = New StackTrace(True).GetFrame(0).GetMethod().Name

        Try
            ' Receive Response
            ' 記錄 Log
            WriteLog(enumWriteLog.ReceiveHttpResponse, sJObject)

            Select Case sFunctionName

                Case "LinkTest/", "LinkTest"

                    Return True

            End Select

            Return False

        Catch ex As Exception
            SendSysMessage(sFunName + " Exception : " + ex.ToString())
            Return False
        End Try

    End Function


    ''' <summary>
    ''' 設定 Response 的 Header
    ''' </summary>
    ''' <param name="sHttpVersion"></param>
    ''' <param name="sMimeHeader"></param>
    ''' <param name="iTotalBytes"></param>
    ''' <param name="sStatusCode"></param>
    ''' <param name="thisSocket"></param>
    Protected Sub SendHeader(ByVal sHttpVersion As String, ByVal sMimeHeader As String, ByVal iTotalBytes As Integer, ByVal sStatusCode As String, ByRef thisSocket As Socket)

        Dim sBuffer As String = ""

        If Len(sMimeHeader) = 0 Then

            sMimeHeader = "text/html"
        End If

        sBuffer = sHttpVersion & sStatusCode & vbCrLf & "Server: X10CamControl" & vbCrLf & "Content-Type: " & sMimeHeader & vbCrLf & "Accept-Ranges: bytes" & vbCrLf & "Content-Length: " & iTotalBytes & vbCrLf & vbCrLf

        Dim bSendData As [Byte]() = Encoding.ASCII.GetBytes(sBuffer)

        SendToBrowser(bSendData, thisSocket)

    End Sub


    ''' <summary>
    ''' Send Response
    ''' </summary>
    ''' <param name="sData"></param>
    ''' <param name="thisSocket"></param>
    Protected Overloads Sub SendToBrowser(ByVal sData As String, ByRef thisSocket As Socket)

        ' Send Response
        ' 記錄 Log
        Dim SendObject As JObject = JsonConvert.DeserializeObject(sData)
        WriteLog(enumWriteLog.SendHttpResponse, SendObject)

        SendToBrowser(Encoding.ASCII.GetBytes(sData), thisSocket)

    End Sub


    ''' <summary>
    ''' Send Response
    ''' </summary>
    ''' <param name="bSendData"></param>
    ''' <param name="thisSocket"></param>
    Protected Overloads Sub SendToBrowser(ByVal bSendData As [Byte](), ByRef thisSocket As Socket)

        Dim iNumBytes As Integer = 0

        If thisSocket.Connected Then

            If (iNumBytes = thisSocket.Send(bSendData, bSendData.Length, 0)) = -1 Then

                'socket error can't send packet
            Else
                'number of bytes sent.
            End If
        Else
            'connection dropped.
        End If

    End Sub


#End Region



#Region "   Send Request"

    ''' <summary>
    ''' Send Link Test 
    ''' </summary>
    Public Sub SendLinkTest(ByVal sRemoteIPAddress As String, ByVal sRemotePort As Integer, ByVal sRemoteBaseURL As String)

        Dim sFunName As String = New StackTrace(True).GetFrame(0).GetMethod().Name

        Try

            ' Http Request
            Dim request As WebRequest = WebRequest.Create("http://" + sRemoteIPAddress.ToString + ":" + sRemotePort.ToString + sRemoteBaseURL + "LinkTest")
            ' Http Method
            request.Method = "POST"
            request.ContentType = "application/json"

            Dim sendObject As JObject = New JObject
            sendObject.Add("LinkTest", "Test")

            ' Send Http Request
            SendHttpRequest(request, sendObject)


            ' Receive Http Response
            Dim response As WebResponse = request.GetResponse()
            Dim stream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(stream)

            Dim receiveObject As JObject = JsonConvert.DeserializeObject(reader.ReadToEnd)

            CheckHttpResponse("LinkTest", receiveObject)


        Catch ex As Exception
            SendSysMessage(sFunName + " Exception : " + ex.ToString())
        End Try

    End Sub


#End Region



#Region "   Receive Request"

    ''' <summary>
    ''' 處理 Link Test
    ''' </summary>
    ''' <param name="sSocket"></param>
    ''' <param name="sHttpVersion"></param>
    ''' <param name="sFunctionName"></param>
    ''' <param name="sContent"></param>
    Private Sub ProcessLinkTest(ByRef sSocket As Socket, ByVal sHttpVersion As String, ByVal sFunctionName As String, ByVal sContent As String)

        Dim sFunName As String = New StackTrace(True).GetFrame(0).GetMethod().Name

        Try

            ' Http Request
            Dim receiveObject As JObject = JsonConvert.DeserializeObject(sContent)

            Dim strLinkTest As String = receiveObject("LinkTest")


            ' Http Response
            Dim sendObject As JObject = New JObject
            sendObject.Add("return", strLinkTest)

            Dim sResponse As String = JsonConvert.SerializeObject(sendObject)
            SendHeader(sHttpVersion, "", sResponse.Length, " 200 OK", sSocket)
            SendToBrowser(sResponse, sSocket)


        Catch ex As Exception
            SendSysMessage(sFunName + " Exception : " + ex.ToString())
        End Try

    End Sub


#End Region




#End Region


End Class
