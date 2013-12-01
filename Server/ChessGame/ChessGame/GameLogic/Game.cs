using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ChessGame.GameLogic
{
    public class Game
    {
        private Board board;
        public enum Locations { a1, a2, a3, a4, a5, a6, a7, a8, b1, b2, b3, b4, b5, b6, b7, b8, c1, c2, c3, c4, c5, c6, c7, c8, d1, d2, d3, d4, d5, d6, d7, d8, e1, e2, e3, e4, e5, e6, e7, e8, f1, f2, f3, f4, f5, f6, f7, f8, g1, g2, g3, g4, g5, g6, g7, g8, h1, h2, h3, h4, h5, h6, h7, h8, invalid };
        public enum TypeOfPiece { king, queen, rook, knight, bishop, pawn }
        public enum Team { Black, White };
        public TcpClient Player1, Player2;
        public int Id
        {
            get;
            set;
        }

        public Game(TcpClient Player1, TcpClient Player2, int identification)
        {
            this.Player1 = Player1;
            this.Player2 = Player2;
            Id = identification;
        }

        void Reset()
        {
            board = new Board();
        }

        void start()
        {

        }

        void Quit()
        {

        }

        public bool Move(Team t, Locations origin, Locations destination)
        {
            if (!board.IsSquareOccupied(origin))
            {
                return false;
            }

            Piece pieceToMove = board.GetWhatIsInSquare(origin);

            if (pieceToMove.Team != t)
            {
                return false;
            }

            bool successful = false;

            switch (pieceToMove.Type)
            {

                case TypeOfPiece.pawn:
                    successful = board.movePawn(origin, destination);
                    break;
                case TypeOfPiece.rook:
                    successful = board.moveRook(origin, destination);
                    break;
                case TypeOfPiece.bishop:
                    successful = board.moveBishiop(origin, destination);
                    break;
                case TypeOfPiece.knight:
                    successful = board.moveKnight(origin, destination);
                    break;
                case TypeOfPiece.queen:
                    successful = board.moveQueen(origin, destination);
                    break;
                case TypeOfPiece.king:
                    successful = board.moveKing(origin, destination);
                    break;

                default:
                    break;
            }


            return successful;
        }


        



    }
}
