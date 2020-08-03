using ChessTable;
using ChessTests.Helpers;
using System.Collections.Generic;

namespace ChessTests
{
    public class Queen : Piece
    {
        public Queen(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Queen;
        }

        public static Piece ValidateMovementAndReturnPiece(Board board, Move move, PieceColor playerColor)
        {
            var destinationCell = board.TransformCoordonatesIntoCell(move.Coordinate);

            CheckDestinationCellAvailability(playerColor, destinationCell);

            List<Orientation> orientations = QueenOrientation();

            List<Piece> findQueens = BoardAction.FindPieces(playerColor, destinationCell, orientations, PieceName.Queen);

            return BoardAction.FoundedPiece(move, findQueens);
        }
      
        public bool CheckForOpponentKingOnSpecificRoutes(Cell currentPosition, PieceColor playerColor)
        {
            List<Orientation> orientations = QueenOrientation();
            return BoardAction.CheckForOpponentKingOnSpecificRoutes(currentPosition, playerColor, orientations);
        }

        public static List<Orientation> QueenOrientation()
        {
            return new List<Orientation>()
            {
                Orientation.Up,  Orientation.DownLeft, Orientation.UpRight,
                Orientation.Right, Orientation.DownRight,
                Orientation.Down,
                Orientation.Left,  Orientation.UpLeft
            };
        }
    }
}
