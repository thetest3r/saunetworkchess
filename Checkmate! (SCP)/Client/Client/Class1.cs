using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Checkmate_
{
    public class CheckmateClient
    {
        private TcpClient server;
        static NetworkStream stream;

        public void ConnecttoServer()
        {
            server = new TcpClient();
            //Needs the I.P. Address of our samuel server
            server.Connect("10.9.68.36", 1991);
            stream = server.GetStream();
            var thread = new Thread(ListenforServer);
            thread.Start();
            SendtoServer("hello");
        }
        public void UpdateClientList()
        {

        }
        //Pass in tcpClient to start it. Will return 0 on success.
        public void SendtoServer(string msg)
        {
            while (true)
            {
            Console.WriteLine("message: ");
            string mssg = Console.ReadLine();
            mssg = mssg + "$";
            byte[] bytes = Encoding.ASCII.GetBytes(mssg);
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
            }
           
        }

        public void ListenforServer()
        {
            Console.Write("I'm listening");
            try
            {
                var bytes = new byte[server.ReceiveBufferSize];
                stream.Read(bytes, 0, server.ReceiveBufferSize);
                string msg = Encoding.ASCII.GetString(bytes);
                int index = msg.IndexOf("$") > 0 ? msg.IndexOf("$")
                    : msg.IndexOf('\0');
                Console.WriteLine(msg);
            }
            catch (SocketException se)
            {
                Console.WriteLine(se);
            }
            
            return;
        }
    }
}