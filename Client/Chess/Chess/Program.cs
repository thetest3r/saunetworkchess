using System;
using Checkmate_;

namespace Chess
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CheckmateClient Client = new CheckmateClient();
        }
    }
}
