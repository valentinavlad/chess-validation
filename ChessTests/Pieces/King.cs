using ChessTable;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests.Pieces
{
    public class King : Piece
    {

        public King(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.King;
        }

    }
}
