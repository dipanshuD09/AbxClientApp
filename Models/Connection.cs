using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbxClientApp.Models
{
    public class Connection
    {
        public string ServerAddress { get; set; } = "localhost";
        public int Port { get; set; } = 3000;
    }
}