using ChessTable;
using ChessTests.Helpers;
using System;
using System.Collections.Generic;

namespace ChessTests
{
    public class Queen : Piece
    {
        private readonly List<Orientation> QueenOrientation = new List<Orientation>()
        {
                Orientation.Up,  Orientation.DownLeft, Orientation.UpRight,
                Orientation.Right, Orientation.DownRight,
                Orientation.Down,
                Orientation.Left,  Orientation.UpLeft

        };

        public Queen(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Queen;
        }

        public override bool ValidateMovementAndReturnPiece(Board board, Move move, PieceColor playerColor, out Piece piece)
        {
            var destinationCell = board.TransformCoordonatesIntoCell(move.Coordinate);
            CheckDestinationCellAvailability(playerColor, destinationCell);
            List<Piece> findQueens = BoardAction.FindPieces(playerColor, destinationCell, QueenOrientation, PieceName.Queen);
            piece = BoardAction.FoundedPiece(move, findQueens);

            return piece != null ? true : false;
        }
      
        public bool CheckForOpponentKingOnSpecificRoutes(Cell currentPosition, PieceColor playerColor)
        {
            return BoardAction.CheckForOpponentKingOnSpecificRoutes(currentPosition, playerColor, QueenOrientation);
        }
    }
}
