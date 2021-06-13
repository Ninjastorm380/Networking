﻿Public Class TestServer : Inherits Networking.Bases.Server
    Public Overrides Sub Run(Client As Networking.TcpClient)
        Dim Limiter As New Networking.Governors.LoopGovernor(10)
        Client.UseBufferedChannels = False
        Do While Client.Connected = True And Online = True
            If Client.HasMessage = True Then
                Dim ReceivedData As Byte()() = Nothing
                Client.ReadJagged(ReceivedData)
                Dim ReceivedMessage As String = Text.Encoding.ASCII.GetString(ReceivedData(0))
                Select Case ReceivedMessage
                    Case "test message"
                        MsgBox("server received test message, responding...", MsgBoxStyle.OkOnly, "TestForm - test server")
                        Client.WriteJagged({Text.Encoding.ASCII.GetBytes("test response")})
                End Select
            End If
            Limiter.Limit()
        Loop
    End Sub
End Class
