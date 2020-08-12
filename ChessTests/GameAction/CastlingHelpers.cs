using System;


namespace ChessTests.GameAction
{
    internal class CastlingHelpers
    {
       Castling castling = new Castling();

        internal Piece TryKingCastling(PieceColor currentPlayer, Move move, Cell destinationCell, Piece piece)
        {
            if (piece.IsOnInitialPosition())
            {
                castling.KingCastling(piece, currentPlayer, destinationCell, move);
                Console.WriteLine(currentPlayer + " makes king castling!");
                return piece;
            }
            else
            {
                throw new InvalidOperationException("Invalid castling!");
            }
        }

        internal Piece TryQueenCastling(PieceColor currentPlayer, Move move, Cell destinationCell, Piece piece)
        {
            if (piece.IsOnInitialPosition())
            {
                castling.QueenCastling(piece, currentPlayer, destinationCell, move);
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
