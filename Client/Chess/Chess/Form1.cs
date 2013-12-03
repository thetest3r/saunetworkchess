using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Checkmate_;
using System.Net.NetworkInformation;
using System.Net;
//using System.Windows.Forms.ImageList;
//http://www.mindstick.com/Articles/73eb92cb-7e33-4de9-bf52-4bf5314f6cda/?Displaying%20an%20array%20of%20images%20in%20pictureBox%20C //Reference this
namespace Chess
{
    public partial class Form1 : Form
    {
        public enum Locations { a1, a2, a3, a4, a5, a6, a7, a8, b1, b2, b3, b4, b5, b6, b7, b8, c1, c2, c3, c4, c5, c6, c7, c8, d1, d2, d3, d4, d5, d6, d7, d8, e1, e2, e3, e4, e5, e6, e7, e8, f1, f2, f3, f4, f5, f6, f7, f8, g1, g2, g3, g4, g5, g6, g7, g8, h1, h2, h3, h4, h5, h6, h7, h8, invalid };
        private TableLayoutPanel cells;
        private Cell prevClickedCell = null;
        private Cell currentClickedCell = null;
        private bool prevTrack = false;
        public String protocolName;
        private static Cell[] cellDuplicate = new Cell[64];
        public CheckmateClient parentClient = null;

        class Cell : PictureBox
        {
            public static readonly System.Drawing.Size CellSize = new System.Drawing.Size(75, 75);
            private static List<Image> chessPieceList = new List<Image>();
            private enum pieces { WCastle, WKnight, WBishop, WKing, WQueen, WPawn, Blank, BCastle, BKnight, BBishop, BKing, BQueen, BPawn };
            private static pieces[,] boardArray = new pieces[8, 8] 
            {
            {pieces.BCastle,pieces.BKnight,pieces.BBishop, pieces.BQueen, pieces.BKing, pieces.BBishop, pieces.BKnight, pieces.BCastle},
            {pieces.BPawn, pieces.BPawn, pieces.BPawn, pieces.BPawn, pieces.BPawn, pieces.BPawn, pieces.BPawn, pieces.BPawn},
            {pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank},
            {pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank},
            {pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank},
            {pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank},
            {pieces.WPawn, pieces.WPawn, pieces.WPawn, pieces.WPawn, pieces.WPawn, pieces.WPawn, pieces.WPawn, pieces.WPawn},
            {pieces.WCastle,pieces.WKnight,pieces.WBishop, pieces.WQueen, pieces.WKing, pieces.WBishop, pieces.WKnight, pieces.WCastle}
            };
            private bool isImageLoaded = false;
            private static int cellID = 0;
            //Temp image that is loaded
            //Image temp_piece = Image.FromFile("C:\\Users\\TT3 Productions\\Documents\\Visual Studio 2012\\Projects\\Chess\\Content\\White_Pawn.png");
            public readonly int row, col;
            public Cell(int row, int col)
                : base()
            {
                if (!isImageLoaded)
                {
                    loadPNG();
                    isImageLoaded = true;
                }
                this.row = row; this.col = col;
                this.Size = CellSize;
                this.BackColor = (col % 2 == row % 2) ? Color.Black : Color.DarkGray;
                this.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Image = loadPiece(row, col);

                cellDuplicate[cellID] = this;
                cellID++;
            }
            public override string ToString() { return "Cell(" + row + "," + col + ")"; }
            public int[] ToInt()
            {
                int[] x = new int[2] { row, col };
                return x;
            }
            private static Image loadPiece(int row, int col)
            {
                switch (boardArray[row, col])
                {
                    case pieces.WPawn:
                        return chessPieceList[0];
                    case pieces.WCastle:
                        return chessPieceList[1];
                    case pieces.WKnight:
                        return chessPieceList[2];
                    case pieces.WBishop:
                        return chessPieceList[3];
                    case pieces.WQueen:
                        return chessPieceList[4];
                    case pieces.WKing:
                        return chessPieceList[5];
                    case pieces.BPawn:
                        return chessPieceList[6];
                    case pieces.BCastle:
                        return chessPieceList[7];
                    case pieces.BKnight:
                        return chessPieceList[8];
                    case pieces.BBishop:
                        return chessPieceList[9];
                    case pieces.BQueen:
                        return chessPieceList[10];
                    case pieces.BKing:
                        return chessPieceList[11];
                    default:
                        return null;
                }
            }
            private void loadPNG()
            {
                //string folder_address = "C:\\Users\\TT3 Prod0uctions\\Documents\\Visual Studio 2012\\Projects\\Chess\\Content";
                string folder_address = "Graphics\\";
                string[] pieceName = new string[12];
                //Console.WriteLine("{0}", sizeof
                pieceName[0] = "White_Pawn.png"; pieceName[1] = "White_Castle.png"; pieceName[2] = "White_Knight.png";
                pieceName[3] = "White_Bishop.png"; pieceName[4] = "White_Queen.png"; pieceName[5] = "White_King.png";

                pieceName[6] = "Black_Pawn.png"; pieceName[7] = "Black_Castle.png"; pieceName[8] = "Black_Knight.png";
                pieceName[9] = "Black_Bishop.png"; pieceName[10] = "Black_Queen.png"; pieceName[11] = "Black_King.png";
                Image temp;
                for (int i = 0; i < 12; i++)
                {
                    string temp_string = AppDomain.CurrentDomain.BaseDirectory + folder_address + pieceName[i];
                    temp = Image.FromFile(temp_string);
                    chessPieceList.Add(temp);
                    //Console.WriteLine("{0}", temp_string);
                }
            }
            //private PictureBox tempSwitchPicture;
            //private Image tempImage;
            //private PictureBox tempTemp;
            //private bool canSwitch = false;
        }

