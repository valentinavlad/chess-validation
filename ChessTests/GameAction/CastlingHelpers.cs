using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests.GameAction
{
    internal static class CastlingHelpers
    {
        internal static Piece TryKingCastling(PieceColor currentPlayer, Move move, Cell destinationCell, Piece piece)
        {
            if (piece.IsOnInitialPosition())
            {
                Castling.KingCastling(piece, currentPlayer, destinationCell, move);
                Console.WriteLine(currentPlayer + " makes king castling!");
                return piece;
            }
            else
            {
                throw new InvalidOperationException("Invalid castling!");
            }
        }

        internal static Piece TryQueenCastling(PieceColor currentPlayer, Move move, Cell destinationCell, Piece piece)
        {
            if (piece.IsOnInitialPosition())
            {
                Castling.QueenCastling(piece, currentPlayer, destinationCell, move);
                Console.WriteLine(currentPlayer + " makes queen castling!");
                return piece;
            }
            else
            {
                throw new InvalidOperationException("Invalid castling!");
            }
        }
    }
}
