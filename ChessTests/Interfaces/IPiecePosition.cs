using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests.Interfaces
{
    public interface IPiecePosition
    {
        public Cell DestinationCell { get; set; }

        public Cell PiecePosition { get; set; }
        //public Cell CurrentPosition { get; set; }

        //public Cell InitialPosition { get; set; }
    }
}
