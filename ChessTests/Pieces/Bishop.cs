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

        public override bool CheckForOpponentKingOnSpecificRoutes(Cell currentPosition, PieceColor playerColor)
        {
            return boardAction.FindKing(currentPosition, playerColor, BishopOrientation) != null ? true : false;
        
        } 

        public override bool ValidateMovement(Board board, Move move, PieceColor playerColor)
        {
            var destinationCell = board.TransformCoordonatesIntoCell(move.Coordinate);
            CheckDestinationCellAvailability(playerColor, destinationCell);
            List<Piece> findBishops = boardAction.FindPieces(playerColor, destinationCell, BishopOrientation, PieceName.Bishop);

            var piece = boardAction.FoundedPiece(move, findBishops);
            if (piece != null) move.PiecePosition = piece.CurrentPosition;
            return piece != null ? true : false;
        }
    }
}