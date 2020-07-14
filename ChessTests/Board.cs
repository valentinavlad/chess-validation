using ChessTests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ChessTable
{
    public class Board
    {
        private Cell[,] cells;

        public Board()
        {
            cells = new Cell[8,8];
            InitializeBoard();
        }

        public void AddPieceInCell(Moves move)
        {

            //CHECK IF CELL IS EMPTY
            //check if move is valid
            if (cells[move.Piece.X, move.Piece.Y].Piece == null && IsValidMove(move))
            {
                cells[move.Piece.X, move.Piece.Y].Piece = move.Piece;
                //reset cell
                move.Piece.Moved = true;
            }
            //if is ocupied see what to do next

        }

        public Cell GetCell(int x, int y)
        {

            if (x < 0 || x > 7 || y < 0 || y > 7)
            {
                throw new Exception("Index out of bound");
            }

            return cells[x,y];
        }

        private bool IsValidMove(Moves move)
        {

            throw new NotImplementedException();
        }

        private void InitializeBoard()
        {
            //initialize black pieces
            cells[0, 0] = new Cell(new Piece(PieceColor.Black,PieceName.Rook));
            cells[0, 1] = new Cell(new Piece(PieceColor.Black, PieceName.Knight));
            cells[0, 2] = new Cell(new Piece(PieceColor.Black, PieceName.Bishop));
            cells[0, 3] = new Cell(new Piece(PieceColor.Black, PieceName.Queen));
            cells[0, 4] = new Cell(new Piece(PieceColor.Black, PieceName.King));
            cells[0, 5] = new Cell(new Piece(PieceColor.Black, PieceName.Bishop));
            cells[0, 6] = new Cell(new Piece(PieceColor.Black, PieceName.Knight));
            cells[0, 7] = new Cell(new Piece(PieceColor.Black, PieceName.Rook));

            //initialize black pawns
            for (int i = 1; i == 1 ; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    cells[i, j] = new Cell(new Piece(PieceColor.Black, PieceName.Pawn));
                }
            }

            // initialize remaining boxes without any piece 
            for (int i = 2; i < 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    cells[i,j] = new Cell(null);
                }
            }

            //initialize white pawns
            for (int i = 6; i == 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    cells[i, j] = new Cell(new Piece(PieceColor.White, PieceName.Pawn));
                }
            }

            //initialize white pieces
            cells[7, 0] = new Cell( new Piece(PieceColor.White, PieceName.Rook));
            cells[7, 1] = new Cell( new Piece(PieceColor.White, PieceName.Knight));
            cells[7, 2] = new Cell( new Piece(PieceColor.White, PieceName.Bishop));
            cells[7, 3] = new Cell( new Piece(PieceColor.White, PieceName.Queen));
            cells[7, 4] = new Cell( new Piece(PieceColor.White, PieceName.King));
            cells[7, 5] = new Cell( new Piece(PieceColor.White, PieceName.Bishop));
            cells[7, 6] = new Cell( new Piece(PieceColor.White, PieceName.Knight));
            cells[7, 7] = new Cell( new Piece(PieceColor.White, PieceName.Rook));
        }
    }
}
