using ChessTable;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests
{
    public class Bishop : Piece
    {

        public Bishop(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Bishop;
        }


        public override bool PieceCanMove(Board board, Cell start, Cell end)
        {
            throw new NotImplementedException();
        }
    }
}
