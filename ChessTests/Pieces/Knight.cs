using ChessTable;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests
{
    public class Knight : Piece
    {
   

        public Knight(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Knight;
        }



        public override bool PieceCanMove(Board board, Cell start, Cell end)
        {
            throw new NotImplementedException();
        }
    }
}
