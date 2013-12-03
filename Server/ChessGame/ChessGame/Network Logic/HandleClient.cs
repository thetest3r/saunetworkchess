using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChessGame.Network_Logic
{
    public class HandleClient
    {
        private ChessGame.GameLogic.Game.Team team;
        private TcpClient _clientSocket;
        private string _clientNumber;
        private ChessGame.GameLogic.Game game;
        public TcpClient getClient()
        {
            return _clientSocket;
        }
        public void AssaignClient(TcpClient clientSocket, string clientNumber)
        {
            _clientNumber = clientNumber;
            _clientSocket = clientSocket;
            
        }
        public void AssaignGame(ref ChessGame.GameLogic.Game newGame, ChessGame.GameLogic.Game.Team t)
        {
            game = newGame;
            team = t;
        }

        public void StartClient()
        {
            var thread = new Thread(RecieveInstructions);
            thread.Start();
        }
        public void RecieveInstructions()
            {
                int requestCount = 0;
                while (true)
                {
                    try
                    {
                        requestCount += 1;
                        string dataFromClient = NetworkHandler.RecieveString(_clientSocket);
                        //Opcode of 8 is for attempting a move form(8|int|int)
                        if (dataFromClient[0] == '8')
                        {
                            Console.WriteLine(dataFromClient);
                            string[] positions = dataFromClient.Split('|');
                            //YourEnum foo = (YourEnum)Enum.Parse(typeof(YourEnum), yourString);
                            ChessGame.GameLogic.Game.Locations origin = (ChessGame.GameLogic.Game.Locations) int.Parse(positions[1]);
                            ChessGame.GameLogic.Game.Locations newPos = (ChessGame.GameLogic.Game.Locations) int.Parse(positions[2]);
                            GameLogic.Game.ResultOfMove valid = game.Move(team, origin, newPos);

                            //bool valid = true;
                            if (valid != GameLogic.Game.ResultOfMove.Failure)
                            {
                                ReturnValidMove(_clientSocket, true);
                                if (game.Player1 == _clientSocket)
                                {
                                    //form(3|int|int)
                                    OpponentsMove(game.Player2, int.Parse(positions[1]), int.Parse(positions[2]));
                                }
                                else
                                    OpponentsMove(game.Player1, int.Parse(positions[1]), int.Parse(positions[2]));

                                if(valid == GameLogic.Game.ResultOfMove.EnemyInCheck)
                                {
                                    if (game.WhatTeamIsInCheck() == GameLogic.Game.Team.White)
                                        NetworkHandler.SendCheck(game.Player1, game.Player2,(int)game.LocationOfKingInCheck(),false);
                                    else if (game.WhatTeamIsInCheck() == GameLogic.Game.Team.Black)
                                        NetworkHandler.SendCheck(game.Player1, game.Player2, (int)game.LocationOfKingInCheck(), true);
                                }
                                else if(valid == GameLogic.Game.ResultOfMove.Checkmate)
                                {
                                    if (game.WhatTeamIsInCheck() == GameLogic.Game.Team.White)
                                        NetworkHandler.PlayerWins(game.Player1, game.Player2, false);
                                    else if (game.WhatTeamIsInCheck() == GameLogic.Game.Team.Black)
                                        NetworkHandler.PlayerWins(game.Player1, game.Player2, true);
                                }
                            }
                            else
                                ReturnValidMove(_clientSocket, false);
                        }//Opcode 1 is for making a move
                        else if (dataFromClient[0] == '1')
                        {

                        }
                        else
                            Console.WriteLine("Command not recognized");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        if (_clientSocket.Connected == false)
                        {
                            Console.WriteLine("removing client");
                            NetworkHandler.removeGame(game.Id);
                            NetworkHandler.endGame(game.Player1, game.Player2);
                            return;
                        }

                    }
                }
            }

        private void ReturnValidMove(TcpClient clientSocket, bool valid)
        {
            string msg = "2|";
            if (valid)
                msg += "1";
            else
                msg += "0";
            var broadcastSocket = clientSocket;
            NetworkStream broadcastStream = broadcastSocket.GetStream();
            byte[] broadcastBytes = Encoding.ASCII.GetBytes(msg);
            broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
            broadcastStream.Flush();
        }
        private void OpponentsMove(TcpClient clientSocket, int curPos, int newPos)
        {
            string msg = "3|" + curPos.ToString() + "|" + newPos.ToString();
            var broadcastSocket = clientSocket;
            NetworkStream broadcastStream = broadcastSocket.GetStream();
            byte[] broadcastBytes = Encoding.ASCII.GetBytes(msg);
            broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
            broadcastStream.Flush();
        }
    }
}
