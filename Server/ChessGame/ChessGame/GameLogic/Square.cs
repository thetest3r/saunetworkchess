using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessGame.GameLogic
{
    class Square
    {

        public bool occupied { get; private set; }

        public Piece piece { get; private set; }

        public Square()
        {
            piece = null;
            occupied = false;
        }

        public bool MoveTo(Piece p)
        {
            if (occupied)
                return false;

            piece = p;
            occupied = true;
            return true;
        }

        public Piece MoveAway()
        {
            Piece p = piece;

            piece = null;
            occupied = false;

            return p;
        }

        public bool Capture(Piece p)
        {
            if (!occupied)
                return false;
            piece = p;
            return true;
        }


    }
}
