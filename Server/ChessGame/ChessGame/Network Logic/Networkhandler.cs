using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ChessGame.Network_Logic;

namespace ChessGame.Network_Logic
{
    class Networkhandler
    {

        public static Hashtable ClientList = new Hashtable();
        public static TcpListener serverSocket;
        List<GameLogic.Game> games = new List<GameLogic.Game>();

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
                i++;
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
            var bytesFrom = new byte[clientSocket.ReceiveBufferSize];
            NetworkStream networkStream = clientSocket.GetStream();
            networkStream.Read(bytesFrom, 0, clientSocket.ReceiveBufferSize);

            string dataFromClient = Encoding.ASCII.GetString(bytesFrom);
            return dataFromClient.Substring(0, dataFromClient.IndexOf("$", StringComparison.Ordinal));
        }
        
        
    }
}
