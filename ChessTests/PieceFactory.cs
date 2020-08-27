using ChessTests.Interfaces;
using ChessTests.Pieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests
{
     class PieceFactory
    {
        public static IPiece CreatePiece(PieceName Piece, PieceColor Color)
        {
            return Piece switch
            {
                PieceName.Bishop => new Bishop(Color),
                PieceName.Queen => new Queen(Color),
                PieceName.Rook => new Rook(Color),
                PieceName.King => new King(Color),
                PieceName.Knight => new Knight(Color),
                _ => new Pawn(Color),
            };
        }
    }
}
