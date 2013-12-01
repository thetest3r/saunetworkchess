using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChessGame.Network_Logic
{
    class Networkhandler
    {

        //public static Hashtable ClientList = new Hashtable();
        public static List<HandleClient> ClientList = new List<HandleClient>();
        public static TcpListener serverSocket;
        private static List<GameLogic.Game> games = new List<GameLogic.Game>();

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
                ClientList.Add(new HandleClient());
                //ClientList.Add("player" + i.ToString(), clientSocket);
                Console.WriteLine("player" + i.ToString() + " joined the lobby.");
                var client = new HandleClient();
                ClientList[i-1].AssaignClient(clientSocket, "player" + i.ToString());
                ClientList[i - 1].StartClient();
                //client.StartClient(clientSocket, "player" + i.ToString());
                if(i % 2 == 0)
                {
                    games.Add(new GameLogic.Game(ClientList.ElementAt(i - 2).getClient(), ClientList.ElementAt(i-1).getClient()));
                    //games[0].Move(ChessGame.GameLogic.Game.Team.White, ChessGame.GameLogic.Game.Locations.a1, ChessGame.GameLogic.Game.Locations.a2);
                    ChessGame.GameLogic.Game g = games[0];
                    ClientList.ElementAt(i - 2).AssaignGame(ref g, ChessGame.GameLogic.Game.Team.White);
                    ClientList.ElementAt(i - 1).AssaignGame(ref g, ChessGame.GameLogic.Game.Team.Black);
                    beginGame(ClientList.ElementAt(i - 2).getClient(), ClientList.ElementAt(i-1).getClient());
                }
                i++;
            }
        }


        public void endGame()
        {

        }

        public static void beginGame(TcpClient client1, TcpClient client2)
        {
            string send = "7|0";
            var broadcastSocket = client1;
            NetworkStream broadcastStream = broadcastSocket.GetStream();
            byte[] broadcastBytes = Encoding.ASCII.GetBytes(send);
            broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
            broadcastStream.Flush();

            send = "7|1";
            broadcastSocket = client2;
            broadcastStream = broadcastSocket.GetStream();
            broadcastBytes = Encoding.ASCII.GetBytes(send);
            broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
            broadcastStream.Flush();
        }
        public static void SendInstruction(string opCode, string instruction, GameLogic.Game game)
        {

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
