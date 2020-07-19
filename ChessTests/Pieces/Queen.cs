using ChessTable;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests
{
    public class Queen : Piece
    {
        public Queen(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Queen;
        }

        public override bool PieceCanMove(Board board, Cell start, Cell end)
        {
            throw new NotImplementedException();
        }
    }
}
