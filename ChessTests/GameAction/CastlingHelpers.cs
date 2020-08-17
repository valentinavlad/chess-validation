using System;


namespace ChessTests.GameAction
{
    internal class CastlingHelpers
    {
        readonly Castling castling = new Castling();

        internal Piece TryKingCastling(PieceColor currentPlayer, Move move, Piece piece)
        {
            if (piece.IsOnInitialPosition())
            {
                castling.KingCastling(piece, currentPlayer, move.DestinationCell, move);
                //Console.WriteLine(currentPlayer + " makes king castling!");
                return piece;
            }
            else
            {
                throw new InvalidOperationException("Invalid castling!");
            }
        }

        internal Piece TryQueenCastling(PieceColor currentPlayer, Move move, Piece piece)
        {
            if (piece.IsOnInitialPosition())
            {
                castling.QueenCastling(piece, currentPlayer, move.DestinationCell, move);
                //Console.WriteLine(currentPlayer + " makes queen castling!");
                return piece;
            }
            else
            {
                throw new InvalidOperationException("Invalid castling!");
            }
        }
    }
}
