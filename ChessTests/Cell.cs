namespace ChessTests
{
    public class Cell
    {
        private Piece piece;
        //make readonly
        public int X { get; set; }
        public int Y { get; set; }

        public Cell(Piece piece)
        {
            Piece = piece;
        }

        public Cell(int x, int y, Piece piece = null)
        {
            X = x;
            Y = y;
            Piece = piece;
            if (piece != null)
            {
                piece.InitialPosition = this;

            }
        }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Piece Piece
        {
            get
            {
                return piece;
            }
            set 
            {
                piece = value;
                if (piece != null)
                {
                    piece.CurrentPosition = this;
                }
            }
        }

    }
}