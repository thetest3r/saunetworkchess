using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessGame.GameLogic
{
    class Piece
    {
        // Each player begins the game with 16 pieces: one king, one queen, two rooks, two knights, two bishops, and eight pawns.
        

        public ChessGame.GameLogic.Game.TypeOfPiece Type { get; private set; }
        public ChessGame.GameLogic.Game.Team Team { get; private set; }


        public Piece(ChessGame.GameLogic.Game.TypeOfPiece t, ChessGame.GameLogic.Game.Team color)
        {
            Type = t;
        }

        public void Promote()
        {

        }

        public void Move()
        {

        }


    }
}
