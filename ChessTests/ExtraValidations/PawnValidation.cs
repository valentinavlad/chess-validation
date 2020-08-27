using ChessTests.Interfaces;
using System;

namespace ChessTests.Validations
{
    internal class PawnValidation
    {
        internal Pawn FindPawnWhoCaptures(Cell destinationCell, PieceColor playerColor, IMove move)
        {
            Orientation orientation = GetPawnOrientation(playerColor);
            var cell = destinationCell.Look(orientation, move.Y);
            if (cell.HasPiece() && cell.HasPiece() && cell.BelongsTo(playerColor))
            {
                return (Pawn)cell.Piece;
            }

            return null;
        }

        internal Pawn FindPawn(Cell destinationCell, PieceColor playerColor)
        {
            CheckDestinationCellHasPiece(destinationCell);
            Orientation orientation = GetPawnOrientation(playerColor);
            var cell = destinationCell.Look(orientation);

            if (cell.HasPawn() && cell.BelongsTo(playerColor))
            {
                return (Pawn)cell.Piece;
            }

            CheckDestinationCellHasPiece(cell);
            //check for two cell movements
            cell = cell.Look(orientation);

            if (cell.HasPawn() && cell.BelongsTo(playerColor))
            {
                var piece = cell.Piece;
                if (piece.IsOnInitialPosition() == false)
                {
                    throw new InvalidOperationException("Pawn is in an invalid state!");
                }
                return (Pawn)piece;
            }

            return null;
        }

        private Orientation GetPawnOrientation(PieceColor playerColor)
        {
            return playerColor == PieceColor.White ? Orientation.Down : Orientation.Up;
        }

        private void CheckDestinationCellHasPiece(Cell destinationCell)
        {
            if (destinationCell.HasPiece())
            {
                throw new InvalidOperationException("Invalid Move");
            }
        }
    }
}
