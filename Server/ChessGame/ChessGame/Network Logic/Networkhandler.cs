using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChessGame.Network_Logic
{
    class Networkhandler
    {

        public static Hashtable ClientList = new Hashtable();
        public static TcpListener serverSocket;

        public static void startListening()
        {
            serverSocket = new TcpListener(IPAddress.Any, 1991);
            serverSocket.Start();
            Console.WriteLine("Checkmate! server started...");
            int i = 1;
            while (true)
            {
                //This next line of code actually blocks
                TcpClient clientSocket = serverSocket.AcceptTcpClient();
                //Somebody connected and set us data
                //string dataFromClient = RecieveString(clientSocket);
                ClientList.Add("player" + i.ToString(), clientSocket);
                Console.WriteLine("player" + i.ToString() + " joined the lobby.");
                var client = new HandleClient();
                client.StartClient(clientSocket, "player" + i.ToString());
                UpdateClientsList();
                i++;
            }
        }

        public static void UpdateClientsList()
        {
            string list = "6";
            foreach(DictionaryEntry item in ClientList)
            {
                int i = 0;
                list += "player" + i + "*";
                    i++;
            }
            foreach (DictionaryEntry item in ClientList)
            {
                var broadcastSocket = (TcpClient)item.Value;
                NetworkStream broadcastStream = broadcastSocket.GetStream();
                byte[] broadcastBytes = Encoding.ASCII.GetBytes(list);
                broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                broadcastStream.Flush();
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

        public static void SendMessage(string opCode, string msg)
        {
            string message = opCode + msg;
            foreach (DictionaryEntry item in ClientList)
            {
                var broadcastSocket = (TcpClient)item.Value;
                NetworkStream broadcastStream = broadcastSocket.GetStream();
                byte[] broadcastBytes = Encoding.ASCII.GetBytes(message);
                broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                broadcastStream.Flush();
            }
        }

        public static string RecieveString(TcpClient clientSocket)
        {
            var bytesFrom = new byte[clientSocket.ReceiveBufferSize];
            NetworkStream networkStream = clientSocket.GetStream();
            networkStream.Read(bytesFrom, 0, clientSocket.ReceiveBufferSize);

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
                        string dataFromClient = Networkhandler.RecieveString(_clientSocket);
                        //Opcode of 0 is for sending a message
                        if (dataFromClient[0] == '0')
                        {
                            //dataFromClient = dataFromClient.TrimStart('0');
                            Console.WriteLine("From Client - " + _clientNumber + ": " + dataFromClient);
                            dataFromClient += "From Client " + _clientNumber;
                            SendString(dataFromClient, _clientNumber, false);
                        }//Opcode 1 is for making a move
                        else if (dataFromClient[0] == '1')
                        {
                            dataFromClient = dataFromClient.TrimStart('1');
                            Console.WriteLine("From Client - " + _clientNumber + ": " + dataFromClient);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        if (_clientSocket.Connected == false)
                        {
                            Console.WriteLine("removing client");
                            ClientList.Remove(_clientSocket);
                            return;
                        }

                    }
                }
            }
        }
    }
}
