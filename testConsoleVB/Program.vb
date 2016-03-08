Imports System
Imports libOwinRestApi
Module Program

    Public Sub Main()

        Dim clsMine As New [myClass]()
        clsMine.name = "Main Program"
        Dim args As New myEventArgs
        args.argObject = clsMine
        RemoveHandler EventSender.myEventSender.StringReturnEvent, AddressOf e_StringReturnEvent
        AddHandler EventSender.myEventSender.StringReturnEvent, AddressOf e_StringReturnEvent
        EventSender.myEventSender.start(args)
        Console.ReadLine()
        EventSender.myEventSender.OnStringReturnEvent(EventSender.myEventSender, args)
        Console.ReadLine()

    End Sub

    Public Sub e_StringReturnEvent(sender As Object, args As myEventArgs)
        For Each item As Reflection.PropertyInfo In args.argObject.GetType().GetProperties()
            Console.WriteLine(item.Name.ToString() + ":" + item.GetValue(args.argObject, Nothing).ToString())
        Next
        Console.WriteLine("Event fired from within the main program")
    End Sub


End Module
