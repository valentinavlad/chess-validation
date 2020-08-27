using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests.Interfaces
{
    public interface IPiece : IPieceProperties, IValidateMovement, IPiecePosition
    {
        public bool IsOnInitialPosition();
    }
}
