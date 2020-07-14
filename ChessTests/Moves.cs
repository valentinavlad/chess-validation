using System.Collections.Generic;

namespace ChessTests
{
    public class Moves
    {
        private string blackMoves;
        private string whiteMoves;
        private Cell start;
        private Cell end;
        private Piece pieceMoved;
        public Moves()
        {}

        public Moves(Piece piece)
        {
            Piece = piece;
        }
        public Moves(Cell start, Cell end)
        {
            this.start = start;
            this.end = end;
            this.pieceMoved = start.Piece;
        }

        public Piece Piece { get; set; }
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