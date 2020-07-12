namespace ChessTests
{
    public class Cell
    {
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Cell(int x, int y, Piece piece)
        {
            X = x;
            Y = y;
            Piece = piece;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Piece Piece { get; set; }
    }
}