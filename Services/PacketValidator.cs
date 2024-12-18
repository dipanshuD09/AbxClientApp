using System;
using System.Collections.Generic;
using System.Linq;
using AbxClientApp.Models;

namespace AbxClientApp.Services
{
    public class PacketValidator
    {
        public List<int> FindMissingSequences(List<Packet> packets)
        {
            packets.Sort((a, b) => a.Sequence.CompareTo(b.Sequence));
            List<int> missing = new List<int>();

            for (int i = packets[0].Sequence; i <= packets[^1].Sequence; i++)
            {
                if (!packets.Exists(p => p.Sequence == i))
                    missing.Add(i);
            }

            return missing;
        }

        public bool ValidatePacket(Packet packet)
        {
            if (packet.Symbol.Length != 4)
            {
                Console.WriteLine("Invalid symbol length.");
                return false;
            }
            if (packet.Quantity < 0 || packet.Price < 0)
            {
                Console.WriteLine("Invalid quantity or price.");
                return false;
            }
            return true;
        }

        public List<Packet> ValidateAndSortPackets(List<Packet> packets)
        {
            return packets.Where(ValidatePacket).OrderBy(p => p.Sequence).ToList();
        }
    }
}