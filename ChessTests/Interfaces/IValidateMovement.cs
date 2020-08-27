using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests.Interfaces
{
    public interface IValidateMovement
    {
        bool ValidateMovement(IBoard board, IMove move);
    }
}
