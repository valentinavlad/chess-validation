
using ChessTable;

namespace ChessTests
{
    public abstract class Piece
    {
        public Cell InitialPosition { get; set; }
        public Cell CurrentPosition { get; set; }

        public PieceColor pieceColor;
        public PieceName Name { get; set; }

        public Piece(PieceColor pieceColor)
        {
            this.pieceColor = pieceColor;
        }

        public abstract bool PieceCanMove(Board board, Cell start, Cell end);
      
        public bool IsOnInitialPosition()
        {
            return InitialPosition == CurrentPosition;
        }


    }
}