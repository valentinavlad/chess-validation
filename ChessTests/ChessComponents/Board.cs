using ChessTests;
using System;
using ChessTests.Pieces;
using System.Collections.Generic;
using ChessTests.Helpers;
using ChessTests.Validations;
using ChessTests.GameAction;

namespace ChessTable
{
    public class Board
    {
        private readonly Cell[,] cells;
        private readonly InitializeBoard initialize;
        private readonly KingValidation kingValidation;
        internal readonly List<Piece> whitePieces = new List<Piece>();
        private readonly CastlingHelpers castling = new CastlingHelpers();

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
        public Piece PlayMove(string moveAN, PieceColor currentPlayer)
        {
            GetPiece(moveAN, currentPlayer, out Move move, out Piece piece);
            if (move.IsKingCastling && !move.IsCheck) return castling.TryKingCastling(currentPlayer, move, piece);

            if (move.IsQueenCastling && !move.IsCheck) return castling.TryQueenCastling(currentPlayer, move, piece);
           
            if (move.IsCapture)
            {
                whitePieces.Remove(move.DestinationCell.Piece);
                move.CapturePiece(piece, move.DestinationCell);
            }

            move.MovePiece(piece, move.DestinationCell);

            if (move.Promotion != null) PromotePawn(move, piece);

            IsKingInCheckMate(currentPlayer, move);

            return piece;
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

        public bool FindPieceWhoNeedsToBeMoved(string moveAN, PieceColor playerColor)
        {
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, playerColor);
            move.DestinationCell = CellAt(move.Coordinate);
            return FindPieceWhoNeedsToBeMoved(move);
        }

        public bool FindPieceWhoNeedsToBeMoved(Move move)
        {
            move.DestinationCell = CellAt(move.Coordinate);
            return move.IsPiece();
        }

        public Piece PromotePawn (Move move, Piece pawn)
        {
            pawn.CurrentPosition = null;
            var pawnPromovatesTo = AddPiece(move.Coordinate, move.Promotion);
            return pawnPromovatesTo;
        }

        private Cell TransformCoordonatesIntoCell(Coordinate coordinate)
        {
            if (coordinate.X >= 0 && coordinate.X <= 7 && coordinate.Y >= 0 && coordinate.Y <= 7)
            {
                return cells[coordinate.X, coordinate.Y];
            }
            throw new IndexOutOfRangeException("Index out of bound");
        }

        private void IsKingInCheckMate(PieceColor currentPlayer, Move move)
        {
            if (move.IsCheckMate)
            {
                GetWin = kingValidation.IsCheckMate(currentPlayer, move);
                if (!GetWin) throw new InvalidOperationException("Illegal win, king is not in checkmate!");
            }
        }

        private void GetPiece(string moveAN, PieceColor currentPlayer, out Move move, out Piece piece )
        {
            move = GetMoveFromNotation(moveAN, currentPlayer);
            FindPieceWhoNeedsToBeMoved(move);
            piece = move.PiecePosition.Piece;
        }

        private Move GetMoveFromNotation(string moveAN, PieceColor currentPlayer)
        {
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, currentPlayer);

            move.DestinationCell = CellAt(move.Coordinate);
            return move;
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
