using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests.Interfaces
{
    public interface IPiece : IPieceProperties, IValidateMovement
    {
        public Cell CurrentPosition { get; set; }
        public Cell InitialPosition { get; set; }
        public bool IsOnInitialPosition();
    }
}
