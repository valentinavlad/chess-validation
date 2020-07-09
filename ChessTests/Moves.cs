using System.Collections.Generic;

namespace ChessTests
{
    public class Moves
    {
        private string blackMoves;
        private string whiteMoves;

        public string BlackMoves
        {
            get { return blackMoves; }   
            set { blackMoves = "black " + value; }
        }
        public string WhiteMoves
        {
            get { return whiteMoves; }
            set { whiteMoves = "white " + value; }
        }
    }
}