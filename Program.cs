using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using AbxClientApp.Models;
using AbxClientApp.Services;


try
{
    // Initialize services
    var connectionHandler = new ConnectionHandler();
    var requestSender = new RequestSender();
    var packetReceiver = new PacketReceiver();
    var packetValidator = new PacketValidator();

    var conn = new Connection();

    Console.WriteLine("Please enter Server Address/IP:");
    conn.ServerAddress = Console.ReadLine();
    Console.WriteLine("Please enter Port Number:");
    if(int.TryParse(Console.ReadLine(), out int port)) conn.Port = port;

    // Step 1: Connect to the server
    using TcpClient client = connectionHandler.EstablishConnection(conn);
    using NetworkStream stream = client.GetStream();

    // Step 2: Request "Stream All Packets"
    requestSender.SendRequest(stream, 1, 0);

    // Step 3: Receive and parse responses
    List<Packet> packets = packetReceiver.ReceivePackets(stream);

    // Step 4: Handle missing sequences
    List<int> missingSequences = packetValidator.FindMissingSequences(packets);
    foreach (int seq in missingSequences)
    {
        // Request each missing sequence
        requestSender.SendRequest(stream, 2, seq);
        packets.Add(packetReceiver.ReceivePacket(stream));
    }

    // Step 5: Validate and sort packets
    packets = packetValidator.ValidateAndSortPackets(packets);

    // Step 6: Generate JSON file
    string json = JsonSerializer.Serialize(packets, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText("output.json", json);
    Console.WriteLine("Output saved to output.json");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}