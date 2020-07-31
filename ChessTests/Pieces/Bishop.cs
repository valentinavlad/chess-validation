using ChessTable;
using ChessTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTests
{
    public class Bishop : Piece
    {

        public Bishop(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Bishop;
        }
        public static Piece ValidateMovementAndReturnPiece(Board board, Move move, PieceColor playerColor)
        {
            var destinationCell = board.TransformCoordonatesIntoCell(move.Coordinate);

            CheckDestinationCellAvailability(playerColor, destinationCell);

            List<Orientation> orientations = BishopOrientation();

            List<Piece> findBishops = BoardAction.FindPieces(playerColor, destinationCell, orientations, PieceName.Bishop);

            return BoardAction.FoundedPiece(move, findBishops);
        }


        public bool CheckForOpponentKingOnSpecificRoutes(Cell currentPosition, PieceColor playerColor)
        {
            List<Orientation> orientations = BishopOrientation();
            return BoardAction.CheckForOpponentKingOnSpecificRoutes(currentPosition, playerColor, orientations);
        }

        private static List<Orientation> BishopOrientation()
        {
            return new List<Orientation>()
            {
                Orientation.UpLeft, Orientation.DownLeft,
                Orientation.UpRight, Orientation.DownRight,
            };
        }
    }
}