        public Form1()
        {
            //loadPNG();
            //ImageList1.ImageSize = new Size(50, 50);
            //ImageList1.Images.Add(Image.FromFile("C:\\Users\\TT3 Productions\\Documents\\Visual Studio 2012\\Projects\\Chess\\Content\\white_pawn.png"));
            InitializeComponent();

            cells = GetBoard();
            this.Controls.Add(cells);
            // Reference: http://msdn.microsoft.com/en-us/library/system.windows.forms.control.controlcollection.clear(v=vs.110).aspx (to clear form)

        }
        private TableLayoutPanel GetBoard()
        {
            TableLayoutPanel b = new TableLayoutPanel();
            b.ColumnCount = 8;
            b.RowCount = 8;
            for (int i = 0; i < b.ColumnCount; i++) { b.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Cell.CellSize.Width)); }
            for (int i = 0; i < b.RowCount; i++) { b.RowStyles.Add(new RowStyle(SizeType.Absolute, Cell.CellSize.Height)); }
            for (int row = 0; row < b.RowCount; row++)
            {
                for (int col = 0; col < b.ColumnCount; col++)
                {
                    Cell cell = new Cell(row, col);
                    cell.Click += new EventHandler(this.cell_Click);
                    b.Controls.Add(cell, col, row);
                }
            }
            b.Padding = new Padding(0);
            b.Size = new System.Drawing.Size(b.ColumnCount * Cell.CellSize.Width, b.RowCount * Cell.CellSize.Height);
            /*Graphics white_pawn = Graphics.FromImage(chessPieceList[0]);
            *white_pawn.DrawImage(chessPieceList[0], new Point(0, 0));
            *ImageList1.Draw(theGraphics, new Point(85, 85), 1);
            *Application.DoEvents();
             * */
            return b;
        }
        private void cell_Click(object sender, EventArgs e)
        {
            Cell temp = (Cell)sender;
            string sendInfo;
            IPAddrBox.Text = "CSP" + temp.ToString();
            int[] rowCol;
            if (prevClickedCell == null)
            {
                //updateNetworkInfo();
                prevClickedCell = (Cell)sender;
                temp.BackColor = System.Drawing.Color.Blue;
            }
            else if (prevClickedCell == temp)
            {
                //updateNetworkInfo();
                rowCol = prevClickedCell.ToInt();
                prevClickedCell = null;
                temp.BackColor = (rowCol[0] % 2 == rowCol[1] % 2) ? Color.Black : Color.DarkGray;
                return;
            }
            else //Needs more conditions
            {
                 
                rowCol = prevClickedCell.ToInt();
                var origin = GetLocation(rowCol[1], rowCol[0]);
                prevClickedCell.BackColor = (rowCol[0] % 2 == rowCol[1] % 2) ? Color.Black : Color.DarkGray;
                //updateNetworkInfo();

                rowCol = prevClickedCell.ToInt();
                listBox1.Items.Add(origin);
                sendInfo = Convert.ToString((int)origin) + "|";

                rowCol = temp.ToInt();
                var destination = GetLocation(rowCol[1], rowCol[0]);
                sendInfo += Convert.ToString((int)destination);
                listBox1.Items.Add(destination);
                
                listBox1.Items.Add(sendInfo);
                
                currentClickedCell = temp;

                if(parentClient.isConnected())
                    parentClient.SendMovetoServer(sendInfo);


                //IpBoxMessage(sendInfo);
                //parentClient.SendtoServer(rowCol[0], rowCol[1]);
                /*
                 *Locations x = Locations.a1;
                  string y = Convert.ToString((int) x) + "|";
                 */
                /*
                rowCol = prevClickedCell.ToInt();
                temp.Image = prevClickedCell.Image;
                prevClickedCell.BackColor = (rowCol[0] % 2 == rowCol[1] % 2) ? Color.Black : Color.DarkGray;
                prevClickedCell.Image = null;
                prevClickedCell = null;
                 * */

            }
            //
            //if (!prevTrack)// prevTrack is 0
            //    prevClickedCell = (Cell)sender;
            //System.Diagnostics.Debug.WriteLine("Click: " + cell);
            string i = sender.ToString();
            IPAddrBox.Text = "Hello World" + i;
            //parentClient.SendtoServer(i);
            //Reference to update picture inside picture box http://stackoverflow.com/questions/9030622/how-to-refresh-picturebox
        }

        private void updateNetworkInfo()
        {
            //Ping Reference http://stackoverflow.com/questions/1281176/making-a-ping-inside-of-my-c-sharp-application
            Ping pingTime = new Ping();
            PingReply pingReply;
            try
            {
                pingReply = pingTime.Send("samuel.cs.southern.edu");
                //IPAddresses Reference http://stackoverflow.com/questions/5271724/get-all-ip-addresses-on-machine
            }
            catch (PingException ex)
            {
                Console.WriteLine(ex);
                networkingLabel.Text =
                    "You are not connected. \n" +
                    "Please check your \n" +
                    "internet connection \n";
                return;
            }
            pingReply = pingTime.Send("samuel.cs.southern.edu");

            String strHostName = Dns.GetHostName();
            //IPHostEntry ipHostEntry = Dns.GetHostByName(strHostName);
            IPHostEntry ipHostEntry = Dns.GetHostEntry(strHostName);

            strHostName = null; //Reusing the string
            foreach (IPAddress ipaddress in ipHostEntry.AddressList)
            {
                strHostName += ipaddress.ToString() + "  ";
            }

            networkingLabel.Text =
                "Connected to: \n" +
                "samuel.cs.southern.edu \n \n" +
                "RTT Time: " + pingReply.RoundtripTime.ToString() + "ms \n \n" +
                "Local IP: " + strHostName + "\n" +
                "Protocol: " + protocolName + "n";
            //Add whichi protocols is using
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //int i = int.Parse(IPAddrBox.Text);
            //i += 1;
            //IPAddrBox.Text = i.ToString();
            parentClient.ConnecttoServer();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void IpBoxMessage(string msg)
        {
            listBox1.Items.Add(msg);
            //IPAddrBox.Text = msg;
            return;
        }
        public void validMove()
        {
            Cell temp = prevClickedCell;
            currentClickedCell.Image = temp.Image;
            prevClickedCell.Image = null;
            prevClickedCell = null;
            currentClickedCell = null;
        }

        public void pieceInCheck(int location)
        {
            int xlocation, ylocation;
            xlocation = GetLocX((Locations)location);
            ylocation = GetLocY((Locations)location);
            cellDuplicate[xlocation + (ylocation * 8)].BackColor = System.Drawing.Color.Red;
        }
        public void oppenentsMove(int currentPos, int newPos)
        {
            int xcurrent, ycurrent, xnewPos, ynewPos;
            xcurrent = GetLocX((Locations)currentPos);
            ycurrent = GetLocY((Locations)currentPos);

            xnewPos = GetLocX((Locations)newPos);
            ynewPos = GetLocY((Locations)newPos);

            Cell temp = cellDuplicate[xnewPos+(ynewPos*8)];
            cellDuplicate[xnewPos + (ynewPos * 8)].Image = cellDuplicate[xcurrent + (ycurrent * 8)].Image;
            cellDuplicate[xcurrent + (ycurrent * 8)].Image = null;
            return;
        }

        public void attachParentNetwork(CheckmateClient parent)
        {
            this.parentClient = parent;
        }
        private Locations GetLocation(int x, int y)
        {
            // X is the column
            // Y is the row
            // X is the column
            // Y is the row

            switch (x)
            {
                case 0:
                    switch (y)
                    {
                        case 7:
                            return Locations.a1;
                        case 6:
                            return Locations.a2;
                        case 5:
                            return Locations.a3;
                        case 4:
                            return Locations.a4;
                        case 3:
                            return Locations.a5;
                        case 2:
                            return Locations.a6;
                        case 1:
                            return Locations.a7;
                        case 0:
                            return Locations.a8;
                        default:
                            return Locations.invalid;
                    }
                case 1:
                    switch (y)
                    {
                        case 7:
                            return Locations.b1;
                        case 6:
                            return Locations.b2;
                        case 5:
                            return Locations.b3;
                        case 4:
                            return Locations.b4;
                        case 3:
                            return Locations.b5;
                        case 2:
                            return Locations.b6;
                        case 1:
                            return Locations.b7;
                        case 0:
                            return Locations.b8;
                        default:
                            return Locations.invalid;

                    }
                case 2:
                    switch (y)
                    {
                        case 7:
                            return Locations.c1;
                        case 6:
                            return Locations.c2;
                        case 5:
                            return Locations.c3;
                        case 4:
                            return Locations.c4;
                        case 3:
                            return Locations.c5;
                        case 2:
                            return Locations.c6;
                        case 1:
                            return Locations.c7;
                        case 0:
                            return Locations.c8;
                        default:
                            return Locations.invalid;

                    }
                case 3:
                    switch (y)
                    {
                        case 7:
                            return Locations.d1;
                        case 6:
                            return Locations.d2;
                        case 5:
                            return Locations.d3;
                        case 4:
                            return Locations.d4;
                        case 3:
                            return Locations.d5;
                        case 2:
                            return Locations.d6;
                        case 1:
                            return Locations.d7;
                        case 0:
                            return Locations.d8;
                        default:
                            return Locations.invalid;

                    }
                case 4:
                    switch (y)
                    {
                        case 7:
                            return Locations.e1;
                        case 6:
                            return Locations.e2;
                        case 5:
                            return Locations.e3;
                        case 4:
                            return Locations.e4;
                        case 3:
                            return Locations.e5;
                        case 2:
                            return Locations.e6;
                        case 1:
                            return Locations.e7;
                        case 0:
                            return Locations.e8;
                        default:
                            return Locations.invalid;
                    }
                case 5:
                    switch (y)
                    {
                        case 7:
                            return Locations.f1;
                        case 6:
                            return Locations.f2;
                        case 5:
                            return Locations.f3;
                        case 4:
                            return Locations.f4;
                        case 3:
                            return Locations.f5;
                        case 2:
                            return Locations.f6;
                        case 1:
                            return Locations.f7;
                        case 0:
                            return Locations.f8;
                        default:
                            return Locations.invalid;

                    };
                case 6:
                    switch (y)
                    {
                        case 7:
                            return Locations.g1;
                        case 6:
                            return Locations.g2;
                        case 5:
                            return Locations.g3;
                        case 4:
                            return Locations.g4;
                        case 3:
                            return Locations.g5;
                        case 2:
                            return Locations.g6;
                        case 1:
                            return Locations.g7;
                        case 0:
                            return Locations.g8;
                        default:
                            return Locations.invalid;

                    }
                case 7:
                    switch (y)
                    {
                        case 7:
                            return Locations.h1;
                        case 6:
                            return Locations.h2;
                        case 5:
                            return Locations.h3;
                        case 4:
                            return Locations.h4;
                        case 3:
                            return Locations.h5;
                        case 2:
                            return Locations.h6;
                        case 1:
                            return Locations.h7;
                        case 0:
                            return Locations.h8;
                        default:
                            return Locations.invalid;
                    }
                default:
                    return Locations.invalid;

            }
        }
        private int GetLocX(Locations loc)
        {
            switch (loc)
            {
                case Locations.a1:
                case Locations.a2:
                case Locations.a3:
                case Locations.a4:
                case Locations.a5:
                case Locations.a6:
                case Locations.a7:
                case Locations.a8:
                    return 0;
                case Locations.b1:
                case Locations.b2:
                case Locations.b3:
                case Locations.b4:
                case Locations.b5:
                case Locations.b6:
                case Locations.b7:
                case Locations.b8:
                    return 1;

                case Locations.c1:
                case Locations.c2:
                case Locations.c3:
                case Locations.c4:
                case Locations.c5:
                case Locations.c6:
                case Locations.c7:
                case Locations.c8:
                    return 2;

                case Locations.d1:
                case Locations.d2:
                case Locations.d3:
                case Locations.d4:
                case Locations.d5:
                case Locations.d6:
                case Locations.d7:
                case Locations.d8:
                    return 3;

                case Locations.e1:
                case Locations.e2:
                case Locations.e3:
                case Locations.e4:
                case Locations.e5:
                case Locations.e6:
                case Locations.e7:
                case Locations.e8:
                    return 4;

                case Locations.f1:
                case Locations.f2:
                case Locations.f3:
                case Locations.f4:
                case Locations.f5:
                case Locations.f6:
                case Locations.f7:
                case Locations.f8:
                    return 5;

                case Locations.g1:
                case Locations.g2:
                case Locations.g3:
                case Locations.g4:
                case Locations.g5:
                case Locations.g6:
                case Locations.g7:
                case Locations.g8:
                    return 6;

                case Locations.h1:
                case Locations.h2:
                case Locations.h3:
                case Locations.h4:
                case Locations.h5:
                case Locations.h6:
                case Locations.h7:
                case Locations.h8:
                    return 7;


                default:
                    return -1;
            }
        }

        private int GetLocY(Locations loc)
        {
            switch (loc)
            {
                case Locations.a1:
                case Locations.b1:
                case Locations.c1:
                case Locations.d1:
                case Locations.e1:
                case Locations.f1:
                case Locations.g1:
                case Locations.h1:
                    return 7;

                case Locations.a2:
                case Locations.b2:
                case Locations.c2:
                case Locations.d2:
                case Locations.e2:
                case Locations.f2:
                case Locations.g2:
                case Locations.h2:
                    return 6;

                case Locations.a3:
                case Locations.b3:
                case Locations.c3:
                case Locations.d3:
                case Locations.e3:
                case Locations.f3:
                case Locations.g3:
                case Locations.h3:
                    return 5;

                case Locations.a4:
                case Locations.b4:
                case Locations.c4:
                case Locations.d4:
                case Locations.e4:
                case Locations.f4:
                case Locations.g4:
                case Locations.h4:
                    return 4;

                case Locations.a5:
                case Locations.b5:
                case Locations.c5:
                case Locations.d5:
                case Locations.e5:
                case Locations.f5:
                case Locations.g5:
                case Locations.h5:
                    return 3;

                case Locations.a6:
                case Locations.b6:
                case Locations.c6:
                case Locations.d6:
                case Locations.e6:
                case Locations.f6:
                case Locations.g6:
                case Locations.h6:
                    return 2;

                case Locations.a7:
                case Locations.b7:
                case Locations.c7:
                case Locations.d7:
                case Locations.e7:
                case Locations.f7:
                case Locations.g7:
                case Locations.h7:
                    return 1;

                case Locations.a8:
                case Locations.b8:
                case Locations.c8:
                case Locations.d8:
                case Locations.e8:
                case Locations.f8:
                case Locations.g8:
                case Locations.h8:
                    return 0;

                default:
                    return -1;
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            parentClient.ExitApplication();
        }
        public void resetClickedCells()
        {
            prevClickedCell = null;
            currentClickedCell = null;
        }

    }
}
