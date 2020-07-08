using ChessTests;
using System;
using System.Collections.Generic;
using System.Text;


namespace ChessTable
{
    public class Board
    {
        private Square[,] squares;

        public Board()
        {
            squares = new Square[8,8];
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            //initialize black pieces
            squares[0, 0] = new Square(0, 0, new Piece(PieceColor.Black,PieceName.Rook));
            squares[0, 1] = new Square(0, 1, new Piece(PieceColor.Black, PieceName.Knight));
            squares[0, 2] = new Square(0, 2, new Piece(PieceColor.Black, PieceName.Bishop));
            squares[0, 3] = new Square(0, 3, new Piece(PieceColor.Black, PieceName.Queen));
            squares[0, 4] = new Square(0, 4, new Piece(PieceColor.Black, PieceName.King));
            squares[0, 5] = new Square(0, 5, new Piece(PieceColor.Black, PieceName.Bishop));
            squares[0, 6] = new Square(0, 6, new Piece(PieceColor.Black, PieceName.Knight));
            squares[0, 7] = new Square(0, 7, new Piece(PieceColor.Black, PieceName.Rook));

            //initialize black pawns
            for (int i = 1; i == 1 ; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    squares[i, j] = new Square(i, j, new Piece(PieceColor.Black, PieceName.Pawn));
                }
            }

            // initialize remaining boxes without any piece 
            for (int i = 2; i < 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    squares[i,j] = new Square(i, j, null);
                }
            }

            //initialize white pawns
            for (int i = 6; i == 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    squares[i, j] = new Square(i, j, new Piece(PieceColor.White, PieceName.Pawn));
                }
            }

            //initialize white pieces
            squares[7, 0] = new Square(7, 0, new Piece(PieceColor.White, PieceName.Rook));
            squares[7, 1] = new Square(7, 1, new Piece(PieceColor.White, PieceName.Knight));
            squares[7, 2] = new Square(7, 2, new Piece(PieceColor.White, PieceName.Bishop));
            squares[7, 3] = new Square(7, 3, new Piece(PieceColor.White, PieceName.Queen));
            squares[7, 4] = new Square(7, 4, new Piece(PieceColor.White, PieceName.King));
            squares[7, 5] = new Square(7, 5, new Piece(PieceColor.White, PieceName.Bishop));
            squares[7, 6] = new Square(7, 6, new Piece(PieceColor.White, PieceName.Knight));
            squares[7, 7] = new Square(7, 7, new Piece(PieceColor.White, PieceName.Rook));
        }
    }
}
