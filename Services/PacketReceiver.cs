using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using AbxClientApp.Models;

namespace AbxClientApp.Services
{
    public class PacketReceiver
    {
        public List<Packet> ReceivePackets(NetworkStream stream)
        {
            List<Packet> packets = new List<Packet>();
            byte[] buffer = new byte[17];

            try
            {
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    if (bytesRead == 17)
                    {
                        packets.Add(ParsePacket(buffer));
                    }
                    else
                    {
                        Console.WriteLine("Received a packet with invalid size. Discarding.");
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error while receiving packets: {ex.Message}");
            }

            return packets;
        }

        public Packet ReceivePacket(NetworkStream stream)
        {
            byte[] buffer = new byte[17];
            _ = stream.Read(buffer, 0, buffer.Length);
            return ParsePacket(buffer);
        }

        private Packet ParsePacket(byte[] buffer)
        {
            return new Packet
            {
                Symbol = Encoding.ASCII.GetString(buffer[..4]),
                BuySellIndicator = (char)buffer[4],
                Quantity = BitConverter.ToInt32(buffer[5..9].Reverse().ToArray()),
                Price = BitConverter.ToInt32(buffer[9..13].Reverse().ToArray()),
                Sequence = BitConverter.ToInt32(buffer[13..17].Reverse().ToArray())
            };
        }
    }
}