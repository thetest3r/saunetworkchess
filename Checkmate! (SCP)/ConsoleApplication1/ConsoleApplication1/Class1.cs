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
    class CheckmateServer
    {
        public static Hashtable ClientList = new Hashtable();
        public static TcpListener serverSocket;

        public static void startListening()
        {
            serverSocket = new TcpListener(IPAddress.Any, 1991);
            serverSocket.Start();
            Console.WriteLine("Chat server started...");
            while (true)
            {
                //This next line of code actually blocks
                TcpClient clientSocket = serverSocket.AcceptTcpClient();
                //Somebody connected and set us data
                string dataFromClient = RecieveString(clientSocket);
                ClientList.Add(dataFromClient, clientSocket);
                Console.WriteLine(dataFromClient + " joined cat room.");
                var client = new HandleClient();
                client.StartClient(clientSocket, dataFromClient);
            }
        }

        public static void SendString(string msg, string uname, bool flag)
        {
            foreach (DictionaryEntry item in ClientList)
            {
                var broadcastSocket = (TcpClient)item.Value;
                NetworkStream broadcastStream = broadcastSocket.GetStream();
                byte[] broadcastBytes = flag ? Encoding.ASCII.GetBytes(uname + " says: " + msg)
                    : Encoding.ASCII.GetBytes(msg);
                broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                broadcastStream.Flush();
            }
        }

        public static string RecieveString(TcpClient clientSocket)
        {
            var bytesFrom = new byte[16384];
            NetworkStream networkStream = clientSocket.GetStream();
            networkStream.Read(bytesFrom, 1, 60);

            string dataFromClient = Encoding.ASCII.GetString(bytesFrom);
            return dataFromClient.Substring(0, dataFromClient.IndexOf("$", StringComparison.Ordinal));
        }

        public class HandleClient
        {
            private TcpClient _clientSocket;
            private string _clientNumber;

            public void StartClient(TcpClient clientSocket, string clientNumber)
            {
                _clientNumber = clientNumber;
                _clientSocket = clientSocket;
                var thread = new Thread(RecieveInstructions);
                thread.Start();
            }

            private void RecieveInstructions()
            {
                var bytesFrom = new byte[16384];
                int requestCount = 0;
                while (true)
                {
                    try
                    {
                        requestCount += 1;
                        string dataFromClient = CheckmateServer.RecieveString(_clientSocket);
                        Console.WriteLine("From Client - " + _clientNumber + ": " + dataFromClient);
                        CheckmateServer.SendString(dataFromClient, _clientNumber, true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }


        public class CheckmateClient
        {
            private TcpClient _socket;
            private NetworkStream _stream;
            private bool _connected;

            public CheckmateClient()
            {
                _connected = false;
            }

            public bool Connected
            {
                get { return _connected; }
                set
                {
                    _connected = value;
                }
            }

            public void ConnecttoServer()
            {
                _socket = new TcpClient();
                //Needs the I.P. Address of our samuel server
                _socket.Connect("127.0.0.1", 8888);
                _stream = _socket.GetStream();
                Connected = true;
                var thread = new Thread(ListentoServer);
                thread.Start();
            }
            public void UpdateClientList()
            {

            }
            //Pass in tcpClient to start it. Will return 0 on success.
            public void ListentoServer()
            {

                return;
            }
        }
	}
}