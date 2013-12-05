using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessGame.GameLogic
{
    class Board
    {
        public Game.Locations WhiteKingLoc
        {
            get;
            private set;
        }
        public Game.Locations BlackKingLoc
        {
            get;
            private set;
        }

        Square[,] board = new Square[8, 8];

        public Board()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = new Square();
                }
            }


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
            setSquare(BlackKing, Game.Locations.e8);
            BlackKingLoc = Game.Locations.e8;
            setSquare(BlackQueen, Game.Locations.d8);
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
            setSquare(WhiteKing, Game.Locations.e1);
            WhiteKingLoc = Game.Locations.e1;
            setSquare(WhiteQueen, Game.Locations.d1);
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
        // overload
        public bool IsSquareOccupied(int x, int y)
        {
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
                    return 0;
                case ChessGame.GameLogic.Game.Locations.b1:
                case ChessGame.GameLogic.Game.Locations.b2:
                case ChessGame.GameLogic.Game.Locations.b3:
                case ChessGame.GameLogic.Game.Locations.b4:
                case ChessGame.GameLogic.Game.Locations.b5:
                case ChessGame.GameLogic.Game.Locations.b6:
                case ChessGame.GameLogic.Game.Locations.b7:
                case ChessGame.GameLogic.Game.Locations.b8:
                    return 1;

                case ChessGame.GameLogic.Game.Locations.c1:
                case ChessGame.GameLogic.Game.Locations.c2:
                case ChessGame.GameLogic.Game.Locations.c3:
                case ChessGame.GameLogic.Game.Locations.c4:
                case ChessGame.GameLogic.Game.Locations.c5:
                case ChessGame.GameLogic.Game.Locations.c6:
                case ChessGame.GameLogic.Game.Locations.c7:
                case ChessGame.GameLogic.Game.Locations.c8:
                    return 2;

                case ChessGame.GameLogic.Game.Locations.d1:
                case ChessGame.GameLogic.Game.Locations.d2:
                case ChessGame.GameLogic.Game.Locations.d3:
                case ChessGame.GameLogic.Game.Locations.d4:
                case ChessGame.GameLogic.Game.Locations.d5:
                case ChessGame.GameLogic.Game.Locations.d6:
                case ChessGame.GameLogic.Game.Locations.d7:
                case ChessGame.GameLogic.Game.Locations.d8:
                    return 3;

                case ChessGame.GameLogic.Game.Locations.e1:
                case ChessGame.GameLogic.Game.Locations.e2:
                case ChessGame.GameLogic.Game.Locations.e3:
                case ChessGame.GameLogic.Game.Locations.e4:
                case ChessGame.GameLogic.Game.Locations.e5:
                case ChessGame.GameLogic.Game.Locations.e6:
                case ChessGame.GameLogic.Game.Locations.e7:
                case ChessGame.GameLogic.Game.Locations.e8:
                    return 4;

                case ChessGame.GameLogic.Game.Locations.f1:
                case ChessGame.GameLogic.Game.Locations.f2:
                case ChessGame.GameLogic.Game.Locations.f3:
                case ChessGame.GameLogic.Game.Locations.f4:
                case ChessGame.GameLogic.Game.Locations.f5:
                case ChessGame.GameLogic.Game.Locations.f6:
                case ChessGame.GameLogic.Game.Locations.f7:
                case ChessGame.GameLogic.Game.Locations.f8:
                    return 5;

                case ChessGame.GameLogic.Game.Locations.g1:
                case ChessGame.GameLogic.Game.Locations.g2:
                case ChessGame.GameLogic.Game.Locations.g3:
                case ChessGame.GameLogic.Game.Locations.g4:
                case ChessGame.GameLogic.Game.Locations.g5:
                case ChessGame.GameLogic.Game.Locations.g6:
                case ChessGame.GameLogic.Game.Locations.g7:
                case ChessGame.GameLogic.Game.Locations.g8:
                    return 6;

                case ChessGame.GameLogic.Game.Locations.h1:
                case ChessGame.GameLogic.Game.Locations.h2:
                case ChessGame.GameLogic.Game.Locations.h3:
                case ChessGame.GameLogic.Game.Locations.h4:
                case ChessGame.GameLogic.Game.Locations.h5:
                case ChessGame.GameLogic.Game.Locations.h6:
                case ChessGame.GameLogic.Game.Locations.h7:
                case ChessGame.GameLogic.Game.Locations.h8:
                    return 7;


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
                    return 0;

                case ChessGame.GameLogic.Game.Locations.a2:
                case ChessGame.GameLogic.Game.Locations.b2:
                case ChessGame.GameLogic.Game.Locations.c2:
                case ChessGame.GameLogic.Game.Locations.d2:
                case ChessGame.GameLogic.Game.Locations.e2:
                case ChessGame.GameLogic.Game.Locations.f2:
                case ChessGame.GameLogic.Game.Locations.g2:
                case ChessGame.GameLogic.Game.Locations.h2:
                    return 1;

                case ChessGame.GameLogic.Game.Locations.a3:
                case ChessGame.GameLogic.Game.Locations.b3:
                case ChessGame.GameLogic.Game.Locations.c3:
                case ChessGame.GameLogic.Game.Locations.d3:
                case ChessGame.GameLogic.Game.Locations.e3:
                case ChessGame.GameLogic.Game.Locations.f3:
                case ChessGame.GameLogic.Game.Locations.g3:
                case ChessGame.GameLogic.Game.Locations.h3:
                    return 2;

                case ChessGame.GameLogic.Game.Locations.a4:
                case ChessGame.GameLogic.Game.Locations.b4:
                case ChessGame.GameLogic.Game.Locations.c4:
                case ChessGame.GameLogic.Game.Locations.d4:
                case ChessGame.GameLogic.Game.Locations.e4:
                case ChessGame.GameLogic.Game.Locations.f4:
                case ChessGame.GameLogic.Game.Locations.g4:
                case ChessGame.GameLogic.Game.Locations.h4:
                    return 3;

                case ChessGame.GameLogic.Game.Locations.a5:
                case ChessGame.GameLogic.Game.Locations.b5:
                case ChessGame.GameLogic.Game.Locations.c5:
                case ChessGame.GameLogic.Game.Locations.d5:
                case ChessGame.GameLogic.Game.Locations.e5:
                case ChessGame.GameLogic.Game.Locations.f5:
                case ChessGame.GameLogic.Game.Locations.g5:
                case ChessGame.GameLogic.Game.Locations.h5:
                    return 4;

                case ChessGame.GameLogic.Game.Locations.a6:
                case ChessGame.GameLogic.Game.Locations.b6:
                case ChessGame.GameLogic.Game.Locations.c6:
                case ChessGame.GameLogic.Game.Locations.d6:
                case ChessGame.GameLogic.Game.Locations.e6:
                case ChessGame.GameLogic.Game.Locations.f6:
                case ChessGame.GameLogic.Game.Locations.g6:
                case ChessGame.GameLogic.Game.Locations.h6:
                    return 5;

                case ChessGame.GameLogic.Game.Locations.a7:
                case ChessGame.GameLogic.Game.Locations.b7:
                case ChessGame.GameLogic.Game.Locations.c7:
                case ChessGame.GameLogic.Game.Locations.d7:
                case ChessGame.GameLogic.Game.Locations.e7:
                case ChessGame.GameLogic.Game.Locations.f7:
                case ChessGame.GameLogic.Game.Locations.g7:
                case ChessGame.GameLogic.Game.Locations.h7:
                    return 6;

                case ChessGame.GameLogic.Game.Locations.a8:
                case ChessGame.GameLogic.Game.Locations.b8:
                case ChessGame.GameLogic.Game.Locations.c8:
                case ChessGame.GameLogic.Game.Locations.d8:
                case ChessGame.GameLogic.Game.Locations.e8:
                case ChessGame.GameLogic.Game.Locations.f8:
                case ChessGame.GameLogic.Game.Locations.g8:
                case ChessGame.GameLogic.Game.Locations.h8:
                    return 7;

                default:
                    return -1;
            }
        }

        private Game.Locations GetLocation(int x, int y)
        {

            switch (x)
            {
                case 0:
                    switch (y)
                    {
                        case 0:
                            return Game.Locations.a1;
                        case 1:
                            return Game.Locations.a2;
                        case 2:
                            return Game.Locations.a3;
                        case 3:
                            return Game.Locations.a4;
                        case 4:
                            return Game.Locations.a5;
                        case 5:
                            return Game.Locations.a6;
                        case 6:
                            return Game.Locations.a7;
                        case 7:
                            return Game.Locations.a8;
                        default:
                            return Game.Locations.invalid;
                    }
                case 1:
                    switch (y)
                    {
                        case 0:
                            return Game.Locations.b1;
                        case 1:
                            return Game.Locations.b2;
                        case 2:
                            return Game.Locations.b3;
                        case 3:
                            return Game.Locations.b4;
                        case 4:
                            return Game.Locations.b5;
                        case 5:
                            return Game.Locations.b6;
                        case 6:
                            return Game.Locations.b7;
                        case 7:
                            return Game.Locations.b8;
                        default:
                            return Game.Locations.invalid;

                    }
                case 2:
                    switch (y)
                    {
                        case 0:
                            return Game.Locations.c1;
                        case 1:
                            return Game.Locations.c2;
                        case 2:
                            return Game.Locations.c3;
                        case 3:
                            return Game.Locations.c4;
                        case 4:
                            return Game.Locations.c5;
                        case 5:
                            return Game.Locations.c6;
                        case 6:
                            return Game.Locations.c7;
                        case 7:
                            return Game.Locations.c8;
                        default:
                            return Game.Locations.invalid;

                    }
                case 3:
                    switch (y)
                    {
                        case 0:
                            return Game.Locations.d1;
                        case 1:
                            return Game.Locations.d2;
                        case 2:
                            return Game.Locations.d3;
                        case 3:
                            return Game.Locations.d4;
                        case 4:
                            return Game.Locations.d5;
                        case 5:
                            return Game.Locations.d6;
                        case 6:
                            return Game.Locations.d7;
                        case 7:
                            return Game.Locations.d8;
                        default:
                            return Game.Locations.invalid;

                    }
                case 4:
                    switch (y)
                    {
                        case 0:
                            return Game.Locations.e1;
                        case 1:
                            return Game.Locations.e2;
                        case 2:
                            return Game.Locations.e3;
                        case 3:
                            return Game.Locations.e4;
                        case 4:
                            return Game.Locations.e5;
                        case 5:
                            return Game.Locations.e6;
                        case 6:
                            return Game.Locations.e7;
                        case 7:
                            return Game.Locations.e8;
                        default:
                            return Game.Locations.invalid;

                    }
                case 5:
                    switch (y)
                    {
                        case 0:
                            return Game.Locations.f1;
                        case 1:
                            return Game.Locations.f2;
                        case 2:
                            return Game.Locations.f3;
                        case 3:
                            return Game.Locations.f4;
                        case 4:
                            return Game.Locations.f5;
                        case 5:
                            return Game.Locations.f6;
                        case 6:
                            return Game.Locations.f7;
                        case 7:
                            return Game.Locations.f8;
                        default:
                            return Game.Locations.invalid;

                    };
                case 6:
                    switch (y)
                    {
                        case 0:
                            return Game.Locations.g1;
                        case 1:
                            return Game.Locations.g2;
                        case 2:
                            return Game.Locations.g3;
                        case 3:
                            return Game.Locations.g4;
                        case 4:
                            return Game.Locations.g5;
                        case 5:
                            return Game.Locations.g6;
                        case 6:
                            return Game.Locations.g7;
                        case 7:
                            return Game.Locations.g8;
                        default:
                            return Game.Locations.invalid;

                    }
                case 7:
                    switch (y)
                    {
                        case 0:
                            return Game.Locations.h1;
                        case 1:
                            return Game.Locations.h2;
                        case 2:
                            return Game.Locations.h3;
                        case 3:
                            return Game.Locations.h4;
                        case 4:
                            return Game.Locations.h5;
                        case 5:
                            return Game.Locations.h6;
                        case 6:
                            return Game.Locations.h7;
                        case 7:
                            return Game.Locations.h8;
                        default:
                            return Game.Locations.invalid;

                    }
                default:
                    return Game.Locations.invalid;
            }

        }

        //still needs work
        private GameLogic.Game.ResultOfMove checkForCheckandMove(int Ox, int Oy, int Dx, int Dy)
        {
            // Add checks for checkmate

            Piece capturedPiece = null;
            bool pieceWasCaptured = false;

            bool moveItselfValid = false;
            Game.ResultOfMove stateOfCheck;



            Piece pieceToMove = board[Ox, Oy].MoveAway();

            GameLogic.Game.Team MovingTeam = pieceToMove.Team;
            GameLogic.Game.Team OpposingTeam;

            if (MovingTeam == Game.Team.Black)
            {
                OpposingTeam = Game.Team.White;
            }
            else
            {
                OpposingTeam = Game.Team.Black;
            }

            // Find locations of the kings.

            GameLogic.Game.Locations locationOfMovingTeamsKing;
            GameLogic.Game.Locations locationOfOpposingTeamsKing;

            if (MovingTeam == Game.Team.Black)
            {
                locationOfMovingTeamsKing = BlackKingLoc;
                locationOfOpposingTeamsKing = WhiteKingLoc;
            }
            else
            {
                locationOfMovingTeamsKing = WhiteKingLoc;
                locationOfOpposingTeamsKing = BlackKingLoc;
            }


            if (board[Dx, Dy].occupied)
            {
                if (board[Dx, Dy].piece.Team == MovingTeam)
                {
                    return Game.ResultOfMove.Failure;
                }

                capturedPiece = board[Dx, Dy].piece;
                board[Dx, Dy].Capture(pieceToMove);
                moveItselfValid = true;
                pieceWasCaptured = true;
            }
            else
            {
                board[Dx, Dy].MoveTo(pieceToMove);
                moveItselfValid = true;
            }

            // If Move Is invalid don't waste time checking for check
            if (!moveItselfValid)
            {
                return GameLogic.Game.ResultOfMove.Failure;
            }


            // Check to see if the move is invalid by putting a player in check
            stateOfCheck = IsKingInCheck(MovingTeam, locationOfMovingTeamsKing);

            if (stateOfCheck == Game.ResultOfMove.EnemyInCheck)
            {
                pieceToMove = board[Dx, Dy].MoveAway();
                board[Ox, Oy].MoveTo(pieceToMove);

                if (pieceWasCaptured)
                {
                    board[Dx, Dy].MoveTo(capturedPiece);
                }

                return GameLogic.Game.ResultOfMove.Failure;
            }

           // check to see if the opposing king is in check or checkmate.
            return IsKingInCheck(OpposingTeam, locationOfOpposingTeamsKing);
        }

        private Game.ResultOfMove IsKingInCheck(GameLogic.Game.Team colorOfKing, GameLogic.Game.Locations locationOfKing)
        {
            // This function will return success if there is no state of check.

            Game.ResultOfMove result = Game.ResultOfMove.Success;

            Game.Team attackingColor;

            List<Game.Locations> listOfPiecesThreateningTheKing = new List<Game.Locations>();

            if (colorOfKing == Game.Team.Black)
            {
                attackingColor = Game.Team.White;
            }
            else
            {
                attackingColor = Game.Team.Black;
            }

            // traverse board and see if any opposing peices can attack the king.
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j].occupied)
                    {
                        if (board[i, j].piece.Team == attackingColor)
                        {
                            if (canThisPeiceMakeThisMove(GetLocation(i, j), locationOfKing))
                            {
                                listOfPiecesThreateningTheKing.Add(GetLocation(i, j));
                                result = Game.ResultOfMove.EnemyInCheck;
                            }
                        }
                    }
                }
            }



            if (result == Game.ResultOfMove.EnemyInCheck)
            {
                // check for checkmate -- future work





                return result;
            }

            return Game.ResultOfMove.Success;
        }

        private bool canThisPeiceMakeThisMove(Game.Locations origin, Game.Locations destination)
        {
            Piece pieceToMove = GetWhatIsInSquare(origin);

            bool successful = false;

            switch (pieceToMove.Type)
            {

                case ChessGame.GameLogic.Game.TypeOfPiece.pawn:
                    successful = tryPawn(origin, destination);
                    break;
                case ChessGame.GameLogic.Game.TypeOfPiece.rook:
                    successful = tryRook(origin, destination);
                    break;
                case ChessGame.GameLogic.Game.TypeOfPiece.bishop:
                    successful = tryBishiop(origin, destination);
                    break;
                case ChessGame.GameLogic.Game.TypeOfPiece.knight:
                    successful = tryKnight(origin, destination);
                    break;
                case ChessGame.GameLogic.Game.TypeOfPiece.queen:
                    successful = tryQueen(origin, destination);
                    break;
                case ChessGame.GameLogic.Game.TypeOfPiece.king:
                    successful = tryKing(origin, destination);
                    break;

                default:
                    successful = false;
                    break;
            }

            return successful;
        }

        public Game.ResultOfMove movePawn(ChessGame.GameLogic.Game.Locations origin, ChessGame.GameLogic.Game.Locations destination)
        {
            int Ox = GetLocX(origin);
            int Oy = GetLocY(origin);

            int Dx = GetLocX(destination);
            int Dy = GetLocY(destination);

            Game.ResultOfMove success = Game.ResultOfMove.Failure;

            ChessGame.GameLogic.Game.Team team = board[Ox, Oy].piece.Team;

            List<Game.Locations> list = new List<Game.Locations>();

            Game.Locations temporary;

            // black moves down, white moves up.

            switch (team)
            {
                case Game.Team.Black:
                    {

                        // If pawn is at origin it can move 2 spaces forward
                        if (Oy == 6)
                        {
                            temporary = GetLocation(Ox, 4);
                            bool isJumpedSpotOccupied = IsSquareOccupied(GetLocation(Ox, 5));
                            if (!isJumpedSpotOccupied)
                            {
                                list.Add(temporary);
                            }
                        }

                        // move forward
                        temporary = GetLocation(Ox, (Oy - 1));
                        if (temporary != Game.Locations.invalid)
                        {
                            if (!IsSquareOccupied(temporary))
                            {
                                list.Add(temporary);
                            }
                        }

                        // capture left
                        temporary = GetLocation((Ox - 1), (Oy - 1));
                        if (temporary != Game.Locations.invalid)
                        {
                            if (IsSquareOccupied(temporary))
                            {
                                if (team != WhatTeamOwnsSquare(temporary))
                                {
                                    list.Add(temporary);
                                }
                            }
                        }

                        // capture right
                        temporary = GetLocation((Ox + 1), (Oy - 1));
                        if (temporary != Game.Locations.invalid)
                        {
                            if (IsSquareOccupied(temporary))
                            {
                                if (team != WhatTeamOwnsSquare(temporary))
                                {
                                    list.Add(temporary);
                                }
                            }
                        }


                    }
                    break;

                case Game.Team.White:
                    {
                        // If pawn is at origin it can move 2 spaces forward
                        if (Oy == 1)
                        {
                            temporary = GetLocation(Ox, 3);
                            bool isJumpedSpotOccupied = IsSquareOccupied(GetLocation(Ox, 2));
                            if (!isJumpedSpotOccupied)
                            {
                                list.Add(temporary);
                            }
                        }

                        // move forward
                        temporary = GetLocation(Ox, (Oy + 1));
                        if (temporary != Game.Locations.invalid)
                        {
                            if (!IsSquareOccupied(temporary))
                            {
                                list.Add(temporary);
                            }
                        }

                        // capture left
                        temporary = GetLocation((Ox - 1), (Oy + 1));
                        if (temporary != Game.Locations.invalid)
                        {
                            if (IsSquareOccupied(temporary))
                            {
                                if (team != WhatTeamOwnsSquare(temporary))
                                {
                                    list.Add(temporary);
                                }
                            }
                        }

                        // capture right
                        temporary = GetLocation((Ox + 1), (Oy + 1));
                        if (temporary != Game.Locations.invalid)
                        {
                            if (IsSquareOccupied(temporary))
                            {
                                if (team != WhatTeamOwnsSquare(temporary))
                                {
                                    list.Add(temporary);
                                }
                            }
                        }


                    }
                    break;

                default:
                    return Game.ResultOfMove.Failure;

            }

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == destination)
                {
                    success = checkForCheckandMove(Ox, Oy, Dx, Dy);
                    if((Dy == 7) && (team == Game.Team.White))
                    {
                        board[Dx, Dy].piece.Promote();
                    }
                    if ((Dy == 0) && (team == Game.Team.Black))
                    {
                        board[Dx, Dy].piece.Promote();
                    }
                    break;
                }
 
            }

            // remember to take into account diagnal capture

            return success;
        }

        public Game.ResultOfMove moveRook(ChessGame.GameLogic.Game.Locations origin, ChessGame.GameLogic.Game.Locations destination)
        {
            int Ox = GetLocX(origin);
            int Oy = GetLocY(origin);

            int Dx = GetLocX(destination);
            int Dy = GetLocY(destination);

            ChessGame.GameLogic.Game.Team team = board[Ox, Oy].piece.Team;

            bool obsticleLeft = false;
            bool obsticleRight = false;
            bool obsticleUp = false;
            bool obsticleDown = false;


            int x = Ox;
            int y = Oy;
            // Look to the left of the rook for the correct move.
            while (!obsticleLeft)
            {
                x -= 1;
                // We don't need to check off the edge of the board
                if (x < 0)
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleLeft = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return checkForCheckandMove(Ox, Oy, Dx, Dy);
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return checkForCheckandMove(Ox, Oy, Dx, Dy);
                }
            }

            x = Ox;

            while (!obsticleRight)
            {
                x += 1;
                // We don't need to check off the edge of the board
                if (x > 7)
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleRight = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return checkForCheckandMove(Ox, Oy, Dx, Dy);
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return checkForCheckandMove(Ox, Oy, Dx, Dy);
                }
            }

            x = Ox;

            // now check up

            while (!obsticleUp)
            {
                y += 1;
                // We don't need to check off the edge of the board
                if (y > 7)
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleUp = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return checkForCheckandMove(Ox, Oy, Dx, Dy);
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return checkForCheckandMove(Ox, Oy, Dx, Dy);
                }
            }

            y = Oy;

            while (!obsticleDown)
            {
                y -= 1;
                // We don't need to check off the edge of the board
                if (y > 7)
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleDown = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return checkForCheckandMove(Ox, Oy, Dx, Dy);
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return checkForCheckandMove(Ox, Oy, Dx, Dy);
                }
            }

            return Game.ResultOfMove.Failure;
        }

        public Game.ResultOfMove moveBishiop(ChessGame.GameLogic.Game.Locations origin, ChessGame.GameLogic.Game.Locations destination)
        {
            int Ox = GetLocX(origin);
            int Oy = GetLocY(origin);

            int Dx = GetLocX(destination);
            int Dy = GetLocY(destination);

            bool obsticleDownLeft = false;
            bool obsticleDownRight = false;
            bool obsticleUpLeft = false;
            bool obsticleUpRight = false;

            ChessGame.GameLogic.Game.Team team = board[Ox, Oy].piece.Team;

            int x = Ox;
            int y = Oy;

            while (!obsticleDownLeft)
            {
                x--;
                y--;

                // We don't need to check off the edge of the board
                if ((x < 0) || (y < 0))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleDownLeft = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return checkForCheckandMove(Ox, Oy, Dx, Dy);
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return checkForCheckandMove(Ox, Oy, Dx, Dy);
                }
            }

            x = Ox;
            y = Oy;

            while (!obsticleDownRight)
            {
                x++;
                y--;

                // We don't need to check off the edge of the board
                if ((x > 7) || (y < 0))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleDownRight = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return checkForCheckandMove(Ox, Oy, Dx, Dy);
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return checkForCheckandMove(Ox, Oy, Dx, Dy);
                }
            }

            x = Ox;
            y = Oy;

            while (!obsticleUpLeft)
            {
                x--;
                y++;

                // We don't need to check off the edge of the board
                if ((x < 0) || (y > 7))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleUpLeft = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return checkForCheckandMove(Ox, Oy, Dx, Dy);
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return checkForCheckandMove(Ox, Oy, Dx, Dy);
                }
            }

            x = Ox;
            y = Oy;

            while (!obsticleUpRight)
            {
                x++;
                y++;

                // We don't need to check off the edge of the board
                if ((x > 7) || (y > 7))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleUpRight = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return checkForCheckandMove(Ox, Oy, Dx, Dy);
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return checkForCheckandMove(Ox, Oy, Dx, Dy);
                }
            }


            return Game.ResultOfMove.Failure;
        }

        public Game.ResultOfMove moveKnight(ChessGame.GameLogic.Game.Locations origin, ChessGame.GameLogic.Game.Locations destination)
        {
            int Ox = GetLocX(origin);
            int Oy = GetLocY(origin);

            int Dx = GetLocX(destination);
            int Dy = GetLocY(destination);

            ChessGame.GameLogic.Game.Team team = board[Ox, Oy].piece.Team;

            List<Game.Locations> list = new List<Game.Locations>();

            Game.Locations temporary;

            temporary = GetLocation((Ox + 1),(Oy - 2));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox + 2), (Oy - 1));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox + 2), (Oy + 1));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox + 1), (Oy + 2));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox - 1), (Oy + 2));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox - 2), (Oy + 1));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox - 2), (Oy - 1));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox - 1), (Oy - 2));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == destination)
                {
                    return checkForCheckandMove(Ox, Oy, Dx, Dy);                    
                }
            }

            return Game.ResultOfMove.Failure;
        }

        public Game.ResultOfMove moveQueen(ChessGame.GameLogic.Game.Locations origin, ChessGame.GameLogic.Game.Locations destination)
        {
            int Ox = GetLocX(origin);
            int Oy = GetLocY(origin);

            int Dx = GetLocX(destination);
            int Dy = GetLocY(destination);

            bool obsticleDownLeft = false;
            bool obsticleDownRight = false;
            bool obsticleUpLeft = false;
            bool obsticleUpRight = false;

            bool obsticleLeft = false;
            bool obsticleRight = false;
            bool obsticleUp = false;
            bool obsticleDown = false;

            ChessGame.GameLogic.Game.Team team = board[Ox, Oy].piece.Team;

            int x = Ox;
            int y = Oy;

            while (!obsticleDownLeft)
            {
                x--;
                y--;

                // We don't need to check off the edge of the board
                if ((x < 0) || (y < 0))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleDownLeft = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return checkForCheckandMove(Ox, Oy, Dx, Dy);
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return checkForCheckandMove(Ox, Oy, Dx, Dy);
                }
            }

            x = Ox;
            y = Oy;

            while (!obsticleDownRight)
            {
                x++;
                y--;

                // We don't need to check off the edge of the board
                if ((x > 7) || (y < 0))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleDownRight = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return checkForCheckandMove(Ox, Oy, Dx, Dy);
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return checkForCheckandMove(Ox, Oy, Dx, Dy);
                }
            }

            int x = Ox;
            int y = Oy;

            while (!obsticleUpLeft)
            {
                x--;
                y++;

                // We don't need to check off the edge of the board
                if ((x < 0) || (y > 7))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleUpLeft = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return checkForCheckandMove(Ox, Oy, Dx, Dy);
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return checkForCheckandMove(Ox, Oy, Dx, Dy);
                }
            }

            int x = Ox;
            int y = Oy;

            while (!obsticleUpRight)
            {
                x++;
                y++;

                // We don't need to check off the edge of the board
                if ((x > 7) || (y > 7))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleUpRight = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return checkForCheckandMove(Ox, Oy, Dx, Dy);
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return checkForCheckandMove(Ox, Oy, Dx, Dy);
                }
            }

            x = Ox;
            y = Oy;
            // Look to the left of the rook for the correct move.
            while (!obsticleLeft)
            {
                x -= 1;
                // We don't need to check off the edge of the board
                if (x < 0)
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleLeft = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return checkForCheckandMove(Ox, Oy, Dx, Dy);
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return checkForCheckandMove(Ox, Oy, Dx, Dy);
                }
            }

            int x = Ox;
            int y = Oy;

            while (!obsticleRight)
            {
                x += 1;
                // We don't need to check off the edge of the board
                if (x > 7)
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleRight = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return checkForCheckandMove(Ox, Oy, Dx, Dy);
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return checkForCheckandMove(Ox, Oy, Dx, Dy);
                }
            }

            x = Ox;

            // now check up

            while (!obsticleUp)
            {
                y += 1;
                // We don't need to check off the edge of the board
                if (y > 7)
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleUp = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return checkForCheckandMove(Ox, Oy, Dx, Dy);
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return checkForCheckandMove(Ox, Oy, Dx, Dy);
                }
            }

            y = Oy;

            while (!obsticleDown)
            {
                y -= 1;
                // We don't need to check off the edge of the board
                if (y > 7)
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleDown = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return checkForCheckandMove(Ox, Oy, Dx, Dy);
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return checkForCheckandMove(Ox, Oy, Dx, Dy);
                }
            }

            return Game.ResultOfMove.Failure;
        }

        public Game.ResultOfMove moveKing(ChessGame.GameLogic.Game.Locations origin, ChessGame.GameLogic.Game.Locations destination)
        {
            int Ox = GetLocX(origin);
            int Oy = GetLocY(origin);

            int Dx = GetLocX(destination);
            int Dy = GetLocY(destination);

            ChessGame.GameLogic.Game.Team team = board[Ox, Oy].piece.Team;

            List<Game.Locations> list = new List<Game.Locations>();

            Game.Locations temporary;

            temporary = GetLocation((Ox + 1), (Oy - 1));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox + 1), (Oy));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox + 1), (Oy + 1));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox), (Oy + 1));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox), (Oy - 1));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox - 1), (Oy + 1));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox - 1), (Oy));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox - 1), (Oy - 1));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == destination)
                {
                    if (team == Game.Team.Black)
                    {
                        BlackKingLoc = destination;
                    }
                    else
                    {
                        WhiteKingLoc = destination;
                    }


                    Game.ResultOfMove returnValue = checkForCheckandMove(Ox, Oy, Dx, Dy);

                    if (returnValue == Game.ResultOfMove.Failure)
                    {
                        if (team == Game.Team.Black)
                        {
                            BlackKingLoc = origin;
                        }
                        else
                        {
                            WhiteKingLoc = origin;
                        }
                    }

                    return returnValue;
                }
            }

            return Game.ResultOfMove.Failure;
        }

        private bool tryPawn(ChessGame.GameLogic.Game.Locations origin, ChessGame.GameLogic.Game.Locations destination)
        {
            int Ox = GetLocX(origin);
            int Oy = GetLocY(origin);

            int Dx = GetLocX(destination);
            int Dy = GetLocY(destination);

            ChessGame.GameLogic.Game.Team team = board[Ox, Oy].piece.Team;

            List<Game.Locations> list = new List<Game.Locations>();

            Game.Locations temporary;

            // black moves down, white moves up.

            switch (team)
            {
                case Game.Team.Black:
                    {

                        
                        // capture left
                        temporary = GetLocation((Ox - 1), (Oy - 1));
                        if (temporary != Game.Locations.invalid)
                        {
                            if (IsSquareOccupied(temporary))
                            {
                                if (team != WhatTeamOwnsSquare(temporary))
                                {
                                    list.Add(temporary);
                                }
                            }
                        }

                        // capture right
                        temporary = GetLocation((Ox + 1), (Oy - 1));
                        if (temporary != Game.Locations.invalid)
                        {
                            if (IsSquareOccupied(temporary))
                            {
                                if (team != WhatTeamOwnsSquare(temporary))
                                {
                                    list.Add(temporary);
                                }
                            }
                        }


                    }
                    break;

                case Game.Team.White:
                    {
                        

                        // capture left
                        temporary = GetLocation((Ox - 1), (Oy + 1));
                        if (temporary != Game.Locations.invalid)
                        {
                            if (IsSquareOccupied(temporary))
                            {
                                if (team != WhatTeamOwnsSquare(temporary))
                                {
                                    list.Add(temporary);
                                }
                            }
                        }

                        // capture right
                        temporary = GetLocation((Ox + 1), (Oy + 1));
                        if (temporary != Game.Locations.invalid)
                        {
                            if (IsSquareOccupied(temporary))
                            {
                                if (team != WhatTeamOwnsSquare(temporary))
                                {
                                    list.Add(temporary);
                                }
                            }
                        }


                    }
                    break;

                default:
                    return false;

            }

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == destination)
                {
                    return true;
                }
 
            }

            // remember to take into account diagnal capture

            return false;
        }

        private bool tryRook(ChessGame.GameLogic.Game.Locations origin, ChessGame.GameLogic.Game.Locations destination)
        {
            int Ox = GetLocX(origin);
            int Oy = GetLocY(origin);

            int Dx = GetLocX(destination);
            int Dy = GetLocY(destination);

            ChessGame.GameLogic.Game.Team team = board[Ox, Oy].piece.Team;

            bool obsticleLeft = false;
            bool obsticleRight = false;
            bool obsticleUp = false;
            bool obsticleDown = false;


            int x = Ox;
            int y = Oy;
            // Look to the left of the rook for the correct move.
            while (!obsticleLeft)
            {
                x -= 1;
                // We don't need to check off the edge of the board
                if ((x < 0) || (x > 7) || (y > 7) || (y < 0))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleLeft = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return true;
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return true;
                }
            }

            x = Ox;

            while (!obsticleRight)
            {
                x += 1;
                // We don't need to check off the edge of the board
                if ((x < 0) || (x > 7) || (y > 7) || (y < 0))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleRight = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return true;
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return true;
                }
            }

            x = Ox;

            // now check up

            while (!obsticleUp)
            {
                y += 1;
                // We don't need to check off the edge of the board
                if ((x < 0) || (x > 7) || (y > 7) || (y < 0))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleUp = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return true;
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return true;
                }
            }

            y = Oy;

            while (!obsticleDown)
            {
                y -= 1;
                // We don't need to check off the edge of the board
                if ((x < 0) || (x > 7) || (y > 7) || (y < 0))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleDown = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return true;
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return true;
                }
            }

            return false;

        }

        private bool tryBishiop(ChessGame.GameLogic.Game.Locations origin, ChessGame.GameLogic.Game.Locations destination)
        {
            int Ox = GetLocX(origin);
            int Oy = GetLocY(origin);

            int Dx = GetLocX(destination);
            int Dy = GetLocY(destination);

            bool obsticleDownLeft = false;
            bool obsticleDownRight = false;
            bool obsticleUpLeft = false;
            bool obsticleUpRight = false;

            ChessGame.GameLogic.Game.Team team = board[Ox, Oy].piece.Team;

            int x = Ox;
            int y = Oy;

            while (!obsticleDownLeft)
            {
                x--;
                y--;

                // We don't need to check off the edge of the board
                if ((x < 0) || (x > 7) || (y > 7) || (y < 0))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleDownLeft = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return true;
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return true;
                }
            }

            x = Ox;
            y = Oy;

            while (!obsticleDownRight)
            {
                x++;
                y--;

                // We don't need to check off the edge of the board
                if ((x < 0) || (x > 7) || (y > 7) || (y < 0))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleDownRight = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return true;
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return true;
                }
            }

            x = Ox;
            y = Oy;

            while (!obsticleUpLeft)
            {
                x--;
                y++;

                // We don't need to check off the edge of the board
                if ((x < 0) || (x > 7) || (y > 7) || (y < 0))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleUpLeft = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return true;
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return true;
                }
            }

            x = Ox;
            y = Oy;

            while (!obsticleUpRight)
            {
                x++;
                y++;

                // We don't need to check off the edge of the board
                if ((x < 0) || (x > 7) || (y > 7) || (y < 0))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleUpRight = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return true;
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return true;
                }
            }


            return false;
        }

        private bool tryKnight(ChessGame.GameLogic.Game.Locations origin, ChessGame.GameLogic.Game.Locations destination)
        {
            int Ox = GetLocX(origin);
            int Oy = GetLocY(origin);

            int Dx = GetLocX(destination);
            int Dy = GetLocY(destination);

            ChessGame.GameLogic.Game.Team team = board[Ox, Oy].piece.Team;

            List<Game.Locations> list = new List<Game.Locations>();

            Game.Locations temporary;

            temporary = GetLocation((Ox + 1), (Oy - 2));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox + 2), (Oy - 1));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox + 2), (Oy + 1));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox + 1), (Oy + 2));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox - 1), (Oy + 2));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox - 2), (Oy + 1));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox - 2), (Oy - 1));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox - 1), (Oy - 2));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == destination)
                {
                    return true;
                }
            }

            return false;
        }

        private bool tryQueen(ChessGame.GameLogic.Game.Locations origin, ChessGame.GameLogic.Game.Locations destination)
        {
            int Ox = GetLocX(origin);
            int Oy = GetLocY(origin);

            int Dx = GetLocX(destination);
            int Dy = GetLocY(destination);

            bool obsticleDownLeft = false;
            bool obsticleDownRight = false;
            bool obsticleUpLeft = false;
            bool obsticleUpRight = false;

            bool obsticleLeft = false;
            bool obsticleRight = false;
            bool obsticleUp = false;
            bool obsticleDown = false;

            ChessGame.GameLogic.Game.Team team = board[Ox, Oy].piece.Team;

            int x = Ox;
            int y = Oy;

            while (!obsticleDownLeft)
            {
                x--;
                y--;

                // We don't need to check off the edge of the board
                if ((x < 0) || (x > 7) || (y > 7) || (y < 0))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleDownLeft = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return true;
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return true;
                }
            }

            x = Ox;
            y = Oy;

            while (!obsticleDownRight)
            {
                x++;
                y--;

                // We don't need to check off the edge of the board
                if ((x < 0) || (x > 7) || (y > 7) || (y < 0))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleDownRight = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return true;
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return true;
                }
            }

            int x = Ox;
            int y = Oy;

            while (!obsticleUpLeft)
            {
                x--;
                y++;

                // We don't need to check off the edge of the board
                if ((x < 0) || (x > 7) || (y > 7) || (y < 0))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleUpLeft = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return true;
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return true;
                }
            }

            int x = Ox;
            int y = Oy;

            while (!obsticleUpRight)
            {
                x++;
                y++;

                // We don't need to check off the edge of the board
                if ((x < 0) || (x > 7) || (y > 7) || (y < 0))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleUpRight = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return true;
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return true;
                }
            }

            x = Ox;
            y = Oy;
            // Look to the left of the rook for the correct move.
            while (!obsticleLeft)
            {
                x -= 1;
                // We don't need to check off the edge of the board
                if ((x < 0) || (x > 7) || (y > 7) || (y < 0))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleLeft = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return true;
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return true;
                }
            }

            x = Ox;

            while (!obsticleRight)
            {
                x += 1;
                // We don't need to check off the edge of the board
                if ((x < 0) || (x > 7) || (y > 7) || (y < 0))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleRight = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return true;
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return true;
                }
            }

            x = Ox;

            // now check up

            while (!obsticleUp)
            {
                y += 1;
                // We don't need to check off the edge of the board
                if ((x < 0) || (x > 7) || (y > 7) || (y < 0))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleUp = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return true;
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return true;
                }
            }

            y = Oy;

            while (!obsticleDown)
            {
                y -= 1;
                // We don't need to check off the edge of the board
                if ((x < 0) || (x > 7) || (y > 7) || (y < 0))
                {
                    break;
                }
                if (IsSquareOccupied(x, y))
                {
                    obsticleDown = true;
                    if (team != board[x, y].piece.Team)
                    {
                        if (GetLocation(x, y) == destination)
                        {
                            return true;
                        }
                    }
                }
                else if (GetLocation(x, y) == destination)
                {
                    return true;
                }
            }

            return false;
        }

        private bool tryKing(ChessGame.GameLogic.Game.Locations origin, ChessGame.GameLogic.Game.Locations destination)
        {
            int Ox = GetLocX(origin);
            int Oy = GetLocY(origin);

            int Dx = GetLocX(destination);
            int Dy = GetLocY(destination);

            ChessGame.GameLogic.Game.Team team = board[Ox, Oy].piece.Team;

            List<Game.Locations> list = new List<Game.Locations>();

            Game.Locations temporary;

            temporary = GetLocation((Ox + 1), (Oy - 1));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox + 1), (Oy));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox + 1), (Oy + 1));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox), (Oy + 1));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox), (Oy - 1));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox - 1), (Oy + 1));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox - 1), (Oy));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            temporary = GetLocation((Ox - 1), (Oy - 1));
            if (temporary != Game.Locations.invalid)
            {
                list.Add(temporary);
            }

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == destination)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
