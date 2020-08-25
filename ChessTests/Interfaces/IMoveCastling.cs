using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests.Interfaces
{
    public interface IMoveCastling
    {
        public bool IsKingCastling { get; set; }
        public bool IsQueenCastling { get; set; }
    }
}
