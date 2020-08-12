using ChessTests.Pieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests.Helpers
{
    internal class InitializeBoard
    {
        private Cell[,] cells;

        public InitializeBoard(Cell[,] cells)
        {
            this.cells = cells;
        }

        internal void InitializeBoardWithPieces()
        {

            //initialize black pieces
            cells[0, 0] = new Cell(0, 0, new Rook(PieceColor.Black), cells);
            cells[0, 1] = new Cell(0, 1, new Knight(PieceColor.Black), cells);
            cells[0, 2] = new Cell(0, 2, new Bishop(PieceColor.Black), cells);
            cells[0, 3] = new Cell(0, 3, new Queen(PieceColor.Black), cells);
            cells[0, 4] = new Cell(0, 4, new King(PieceColor.Black), cells);
            cells[0, 5] = new Cell(0, 5, new Bishop(PieceColor.Black), cells);
            cells[0, 6] = new Cell(0, 6, new Knight(PieceColor.Black), cells);
            cells[0, 7] = new Cell(0, 7, new Rook(PieceColor.Black), cells);

            //initialize black pawns
            for (int i = 1; i == 1; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    cells[i, j] = new Cell(i, j, new Pawn(PieceColor.Black), cells);
                }
            }

            // initialize remaining boxes without any piece 
            for (int i = 2; i < 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    cells[i, j] = new Cell(i, j, null, cells);
                }
            }

            //initialize white pawns
            for (int i = 6; i == 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    cells[i, j] = new Cell(i, j, new Pawn(PieceColor.White), cells);
                }
            }

            //initialize white pieces
            cells[7, 0] = new Cell(7, 0, new Rook(PieceColor.White), cells);
            cells[7, 1] = new Cell(7, 1, new Knight(PieceColor.White), cells);
            cells[7, 2] = new Cell(7, 2, new Bishop(PieceColor.White), cells);
            cells[7, 3] = new Cell(7, 3, new Queen(PieceColor.White), cells);
            cells[7, 4] = new Cell(7, 4, new King(PieceColor.White), cells);
            cells[7, 5] = new Cell(7, 5, new Bishop(PieceColor.White), cells);
            cells[7, 6] = new Cell(7, 6, new Knight(PieceColor.White), cells);
            cells[7, 7] = new Cell(7, 7, new Rook(PieceColor.White), cells);
        }

        internal void InitializeBoardWithoutPieces()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    cells[i, j] = new Cell(i, j, cells);
                }
            }
        }


 

    }
}
