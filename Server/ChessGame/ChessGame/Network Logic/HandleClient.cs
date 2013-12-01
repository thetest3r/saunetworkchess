﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ChessGame.Network_Logic;
using ChessGame.GameLogic;

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
        public void replyToMove(bool valid)
        {

        }
        public void declareCheck(int location, bool player)
        {

        }
        public void playerWins(bool player)
        {


            string msg ="";
            if (player == true)
                msg += '1';
            else
                msg += '0';
            //Networkhandler.SendInstruction(4,)

        }


        public void RecieveInstructions()
            {
                //var bytesFrom = new byte[16384];
                int requestCount = 0;
                //GameLogic.Game g;
                //bool isP1Turn = false;
                //for (int i = 0; i < games.Count; i++)
                //{
                //    if (games[i].Player1 == playerId)
                //    {
                //        isP1Turn = true;
                //        g = games[i];
                //    }
                //}
                //if (!isP1Turn)
                //{
                //    for (int i = 0; i < games.Count; i++)
                //    {
                //        if (games[i].Player2 == playerId)
                //        {
                //            g = games[i];
                //        }
                //    }
                //}
                while (true)
                {
                    try
                    {
                        requestCount += 1;
                        string dataFromClient = Networkhandler.RecieveString(_clientSocket);
                        //Opcode of 8 is for attempting a move form(8|int|int)
                        if (dataFromClient[0] == '8')
                        {
                            dataFromClient = dataFromClient.Trim('|');
                            string[] positions = dataFromClient.Split('|');
                            //YourEnum foo = (YourEnum)Enum.Parse(typeof(YourEnum), yourString);
                            //ChessGame.GameLogic.Game.Locations origin = (ChessGame.GameLogic.Game.Locations)Enum.Parse(typeof(ChessGame.GameLogic.Game.Locations), positions[0]);
                            //ChessGame.GameLogic.Game.Locations newPos = (ChessGame.GameLogic.Game.Locations)Enum.Parse(typeof(ChessGame.GameLogic.Game.Locations), positions[1]);
                            //bool valid = game.Move(team, origin, newPos);
                            foreach(string i in positions)
                            {
                                Console.WriteLine(i);
                            }
                            bool valid = true;
                            if (valid)
                            {
                                ReturnValidMove(_clientSocket, valid);
                                if (game.Player1 == _clientSocket)
                                {
                                    //form(3|int|int)
                                    OpponentsMove(game.Player2, int.Parse(positions[0]), int.Parse(positions[1]));
                                }
                                else
                                    OpponentsMove(game.Player1, int.Parse(positions[0]), int.Parse(positions[1]));
                            }
                            else
                                ReturnValidMove(_clientSocket, valid);
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
                            //ClientList.Remove(_clientSocket);
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
