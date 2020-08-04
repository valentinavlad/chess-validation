using ChessTable;
using ChessTests.Helpers;
using System;
using System.Collections.Generic;

namespace ChessTests.Pieces
{
    public class Rook : Piece
    {
        public Rook(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Rook;
        }

        public static Piece ValidateMovementAndReturnPiece(Board board, Move move, PieceColor playerColor)
        {
            var destinationCell = board.TransformCoordonatesIntoCell(move.Coordinate);

            CheckDestinationCellAvailability(playerColor, destinationCell);

            List<Orientation> orientations = RookOrientation();

            List<Piece> findRooks = BoardAction.FindPieces(playerColor, destinationCell, orientations, PieceName.Rook);

            return BoardAction.FoundedPiece(move, findRooks);
        }

        public bool CheckForOpponentKingOnSpecificRoutes(Cell currentPosition, PieceColor playerColor)
        {
            List<Orientation> orientations = RookOrientation();
            return BoardAction.CheckForOpponentKingOnSpecificRoutes(currentPosition, playerColor, orientations);
        }

        private static List<Orientation> RookOrientation()
        {
            return new List<Orientation>()
            {
                Orientation.Up,    Orientation.Down,
                Orientation.Right,   Orientation.Left
            };
        }
        private static void CheckDestinationCellAvailability(PieceColor playerColor, Cell destinationCell)
        {
            if (destinationCell.BelongsTo(playerColor))
            {
                throw new InvalidOperationException("Invalid Move");
            }
        }

        public override bool ValidateMovementAndReturnPiece(Board board, Move move, PieceColor playerColor, out Piece piece)
        {
            throw new NotImplementedException();
        }
    }
}
