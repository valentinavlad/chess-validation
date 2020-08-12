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
        private readonly ChessTests.Helpers.Action action;
        private CastlingHelpers castling = new CastlingHelpers();

        internal BoardAction boardAction = new BoardAction();
        internal readonly List<Piece> whitePieces = new List<Piece>();

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

        public Piece PlayMove(string moveAN, PieceColor currentPlayer)
        {
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, currentPlayer);

             move.DestinationCell = TransformCoordonatesIntoCell(move.Coordinate);

            var isPiece = FindPieceWhoNeedsToBeMoved(move);
            Piece piece = null;
            if (isPiece) piece = move.PiecePosition.Piece;

            if (piece == null) throw new InvalidOperationException("Invalid move!");

            if (move.IsKingCastling && !move.IsCheck) return castling.TryKingCastling(currentPlayer, move, move.DestinationCell, piece);

            if (move.IsQueenCastling && !move.IsCheck) return castling.TryQueenCastling(currentPlayer, move, move.DestinationCell, piece);

            if (move.IsCapture)
            {
                whitePieces.Remove(move.DestinationCell.Piece);
                // Console.WriteLine(currentPlayer + " capture " + destinationCell.Piece + " at " + move.Coordinates);
                move.CapturePiece(piece, move.DestinationCell);
            }

            move.MovePiece(piece, move.DestinationCell);

            PawnPromotion(move, piece);
            
            IsKingInCheckMate(currentPlayer, move, piece);

            return piece;
        }

        private void IsKingInCheckMate(PieceColor currentPlayer, Move move, Piece piece)
        {
            if (move.IsCheckMate)
            {
                GetWin = IsCheckMate(currentPlayer, move, piece);
                if (!GetWin)  throw new InvalidOperationException("Illegal win, king is not in checkmate!");   
            }
        }

        private void PawnPromotion(Move move, Piece piece)
        {
            if (move.Promotion != null)
            {
                PromotePawn(move, piece);
                //Console.WriteLine(currentPlayer + " pawn promoted to " + promotedTo);
            }
        }

        public bool FindPieceWhoNeedsToBeMoved(string moveAN, PieceColor playerColor)
        {
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, playerColor);
            return FindPieceWhoNeedsToBeMoved(move);
        }

        public bool FindPieceWhoNeedsToBeMoved(Move move)
        {
            move.DestinationCell = TransformCoordonatesIntoCell(move.Coordinate);
            return IsPiece(move);
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

        private bool IsCheckMate(PieceColor currentPlayer, Move move, Piece piece)
        {
            var king = (King)boardAction.FindKing(piece.CurrentPosition, currentPlayer);     
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

        private bool IsPiece(Move move)
        {
            switch (move.PieceName)
            {
                case PieceName.Pawn:
                    Piece pawn = new Pawn(move.Color);
                    return pawn.ValidateMovement(move);

                case PieceName.Queen:
                    Piece queen = new Queen(move.Color);
                    return queen.ValidateMovement(move);

                case PieceName.Bishop:
                    Piece bishop = new Bishop(move.Color);
                    return bishop.ValidateMovement(move);

                case PieceName.Rook:
                    Piece rook = new Rook(move.Color);
                    return rook.ValidateMovement(move);

                case PieceName.King:
                    Piece king = new King(move.Color);
                    return king.ValidateMovement(move);

                case PieceName.Knight:
                    Piece knight = new Knight(move.Color);
                    return knight.ValidateMovement(move);

                default:
                    return false;
            }
        }
    }
}
