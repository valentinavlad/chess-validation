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

        public override bool CheckForOpponentKingOnSpecificRoutes(Move move)
        {
            return boardAction.FindPieces(move, PieceName.King, BishopOrientation).Count != 0 ? true : false;  
        } 

        public override bool ValidateMovement(Move move)
        {
            move.DestinationCell.CheckDestinationCellAvailability(move.Color);
            List<Piece> findBishops = boardAction.FindPieces(move, PieceName.Bishop, BishopOrientation);

            var piece = boardAction.FoundedPiece(move, findBishops);
            if (piece != null) move.PiecePosition = piece.CurrentPosition;
            return piece != null ? true : false;
        }
    }
}