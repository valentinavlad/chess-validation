
using ChessTable;
using System;

namespace ChessTests
{
    public class Piece
    {
        public PieceColor pieceColor;
        public Piece(PieceColor pieceColor)
        {
            this.pieceColor = pieceColor;
        }

        public Cell CurrentPosition { get; set; }
        public Cell InitialPosition { get; set; }
        public PieceName Name { get; set; }
        public bool IsOnInitialPosition()
        {
            return InitialPosition == CurrentPosition;
        }

        protected static void CheckDestinationCellAvailability(PieceColor playerColor, Cell destinationCell)
        {
            if (destinationCell.BelongsTo(playerColor))
            {
                throw new InvalidOperationException("Invalid Move");
            }
        }

    }
}