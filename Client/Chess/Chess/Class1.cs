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
            server.Connect("127.0.0.1", 1991);
            stream = server.GetStream();
            //var thread = new Thread(ListenforServer);
            //thread.Start();
        }
        public void UpdateClientList()
        {

        }
        //Pass in tcpClient to start it. Will return 0 on success.
        public void SendtoServer(string opcode,string msg)
        {
            string data = opcode + msg + "$";
            byte[] bytes = Encoding.ASCII.GetBytes(data);
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }

        public void ListenforServer()
        {
            while (true)
            {
                Console.Write("I'm listening");
                var bytes = new byte[server.ReceiveBufferSize];
                stream.Read(bytes, 0, server.ReceiveBufferSize);
                string msg = Encoding.ASCII.GetString(bytes);
                int index = msg.IndexOf("$") > 0 ? msg.IndexOf("$")
                    : msg.IndexOf('\0');
            }
            return;
        }
    }
}