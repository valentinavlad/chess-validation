using ChessTests.Helpers;
using ChessTests.Interfaces;

namespace ChessTests
{
    public abstract class Piece :  IPieceProperties, IValidateMovement
    {
        internal BoardAction boardAction = new BoardAction();
       
        public PieceColor pieceColor { get; }
         public PieceName Name { get; set; }

        public Piece(PieceColor pieceColor)
        {
            this.pieceColor = pieceColor;
        }

        public Cell CurrentPosition { get; set; }

        public Cell InitialPosition { get; set; }

        public bool IsOnInitialPosition()
        {
            return InitialPosition == CurrentPosition;
        }

        public abstract bool ValidateMovement(Move move);
    }
}