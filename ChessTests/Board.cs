using ChessTests;
using System;
using ChessTests.Pieces;
using System.Collections.Generic;
using System.Linq;
using ChessTests.Helpers;
using ChessTests.Validations;
using ChessTests.GameAction;

namespace ChessTable
{
    public class Board
    {
        private readonly Cell[,] cells;
        internal readonly List<Piece> whitePieces = new List<Piece>();
        private readonly InitializeBoard initialize;
        private readonly KingValidation kingValidation;
        private readonly ChessTests.Helpers.Action action;

        public Board(bool withPieces = true)
        {
            cells = new Cell[8, 8];
            initialize = new InitializeBoard(cells);
            kingValidation = new KingValidation(this);
            action = new ChessTests.Helpers.Action(this);
            if (withPieces)
            {
                initialize.InitializeBoardWithPieces();
            }
            else
            {
                initialize.InitializeBoardWithoutPieces();
            }
            PopulateWhiteListPiece();
        }

        public bool GetWin { get; set; }

        public Piece FindPieceWhoNeedsToBeMoved(string moveAN, PieceColor playerColor)
        {
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, playerColor);
            return FindPieceWhoNeedsToBeMoved(move, playerColor);
        }

        public Piece FindPieceWhoNeedsToBeMoved(Move move, PieceColor playerColor)
        {
            return GetPiece(move, playerColor, move.PieceName);
        }

        public Piece PlayMove(string moveAN, PieceColor currentPlayer)
        {
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, currentPlayer);

            var destinationCell = TransformCoordonatesIntoCell(move.Coordinate);

            var piece = FindPieceWhoNeedsToBeMoved(move, currentPlayer);

            if (piece == null) throw new InvalidOperationException("Invalid move!");       

            if (move.IsKingCastling && !move.IsCheck) return CastlingHelpers.TryKingCastling(currentPlayer, move, destinationCell, piece);

            if (move.IsQueenCastling && !move.IsCheck) return CastlingHelpers.TryQueenCastling(currentPlayer, move, destinationCell, piece);
           
            if (move.IsCapture)
            {
                whitePieces.Remove(destinationCell.Piece);
               // Console.WriteLine(currentPlayer + " capture " + destinationCell.Piece + " at " + move.Coordinates);
                move.CapturePiece(piece, destinationCell);
            }

            move.MovePiece(piece, destinationCell);

            if (move.Promotion != null)
            {
                PromotePawn(move, piece);
                //Console.WriteLine(currentPlayer + " pawn promoted to " + promotedTo);
            }

            if (move.IsCheck)
            {
                //verify if king is actually in check
                if (!GameValidation.CheckIfKingIsInCheck(piece, currentPlayer, move))
                {
                    move.IsCapture = false;
                }
               // Console.WriteLine(currentPlayer + " puts opponent king in check!");
            }

            if (move.IsCheckMate)
            {
                GetWin = IsCheckMate(currentPlayer, move, piece);
                if (GetWin)
                {
                    //Console.WriteLine(currentPlayer + " puts opponent king in checkmate!");
                }
                else
                {
                    throw new InvalidOperationException("Illegal win, king is not in checkmate!");
                }
            }

            return piece;
        }

        public Piece PromotePawn(Move move, Piece pawn)
        {
            pawn.CurrentPosition = null;
            var pawnPromovatesTo = action.AddPiece(move.Coordinate, move.Promotion);
            return pawnPromovatesTo;
        }

        public Cell TransformCoordonatesIntoCell(Coordinate coordinate)
        {
            if (coordinate.X >= 0 && coordinate.X <= 7 && coordinate.Y >= 0 && coordinate.Y <= 7)
            {
                return cells[coordinate.X, coordinate.Y];
            }
            throw new IndexOutOfRangeException("Index out of bound");
        }

        private Piece GetPiece(Move move, PieceColor playerColor, PieceName pieceName)
        {
            switch (pieceName)
            {
                case PieceName.Pawn:
                    Piece pawn = new Pawn(playerColor);
                    bool result = pawn.ValidateMovementAndReturnPiece(this, move, playerColor, out pawn);
                    return result ? pawn : null;
                case PieceName.Queen:
                    return Queen.ValidateMovementAndReturnPiece(this, move, playerColor);
                case PieceName.Bishop:
                    return Bishop.ValidateMovementAndReturnPiece(this, move, playerColor);
                case PieceName.Rook:
                    return Rook.ValidateMovementAndReturnPiece(this, move, playerColor);
                case PieceName.King:
                    return King.ValidateMovementAndReturnPiece(this, move, playerColor);
                case PieceName.Knight:
                    return Knight.ValidateMovementAndReturnPiece(this, move, playerColor);
                default:
                    return null;
            }
        }

        private bool IsCheckMate(PieceColor currentPlayer, Move move, Piece piece)
        {
            var king = (King)BoardAction.FindKing(piece.CurrentPosition, currentPlayer);     
            return kingValidation.CheckIfKingIsInCheckMate(king, currentPlayer, move);
        }

        private void PopulateWhiteListPiece()
        {
            for (int i = 7; i >= 6; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    whitePieces.Add(cells[i, j].Piece);
                }
            }
        }
    }
}
