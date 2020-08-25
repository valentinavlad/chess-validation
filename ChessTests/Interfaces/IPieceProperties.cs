using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests.Interfaces
{
    public interface IPieceProperties : IValidateMovement
    {
        public PieceName Name { get; set; }

        public PieceColor pieceColor { get; }
  
    }
}
