﻿using System;
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
            Console.WriteLine("Checkmate! server started...");
            int i = 1;
            while (true)
            {
                //This next line of code actually blocks
                TcpClient clientSocket = serverSocket.AcceptTcpClient();
                //Somebody connected and set us data
                //string dataFromClient = RecieveString(clientSocket);
                ClientList.Add("player"+i.ToString(), clientSocket);
                Console.WriteLine("player" + i.ToString() + " joined the lobby.");
                var client = new HandleClient();
                client.StartClient(clientSocket, "player" + i.ToString());
                //SendString("player" + i.ToString() + " joined.", "player" + i.ToString(), true);
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
                        //Opcode of 0 is for sending a message
                        if(dataFromClient[0] == '0')
                        {
                           dataFromClient =  dataFromClient.TrimStart('0');
                            Console.WriteLine("From Client - " + _clientNumber + ": " + dataFromClient);
                        }//Opcode 1 is for making a move
                        else if(dataFromClient[0] == '1')
                        {
                            dataFromClient = dataFromClient.TrimStart('1');
                            Console.WriteLine("From Client - " + _clientNumber + ": " + dataFromClient);
                        }
                        CheckmateServer.SendString(dataFromClient, _clientNumber, true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        if(_clientSocket.Connected == false)
                        {
                            return;
                        }
                            
                    }
                }
            }
        }
    }
}