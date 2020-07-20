namespace ChessTests
{
    public class Cell
    {
        private Piece piece;
        private Cell[,] cells;
        public void SetCells(Cell[,] cells) 
        {
            this.cells = cells;
        }
        //make readonly
        public int X { get; set; }
        public int Y { get; set; }

        public Cell(Piece piece)
        {
            Piece = piece;
        }

        public Cell(int x, int y, Piece piece = null, Cell[,] cells = null)
        {
            X = x;
            Y = y;
            Piece = piece;
            if (piece != null)
            {
                piece.InitialPosition = this;

            }
            this.cells = cells;
        }

        public Cell(int x, int y, Cell[,] cells = null)
        {
            X = x;
            Y = y;
            this.cells = cells;
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
        
        public Cell LookDown()
        {
            int i = X + 1;
            return cells[i, Y];
        }

        public bool HasPawn()
        {
            return HasPiece() && Piece.Name == PieceName.Pawn;
        }

        public bool HasPiece()
        {
            return Piece != null;
        }

        public bool BelongsTo(PieceColor playerColor)
        {
            return HasPiece() && Piece.pieceColor == playerColor;
        }
    }
}