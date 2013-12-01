using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChessGame.Network_Logic
{
    /*
     * The Networkhandler handles all incoming clients and games. It provides methods.
     *
     * 
     * 
     * 
     */
    class NetworkHandler
    {
        public static List<HandleClient> ClientList = new List<HandleClient>();
        public static TcpListener serverSocket;
        int numClients = 1;
        public static void startListening()
        {
            GameManager.GameManager gameManager = GameManager.GameManager.Instance;
            serverSocket = new TcpListener(IPAddress.Any, 1991);
            serverSocket.Start();
            Console.WriteLine("Checkmate! server started...");
            int i = 1;

            while (true)
            {
                TcpClient clientSocket = serverSocket.AcceptTcpClient();
                ClientList.Add(new HandleClient());
                Console.WriteLine("player" + i.ToString() + " joined the lobby.");
                var client = new HandleClient();
                ClientList[i - 1].AssaignClient(clientSocket, "player" + i.ToString());
                ClientList[i - 1].StartClient();
                if (i % 2 == 0)
                {
                    gameManager.AddGame(new GameLogic.Game(ClientList.ElementAt(i - 2).getClient(), ClientList.ElementAt(i - 1).getClient(), i));
                    ChessGame.GameLogic.Game g = gameManager.GetLastItem();
                    ClientList.ElementAt(i - 2).AssaignGame(ref g, ChessGame.GameLogic.Game.Team.White);
                    ClientList.ElementAt(i - 1).AssaignGame(ref g, ChessGame.GameLogic.Game.Team.Black);
                    beginGame(ClientList.ElementAt(i - 2).getClient(), ClientList.ElementAt(i - 1).getClient());
                }
                i++;
            }
        }
        public static void endGame(TcpClient client1, TcpClient client2)
        {
            string send = "6|";
            SendMessage(client1, send);
            SendMessage(client2, send);
        }
        public static void removeGame(int id)
        {
            GameManager.GameManager gameManager = GameManager.GameManager.Instance;
            gameManager.RemoveGame(id);
        }

        public static void beginGame(TcpClient client1, TcpClient client2)
        {
            string send = "7|0";
            SendMessage(client1, send);
            send = "7|1";
            SendMessage(client2, send);
        }

        public static void SendCheck(TcpClient client1, TcpClient client2, int location, bool player)
        {
            string send = "4|" + location.ToString() + "|";
            if (player)
                send += '1';
            else
                send += '0';
            SendMessage(client1, send);
            SendMessage(client2, send);
        }
        public static void PlayerWins(TcpClient client1, TcpClient client2, bool player)
        {
            string send = "5|";
            if (player)
                send += '1';
            else
                send += '0';
            SendMessage(client1, send);
            SendMessage(client2, send);
        }

        public static void SendMessage(TcpClient clientSocket, string msg)
        {
            var broadcastSocket = clientSocket;
            NetworkStream broadcastStream = broadcastSocket.GetStream();
            byte[] broadcastBytes = Encoding.ASCII.GetBytes(msg);
            broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
            broadcastStream.Flush();
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
