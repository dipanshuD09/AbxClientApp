using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbxClientApp.Models
{
    public class Packet
    {
        public string Symbol { get; set; }
        public char BuySellIndicator { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Sequence { get; set; }
    }
}