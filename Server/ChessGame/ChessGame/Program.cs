using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessGame.Network_Logic;

namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Networkhandler.startListening();
        }
    }
}
