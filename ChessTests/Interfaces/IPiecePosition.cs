using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests.Interfaces
{
    public interface IPiecePosition
    {
        public Cell DestinationCell { get; set; }
        public Cell PiecePosition { get; set; }
    }
}
