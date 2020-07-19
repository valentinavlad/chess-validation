using ChessTable;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests
{
    public class Pawn : Piece
    {
        public Pawn(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Pawn;
        }


        public override bool PieceCanMove(Board board, Cell start, Cell end)
        {
            throw new NotImplementedException();
        }
    }
}
