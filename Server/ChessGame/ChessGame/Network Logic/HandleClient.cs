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

        private void RecieveInstructions(int playerId)
            {
                var bytesFrom = new byte[16384];
                int requestCount = 0;
                GameLogic.Game g;
                bool isP1Turn = false;
                for (int i = 0; i < games.Count; i++)
                {
                    if (games[i].Player1 == playerId)
                    {
                        isP1Turn = true;
                        g = games[i];
                    }
                }
                if (!isP1Turn)
                {
                    for (int i = 0; i < games.Count; i++)
                    {
                        if (games[i].Player2 == playerId)
                        {
                            g = games[i];
                        }
                    }
                }
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
                        else if (dataFromClient[0] == '8')
                        {
                            // do stuff with message.
                            
                            
                            // Do stuff with game

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
