﻿using ChessTests.Interfaces;
using ChessTests.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessTests
{
    public class Pawn : Piece, ICheckOpponentKing
    {
        private readonly List<Orientation> WhitePawnCaptureOrientation = new List<Orientation>()
                { Orientation.UpLeft, Orientation.UpRight };
        private readonly List<Orientation> BlackPawnCaptureOrientation = new List<Orientation>()
                { Orientation.DownLeft, Orientation.DownRight };
        private PawnValidation validation = new PawnValidation();
        
        public Pawn(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Pawn;
        }

        public override bool ValidateMovement(IBoard board, IMove move)
        {
            Piece piece = GetPawn(move.DestinationCell, move.PieceColor, move);

            return piece != null? true : false;
        }

        public bool CheckForOpponentKingOnSpecificRoutes(IBoard board, Move move)
        {
            var orientations = move.PieceColor == PieceColor.White 
                ? WhitePawnCaptureOrientation 
                : BlackPawnCaptureOrientation;
            return board.FindPieces(move, orientations).Count() != 0 ? true : false;
        }

        private Piece GetPawn(Cell destinationCell, PieceColor playerColor, IMove move)
        {
            var piece = !move.IsCapture 
                ? validation.FindPawn(destinationCell, playerColor)
                : validation.FindPawnWhoCaptures(destinationCell, playerColor, move);
    
            try
            {
                if (piece != null) move.PiecePosition = piece.DestinationCell;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Illegal move!");
            }
            return piece;
        }
    }
}