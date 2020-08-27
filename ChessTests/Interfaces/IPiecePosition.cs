using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests.Interfaces
{
    public interface IPiecePosition
    {
        public Cell CurrentPosition { get; set; }

        public Cell InitialPosition { get; set; }
    }
}
