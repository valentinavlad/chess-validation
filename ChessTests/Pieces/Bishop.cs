using ChessTable;
using ChessTests.Helpers;
using System;
using System.Collections.Generic;

namespace ChessTests
{
    public class Bishop : Piece
    {
        private readonly List<Orientation> BishopOrientation = new List<Orientation>()
        {
            Orientation.UpLeft, Orientation.DownLeft,
            Orientation.UpRight, Orientation.DownRight,
        };

        public Bishop(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Bishop;
        }

        public override bool ValidateMovementAndReturnPiece(Board board, Move move, PieceColor playerColor, out Piece piece)
        {
            var destinationCell = board.TransformCoordonatesIntoCell(move.Coordinate);
            CheckDestinationCellAvailability(playerColor, destinationCell);
            List<Piece> findBishops = BoardAction.FindPieces(playerColor, destinationCell, BishopOrientation, PieceName.Bishop);

            piece = BoardAction.FoundedPiece(move, findBishops);
            return piece != null ? true : false;
        }

        public bool CheckForOpponentKingOnSpecificRoutes(Cell currentPosition, PieceColor playerColor)
        {
            return BoardAction.CheckForOpponentKingOnSpecificRoutes(currentPosition, playerColor, BishopOrientation);
        }
    }
}