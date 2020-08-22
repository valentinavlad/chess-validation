using ChessTests.Interfaces;
using ChessTests.Validations;
using System;
using System.Collections.Generic;

namespace ChessTests
{
    public class Pawn : Piece
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

        public override bool ValidateMovement(Move move)
        {
            Piece piece = GetPawn(move.DestinationCell, move.Color, move);

            return piece != null? true : false;
        }

        public override bool CheckForOpponentKingOnSpecificRoutes(Move move)
        {
            var orientations = move.Color == PieceColor.White 
                ? WhitePawnCaptureOrientation 
                : BlackPawnCaptureOrientation;
            return boardAction.FindPieces(move, PieceName.King, orientations).Count != 0 ? true : false;
            //return boardAction.FindKing(currentPosition, playerColor, orientations) != null ? true : false;   
        }

        private Piece GetPawn(Cell destinationCell, PieceColor playerColor, Move move)
        {
            var piece = !move.IsCapture 
                ? validation.FindPawn(destinationCell, playerColor)
                : validation.FindPawnWhoCaptures(destinationCell, playerColor, move);
    
            try
            {
                if (piece != null) move.PiecePosition = piece.CurrentPosition;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Illegal move!");
            }
            return piece;
        }
    }
}