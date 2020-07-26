
using ChessTable;
using System;

namespace ChessTests
{
    public class Piece
    {
        public Cell InitialPosition { get; set; }
        public Cell CurrentPosition { get; set; }

        public PieceColor pieceColor;
        public PieceName Name { get; set; }

        public Piece(PieceColor pieceColor)
        {
            this.pieceColor = pieceColor;
        }

        public bool IsOnInitialPosition()
        {
            return InitialPosition == CurrentPosition;
        }

        protected static void CheckDestinationCellAvailability(PieceColor playerColor, Cell destinationCell)
        {
            if (destinationCell.HasPiece() && destinationCell.Piece.pieceColor == playerColor)
            {
                throw new InvalidOperationException("Invalid Move");
            }
        }


    }
}