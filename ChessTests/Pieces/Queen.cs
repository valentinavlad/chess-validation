﻿using ChessTests.Interfaces;
using System.Collections.Generic;

namespace ChessTests
{
    public class Queen : Piece, ICheckOpponentKing
    {
        private readonly List<Orientation> QueenOrientation = new List<Orientation>()
        {
                Orientation.Up,  Orientation.DownLeft, Orientation.UpRight,
                Orientation.Right, Orientation.DownRight, Orientation.Down,
                Orientation.Left,  Orientation.UpLeft
        };

        public Queen(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Queen;
        }

        public override bool ValidateMovement(Move move)
        {
            move.DestinationCell.CheckDestinationCellAvailability(move.Color);

            List<Piece> findQueens = boardAction.FindPieces(move, QueenOrientation);

            var piece = boardAction.FoundedPiece(move, findQueens);

            if (piece != null) move.PiecePosition = piece.CurrentPosition;

            return piece != null ? true : false;
        }

        public  bool CheckForOpponentKingOnSpecificRoutes(Move move)
        {
            return boardAction.FindPieces(move, PieceName.King, QueenOrientation).Count != 0 ? true : false;
        }
    }
}
