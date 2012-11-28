Imports System.Net
Imports System.Net.Sockets

Module Client

    Sub Main()
        ' Declare some passthru variables
        Dim varServer = InputBox("Address to connect to?", , "localhost")
        Dim varMessage = InputBox("Text to send?", , "I sent this at " & Now())
        Dim intPort = InputBox("Port to connect and send data on? (Default is 14000)", , "14000")
        ' Initiate the connect function
        Connect(varServer, varMessage, CInt(intPort))
    End Sub
    Sub Connect(ByVal server As [String], ByVal message As [String], ByVal intPort As Int32)
        Try
            ' Create a TcpClient. 
            ' Note, for this client to work you need to have a TcpServer  
            ' connected to the same address as specified by the server, port 
            ' combination. 
            Dim port As Int32 = intPort
            Dim client As New TcpClient(server, port)
            Console.WriteLine("Hostname: " & server)
            Console.WriteLine("Port: " & port & vbCrLf)

            ' Translate the passed message into ASCII and store it as a Byte array. 
            Dim data As [Byte]() = System.Text.Encoding.ASCII.GetBytes(message)

            ' Get a client stream for reading and writing. 
            '  Stream stream = client.GetStream(); 
            Dim stream As NetworkStream = client.GetStream()

            ' Send the message to the connected TcpServer. 
            stream.Write(data, 0, data.Length)

            Console.WriteLine("Sent to server:" & vbCrLf & vbCrLf & "{0}" & vbCrLf & vbCrLf, message)

            ' Receive the TcpServer.response. 
            ' Buffer to store the response bytes.
            data = New [Byte](256) {}

            ' String to store the response ASCII representation. 
            Dim responseData As [String] = [String].Empty

            ' Read the first batch of the TcpServer response bytes. 
            Dim bytes As Int32 = stream.Read(data, 0, data.Length)
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes)
            Console.WriteLine("Received from server:" & vbCrLf & vbCrLf & "{0}" & vbCrLf & vbCrLf, responseData)

            ' Close everything.
            stream.Close()
            client.Close()
        Catch e As ArgumentNullException
            Console.WriteLine("Error!! ArgumentNullException: {0}", e)
        Catch e As SocketException
            Console.WriteLine("Error!! SocketException: {0}", e)
        End Try

        Console.WriteLine(ControlChars.Cr + "Press Enter to GTFO...")
        Console.Read()
    End Sub 'Connect
End Module
