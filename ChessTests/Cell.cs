namespace ChessTests
{
    public class Cell
    {
        public Cell(Piece piece)
        {
            Piece = piece;
        }
        public Piece Piece { get; set; }

        public Piece ReleaseCell()
        {
            Piece releasedPiece = Piece;
            Piece = null;
            return releasedPiece;
        }
    }
}