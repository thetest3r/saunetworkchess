using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Chess
{
    public partial class Form1 : Form
    {
        private TableLayoutPanel cells;
        class Cell : PictureBox
        {
            public static readonly System.Drawing.Size CellSize = new System.Drawing.Size(50, 50);
            public readonly int row, col;
            public Cell(int row, int col)
                : base()
            {
                this.row = row; this.col = col;
                this.Size = CellSize;
                this.BackColor = (col % 2 == row % 2) ? Color.Black : Color.White;
            }
            public override string ToString() { return "Cell(" + row + "," + col + ")"; }
        }

        public Form1()
        {
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
            return b;
        }

        private void cell_Click(object sender, EventArgs e)
        {
            Cell cell = (Cell)sender;
            System.Diagnostics.Debug.WriteLine("Click: " + cell);
        }

    }
}
