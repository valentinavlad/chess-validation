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

        public bool IsCheck { get; set; }
        public bool IsCheckMate { get; set; }
    }
}
