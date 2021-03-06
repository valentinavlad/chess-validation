﻿using ChessTests.Interfaces;
using ChessTests.Pieces;
using System;

namespace ChessTests
{
    public class Move : IMove
    {

        //dectination cell
        public Cell DestinationCell { get; set; }
        //piece position
        public Cell PiecePosition { get; set; }
        public PieceColor PieceColor { get; set; }
        public PieceName Name { get; set; }

        public Coordinate Coordinate { get; set; }
        public string Coordinates { get; set; }
        public int Y { get; set; } = -1;
        public bool IsCheck { get; set; }
        public bool IsCheckMate { get; set; }
        public bool IsKingCastling { get; set; }
        public bool IsQueenCastling { get; set; }
        public Piece Promotion { get; set; }
        public bool IsCapture { get; set; }
 
        public void CapturePiece(IPiece attacker, Cell cellDestination)
        {
            if (!CellHasOpponentPiece(attacker, cellDestination))
            {
                throw new InvalidOperationException("Invalid move!");
            }
        
            var opponent = cellDestination.Piece;
            opponent.DestinationCell = null;
            cellDestination.Piece = null;   
        }

        public void MovePiece(IPiece piece, Cell destinationCell)
        {
            var previousPosition = piece.DestinationCell;
            destinationCell.Piece = (Piece)piece;
            previousPosition.Piece = null;
            piece.DestinationCell = destinationCell;
        }
     

     
        private bool CellHasOpponentPiece(IPiece attacker, Cell cellDestination)
        {
            var opponent = cellDestination.Piece;
            return opponent != null && opponent.PieceColor != attacker.PieceColor;
        }
    }
}
