using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessGame.GameLogic
{
    class Board
    {

        
        Square[,] board = new Square[8, 8];

        public Board()
        {
            SetUpBoard();
        }

        public void SetUpBoard()
        {
            Piece BlackRook = new Piece(Game.TypeOfPiece.rook, Game.Team.Black);
            Piece BlackKnight = new Piece(Game.TypeOfPiece.knight, Game.Team.Black);
            Piece BlackBishiop = new Piece(Game.TypeOfPiece.bishop, Game.Team.Black);
            Piece BlackKing = new Piece(Game.TypeOfPiece.king, Game.Team.Black);
            Piece BlackQueen = new Piece(Game.TypeOfPiece.queen, Game.Team.Black);
            Piece BlackPawn = new Piece(Game.TypeOfPiece.pawn, Game.Team.Black);

            Piece WhiteRook = new Piece(Game.TypeOfPiece.rook, Game.Team.White);
            Piece WhiteKnight = new Piece(Game.TypeOfPiece.knight, Game.Team.White);
            Piece WhiteBishiop = new Piece(Game.TypeOfPiece.bishop, Game.Team.White);
            Piece WhiteKing = new Piece(Game.TypeOfPiece.king, Game.Team.White);
            Piece WhiteQueen = new Piece(Game.TypeOfPiece.queen, Game.Team.White);
            Piece WhitePawn = new Piece(Game.TypeOfPiece.pawn, Game.Team.White);

            setSquare(BlackRook, Game.Locations.a8);
            setSquare(BlackKnight, Game.Locations.b8);
            setSquare(BlackBishiop, Game.Locations.c8);
            setSquare(BlackKing, Game.Locations.d8);
            setSquare(BlackQueen, Game.Locations.e8);
            setSquare(BlackBishiop, Game.Locations.f8);
            setSquare(BlackKnight, Game.Locations.g8);
            setSquare(BlackRook, Game.Locations.h8);

            setSquare(BlackPawn, Game.Locations.a7);
            setSquare(BlackPawn, Game.Locations.b7);
            setSquare(BlackPawn, Game.Locations.c7);
            setSquare(BlackPawn, Game.Locations.d7);
            setSquare(BlackPawn, Game.Locations.e7);
            setSquare(BlackPawn, Game.Locations.f7);
            setSquare(BlackPawn, Game.Locations.g7);
            setSquare(BlackPawn, Game.Locations.h7);

            setSquare(WhiteRook, Game.Locations.a1);
            setSquare(WhiteKnight, Game.Locations.b1);
            setSquare(WhiteBishiop, Game.Locations.c1);
            setSquare(WhiteKing, Game.Locations.d1);
            setSquare(WhiteQueen, Game.Locations.e1);
            setSquare(WhiteBishiop, Game.Locations.f1);
            setSquare(WhiteKnight, Game.Locations.g1);
            setSquare(WhiteRook, Game.Locations.h1);

            setSquare(WhitePawn, Game.Locations.a2);
            setSquare(WhitePawn, Game.Locations.b2);
            setSquare(WhitePawn, Game.Locations.c2);
            setSquare(WhitePawn, Game.Locations.d2);
            setSquare(WhitePawn, Game.Locations.e2);
            setSquare(WhitePawn, Game.Locations.f2);
            setSquare(WhitePawn, Game.Locations.g2);
            setSquare(WhitePawn, Game.Locations.h2);
        }

        private bool setSquare(Piece p, ChessGame.GameLogic.Game.Locations loc)
        {
            int x = GetLocX(loc);
            int y = GetLocY(loc);
            return board[x, y].MoveTo(p);
        }

        public bool IsSquareOccupied(Game.Locations loc)
        {
            int x = GetLocX(loc);
            int y = GetLocY(loc);
            return board[x, y].occupied;
        }

        public Piece GetWhatIsInSquare(Game.Locations loc)
        {
            int x = GetLocX(loc);
            int y = GetLocY(loc);
            return board[x, y].piece;
        }

        public Game.Team WhatTeamOwnsSquare(Game.Locations loc)
        {
            int x = GetLocX(loc);
            int y = GetLocY(loc);
            return board[x, y].piece.Team;
        }

        private int GetLocX(ChessGame.GameLogic.Game.Locations loc)
        {
            switch (loc)
            {
                case ChessGame.GameLogic.Game.Locations.a1:
                case ChessGame.GameLogic.Game.Locations.a2:
                case ChessGame.GameLogic.Game.Locations.a3:
                case ChessGame.GameLogic.Game.Locations.a4:
                case ChessGame.GameLogic.Game.Locations.a5:
                case ChessGame.GameLogic.Game.Locations.a6:
                case ChessGame.GameLogic.Game.Locations.a7:
                case ChessGame.GameLogic.Game.Locations.a8:
                    return 1;
                case ChessGame.GameLogic.Game.Locations.b1:
                case ChessGame.GameLogic.Game.Locations.b2:
                case ChessGame.GameLogic.Game.Locations.b3:
                case ChessGame.GameLogic.Game.Locations.b4:
                case ChessGame.GameLogic.Game.Locations.b5:
                case ChessGame.GameLogic.Game.Locations.b6:
                case ChessGame.GameLogic.Game.Locations.b7:
                case ChessGame.GameLogic.Game.Locations.b8:
                    return 2;

                case ChessGame.GameLogic.Game.Locations.c1:
                case ChessGame.GameLogic.Game.Locations.c2:
                case ChessGame.GameLogic.Game.Locations.c3:
                case ChessGame.GameLogic.Game.Locations.c4:
                case ChessGame.GameLogic.Game.Locations.c5:
                case ChessGame.GameLogic.Game.Locations.c6:
                case ChessGame.GameLogic.Game.Locations.c7:
                case ChessGame.GameLogic.Game.Locations.c8:
                    return 3;

                case ChessGame.GameLogic.Game.Locations.d1:
                case ChessGame.GameLogic.Game.Locations.d2:
                case ChessGame.GameLogic.Game.Locations.d3:
                case ChessGame.GameLogic.Game.Locations.d4:
                case ChessGame.GameLogic.Game.Locations.d5:
                case ChessGame.GameLogic.Game.Locations.d6:
                case ChessGame.GameLogic.Game.Locations.d7:
                case ChessGame.GameLogic.Game.Locations.d8:
                    return 4;

                case ChessGame.GameLogic.Game.Locations.e1:
                case ChessGame.GameLogic.Game.Locations.e2:
                case ChessGame.GameLogic.Game.Locations.e3:
                case ChessGame.GameLogic.Game.Locations.e4:
                case ChessGame.GameLogic.Game.Locations.e5:
                case ChessGame.GameLogic.Game.Locations.e6:
                case ChessGame.GameLogic.Game.Locations.e7:
                case ChessGame.GameLogic.Game.Locations.e8:
                    return 5;

                case ChessGame.GameLogic.Game.Locations.f1:
                case ChessGame.GameLogic.Game.Locations.f2:
                case ChessGame.GameLogic.Game.Locations.f3:
                case ChessGame.GameLogic.Game.Locations.f4:
                case ChessGame.GameLogic.Game.Locations.f5:
                case ChessGame.GameLogic.Game.Locations.f6:
                case ChessGame.GameLogic.Game.Locations.f7:
                case ChessGame.GameLogic.Game.Locations.f8:
                    return 6;

                case ChessGame.GameLogic.Game.Locations.g1:
                case ChessGame.GameLogic.Game.Locations.g2:
                case ChessGame.GameLogic.Game.Locations.g3:
                case ChessGame.GameLogic.Game.Locations.g4:
                case ChessGame.GameLogic.Game.Locations.g5:
                case ChessGame.GameLogic.Game.Locations.g6:
                case ChessGame.GameLogic.Game.Locations.g7:
                case ChessGame.GameLogic.Game.Locations.g8:
                    return 7;

                case ChessGame.GameLogic.Game.Locations.h1:
                case ChessGame.GameLogic.Game.Locations.h2:
                case ChessGame.GameLogic.Game.Locations.h3:
                case ChessGame.GameLogic.Game.Locations.h4:
                case ChessGame.GameLogic.Game.Locations.h5:
                case ChessGame.GameLogic.Game.Locations.h6:
                case ChessGame.GameLogic.Game.Locations.h7:
                case ChessGame.GameLogic.Game.Locations.h8:
                    return 8;


                default:
                    return -1;
            }
        }

        private int GetLocY(ChessGame.GameLogic.Game.Locations loc)
        {
            switch (loc)
            {
                case ChessGame.GameLogic.Game.Locations.a1:
                case ChessGame.GameLogic.Game.Locations.b1:
                case ChessGame.GameLogic.Game.Locations.c1:
                case ChessGame.GameLogic.Game.Locations.d1:
                case ChessGame.GameLogic.Game.Locations.e1:
                case ChessGame.GameLogic.Game.Locations.f1:
                case ChessGame.GameLogic.Game.Locations.g1:
                case ChessGame.GameLogic.Game.Locations.h1:
                    return 1;

                case ChessGame.GameLogic.Game.Locations.a2:
                case ChessGame.GameLogic.Game.Locations.b2:
                case ChessGame.GameLogic.Game.Locations.c2:
                case ChessGame.GameLogic.Game.Locations.d2:
                case ChessGame.GameLogic.Game.Locations.e2:
                case ChessGame.GameLogic.Game.Locations.f2:
                case ChessGame.GameLogic.Game.Locations.g2:
                case ChessGame.GameLogic.Game.Locations.h2:
                    return 2;

                case ChessGame.GameLogic.Game.Locations.a3:
                case ChessGame.GameLogic.Game.Locations.b3:
                case ChessGame.GameLogic.Game.Locations.c3:
                case ChessGame.GameLogic.Game.Locations.d3:
                case ChessGame.GameLogic.Game.Locations.e3:
                case ChessGame.GameLogic.Game.Locations.f3:
                case ChessGame.GameLogic.Game.Locations.g3:
                case ChessGame.GameLogic.Game.Locations.h3:
                    return 3;

                case ChessGame.GameLogic.Game.Locations.a4:
                case ChessGame.GameLogic.Game.Locations.b4:
                case ChessGame.GameLogic.Game.Locations.c4:
                case ChessGame.GameLogic.Game.Locations.d4:
                case ChessGame.GameLogic.Game.Locations.e4:
                case ChessGame.GameLogic.Game.Locations.f4:
                case ChessGame.GameLogic.Game.Locations.g4:
                case ChessGame.GameLogic.Game.Locations.h4:
                    return 4;

                case ChessGame.GameLogic.Game.Locations.a5:
                case ChessGame.GameLogic.Game.Locations.b5:
                case ChessGame.GameLogic.Game.Locations.c5:
                case ChessGame.GameLogic.Game.Locations.d5:
                case ChessGame.GameLogic.Game.Locations.e5:
                case ChessGame.GameLogic.Game.Locations.f5:
                case ChessGame.GameLogic.Game.Locations.g5:
                case ChessGame.GameLogic.Game.Locations.h5:
                    return 5;

                case ChessGame.GameLogic.Game.Locations.a6:
                case ChessGame.GameLogic.Game.Locations.b6:
                case ChessGame.GameLogic.Game.Locations.c6:
                case ChessGame.GameLogic.Game.Locations.d6:
                case ChessGame.GameLogic.Game.Locations.e6:
                case ChessGame.GameLogic.Game.Locations.f6:
                case ChessGame.GameLogic.Game.Locations.g6:
                case ChessGame.GameLogic.Game.Locations.h6:
                    return 6;

                case ChessGame.GameLogic.Game.Locations.a7:
                case ChessGame.GameLogic.Game.Locations.b7:
                case ChessGame.GameLogic.Game.Locations.c7:
                case ChessGame.GameLogic.Game.Locations.d7:
                case ChessGame.GameLogic.Game.Locations.e7:
                case ChessGame.GameLogic.Game.Locations.f7:
                case ChessGame.GameLogic.Game.Locations.g7:
                case ChessGame.GameLogic.Game.Locations.h7:
                    return 7;

                case ChessGame.GameLogic.Game.Locations.a8:
                case ChessGame.GameLogic.Game.Locations.b8:
                case ChessGame.GameLogic.Game.Locations.c8:
                case ChessGame.GameLogic.Game.Locations.d8:
                case ChessGame.GameLogic.Game.Locations.e8:
                case ChessGame.GameLogic.Game.Locations.f8:
                case ChessGame.GameLogic.Game.Locations.g8:
                case ChessGame.GameLogic.Game.Locations.h8:
                    return 8;

                default:
                    return -1;
            }
        }


        public bool movePawn(ChessGame.GameLogic.Game.Locations origin, ChessGame.GameLogic.Game.Locations destination)
        {
            int Ox = GetLocX(origin);
            int Oy = GetLocY(origin);

            int Dx = GetLocX(destination);
            int Dy = GetLocY(destination);

            bool success = false;

            ChessGame.GameLogic.Game.Team team = board[Ox, Oy].piece.Team;

            // black moves down, white moves up.

            switch (team)
            {
                case Game.Team.Black:
                    // If the pawn is moving forward. No capture.
                    if (Ox == Dx)
                    {
                        // When moving forward a pawn cannot capture.
                        if(!IsSquareOccupied(destination))
                        {
                            // Black pawns move down the board.
                            if ((Dy + 1) == Oy)
                            {
                                setSquare(GetWhatIsInSquare(origin), destination);
                                success = true;
                            }
                        }
                    }

                    // diagnal capture

                    // Black pawns move down the board.
                    if ((Dy + 1) == Oy)
                    {
                        // capture diagnal right.
                        if (Dx == (Ox + 1))
                        {
                            if (IsSquareOccupied(destination))
                            {
                                if (board[Dx, Dy].piece.Team != team)
                                {
                                    // We know that spot is ocupied by the enemy.
                                    board[Dx, Dy].Capture(board[Ox, Oy].MoveAway());
                                }
                            }
                        }
                        // capture diagnal left
                        else if (Dx == (Ox - 1))
                        {
                            if (IsSquareOccupied(destination))
                            {
                                if (board[Dx, Dy].piece.Team != team)
                                {
                                    // We know that spot is ocupied by the enemy.
                                }
                            }
                        } 
                    }

                    

                    break;

                case Game.Team.White:
                    if (Ox == Dx)
                    {
                        // When moving forward a pawn cannot capture.
                        if (!IsSquareOccupied(destination))
                        {
                            //White pawns move up the board.
                            if ((Dy - 1) == Oy)
                            {
                                setSquare(GetWhatIsInSquare(origin), destination);
                                success = true;
                            }
                        }
                    }
                    break;

                default:
                    return false;

            }

            // remember to take into account diagnal capture

            return success;
        }

        public bool moveRook(ChessGame.GameLogic.Game.Locations origin, ChessGame.GameLogic.Game.Locations destination)
        {
            int Ox = GetLocX(origin);
            int Oy = GetLocY(origin);

            int Dx = GetLocX(destination);
            int Dy = GetLocY(destination);

            return false;
        }

        public bool moveBishiop(ChessGame.GameLogic.Game.Locations origin, ChessGame.GameLogic.Game.Locations destination)
        {
            int Ox = GetLocX(origin);
            int Oy = GetLocY(origin);

            int Dx = GetLocX(destination);
            int Dy = GetLocY(destination);

            return false;
        }

        public bool moveKnight(ChessGame.GameLogic.Game.Locations origin, ChessGame.GameLogic.Game.Locations destination)
        {
            int Ox = GetLocX(origin);
            int Oy = GetLocY(origin);

            int Dx = GetLocX(destination);
            int Dy = GetLocY(destination);

            return false;
        }

        public bool moveQueen(ChessGame.GameLogic.Game.Locations origin, ChessGame.GameLogic.Game.Locations destination)
        {
            int Ox = GetLocX(origin);
            int Oy = GetLocY(origin);

            int Dx = GetLocX(destination);
            int Dy = GetLocY(destination);

            return false;
        }

        public bool moveKing(ChessGame.GameLogic.Game.Locations origin, ChessGame.GameLogic.Game.Locations destination)
        {
            int Ox = GetLocX(origin);
            int Oy = GetLocY(origin);

            int Dx = GetLocX(destination);
            int Dy = GetLocY(destination);

            return false;
        }

    }
}
