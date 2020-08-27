using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests.Interfaces
{
    public interface IMove : ICoordinate, IMoveCastling, IMoveCheck
    {
        public Piece Promotion { get; set; }
        public bool IsCapture { get; set; }
        public IPiece Piece { get; set; }
        public void CapturePiece(IPiece attacker, Cell cellDestination);
        public void MovePiece(IPiece piece, Cell destinationCell);
    }
}
