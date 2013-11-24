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
using System.Windows.Forms;
using Chess;

namespace Checkmate_
{
    public class CheckmateClient
    {
        private TcpClient server;
        static NetworkStream stream;
        private ListBox parentListBox;

        public void ConnecttoServer()
        {
            server = new TcpClient();
            //Needs the I.P. Address of our samuel server
            server.Connect("127.0.0.1", 1991);
            stream = server.GetStream();
            var thread = new Thread(ListenforServer);
            thread.Start();
        }
        public void ConnectForm(ref ListBox form)
        {
            parentListBox = form;
            return;
        }
        public void UpdateClientList()
        {

        }
        //Pass in tcpClient to start it. Will return 0 on success.
        public void SendtoServer(string opcode,string msg)
        {
            string data = opcode + msg + "$";
            byte[] bytes = Encoding.ASCII.GetBytes(data);
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }

        public void ListenforServer()
        {
            try
            {
                while (true)
                {
                    var bytes = new byte[server.ReceiveBufferSize];
                    stream.Read(bytes, 0, server.ReceiveBufferSize);
                    string msg = Encoding.ASCII.GetString(bytes);
                    int index = msg.IndexOf("$") > 0 ? msg.IndexOf("$")
                        : msg.IndexOf('\0');
                    // 0 is a message
                    if (msg[0] == '0')
                    {
                        MessageBox.Show(msg);
                    }
                        // 6 is to send a players name and add it to the listbox
                    else if(msg[0]== '6')
                    {
                        msg = msg.TrimStart('6');
                        while (msg.Length > 0)
                        {
                            string player = "";
                            for (int i = 0; i < msg.Length; i++)
                            {
                                if (msg[i] != '*')
                                    player += msg[i];
                                else
                                    msg = msg.TrimStart('*');
                            }
                            //parentListBox.Items.Add(player);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("You've been disconnected.");
                MessageBox.Show(ex.ToString());
                return;
            }
            return;
        }
    }
}