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
        ' TODO: Remove the below line when done testing
        'Console.WriteLine(sender.request.requesturi.absolutepath.ToString())



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

        args.argObject = encoders_array
        If sender.GetType() Is GetType(libOwinRestApi.EncodersController) Then
            Return args.argObject
        ElseIf sender <= encoders_array.Length() Then
            Return args.argObject(sender - 1)
        Else
            Dim myObj As New Object()
            myObj = New String("Boo, nothing was found")
            args.argObject = myObj
            Return args
        End If
    End Function


End Module

