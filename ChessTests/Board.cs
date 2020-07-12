using ChessTests;
using System;
using System.Collections.Generic;
using System.Text;


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
        public void AddPieceInCell(Cell cell)
        {
            //CHECK IF CELL IS EMPTY
            if (cells[cell.X, cell.Y].Piece == null)
            {
                cells[cell.X, cell.Y] = new Cell(cell.X, cell.Y, cell.Piece);
            } 
            //if is ocupied see what to do next
        }

        private void InitializeBoard()
        {
            //initialize black pieces
            cells[0, 0] = new Cell(0, 0, new Piece(PieceColor.Black,PieceName.Rook));
            cells[0, 1] = new Cell(0, 1, new Piece(PieceColor.Black, PieceName.Knight));
            cells[0, 2] = new Cell(0, 2, new Piece(PieceColor.Black, PieceName.Bishop));
            cells[0, 3] = new Cell(0, 3, new Piece(PieceColor.Black, PieceName.Queen));
            cells[0, 4] = new Cell(0, 4, new Piece(PieceColor.Black, PieceName.King));
            cells[0, 5] = new Cell(0, 5, new Piece(PieceColor.Black, PieceName.Bishop));
            cells[0, 6] = new Cell(0, 6, new Piece(PieceColor.Black, PieceName.Knight));
            cells[0, 7] = new Cell(0, 7, new Piece(PieceColor.Black, PieceName.Rook));

            //initialize black pawns
            for (int i = 1; i == 1 ; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    cells[i, j] = new Cell(i, j, new Piece(PieceColor.Black, PieceName.Pawn));
                }
            }

            // initialize remaining boxes without any piece 
            for (int i = 2; i < 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    cells[i,j] = new Cell(i, j, null);
                }
            }

            //initialize white pawns
            for (int i = 6; i == 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    cells[i, j] = new Cell(i, j, new Piece(PieceColor.White, PieceName.Pawn));
                }
            }

            //initialize white pieces
            cells[7, 0] = new Cell(7, 0, new Piece(PieceColor.White, PieceName.Rook));
            cells[7, 1] = new Cell(7, 1, new Piece(PieceColor.White, PieceName.Knight));
            cells[7, 2] = new Cell(7, 2, new Piece(PieceColor.White, PieceName.Bishop));
            cells[7, 3] = new Cell(7, 3, new Piece(PieceColor.White, PieceName.Queen));
            cells[7, 4] = new Cell(7, 4, new Piece(PieceColor.White, PieceName.King));
            cells[7, 5] = new Cell(7, 5, new Piece(PieceColor.White, PieceName.Bishop));
            cells[7, 6] = new Cell(7, 6, new Piece(PieceColor.White, PieceName.Knight));
            cells[7, 7] = new Cell(7, 7, new Piece(PieceColor.White, PieceName.Rook));
        }
    }
}
