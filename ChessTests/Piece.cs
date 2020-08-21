﻿using ChessTests.Helpers;

namespace ChessTests
{
    public abstract class Piece 
    {
        internal BoardAction boardAction = new BoardAction();
       
        public PieceColor pieceColor;

        public Piece(PieceColor pieceColor)
        {
            this.pieceColor = pieceColor;
        }

        public Cell CurrentPosition { get; set; }

        public Cell InitialPosition { get; set; }

        public PieceName Name { get; set; }

        public bool IsOnInitialPosition()
        {
            return InitialPosition == CurrentPosition;
        }

        public abstract bool ValidateMovement(Move move);

    }
}