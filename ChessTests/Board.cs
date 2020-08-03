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
        private Cell[,] cells;
        internal readonly List<Piece> whitePieces = new List<Piece>();
        private readonly InitializeBoard initialize;
        readonly KingValidation kingValidation;
        private Cell checkMatePosition;
        public Board(bool withPieces = true)
        {
            cells = new Cell[8, 8];
            initialize = new InitializeBoard(cells);
            kingValidation = new KingValidation(this);
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
                var promotedTo = PromotePawn(move, piece);
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
            var pawnPromovatesTo = AddPiece(move.Coordinate, move.Promotion);
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

        internal Piece AddPiece(string coordsAN, Piece piece)
        {
            var coordinates = MoveNotationCoordinatesConverter.ConvertChessCoordinatesToArrayIndexes(coordsAN);
            return AddPiece(coordinates, piece);
        }

        internal Piece AddPiece(Coordinate coordinates, Piece piece)
        {
            var cell = CellAt(coordinates);
            cell.Piece = piece;
            return cell.Piece;
        }

        internal Cell CellAt(string coordsAN)
        {
            var result = MoveNotationCoordinatesConverter.ConvertChessCoordinatesToArrayIndexes(coordsAN);
            return TransformCoordonatesIntoCell(result);
        }

        internal Cell CellAt(Coordinate coordinates)
        {
            return TransformCoordonatesIntoCell(coordinates);
        }

        private Piece GetPiece(Move move, PieceColor playerColor, PieceName pieceName)
        {
            switch (pieceName)
            {
                case PieceName.Pawn:
                    return Pawn.ValidateMovementAndReturnPiece(this, move, playerColor);
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
            checkMatePosition = piece.CurrentPosition;
            //find king
            var king = (King)BoardAction.FindKing(checkMatePosition, currentPlayer);
            //verify if is in check mate

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
