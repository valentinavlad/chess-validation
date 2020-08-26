using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests.GameAction
{
    internal class Castling
    {
        internal void KingCastling(Piece king, PieceColor currentPlayer, Cell destinationCell, Move move)
        {
            var currentCell = destinationCell;
            if (currentCell.Piece != null) throw new InvalidOperationException("Invalid state!");

            currentCell = currentCell.Look(Orientation.Right);
            if (currentCell.Piece.Name == PieceName.Rook && currentPlayer == currentCell.Piece.PieceColor && king.IsOnInitialPosition())
            {
                var rook = currentCell.Piece;
                if (rook.IsOnInitialPosition())
                {
                    move.MovePiece(king, destinationCell);
                    move.MovePiece(rook, destinationCell.Look(Orientation.Left));
                }
                else
                {
                    throw new InvalidOperationException("Rook is invalid state!");
                }
            }

        }
        internal void QueenCastling(Piece king, PieceColor currentPlayer, Cell destinationCell, Move move)
        {
            var currentCell = destinationCell.Look(Orientation.Right);
            if (currentCell.Piece == null)
            {
                for (int i = currentCell.Y - 1; i >= 0; i--)
                {
                    currentCell = currentCell.Look(Orientation.Left);
                    if (currentCell == null) break;
                    if (currentCell.Piece != null && currentCell.Y != 0) throw new InvalidOperationException("Invalid state!");
                    if (currentCell.Y == 0)
                    {
                        if (currentCell.Piece.Name == PieceName.Rook && currentPlayer == currentCell.Piece.PieceColor && king.IsOnInitialPosition())
                        {
                            var rook = currentCell.Piece;
                            move.MovePiece(king, destinationCell);
                            move.MovePiece(rook, destinationCell.Look(Orientation.Right));
                        }
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Invalid state!");
            }
        }
    }
}
