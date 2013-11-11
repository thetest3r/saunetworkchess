using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using System.Windows.Forms.ImageList;
//http://www.mindstick.com/Articles/73eb92cb-7e33-4de9-bf52-4bf5314f6cda/?Displaying%20an%20array%20of%20images%20in%20pictureBox%20C //Reference this
namespace Chess
{
    public partial class Form1 : Form
    {
        private TableLayoutPanel cells;

        class Cell : PictureBox
        {
            public static readonly System.Drawing.Size CellSize = new System.Drawing.Size(75, 75);
            private static List<Image> chessPieceList = new List<Image>();
            private enum pieces { WCastle, WKnight, WBishop, WKing, WQueen, WPawn, Blank, BCastle, BKnight, BBishop, BKing, BQueen, BPawn };
            private static  pieces[,] boardArray = new pieces[8, 8] 
            {
            {pieces.BCastle,pieces.BKnight,pieces.BBishop, pieces.BKing, pieces.BQueen, pieces.BBishop, pieces.BKnight, pieces.BCastle},
            {pieces.BPawn, pieces.BPawn, pieces.BPawn, pieces.BPawn, pieces.BPawn, pieces.BPawn, pieces.BPawn, pieces.BPawn},
            {pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank},
            {pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank},
            {pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank},
            {pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank, pieces.Blank},
            {pieces.WPawn, pieces.WPawn, pieces.WPawn, pieces.WPawn, pieces.WPawn, pieces.WPawn, pieces.WPawn, pieces.WPawn},
            {pieces.WCastle,pieces.WKnight,pieces.WBishop, pieces.WKing, pieces.WQueen, pieces.WBishop, pieces.WKnight, pieces.WCastle}
            };
            private bool isImageLoaded = false;
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
            }
            public override string ToString() { return "Cell(" + row + "," + col + ")"; }
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
                string folder_address = "C:\\Users\\TT3 Productions\\Documents\\Visual Studio 2012\\Projects\\Chess\\Content";
                string[] pieceName = new string[12];
                //Console.WriteLine("{0}", sizeof
                pieceName[0] = "\\White_Pawn.png"; pieceName[1] = "\\White_Castle.png"; pieceName[2] = "\\White_Knight.png";
                pieceName[3] = "\\White_Bishop.png"; pieceName[4] = "\\White_Queen.png"; pieceName[5] = "\\White_King.png";

                pieceName[6] = "\\Black_Pawn.png"; pieceName[7] = "\\Black_Castle.png"; pieceName[8] = "\\Black_Knight.png";
                pieceName[9] = "\\Black_Bishop.png"; pieceName[10] = "\\Black_Queen.png"; pieceName[11] = "\\Black_King.png";
                Image temp;
                string temp_string;
                for (int i = 0; i < 12; i++)
                {
                    temp_string = (folder_address + pieceName[i]);
                    temp = Image.FromFile(temp_string);
                    chessPieceList.Add(temp);
                    //Console.WriteLine("{0}", temp_string);
                }
            }
            private PictureBox tempSwitchPicture;
            private Image tempImage;
            private PictureBox tempTemp;
            private bool canSwitch = false;

            private void PictureClick(object sender, MouseEventArgs e)
            {
                if (!canSwitch)
                {
                    tempSwitchPicture = (PictureBox)sender;
                    tempImage = tempSwitchPicture.Image;
                    canSwitch = true;
                }
                if (canSwitch)
                {
                    if (DoDragDrop(tempSwitchPicture, DragDropEffects.Move) == DragDropEffects.Move)
                    {
                        tempTemp = (PictureBox)sender;
                        tempTemp.Image = tempImage;
                    }
                }
            }
            private void pieceTracker()
            {

            }

            public void dostuff()
            {

            }

        }

        public Form1()
        {
            //loadPNG();
            //ImageList1.ImageSize = new Size(50, 50);
            //ImageList1.Images.Add(Image.FromFile("C:\\Users\\TT3 Productions\\Documents\\Visual Studio 2012\\Projects\\Chess\\Content\\white_pawn.png"));
            InitializeComponent();
            cells = GetBoard();
            this.Controls.Add(cells);
            
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
                    cell.Click += new EventHandler(cell_Click);
                    b.Controls.Add(cell, col, row);
                }
            }
            b.Padding = new Padding(0);
            b.Size = new System.Drawing.Size(b.ColumnCount * Cell.CellSize.Width, b.RowCount * Cell.CellSize.Height);
            //Graphics white_pawn = Graphics.FromImage(chessPieceList[0]);
            //white_pawn.DrawImage(chessPieceList[0], new Point(0, 0));
            //ImageList1.Draw(theGraphics, new Point(85, 85), 1);
            //Application.DoEvents();
            return b;
        }

        private void cell_Click(object sender, EventArgs e)
        {
            Cell cell = (Cell)sender;
            System.Diagnostics.Debug.WriteLine("Click: " + cell);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
