
namespace ChessTests
{
    public class Piece
    {
        public Piece()
        {
          
        }

        public Piece(PieceColor pieceColor, PieceName pieceName)
        {
            PieceColor = pieceColor;
            PieceName = pieceName;
        }

        //public int X { get; set; }
        //public int Y { get; set; }
        public PieceColor PieceColor { get; set; }
        public PieceName PieceName { get; set; }
    }
}