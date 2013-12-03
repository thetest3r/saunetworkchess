using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Chess;

namespace Checkmate_
{
    public class CheckmateClient
    {
        private TcpClient server;
        static NetworkStream stream;
        private FormThread form;
        bool inCheck = false;

        public CheckmateClient()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new FormThread();
            ThreadStart ts = new ThreadStart(form.StartUiThread);
            Thread t = new Thread(ts);
            t.Start();
            Thread.Sleep(2000);
            form._form.parentClient = this;
        }
        public void ConnecttoServer()
        {
            if (server != null)
            {
                form._form.IpBoxMessage("You are already connected");
                return;
            }
                
            server = new TcpClient();
            //Needs the I.P. Address of our samuel server
            try
            {
                server.Connect("127.0.0.1", 1991);
                form._form.IpBoxMessage("Connected to Server");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                form._form.IpBoxMessage("Unable to Reach Server");
                return;
            }
            stream = server.GetStream();
            var thread = new Thread(ListenforServer);
            thread.Start();
        }

        public void SendResignation(string data)
        {
            string data1 = "9|$";
            byte[] bytes = Encoding.ASCII.GetBytes(data1);
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }

        //Pass in tcpClient to start it. Will return 0 on success.
        public void SendMovetoServer(string data)
        {
            string data1 = "8|" + data + "$";
            form._form.IpBoxMessage(data);
            byte[] bytes = Encoding.ASCII.GetBytes(data1);
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }
        public void ExitApplication()
        {
            Environment.Exit(0);
        }

        public bool isConnected()
        {
            if (server == null)
                return false;
            return server.Connected;
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
                    // 2 is a reply from a move form(2|bool)
                    form._form.IpBoxMessage(msg);
                    if (msg[0] == '2')
                    {
                        if (msg[2] == '0')
                        {
                            form._form.IpBoxMessage("Invalid Move");
                            form._form.resetClickedCells();

                        }
                        else if (msg[2] == '1')
                        {
                            form._form.IpBoxMessage("Move Accepted");
                            form._form.validMove();
                        }
                        else
                            MessageBox.Show("OpCode 2 - Reply not recognized");
                    }
                    // 3 is when the other player moves form(3|int|int)
                    else if (msg[0] == '3')
                    {
                        string[] positions = msg.Split('|');
                        form._form.oppenentsMove(int.Parse(positions[1]), int.Parse(positions[2]));
                    }
                    // 4 declares check form(4|int|bool)
                    else if (msg[0] == '4')
                    {
                        string[] message = msg.Split('|');
                        if (message[2] == "0")
                        {
                            form._form.IpBoxMessage("White is in check!");
                        }
                        else if (message[2] == "1")
                        {
                            form._form.IpBoxMessage("Black is in check!");
                        }
                        else
                            MessageBox.Show("OpCode 4 - Reply not recognized");
                        form._form.pieceInCheck(int.Parse(message[1]));
                    }
                    // 5 player wins - 0 for white 1 for black form(5|bool)
                    else if (msg[0] == '5')
                    {
                        if (msg[2] == '0')
                        {
                            form._form.IpBoxMessage("Checkmate!\nWhite Wins!");

                        }
                        else if (msg[2] == '1')
                        {
                            form._form.IpBoxMessage("Checkmate!\nBlack Wins!");
                        }
                        else
                            MessageBox.Show("OpCode 2 - Reply not recognized");
                    }
                    // 6 for end game form(6|)
                    else if (msg[0] == '6')
                    {
                        MessageBox.Show("The other player quit.");
                        ExitApplication();
                        return;
                    }
                    // 7 is to begin a game form(7|bool|int)
                    else if (msg[0] == '7')
                    {
                        if (msg[2] == '0')
                        {
                            form._form.IpBoxMessage("The game has begun. You are white. It's black's turn.");

                        }
                        else if (msg[2] == '1')
                        {
                            form._form.IpBoxMessage("The game has begun. You are black. It's your turn.");
                        }
                        else
                            MessageBox.Show("OpCode 2 - Reply not recognized");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("You've been disconnected.");
                //MessageBox.Show(ex.ToString());
                Application.Exit();
                return;
            }
        }
    }
    public class FormThread
    {
        public Form1 _form;
        public FormThread()
        {

        }
        public void StartUiThread()
        {
            _form = new Form1();
            Application.Run(_form);
        }
    }
}