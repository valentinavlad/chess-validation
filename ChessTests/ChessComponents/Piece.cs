using ChessTests.Helpers;
using ChessTests.Interfaces;

namespace ChessTests
{
    public abstract class Piece : IPiece
    {
        public PieceColor PieceColor { get; set; }
        public PieceName Name { get; set; }

        public Piece(PieceColor pieceColor)
        {
            this.PieceColor = pieceColor;
        }

        public Cell DestinationCell { get; set; }

        public Cell PiecePosition { get; set; }
      

        public bool IsOnInitialPosition()
        {
            return PiecePosition == DestinationCell;
        }

        public abstract bool ValidateMovement(IBoard board, IMove move);
    }
}