using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTable
{
    public class Board
    {
        private Square[,] squares;

        public Board()
        {
            squares = new Square[8,8];
        }
    }
}
