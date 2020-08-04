using ChessTable;
using System;

namespace ChessTests
{
    public abstract class Piece
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
        public abstract bool ValidateMovementAndReturnPiece(Board board, Move move, PieceColor playerColor, out Piece piece);

        internal void CheckDestinationCellAvailability(PieceColor playerColor, Cell destinationCell)
        {
            if (destinationCell.BelongsTo(playerColor))
            {
                throw new InvalidOperationException("Invalid Move");
            }
        }

    }
}