namespace ChessTests
{
    public class Square
    {
        public Square(int x, int y, Piece piece)
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