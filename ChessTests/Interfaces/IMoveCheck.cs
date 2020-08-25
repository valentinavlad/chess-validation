using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests.Interfaces
{
    public interface IMoveCheck
    {
        public bool IsCheck { get; set; }
        public bool IsCheckMate { get; set; }
    }
}
