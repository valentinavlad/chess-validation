using ChessTable;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests.Pieces
{
    public class Rook : Piece
    {

        public Rook(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Rook;
        }
    }
}
