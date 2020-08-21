using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests.Interfaces
{
    public interface ICheckOpponentKing
    {
        public bool CheckForOpponentKingOnSpecificRoutes(Move move);
    }
}
