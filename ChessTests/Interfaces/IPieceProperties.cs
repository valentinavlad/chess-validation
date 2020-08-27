using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests.Interfaces
{
    public interface IPieceProperties
    {
        public PieceName Name { get; set; }

        public PieceColor PieceColor { get; set; }
    }
}
