using System;

namespace ChessTests.GameAction
{
    internal class CastlingHelpers
    {
        readonly Castling castling = new Castling();

        internal bool TryKingCastling(PieceColor currentPlayer, Move move, Piece piece)
        {
            if (piece.IsOnInitialPosition())
            {
                castling.KingCastling(piece, currentPlayer, move.DestinationCell, move);
                //Console.WriteLine(currentPlayer + " makes king castling!");
                return true;
            }
            else
            {
                throw new InvalidOperationException("Invalid castling!");
            }
        }

        internal bool TryQueenCastling(PieceColor currentPlayer, Move move, Piece piece)
        {
            if (piece.IsOnInitialPosition())
            {
                castling.QueenCastling(piece, currentPlayer, move.DestinationCell, move);
                //Console.WriteLine(currentPlayer + " makes queen castling!");
                return true;
            }
            else
            {
                throw new InvalidOperationException("Invalid castling!");
            }
        }
    }
}
