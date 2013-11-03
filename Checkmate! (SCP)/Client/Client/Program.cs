using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkmate_;

namespace Client
{
    class Program
    {
        static CheckmateClient client;
        static void Main(string[] args)
        {
            client = new CheckmateClient();
            client.ConnecttoServer();
        }
    }
}
