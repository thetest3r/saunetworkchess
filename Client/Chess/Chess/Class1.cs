using System;
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
using System.ComponentModel;

namespace Checkmate_
{
    public class CheckmateClient
    {
        private TcpClient server;
        static NetworkStream stream;
        private FormThread form;

        public CheckmateClient()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new FormThread();
            ThreadStart ts = new ThreadStart(form.StartUiThread);
            Thread t = new Thread(ts);
            t.Start();
        }
        public void ConnecttoServer()
        {
            server = new TcpClient();
            //Needs the I.P. Address of our samuel server
            
            try
            {
                server.Connect("127.0.0.1", 1991);
            }
            catch(Exception ex)
            {
                return;
            }
            stream = server.GetStream();
            var thread = new Thread(ListenforServer);
            thread.Start();
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
                    // 2 is a reply from a move form(1|bool)
                    if (msg[0] == '2')
                    {
                        if (msg[1] == '0')
                        {
                            form._form.IpBoxMessage("Invalid Move");

                        }
                        else if (msg[1] == '1')
                        {
                            form._form.IpBoxMessage("Move Accepted");
                        }
                        else
                            MessageBox.Show("OpCode 2 - Reply not recognized");
                    }
                        // 3 is when the other player moves form(3|int|int)
                    else if(msg[0]== '3')
                    {

                    }
                        // 4 declares check form(4|int|bool)
                    else if (msg[0] == '4')
                    {

                    }
                        // 5 player wins - 0 for white 1 for black form(5|bool)
                    else if (msg[0] == '5')
                    {

                    }
                        // 6 for end game form(6|)
                    else if (msg[0] == '6')
                    {

                    }
                        // 7 is to begin a game form(7|bool)
                    else if (msg[0] == '7')
                    {

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
        public class FormThread
           {
            public Form1 _form;
            public FormThread(){

            }
     public void StartUiThread(){
         _form = new Form1();
        Application.Run(_form);
        }
    }
}