using ChessTests.Interfaces;
using ChessTests.Pieces;
using System;

namespace ChessTests
{
    public class Move : ICoordinate, IMoveCastling, IMoveCheck, IPiecePosition
    {
        public Cell DestinationCell { get; set; }
        public Cell PiecePosition { get; set; }
        public PieceColor Color { get; set; }
        public PieceName PieceName { get; set; }
        public Coordinate Coordinate { get; set; }
        public string Coordinates { get; set; }
        public int Y { get; set; } = -1;
        public bool IsCapture { get; set; }
        public bool IsCheck { get; set; }
        public bool IsCheckMate { get; set; }
        public bool IsKingCastling { get; set; }
        public bool IsQueenCastling { get; set; }
        public Piece Promotion { get; set; }

        public void CapturePiece(Piece attacker, Cell cellDestination)
        {
            if (!CellHasOpponentPiece(attacker, cellDestination))
            {
                throw new InvalidOperationException("Invalid move!");
            }
        
            var opponent = cellDestination.Piece;
            opponent.CurrentPosition = null;
            cellDestination.Piece = null;   
        }

        public void MovePiece(Piece piece, Cell destinationCell)
        {
            var previousPosition = piece.CurrentPosition;
            destinationCell.Piece = piece;
            previousPosition.Piece = null;
            piece.CurrentPosition = destinationCell;
        }

        internal bool IsPiece()
        {
            IPieceProperties piece = PieceFactory.CreatePiece(PieceName, Color);
            return piece.ValidateMovement(this);
        }
        
        
        private bool CellHasOpponentPiece(Piece attacker, Cell cellDestination)
        {
            var opponent = cellDestination.Piece;
            return opponent != null && opponent.PieceColor != attacker.PieceColor;
        }
    }
}
