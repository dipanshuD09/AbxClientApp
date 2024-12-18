using System;
using System.Net.Sockets;
using AbxClientApp.Models;

namespace AbxClientApp.Services
{
    public class ConnectionHandler
    {

        public TcpClient EstablishConnection(Connection conn, int maxRetries = 5, int retryDelay = 2000)
        {
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    TcpClient client = new();
                    client.Connect(conn.ServerAddress, conn.Port);
                    Console.WriteLine("Connected to the server.");
                    return client;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Connection attempt {attempt} failed: {ex.Message}");
                    if (attempt < maxRetries)
                    {
                        Console.WriteLine("Retrying...");
                        System.Threading.Thread.Sleep(retryDelay);
                    }
                }
            }

            throw new Exception("Unable to establish a connection after multiple attempts.");
        }
    }
}