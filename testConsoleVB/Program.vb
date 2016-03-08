Imports System
Imports libOwinRestApi
Class ExampleObject
    Property name As String
    Property myId As Int32
End Class
Module Program
    Public clsMine As New ExampleObject()
    Public Sub Main()

        clsMine.name = "Main Program"
        clsMine.myId = 911

        RemoveHandler RESTEvent.e_RESTEvent.getRequested, AddressOf e_OnGetRequested
        AddHandler RESTEvent.e_RESTEvent.getRequested, AddressOf e_OnGetRequested
        RESTEvent.e_RESTEvent.start()
        Console.ReadLine()

    End Sub

    Public Function e_OnGetRequested(sender As Object, args As RESTEventArgs)
        args.argObject = clsMine
        Return args
    End Function


End Module

