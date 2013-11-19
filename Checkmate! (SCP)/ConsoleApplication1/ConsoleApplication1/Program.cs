using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkmate_;

namespace ConsoleApplication1 
{
    class Program
    {

        static CheckmateServer server;
        static void Main(string[] args)
        {
            CheckmateServer.startListening();
        }
    }
}
