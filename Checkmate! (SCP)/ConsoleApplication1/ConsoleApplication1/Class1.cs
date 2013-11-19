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
            Console.WriteLine("Chat server started...");
            while (true)
            {
                //This next line of code actually blocks
                TcpClient clientSocket = serverSocket.AcceptTcpClient();
                //Somebody connected and set us data
                string dataFromClient = RecieveString(clientSocket);
                ClientList.Add(dataFromClient, clientSocket);
                Console.WriteLine(dataFromClient + " joined chat room.");
                var client = new HandleClient();
                client.StartClient(clientSocket, dataFromClient);
                SendString(dataFromClient + " joined.", dataFromClient, true);
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
                        Console.WriteLine("From Client - " + _clientNumber + ": " + dataFromClient);
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