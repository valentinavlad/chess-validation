using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests
{
    public class Move
    {
        public Coordinate Coordinate { get; set; }
        public Piece Promotion { get; set; }

        //iesa care vine mutatap
        public PieceName PieceName { get; set; }

        public PieceColor Color { get; set; }

        //the file(a-h) from which the piece departed
        public int Y { get; set; }

        public bool IsCheck { get; set; }
        public bool IsCheckMate { get; set; }
        public bool IsCapture { get; set; }
        public bool IsKingCastling { get; set; }
        public bool IsQueenCastling { get; set; }
    }
}
