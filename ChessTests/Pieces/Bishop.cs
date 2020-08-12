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

        public override bool ValidateMovement(Move move, PieceColor playerColor)
        {
            CheckDestinationCellAvailability(playerColor, move.DestinationCell);
            List<Piece> findBishops = boardAction.FindPieces(playerColor, move.DestinationCell, BishopOrientation, PieceName.Bishop);

            var piece = boardAction.FoundedPiece(move, findBishops);
            if (piece != null) move.PiecePosition = piece.CurrentPosition;
            return piece != null ? true : false;
        }
    }
}