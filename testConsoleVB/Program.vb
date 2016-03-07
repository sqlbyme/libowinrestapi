Imports System
Imports libOwinRestApi
Module Program
    Public es As New EventSender


    Private Class clsData
        Property name As String
        Property id As Int32
    End Class



    Sub Main()
        Dim args As New myEventArgs
        args.argValue = "Mike"
        RemoveHandler es.StringReturnEvent, AddressOf e_StringReturnEvent
        AddHandler es.StringReturnEvent, AddressOf e_StringReturnEvent
        es.start(args)
        Console.ReadLine()
        es.trigger(args)
        Console.ReadLine()

    End Sub

    Public Sub e_StringReturnEvent(sender As Object, args As myEventArgs)
        Console.WriteLine(args.argValue.ToString())
        Console.WriteLine("Event fired from within the main program")
    End Sub


End Module
