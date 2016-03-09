Imports System
Imports libOwinRestApi
Class Encoder
    Property name As String
    Property encId As Int32
    Property status As Boolean
End Class
Module Program
    Public encoders_array(2) As Encoder
    Public Sub Main()
        RemoveHandler RESTEvent.e_RESTEvent.getRequested, AddressOf e_OnGetRequested
        AddHandler RESTEvent.e_RESTEvent.getRequested, AddressOf e_OnGetRequested
        Dim myServer As New ApiServer()
        myServer.ServerStart()
        Console.ReadLine()
        myServer.ServerStop()
        Console.ReadLine()

    End Sub

    Public Function e_OnGetRequested(sender As Object, args As RESTEventArgs)

        Dim encoder1 As New Encoder()
        encoder1.encId = 1
        encoder1.name = "Upstage"
        encoder1.status = True

        Dim encoder2 As New Encoder()
        encoder2.encId = 2
        encoder2.name = "Dnstage"
        encoder2.status = True

        Dim encoder3 As New Encoder()
        encoder3.encId = 3
        encoder3.name = "Off Stage"
        encoder3.status = False

        encoders_array(0) = New Encoder()
        encoders_array(1) = New Encoder()
        encoders_array(2) = New Encoder()

        encoders_array(0) = encoder1
        encoders_array(1) = encoder2
        encoders_array(2) = encoder3

        args.response = encoders_array

        If sender.GetType() Is GetType(libOwinRestApi.EncodersController) Then
            Return args
        ElseIf sender <= encoders_array.Length() And sender - 1 >= 0 Then
            Return args.response(sender - 1)
        Else
            Dim myObj As New Object()
            myObj = New String("Boo, nothing was found")
            args.response = myObj
            Return args
        End If

    End Function

End Module

